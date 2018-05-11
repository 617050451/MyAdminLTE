using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace AdminLTE.Admin.Temp
{
    public partial class AddPage : System.Web.UI.Page
    {
        BLL.t_TablesClass tableModel = new t_TablesClass("9D2512E9-6FF4-4E7E-BBB8-23DE83755D18");
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
                        Response.Write(tableModel.GetDataListJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                        Response.End();
                    }
                    else if (GetType == "SaveFromData")
                    {
                        var ChoiceValue = Request.QueryString["ChoiceValue"];
                        var FromValues = Server.UrlDecode(Request.QueryString["FromValues"]);
                        Model.t_Tables ModelData = (FromValues == null ? null : JsonHelper.DeserializeJsonToObject<Model.t_Tables>(FromValues));//表单数据
                        var RowNum = true;
                        var CodeJson = "";
                        if (ChoiceValue != null && ChoiceValue != "")//修改
                        {
                            //RowNum = tableModel.UpdateModel(ModelData, ChoiceValue);
                        }
                        else//添加
                        {
                            //RowNum = tableModel.InsertModel(ModelData);
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
                }
                else
                {
                    ltlhead.Text = tableModel.GetTableHtml();
                    ltlbnt.Text = tableModel.SetBntHtml();
                    ltlStrWhere.Text = tableModel.SetStrWhereHtml();
                    ColumnsJson.Value = tableModel.ColumnsJson;
                    IsPlus.Value = tableModel.TableModel.Plus.ToString();
                    IsWhere.Value = tableModel.TableModel.Strwhere.ToString();
                    IsChoice.Value = tableModel.TableModel.Choice.ToString();
                }
            }
        }
    }
}