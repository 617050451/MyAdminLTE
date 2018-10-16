using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE.Ajax
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    public class GetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JudgeType(context);
        }

        public void JudgeType(HttpContext context)
        {
            var GetType = context.Request["GetType"];
            switch (GetType)
            {
                case "GetDataList":
                    GetDataList(context);
                    break;
                default:
                    break;
            }
        }
        public void GetDataList(HttpContext context)
        {
            BLL.B_Table TableBll = new BLL.B_Table(1);
            int PageIndex = Convert.ToInt32(context.Request["page"]);
            int PageSize = Convert.ToInt32(context.Request["limit"]);
            int PageStart = Convert.ToInt32(context.Request["start"]);
            string Order = context.Request["order"];
            string OSrderDir = context.Request["orderDir"];
            var WhereValues = context.Request["WhereValues"];
            System.Data.DataTable dt = (WhereValues == null ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(WhereValues));//条件数据
            context.Response.Write(TableBll.GetDataListJson(TableBll.GetTableModel(), PageStart, PageIndex, PageSize, ""));
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}