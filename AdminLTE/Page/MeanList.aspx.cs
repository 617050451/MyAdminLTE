using BLL;
using System;
using System.Data;

namespace AdminLTE.Admin.Aspx
{
    public partial class MeanList : System.Web.UI.Page
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
                    var WhereValues = Request.QueryString["WhereValues"];
                    if (GetType == "GetDataList")
                    {
                        DataTable dt = (WhereValues == null ? null : JsonHelper.DeserializeJsonToObject<DataTable>(WhereValues));//条件数据
                        Response.Write(BLL.MeanListClass.GetDataJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                        Response.End();
                    }
                    else if (GetType == "BntOperation")
                    {
                        string values = Request.QueryString["values"];
                        Response.Write(BLL.BaseClass.DeleteItemID(BLL.MeanListClass.TMeanList.TableName, BLL.MeanListClass.OneFileName, values));
                        Response.End();
                    }
                }
                else
                {
                    ltlhead.Text = BLL.MeanListClass.GetTableHtml();
                    ltlbnt.Text = BLL.MeanListClass.SetBntHtml();
                    ltlStrWhere.Text = BLL.MeanListClass.SetStrWhereHtml();
                    IsPlus.Value = BLL.MeanListClass.TMeanList.Plus;
                    IsWhere.Value = BLL.MeanListClass.TMeanList.Strwhere;
                    ColumnsJson.Value = BLL.MeanListClass.ColumnsJson;
                    IsChoice.Value = BLL.MeanListClass.TMeanList.Choice;
                }
            }
        }
    }
}