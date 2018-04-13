using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLL
{
    public class AdminLogin
    {
        public static string Login(string userid, string userpwd)
        {
            string sql = string.Format("SELECT * FROM [t_Users] where userid='{0}' and UserPwd='{1}'", userid, userpwd);
            DataTable dt = BaseClass.GetDataTable(sql);
            if (BaseClass.estimate(dt))
            {
                return dt.Rows[0]["GUID"].ToString();
            }
            else
            {
                return "";
            }
        }
        public static DataTable GetUserInfo(string userguid)
        {
            string sql = string.Format("SELECT * FROM [t_Users] where guid='{0}'", userguid);
            DataTable dt = BaseClass.GetDataTable(sql);
            if (BaseClass.estimate(dt))
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
