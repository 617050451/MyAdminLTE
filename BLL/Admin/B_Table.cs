using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class B_Table
    {
        public int bItemID { get; set; }
        public B_Table(int ItemID)
        {
            bItemID = ItemID;
        }
        /// <summary>
        /// 获取表格实体
        /// </summary>
        /// <returns></returns>
        public Model.M_Table GetTableModel()
        {
            return BaseClass.XmlSelectTableModel(bItemID);
        }
        /// <summary>
        /// 查询字段信息
        /// </summary>
        public List<Model.M_TableField> GetTableFieldModel()
        {
            return BaseClass.XmlSelectAllTableFieldInfo(bItemID);
        }
        /// <summary>
        /// 修改表格实体
        /// </summary>
        public bool UpdateTableModel(System.Data.DataTable dataTable)
        {
            return BaseClass.XmlUpdateTableModel(bItemID, dataTable);
        }
        /// <summary>
        /// 修改字段信息
        /// </summary>
        public bool UpdateTableFieldModel(System.Data.DataTable dataTable)
        {
            return BaseClass.XmlUpdateTableFielModel(bItemID, dataTable);
        }
        /// <summary>
        /// 跟新字段信息
        /// </summary>
        public bool ToUpdateTableFieldModel()
        {
            return BaseClass.XmlToUpdateTableFielModel(bItemID);
        }
        /// <summary>
        /// 自动排序
        /// </summary>
        /// <returns></returns>
        public bool SetOrder()
        {
            return BaseClass.XmlToSetOrder(bItemID);
        }
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="PageName"></param>
        /// <param name="TableGUID"></param>
        public void SavePageHtml(int ItemID)
        {
            BLL.B_Table TableBll = new BLL.B_Table(ItemID);
            var TableModel = TableBll.GetTableModel();
            var TableFielModelList = TableBll.GetTableFieldModel();
            System.IO.StreamReader h_hovertreeSr = new System.IO.StreamReader(System.Web.HttpContext.Current.Request.MapPath("\\Admin\\PageManage\\Temp\\List.html.temp"));
            string h_hovertreeTemplate = h_hovertreeSr.ReadToEnd();
            //当前网站根目录物理路径  
            System.IO.DirectoryInfo h_dir = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Request.PhysicalApplicationPath);
            //HoverTreeWeb项目根目录下主页文件html
            string h_path = string.Format(h_dir.Parent.FullName + "\\AdminLTE\\Page\\{0}.html", TableModel.FileName);
            if (File.Exists(h_path))
                File.Delete(h_path);
            System.IO.FileStream fs = new System.IO.FileStream(h_path, System.IO.FileMode.Create, System.IO.FileAccess.Write);//创建写入文件             
            System.IO.StreamWriter h_sw = new System.IO.StreamWriter(fs, Encoding.UTF8);
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{Title}", TableModel.Title).Replace("{IsPlus}", TableModel.IsPlus.ToString()).Replace("{IsWhere}", TableModel.IsWhere.ToString()).Replace("{IsChoice}", TableModel.IsChoice.ToString());
            StringBuilder TableThead = new StringBuilder();
            StringBuilder Columns = new StringBuilder();
            StringBuilder TopButton = new StringBuilder();
            if (TableModel.IsChoice == 1)
            {
                TableThead.Append("<th style=\"width:12px;\"  rowspan=\"1\" colspan=\"1\" aria-label=\"\"><input bnt-click=\"SelectAll\" name=\"SelectAll\" type=\"checkbox\" class=\"table-checkable\"></th>");
                Columns.Append("{ \"data\": \"ItemID\", render: function (data, type, row) { return \"<input  bnt-click='CheckBoxItemID' name='CheckBoxItemID' type='checkbox' class='table-checkable'  value='\" + data + \"'/>\" } },");
            }
            if (TableModel.IsDelete == 1 && TableModel.IsChoice == 1)
                TopButton.Append("<button name='DeleteItemID' bnt-click='DeleteItemID' type='button' class='btn btn-danger btn-xs'>删　除</button>");
            if (TableModel.IsInsert == 1)
                TopButton.Append("<button name='InsertItemID' bnt-click='InsertItemID' type='button' class='btn btn-success btn-xs'>新　增</button>");
            foreach (var item in TableFielModelList)
            {
                if (item.FieldStatusID == 1)
                {
                    TableThead.Append(string.Format("<th aria-controls=\"example\" rowspan=\"1\" colspan=\"1\" aria-label=\"{0}: \">{1}</th>", item.FieldText, item.FieldText));
                    Columns.Append(SetFieldDataType(item.FieldDataType, item.FieldData, item.FieldKey));
                }
            }
            if (TableModel.IsUpdate == 1 || (TableModel.IsDelete == 1 && TableModel.IsChoice == 0))
            {
                TableThead.Append(string.Format("<th aria-controls=\"example\" rowspan=\"1\" colspan=\"1\" aria-label=\"操作: \">{0}</th>", "操作"));
                var BntHmtl = string.Empty;
                if (TableModel.IsUpdate == 1)
                    BntHmtl += "<button name='UpdateItemID' bnt-click = 'UpdateItemID' style='margin:2px;' type = 'button' class='btn btn-warning  btn-xs'>修　改</button>";
                if (TableModel.IsDelete == 1)
                    BntHmtl += "<button name='DeleteItemID' bnt-click='DeleteItemID' type='button' class='btn btn-danger btn-xs'>删　除</button>";
                if (!string.IsNullOrWhiteSpace(BntHmtl))
                    Columns.Append("{\"data\": \"" + "ItemID" + "\", render: function (data, type, row) { return \"" + BntHmtl + "\"}},");
            }
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TableThead}", TableThead.ToString());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{Columns}", Columns.ToString().TrimEnd(','));
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TopBotton}", TopButton.ToString());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{BottomHtml}", "");
            h_sw.Write(h_hovertreeTemplate);
            h_sw.Close();
            fs.Close();
            h_hovertreeSr.Close();
        }
        /// <summary>
        /// 获取表格数据Json
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PageStart"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetDataListJson(int ItemID, int PageStart, int PageIndex, int PageSize, string order)
        {
            System.Data.DataSet ds = BaseClass.GetDataSet(SetFieldSQL(ItemID));
            if (ds != null)
            {
                System.Data.DataTable tableJson = ds.Tables[0];
                if (tableJson != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"total\":" + tableJson.Rows.Count + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                    string datatablejson = JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                    sb.Append(datatablejson);
                    sb.Append("}");
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
        //配置转换显示SQL
        public string SetFieldSQL(int ItemID)
        {
            BLL.B_Table TableBll = new BLL.B_Table(ItemID);
            var TableModel = TableBll.GetTableModel();
            var TableFielModelList = TableBll.GetTableFieldModel();
            var TableName = TableModel.TableName;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT " + TableModel.PrimaryKey + " as ItemID");
            string SQLFieldKey = "";
            foreach (var item in TableFielModelList)
            {
                if (item.FieldDataType == 3)
                {
                    SQLFieldKey += ",(" + item.FieldData.Replace("row.", "NewCyFsTable.") + ") as " + item.FieldKey;
                }
                else
                    SQLFieldKey += "," + item.FieldKey;
            }
            sb.Append(SQLFieldKey.TrimEnd(',') + " FROM " + TableName + " AS NewCyFsTable ");
            return sb.ToString();
        }
        //解析转换显示
        public string SetFieldDataType(int FieldDataType, string FieldData, string FieldKey)
        {
            string data = string.Empty;
            if (FieldDataType == 1)
                data = ", render: function (data, type, row) { return  data }";
            if (FieldDataType == 2)
                data = ", render: function (data, type, row) { return  " + FieldData + " }";
            if (FieldDataType == 3)
                data = ", render: function (data, type, row) { return data;}";
            if (FieldDataType == 4)
            {
                if (FieldData == "yearM")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy-MM\");}";
                else if (FieldData == "yearMzw")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy年MM月\");}";
                else if (FieldData == "date")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy-MM-dd\");}";
                else if (FieldData == "datezw")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy年MM月dd日 \");}";
                else if (FieldData == "time1")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy-MM-dd HH:mm\");}";
                else if (FieldData == "time1zw")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy年MM月dd HH时mm分\");}";
                else if (FieldData == "time2")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy-MM-dd HH:mm:ss\");}";
                else if (FieldData == "time2zw")
                    data = ", render: function (data, type, row) { return SetDateTime(data,\"yyyy年MM月dd HH时mm分ss秒\");}";
                else if (FieldData == "img")
                    data = ", render: function (data, type, row) {  SetImgUrl(row,data,function (reData) { data=reData;}); return data; }";
                else
                    data = ", render: function (data, type, row) {  return data }";
            }
            return "{\"data\": \"" + FieldKey + "\"" + data + "},";
        }
        
    }
}

