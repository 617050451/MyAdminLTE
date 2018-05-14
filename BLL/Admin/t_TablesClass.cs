using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace BLL
{
    public class t_TablesClass
    {
        public Model.t_Tables TableModel = new Model.t_Tables();
        public string OneFileName = string.Empty;
        public string ColumnsJson = string.Empty;
        public string GUIDValue = string.Empty;
        public t_TablesClass(string GUID)
        {
            GUIDValue = GUID;
            TableModel = DataTableToModel<Model.t_Tables>(string.Format("select * from [t_Tables] WHERE [GUID] ='{0}'", GUIDValue))[0];
            OneFileName = GetTopOneFileName(TableModel.TableName);
        }
        //获取表的第一个字段名
        public string GetTopOneFileName(string TableName)
        {
            string sql = string.Format("Select  top(1)Name FROM SysColumns Where id=Object_Id('{0}')", TableName);
            return DAL.SQLDBHelpercs.ExecuteReader(sql);
        }
        //获取TableFiel信息
        public DataTable GetTableFieldInfo(string GUIDValue)
        {
            string sql = string.Format("select  * from [t_TableField] WHERE [TableGUID] ='{0}' order by FieldOrder ", GUIDValue);
            return GetDataTable(sql);
        }
        //获取高级查询
        public string SetStrWhere(DataTable dt)
        {
            string strWhere = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["value"].ToString() != "")
                    {
                        string[] list = dt.Rows[i]["name"].ToString().Split('|');
                        string type = list[1];
                        string name = list[0];
                        switch (type)
                        {
                            case "1":
                                strWhere += (strWhere != "" ? " and " : "") + name + " like '%" + dt.Rows[i]["value"].ToString() + "%'";
                                break;
                            case "2":
                                if (dt.Rows[i]["value"].ToString() != "0")
                                    strWhere += (strWhere != "" ? " and " : "") + name + " = '" + dt.Rows[i]["value"].ToString() + "'";
                                break;
                            case "3":
                                strWhere += (strWhere != "" ? " and " : "") + " convert(varchar(50)," + name + ",23) = '" + dt.Rows[i]["value"].ToString() + "'";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return strWhere;
        }
        //设置数据总条数SQL拼接
        public string GetTSQL(string SQL, string fileName, string strWhere, string order, bool isCbGuid)
        {
            SQL = SQL.ToUpper().Replace("SELECT", "SELECT TOP(20000) ");
            string sqlstr = " SELECT " + fileName;
            if (isCbGuid)
                sqlstr += ",GUID AS CbGuid";
            sqlstr += " from (" + SQL + ") as NEWTABLE";
            if (strWhere != "")
                sqlstr += " where " + strWhere;
            if (order != "")
                sqlstr += " order by " + order;
            return sqlstr + ";";
        }
        // 设置分页SQL语句拼接
        public string PageBySQL(string SQL, string TableName, string OneFileName, string SQLWhere, string SQLOrder, int PageIndex, int PageLimit)
        {
            SQL = SQL.ToUpper().Replace("SELECT", "SELECT TOP(20000) ");
            int minNum = (Convert.ToInt32(PageIndex) - 1) * Convert.ToInt32(PageLimit) + 1;
            int maxNum = Convert.ToInt32(PageIndex) * Convert.ToInt32(PageLimit);
            if (!string.IsNullOrEmpty(SQLWhere))
            {
                SQLWhere = " where " + SQLWhere;
            }
            if (string.IsNullOrEmpty(SQLOrder))
            {
                SQLOrder = OneFileName + " ASC";
            }
            string sql = string.Format(@"SELECT " + OneFileName + " AS 'ItemID',* FROM (SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS NewRowID,* FROM ({1}) AS NOPOSTNEWTABLE {2})NOPOTST WHERE NOPOTST.NewRowID >={3} AND NOPOTST.NewRowID <= {4}", SQLOrder, SQL, SQLWhere, minNum, maxNum);
            return sql + ";";
        }
        //返回一个list<MODEL>
        public static List<Object> SelectModel(string sql,string tableName)
        {
            List<Object> Listobjectdata = new List<Object>();
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ObjectData objectdata = new ObjectData(tableName);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        objectdata.SetValue(dt.Columns[j].ColumnName, dt.Rows[i][j].ToString());
                    }
                    Listobjectdata.Add(objectdata);
                }
            }
            return Listobjectdata;
        }
        //返回DataTable
        public static DataTable GetDataTable(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        //返回DataSet
        public static DataSet GetDataSet(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            return ds;
        }
        //返回实体类对象
        public static List<T> DataTableToModel<T>(string sql) where T : class, new()
        {
            List<T> itemlist = null;
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable source = ds.Tables[0];
                itemlist = new List<T>();
                T item = null;
                Type targettype = typeof(T);
                Type ptype = null;
                Object value = null;
                foreach (DataRow dr in source.Rows)
                {
                    item = new T();
                    foreach (PropertyInfo pi in targettype.GetProperties())
                    {
                        if (pi.CanWrite && source.Columns.Contains(pi.Name))
                        {
                            ptype = Type.GetType(pi.PropertyType.FullName);
                            value = Convert.ChangeType(dr[pi.Name], ptype);
                            pi.SetValue(item, value, null);
                        }
                    }
                    itemlist.Add(item);
                }
            }
            return itemlist;
        }
        //设置表格
        public string GetTableHtml()
        {
            DataTable dt = GetTableFieldInfo(GUIDValue);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbjson = new StringBuilder();
            string BntHtml = string.Empty;
            if (TableModel.IsChoice == 1)
            {
                BntHtml += "<button name = 'UpdateItemID' type = 'button' class='btn btn-warning  btn-xs' value='\" + data + \"'>修　改</button>&nbsp;";
            }
            sb.Append("<thead><tr>");
            if (TableModel.IsChoice == 1)
            {
                string html = "<input type=\"checkbox\" id=\"selectAll\" class=\"table-checkable\" >";
                sb.Append("<th style=\"width:13px;\">" + html + "</th>");
                sbjson.Append("{\"data\": \"ItemID\", render: function (data, type, row) { return \"<input  name='checkboxItemID' type='checkbox' class='table-checkable'  value='\" + data + \"'/>\"}},");
            }
            else if (TableModel.IsDelete == 1)
            {
                BntHtml += "<button name = 'DeleteItemID' type = 'button' class='btn btn-danger  btn-xs' value='\" + data + \"'>删　除</button>&nbsp;";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FieldStatusID"].ToString() == "1")
                {
                    sb.Append("<th>" + dt.Rows[i]["FieldValue"].ToString() + "</th>");
                    string data = "";
                    if (dt.Rows[i]["FieldData"].ToString() != "")
                        data = ", render: function (data, type, row) { return  " + dt.Rows[i]["FieldData"].ToString() + " }";
                    sbjson.Append("{\"data\": \"" + dt.Rows[i]["FieldKey"].ToString() + "\"" + data + "},");//,\"sClass\": \"text-center\"
                }
            }
            if (!string.IsNullOrEmpty(BntHtml))
            {
                sb.Append("<th>操作</th>");
                sbjson.Append("{\"data\": \"ItemID\", render: function (data, type, row) { return \"" + BntHtml + "\"}}");
            }
            sb.Append("</tr></thead>");
            ColumnsJson = "[" + sbjson.ToString().TrimEnd(',') + "]";
            return sb.ToString();
        }
        //设置高级查询
        public string SetStrWhereHtml()
        {
            DataTable dt = GetTableFieldInfo(GUIDValue);
            string strHtml = "";
            if (TableModel.IsWhere == 1)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string type = dt.Rows[i]["SelectType"].ToString();
                        switch (type)
                        {
                            case "1":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（模糊查询）</span></label >";
                                strHtml += "<input type=\"text\" name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\"  class=\"form-control\" placeholder=\"" + dt.Rows[i]["FieldValue"].ToString() + "\" />";
                                strHtml += "</div>";
                                break;
                            case "2":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（下拉查询）</span></label >";
                                strHtml += "<select name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\" class=\"form-control select2 select2-hidden-accessible\"  tabindex=\"-1\" aria-hidden=\"true\" >";
                                strHtml += "<option selected = \"selected\" value = \"0\" >全部</option >";
                                string tsql = dt.Rows[i]["SelectData"].ToString();
                                DataTable tsqldt = GetDataTable(tsql);
                                if (tsqldt != null && tsqldt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < tsqldt.Rows.Count; j++)
                                    {
                                        strHtml += "<option value = \"" + tsqldt.Rows[j][1].ToString() + "\" >" + tsqldt.Rows[j][0].ToString() + "</option >";
                                    }
                                }
                                strHtml += "</select>";
                                strHtml += "</div>";
                                break;
                            case "3":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（等于查询）<span></label >";
                                strHtml += "<input type=\"text\" name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\" data-type=\"datepicker\"  class=\"form-control\" placeholder=\"" + dt.Rows[i]["FieldValue"].ToString() + "\" />";
                                strHtml += "</div>";
                                break;
                            default:
                                break;
                        }
                    }
                    strHtml += "<div class=\"col-sm-1 table-p\" style=\"margin-top:30px;\"><button type =\"button\" class=\"btn btn-danger pull-right btn-block btn-primary\" onclick=\"getJsonData('select')\">查询</button></div>";
                }
            }
            return strHtml;
        }
        //设置按钮
        public string SetBntHtml()
        {
            string bntHtml = "";
            if (TableModel.IsDelete == 1 && TableModel.IsChoice == 1)
                bntHtml += "<button name=\"DeleteItemID\" type=\"button\" class=\"btn btn-danger btn-xs\">删　除</button>&nbsp;";
            if (TableModel.IsInsert == 1)
                bntHtml += "<button name=\"InsertItemID\" type=\"button\" class=\"btn btn-success btn-xs\">新　增</button>&nbsp;";
            return bntHtml;
        }
        //设置Sum显示
        public string SetSumHtml(string Strwhere)
        {
            string[] color = { "success", "primary", "info", "warning", "danger", "default" };
            string result = ",\"sumHtml\":\"";
            string[] list = TableModel.CountData.Split('|');
            string sql = " select {0} from " + TableModel.TableName + (Strwhere == "" ? "" : " where " + Strwhere);
            for (int i = 0; i < list.Length; i++)
            {
                string resul = list[i];
                Regex reg = new Regex("(?<={).*?(?=})", RegexOptions.IgnoreCase);
                MatchCollection mc = reg.Matches(resul);
                foreach (Match m in mc)
                {
                    resul = resul.Replace("{" + m.Value + "}", GetDataViewSQL(string.Format(sql, m.Value)));
                }
                result += "<span class='label label-" + color[i] + "' style='font-size: small'>" + resul + "</span>&nbsp;";
            }
            return result;
        }
        //获取表格数据Json
        public  string GetDataListJson(DataTable dt, int PageStart, int PageIndex, int PageSize, string order)
        {
            string strwhere = SetStrWhere(dt);
            string sumsqlStr = GetTSQL(TableModel.SQL, "COUNT(" + OneFileName + ") as COUNTS", strwhere, "", false);
            string sqlStr = PageBySQL(TableModel.SQL, TableModel.TableName, OneFileName, strwhere, order, PageIndex, PageSize);
            DataSet ds = GetDataSet(sqlStr + sumsqlStr);
            if (ds != null)
            {
                DataTable tableJson = ds.Tables[0];
                DataTable tableSum = ds.Tables[1];
                if (tableJson != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"total\":" + tableSum.Rows[0]["COUNTS"].ToString() + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                    string datatablejson = JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                    sb.Append(datatablejson);
                    sb.Append(SetSumHtml(strwhere));
                    sb.Append("\"}");
                    return sb.ToString().Replace("\n", "");
                }
                else
                {
                    return "{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[]}";
                }
            }
            else
            {
                return "{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[]}";
            }
        }
        //获取单条数据
        public  string GetDataViewJson(string IdentityValue)
        {
            string sql = string.Format("select * from {0} where {1}='{2}'", TableModel.TableName, OneFileName, IdentityValue);
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return JsonHelper.DataTableToJsonWithJsonNet(tableJson);
            else
                return "";
        }
        //获取单行单数据
        public string GetDataViewSQL(string sql)
        {
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return tableJson.Rows[0][0].ToString();
            else
                return "";
        }
        //新增数据
        public  bool InsertModel(ObjectData model)
        {
            var obj = model.GetValues();
            StringBuilder commandText = new StringBuilder(" insert into ");
            string tableName = model.TableName;//表名称
            var pros = model.GetValues().Keys;//所有字段名称
            StringBuilder fieldStr = new StringBuilder();//拼接需要插入数据库的字段
            StringBuilder paramStr = new StringBuilder();//拼接每个字段对应的参数
            List<SqlParameter> paramlist = new List<SqlParameter>();
            foreach (var item in pros)
            {
                string fieldName = item;
                if (!fieldName.ToUpper().Equals(OneFileName == null ? "" : OneFileName.ToUpper()))
                {
                    var fieldValue = model.GetValue(fieldName);
                    if (model.IsSet(fieldName))//是否赋值了
                    {
                        //非自动增长字段才加入SQL语句
                        fieldStr.Append("[" + fieldName + "],");
                        paramStr.Append("@" + fieldName + ",");
                        if (fieldValue == null) fieldValue = DBNull.Value;//如果该值为空的话,则将其转化为数据库的NULL
                        paramlist.Add(new SqlParameter(fieldName, fieldValue));//给每个参数赋值
                    }
                }
            }
            SqlParameter[] param = paramlist.ToArray();
            commandText.Append(tableName);
            commandText.Append(" ( ");
            commandText.Append(fieldStr.ToString().TrimEnd(','));
            commandText.Append(" ) values ( ");
            commandText.Append(paramStr.ToString().TrimEnd(','));
            commandText.Append(" ) ");//拼接成完整的字符串
            return DAL.SQLDBHelpercs.ExecuteNonQuery(commandText.ToString(), param, "sql");
        }
        //修改
        public bool UpdateModel(ObjectData model, string IdentityValue)
        {
            var obj = model.GetValues();
            StringBuilder commandText = new StringBuilder(" update ");
            string tableName = model.TableName;//表名称
            var pros = model.GetValues().Keys;//所有字段名称
            StringBuilder fieldStr = new StringBuilder();//拼接需要插入数据库的字段
            List<SqlParameter> paramlist = new List<SqlParameter>();
            foreach (var item in pros)
            {
                string fieldName = item;
                if (!fieldName.ToUpper().Equals(OneFileName == null ? "" : OneFileName.ToUpper()))
                {
                    if (model.IsSet(fieldName))//是否赋值了
                    {
                        var fieldValue = model.GetValue(fieldName);
                        //非自动增长字段才加入SQL语句
                        fieldStr.Append("[" + fieldName + "]=@" + fieldName + ",");
                        if (fieldValue == null) fieldValue = DBNull.Value;//如果该值为空的话,则将其转化为数据库的NULL
                        paramlist.Add(new SqlParameter(fieldName, fieldValue));//给每个参数赋值
                    }
                }
            }
            SqlParameter[] param = paramlist.ToArray();
            commandText.Append(tableName);
            commandText.Append(" set ");
            commandText.Append(fieldStr.ToString().TrimEnd(','));
            commandText.Append(" where " + OneFileName + "='" + IdentityValue + "'");//拼接成完整的字符串
            return DAL.SQLDBHelpercs.ExecuteNonQuery(commandText.ToString(), param, "sql");
        }
        //删除
        public  bool DeleteData(string IdentityValue)
        {
            string DelSql = string.Format("DELETE FROM [t_Tables] WHERE {0} IN({1})", OneFileName, IdentityValue);
            return DAL.SQLDBHelpercs.ExecuteNonQuery(DelSql, null, "sql");
        }
    }
}
