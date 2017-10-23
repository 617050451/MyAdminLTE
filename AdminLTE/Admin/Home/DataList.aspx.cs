using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cases_baseprint_HomePage_DataList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string GetType =Request.QueryString["gettype"];
            int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
            int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
            if (GetType != null && GetType == "getDate")
            {
                PageIndex = 1;
                PageSize = 25;
                string sqlStr = string.Format(@"SELECT * FROM [qds108295464_db].[dbo].[t_Mean]");
                getPrintPrizeJson(BLL.BaseClass.getDataTable(sqlStr), PageIndex, PageSize);
            }
           
        }
    }
    //获取数据
    void getPrintPrizeJson(DataTable dt, int PageIndex, int PageSize)
    {
        if (dt != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + dt.Rows.Count + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.Append("{\"GUID\":\"" + dt.Rows[i]["GUID"].ToString() + "\"");
                sb.Append(",\"ParentID\":\"" + dt.Rows[i]["ParentID"].ToString() + "\"");
                sb.Append(",\"MeanHeader\":\"" + dt.Rows[i]["MeanHeader"].ToString() + "\"");
                sb.Append(",\"MeanName\":\"" + dt.Rows[i]["MeanName"].ToString() + "\"");
                sb.Append(",\"MeanUrl\":\"" + dt.Rows[i]["MeanUrl"].ToString() + "\"");
                sb.Append(",\"MeanLevel\":\"" + dt.Rows[i]["MeanLevel"].ToString() + "\"");
                sb.Append(",\"MeanClass\":\"" + dt.Rows[i]["MeanClass"].ToString() + "\"");
                sb.Append(",\"MeanOrder\":\"" + dt.Rows[i]["MeanOrder"].ToString() + "\"");
                sb.Append(",\"StatusID\":\"" + dt.Rows[i]["StatusID"].ToString() + "\"");
                sb.Append(",\"CreateTime\":\"" + Convert.ToDateTime(dt.Rows[i]["CreateTime"].ToString()).ToString("yyyy-MM-dd") + "\"}");
            }
            sb.Append("]}");
            Response.Write(sb.ToString().Replace("\n", ""));
            Response.End();
        }
        else
        {
            Response.Write("{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[]}");
            Response.End();
        }
    }
    //分页
    public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
    { 
        if (PageIndex == 0)
            return dt;//0页代表每页数据，直接返回
        DataTable newdt = dt.Copy();
        newdt.Clear();//copy dt的框架
        int rowbegin = (PageIndex - 1) * PageSize;
        int rowend = PageIndex * PageSize;
        if (rowbegin >= dt.Rows.Count)
            return newdt;//源数据记录数小于等于要显示的记录，直接返回dt
        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }
        return newdt;
    }
}