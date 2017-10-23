using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApiHelper;

public partial class Cases_baseprint_HomePage_DataList : System.Web.UI.Page
{
    public DataTable DtList;
    public DataTable NewDt;
    private int limit;
    public int Limit
    {
        get 
        { 
            if(limit==0)
                limit=(int)Session["Limit"] ;
            return limit;
        }
        set { Session["Limit"] = limit = value; }
    }
    public  int Start ;
    public  int Page ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string dataJson = Request.QueryString["dataJson"];
            string type = Request.QueryString["type"];
            string limit1 = Request.QueryString["limit"];
            string start = Request.QueryString["start"];
            string page1 = Request.QueryString["page"];
            string order = Request.QueryString["order"];
            string orderDir = Request.QueryString["orderDir"];
            string empid = Request.QueryString["empid"];
            string departmentId = Request.QueryString["departmentId"];
            string quantifyRecordId = Request.QueryString["QuantifyRecordId"];
            string getType = Request.QueryString["getType"];
            string state = Request.QueryString["state"];
            if (limit1 != null && limit1 != "")
            {
                if (state == "0")
                {
                    if (dataJson != null && dataJson != "")
                    {
                        PrizeModel prizeModel = JsonConvert.DeserializeObject<PrizeModel>(dataJson);
                        getPrintPrize(prizeModel, type);
                    }
                }
                Limit = Convert.ToInt32(limit1);
                Start = Convert.ToInt32(start);
                Page = Convert.ToInt32(page1);
                NewDt = (DataTable)Session["NewDt"];
                getPrintPrizeJson(NewDt, Limit, Page, order, orderDir);
            }
            else if (empid != null && departmentId != null && quantifyRecordId != null)
            {
                DtList = (DataTable)Session["DtList"];
                if (DtList != null)
                {
                    NewDt = DtList.Clone();
                    if (quantifyRecordId == "")
                    {
                        for (int i = 0; i < DtList.Rows.Count; i++)
                        {
                            var q = false;
                            if (departmentId == "")
                                q = true;
                            else
                            {
                                var tempid = DtList.Rows[i]["departmentId"].ToString().Split(',');
                                for (int j = 0; j < tempid.Length; j++)
                                {
                                    q = departmentId.Contains(tempid[j]);
                                    if (q)
                                        break;
                                }
                            }
                            var w = (empid == "" ? true : empid.Contains(DtList.Rows[i]["empid"].ToString()));
                            if (q && w)
                                NewDt.ImportRow(DtList.Rows[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DtList.Rows.Count; i++)
                        {
                            var t = quantifyRecordId.Contains(DtList.Rows[i]["quantifyRecordId"].ToString());
                            if (t)
                                NewDt.ImportRow(DtList.Rows[i]);
                        }
                    }
                    Session["NewDt"] = NewDt;
                    getPrintPrizeJson(NewDt, Limit, Page, order, orderDir);

                }
                else
                {
                    Response.Write("false");
                    Response.End();
                }
            }
            if (getType != null)
            {
                NewDt = (DataTable)Session["NewDt"];
                if (getType == "getJson")
                    getdatatable(NewDt);
                else if (getType == "getJsonPrint")
                    getPrintPrizePcJsonData(NewDt);
            }
        }
    }

    //获取最新奖票数量
    void getPrintPrize(PrizeModel prizeModel, string type)
    {
        PrizeApi print = new PrizeApi();
        T_LoginInfo userInfo = Session["UserInfo"] as T_LoginInfo;
        if (userInfo != null)
        {
            ApiData data = getApiData(prizeModel);
            List<T_PrizeInfo> prize = print.GetPrizes1(data);
            //获取奖票数据
            //DateTime LastTime = Convert.ToDateTime("2017-06-01 10:10:12");
            //DateTime ThisTime = Convert.ToDateTime("2017-08-01 10:10:12");
            //string lastTime = ConvertTimestamp(LastTime);
            //string thistime = ConvertTimestamp(ThisTime);
            //List<T_PrizeInfo> prize = print.GetPrizes(userInfo.EmpId, lastTime, thistime, userInfo.CompanyId, false);
            DtList = FillDataTable<T_PrizeInfo>(prize);
            NewDt = DtList;
            Session["DtList"] = DtList;
            Session["NewDt"] = NewDt;
            //if (type == "0")
            //    getdatatable1(DtList);
            //else if (type == "1")
            //    getPrintPrizePcJsonData(DtList);
        }
        else 
        {
            Response.Write("false");
            Response.End();
        }
    }

    private string ConvertTimestamp(DateTime time)
    {
        double intResult = 0;
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        intResult = (time - startTime).TotalMilliseconds;
        return Math.Round(intResult, 0).ToString();
    }

    //获取已打印批次数据
    void getPrintPrizePcJsonData(DataTable dt)
    {
        if (dt != null)
        {
            PrizeApi print = new PrizeApi();
            T_LoginInfo data = new T_LoginInfo();
            data = Session["UserInfo"] as T_LoginInfo;
            if (data != null)
            {
                string sql = string.Format(@"select p.EmpName,p.CreateDate, p1.BATCHID,p1.PrizeID   from PrizePrint.dbo.T_PrintBatch P
                left JOIN PrizePrint.dbo.T_PrintBatchDetail P1 ON P.ID=P1.BATCHID  where  p.CompanyId='{0}' ", data.CompanyId);
                DataSet ds = FLYSO.ToolKit.TSQL.GetDataSet(sql);
                DataTable dts = ds.Tables[0];
                var query1 =
                from rHead in dt.AsEnumerable()
                from rTail in dts.AsEnumerable()
                where rHead.Field<String>("QuantifyRecordId") == rTail.Field<String>("PrizeID")
                select new
                {
                    BatchID = rTail.Field<Int32>("BatchID"),
                    EmpName = rTail.Field<String>("EmpName"),
                    CreateDate = rTail.Field<DateTime>("CreateDate"),
                    PrizeID = rTail.Field<String>("PrizeID"),
                    Department = rHead.Field<String>("Department"),
                    DepartmentId = rHead.Field<String>("DepartmentId")
                };
                DataTable dtNew = new DataTable();
                dtNew.Columns.Add("BatchID", typeof(int));
                dtNew.Columns.Add("EmpName", typeof(string));
                dtNew.Columns.Add("CreateDate", typeof(DateTime));
                dtNew.Columns.Add("PrizeID", typeof(string));
                dtNew.Columns.Add("Department", typeof(string));
                dtNew.Columns.Add("DepartmentId", typeof(string));
                dtNew.Columns.Add("cnt", typeof(int));
                foreach (var obj in query1)
                {
                    dtNew.Rows.Add(obj.BatchID, obj.EmpName, obj.CreateDate, obj.PrizeID, obj.Department, obj.DepartmentId, 1);
                };
                string Json = getData(dtNew);
                Response.Write(Json);
                Response.End();
            }
            else {
                Response.Write("false");
                Response.End();
            }
        }
        else 
        {
            Response.Write("");
            Response.End();
        }
    }
    //
    string getData(DataTable dt)
    {

        if (dt != null && dt.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            //Linq分组查询，并按分组显示人员明细
            var query = from g in dt.AsEnumerable()
                        group g by new { t1 = g.Field<int>("BATCHID"), t2 = g.Field<string>("EmpName"), t3 = g.Field<DateTime>("CreateDate") } into companys
                        select new { BATCHID = companys.Key.t1, EmpName = companys.Key.t2, CreateDate = companys.Key.t3, cnt = companys.Sum(d => Convert.ToInt32(d["cnt"]))};
            int i = 0;
            foreach (var userInfo in query)
            {
                StringBuilder sb1 = new StringBuilder();
                if (i > 0)
                    sb1.Append(",");
                sb1.Append("{");
                sb1.Append("'id': '" + userInfo.BATCHID + "',");
                sb1.Append("'text': '" + userInfo.EmpName + "   " + userInfo.CreateDate.ToString("yyyy-MM-dd HH:mm") + "（" + userInfo.cnt + " 张）'");
                string data = selectRows(dt, userInfo.BATCHID.ToString());
                sb1.Append(data);
                sb1.Append("}");
                if (data != "")
                {
                    sb.Append(sb1.ToString());
                }
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }
        else
        {
            return "";
        }
    }
    //
    string selectRows(DataTable dts, string ids)
    {
        try
        {
            var query = from g in dts.AsEnumerable()
                        where g.Field<int>("BatchID") == int.Parse(ids) //条件
                        group g by new { t1 = g.Field<string>("DepartmentId"), t2 = g.Field<string>("Department") } into companys
                        select new { DepartmentId = companys.Key.t1, Department = companys.Key.t2, cnt = companys.Sum(d => Convert.ToInt32(d["cnt"])), PrizeIDs = string.Join(",", companys.Select(s => s["PrizeID"])) };           
            StringBuilder sb = new StringBuilder();
            sb.Append(",'children': [");
            foreach (var userInfo in query)
            {
                sb.Append("{");
                sb.Append("'id': '" + userInfo.PrizeIDs + "',");
                sb.Append("'text': '" + userInfo.Department + "（" + userInfo.cnt + " 张）'");
                sb.Append("},");
            }
            sb.ToString().Trim(',');
            sb.Append("]");
            return sb.ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }

    //分组查询奖扣员工的奖票
    void getdatatable(DataTable dt)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<div class='form-group' style='margin:2px;'><input type='text' id='userInfoText' onkeyup='funUserChange(this)' class='form-control'  placeholder='请输入员工或工号' /></div>");
        sb.Append("<div style='overflow-y:auto;height:100%;width:107%'>");
        sb.Append("<table id='selEmpid' style='width:100%;margin-left:2px;min-width:200px;word-wrap:break-word;word-break:break-all;'>  ");
        if (dt != null)
        {            
            var query = from t in dt.AsEnumerable()
                        group t by new { t1 = t.Field<string>("empid") } into m
                        select new
                        {
                            empid = m.Key.t1,
                            EmpName = m.First().Field<string>("EmpName"),
                            EmpNum = m.First().Field<string>("EmpNum"),
                            rowcount = m.Count()
                        };
            if (query.ToList().Count > 0)
            {
                foreach (var item in query.ToList())
                {
                    sb.Append(" <tr><td><input name='userInfoCK' style='width:18px;' type='checkbox' data-i='" + item.empid + "' onclick='funCheck(this)' /></td><td name='userName'  select-i='" + item.EmpName + item.EmpNum + "'>" + item.EmpName + "(" + item.EmpNum + ")<font style=\"color: blue;\"> [" + item.rowcount + "张]</font></td>");//dt.Rows[i]["cnt"].ToString()
                }
            }
            else
            {
                sb.Append("<tr><td colspan='3'><div>没有记录!<div></td></tr>");
            }
            sb.Append("</table><div style='height:100px;'></div></div>");
            Response.Write(sb.ToString());
            Response.End();
        }
        else 
        {
            sb.Append("<tr><td colspan='3'><div>没有记录!<div></td></tr>");
            sb.Append("</table><div style='height:100px;'></div></div>");
            Response.Write(sb.ToString());
            Response.End();
        }
    }

    //获取奖票数据（json）
    void getPrintPrizeJson(DataTable dt, int limit, int page, string order, string orderDir) 
    {
        if (dt != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + dt.Rows.Count + ",\"page\":10,\"limit\":" + limit + ",\"data\":[");
            if (page == 0)
                page = 1;
            DataTable newdt = GetPagedTable(dt, page, limit, order, orderDir);
            for (int i = 0; i < newdt.Rows.Count; i++)
            {
                if (i > 0)
                    sb.Append(",");
                sb.Append("{\"QuantifyRecordId\":\"" + newdt.Rows[i]["QuantifyRecordId"].ToString() + "\"");
                sb.Append(",\"Department\":\"" + newdt.Rows[i]["Department"].ToString() + "\"");
                sb.Append(",\"EmpName\":\"" + newdt.Rows[i]["EmpName"].ToString() + "\"");
                sb.Append(",\"EmpNum\":\"" + newdt.Rows[i]["EmpNum"].ToString() + "\"");
                sb.Append(",\"QuantifyDate\":\"" + Convert.ToDateTime(newdt.Rows[i]["QuantifyDate"].ToString()).ToString("yyyy-MM-dd") + "\"");
                sb.Append(",\"QuantifyName\":\"" + newdt.Rows[i]["QuantifyName"].ToString() + "\"");
                sb.Append(",\"EventName\":\"" + newdt.Rows[i]["EventName"].ToString() + "\"");
                sb.Append(",\"BScore\":\"" + newdt.Rows[i]["BScore"].ToString() + "\"");
                sb.Append(",\"AttnBy\":\"" + newdt.Rows[i]["AttnBy"].ToString() + "\"");
                sb.Append(",\"AuditBy\":\"" + newdt.Rows[i]["AuditBy"].ToString() + "\"");
                sb.Append(",\"CreateBy\":\"" + newdt.Rows[i]["CreateBy"].ToString() + "\"");
                sb.Append(",\"IsPrint\":\"" + newdt.Rows[i]["IsPrint"].ToString() + "\"");
                sb.Append(",\"EventDetail\":\"" + newdt.Rows[i]["EventDetail"].ToString() + "\"}");
            }
            sb.Append("]}");
            Response.Write(sb.ToString().Replace("\n", ""));
            Response.End();
        }
        else 
        {
            Response.Write("{\"total\":" + 0 + ",\"page\":10,\"limit\":" + limit + ",\"data\":[]}");
            Response.End();
        }
    }

    ApiData getApiData(PrizeModel prizeModel)
    {
        ApiData apidata = new ApiData();
        if (prizeModel.CompanyId != "")
            apidata.SetValue("companyId", prizeModel.CompanyId);
        if (prizeModel.Print != "")
            apidata.SetValue("print", prizeModel.Print);
        if (prizeModel.Del != "")
            apidata.SetValue("del", prizeModel.Del);
        if (prizeModel.Prize != "")
            apidata.SetValue("prize", prizeModel.Prize);
        if (prizeModel.Recycle != "")
            apidata.SetValue("recycle", prizeModel.Recycle);
        if (prizeModel.QuantifyName != "")
            apidata.SetValue("quantifyName", prizeModel.QuantifyName);
        if (prizeModel.FullName != "")
            apidata.SetValue("fullName", prizeModel.FullName);
        if (prizeModel.EventName != "")
            apidata.SetValue("eventName", prizeModel.EventName);
        if (prizeModel.AttnBy != "")
            apidata.SetValue("attnBy", prizeModel.AttnBy);
        if (prizeModel.AuditBy != "")
            apidata.SetValue("auditBy", prizeModel.AuditBy);
        if (prizeModel.CreateBy != "")
            apidata.SetValue("vreateBy", prizeModel.CreateBy);
        if (prizeModel.QuantifyDateStart != "")
            apidata.SetValue("quantifyDateStart", prizeModel.QuantifyDateStart);
        if (prizeModel.QuantifyDateEnd != "")
            apidata.SetValue("quantifyDateEnd", prizeModel.QuantifyDateEnd);
        if (prizeModel.BscoreMin != "")
            apidata.SetValue("bscoreMin", prizeModel.BscoreMin);
        if (prizeModel.BscoreMax != "")
            apidata.SetValue("bscoreMax", prizeModel.BscoreMax);
        return apidata;
    }
    /// <summary>
    /// 实体类转换成DataTable
    /// 调用示例：DataTable dt= FillDataTable(Entitylist.ToList());
    /// </summary>
    /// <param name="modelList">实体类列表</param>
    /// <returns></returns>
    public DataTable FillDataTable<T>(List<T> modelList)
    {
        if (modelList == null || modelList.Count == 0)
        {
            return null;
        }
        DataTable dt = CreateData(modelList[0]);//创建表结构

        foreach (T model in modelList)
        {
            DataRow dataRow = dt.NewRow();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
            }
            dt.Rows.Add(dataRow);
        }
        return dt;
    }
    /// <summary>
    /// 根据实体类得到表结构
    /// </summary>
    /// <param name="model">实体类</param>
    /// <returns></returns>
    private DataTable CreateData<T>(T model)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);
        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
        {
            if (propertyInfo.Name != "CTimestamp")//些字段为oracle中的Timesstarmp类型
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            else
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, typeof(DateTime)));
            }
        }
        return dataTable;
    }
    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DtList != null)
        {
            string sql = " QuantifyRecordId  in (" + Prizes.Value + ")";
            DataTable tblDatas = new DataTable();
            tblDatas.Columns.Add("部门名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("奖扣对象", Type.GetType("System.String"));
            tblDatas.Columns.Add("工号", Type.GetType("System.String"));
            tblDatas.Columns.Add("奖扣日期", Type.GetType("System.String"));
            tblDatas.Columns.Add("主题", Type.GetType("System.String"));
            tblDatas.Columns.Add("事件", Type.GetType("System.String"));
            tblDatas.Columns.Add("B分", Type.GetType("System.String"));
            tblDatas.Columns.Add("记录人", Type.GetType("System.String"));
            tblDatas.Columns.Add("初审人", Type.GetType("System.String"));
            tblDatas.Columns.Add("终审人", Type.GetType("System.String"));
            tblDatas.Columns.Add("事件描述", Type.GetType("System.String"));
            DataRow[] drArr = DtList.Select(sql);
            for (int i = 0; i < drArr.Length; i++)
            {
                DataRow dr = tblDatas.NewRow();
                dr[0] = drArr[i]["Department"].ToString();
                dr[1] = drArr[i]["EmpName"].ToString();
                dr[2] = drArr[i]["EmpNum"].ToString();
                dr[3] = Convert.ToDateTime(drArr[i]["QuantifyDate"].ToString()).ToString("yyyy-MM-dd");
                dr[4] = drArr[i]["QuantifyName"].ToString();
                dr[5] = drArr[i]["EventName"].ToString();
                dr[6] = drArr[i]["BScore"].ToString();
                dr[7] = drArr[i]["CreateBy"].ToString();
                dr[8] = drArr[i]["AttnBy"].ToString();
                dr[9] = drArr[i]["AuditBy"].ToString();
                dr[10] = drArr[i]["EventDetail"].ToString();
                tblDatas.Rows.Add(dr);
            }
            string tm = DateTime.Now.ToString("yyyyMMddHHmm");
            ExportToExcel(this, tblDatas, tm + ".xls");
        }
    }

    public  void ExportToExcel(System.Web.UI.Page page, System.Data.DataTable tab, string FileName)
    {
        System.Web.HttpResponse httpResponse = page.Response;
        System.Web.UI.WebControls.DataGrid dataGrid = new System.Web.UI.WebControls.DataGrid();
        dataGrid.DataSource = tab.DefaultView;
        dataGrid.AllowPaging = false;
        dataGrid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
        dataGrid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        dataGrid.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
        dataGrid.HeaderStyle.Font.Bold = true;
        dataGrid.DataBind();
        httpResponse.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));          
        httpResponse.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        httpResponse.ContentType = "application/ms-excel";
        httpResponse.Charset = "UTF-8";
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        dataGrid.RenderControl(hw);
        string filePath = page.Server.MapPath(".") + "//Files//" + FileName;
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        httpResponse.Write(tw.ToString());
        httpResponse.End();
    }

    public class PrizeModel
    {
        public string CompanyId { get; set; }
        public string QuantifyName { get; set; }
        public string FullName { get; set; }
        public string AttnBy { get; set; }
        public string QuantifyDateStart { get; set; }
        public string QuantifyDateEnd { get; set; }
        public string EventName { get; set; }
        public string CreateBy { get; set; }
        public string AuditBy { get; set; }
        public string BscoreMin { get; set; }
        public string BscoreMax { get; set; }
        public string Prize { get; set; }
        public string Print { get; set; }
        public string Del { get; set; }
        public string Recycle { get; set; }
    }

    public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize,string order,string orderDir)//PageIndex表示第几页，PageSize表示每页的记录数
    {
        if (order != null && orderDir != null)
        {
            switch (order)
            {
                case "4": order = "QuantifyDate";
                    break;
                case "7": order = "BScore";
                    break;
                default:
                    order = "QuantifyDate";
                    break;
            }
            DataView dataView = dt.DefaultView;
            dataView.Sort = order + " " + orderDir;
            dt = dataView.ToTable();
        }
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