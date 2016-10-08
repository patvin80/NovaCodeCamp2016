using LinqToTwitter;
using OfficeApp2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//http://johnatten.com/2013/07/01/creating-a-clean-minimal-footprint-asp-net-webapi-project-with-vs-2012-and-asp-net-mvc-4/
namespace OfficeApp2Web
{
    public class ValuesController : ApiController
    {
        public string WatsonUID = ConfigurationManager.AppSettings["WatsonUID"].ToString();
        public string WatsonPWD = ConfigurationManager.AppSettings["WatsonPWD"].ToString();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #region "Talk About this Section"
        // GET api/<controller>/Get?message=.....
        public string Get(string message)
        {
            WatsonTone toneAnalyzer = new WatsonTone(
                                           "https://gateway.watsonplatform.net/tone-analyzer/api",
                                           WatsonUID,
                                           WatsonPWD);


            return toneAnalyzer.AnalyzeTone(message);//get some json from your DB
        }
        #endregion

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}