using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin.Home
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string GetType = Request["gettype"];
                if (GetType != null && GetType == "login")
                {
                    string userid = Request["userid"];
                    string password = Request["password"];
                    string UserGUID = BLL.AdminLogin.Login(userid, password);
                    if (UserGUID != "")
                    {
                        Session["UserGUID"] = UserGUID;
                        Session.Timeout = 30;
                        Response.Write("True");
                    }
                    else {
                        Response.Write("Flase");
                    }                 
                    Response.End();
                }
            }

        }
    }
}