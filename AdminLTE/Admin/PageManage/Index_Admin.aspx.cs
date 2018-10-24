using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin.PageManage
{
    public partial class Index_Admin : BasePage
    {
        public string strJson = null;
        public string imgurl = "../../Script/AdminLTE-2.4.2/dist/img/user2-160x160.jpg";
        public string username = "admin";
        protected void Page_Load(object sender, EventArgs e)
        {
            var UserGUID = Session["UserGUID"];
            if (UserGUID != null && UserGUID.ToString() != "")
            {
               System.Data.DataTable dtUserInfo = BLL.AdminLogin.GetUserInfo(UserGUID.ToString());
                if (BLL.BaseClass.IsNullOrNotNull(dtUserInfo))
                {
                    imgurl = dtUserInfo.Rows[0]["UserImg"].ToString();
                    username = dtUserInfo.Rows[0]["UserName"].ToString();
                }
            }
        }
    }
}