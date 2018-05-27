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
        //获取单行单列数据
        public static string GetDataViewSQL(string sql)
        {
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return tableJson.Rows[0][0].ToString();
            else
                return "";
        }
        //获取单行数据
        public static DataRow GetDataRowViewSQL(string sql)
        {
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return tableJson.Rows[0];
            else
                return null;
        }
        //返回一个list<MODEL>
        public static List<Object> SelectModel(string sql,string tablename)
        {
            List<Object> Listobjectdata = new List<Object>();
            DataSet ds = DAL.SQLDBHelpercs.ExecuteReader(sql, null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ObjectData objectdata = new ObjectData(tablename);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        objectdata.SetValue(dt.Columns[j].ColumnName, dt.Rows[i][j].ToString());
                    }
                    Listobjectdata.Add(objectdata);
                }
            }
            return Listobjectdata;
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
    }
}