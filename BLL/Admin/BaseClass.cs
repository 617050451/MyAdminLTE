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
        public static List<Model.M_Table> XmlSelectAllTableModelList()
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
                var TableID = xn.Attributes["TableID"].Value;
                var Title = xn.Attributes["Title"].Value;
                var FileName = xn.Attributes["FileName"].Value;
                var IsPlus = xn.Attributes["IsPlus"].Value;
                var IsWhere = xn.Attributes["IsWhere"].Value;
                var IsChoice = xn.Attributes["IsChoice"].Value;
                var IsInsert = xn.Attributes["IsInsert"].Value;
                var IsUpdate = xn.Attributes["IsUpdate"].Value;
                var IsDelete = xn.Attributes["IsDelete"].Value;
                var SQL = xn.Attributes["SQL"].Value;
                var TableName = xn.Attributes["SQL"].Value;
                var Note = xn.Attributes["Note"].Value;
                mt.TableID = Convert.ToInt32(TableID);
                mt.Title = Title;
                mt.FileName = FileName;
                mt.SQL = SQL;
                mt.TableName = TableName;
                mt.IsPlus = Convert.ToInt32(IsPlus);
                mt.IsWhere = Convert.ToInt32(IsWhere);
                mt.IsChoice = Convert.ToInt32(IsChoice);
                mt.IsInsert = Convert.ToInt32(IsInsert);
                mt.IsUpdate = Convert.ToInt32(IsUpdate);
                mt.IsDelete = Convert.ToInt32(IsDelete);
                mt.Note = Note;
                ListModel.Add(mt);
            }
            return ListModel;
        }
        /// <summary>
        /// 查询页面数据
        /// </summary>
        /// <returns></returns>
        public static Model.M_Table XmlSelectTableModel(int ItemID)
        {
            Model.M_Table mt = new Model.M_Table();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNodeList rootChil = root.ChildNodes;//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                if (xn.Attributes["TableID"].Value == ItemID.ToString())
                {
                    var TableID = xn.Attributes["TableID"].Value;
                    var Title = xn.Attributes["Title"].Value;
                    var FileName = xn.Attributes["FileName"].Value;
                    var IsPlus = xn.Attributes["IsPlus"].Value;
                    var IsWhere = xn.Attributes["IsWhere"].Value;
                    var IsChoice = xn.Attributes["IsChoice"].Value;
                    var IsInsert = xn.Attributes["IsInsert"].Value;
                    var IsUpdate = xn.Attributes["IsUpdate"].Value;
                    var IsDelete = xn.Attributes["IsDelete"].Value;
                    var SQL = xn.Attributes["SQL"].Value;
                    var TableName = xn.Attributes["TableName"].Value;
                    var Note = xn.Attributes["Note"].Value;
                    mt.TableID = Convert.ToInt32(TableID);
                    mt.Title = Title;
                    mt.FileName = FileName;
                    mt.SQL = SQL;
                    mt.TableName = TableName;
                    mt.IsPlus = Convert.ToInt32(IsPlus);
                    mt.IsWhere = Convert.ToInt32(IsWhere);
                    mt.IsChoice = Convert.ToInt32(IsChoice);
                    mt.IsInsert = Convert.ToInt32(IsInsert);
                    mt.IsUpdate = Convert.ToInt32(IsUpdate);
                    mt.IsDelete = Convert.ToInt32(IsDelete);
                    mt.Note = Note;
                    break;
                }
            }
            return mt;
        }
        /// <summary>
        /// 查询页面字段信息
        /// </summary>
        /// <param name="ItemID">页面数据ID</param>
        /// <returns></returns>
        public static List<Model.M_TableField> XmlSelectAllTableFieldInfo(int ItemID)
        {
            List<Model.M_TableField> ListModel = new List<Model.M_TableField>();
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            XmlNodeList rootChil = root.ChildNodes;//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                if (xn.Attributes["TableID"].Value == ItemID.ToString())
                {
                    XmlNode XmlColumnsNode = xn.SelectSingleNode("COLUMNS");
                    foreach (XmlNode xcn in XmlColumnsNode.ChildNodes)
                    {
                        Model.M_TableField mf = new Model.M_TableField();
                        string FieldKey = xcn.Attributes["FieldKey"].Value;
                        string FieldText = xcn.Attributes["FieldText"].Value;
                        string FieldDataType = xcn.Attributes["FieldDataType"].Value;
                        string FieldData = xcn.Attributes["FieldData"].Value;
                        string FieldStatusID = xcn.Attributes["FieldStatusID"].Value;
                        string SelectType = xcn.Attributes["SelectType"].Value;
                        string SelectData = xcn.Attributes["SelectData"].Value;
                        string FieldOrder = xcn.Attributes["FieldOrder"].Value;
                        mf.TableID = ItemID;
                        mf.FieldKey = FieldKey;
                        mf.FieldText = FieldText;
                        mf.FieldDataType = Convert.ToInt32(FieldDataType);
                        mf.FieldData = FieldData;
                        mf.FieldStatusID = Convert.ToInt32(FieldStatusID);
                        mf.SelectType = Convert.ToInt32(SelectType);
                        mf.SelectData = SelectData;
                        mf.FieldOrder = Convert.ToInt32(FieldOrder);
                        ListModel.Add(mf);
                    }
                    break;
                }
            }
            IEnumerable<Model.M_TableField> query = from items in ListModel orderby items.FieldOrder select items;
            return query.ToList<Model.M_TableField>();
        }
        /// <summary>
        /// 获取页面字段Html
        /// </summary>
        /// <param name="mf">页面字段信息实体</param>
        /// <returns></returns>
        public static string XmlSelectAllTableFieldXml(Model.M_TableField mf)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr role=\"row\" class=\"odd\">");
            sb.Append("<td>" + mf.FieldKey + "<div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldKey\" class=\"form-control hidden\" value='" + mf.FieldKey + "'/></div></td>");
            sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldText\"  class=\"form-control\" value='" + mf.FieldText + "\'/></div></td>");
            sb.Append("<td class=\"form-inline\">" + GetFieldDataTypeHtml(mf.FieldDataType, mf.FieldData) + "</td>");//
            sb.Append("<td>" + GetFieldStatusIDHtml(mf.FieldStatusID) + "</td>");
            sb.Append("<td class=\"form-inline\">" + GetSelectTypeHtml(mf.SelectType, mf.SelectData) + "</div>");
            sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\"   class=\"form-control\"  name=\"FieldOrder\" value='" + mf.FieldOrder + "'/></div></td>");
            return sb.ToString();
        }
        /// <summary>
        /// 修改xml表格信息
        /// </summary>
        /// <param name="ItemID">页面数据ID</param>
        /// <param name="dataTable">表格数据</param>
        /// <returns></returns>
        public static bool XmlUpdateTableModel(int ItemID, DataTable dataTable)
        {
            try
            {
                var returnData = false;
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNodeList rootChil = root.ChildNodes;//获取子节点
                foreach (XmlNode xn in rootChil)
                {
                    if (xn.Attributes["TableID"].Value == ItemID.ToString())
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            var ColunmName = dataTable.Rows[i][0].ToString();
                            var ColunmValue = dataTable.Rows[i][1].ToString();
                            xn.Attributes[ColunmName].Value = ColunmValue;
                        }
                        xml.Save(xmlPath);
                        returnData = true;
                        break;
                    }
                }
                return returnData;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 修改xml表格字段信息
        /// </summary>
        /// <param name="ItemID">页面数据ID</param>
        /// <param name="dataTable">表格字段数据</param>
        /// <returns></returns>
        public static bool XmlUpdateTableFielModel(int ItemID, DataTable dataTable)
        {
            try
            {
                var returnData = false;
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNodeList rootChil = root.ChildNodes;//获取子节点
                foreach (XmlNode xn in rootChil)
                {
                    if (xn.Attributes["TableID"].Value == ItemID.ToString())
                    {
                        XmlNode XmlColumnsNode = xn.SelectSingleNode("COLUMNS");
                        XmlNode xmlNodeFieldKey = XmlColumnsNode.SelectSingleNode(dataTable.Rows[0][1].ToString());
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            var ColunmName = dataTable.Rows[i][0].ToString();
                            var ColunmValue = dataTable.Rows[i][1].ToString();
                            if (ColunmName == "FieldKey")
                                xmlNodeFieldKey = XmlColumnsNode.SelectSingleNode(ColunmValue);
                            else
                                xmlNodeFieldKey.Attributes[ColunmName].Value = ColunmValue;
                        }
                        xml.Save(xmlPath);
                        returnData = true;
                        break;
                    }
                }
                return returnData;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新xml表格字段信息
        /// </summary>
        /// <param name="ItemID">页面数据ID</param>
        /// <returns></returns>
        public static bool XmlToUpdateTableFielModel(int ItemID)
        {
            try
            {
                var returnData = false;
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNodeList rootChil = root.ChildNodes;//获取子节点
                foreach (XmlNode xn in rootChil)
                {
                    if (xn.Attributes["TableID"].Value == ItemID.ToString())
                    {
                        XmlNode XmlColumnsNode = xn.SelectSingleNode("COLUMNS");

                        foreach (XmlNode xcn in XmlColumnsNode.ChildNodes)
                        {

                        }
                        xml.Save(xmlPath);
                        returnData = true;
                        break;
                    }
                }
                return returnData;
            }
            catch (Exception)
            {
                return false;
            }
        }






























        /// <summary>
        /// 设置显示方式
        /// </summary>
        /// <param name="FieldDataType">类型</param>
        /// <param name="FieldData">数值</param>
        /// <returns></returns>
        static string GetFieldDataTypeHtml(int FieldDataType, string FieldData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"input-group input-group-sm\">");
            sb.Append("<select name=\"FieldDataType\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
            sb.Append("<option " + (FieldDataType == 1 ? "selected=\"selected\"" : "") + " value=\"1\">原始数据</option>");
            sb.Append("<option " + (FieldDataType == 2 ? "selected=\"selected\"" : "") + " value=\"2\">固定前台转换</option>");
            sb.Append("<option " + (FieldDataType == 3 ? "selected=\"selected\"" : "") + " value=\"3\">动态前台转换</option>");
            sb.Append("<option " + (FieldDataType == 4 ? "selected=\"selected\"" : "") + " value=\"4\">数据格式化</option>");
            sb.Append("</select>");
            sb.Append("</div>");
            sb.Append("<a class=\"text-primary " + (FieldDataType == 1? "hidden" : "") + "\" href=\"javascript:void(0)\" onclick=\"setFieldData(this)\">&nbsp;设置</a><input type=\"text\" name=\"FieldData\" class=\"form-control hidden\" value='" + FieldData + "'/>");
            return sb.ToString();
        }
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="FieldStatusID">数值</param>
        /// <returns></returns>
        static string GetFieldStatusIDHtml(int FieldStatusID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"input-group input-group-sm\"><select name=\"FieldStatusID\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
            sb.Append("<option " + (FieldStatusID == 1 ? "selected=\"selected\"" : "") + " value=\"1\">启用</option>");
            sb.Append("<option " + (FieldStatusID == 0 ? "selected=\"selected\"" : "") + " value=\"0\">禁用</option>");
            sb.Append("</select></div>");
            return sb.ToString();
        }
        /// <summary>
        /// 设置查询方式
        /// </summary>
        /// <param name="SelectType">类型</param>
        /// <param name="SelectData">数值</param>
        /// <returns></returns>
        static string GetSelectTypeHtml(int SelectType, string SelectData)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"input-group input-group-sm \"><select name=\"SelectType\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
            sb.Append("<option " + (SelectType == 0 ? "selected=\"selected\"" : "") + " value=\"0\">不启用</option>");
            sb.Append("<option " + (SelectType == 1 ? "selected=\"selected\"" : "") + " value=\"1\">模糊查询</option>");
            sb.Append("<option " + (SelectType == 2 ? "selected=\"selected\"" : "") + " value=\"2\">下拉查询</option>");
            sb.Append("<option " + (SelectType == 3 ? "selected=\"selected\"" : "") + " value=\"3\">等于查询</option>");
            sb.Append("</select></div>");
            sb.Append("&nbsp;<a class=\"text-primary " + (SelectType == 2 ? "" : "hidden") + "\"  href=\"javascript:void(0)\" onclick=\"setSelectData(this)\">&nbsp;设置</a>");
            sb.Append("<input type=\"text\" class=\"form-control hidden\" name=\"SelectData\" value='" + SelectData + "'/>");
            return sb.ToString();
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