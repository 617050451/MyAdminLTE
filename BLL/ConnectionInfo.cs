using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace AdminBaseClass
{
    public class ConnectionInfo
    {
         /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionCon(string ConnectionString)
        {
            bool IsCanConnectioned = false;
            //创建连接对象
            SqlConnection mySqlConnection = new SqlConnection(ConnectionString);
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch
            {
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                mySqlConnection.Close();
            }
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                return IsCanConnectioned;
            }
            else
            {
                return IsCanConnectioned;
            }
        }
    }
}
