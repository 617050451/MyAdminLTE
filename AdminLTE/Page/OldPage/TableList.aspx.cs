using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdminLTE.Page
{
    public partial class TableList : BasePage
    {
       public  BLL.t_TablesClass tableModel = new BLL.t_TablesClass("9D2512E9-6FF4-4E7E-BBB8-23DE83755D17");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request.QueryString["gettype"];
                if (GetType != null)
                {
                    LoadBind(GetType);
                }
            }
        }
        void LoadBind(string GetType)
        {
            int PageIndex = Convert.ToInt32(Request.QueryString["page"]);
            int PageSize = Convert.ToInt32(Request.QueryString["limit"]);
            int PageStart = Convert.ToInt32(Request.QueryString["start"]);
            string Order = Request.QueryString["order"];
            string OSrderDir = Request.QueryString["orderDir"];
            var WhereValues = Request.QueryString["WhereValues"];
            if (GetType == "GetDataList")
            {
                System.Data.DataTable dt = (WhereValues == null ? null : BLL.JsonHelper.DeserializeJsonToObject<System.Data.DataTable>(WhereValues));//条件数据
                Response.Write(tableModel.GetDataListJson(dt, PageStart, PageIndex, PageSize, " " + Order + " " + OSrderDir));
                Response.End();
            }
            else if (GetType == "SaveFromData")
            {
                var ChoiceValue = Request.QueryString["ChoiceValue"];
                var FromValues = Server.UrlDecode(Request.QueryString["FromValues"]);
                BLL.ObjectData ModelData = new BLL.ObjectData(tableModel.TableModel.TableName);
                ModelData.SetValues(FromValues);//表单数据
                var RowNum = true;
                var CodeJson = "";
                if (ChoiceValue != null && ChoiceValue != "")//修改
                {
                    RowNum = tableModel.UpdateModel(ModelData, ChoiceValue);
                    if (RowNum)
                        CodeJson = "[{\"code\":100}]";
                    else
                        CodeJson = "[{\"code\":105}]";
                    //修改后执行
                }
                else//添加
                {
                    ChoiceValue = tableModel.InsertModel(ModelData, Guid.NewGuid().ToString());
                    if (!string.IsNullOrWhiteSpace(ChoiceValue))
                        CodeJson = "[{\"code\":100}]";
                    else
                        CodeJson = "[{\"code\":105}]";
                    //添加后执行
                }
                BLL.t_TablesClass ItemModel = new BLL.t_TablesClass(ChoiceValue);
                SaveHtml(ItemModel.TableModel.FileName, ItemModel.TableModel.GUID.ToString());
                ItemModel.SetTableFieldInfo();
                Response.Write(CodeJson);
                Response.End();
            }
            else if (GetType == "GetDataView")
            {
                var ChoiceValue = Request.QueryString["ChoiceValue"];
                Response.Write(tableModel.GetDataViewJson(ChoiceValue));
                Response.End();
            }
            else if (GetType == "BntOperation")
            {
                var ChoiceValue = Request["ChoiceValue"];
                if (!string.IsNullOrWhiteSpace(ChoiceValue))
                    Response.Write(tableModel.DeleteData(ChoiceValue));
                else
                    Response.Write("False");
                Response.End();
            }
            else if (GetType == "GetFieldKeyValue")
            {
                Dictionary<string, string> data = new Dictionary<string, string> { };
                for (int i = 0; i < Request.QueryString.Count; i++)
                {
                    data.Add(Request.QueryString.Keys[i].ToString(), Request.QueryString[i].ToString());
                }
                if (data.Count > 0)
                    Response.Write(tableModel.GetFieldKeyValue(data));
                else
                    Response.Write("False");
                Response.End();
            }
        }
        //生成页面
        void SaveHtml(string PageName, string TableGUID)
        {
            System.IO.StreamReader h_hovertreeSr = new System.IO.StreamReader(System.Web.HttpContext.Current.Request.MapPath("\\Admin\\PageManage\\Temp\\TempPageList.aspx.temp"));
            string h_hovertreeTemplate = h_hovertreeSr.ReadToEnd();
            //当前网站根目录物理路径  
            System.IO.DirectoryInfo h_dir = new System.IO.DirectoryInfo(System.Web.HttpContext.Current.Request.PhysicalApplicationPath);
            //HoverTreeWeb项目根目录下主页文件  aspx
            string h_path = string.Format(h_dir.Parent.FullName + "\\AdminLTE\\Page\\{0}.aspx", PageName);
            if (!File.Exists(h_path))
            {
                System.IO.FileStream fs = new System.IO.FileStream(h_path, System.IO.FileMode.Create, System.IO.FileAccess.Write);//创建写入文件             
                System.IO.StreamWriter h_sw = new System.IO.StreamWriter(fs, Encoding.UTF8);
                h_sw.Write(h_hovertreeTemplate.Replace("{TableGUID}", TableGUID).Replace("{PageName}", PageName));
                h_sw.Close();
                fs.Close();
            }
            h_hovertreeSr = new System.IO.StreamReader(System.Web.HttpContext.Current.Request.MapPath("\\Admin\\PageManage\\Temp\\TempPageList.aspx.cs.temp"));
            h_hovertreeTemplate = h_hovertreeSr.ReadToEnd();
            //HoverTreeWeb项目根目录下主页文件  aspx.cs
            h_path = string.Format(h_dir.Parent.FullName + "\\AdminLTE\\Page\\{0}.aspx.cs", PageName);
            if (!File.Exists(h_path))
            {
                System.IO.FileStream fs = new System.IO.FileStream(h_path, System.IO.FileMode.Create, System.IO.FileAccess.Write);//创建写入文件             
                System.IO.StreamWriter h_sw = new System.IO.StreamWriter(fs, Encoding.UTF8);
                h_sw.Write(h_hovertreeTemplate.Replace("{TableGUID}", TableGUID).Replace("{PageName}", PageName));
                h_sw.Close();
                fs.Close();
            }
            //end 
            h_hovertreeSr.Close();
        }
    }
}