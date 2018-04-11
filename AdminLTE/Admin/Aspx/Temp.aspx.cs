using BLL;
using System;
using System.Data;

namespace AdminLTE.Admin.Aspx
{
    public partial class Temp : System.Web.UI.Page
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
                        Response.Write(BLL.MeanList.GetDataJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                        Response.End();
                    }
                    else if (GetType == "bntOperation")
                    {
                        string values = Request.QueryString["values"];
                        Response.Write(BLL.BaseClass.DeleteItemID(BLL.MeanList.FileName, BLL.MeanList.OneFileName, values));
                        Response.End();
                    }
                }
                else
                {
                    ltlhead.Text = BLL.MeanList.GetTableHtml();
                    ltlbnt.Text = BLL.MeanList.setBntHtml();
                    ltlStrWhere.Text = BLL.MeanList.SetStrWhereHtml();
                    IsPlus.Value = BLL.MeanList.Plus;
                    IsWhere.Value = BLL.MeanList.Strwhere;
                    ColumnsJson.Value = BLL.MeanList.ColumnsJson;
                }
            }
        }
    }
}