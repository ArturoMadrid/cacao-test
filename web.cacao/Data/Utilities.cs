using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace web.cacao.Data
{
    public class Utilities
    {
        public enum Metodo
        {
            POST = 1,
            GET = 2
        }

        public static string ObtenerHtmlResponse(string path, Metodo tipo, string cuerpo = null)
        {
            String result = null;
            Byte[] bytesCuerpo = cuerpo == null ? null : UTF8Encoding.UTF8.GetBytes((String)cuerpo);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            request.KeepAlive = true;
            request.Method = tipo.ToString();
            request.KeepAlive = false;
            request.Accept = "*/*";
            request.Timeout = 150000;
            request.ContentLength = cuerpo == null ? 0 : bytesCuerpo.Length;

            //request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.ContentType = "application/json";

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ((senderer, certificate, chain, ssPolicyErrors) => true);

                if (cuerpo != null)
                {
                    using (Stream POSTstream = request.GetRequestStream())
                    {
                        POSTstream.Write(bytesCuerpo, 0, bytesCuerpo.Length);
                        POSTstream.Close();
                    }
                }

                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                result = sr.ReadToEnd();

                return result;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (ex.Response is HttpWebResponse response)
                        throw new Exception($"Status Code: {(int)response.StatusCode} | Msg: {ex.Message} | IE: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)} | ST: {ex.StackTrace}");
                }
                throw;
            }
        }

        public static string SerializarJson(object objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public static T DeserializarJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
