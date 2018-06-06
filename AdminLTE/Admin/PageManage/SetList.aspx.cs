using System;
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
        public BLL.t_TablesClass tableModel;
        public string ItemGUID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ItemGUID = Request["ItemGUID"];
            tableModel = new BLL.t_TablesClass(ItemGUID);
            if (!IsPostBack)
            {
                string GetType = Request.Form["gettype"];
                if (GetType != null)
                {
                    if (GetType == "SetData")
                    {
                        var PageData = Request.Form["values"];
                        var TableInfo = Request.Form["tableInfo"];
                        DataTable dt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                        DataTable tableInfodt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(TableInfo);
                        if (tableModel.SaveUpdateList(dt, tableInfodt))
                        {
                            Response.Write("True");
                            Response.End();
                        }
                    }
                    else if (GetType == "SetTableData")
                    {
                        var SetTableInfo = Request.Form["settableinfo"];
                        DataTable SetTableInfodt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(SetTableInfo);
                        if (tableModel.SaveUpdateList(null, SetTableInfodt))
                        {
                            //更新TableFieldInfo信息
                            tableModel.SetTableFieldInfo();
                            Response.Write("True");
                            Response.End();
                        }
                    }
                    else if (GetType == "SetOrder")
                    {
                        Response.Write(tableModel.SetOrder());
                        Response.End();
                    }
                }
            }
        }
        public string GetSetListHtml(string ItemGUID)
        {
            DataTable dt = tableModel.TableFieldInfo;
            StringBuilder sb = new StringBuilder();
            if (BLL.BaseClass.estimate(dt))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FieldKey = dt.Rows[i]["FieldKey"].ToString();
                    string FieldValue = dt.Rows[i]["FieldValue"].ToString();
                    string FieldDataType = dt.Rows[i]["FieldDataType"].ToString();
                    string FieldData = dt.Rows[i]["FieldData"].ToString();
                    string FieldStatusID = dt.Rows[i]["FieldStatusID"].ToString();
                    string FieldOrder = dt.Rows[i]["FieldOrder"].ToString();
                    string SelectType = dt.Rows[i]["SelectType"].ToString();
                    string SelectData = dt.Rows[i]["SelectData"].ToString();
                    sb.Append("<tr role=\"row\" class=\"odd\">");
                    sb.Append("<td>" + FieldKey + "<div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldKey\" class=\"form-control hidden\" value='" + FieldKey + "'/></div></td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldValue\"  class=\"form-control\" value='" + FieldValue + "\'/></div></td>");
                    sb.Append("<td class=\"form-inline\"><div class=\"input-group input-group-sm\"><select name=\"FieldDataType\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\">");
                    sb.Append("<option " + (FieldDataType == "1" ? "selected=\"selected\"" : "") + " value=\"1\">原始数据</option>");
                    sb.Append("<option " + (FieldDataType == "2" ? "selected=\"selected\"" : "") + " value=\"2\">固定前台转换</option>");
                    sb.Append("<option " + (FieldDataType == "3" ? "selected=\"selected\"" : "") + " value=\"3\">动态前台转换</option>");
                    sb.Append("<option " + (FieldDataType == "4" ? "selected=\"selected\"" : "") + " value=\"4\">数据格式化</option>");
                    sb.Append("</select></div><a class=\"text-primary " + (FieldDataType == "1" ? "hidden" : "") + "\" href=\"javascript:void(0)\" onclick=\"setFieldData(this)\">&nbsp;设置</a><input type=\"text\" name=\"FieldData\" class=\"form-control hidden\" value='" + FieldData + "'/></td>");
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
                    sb.Append("&nbsp;<a class=\"text-primary " + (SelectType == "2" ? "" : "hidden") + "\"  href=\"javascript:void(0)\" onclick=\"setSelectData(this)\">&nbsp;设置</a>");
                    sb.Append("<input type=\"text\" class=\"form-control hidden\" name=\"SelectData\" value='" + SelectData + "'/>");
                    sb.Append("</td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\"   class=\"form-control\"  name=\"FieldOrder\" value='" + FieldOrder + "'/></div></td>");
                }
            }
            return sb.ToString();
        }
    }
}