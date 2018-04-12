using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
namespace BLL
{
    public class BaseClass
    {
        public BaseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //添加
        public static bool insertModel(object model, string identityName)
        {
            StringBuilder commandText = new StringBuilder(" insert into ");
            Type type = model.GetType();
            string tableName = type.Name;//表名称
            PropertyInfo[] pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);//所有字段名称
            StringBuilder fieldStr = new StringBuilder();//拼接需要插入数据库的字段
            StringBuilder paramStr = new StringBuilder();//拼接每个字段对应的参数
            int len = pros.Length;
            SqlParameter[] param = new SqlParameter[len];
            if (!"".Equals(identityName) && null != identityName) param = new SqlParameter[len - 1];//如果有自动增长的字段,则该字段不需要SqlParameter
            int paramLIndex = 0;
            for (int i = 0; i < len; i++)
            {
                string fieldName = pros[i].Name;
                if (!fieldName.ToUpper().Equals(identityName == null ? "" : identityName.ToUpper()))
                {
                    //非自动增长字段才加入SQL语句
                    fieldStr.Append(fieldName);
                    paramStr.Append("@" + fieldName);
                    if (i < (len - 1))
                    {
                        fieldStr.Append(",");//参数和字段用逗号隔开
                        paramStr.Append(",");
                    } object val = type.GetProperty(fieldName).GetValue(model, null);// 根据属性名称获取当前属性的值
                    if (val == null) val = DBNull.Value;//如果该值为空的话,则将其转化为数据库的NULL
                    param[paramLIndex] = new SqlParameter(fieldName, val);//给每个参数赋值
                    paramLIndex++;
                }
            }
            commandText.Append(tableName);
            commandText.Append(" ( ");
            commandText.Append(fieldStr);
            commandText.Append(" ) values ( ");
            commandText.Append(paramStr);
            commandText.Append(" ) ");//拼接成完整的字符串
            return DAL.SQLDBHelpercs.ExecuteNonQuery(commandText.ToString(), param, "sql");
        }
        //返回一个list<MODEL>
        public static List<Object> selectModel(string top, string strWhere, string orderby)
        {
            List<Object> Listobjectdata = new List<Object>();
            string sql = "select * from ChenYTest.dbo.t_ConfigCon";
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ObjectData objectdata = new ObjectData();
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
        public static DataTable getDataTable(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        public static DataSet getDataSet(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            return ds;
        }
        //获取表格信息
        public static DataTable getTableInfo(string guid)
        {
            string sql = string.Format("select * from t_Tables where guid='{0}'", guid);
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        //获取字段信息
        public static DataTable getTableFieldInfo(string guid)
        {
            string sql = string.Format("SELECT * FROM t_TableField WHERE TableGUID='{0}' order by  FieldOrder asc", guid);
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        //高级查询
        public static string SetStrWhere(DataTable dt)
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
        //设置高级查询
        public static string setStrWhereHtml(DataTable dt)
        {
            string strHtml = "";
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
                            DataTable tsqldt = BaseClass.getDataTable(tsql);
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
            return strHtml;
        }
        //设置按钮
        public static string setBntHtml(DataTable dt)
        {
            string bntHtml = "";
            if (estimate(dt))
            {
                if (dt.Rows[0]["insert"].ToString() == "1")
                    bntHtml += "<button type=\"button\" class=\"btn btn-success btn-xs\">新　增</button>&nbsp;";
                if (dt.Rows[0]["update"].ToString() == "1")
                    bntHtml += "<button type=\"button\" class=\"btn btn-warning btn-xs\">修　改</button>&nbsp;";
                if (dt.Rows[0]["delete"].ToString() == "1")
                    bntHtml += "<button id=\"deleteGUID\" tableValue=\"\" type=\"button\" class=\"btn btn-danger btn-xs\">删　除</button>&nbsp;";
            }
            return bntHtml;
        }
        //设置表格
        public static string getTableHtml(DataTable dt, string choice, ref string columnsJson)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbjson = new StringBuilder();
            sb.Append("<thead><tr>");
            if (choice == "1")
            {
                string html = "<input type=\"checkbox\" id=\"selectAll\" class=\"table-checkable\" >";
                sb.Append("<th style=\"width:13px;\">" + html + "</th>");
                sbjson.Append("{\"data\": \"CBGUID\", render: function (data, type, row) { return \"<input  name='checkboxGuid' type='checkbox' class='table-checkable'  value='\" + data + \"'/>\"}},");
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
            sb.Append("</tr></thead>");
            columnsJson = "[" + sbjson.ToString().TrimEnd(',') + "]";
            return sb.ToString();
        }
        //sql解析拼接
        public static string GetTSQL(string tableName, string fileName,string strWhere, string order, bool isCbGuid)
        {
            string sqlstr = " SELECT " + fileName;
            if (isCbGuid)
                sqlstr += ",GUID AS CbGuid";
            sqlstr += " from (" + tableName + ") as NEWTABLE";
            if (strWhere != "")
                sqlstr += " where " + strWhere;
            if (order != "")
                sqlstr += " order by " + order;
            return sqlstr;
        }
        //判断dt
        public static bool estimate(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //读取变量
        public static string getValueForKey(string key)
        {
            return "";
        }
        //删除
        public static bool DeleteItemID(string tableName,string OneFileName, string values)
        {
            string sqldel = string.Format(@" delete from {0} where "+ OneFileName + " in ({1})", tableName, values);
            return DAL.SQLDBHelpercs.ExecuteNonQuery(sqldel, null, "sql");
        }
        //显示页面修改保存
        public static bool SaveUpdateList(DataTable dt, DataTable tableInfo, string guid)
        {
            string FieldKey = "";
            StringBuilder sbSQL = new StringBuilder();
            string str = "";
            if (estimate(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["name"].ToString() == "FieldKey")
                    {
                        if (i > 0)
                        {
                            sbSQL.Append(str.TrimEnd(','));
                            sbSQL.Append(string.Format(" where TableGUID ='{0}' and FieldKey = '{1}'; ", guid, FieldKey));
                            str = "";
                        }
                        FieldKey = dt.Rows[i]["value"].ToString();
                        sbSQL.Append(" update[t_TableField] set ");
                    }
                    else
                    {
                        str += string.Format("[{0}]='{1}',", dt.Rows[i]["name"].ToString(), dt.Rows[i]["value"].ToString());
                    }
                }
                sbSQL.Append(string.Format(str.TrimEnd(',') + " where TableGUID ='{0}' and FieldKey = '{1}'; ", guid, FieldKey));
                str = "";
            }
            if (estimate(tableInfo))
            {
                sbSQL.Append(" update t_Tables set ");
                for (int j = 0; j < tableInfo.Rows.Count; j++)
                {
                    str += string.Format("[{0}]='{1}',", tableInfo.Rows[j]["name"].ToString(), tableInfo.Rows[j]["value"].ToString().Replace("'", "''"));
                }
                sbSQL.Append(string.Format(str.TrimEnd(',') + " where guid ='{0}' ; ", guid));
            }
            return DAL.SQLDBHelpercs.ExecuteNonQuery(sbSQL.ToString(), null, "sql");
        }
        /// <summary>
        /// 拼接分页SQL语句
        /// </summary>
        /// <param name=""></param>
        public static string PageBySQL(string SQL, string TableName, string OneFileName, string SQLWhere, string SQLOrder, int PageIndex, int PageLimit)
        {
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
            return sql;
        }
        //
        public static string GetTopOneFileName(string TableName)
        {
            string sql = string.Format("Select  top(1)Name FROM SysColumns Where id=Object_Id('{0}')", TableName);
            return DAL.SQLDBHelpercs.ExecuteReader(sql);
        }
    }
}