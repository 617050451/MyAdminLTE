using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace BLL
{
    public class WeChatHelper
    {
        #region 单例模式
        private static WeChatHelper singleMode;
        private static readonly object locker = new object();
        public static WeChatHelper GetInstance
        {
            get
            {
                if (singleMode == null)
                {
                    lock (locker)
                    {
                        if (singleMode == null)
                            singleMode = new WeChatHelper();
                    }
                }
                return singleMode;
            }
        }

        private WeChatHelper() { }

        #endregion

        private static string APPID = "wx6550f48a90fd4106";
        private static string APPSECRET = "6065b4f21a3ae66b1f25d25b7966141e";

        private string IsExistAccess_Token()
        {
            var dt = BaseClass.GetDataTable("select top(1) AccessToken from [t_WxConfig] WHERE [OverTime]>GetDate() and AppID='" + APPID + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["AccessToken"].ToString();
            }
            else
            {
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + APPID + "&secret=" + APPSECRET;
                var jsonStr = Get(url);
                var reObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonStr);
                BaseClass.ExecuteNonQuerySQL("update [t_WxConfig] set  AccessToken='" + reObj.access_token + "',[OverTime]=DATEADD(hour,2, getdate()) where AppID='" + APPID + "'");
                return reObj.access_token;
            }
        }
        #region 微信基础参数获取

        private string GetJsTicket()
        {
            var token = IsExistAccess_Token();
            var url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";
            var jsonStr = Get(url);
            var reObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonStr);
            return reObj.ticket;
        }
        #endregion

        public string GetConfig1()
        {
            var pageUrl = HttpContext.Current.Request.Url.ToString();
            var jsapi_ticket = GetJsTicket();
            var timestamp = GenerateTimeStamp();
            var nonceStr = GenerateNonceStr();
            var signature = Getsignature(nonceStr, timestamp, pageUrl, jsapi_ticket);
            var dict = new Dictionary<string, string>();
            dict.Add("appId", APPID);
            dict.Add("timeStamp", timestamp);
            dict.Add("nonceStr", nonceStr);
            dict.Add("signature", signature);
            return Newtonsoft.Json.JsonConvert.SerializeObject(dict);

        }

        public static string GetConfig()
        {
            return GetInstance.GetConfig1();
        }

        private string Getsignature(string noncestr, string timestamp, string pageUrl, string jsapi_ticket)
        {

            string tempstr = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + pageUrl;
            var sha1 = SHA1(tempstr);
            return sha1.ToLower();
        }


        #region 通用辅助方法组


        public static string Get(string url)
        {
            var request = System.Net.WebRequest.Create(url);
            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
            using (var sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }


        /// <summary>
        /// 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        /// </summary>
        /// <returns></returns>
        private static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成随机串，随机串包含字母或数字
        /// </summary>
        /// <returns></returns>
        private static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }


        #region 加密
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <returns>返回40位UTF8 大写</returns>  
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        #endregion
        #endregion
    }
}
