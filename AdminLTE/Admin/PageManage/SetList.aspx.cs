﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin
{
    public partial class SetList : BasePage
    {
        public int ItemID { get; set; }
        public Model.M_Table TableModel { get; set; }
        public List<Model.M_TableField> TableFielModelList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request["ItemID"]))
            {
                //输入错误
            }
            ItemID = Convert.ToInt32(Request["ItemID"]);
            BLL.B_Table TableBll = new BLL.B_Table(ItemID);
            TableModel = TableBll.GetTableModel();
            TableFielModelList = TableBll.GetTableFieldModel();
            if (!IsPostBack)
            {
                string GetType = Request.Form["gettype"];
                if (GetType != null)
                {
                    if (GetType == "SetData")
                    {
                        var PageData = Request.Form["values"];
                        var TableInfo = Request.Form["tableInfo"];
                        DataTable TableInfoDt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(TableInfo);
                        DataTable TableFileInfoDt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                        if (TableBll.UpdateTableModel(TableInfoDt) && TableBll.UpdateTableFieldModel(TableFileInfoDt))
                        {
                            TableBll.SavePageHtml(TableModel, TableFielModelList);
                            Response.Write("True");
                        }                           
                        else
                            Response.Write("False");
                        Response.End();
                    }
                    else if (GetType == "SetTableData")
                    {
                        var SetTableInfo = Request.Form["settableinfo"];
                        DataTable SetTableInfodt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(SetTableInfo);
                        if (TableBll.UpdateTableModel(SetTableInfodt))
                        {
                            //更新TableFieldInfo信息
                            TableBll.ToUpdateTableFieldModel();
                            Response.Write("True");
                        }
                        else
                            Response.Write("False");
                        Response.End();
                    }
                    else if (GetType == "SetOrder")
                    {
                        Response.Write(TableBll.SetOrder());
                        Response.End();
                    }
                }
            }
        }
        public string GetSetListHtml(List<Model.M_TableField> TableFielModelList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in TableFielModelList)
            {
                sb.Append(BLL.BaseClass.XmlSelectAllTableFieldXml(item));
            }
            return sb.ToString();
        }
    }
}