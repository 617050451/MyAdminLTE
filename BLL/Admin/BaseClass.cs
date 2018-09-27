using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;

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
        /// <summary>
        /// 获取全部页面数据
        /// </summary>
        /// <returns></returns>
        public static List<Model.M_Table> XmlSelectGetAllTableModelList()
        {
            List<Model.M_Table> ListModel = new List<Model.M_Table>();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNodeList rootChil = root.ChildNodes;//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                Model.M_Table mt = new Model.M_Table();
                var TableID = xn.Attributes["ID"].Value;
                var Title = xn.Attributes["TITLE"].Value;
                var FileName = xn.Attributes["FILENAME"].Value;
                var Note = xn.Attributes["NOTE"].Value;
                var IsPlus = xn.Attributes["ISPLUS"].Value;
                var IsWhere = xn.Attributes["ISWHERE"].Value;
                var IsChoice = xn.Attributes["ISCHOICE"].Value;
                mt.TableID = Convert.ToInt32(TableID);
                mt.Title = Title;
                mt.FileName = FileName;
                mt.Note = Note;
                mt.IsPlus = Convert.ToInt32(IsPlus);
                mt.IsWhere = Convert.ToInt32(IsWhere);
                mt.IsChoice = Convert.ToInt32(IsChoice);
                ListModel.Add(mt);
            }
            XmlSelectGetAllTableFieldInfo(1);
            return ListModel;
        }
        /// <summary>
        /// 查询页面字段信息
        /// </summary>
        /// <param name="ItemID">页面数据ID</param>
        /// <returns></returns>
        public static DataTable XmlSelectGetAllTableFieldInfo(int ItemID)
        {
            List<Model.M_Table> ListModel = new List<Model.M_Table>();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNodeList rootChil = root.ChildNodes;//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                if (xn.Attributes["ID"].Value == ItemID.ToString())
                {
                    XmlNodeList XmlColumnsNodeList = xn.SelectNodes("COLUMNS");
                    foreach (XmlNode xcnl in XmlColumnsNodeList)
                    {

                    }
                    break;
                }
            }
            return null;
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
        public static List<Object> SelectModel(string sql, string tablename)
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
        public static string GetValueForKey(string sql)
        {
            System.Text.RegularExpressions.Regex reg = new Regex("(?<=sg\\().*?(?=\\))", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(sql);
            foreach (Match m in mc)
            {
                sql = sql.Replace("sg(" + m.Value + ")", "'" + m.Value + "'");
            }
            return sql;
        }
        //获取全部表格数据
        public static List<Model.M_Table> GetAllTableModelList()
        {
           return XmlSelectGetAllTableModelList();
            //List<Model.t_Tables> DataTableModel = DataTableToModel<Model.t_Tables>("SELECT * FROM T_TABLES");
            //return DataTableModel;
        }

        /// 创建
        /// </summary>
        public static void XmlCreate(string XmlName)
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + XmlName + ".xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
                                                  //添加节点
            XmlElement node = xml.CreateElement("Commodity");
            ////节点属性 赋值
            //var BarCode = context.Request["BarCode"];
            //var Name = context.Request["Name"];
            //var Price = context.Request["Price"];
            //var Price1 = context.Request["Price1"];
            //var Stock = context.Request["Stock"];
            //var IsDelete = context.Request["IsDelete"];
            //node.SetAttribute("ID", BarCode);
            //XmlElement theElemBarCode = xml.CreateElement("BarCode");
            //theElemBarCode.InnerText = BarCode;
            //XmlElement theElemName = xml.CreateElement("Name");
            //theElemName.InnerText = Name;
            //XmlElement theElemPrice = xml.CreateElement("Price");
            //theElemPrice.InnerText = Price;
            //XmlElement theElemPrice1 = xml.CreateElement("Price1");
            //theElemPrice1.InnerText = Price1;
            //XmlElement theElemStock = xml.CreateElement("Stock");
            //theElemStock.InnerText = Stock;
            //XmlElement theElemIsDelete = xml.CreateElement("IsDelete");
            //theElemIsDelete.InnerText = IsDelete;
            ////
            //node.AppendChild(theElemBarCode);
            //node.AppendChild(theElemName);
            //node.AppendChild(theElemPrice);
            //node.AppendChild(theElemPrice1);
            //node.AppendChild(theElemStock);
            //node.AppendChild(theElemIsDelete);
            //root.AppendChild(node);
            ////
            //xml.Save(xmlPath);//保存 xml文件
        }
        // <summary>
        /// 查询
        /// </summary>
        /// <summary>
        /// 删除
        /// </summary>
        public void XmlDel()
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "Document\\XMLFile1.xml";//文件路径
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            XmlNodeList rootChil = root.ChildNodes;//获取子节点

            foreach (XmlNode xn in rootChil)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("id") == "student")//节点属性值条件比对
                {
                    xn.ParentNode.RemoveChild(xn);
                    // xn.RemoveAll();  
                }
            }
            xml.Save(xmlPath);
            //xmltext = root.ToString();
        }



        /// <summary>
        /// 修改
        /// </summary>
        public void XmlUpdate()
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "Document\\XMLFile1.xml";//文件路径
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            XmlNodeList rootChil = root.ChildNodes;//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("id") == "student")//节点属性值条件比对
                {
                    xe.SetAttribute("id", "stu");
                }
            }
            xml.Save(xmlPath);
        }

    }
}