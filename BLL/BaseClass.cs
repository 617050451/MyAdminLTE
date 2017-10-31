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
        //
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
        //高级查询
        public static string setStrWhere(string columnName, string columnValue, string type)
        {
            string strWhere = "";
            switch (type)
            {
                case "1":
                    strWhere = " and " + columnName + " like '%" + columnValue + "%'";
                    break;
                case "2":
                    if (columnValue != "0")
                        strWhere = " and " + columnName + " = '" + columnValue + "'";
                    break;
                case "3":
                    strWhere = " and convert(varchar(50)," + columnName + ",23) <= '" + columnValue + "'";
                    break;
                default:
                    break;
            }
            return strWhere;
        }
        //页面加载设置
        public static string setStrHtml(string sqlStr)
        {
            string[] list = sqlStr.Split('↓');
            string strHtml = "";
            for (var i = 0; i < list.Length; i++)
            {
                string[] listc = list[i].Split('↑');
                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                if (listc[2] == "1")
                {
                    strHtml += "<label class=\"col-xs control-label table-label\">" + listc[1] + "</label >";
                    strHtml += "<input type=\"text\" name=\"" + listc[0] + "|" + listc[2] + "\"  class=\"form-control\" placeholder=\"" + listc[1] + "\" />";
                }
                else if (listc[2] == "2")
                {
                    strHtml += "<label class=\"col-xs control-label table-label\">" + listc[1] + "</label >";
                    strHtml += "<input type=\"text\" name=\"" + listc[0] + "|" + listc[2] + "\" class=\"form-control\" placeholder=\"" + listc[1] + "\" />";
                }
                else if (listc[2] == "3")
                {
                    strHtml += "<label class=\"col-xs control-label table-label\">" + listc[1] + "</label >";
                    strHtml += "<select name=\"" + listc[0] + "|" + listc[2] + "\" class=\"form-control select2 select2-hidden-accessible\"  tabindex=\"-1\" aria-hidden=\"true\" >";
                    strHtml += "<option selected = \"selected\" value = \"0\" >全部</option >";
                    string[] listw = listc[3].Split('◇');
                    if (listw[0].ToUpper() == "SQL")
                    {
                        string sql = listw[1];
                        DataTable dt = BaseClass.getDataTable(sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                strHtml += "<option value = \"" + dt.Rows[j][1].ToString() + "\" >" + dt.Rows[j][0].ToString() + "</option >";
                            }
                        }
                    }
                    strHtml += "</select>";
                }
                strHtml += "</div>";
            }
            strHtml += "<div class=\"col-sm-1 table-p\" style=\"margin-top:30px;\"><button type =\"button\" class=\"btn btn-danger pull-right btn-block btn-primary\" onclick=\"getJsonData('select')\">查询</button></div>";
            return strHtml;
        }
    }
}