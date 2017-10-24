using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class Cases_baseprint_HomePage_DataList : System.Web.UI.Page
{
    public static string listColumn = "GUID|菜单编号,ParentID|父级菜单,MeanHeader|菜单Header,MeanName|菜单名称,MeanUrl|菜单地址,MeanLevel|菜单级别,MeanClass|菜单样式,MeanOrder|排序,StatusID|状态,CreateTime|创建时间";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string GetType = Request.QueryString["gettype"];
            int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
            int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
            int PageStart = Convert.ToInt32(Request.QueryString["start"]);
            var PageData = Request.QueryString["values"];
            if (GetType != null && GetType == "getDate")
            {
                DataTable dt = JsonHelper.DeserializeJsonToObject<DataTable>(PageData);
                string sqlStr = string.Format(@"SELECT * FROM [qds108295464_db].[dbo].[t_Mean] where 1=1 " + getSQLWhere(dt));
                getDataJson(BLL.BaseClass.getDataTable(sqlStr), PageIndex, PageSize);
            }
        }
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
                    if (dt.Rows[i]["name"].ToString() == "MeanName")
                    {
                        strWhere += " and " + dt.Rows[i]["name"].ToString() + " like '%" + dt.Rows[i]["value"].ToString() + "%'";
                    }
                    else if (dt.Rows[i]["name"].ToString() == "MeanClass")
                    {
                        strWhere += " and " + dt.Rows[i]["name"].ToString() + " = '" + dt.Rows[i]["value"].ToString() + "'";
                    }
                    else if (dt.Rows[i]["name"].ToString() == "CreateTime")
                    {
                        strWhere += " and convert(varchar(50)," + dt.Rows[i]["name"].ToString() + ",23) <= '" + dt.Rows[i]["value"].ToString() + "'";
                    } 
                }
                
            }  
        }
        return strWhere;
    }
}