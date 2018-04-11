using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MeanList
    {
        public static string GuID = "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18";
        public static string Title = "菜单管理2";
        public static string SQL = "SELECT top(10000) * FROM  [t_Mean]";
        public static string TableName = "[t_Mean]";
        public static string FileName = "MeanList";
        public static string Note = "";
        public static string Choice = "1";
        public static string Insert = "1";
        public static string Update = "1";
        public static string Delete = "1";
        public static string Strwhere = "1";
        public static string CountData = "'记录总条数：'+ convert(varchar(20),count(guid))+'（条）'";
        public static string Plus = "1";
        public static string ColumnsJson = string.Empty;
        public static string OneFileName= BaseClass.GetTopOneFileName(TableName);
        //实例化字段数据
        public static DataTable SetTableFieldInfo()
        {
            DataTable TableFieldInfo = new DataTable();
            DataColumn dc = new DataColumn("FieldKey", Type.GetType("System.String"));
            dc = new DataColumn("FieldKey", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("FieldValue", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("SelectType", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("SelectData", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("FieldStatusID", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("FieldData", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            dc = new DataColumn("FieldOrder", Type.GetType("System.String"));
            TableFieldInfo.Columns.Add(dc);
            return TableFieldInfo;
        }
        //添加字段数据
        public static System.Data.DataTable GetTableFieldInfo()
        {
            System.Data.DataTable TableFieldInfo = SetTableFieldInfo();
            DataRow dr = TableFieldInfo.NewRow();
            dr["FieldKey"] = "MeanClass"; dr["FieldValue"] = "菜单样式"; dr["SelectType"] = "2"; dr["SelectData"] = "1"; dr["FieldStatusID"] = "1"; dr["FieldData"] = ""; dr["FieldOrder"] = "4";
            TableFieldInfo.Rows.Add(dr);
            dr = TableFieldInfo.NewRow();
            dr["FieldKey"] = "GUID"; dr["FieldValue"] = "菜单编号"; dr["SelectType"] = "0"; dr["SelectData"] = "1"; dr["FieldStatusID"] = "1"; dr["FieldData"] = ""; dr["FieldOrder"] = "1";
            TableFieldInfo.Rows.Add(dr);
            dr = TableFieldInfo.NewRow();
            dr["FieldKey"] = "MeanOrder"; dr["FieldValue"] = "排序"; dr["SelectType"] = "0"; dr["SelectData"] = "1"; dr["FieldStatusID"] = "1"; dr["FieldData"] = ""; dr["FieldOrder"] = "3";
            TableFieldInfo.Rows.Add(dr);
            dr = TableFieldInfo.NewRow();
            dr["FieldKey"] = "MeanName"; dr["FieldValue"] = "菜单名称"; dr["SelectType"] = "1"; dr["SelectData"] = "1"; dr["FieldStatusID"] = "1"; dr["FieldData"] = ""; dr["FieldOrder"] = "2";
            TableFieldInfo.Rows.Add(dr);
            dr = TableFieldInfo.NewRow();
            TableFieldInfo.DefaultView.Sort = "FieldOrder ASC";
            return TableFieldInfo.DefaultView.ToTable();
        }
        //设置表格
        public static string GetTableHtml()
        {
            DataTable dt = GetTableFieldInfo();
            StringBuilder sb = new StringBuilder();
            StringBuilder sbjson = new StringBuilder();
            sb.Append("<thead><tr>");
            if (Choice == "1")
            {
                string html = "<input type=\"checkbox\" id=\"selectAll\" class=\"table-checkable\" >";
                sb.Append("<th style=\"width:13px;\">" + html + "</th>");
                sbjson.Append("{\"data\": \"ItemID\", render: function (data, type, row) { return \"<input  name='checkboxItemID' type='checkbox' class='table-checkable'  value='\" + data + \"'/>\"}},");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["FieldStatusID"].ToString() == "1")
                {
                    sb.Append("<th>" + dt.Rows[i]["FieldValue"].ToString() + "</th>");
                    string data = "";
                    if (dt.Rows[i]["FieldData"].ToString() != "")
                        data = ", render: function (data, type, row) { return  " + dt.Rows[i]["FieldData"].ToString() + " }";
                    sbjson.Append("{\"data\": \"" + dt.Rows[i]["FieldKey"].ToString() + "\"" + data + "},");//,\"sClass\": \"text-center\"
                }
            }
            if (Update == "1")
            {
                sb.Append("<th>操作</th>");
                sbjson.Append("{\"data\": \"UpdateItemID\", render: function (data, type, row) { return \"<button  type='button' class='btn btn-warning  btn-xs' style='width: 60px;height: 27px;'>修　改</button>\"}},");
            }
            sb.Append("</tr></thead>");
            ColumnsJson = "[" + sbjson.ToString().TrimEnd(',') + "]";
            return sb.ToString();
        }
        //设置高级查询
        public static string SetStrWhereHtml()
        {
            DataTable dt = GetTableFieldInfo();
            string strHtml = "";
            if (Strwhere == "1")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string type = dt.Rows[i]["SelectType"].ToString();
                        switch (type)
                        {
                            case "1":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（模糊查询）</span></label >";
                                strHtml += "<input type=\"text\" name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\"  class=\"form-control\" placeholder=\"" + dt.Rows[i]["FieldValue"].ToString() + "\" />";
                                strHtml += "</div>";
                                break;
                            case "2":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（下拉查询）</span></label >";
                                strHtml += "<select name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\" class=\"form-control select2 select2-hidden-accessible\"  tabindex=\"-1\" aria-hidden=\"true\" >";
                                strHtml += "<option selected = \"selected\" value = \"0\" >全部</option >";
                                string tsql = dt.Rows[i]["SelectData"].ToString();
                                DataTable tsqldt = BaseClass.getDataTable(tsql);
                                if (tsqldt != null && tsqldt.Rows.Count > 0)
                                {
                                    for (int j = 0; j < tsqldt.Rows.Count; j++)
                                    {
                                        strHtml += "<option value = \"" + tsqldt.Rows[j][1].ToString() + "\" >" + tsqldt.Rows[j][0].ToString() + "</option >";
                                    }
                                }
                                strHtml += "</select>";
                                strHtml += "</div>";
                                break;
                            case "3":
                                strHtml += "<div class=\"col-lg-2 col-xs-5 table-s\">";
                                strHtml += "<label class=\"col-xs control-label table-label\">" + dt.Rows[i]["FieldValue"].ToString() + "<span class=\"text-danger\">（等于查询）<span></label >";
                                strHtml += "<input type=\"text\" name=\"" + dt.Rows[i]["FieldKey"].ToString() + "|" + dt.Rows[i]["SelectType"].ToString() + "\" data-type=\"datepicker\"  class=\"form-control\" placeholder=\"" + dt.Rows[i]["FieldValue"].ToString() + "\" />";
                                strHtml += "</div>";
                                break;
                            default:
                                break;
                        }
                    }
                    strHtml += "<div class=\"col-sm-1 table-p\" style=\"margin-top:30px;\"><button type =\"button\" class=\"btn btn-danger pull-right btn-block btn-primary\" onclick=\"getJsonData('select')\">查询</button></div>";
                }
            }
            return strHtml;
        }
        //设置按钮
        public static string setBntHtml()
        {
            string bntHtml = "";
            if (Delete == "1")
                bntHtml += "<button id=\"deleteItemID\" tableValue=\"\" type=\"button\" class=\"btn btn-danger btn-xs\">删　除</button>&nbsp;";
            if (Insert == "1")
                bntHtml += "<button type=\"button\" class=\"btn btn-success btn-xs\">新　增</button>&nbsp;";
            return bntHtml;
        }
        //获取表格数据Json
        public static string GetDataJson(DataTable dt, int PageStart, int PageIndex, int PageSize, string order)
        {
            string strwhere = BaseClass.SetStrWhere(dt);
            string sumsqlStr = BaseClass.GetTSQL(SQL, "COUNT(GUID) as COUNTS," + CountData, strwhere, "", false);
            string sqlStr = BaseClass.PageBySQL(SQL, TableName, OneFileName, strwhere, order, PageIndex, PageSize);
            DataSet ds = BLL.BaseClass.getDataSet(sqlStr + sumsqlStr);
            DataTable tableJson = ds.Tables[0];
            DataTable tableSum = ds.Tables[1];
            if (ds != null)
            {
                if (tableJson != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"total\":" + tableSum.Rows[0]["COUNTS"].ToString() + ",\"page\":1,\"limit\":" + PageSize + ",\"data\":");
                    string datatablejson = JsonHelper.DataTableToJsonWithJsonNet(tableJson);
                    sb.Append(datatablejson);
                    sb.Append(",\"sumHtml\":\"");
                    for (int i = 1; i < tableSum.Columns.Count; i++)
                    {
                        sb.Append("<span class='label label-warning' style='font-size: small'>" + tableSum.Rows[0][i].ToString() + "</span>");
                    }
                    sb.Append("\"}");
                    //sb.Append(",\"sumHtml\":\"<span class='label label-danger'>交易总金额：20（元）</span>&nbsp;<span class='label label-warning'>总营业额：1245.15（元）</span>&nbsp;<span class='label label-info'>总笔数：" + tableJson.Rows.Count + "（笔）</span>\"}");
                    return sb.ToString().Replace("\n", "");
                }
                else
                {
                    return "{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[],\"sumData\":\"\"}";
                }
            }
            else
            {
                return "{\"total\":" + 0 + ",\"page\":0,\"limit\":" + PageSize + ",\"data\":[],\"sumData\":\"\"}";
            }
        }
    }
}
