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
            XmlNodeList rootChil = root.SelectNodes("OPTION");//获取子节点
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
                var PredefinedSQL = xn.Attributes["PredefinedSQL"].Value;
                var TableType = xn.Attributes["TableType"].Value;
                var TableName = xn.Attributes["TableName"].Value;
                var PrimaryKey = xn.Attributes["PrimaryKey"].Value;
                var Note = xn.Attributes["Note"].Value;
                mt.TableID = Convert.ToInt32(TableID);
                mt.Title = Title;
                mt.FileName = FileName;
                mt.SQL = SQL;
                mt.PredefinedSQL = PredefinedSQL;
                mt.TableName = TableName;
                mt.TableType = Convert.ToInt32(TableType); ;
                mt.PrimaryKey = PrimaryKey;
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
            XmlNode xn = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
            if (xn != null)
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
                var PredefinedSQL = xn.Attributes["PredefinedSQL"].Value;
                var TableType = xn.Attributes["TableType"].Value;
                var TableName = xn.Attributes["TableName"].Value;
                var PrimaryKey = xn.Attributes["PrimaryKey"].Value;
                var Note = xn.Attributes["Note"].Value;
                mt.TableID = Convert.ToInt32(TableID);
                mt.Title = Title;
                mt.FileName = FileName;
                mt.SQL = SQL;
                mt.PredefinedSQL = PredefinedSQL;
                mt.TableName = TableName;
                mt.TableType = Convert.ToInt32(TableType); ;
                mt.PrimaryKey = PrimaryKey;
                mt.IsPlus = Convert.ToInt32(IsPlus);
                mt.IsWhere = Convert.ToInt32(IsWhere);
                mt.IsChoice = Convert.ToInt32(IsChoice);
                mt.IsInsert = Convert.ToInt32(IsInsert);
                mt.IsUpdate = Convert.ToInt32(IsUpdate);
                mt.IsDelete = Convert.ToInt32(IsDelete);
                mt.Note = Note;
            }
            return mt;
        }
        /// <summary>
        /// 查询指定key的InnerText值
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string XmlSelectTableKeyInnerText(int ItemID,string KeyName)
        {
            string data = "";
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNode xn = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
            if (xn != null)
            {
                XmlNode XmlColumnsNode = xn.SelectSingleNode(KeyName);
                if (XmlColumnsNode != null)
                    data = XmlColumnsNode.InnerText;
            }
            return data;
        }
        /// <summary>
        /// 修改指定key的InnerText值
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="KeyName"></param>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public static bool XmlUpdateTableKeyInnerText(int ItemID, string KeyName, string KeyValue)
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNode xn = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
            if (xn != null)
            {
                XmlNode XmlColumnsNode = xn.SelectSingleNode(KeyName);
                if (XmlColumnsNode != null)
                {
                    XmlColumnsNode.InnerText = KeyValue;
                    xml.Save(xmlPath);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
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
            XmlNode rootChil = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
            if (rootChil != null)
            {
                XmlNode XmlColumnsNode = rootChil.SelectSingleNode("COLUMNS");
                if (XmlColumnsNode != null)
                {
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
                }
            }
            IEnumerable<Model.M_TableField> query = from items in ListModel orderby items.FieldOrder select items;
            return query.ToList<Model.M_TableField>();
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
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNode rootChil = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
                if (rootChil != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        var ColunmName = dataTable.Rows[i][0].ToString();
                        var ColunmValue = dataTable.Rows[i][1].ToString();
                        rootChil.Attributes[ColunmName].Value = ColunmValue;
                    }
                    xml.Save(xmlPath);
                }
                return true;
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
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNode rootChil = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
                if (rootChil != null)
                {
                    XmlNode XmlColumnsNode = rootChil.SelectSingleNode("COLUMNS");
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
                }
                return true;
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
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNode rootChil = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
                if (rootChil != null)
                {
                    XmlNode XmlColumnsNode = rootChil.SelectSingleNode("COLUMNS");
                    var SQL = rootChil.Attributes["SQL"].Value;
                    List<XmlElement> xmlAddList = new List<XmlElement>();
                    DataTable ColumnsDT = GetDataTableColumns(SQL);
                    rootChil.Attributes["PrimaryKey"].Value = ColumnsDT.Columns[0].ColumnName;
                    for (int i = 0; i < ColumnsDT.Columns.Count; i++)
                    {
                        var IsExist = false;
                        var ColunmName = ColumnsDT.Columns[i].ColumnName;
                        foreach (XmlNode xcn in XmlColumnsNode.ChildNodes)
                        {
                            string FieldKey = xcn.Attributes["FieldKey"].Value;
                            if (ColunmName.ToUpper() == FieldKey.ToUpper())
                            {
                                IsExist = true;
                                break;
                            }
                        }
                        if (!IsExist)
                        {
                            XmlElement xmlElemDeptChildId = xml.CreateElement(ColunmName);
                            xmlElemDeptChildId.SetAttribute("FieldKey", ColunmName);
                            xmlElemDeptChildId.SetAttribute("FieldText", ColunmName);
                            xmlElemDeptChildId.SetAttribute("FieldDataType", "1");
                            xmlElemDeptChildId.SetAttribute("FieldData", "");
                            xmlElemDeptChildId.SetAttribute("FieldStatusID", "1");
                            xmlElemDeptChildId.SetAttribute("SelectType", "0");
                            xmlElemDeptChildId.SetAttribute("SelectData", "");
                            xmlElemDeptChildId.SetAttribute("FieldOrder", (i + 1).ToString());
                            xmlElemDeptChildId.InnerText = "";
                            xmlAddList.Add(xmlElemDeptChildId);
                        }
                    }
                    for (int i = 0; i < xmlAddList.Count; i++)
                    {
                        XmlColumnsNode.AppendChild(xmlAddList[i]);
                    }
                    foreach (XmlNode xcn in XmlColumnsNode.ChildNodes)
                    {
                        string FieldKey = xcn.Attributes["FieldKey"].Value;
                        if (!ColumnsDT.Columns.Contains(FieldKey))
                            XmlColumnsNode.RemoveChild(xcn);
                    }
                    xml.Save(xmlPath);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 自动排序
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool XmlToSetOrder(int ItemID)
        {
            try
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\Table.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNode rootChil = root.SelectSingleNode("OPTION[@TableID=" + ItemID + "]");//获取指定子节点  
                if (rootChil != null)
                {
                    XmlNode XmlColumnsNode = rootChil.SelectSingleNode("COLUMNS");
                    var SQL = rootChil.Attributes["SQL"].Value;
                    List<XmlElement> xmlAddList = new List<XmlElement>();
                    DataTable ColumnsDT = GetDataTableColumns(SQL);
                    for (int i = 0; i < ColumnsDT.Columns.Count; i++)
                    {
                        var ColunmName = ColumnsDT.Columns[i].ColumnName;
                        XmlColumnsNode.SelectSingleNode(ColunmName).Attributes["FieldOrder"].Value = (i + 1).ToString();
                    }
                    xml.Save(xmlPath);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取数据，数据来源xml文件
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTableForXML(DataTable TableColumns, string TableName)
        {
            TableColumns.Columns.Add(new DataColumn("ItemID", Type.GetType("System.String")));
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + TableName;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNodeList rootChil = root.SelectNodes("OPTION");//获取子节点
            foreach (XmlNode xn in rootChil)
            {
                DataRow dr = TableColumns.NewRow();
                for (int i = 0; i < TableColumns.Columns.Count; i++)
                {
                    var ColumnName = TableColumns.Columns[i].ColumnName;
                    var ColumnValue = "";
                    if (ColumnName == "ItemID")
                        ColumnValue = xn.Attributes[TableColumns.Columns[0].ColumnName].Value;
                    else
                        ColumnValue = xn.Attributes[ColumnName].Value;
                    dr[ColumnName] = ColumnValue;
                }
                TableColumns.Rows.Add(dr);
            }
            return TableColumns;
        }
        /// <summary>
        /// 获取单行数据，数据来源xml文件
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataViewForXML(DataTable TableColumns, string TableName, string ItemID)
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + TableName;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            XmlNode xn = root.SelectSingleNode("OPTION[@" + TableColumns.Columns[0].ColumnName + "=" + ItemID + "]");//获取指定子节点  
            if (xn != null)
            {
                DataRow dr = TableColumns.NewRow();
                for (int i = 0; i < TableColumns.Columns.Count; i++)
                {
                    var ColumnName = TableColumns.Columns[i].ColumnName;
                    var ColumnValue = xn.Attributes[ColumnName].Value;
                    dr[ColumnName] = ColumnValue;
                }
                TableColumns.Rows.Add(dr);
            }
            return TableColumns;
        }
        /// <summary>
        /// 获取页面字段Html
        /// </summary>
        /// <param name="mf">页面字段信息实体</param>
        /// <returns></returns>
        public static string SelectAllTableFieldHtml(Model.M_TableField mf)
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
            sb.Append("<option " + (FieldDataType == 2 ? "selected=\"selected\"" : "") + " value=\"2\">固定转换</option>");
            sb.Append("<option " + (FieldDataType == 3 ? "selected=\"selected\"" : "") + " value=\"3\">动态转换</option>");
            sb.Append("<option " + (FieldDataType == 4 ? "selected=\"selected\"" : "") + " value=\"4\">数据格式化</option>");
            sb.Append("<option " + (FieldDataType == 5 ? "selected=\"selected\"" : "") + " value=\"5\">前台自定义</option>");
            sb.Append("</select>");
            sb.Append("</div>");
            sb.Append("<a class=\"text-primary " + (FieldDataType == 1 ? "hidden" : "") + "\" href=\"javascript:void(0)\" onclick=\"setFieldData(this)\">&nbsp;设置</a><input type=\"text\" name=\"FieldData\" class=\"form-control hidden\" value='" + FieldData + "'/>");
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
            sb.Append("<option " + (SelectType == 4 ? "selected=\"selected\"" : "") + " value=\"4\">时间查询</option>");
            sb.Append("<option " + (SelectType == 5 ? "selected=\"selected\"" : "") + " value=\"5\">高级查询</option>");
            sb.Append("</select></div>");
            sb.Append("&nbsp;<a class=\"text-primary " + (SelectType == 2 ? "" : "hidden") + "\"  href=\"javascript:void(0)\" onclick=\"setSelectData(this)\">&nbsp;设置</a>");
            sb.Append("<input type=\"text\" class=\"form-control hidden\" name=\"SelectData\" value='" + SelectData + "'/>");
            return sb.ToString();
        }
        /// <summary>
        /// 删除XML数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="PrimaryKey"></param>
        /// <param name="ItemIDs"></param>
        /// <returns></returns>
        public static bool XmlDeleteTableModel(string TableName, string PrimaryKey, string ItemIDs)
        {
            string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + TableName;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);//读取文件
            XmlElement root = xml.DocumentElement;//获取根节点
            StringBuilder sb = new StringBuilder();
            var ListItemID = ItemIDs.Split(',');
            foreach (var item in ListItemID)
            {
                XmlNode rootChilNode = root.SelectSingleNode("OPTION[@" + PrimaryKey + "=" + item + "]");//获取指定子节点  
                root.RemoveChild(rootChilNode);
            }
            xml.Save(xmlPath);
            return true;
        }
        /// <summary>
        /// 修改xml表格信息
        /// </summary>
        /// <param name="ModelData"></param>
        /// <param name="PrimaryKey"></param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool XmlUpdateTableModel(BLL.ObjectData ModelData, string PrimaryKey, string ItemID)
        {
            try
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + ModelData.TableName;
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlNode rootChil = root.SelectSingleNode("OPTION[@" + PrimaryKey + "=" + ItemID + "]");//获取指定子节点  
                if (rootChil != null)
                {
                    var pros = ModelData.GetValues().Keys;//所有字段名称
                    List<SqlParameter> paramlist = new List<SqlParameter>();
                    foreach (var fieldName in pros)
                    {
                        if (!fieldName.ToUpper().Equals(PrimaryKey == null ? "" : PrimaryKey.ToUpper()))
                        {
                            if (ModelData.IsSet(fieldName))//是否赋值了
                            {
                                var fieldValue = ModelData.GetValue(fieldName);
                                rootChil.Attributes[fieldName].Value = fieldValue.ToString();
                            }
                        }
                    }
                    xml.Save(xmlPath);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 新增xml表格信息
        /// </summary>
        /// <param name="ModelData"></param>
        /// <returns></returns>
        public static string XmlInsertTableModel(BLL.ObjectData ModelData, string PrimaryKey)
        {
            try
            {
                string xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\DataXML\\" + ModelData.TableName;
                XmlDocument xml = new XmlDocument();
                xml.Load(xmlPath);//读取文件
                XmlElement root = xml.DocumentElement;//获取根节点
                XmlElement xmlElemDeptChildId = xml.CreateElement("OPTION");
                var pros = ModelData.GetValues().Keys;//所有字段名称
                List<SqlParameter> paramlist = new List<SqlParameter>();
                XmlNode XmlColumnsNode = root.SelectSingleNode("MAXID");
                var MaxID = Convert.ToInt32(XmlColumnsNode.InnerText) + 1;
                foreach (var fieldName in pros)
                {
                    if (!fieldName.ToUpper().Equals(PrimaryKey == null ? "" : PrimaryKey.ToUpper()))
                    {
                        if (ModelData.IsSet(fieldName))//是否赋值了
                        {
                            var fieldValue = ModelData.GetValue(fieldName);
                            xmlElemDeptChildId.SetAttribute(fieldName, fieldValue.ToString());
                        }
                    }
                    else
                        xmlElemDeptChildId.SetAttribute(fieldName, MaxID.ToString());
                }
                if (ModelData.TableName == "Table.xml")
                {
                    xmlElemDeptChildId.SetAttribute("TableID", MaxID.ToString());
                    xmlElemDeptChildId.SetAttribute("IsPlus", "0");
                    xmlElemDeptChildId.SetAttribute("IsWhere", "0");
                    xmlElemDeptChildId.SetAttribute("IsChoice", "0");
                    xmlElemDeptChildId.SetAttribute("IsInsert", "0");
                    xmlElemDeptChildId.SetAttribute("IsUpdate", "0");
                    xmlElemDeptChildId.SetAttribute("IsDelete", "0");
                    xmlElemDeptChildId.SetAttribute("PredefinedSQL", "");
                    xmlElemDeptChildId.SetAttribute("PrimaryKey", "TableID");
                }
                xmlElemDeptChildId.InnerText = "";
                if (ModelData.TableName == "Table.xml")
                {
                    XmlElement xmlCOLUMNSChildId = xml.CreateElement("COLUMNS");
                    xmlCOLUMNSChildId.InnerText = "";
                    xmlElemDeptChildId.AppendChild(xmlCOLUMNSChildId);

                    XmlElement xmlTopHeadChildId = xml.CreateElement("TopHead");
                    xmlTopHeadChildId.InnerText = "";
                    xmlElemDeptChildId.AppendChild(xmlTopHeadChildId);

                    XmlElement xmlBottomHtmlChildId = xml.CreateElement("BottomHtml");
                    xmlBottomHtmlChildId.InnerText = "";
                    xmlElemDeptChildId.AppendChild(xmlBottomHtmlChildId);

                    XmlElement xmlBottomScriptChildId = xml.CreateElement("BottomScript");
                    xmlBottomScriptChildId.InnerText = "";
                    xmlElemDeptChildId.AppendChild(xmlBottomScriptChildId);
                }
                root.AppendChild(xmlElemDeptChildId);
                xml.Save(xmlPath);
                XmlColumnsNode.InnerText = MaxID.ToString();
                if (ModelData.TableName == "Table.xml")
                    XmlToUpdateTableFielModel(MaxID);
                return MaxID.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }


        //获取表格字段
        public static DataTable GetDataTableColumns(string sql)
        {
            return BaseClass.GetDataTable("select * from (" + sql + ")as cyfstb where 1=2");
        }
        public static bool ExecuteNonQuerySQL(string SQL)
        {
            return DAL.SQLDBHelpercs.ExecuteNonQuery(SQL);
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
        public static bool IsNullOrNotNull(DataTable dt)
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
        //新增数据
        public static string InsertModel(ObjectData model, string PrimaryKey)
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
                if (!fieldName.ToUpper().Equals(PrimaryKey.ToUpper()))
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
                    else
                    {
                        fieldStr.Append("[" + fieldName + "],");
                        paramStr.Append("@" + fieldName + ",");
                        paramlist.Add(new SqlParameter(fieldName, "NEWID()"));//给每个参数赋值
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
        //修改
        public static bool UpdateModel(ObjectData model, string PrimaryKey, string ItemID)
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
                if (!fieldName.ToUpper().Equals(PrimaryKey == null ? "" : PrimaryKey.ToUpper()))
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
            commandText.Append(" where " + PrimaryKey + "='" + ItemID + "'");//拼接成完整的字符串
            return DAL.SQLDBHelpercs.ExecuteNonQuery(commandText.ToString(), param, "sql");
        }
        //删除
        public static bool DeleteModel(string TableName, string PrimaryKey, string ItemIDs)
        {
            var ListItemID = ItemIDs.Split(',');
            StringBuilder sql = new StringBuilder();
            foreach (var item in ListItemID)
                sql.Append(string.Format(" DELETE FROM " + TableName + " WHERE " + PrimaryKey + " = '{0}';", item));
            return ExecuteNonQuerySQL(sql.ToString());
        }
    }
}