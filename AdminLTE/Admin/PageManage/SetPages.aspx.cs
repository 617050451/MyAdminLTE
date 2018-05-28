using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace AdminLTE.Admin.Temp
{
    public partial class SetPages : BasePage
    {
        public string OptionList = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        void BindData()
        {
            var data = BLL.BaseClass.GetAllTableModelList();
            StringBuilder sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append("<option value=\"" + item.GUID + "\" page-title=\"" + item.Title + "\" page-name=\"" + item.FileName + "\">页面标题：[ " + item.Title + " ]　页面名称：[ " + item.FileName + " ]" + (item.Note == "" ? "" : "　说明：[ " + item.Note + " ]") + "</option>");
            }
            OptionList = sb.ToString();
        }
    }
}