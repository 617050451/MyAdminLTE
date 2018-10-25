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
        public void SavePageHtml()
        {
            var TableModel = this.GetTableModel();
            var TableFielModelList = this.GetTableFieldModel();
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
                TopButton.Append("<button name='DeleteItemID' bnt-click='DeleteItemID' style='margin-right:2px;' type='button' class='btn btn-danger btn-xs'>删　除</button>");
            if (TableModel.IsInsert == 1)
                TopButton.Append("<button name='InsertItemID' bnt-click='InsertItemID' style='margin-right:2px;' type='button' class='btn btn-success btn-xs'>新　增</button>");
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
                    BntHmtl += "<button name='UpdateItemID' bnt-click = 'UpdateItemID'  type = 'button' class='btn btn-warning  btn-xs' value='\"+ data+\"'>修　改</button> ";
                if (TableModel.IsDelete == 1 && TableModel.IsChoice == 0)
                    BntHmtl += "<button name='DeleteItemID' bnt-click='DeleteItemID'  type='button' class='btn btn-danger btn-xs' value='\"+ data+\"'>删　除</button>";
                if (!string.IsNullOrWhiteSpace(BntHmtl))
                    Columns.Append("{\"data\": \"" + "ItemID" + "\", render: function (data, type, row) { return \"" + BntHmtl + "\"}},");
            }
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TableID}", bItemID.ToString());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TableThead}", TableThead.ToString());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{Columns}", Columns.ToString().TrimEnd(','));
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{WhereHtml}", SetStrWhereHtml());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{funaggregate}", "{}");
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TopBotton}", TopButton.ToString());
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{TopHead}", "");
            h_hovertreeTemplate = h_hovertreeTemplate.Replace("{BottomScript}", "");           
            h_sw.Write(h_hovertreeTemplate);
            h_sw.Close();
            fs.Close();
            h_hovertreeSr.Close();
        }
        /// <summary>
        /// 解析转换显示
        /// </summary>
        /// <param name="FieldDataType"></param>
        /// <param name="FieldData"></param>
        /// <param name="FieldKey"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 设置高级查询
        /// </summary>
        /// <returns></returns>
        public string SetStrWhereHtml()
        {
            var TableModel = this.GetTableModel();
            var TableFielModelList = this.GetTableFieldModel();
            string strHtml = "";
            if (TableModel.IsWhere == 1)
            {
                if (TableFielModelList != null && TableFielModelList.Count > 0)
                {
                    foreach (var item in TableFielModelList)
                    {
                        string type = item.SelectType.ToString();
                        switch (type)
                        {
                            case "1":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + item.FieldText + "<span class=\"text-danger\">（模糊查询）</span></label >";
                                strHtml += "<input type=\"text\" name=\"" + item.FieldKey + "\"  class=\"form-control\" placeholder=\"" + item.FieldText + "\" />";
                                strHtml += "</div>";
                                break;
                            case "2":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + item.FieldText + "<span class=\"text-danger\">（下拉查询）</span></label >";
                                strHtml += "<select name=\"" + item.FieldKey + "\" class=\"form-control select2 select2-hidden-accessible\"  aria-hidden=\"true\" >";
                                strHtml += "<option selected = \"selected\" value = \"AllOption\" >全部</option >";
                                string data = item.SelectData;
                                System.Data.DataTable objdata = JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(data);
                                if (objdata != null && objdata.Rows.Count > 0)
                                {
                                    for (int j = 0; j < objdata.Rows.Count; j++)
                                    {
                                        if (objdata.Rows[j][0].ToString().ToUpper() == "SQL")
                                        {
                                            var sqldata = objdata.Rows[j][1].ToString();
                                            System.Data.DataTable tsqldt = BaseClass.GetDataTable(BaseClass.GetValueForKey(sqldata));
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
                                strHtml += "<label class=\"col-xs control-label table-label\">" + item.FieldText + "<span class=\"text-danger\">（等于查询）<span></label >";
                                strHtml += "<input type=\"text\" name=\"" + item.FieldKey + "\" data-type=\"datepicker\"  class=\"form-control\" placeholder=\"" + item.FieldText + "\" />";
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
        /// <summary>
        /// 配置聚合显示SQL
        /// </summary>
        /// <returns></returns>
        public string SetCountSQL(string where)
        {
            var TableModel = this.GetTableModel();
            string strHtml = string.Format("COUNT({0}) AS COUNTS", TableModel.PrimaryKey);
            var PredefinedSQL = TableModel.PredefinedSQL;
            System.Data.DataTable dt = (PredefinedSQL == null || PredefinedSQL == "" ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(PredefinedSQL));//聚合显示
            if (BaseClass.IsNullOrNotNull(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var DataRow = dt.Rows[i];
                    var Type = DataRow["type"].ToString();
                    var Key = DataRow["key"].ToString();
                    var Title = DataRow["title"].ToString();
                    if (Type.ToUpper() == "SQL")
                        strHtml += string.Format(",('{0}'+ CONVERT(VARCHAR(20),({1}))) AS ColumnName" + i.ToString(), Title, BaseClass.GetValueForKey(Key));
                    else
                        strHtml += string.Format(",('{0}'+ CONVERT(VARCHAR(20),{1}({2}))) AS ColumnName" + i.ToString(), Title, Type, Key);
                }
            }
            var WhereSQL = GetWhereSQL(where);
            return " SELECT " + strHtml + " FROM " + TableModel.TableName + " AS NewCyFsTable " + WhereSQL;
        }
        //设置配置聚合显示HTML
        public string SetSumHtml(System.Data.DataTable dataTable)
        {
            if (dataTable.Columns.Count > 1)
            {
                string[] color = { "success", "primary", "info", "warning", "danger", "default" };
                string result = ",\"sumHtml\":\"";
                for (int i = 1; i < dataTable.Columns.Count; i++)
                {
                    var resul = dataTable.Rows[0][i].ToString();
                    result += "<span class='label label-" + color[i - 1] + "' style='font-size: small'>" + resul + "</span>&nbsp;";
                }
                return result + "\"";
            }
            else
                return "";
        }
        /// <summary>
        /// 配置SQL
        /// </summary>
        /// <returns></returns>
        public string SetFieldSQL(string where, string order, int PageIndex, int PageLimit)
        {
            var TableModel = this.GetTableModel();
            var TableFielModelList = this.GetTableFieldModel();
            var TableName = TableModel.TableName;
            int minNum = (Convert.ToInt32(PageIndex) - 1) * Convert.ToInt32(PageLimit) + 1;
            int maxNum = Convert.ToInt32(PageIndex) * Convert.ToInt32(PageLimit);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP(20000) ROW_NUMBER() OVER({0}) AS NewRowID," + TableModel.PrimaryKey + " as ItemID");
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
            var WhereSQL = GetWhereSQL(where);
            var OrderBySQL = GetOrderBySQL(order);
            sb.Append(WhereSQL);
            sb.Append(OrderBySQL);
            var NewPageSQL = string.Format(sb.ToString(), OrderBySQL);
            var PageSQL = string.Format("SELECT * FROM ({0}) AS PageNewCyFsTable WHERE PageNewCyFsTable.NewRowID >={1} AND PageNewCyFsTable.NewRowID <= {2};", NewPageSQL, minNum, maxNum);
            return PageSQL;
        }
        /// <summary>
        /// 配置where条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public string GetWhereSQL(string where)
        {
            var TableFielModelList = this.GetTableFieldModel();
            StringBuilder sb = new StringBuilder();
            System.Data.DataTable dt = (where == null || where == "" ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(where));//条件数据
            if (BaseClass.IsNullOrNotNull(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var FieldKey = dt.Rows[i][0].ToString();
                    var FieldValue = dt.Rows[i][1].ToString();
                    if (!string.IsNullOrWhiteSpace(FieldValue) && FieldValue != "AllOption")
                    {
                        List<Model.M_TableField> listmf = TableFielModelList.Where(x => x.FieldKey == FieldKey).ToList();
                        if (listmf != null && listmf.Count > 0)
                        {
                            var item = listmf[0];
                            var gstype = " = ";
                            var SelectType = item.SelectType;
                            var SelectData = item.SelectData;
                            if (SelectType == 1)
                            {
                                gstype = " LIKE ";
                                FieldValue = "%" + FieldValue + "%";
                            }
                            if (SelectType == 2 || SelectType == 3)
                                gstype = " = ";
                            sb.Append(" NewCyFsTable." + FieldKey + gstype + FieldValue);
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(sb.ToString()))
                return "";
            else
                return " WHERE " + sb.ToString();
        }
        /// <summary>
        /// 配置排序
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetOrderBySQL(string order)
        {
            var TableFielModelList = this.GetTableFieldModel();
            StringBuilder sb = new StringBuilder();
            System.Data.DataTable dt = (order == null ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(order));//条件数据
            if (BaseClass.IsNullOrNotNull(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var Index = Convert.ToInt32(dt.Rows[i][0].ToString());
                    var FieldKey = TableFielModelList[Index].FieldKey;
                    var FieldValue = dt.Rows[i][1].ToString();
                    if (!string.IsNullOrWhiteSpace(FieldValue))
                        sb.Append(" NewCyFsTable." + FieldKey + " " + FieldValue + ",");
                }
            }
            if (string.IsNullOrWhiteSpace(sb.ToString()))
                return "";
            else
                return " ORDER BY " + sb.ToString().TrimEnd(',');
        }




        /// <summary>
        /// 获取表格数据Json
        /// </summary>
        /// <param name="PageStart"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetDataListJson(int PageStart, int PageIndex, int PageSize, string where, string order)
        {
            var TableModel = this.GetTableModel();
            if (TableModel.TableType == 1)
            {
                System.Data.DataSet ds = BaseClass.GetDataSet(SetFieldSQL(where, order, PageIndex, PageSize) + SetCountSQL(where));
                if (ds != null)
                {
                    System.Data.DataTable tableJson = ds.Tables[0];
                    System.Data.DataTable tableCount = ds.Tables[1];
                    if (tableJson != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("{\"total\":" + tableCount.Rows[0]["COUNTS"].ToString() + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                        string datatablejson = JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                        sb.Append(datatablejson);
                        sb.Append(SetSumHtml(tableCount));
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
            else if (TableModel.TableType == 2)
            {
                var TableFielModelList = this.GetTableFieldModel();
                System.Data.DataTable dataTable = BaseClass.GetDataTableColumns(TableModel.SQL);
                var tableJson = BaseClass.GetDataTableForXML(dataTable, TableModel.TableName);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"total\":" + tableJson.Rows.Count + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                var datatablejson = JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                sb.Append(datatablejson);
                sb.Append("}");
                return sb.ToString().Replace("\n", "");
            }
            else
            {
                return "{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[]}";
            }
        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public string GetDataView(string ItemID)
        {
            var TableModel = this.GetTableModel();
            if (TableModel.TableType == 1)//表格数据
            {
                string sql = string.Format("SELECT TOP(1)* FROM ({0}) AS NewCyFsTable  WHERE NewCyFsTable.{1}='{2}'", TableModel.SQL, TableModel.PrimaryKey, ItemID);
                System.Data.DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
                if (tableJson != null && tableJson.Rows.Count > 0)
                    return JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                else
                    return "";
            }
            else if (TableModel.TableType == 2)//XML数据
            {
                var TableFielModelList = this.GetTableFieldModel();
                System.Data.DataTable dataTable = BaseClass.GetDataTableColumns(TableModel.SQL);
                var tableJson = BaseClass.GetDataViewForXML(dataTable, TableModel.TableName, ItemID);
                if (tableJson != null && tableJson.Rows.Count > 0)
                    return JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                else
                    return "";
            }
            else
                return "";
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public string InsertTableData(string FromValues)
        {
            var TableModel = this.GetTableModel();
            BLL.ObjectData ModelData = new BLL.ObjectData(TableModel.TableName);
            ModelData.SetValues(FromValues);//表单数据
            if (TableModel.TableType == 1)//表格数据
                return BaseClass.InsertModel(ModelData, TableModel.PrimaryKey);
            else if (TableModel.TableType == 2)//XML数据
                return BaseClass.XmlInsertTableModel(ModelData, TableModel.PrimaryKey);
            else
                return "";
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public bool UpdateTableData(string FromValues, string ItemID)
        {
            var TableModel = this.GetTableModel();
            BLL.ObjectData ModelData = new BLL.ObjectData(TableModel.TableName);
            ModelData.SetValues(FromValues);//表单数据
            if (TableModel.TableType == 1)//表格数据
                return BaseClass.UpdateModel(ModelData, TableModel.PrimaryKey, ItemID);
            else if (TableModel.TableType == 2)//XML数据
                return BaseClass.XmlUpdateTableModel(ModelData, TableModel.PrimaryKey, ItemID);
            else
                return false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ItemIDs"></param>
        /// <returns></returns>
        public bool DeleteTableData(string ItemIDs)
        {
            var TableModel = this.GetTableModel();
            if (TableModel.TableType == 1)
                return BaseClass.DeleteModel(TableModel.TableName, TableModel.PrimaryKey, ItemIDs);
            else
                return BaseClass.XmlDeleteTableModel(TableModel.TableName, TableModel.PrimaryKey, ItemIDs);
        }
    }
}

