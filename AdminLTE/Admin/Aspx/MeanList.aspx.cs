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
        public string guid = "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18";//9D2512E9-6FF4-4E7E-BBB8-23DE83755D18
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
                        DataTable tableInfo = BLL.BaseClass.getTableInfo(guid);
                        if (tableInfo != null && tableInfo.Rows.Count > 0)
                        {
                            string tsql = tableInfo.Rows[0]["TSQL"].ToString();
                            DataTable dt = JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                            string sqlStr = tsql + BLL.BaseClass.setStrWhere(dt);
                            getDataJson(BLL.BaseClass.getDataTable(sqlStr), PageIndex, PageSize);
                        }
                    }
                }
                else
                {
                    DataTable tableFieldInfo = BLL.BaseClass.getTableFieldInfo(guid);
                    lblhead.Text = BLL.BaseClass.getTableHtml(tableFieldInfo, ref columnsJson);
                    lblStrWhere.Text = BaseClass.setStrWhereHtml(tableFieldInfo);
                }
            }
			//StartWriteOne
            //EndWriteOne
        }
        //获取数据
        void getDataJson(DataTable dt, int PageIndex, int PageSize)
        {
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"total\":" + dt.Rows.Count + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                string datatablejson = JsonHelper.DataTableToJsonWithJsonNet(dt);
                sb.Append(datatablejson);
                sb.Append("}");
                Response.Write(sb.ToString().Replace("\n", ""));
                Response.End();
            }
            else
            {
                Response.Write("{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[]}");
                Response.End();
            }
			//StartWriteTwo
            //EndWriteTwo
        }
    }
}