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
                    break;                     
                default:
                    break;
            }
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