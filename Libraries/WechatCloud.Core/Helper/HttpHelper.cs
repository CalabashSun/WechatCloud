using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WechatCloud.Core.Helper
{
    public class HttpHelper
    {
        /// <summary>
        /// get数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> headers, string contentType)
        {
            HttpWebRequest webRequest = null;
            HttpWebResponse webResponse = null;
            Stream resStream = null;
            StreamReader resStreamReader = null;
            // 响应信息
            string respContent = string.Empty;
            try
            {
                //构造一个Web请求的对象
                webRequest = (HttpWebRequest)WebRequest.Create(url);

                if (null != headers)
                {
                    // 设置请求头
                    foreach (KeyValuePair<string, string> kvp in headers)
                    {
                        webRequest.Headers.Add(kvp.Key, kvp.Value);
                    }
                }
                webRequest.ContentType = contentType;
                // 请求将跟随的重定向响应的最大数目。 默认值为 50
                webRequest.MaximumAutomaticRedirections = 50;
                webRequest.AllowAutoRedirect = true;

                //获取Web请求的响应内容
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                resStream = webResponse.GetResponseStream();
                //通过响应流构造一个StreamReader
                resStreamReader = new StreamReader(resStream, Encoding.UTF8);
                respContent = resStreamReader.ReadToEnd();
            }
            catch (WebException ex)
            {
                webResponse = (HttpWebResponse)ex.Response;
                int httpCode = (int)webResponse.StatusCode;

                resStream = webResponse.GetResponseStream();
                resStreamReader = new StreamReader(resStream, Encoding.UTF8);
                respContent = resStreamReader.ReadToEnd();
            }
            finally
            {
                if (null != resStreamReader)
                {
                    resStreamReader.Close();
                }
                if (null != resStream)
                {
                    resStream.Close();
                }
                if (null != webResponse)
                {
                    webResponse.Close();
                }
                if (null != webRequest)
                {
                    webRequest.Abort();
                }
            }
            return respContent;
        }


        public static string Post(string url, Dictionary<string, string> headers, string data, string contentType)
        {
            return executeHttpRequest(url, headers, data, "POST", contentType);
        }

        /// <summary>
        /// 数据库插入记录
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsondata"></param>
        /// <returns></returns>
        public static string PostJsonData(string url, string jsondata)
        {
            string contentType = "application/json";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddParameter(contentType, jsondata, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response.Content;
        }


        /// <summary>
        /// 上传文件流至微信服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostFileByteWechat(string url, byte[] fileData, string fileName, Dictionary<string, string> postData)
        {
            fileName = CommonHelper.GenerateRandomDigitCode(5) + ".jpg";
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.Method = Method.POST;
            if (null != postData)
            {
                // 设置请求头
                foreach (KeyValuePair<string, string> kvp in postData)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }
            request.AddFileBytes("file", fileData, fileName);
            var response = client.Execute(request);
            return response.Content;
        }
        /// <summary>
        /// 上传图片至微信服务器
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string PostFileWechat(string url, string filePath, Dictionary<string, string> postData)
        {

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.Method = Method.POST;
            if (null != postData)
            {
                // 设置请求头
                foreach (KeyValuePair<string, string> kvp in postData)
                {
                    request.AddParameter(kvp.Key, kvp.Value);
                }
            }
            request.AddFile("file", filePath);
            var response = client.Execute(request);
            return response.Content;
        }

        #region 执行Http请求
        private static string executeHttpRequest(string url, Dictionary<string, string> headers, string data, string reqMethod, string contentType)
        {
            HttpWebRequest webRequest = null;
            HttpWebResponse webResponse = null;
            Stream reqStream = null;
            Stream resStream = null;
            StreamReader resStreamReader = null;
            // 响应信息
            string respContent = string.Empty;
            try
            {
                //构造一个Web请求的对象
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");

                if (null != headers)
                {
                    // 设置请求头
                    foreach (KeyValuePair<string, string> kvp in headers)
                    {
                        webRequest.Headers.Add(kvp.Key, kvp.Value);
                    }
                }

                webRequest.Headers.Add("Platform", "10");
                //转成网络流
                if (null != data)
                {
                    byte[] bodyBytes = encoding.GetBytes(data);
                    webRequest.Method = reqMethod;
                    webRequest.ContentLength = bodyBytes.Length;
                    webRequest.ContentType = contentType;
                    // 请求将跟随的重定向响应的最大数目。 默认值为 50
                    webRequest.MaximumAutomaticRedirections = 50;
                    webRequest.AllowAutoRedirect = true;

                    // 发送请求
                    reqStream = webRequest.GetRequestStream();
                    reqStream.Write(bodyBytes, 0, bodyBytes.Length);

                }
                else
                {
                    webRequest.Method = reqMethod;
                    webRequest.ContentType = contentType;
                    // 请求将跟随的重定向响应的最大数目。 默认值为 50
                    webRequest.MaximumAutomaticRedirections = 50;
                    webRequest.AllowAutoRedirect = true;
                    reqStream = webRequest.GetRequestStream();
                }

                reqStream.Close();


                //获取Web请求的响应内容
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                int httpStatus = (int)webResponse.StatusCode;

                string statusDescription = webResponse.StatusDescription;

                resStream = webResponse.GetResponseStream();
                //通过响应流构造一个StreamReader
                resStreamReader = new StreamReader(resStream, Encoding.UTF8);
                respContent = resStreamReader.ReadToEnd();
            }
            catch (WebException ex)
            {
                webResponse = (HttpWebResponse)ex.Response;
                int httpStatus = (int)webResponse.StatusCode;
                string statusDescription = webResponse.StatusDescription;

                resStream = webResponse.GetResponseStream();
                resStreamReader = new StreamReader(resStream, Encoding.UTF8);
                respContent = resStreamReader.ReadToEnd();
            }
            finally
            {
                if (null != reqStream)
                {
                    reqStream.Close();
                }
                if (null != resStreamReader)
                {
                    resStreamReader.Close();
                }
                if (null != resStream)
                {
                    resStream.Close();
                }
                if (null != webResponse)
                {
                    webResponse.Close();
                }
                if (null != webRequest)
                {
                    webRequest.Abort();
                }
            }
            return respContent;
        }
        #endregion
    }
}
