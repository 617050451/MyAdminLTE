using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace AdminLTE.Admin.Aspx
{
    public partial class MeanList : System.Web.UI.Page
    {
	    public string columnsJson = "";
        public static string guid = "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18";
        public static DataTable tableInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
			    string GetType = Request.QueryString["gettype"];
                int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
                int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
                int PageStart = Convert.ToInt32(Request.QueryString["start"]);
                var PageData = Request.QueryString["values"];
                if (GetType != null)
                {
                    if (GetType == "getDate")
                    {
                        if (tableInfo != null && tableInfo.Rows.Count > 0)
                        {
                            string tsql = tableInfo.Rows[0]["TSQL"].ToString();
                            DataTable dt = JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                            Response.Write(BLL.BaseClass.getDataJson(tsql, dt, PageStart, PageIndex, PageSize));
                            Response.End();
                        }
                    }
                    else if (GetType == "bntOperation")
                    {
                        tableInfo = BLL.BaseClass.getTableInfo(guid);
                        string values = Request.QueryString["values"];
                        Response.Write(BLL.BaseClass.deleteGUID(tableInfo, values));
                        Response.End();
                    }
                }
                else
                {
                    tableInfo = BLL.BaseClass.getTableInfo(guid);
                    DataTable tableFieldInfo = BLL.BaseClass.getTableFieldInfo(guid); 
                    ltlhead.Text = BLL.BaseClass.getTableHtml(tableFieldInfo, tableInfo.Rows[0]["choice"].ToString(), ref columnsJson);
                    ltlStrWhere.Text = BaseClass.setStrWhereHtml(tableFieldInfo);
					ltlbnt.Text = BLL.BaseClass.setBntHtml(tableInfo);
                }
            }
			//StartWriteOne                                                                                
            //EndWriteOne
        }
    }
}