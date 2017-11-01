using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminLTE.Admin.Temp
{
    public partial class ListTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string guid = "9D2512E9-6FF4-4E7E-BBB8-23DE83755D18";
            DataTable dt = BLL.BaseClass.getTableInfo(guid);
            if (dt != null && dt.Rows.Count > 0)
            {
                //aspx
                string InstanceURL = dt.Rows[0]["FileName"].ToString();
                string Title = dt.Rows[0]["Title"].ToString();
                string text = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Admin/Temp/ListTemp.temp"));
                text = text.Replace("*name*", InstanceURL);
                text = text.Replace("*title*", Title);
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx"), text, Encoding.UTF8);
                //cs
                string textcs = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Admin/Temp/ListTemp.cs.temp"));
                try
                {
                    string result = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx.cs"));
                    string one = Regex.Match(result, "(?<=(//StartWriteOne))[.\\s\\S]*?(?=(//EndWriteOne))", RegexOptions.Multiline | RegexOptions.Singleline).Value;
                    string two = Regex.Match(result, "(?<=(//StartWriteTwo))[.\\s\\S]*?(?=(//EndWriteTwo))", RegexOptions.Multiline | RegexOptions.Singleline).Value;
                    textcs = textcs.Replace("//StartWriteOne", "//StartWriteOne" + one);
                    textcs = textcs.Replace("//StartWriteTwo", "//StartWriteTwo" + two);
                }
                catch (Exception)
                {
                }
                textcs = textcs.Replace("*class*", InstanceURL);
                textcs = textcs.Replace("*guid*", guid);
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx.cs"), textcs, Encoding.UTF8);
                //designer
                string textde = @"namespace AdminLTE.Admin.Aspx{
                public partial class " + InstanceURL +
                       "{" +
                           "protected global::System.Web.UI.WebControls.Literal ltlhead;" +
                           "protected global::System.Web.UI.WebControls.Literal ltlStrWhere;" +
                           "protected global::System.Web.UI.WebControls.Literal ltlbnt;}" +
                       "}";
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx.designer.cs"), textde, Encoding.UTF8);
            }
        }
    }
}