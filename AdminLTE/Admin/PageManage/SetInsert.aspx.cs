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
                
            }
        }
    }
}