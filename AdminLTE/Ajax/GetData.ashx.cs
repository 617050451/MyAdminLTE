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
            var option = context.Request["option"];
            if (!string.IsNullOrWhiteSpace(option) && Convert.ToInt32(option) > 0)
            {
                var gettype = context.Request["gettype"];
                switch (gettype)
                {
                    case "GetDataList":
                        GetDataList(context, Convert.ToInt32(option));
                        break;
                    case "BntDeleteItemID":
                        BntDeleteItemID(context, Convert.ToInt32(option));
                        break;
                    case "GetDataView":
                        GetDataView(context, Convert.ToInt32(option));
                        break;
                    case "SaveFromData":
                        var ChoiceValue = context.Request["ChoiceValue"];
                        if(string.IsNullOrWhiteSpace(ChoiceValue))
                            InsertData(context, Convert.ToInt32(option));
                       else
                            UpdateData(context, Convert.ToInt32(option));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                context.Response.Write("{\"code\":405,\"msg\":\"缺少参数：option\"}");
                context.Response.End();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        public void GetDataList(HttpContext context,int option)
        {
            BLL.B_Table TableBll = new BLL.B_Table(option);
            int PageIndex = Convert.ToInt32(context.Request["page"]);
            int PageSize = Convert.ToInt32(context.Request["limit"]);
            int PageStart = Convert.ToInt32(context.Request["start"]);
            string Order = context.Request["order"];
            var WhereValues = context.Request["where"];
            context.Response.Write(TableBll.GetDataListJson(PageStart, PageIndex, PageSize, WhereValues, Order));
            context.Response.End();
        }
        /// <summary>
        /// 获取单行数据
        /// </summary>
        public void GetDataView(HttpContext context, int option)
        {
            var ChoiceValue = context.Request["ChoiceValue"];
            BLL.B_Table TableBll = new BLL.B_Table(option);
            context.Response.Write(TableBll.GetDataView(ChoiceValue));
            context.Response.End();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="context"></param>
        /// <param name="option"></param>
        public void InsertData(HttpContext context, int option)
        {
            var FromValues = context.Server.UrlDecode(context.Request.QueryString["FromValues"]);
            BLL.B_Table TableBll = new BLL.B_Table(option);
            var NewID = TableBll.InsertTableData(FromValues);
            if (!string.IsNullOrWhiteSpace(NewID))
                context.Response.Write("{\"code\":1,\"msg\":\"保存成功!\",\"newitemid\":\"" + NewID + "\"}");
            else
                context.Response.Write("{\"code\":500,\"msg\":\"保存失败!\"}");
            context.Response.End();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="context"></param>
        /// <param name="option"></param>
        public void UpdateData(HttpContext context, int option)
        {
            var ChoiceValue = context.Request["ChoiceValue"];
            var FromValues = context.Server.UrlDecode(context.Request.QueryString["FromValues"]);
            BLL.B_Table TableBll = new BLL.B_Table(option);
            if (TableBll.UpdateTableData(FromValues, ChoiceValue))
                context.Response.Write("{\"code\":1,\"msg\":\"保存成功!\"}");
            else
                context.Response.Write("{\"code\":500,\"msg\":\"保存失败!\"}");
            context.Response.End();
        }
        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="context"></param>
        public void BntDeleteItemID(HttpContext context, int option)
        {
            var choicevalue = context.Request["choicevalue"];
            BLL.B_Table TableBll = new BLL.B_Table(option);
            if(TableBll.DeleteTableData(choicevalue))
                context.Response.Write("{\"code\":1,\"msg\":\"删除成功!\"}");
            else
                context.Response.Write("{\"code\":500,\"msg\":\"删除失败!\"}");
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