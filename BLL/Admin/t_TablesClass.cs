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
        public DataTable TableFieldInfo = null;
        public DataTable TableFieldInsertInfo = null;
        public static string OneFileName = string.Empty;
        public string ColumnsJson = string.Empty;
        public string GUIDValue = string.Empty;
        public t_TablesClass(string GUID)
        {
            GUIDValue = GUID;
            TableModel = BaseClass.DataTableToModel<Model.t_Tables>(string.Format("select * from [t_Tables] WHERE [GUID] ='{0}'", GUIDValue))[0];
            OneFileName = GetTopOneFileName();
            GetTableFieldInfo();
            GetTableFieldInsertInfo();
        }
        //获取表的第一个字段名
        public  string GetTopOneFileName()
        {
            string sql = string.Format("Select  top(1)Name FROM SysColumns Where id=Object_Id('{0}')", TableModel.TableName);
            return DAL.SQLDBHelpercs.ExecuteReaderView(sql, null);
        }
        //获取TableFiel信息
        public void GetTableFieldInfo()
        {
            string sql = string.Format("select  * from [t_TableField] WHERE [TableGUID] ='{0}' order by FieldOrder ", GUIDValue);
            TableFieldInfo = BaseClass.GetDataTable(sql);
        }
        //获取TableFielInsert信息
        public void GetTableFieldInsertInfo()
        {
            string sql = string.Format("select  * from [t_TableFieldInert] WHERE [TableGUID] ='{0}' order by FieldOrder ", GUIDValue);
            TableFieldInsertInfo = BaseClass.GetDataTable(sql);
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
                                if (dt.Rows[i]["value"].ToString() != "00")
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
        //获取更多按钮数据
        public DataTable GetMoreButtonsInfo()
        {
            string sql = string.Format("select * from t_MoreButtons where BntIsEnable=1 and TableGUID='{0}'", TableModel.GUID);
            return BaseClass.GetDataTable(sql);
        }
        //设置表格
        public string GetTableHtml()
        {
            DataTable dt = TableFieldInfo;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbjson = new StringBuilder();
            string BntHtml = string.Empty;
            DataTable MoreBntDt = GetMoreButtonsInfo();
            if (MoreBntDt != null && MoreBntDt.Rows.Count > 0)
            {
                foreach (DataRow row in MoreBntDt.Rows)//onclick='MoreBntClick(\" + JSON.stringify(row) + \",this)'
                {
                    BntHtml += "<button bnt-click='MoreBntClick'   style='margin:2px;' bnt-action='" + row["BntAction"] + "' bnt-confirmtext='" + row["ConfirmText"] + "' bnt-actioncontent='" + (row["BntAction"].ToString() != "4" ? row["BntActionContent"].ToString() : "") + "' type = 'button' class='btn btn-primary  btn-xs' value='\" + data + \"'>" + row["BntName"] + "</button>";
                }
            }
            if (TableModel.IsChoice == 1)
                BntHtml += "<button name='UpdateItemID' bnt-click = 'UpdateItemID' style='margin:2px;' type = 'button' class='btn btn-warning  btn-xs' value='\" + data + \"'>修　改</button>";
            sb.Append("<thead><tr>");
            if (TableModel.IsChoice == 1)
            {
                string html = "<input bnt-click='SelectAll' name='SelectAll' type=\"checkbox\"   class=\"table-checkable\" >";
                sb.Append("<th style=\"width:13px;\">" + html + "</th>");
                sbjson.Append("{\"data\": \"ItemID\", render: function (data, type, row) { return \"<input  bnt-click='CheckBoxItemID' name='CheckBoxItemID' type='checkbox' class='table-checkable'  value='\" + data + \"'/>\"}},");
            }
            else if (TableModel.IsDelete == 1)
                BntHtml += "<button  name = 'DeleteItemID' bnt-click = 'DeleteItemID'  type = 'button' class='btn btn-danger  btn-xs' value='\" + data + \"'>删　除</button>&nbsp;";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FieldStatusID"].ToString() == "1")
                {
                    sb.Append("<th>" + dt.Rows[i]["FieldValue"].ToString() + "</th>");
                    string data = SetFieldDataType(dt.Rows[i]["FieldDataType"].ToString(), dt.Rows[i]["FieldData"].ToString(), dt.Rows[i]["FieldKey"].ToString());
                    sbjson.Append("{\"data\": \"" + dt.Rows[i]["FieldKey"].ToString() + "\"" + data + "},");
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
        //解析转换显示
        public string SetFieldDataType(string FieldDataType, string FieldData, string FieldKey)
        {
            string data = string.Empty;
            if (FieldDataType == "1")
                data = ", render: function (data, type, row) { return  data }";
            if (FieldDataType == "2")
                data = ", render: function (data, type, row) { return  " + FieldData + " }";
            if (FieldDataType == "3")
                data = ", render: function (data, type, row) { GetFieldKeyValue(row,'" + FieldKey + "',function (reData) { data=reData;}); return data;}";
            if (FieldDataType == "4")
            {
                var obj = "data";
                if (FieldData == "yearM")
                    obj = " SetDateTime(data,\"yyyy-MM\");";
                else if (FieldData == "yearMzw")
                    obj = " SetDateTime(data,\"yyyy年MM月\");";
                else if (FieldData == "date")
                    obj = " SetDateTime(data,\"yyyy-MM-dd\");";
                else if (FieldData == "datezw")
                    obj = " SetDateTime(data,\"yyyy年MM月dd日 \");";
                else if (FieldData == "time1")
                    obj = " SetDateTime(data,\"yyyy-MM-dd HH:mm\"); ";
                else if (FieldData == "time1zw")
                    obj = " SetDateTime(data,\"yyyy年MM月dd HH时mm分\"); ";
                else if (FieldData == "time2")
                    obj = " SetDateTime(data,\"yyyy-MM-dd HH:mm:ss\"); ";
                else if (FieldData == "time2zw")
                    obj = " SetDateTime(data,\"yyyy年MM月dd HH时mm分ss秒\"); ";
                else if (FieldData == "img")
                    data = ", render: function (data, type, row) {  SetImgUrl(row,data,function (reData) { data=reData;}); return data; }";
                else
                    data = ", render: function (data, type, row) {  return " + obj + " }";
            }
            return data;
        }
        //设置高级查询
        public string SetStrWhereHtml()
        {
            DataTable dt = TableFieldInfo;
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
                                strHtml += "<option selected = \"selected\" value = \"00\" >全部</option >";
                                string data = dt.Rows[i]["SelectData"].ToString();
                                DataTable objdata = JsonHelper.DeserializeJsonToObject<DataTable>(data);
                                if (objdata != null && objdata.Rows.Count > 0)
                                {

                                    for (int j = 0; j < objdata.Rows.Count; j++)
                                    {
                                        if (objdata.Rows[j][0].ToString().ToUpper() == "SQL")
                                        {
                                            var sqldata = objdata.Rows[j][1].ToString();
                                            DataTable tsqldt = BaseClass.GetDataTable(BaseClass.GetValueForKey(sqldata));
                                            if (tsqldt != null && tsqldt.Rows.Count > 0)
                                            {
                                                for (int m = 0; m < tsqldt.Rows.Count; m++)
                                                {
                                                    strHtml += "<option value = \"" + tsqldt.Rows[m][1].ToString() + "\" >" + tsqldt.Rows[m][0].ToString() + "</option >";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strHtml += "<option value = \"" + objdata.Rows[j][1].ToString() + "\" >" + objdata.Rows[j][0].ToString() + "</option >";
                                        }
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
                    strHtml += "<div bnt-click=\"Select\" class=\"col-sm-1 table-p\" style=\"margin-top:30px;\"><button type =\"button\" class=\"btn btn-danger pull-right btn-block btn-primary\">查询</button></div>";
                }
            }
            return strHtml;
        }
        //设置按钮
        public string SetBntHtml()
        {
            string bntHtml = "";
            if (TableModel.IsDelete == 1 && TableModel.IsChoice == 1)  
                bntHtml += "<button name=\"DeleteItemID\" bnt-click type=\"button\" class=\"btn btn-danger btn-xs\">删　除</button>&nbsp;";
            if (TableModel.IsInsert == 1)
                bntHtml += "<button name=\"InsertItemID\"  bnt-click=\"InsertItemID\" type=\"button\" class=\"btn btn-success btn-xs\">新　增</button>&nbsp;";
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
                    resul = resul.Replace("{" + m.Value + "}", BaseClass.GetDataViewSQL(string.Format(sql, m.Value)));
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
            DataSet ds = BaseClass.GetDataSet(sqlStr + sumsqlStr);
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
        //获取表格单条数据
        public string GetDataViewJson(string IdentityValue)
        {
            string sql = string.Format("select * from {0} where {1}='{2}'", TableModel.TableName, OneFileName, IdentityValue);
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return JsonHelper.DataTableToJsonWithJsonNet(tableJson);
            else
                return "";
        }
        //获取显示转换数据
        public string GetFieldKeyValue(Dictionary<string, string> data)
        {
            DataRow[] dr = TableFieldInfo.Select("FieldKey='" + data["FieldKey"] + "'");
            var fieldata = dr[0]["FieldData"].ToString();
            foreach (var item in data)
            {
                fieldata = fieldata.Replace("row." + item.Key, item.Value);
            }
            return BaseClass.GetDataViewSQL(BaseClass.GetValueForKey(fieldata));
        }
        //新增数据
        public string InsertModel(ObjectData model)
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
                if (!fieldName.ToUpper().Equals(OneFileName.ToUpper()))
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
            commandText.Append(";select SCOPE_IDENTITY()");
            return DAL.SQLDBHelpercs.ExecuteReaderView(commandText.ToString(), param);
        }
        //新增数据
        public string InsertModel(ObjectData model, string ItemGuID)
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
                if (!fieldName.ToUpper().Equals(OneFileName.ToUpper()))
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
            //非自动增长字段才加入SQL语句
            fieldStr.Append("[GUID]");
            paramStr.Append("@GUID");
            paramlist.Add(new SqlParameter("GUID", ItemGuID));//给每个参数赋值
            SqlParameter[] param = paramlist.ToArray();
            commandText.Append(tableName);
            commandText.Append(" ( ");
            commandText.Append(fieldStr.ToString().TrimEnd(','));
            commandText.Append(" ) values ( ");
            commandText.Append(paramStr.ToString().TrimEnd(','));
            commandText.Append(" ) ");//拼接成完整的字符串
            commandText.Append(";select '" + ItemGuID + "'");
            return DAL.SQLDBHelpercs.ExecuteReaderView(commandText.ToString(), param);
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
            string DelSql = string.Format("DELETE FROM {0} WHERE {1} IN({2})", TableModel.TableName, OneFileName, IdentityValue);
            return DAL.SQLDBHelpercs.ExecuteNonQuery(DelSql, null, "sql");
        }
        //判断dt
        public static bool estimate(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //显示页面修改保存
        public bool SaveUpdateTableInfo(DataTable TableInfo)
        {
            StringBuilder sbSQL = new StringBuilder();
            string str = "";
            if (estimate(TableInfo))
            {
                sbSQL.Append(" update t_Tables set ");
                for (int j = 0; j < TableInfo.Rows.Count; j++)
                {
                    str += string.Format("[{0}]='{1}',", TableInfo.Rows[j]["name"].ToString(), TableInfo.Rows[j]["value"].ToString().Replace("'", "''"));
                }
                sbSQL.Append(str.TrimEnd(',') + string.Format(" where guid ='{0}' ; ", GUIDValue));
            }
            return DAL.SQLDBHelpercs.ExecuteNonQuery(sbSQL.ToString(), null, "sql");
        }
        public  bool SaveUpdateTable(string TableName, DataTable TableKeyInfo)
        {
            string FieldKey = "";
            StringBuilder sbSQL = new StringBuilder();
            string str = "";
            if (estimate(TableKeyInfo))
            {
                for (int i = 0; i < TableKeyInfo.Rows.Count; i++)
                {
                    if (TableKeyInfo.Rows[i]["name"].ToString() == "FieldKey")
                    {
                        if (i > 0)
                        {
                            sbSQL.Append(str.TrimEnd(','));
                            sbSQL.Append(string.Format(" where TableGUID ='{0}' and FieldKey = '{1}'; ", GUIDValue, FieldKey));
                            str = "";
                        }
                        FieldKey = TableKeyInfo.Rows[i]["value"].ToString();
                        sbSQL.Append(" update "+ TableName + " set ");
                    }
                    else
                    {
                        str += string.Format("[{0}]='{1}',", TableKeyInfo.Rows[i]["name"].ToString(), TableKeyInfo.Rows[i]["value"].ToString());
                    }
                }
                sbSQL.Append(string.Format(str.TrimEnd(',') + " where TableGUID ='{0}' and FieldKey = '{1}'; ", GUIDValue, FieldKey));
            }
            return DAL.SQLDBHelpercs.ExecuteNonQuery(sbSQL.ToString(), null, "sql");
        }
        //自动排序
        public bool SetOrder()
        {
            DataTable dt = BaseClass.GetDataTable(TableModel.SQL.ToUpper().Replace("SELECT", "SELECT TOP(1) "));
            if (dt != null && dt.Columns.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("update  [t_TableField] set FieldOrder=" + (i + 1) + " where FieldKey='" + dt.Columns[i].ColumnName + "'; ");
                }
                return DAL.SQLDBHelpercs.ExecuteNonQuery(sb.ToString(), null, "sql");
            }
            else
                return false;
        }
        //自动排序
        public bool SetInsertOrder()
        {
            DataTable dt = BaseClass.GetDataTable(TableModel.SQL.ToUpper().Replace("SELECT", "SELECT TOP(1) "));
            if (dt != null && dt.Columns.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append("update  [t_TableFieldInert] set FieldOrder=" + (i + 1) + " where FieldKey='" + dt.Columns[i].ColumnName + "'; ");
                }
                return DAL.SQLDBHelpercs.ExecuteNonQuery(sb.ToString(), null, "sql");
            }
            else
                return false;
        }
        //更新TableFieldInfo数据
        public bool SetTableFieldInfo()
        {
            DataTable newdt = BaseClass.GetDataTable(TableModel.SQL.ToUpper().Replace("SELECT", "SELECT TOP(1) "));
            if (newdt != null && newdt.Columns.Count > 0)
            {
                DataTable dt = BaseClass.GetDataTable(string.Format("SELECT FieldKey FROM  [t_TableField] WHERE TableGUID='{0}'", TableModel.GUID));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newdt.Columns.Count; i++)
                {
                    DataRow[] drs = dt.Select("FieldKey='" + newdt.Columns[i].ColumnName + "'");
                    if (drs.Length == 0)
                        sb.Append(string.Format(" INSERT INTO [t_TableField] (TableGUID,FieldKey,FieldValue,FieldOrder) VALUES('{0}','{1}','{2}','{3}');", TableModel.GUID, newdt.Columns[i].ColumnName, newdt.Columns[i].ColumnName, i + 1));
                    else
                        dt.Rows.Remove(drs[0]);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append(string.Format(" DELETE [t_TableField] WHERE TableGUID='{0}' AND FieldKey='{1}';", TableModel.GUID, dt.Rows[i]["FieldKey"].ToString()));
                }
                StringBuilder insertsb = new StringBuilder();
                DataTable InsertDt = BaseClass.GetDataTable(string.Format("SELECT FieldKey FROM t_TableFieldInert WHERE TableGUID='{0}'", TableModel.GUID));
                for (int i = 0; i < newdt.Columns.Count; i++)
                {
                    DataRow[] drs = InsertDt.Select("FieldKey='" + newdt.Columns[i].ColumnName + "'");
                    if (drs.Length == 0)
                        insertsb.Append(string.Format(" INSERT INTO [t_TableFieldInert] (TableGUID,FieldKey,FieldValue,FieldOrder) VALUES('{0}','{1}',(SELECT  TOP(1) FieldValue FROM  [t_TableField] WHERE FieldKey='{2}' AND TableGUID='{3}'),'{4}');", TableModel.GUID, newdt.Columns[i].ColumnName, newdt.Columns[i].ColumnName, TableModel.GUID, i + 1));
                    else
                        dt.Rows.Remove(drs[0]);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    insertsb.Append(string.Format(" DELETE [t_TableFieldInert] WHERE TableGUID='{0}' AND FieldKey='{1}';", TableModel.GUID, dt.Rows[i]["FieldKey"].ToString()));
                }
                if (!string.IsNullOrWhiteSpace(sb.ToString()))
                    DAL.SQLDBHelpercs.ExecuteNonQuery(sb.ToString(), null, "sql");
                if (!string.IsNullOrWhiteSpace(insertsb.ToString()))
                    DAL.SQLDBHelpercs.ExecuteNonQuery(insertsb.ToString(), null, "sql");
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
