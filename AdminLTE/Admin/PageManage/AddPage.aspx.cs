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
                    var WhereValues = Request.QueryString["WhereValues"];
                    if (GetType == "GetDataList")
                    {
                        DataTable dt = (WhereValues == null ? null : JsonHelper.DeserializeJsonToObject<DataTable>(WhereValues));//条件数据
                        Response.Write(BLL.t_TablesClass.GetDataListJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                        Response.End();
                    }
                    else if (GetType == "SaveFromData")
                    {
                        var ChoiceKey = BLL.t_TablesClass.OneFileName;
                        var ChoiceValue = Request.QueryString["ChoiceValue"];
                        var FromValues = Request.QueryString["FromValues"];
                        Model.t_Tables ModelData = (FromValues == null ? null : JsonHelper.DeserializeJsonToObject<Model.t_Tables>(FromValues));//表单数据
                        var RowNum = true;
                        var CodeJson = "";
                        if (ChoiceValue != null && ChoiceValue != "")//修改
                        {
                            RowNum = BLL.BaseClass.UpdateModel(ModelData, ChoiceKey, ChoiceValue);
                        }
                        else//添加
                        {
                            RowNum = BLL.BaseClass.InsertModel(ModelData, ChoiceKey);
                        }
                        if (RowNum)
                            CodeJson = "[{\"code\":100}]";
                        else
                            CodeJson = "[{\"code\":105}]";
                        Response.Write(CodeJson);
                        Response.End();
                    }
                    else if (GetType == "GetDataView")
                    {
                        var ChoiceKey = BLL.t_TablesClass.OneFileName;
                        var ChoiceValue = Request.QueryString["ChoiceValue"];
                        Response.Write(BLL.t_TablesClass.GetDataViewJson(ChoiceKey, ChoiceValue));
                        Response.End();
                    }
                }
                else
                {
                    ltlbnt.Text = "<button type=\"button\" class=\"btn btn-success btn-xs\" onclick=\"LayerOpenHtml('添加页面','SaveInsertFromData')\">新　增</button>";
                }
            }
        }
    }
}