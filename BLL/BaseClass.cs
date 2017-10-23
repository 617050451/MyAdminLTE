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
    }
}