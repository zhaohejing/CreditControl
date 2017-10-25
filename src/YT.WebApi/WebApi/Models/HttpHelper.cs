using System;
using System.IO;
using System.Net;
using System.Text;

namespace YT.WebApi.Models
{
    public static class HttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        public static string Get(string urlString)
        {
            //定义局部变量  
            HttpWebRequest httpWebRequest;
            Stream stream;
            string htmlString;

            //请求页面  
            try
            {
                httpWebRequest = WebRequest.Create(urlString) as HttpWebRequest;
            }
            //处理异常  
            catch (Exception ex)
            {
                throw new Exception("建立页面请求时发生错误！", ex);
            }
            httpWebRequest.UserAgent =
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; Maxthon 2.0)";
            //获取服务器的返回信息  
            try
            {
                var httpWebRespones = (HttpWebResponse)httpWebRequest.GetResponse();
                stream = httpWebRespones.GetResponseStream();
            }
            //处理异常  
            catch (Exception ex)
            {
                throw new Exception("接受服务器返回页面时发生错误！", ex);
            }
            StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            //读取返回页面  
            try
            {
                htmlString = streamReader.ReadToEnd();
            }
            catch
            {
                htmlString = string.Empty;
            }
            //释放资源返回结果  
            streamReader.Close();
            stream.Close();
            return htmlString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Post(string url, string param)
        {
            var strUrl = url;
            var request = (HttpWebRequest)WebRequest.Create(strUrl);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            var paraUrlCoded = param;
            var payload = Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            var writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var s = response.GetResponseStream();
            string strDate;
            var strValue = "";
            var reader = new StreamReader(s, Encoding.UTF8);
            while ((strDate = reader.ReadLine()) != null)
            {
                strValue += strDate + "\r\n";
            }
            return strValue;
        }
    }
}
