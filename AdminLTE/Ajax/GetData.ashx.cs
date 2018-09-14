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
        public BLL.t_TablesClass tableModel = new BLL.t_TablesClass("9D2512E9-6FF4-4E7E-BBB8-23DE83755D17");
        public void GetDataList(HttpContext context)
        {
            int PageIndex = Convert.ToInt32(context.Request["page"]);
            int PageSize = Convert.ToInt32(context.Request["limit"]);
            int PageStart = Convert.ToInt32(context.Request["start"]);
            string Order = context.Request["order"];
            string OSrderDir = context.Request["orderDir"];
            var WhereValues = context.Request["WhereValues"];
            System.Data.DataTable dt = (WhereValues == null ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(WhereValues));//条件数据
            context.Response.Write(tableModel.GetDataListJson(dt, PageStart, PageIndex, PageSize, ""));
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