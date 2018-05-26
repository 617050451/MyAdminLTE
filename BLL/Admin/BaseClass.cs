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

        //返回DataTable
        public static DataTable GetDataTable(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        public static DataSet GetDataSet(string strSql)
        {
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(strSql, null);
            return ds;
        }
      
        //设置按钮
        public static string SetBntHtml(DataTable dt)
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
        //判断dt
        public static bool estimate(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        //读取变量
        public static string GetValueForKey(string key)
        {
            return "";
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
        //获取表的第一个字段名
        public static string GetTopOneFileName(string TableName)
        {
            string sql = string.Format("Select  top(1)Name FROM SysColumns Where id=Object_Id('{0}')", TableName);
            return DAL.SQLDBHelpercs.ExecuteReader(sql);
        }
        //
    }
}