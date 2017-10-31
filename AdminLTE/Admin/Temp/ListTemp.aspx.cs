using System;
using System.Collections.Generic;
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
            //aspx
            string InstanceURL = "Mean";
            string text = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Admin/Temp/ListTemp.temp"));
            text = text.Replace("*name*", "Mean");
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
            textcs = textcs.Replace("*listColumn*", "GUID|菜单编号|1,ParentID|父级菜单|1,MeanHeader|菜单Header|1,MeanName|菜单名称|1");
            textcs = textcs.Replace("*SQLWhere*", "MeanName↑菜单名称↑1↓CreateTime↑CreateTime↑3↓MeanClass↑MeanClass↑2↑sql◇SELECT DISTINCT([MeanClass]) AS 'key',([MeanClass]) as 'value'  FROM [qds108295464_db].[dbo].[t_Mean]");
            textcs = textcs.Replace("*sqlStr*", "SELECT * FROM [qds108295464_db].[dbo].[t_Mean] where 1=1  ");
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx.cs"), textcs, Encoding.UTF8);
            //designer
            string textde= "namespace AdminLTE.Admin.Aspx{public partial class " + "Mean" + "{}}";
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/Admin/Aspx/" + InstanceURL + ".aspx.designer.cs"), textde, Encoding.UTF8);
        }
    }
}