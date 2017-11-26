﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin.Temp
{
    public partial class setList : System.Web.UI.Page
    {
        public static DataTable tableInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request.Form["gettype"];
                if (GetType != null)
                {
                    if (GetType == "setData")
                    {
                        string TableGuid = Request.Form["tableguid"];
                        var PageData = Request.Form["values"];
                        var TableInfo = Request.Form["tableInfo"];
                        DataTable dt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                        DataTable tableInfodt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(TableInfo);
                        if (BLL.BaseClass.SaveUpdateList(dt,tableInfodt,TableGuid))
                        {
                            Response.Write("True");
                            Response.End();
                        }
                    }
                    else if (GetType == "setTableData")
                    {
                        string TableGuid = Request.Form["tableguid"];
                        var SetTableInfo = Request.Form["settableinfo"];
                        DataTable SetTableInfodt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(SetTableInfo);
                        if (BLL.BaseClass.SaveUpdateList(null, SetTableInfodt, TableGuid))
                        {
                            Response.Write("True");
                            Response.End();
                        };
                    }
                }
                else
                {
                    string tableGuid = Request.QueryString["tableguid"];
                    if (tableGuid != null)
                    {
                        tableInfo = BLL.BaseClass.getTableInfo(tableGuid);
                        ltlTable.Text = getSetListHtml(tableGuid);
                    }
                }
            }
        }
        string getSetListHtml(string tableGuid)
        {
            //string sql = string.Format(@"select * from [t_TableField]  where TableGUID ='{0}' ORDER BY [FieldOrder]", tableGuid);
            string sql = string.Format(@"select * from [t_TableField]  ORDER BY [FieldOrder]");
            DataTable dt = BLL.BaseClass.getDataTable(sql);
            StringBuilder sb = new StringBuilder();
            if (BLL.BaseClass.estimate(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FieldKey = dt.Rows[i]["FieldKey"].ToString();
                    string FieldValue = dt.Rows[i]["FieldValue"].ToString();
                    string FieldData = dt.Rows[i]["FieldData"].ToString();
                    string FieldStatusID = dt.Rows[i]["FieldStatusID"].ToString();
                    string FieldOrder = dt.Rows[i]["FieldOrder"].ToString();
                    string SelectType = dt.Rows[i]["SelectType"].ToString();
                    string SelectData = dt.Rows[i]["SelectData"].ToString();
                    sb.Append("<tr role=\"row\" class=\"odd\">");
                    sb.Append("<td>" + FieldKey + "<div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldKey\" class=\"form-control hidden\" value='" + FieldKey + "'/></div></td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldValue\"  class=\"form-control\" value='" + FieldValue + "\'/></div></td>");
                    sb.Append("<td><a class=\"text-primary\"  href=\"javascript:void(0)\" onclick=\"setFieldData(this)\">设置</a><input type=\"text\" name=\"FieldData\" class=\"form-control hidden\" value='" + FieldData + "'/></td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><select name=\"FieldStatusID\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
                    sb.Append("<option " + (FieldStatusID == "1" ? "selected=\"selected\"" : "") + " value=\"1\">启用</option>");
                    sb.Append("<option " + (FieldStatusID == "0" ? "selected=\"selected\"" : "") + " value=\"0\">禁用</option>");
                    sb.Append("</select></div></td>");
                    sb.Append("<td class=\"form-inline\"><div class=\"input-group input-group-sm \"><select name=\"SelectType\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
                    sb.Append("<option " + (SelectType == "0" ? "selected=\"selected\"" : "") + " value=\"0\">不启用</option>");
                    sb.Append("<option " + (SelectType == "1" ? "selected=\"selected\"" : "") + " value=\"1\">模糊查询</option>");
                    sb.Append("<option " + (SelectType == "2" ? "selected=\"selected\"" : "") + " value=\"2\">下拉查询</option>");
                    sb.Append("<option " + (SelectType == "3" ? "selected=\"selected\"" : "") + " value=\"3\">等于查询</option>");
                    sb.Append("</select></div>");
                    if (SelectType == "0")
                        sb.Append("&nbsp;<a class=\"text-primary hidden\"  href=\"javascript:void(0)\" onclick=\"setSelectData(this)\">设置</a>");
                    else
                        sb.Append("&nbsp;<a class=\"text-primary\"  href=\"javascript:void(0)\" onclick=\"setSelectData(this)\">设置</a>"); 
                    sb.Append("<input type=\"text\" class=\"form-control hidden\" name=\"SelectData\" value=\"" + SelectData + "\"/>");
                    sb.Append("</td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\"   class=\"form-control\"  name=\"FieldOrder\" value='" + FieldOrder + "'/></div></td>");
                }
            }
            return sb.ToString();
        }
    }
}