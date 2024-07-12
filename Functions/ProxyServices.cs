using Kelloggs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Kelloggs.Functions
{
    public class ProxyGet
    {
        private JavaScriptSerializer json;
        private string responseJSon;
        private ProxyWcfRest WcfeCommerce;

        public ProxyWcfRest GetWcfeCommerce1()
        {
            return WcfeCommerce;
        }

        public void SetWcfeCommerce1(ProxyWcfRest value)
        {
            WcfeCommerce = value;
        }

        public JavaScriptSerializer GetJson()
        {
            return json;
        }

        public void SetJson(JavaScriptSerializer value)
        {
            json = value;
        }

        public string CallAPIPost(string metodo)
        {
            try
            {
                SetJson(new JavaScriptSerializer());
                SetWcfeCommerce1(new ProxyWcfRest());
                GetWcfeCommerce1().Uri = ConfigurationManager.AppSettings["ApiUrl"] + "api/" + metodo;
                GetWcfeCommerce1().Method = "POST";
                responseJSon = GetWcfeCommerce1().GetJSonResponse();
            }
            catch (Exception ex)
            {
                DDLog.objLog4Net.Error("Application_Error: ProxyGet-Controller ", ex);
            }
            return responseJSon;

        }

        public string CallAPIPost(string metodo, Parameters param)
        {
            Dictionary<string, object> requestJSon = new Dictionary<string, object>();
            try
            {
                Dictionary<string, object> requestJSon2 = new Dictionary<string, object>();
                foreach (var p in param.Parameter)
                {
                    if (p.Object != null)
                        requestJSon.Add(p.Field, p.Object);
                    else
                        requestJSon.Add(p.Field, p.Value);
                }

                requestJSon2.Add("Args", requestJSon);

                SetJson(new JavaScriptSerializer());
                SetWcfeCommerce1(new ProxyWcfRest());
                GetWcfeCommerce1().Uri = ConfigurationManager.AppSettings["ApiUrl"] + "api/" + metodo;
                GetWcfeCommerce1().Method = "POST";
                GetWcfeCommerce1().DataJSon = GetJson().Serialize(requestJSon2);
                responseJSon = GetWcfeCommerce1().GetJSonResponse();
            }
            catch (Exception ex)
            {
                DDLog.objLog4Net.Error("Application_Error: ProxyGet-Controller ", ex);
            }
            return responseJSon;

        }


    }
    public class ProxyWcfRest
    {
        private Stream dataStream;
        private WebResponse response;
        private StreamReader reader;

        public string Uri { get; set; }
        public string DataJSon { get; set; }
        public string Method { get; set; }
        public string InvokeStatus { get; set; }

        public string GetJSonResponse()
        {
            string responseFromServer = string.Empty;

            try
            {
                byte[] byteArray = null;
                if (DataJSon != null)
                    byteArray = Encoding.UTF8.GetBytes(DataJSon);

                WebRequest request = WebRequest.Create(Uri);
                request.Method = Method;
                request.ContentType = "application/json";
                if (byteArray != null)
                    request.ContentLength = byteArray.Length;

                request.Headers.Add(HttpRequestHeader.Authorization, "");

                dataStream = request.GetRequestStream();
                if (byteArray != null)
                    dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                response = request.GetResponse();
                InvokeStatus = ((HttpWebResponse)response).StatusDescription;
                dataStream = response.GetResponseStream();

                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                DDLog.objLog4Net.Error("Application_Error: GetJSonResponse ", ex);
                responseFromServer = "{\"Success\": 0, \"Message\": \"" + ex.Message.ToString() + "\"}";
            }
            finally
            {
                reader?.Close();
                dataStream?.Close();
                response?.Close();
            }

            return responseFromServer;
        }

    }
}