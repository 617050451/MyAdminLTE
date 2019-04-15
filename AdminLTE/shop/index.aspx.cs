using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Shop
{
    public partial class index : System.Web.UI.Page
    {
        public string WxConfigJson{ get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var gettype = Request["gettype"];
                if (!string.IsNullOrWhiteSpace(gettype) && gettype == "SaveProdect")
                {
                    var code = Request["code"];
                    var name = Request["name"];
                    var price = Request["price"];
                    string sql = string.Format("if(select COUNT(id) from [t_shop] WHERE [Code] ='{0}')=0 INSERT INTO [t_shop]([Code] , [Name] , [Price] )VALUES('{1}','{2}','{3}')", code, code, name, price);
                    if (BLL.BaseClass.ExecuteNonQuerySQL(sql))
                        Response.Write(1);
                    else
                        Response.Write(0);
                    Response.End();
                }
                else
                {
                    WxConfigJson = BLL.WeChatHelper.GetConfig();
                }
            }
        }
    }
}