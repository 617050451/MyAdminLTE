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
        public string strJson = null;
        public string imgurl = "../../Script/AdminLTE-2.4.2/dist/img/user2-160x160.jpg";
        public string username = "admin";
        protected void Page_Load(object sender, EventArgs e)
        {
            var UserGUID = Session["UserGUID"];
            if (UserGUID != null && UserGUID.ToString() != "")
            {
                DataTable dtUserInfo = BLL.AdminLogin.GetUserInfo(UserGUID.ToString());
                if (BLL.BaseClass.estimate(dtUserInfo))
                {
                    imgurl = dtUserInfo.Rows[0]["UserImg"].ToString();
                    username = dtUserInfo.Rows[0]["UserName"].ToString();
                    BLL.AdminLTEHelper adminlte = new BLL.AdminLTEHelper();
                    adminlte.GetMeanJsonData("GUID", "MeanName", "ParentID", "0", "MeanLevel");
                    strJson = adminlte.result.ToString();
                }
            }
        }
    }
}