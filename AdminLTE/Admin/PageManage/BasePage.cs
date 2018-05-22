using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE.Admin
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (UserID == 0)
            //{
            //    Response.Redirect("https:www.baidu.com");
            //}
        }
        private int UserIDValue;
        public int UserID
        {
            get
            {
                return UserIDValue;
            }
            set
            {
                if (HttpContext.Current.Request.Cookies["UserID"] != null)
                {
                    UserIDValue = int.Parse(HttpContext.Current.Request.Cookies["UserID"].Value);
                }
                else if (HttpContext.Current.Session["UserID"] != null)
                {
                    UserIDValue = int.Parse(HttpContext.Current.Session["UserID"].ToString());
                }
            }
        }
    }
}