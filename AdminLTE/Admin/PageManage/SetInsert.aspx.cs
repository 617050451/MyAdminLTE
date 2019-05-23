using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace AdminLTE.Admin.PageManage
{
    public partial class SetInsert : System.Web.UI.Page
    {
        public int ItemID { get; set; }
        public Model.M_Table TableModel { get; set; }
        public BLL.B_Table TableBll = null;
        public List<Model.M_TableField> TableFielModelList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request["ItemID"]))
            {
                //输入错误
                
            }
            ItemID = Convert.ToInt32(Request["ItemID"]);
            TableBll = new BLL.B_Table(ItemID);
            TableModel = TableBll.GetTableModel();
            TableFielModelList = TableBll.GetTableFieldModel();
            if (!IsPostBack)
            {
                BindData();
            }
        }
        void BindData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in TableFielModelList)
            {
                var FieldKey = item.FieldKey;
                var FieldText = item.FieldText;
                sb.Append("<li><span class=\"handle ui-sortable-handle\"><i class=\"fa fa-ellipsis-v\"></i><i class=\"fa fa-ellipsis-v\"></i><i class=\"fa fa-ellipsis-v\"></i></span>");
                sb.Append("<div class=\"checkbox\" style=\"display: inline-block; min-width: 120px;\">");
                sb.Append("<label><input type = \"checkbox\" name=\"FieldKey\" value=\""+ FieldKey + "\" checked=\"checked\" />"+ FieldKey + "</label></div>");
                sb.Append("<input type = \"text\" name=\"FieldText\" value=\""+ FieldText + "\" style=\"width: 100px; \"  /> ");
                sb.Append("<select style = \"height: 27px; \" >");
                sb.Append("<option value = \"1\" selected=\"selected\">文本框</option>");
                sb.Append("<option value = \"2\" >文本框（多行）</option >");
                sb.Append("<option value = \"3\" >下拉框</ option >");
                sb.Append("<option value = \"4\" >单选（radio）</option >");
                sb.Append("<option value = \"5\" >多选（checkbox）</option >");
                sb.Append("<option value = \"-1\" >不显示（hidden）</option >");
                sb.Append("<option value = \"-2\" >不显示（label）</option >");
                sb.Append("</select> ");
                sb.Append("<select style = \"height: 27px; \" onchange=\"SetOneCulomnNum(this)\" >");
                sb.Append("<option value = \"0\" selected=\"selected\">不合并</option>");
                sb.Append("<option value = \"1\" >合并1列</ option >");
                sb.Append("<option value = \"2\" >合并2列</ option >");
                sb.Append("<option value = \"3\" >合并3列</ option ></select>");
                sb.Append(" <a href=\"javascript:void(0) \">数据验证</a>");
                sb.Append("</li>");
            }
            ltl_file.Text = sb.ToString();
        }
    }
}