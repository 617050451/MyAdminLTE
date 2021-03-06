﻿using System;
using System.Collections.Generic;

namespace AdminLTE.Page
{
    public partial class MoreButtonList : BasePage
    {
       public  BLL.t_TablesClass tableModel = new BLL.t_TablesClass("01640bc0-c350-42a4-bda5-7247252afc0a");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request.QueryString["gettype"];
                if (GetType != null)
                {
                    LoadBind(GetType);
                }
            }
        }
        void LoadBind(string GetType)
        {
            int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
            int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
            int PageStart = Convert.ToInt32(Request.QueryString["start"]);
            string Order = Request.QueryString["order"];
            string OSrderDir = Request.QueryString["orderDir"];
            var WhereValues = Request.QueryString["WhereValues"];
            if (GetType == "GetDataList")
            {
                System.Data.DataTable dt = (WhereValues == null ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(WhereValues));//条件数据
                Response.Write(tableModel.GetDataListJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                Response.End();
            }
            else if (GetType == "SaveFromData")
            {
                var ChoiceValue = Request.QueryString["ChoiceValue"];
                var FromValues = Server.UrlDecode(Request.QueryString["FromValues"]);
                BLL.ObjectData ModelData = new BLL.ObjectData(tableModel.TableModel.TableName);
                ModelData.SetValues(FromValues);//表单数据
                var RowNum = true;
                var CodeJson = "";
                if (ChoiceValue != null && ChoiceValue != "")//修改
                {
                    RowNum = tableModel.UpdateModel(ModelData, ChoiceValue);
                    if (RowNum)
                        CodeJson = "[{\"code\":100}]";
                    else
                        CodeJson = "[{\"code\":105}]";
                }
                else//添加
                {
                    var ItemID = tableModel.InsertModel(ModelData);
                    if (string.IsNullOrWhiteSpace(ItemID))
                        CodeJson = "[{\"code\":100}]";
                    else
                        CodeJson = "[{\"code\":105}]";
                }              
                Response.Write(CodeJson);
                Response.End();
            }
            else if (GetType == "GetDataView")
            {
                var ChoiceValue = Request.QueryString["ChoiceValue"];
                Response.Write(tableModel.GetDataViewJson(ChoiceValue));
                Response.End();
            }
            else if (GetType == "BntOperation")
            {
                var ChoiceValue = Request["ChoiceValue"];
                if (!string.IsNullOrWhiteSpace(ChoiceValue))
                    Response.Write(tableModel.DeleteData(ChoiceValue));
                else
                    Response.Write("False");
                Response.End();
            }
            else if (GetType == "GetFieldKeyValue")
            {
                Dictionary<string, string> data = new Dictionary<string, string> { };
                for (int i = 0; i < Request.QueryString.Count; i++)
                {
                    data.Add(Request.QueryString.Keys[i].ToString(), Request.QueryString[i].ToString());
                }
                if (data.Count > 0)
                    Response.Write(tableModel.GetFieldKeyValue(data));
                else
                    Response.Write("False");
                Response.End();
            }
        }
    }
}