using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Caiman.database
{
    public class CallAPI
    {
        Uri baseUrl = new Uri("http://127.0.0.1/public/games/");

        IRestClient client;

        IRestRequest request =new RestRequest("get", Method.GET); 

        public CallAPI()
        {
            client = new RestClient(baseUrl);
            request.AddParameter("clientId", 123);
        }
        public class DataObject
        {
            public string Name { get; set; }
        }

        public string Call()
        {
            string value = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(request);
            if (response.Content != "")
            {
                value =  response.Content.ToString();
            }

            return value;





        }

        public class Args
        {
            public string clientId { get; set; }
        }

        public class Headers
        {
            public string Accept { get; set; }

            public string AcceptEncoding { get; set; }

            public string AcceptLanguage { get; set; }

            public string Authorization { get; set; }

            public string Connection { get; set; }

            public string Dnt { get; set; }

            public string Host { get; set; }

            public string Origin { get; set; }

            public string Referer { get; set; }

            public string UserAgent { get; set; }
        }

        public class RootObject
        {
            public Args args { get; set; }

            public Headers headers { get; set; }

            public string origin { get; set; }

            public string url { get; set; }

            public string data { get; set; }

            public Dictionary<string, string> files { get; set; }
        }
    }
}
