using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request.Form["gettype"];
                var PageData = Request.Form["values"];
                if (GetType != null)
                {
                    if (GetType == "setDate")
                    {
                        DataTable dt = BLL.JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                        if (BLL.BaseClass.updateList(dt, "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18"))
                        {
                            Response.Write("True");
                            Response.End();
                        };
                    }
                }
                else
                {
                    ltlTable.Text = getSetListHtml();
                }
            }
        }
        string getSetListHtml()
        {
            string sql = string.Format(@"select * from [t_TableField] ORDER BY [FieldOrder]");
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
                    sb.Append("<tr role=\"row\" class=\"odd\">");
                    sb.Append("<td>"+ FieldKey + "<div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldKey\" class=\"form-control hidden\" value='"+ FieldKey + "'/></div></td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldValue\" value='"+ FieldValue + "\'/></div></td>");
                    sb.Append("<td>显示转换<div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldData\" class=\"form-control hidden\" value='"+ FieldData + "'/></div></td>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><select name=\"FieldStatusID\" class=\"form-control select2 select2-hidden-accessible\" tabindex=\"-1\" aria-hidden=\"true\"><option "+ (FieldStatusID == "1" ? "selected=\"selected\"" : "") + " value=\"1\">启用</option><option " + (FieldStatusID == "0" ? "selected=\"selected\"" : "") + " value=\"0\">禁用</option></select></td></div>");
                    sb.Append("<td><div class=\"input-group input-group-sm\"><input type=\"text\" name=\"FieldOrder\" value='" + FieldOrder + "'/></div></td>");
                }
            }
            return sb.ToString();
        }
    }
}