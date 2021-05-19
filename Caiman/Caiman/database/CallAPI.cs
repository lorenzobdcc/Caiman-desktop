using Caiman.models;
using Newtonsoft.Json;
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
        private const string URL_STRING = "http://api.caiman.cfpt.info";
        Uri baseUrl = new Uri(URL_STRING);

        IRestClient client;

        IRestRequest requestGET =new RestRequest("get", Method.GET);
        IRestRequest requestPOST = new RestRequest("post", Method.POST);

        public CallAPI()
        {
            client = new RestClient(baseUrl);
        }
        public class DataObject
        {
            public string Name { get; set; }
        }

        public string Call()
        {
            string value = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);
            if (response.Content != "")
            {
                value =  response.Content.ToString();
            }

            return value;

        }

        public User CallLogin(string username, string password)
        {
            User tempUser = new User();
            baseUrl = new Uri("http://api.caiman.cfpt.info/users/connection/");
            client.BaseUrl = baseUrl;
            string tempString = "";

            requestPOST.AddParameter("username", username);
            requestPOST.AddParameter("password", password);
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);


            if (response.Content != "")
            {
                tempString = response.Content.ToString();
            }

            if (response.IsSuccessful)
            {
                dynamic data = JsonConvert.DeserializeObject(tempString);
                tempUser.id = data.id;
                tempUser.username = data.username;
                tempUser.apitoken = data.apitocken;
                tempUser.email = data.email;
            }
            return tempUser;
        }

        public List<Game> CallAllGames()
        {
            requestGET = new RestRequest("get", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            requestGET =new RestRequest("get", Method.GET);
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                foreach (var game in data)
                {
                    Game tempGame = new Game((int)game.id.Value, game.description.Value, game.imageName.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
                    lst_games.Add(tempGame);
                }

            }

            return lst_games;
        }

        public List<Category> CallAllCategories()
        {
            List<Category> lst_categories = new List<Category>();
            baseUrl = new Uri((URL_STRING + @"/categories/"));
            string tempString = "";

            client.BaseUrl = baseUrl;
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);


            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                foreach (var category in data)
                {
                    Category tempCategory = new Category((int)category.id.Value, category.name.Value);
                    lst_categories.Add(tempCategory);
                }

            }

            return lst_categories;
        }

        public List<Game> CallUserFavoriteGames(int userId)
        {
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestGET = new RestRequest("get", Method.GET);
            requestGET.AddParameter("byUserFavorite", userId);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);


            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                foreach (var game in data)
                {
                    Game tempGame = new Game((int)game.id.Value, game.description.Value, game.imageName.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
                    lst_games.Add(tempGame);
                }

            }

            return lst_games;
        }

        public List<Game> CallGamesFromCategory(int categoryId)
        {
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestGET = new RestRequest("get", Method.GET);
            requestGET.AddParameter("byCategory", categoryId);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);


            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                foreach (var game in data)
                {
                    Game tempGame = new Game((int)game.id.Value, game.description.Value, game.imageName.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
                    lst_games.Add(tempGame);
                }

            }

            return lst_games;
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
