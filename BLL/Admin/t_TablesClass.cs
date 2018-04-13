using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class t_TablesClass
    {
        public static string SQL = "select [GUID],[Title],[FileName] from [t_Tables] ";
        public static string TableName = "[t_Tables] ";
        public static string OneFileName = "GUID";
        //获取表格数据Json
        public static string GetDataListJson(DataTable dt, int PageStart, int PageIndex, int PageSize, string order)
        {
            string strwhere = BaseClass.SetStrWhere(dt);
            string sumsqlStr = BaseClass.GetTSQL(SQL, "COUNT("+ OneFileName + ") as COUNTS", strwhere, "", false);
            string sqlStr = BaseClass.PageBySQL(SQL, TableName, OneFileName, strwhere, order, PageIndex, PageSize);
            DataSet ds = BLL.BaseClass.GetDataSet(sqlStr+ sumsqlStr);
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
        public static string GetDataViewJson(string ChoiceKey, string ChoiceValue)
        {
            string sql = string.Format("select * from {0} where {1}='{2}'", TableName, ChoiceKey, ChoiceValue);
            DataTable tableJson = DAL.SQLDBHelpercs.ExecuteReaderTable(sql, null);
            if (tableJson != null && tableJson.Rows.Count > 0)
                return JsonHelper.DataTableToJsonWithJsonNet(tableJson);
            else
                return "";
        }
    }
}
