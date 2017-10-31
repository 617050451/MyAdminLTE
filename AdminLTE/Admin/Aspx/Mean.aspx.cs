﻿using System;
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
    public partial class Mean : System.Web.UI.Page
    {
        public static string listColumn = "GUID|菜单编号|1,ParentID|父级菜单|1,MeanHeader|菜单Header|1,MeanName|菜单名称|1";
		public static string SQLWhere ="MeanName↑菜单名称↑1↓CreateTime↑CreateTime↑2↓MeanClass↑MeanClass↑3↑sql◇SELECT DISTINCT([MeanClass]) AS 'key',([MeanClass]) as 'value'  FROM [qds108295464_db].[dbo].[t_Mean]";

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
                        DataTable dt = JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                        string sqlStr = string.Format(@"SELECT * FROM[qds108295464_db].[dbo].[t_Mean] where 1=1  " + getSQLWhere(dt));
                        getDataJson(BLL.BaseClass.getDataTable(sqlStr), PageIndex, PageSize);
                    }
                    else if (GetType == "setHtml")
                    {
                        Response.Write(BaseClass.setStrHtml(SQLWhere));
                        Response.End();
                    }
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
        //高级查询
        string getSQLWhere(DataTable dt)
        {
            string strWhere = "";
            if (dt != null && dt.Rows.Count > 0)
            { 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["value"].ToString() != "")
                    {
                       string[] list = dt.Rows[i]["name"].ToString().Split('|');
                       strWhere += BaseClass.setStrWhere(list[0], dt.Rows[i]["value"].ToString(), list[1]);
                    }
                }
            }
            return strWhere;
        }
    }
}