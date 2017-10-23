using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = BLL.BaseClass.getDataTable("SELECT * FROM [qds108295464_db].[dbo].[t_Mean]");
            BLL.AdminLTEHelper adminlte = new BLL.AdminLTEHelper();
            adminlte.GetTreeJsonByTable(dt, "GUID", "MeanName", "ParentID", "0", "MeanLevel");
            string strJson = adminlte.result.ToString();
        }
    }
}