using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin.Temp
{
    public partial class AddPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request.QueryString["gettype"];
                if (GetType != null)
                {
                    int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
                    int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
                    int PageStart = Convert.ToInt32(Request.QueryString["start"]);
                    string Order = Request.QueryString["order"];
                    string OSrderDir = Request.QueryString["orderDir"];
                    var PageData = Request.QueryString["values"];
                    if (GetType == "getDate")
                    {
                        DataTable dt = JsonHelper.DeserializeJsonToObject<DataTable>(PageData);//条件数据
                        Response.Write(BLL.TablesClass.GetDataJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                        Response.End();
                    }
                }
                else
                {
                    ltlbnt.Text = "<button type=\"button\" class=\"btn btn-success btn-xs\" onclick=\"ShowAddTableHtml('添加页面')\">新　增</button>";
                }
            }
        }
    }
}