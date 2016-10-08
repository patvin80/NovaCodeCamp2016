using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace OfficeApp2.Models
{
    class WatsonTone
    {
        private string url;
        private string uid;
        private string pwd;

        public WatsonTone(string baseURL, string uid, string pwd)
        {
            this.url = baseURL + "/v3/tone?version=2016-05-19";
            this.uid = uid;
            this.pwd = pwd;

            Console.WriteLine("url:" + this.url);
            Console.WriteLine("uid:" + this.uid);
            Console.WriteLine("pwd:" + this.pwd);
        }

        public string AnalyzeTone(string question)
        {
            string answers = null;
            string data = "{\"text\" : \"" + question + "\"}";

            var qaCall = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                #region "Talk about Authentication"
                
                string auth = string.Format("{0}:{1}", this.uid, this.pwd);
                string auth64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
                string credentials = string.Format("{0} {1}", "Basic", auth64);

                qaCall.Headers[HttpRequestHeader.Authorization] = credentials;
                qaCall.Method = "POST";
                qaCall.Accept = "application/json";
                qaCall.ContentType = "application/json";
                
                #endregion

                var encoding = new UTF8Encoding();
                var payload = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                qaCall.ContentLength = payload.Length;
                using (var callStream = qaCall.GetRequestStream())
                {
                    callStream.Write(payload, 0, payload.Length);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("error:" + e.Message);
                Console.ReadKey();
            }

            try
            {
                WebResponse qaResponse = qaCall.GetResponse();
                Stream requestStream = qaResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(requestStream);
                answers = responseReader.ReadToEnd();
                responseReader.Close();
            }
            catch (System.Net.WebException e)
            {
                Console.Out.WriteLine("errors:" + e.Message);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("error:" + e.Message);
                Console.ReadKey();
            }

            return answers;
        }
    }
}