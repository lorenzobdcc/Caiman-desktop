using Caiman.logique;
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



        public User CallLogin(string username, string password,EmulatorsManager emulatorManagerp)
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
                tempUser.caimanToken = data.caimanToken;
                tempUser.CreateUserFolder();
                tempUser.CreateSaveManagers(emulatorManagerp);
            }
            return tempUser;
        }
        public User CallLoginToken(string token,EmulatorsManager emulatorManagerP)
        {
            User tempUser = new User();
            baseUrl = new Uri("http://api.caiman.cfpt.info/users/connection/");
            client.BaseUrl = baseUrl;
            string tempString = "";

            requestPOST.AddParameter("caimanToken", token);
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
                tempUser.caimanToken = data.caimanToken;

                tempUser.CreateUserFolder();
                tempUser.CreateSaveManagers(emulatorManagerP);
            }
            return tempUser;
        }

        public void UploadSave(int idEmulator, int idUser ,string apiKey, string path)
        {
            requestPOST = new RestRequest("", Method.POST);
            baseUrl = new Uri("http://api.caiman.cfpt.info/games/");
            client.BaseUrl = baseUrl;
            string tempString = "";

            requestPOST.AddParameter("apiKey", apiKey);
            requestPOST.AddParameter("idUser", idUser);
            requestPOST.AddParameter("idEmulator", idEmulator);
            requestPOST.AddHeader("Content-Type", "multipart/form-data");
            requestPOST.AddFile("fileSave", path);
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);

        }

        public List<Game> CallAllGames()
        {
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            requestGET = new RestRequest("get", Method.GET);
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                foreach (var game in data)
                {
                    int temp_id = (int)game.id.Value;
                    Game tempGame = new Game((int)game.id.Value, game.name.Value, game.description.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
                    lst_games.Add(tempGame);
                }

            }

            return lst_games;
        }

        public Game CallOneGame(int idGame)
        {
            Game tempGame = new Game();
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/"+idGame;
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                 tempGame = new Game((int)data.id.Value, data.name.Value, data.description.Value, data.imageName.Value, (int)data.idConsole.Value, (int)data.idFile.Value);

            }

            return tempGame;
        }

        public TimeInGame CallTimeInGameUser(int idGame,int idUser)
        {
            TimeInGame time = new TimeInGame();
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestGET.AddParameter("idGameTime", idGame);
            requestGET.AddParameter("idUser", idUser);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                time = new TimeInGame((int)data.minutes.Value);


            }

            return time;
        }
        public string CallFileNameGame(int idGame)
        {
            FileModel tempFile = new FileModel();
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";


            requestGET.AddParameter("gameFileName", idGame);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                tempFile = new FileModel((int)data.id.Value, data.filename.Value, data.date.Value);


            }

            return tempFile.filename;
        }

        public string CallFolderNameGame(int idGame)
        {
            ConsoleModel tempConsole = new ConsoleModel();
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";


            requestGET.AddParameter("gameConsole", idGame);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                tempConsole = new ConsoleModel((int)data.id.Value, data.name.Value, data.folderName.Value, (int)data.idEmulator.Value);


            }

            return tempConsole.folderName;
        }
        public string CallConsoleNameGame(int idGame)
        {
            ConsoleModel tempConsole = new ConsoleModel();
            requestGET = new RestRequest("", Method.GET);
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";


            requestGET.AddParameter("gameConsole", idGame);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestGET);

            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                tempConsole = new ConsoleModel((int)data.id.Value, data.name.Value, data.folderName.Value, (int)data.idEmulator.Value);


            }

            return tempConsole.name;
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
                    Game tempGame = new Game((int)game.id.Value, game.name.Value, game.description.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
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
                    Game tempGame = new Game((int)game.id.Value, game.name.Value, game.description.Value, game.imageName.Value, (int)game.idConsole.Value, (int)game.idFile.Value);
                    lst_games.Add(tempGame);
                }

            }

            return lst_games;
        }

        public void AddGameToFavorite(int idGame,int idUser)
        {
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestPOST = new RestRequest("post", Method.POST);
            requestPOST.AddParameter("idGameAdd", idGame);
            requestPOST.AddParameter("idUser", idUser);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);

        }

        public void RemoveGameFromFavorite(int idGame, int idUser)
        {
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestPOST = new RestRequest("post", Method.POST);
            requestPOST.AddParameter("idGameRemove", idGame);
            requestPOST.AddParameter("idUser", idUser);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);

        }

        public void AddOneMinuteToGame(int idGame, int idUser)
        {
            List<Game> lst_games = new List<Game>();
            string fullURL = URL_STRING + "/games/";
            requestPOST = new RestRequest("post", Method.POST);
            requestPOST.AddParameter("idGameTimeAdd", idGame);
            requestPOST.AddParameter("idUser", idUser);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);

        }

        public bool CheckIfGameIsInFavorite(int idGame, int idUser)
        {
            string fullURL = URL_STRING + "/games/";
            requestPOST = new RestRequest("post", Method.POST);
            requestPOST.AddParameter("idGameCheck", idGame);
            requestPOST.AddParameter("idUser", idUser);
            baseUrl = new Uri(fullURL);
            client.BaseUrl = baseUrl;
            string tempString = "";
            IRestResponse<RootObject> response = client.Execute<RootObject>(requestPOST);
            bool tempValue = false;
            if (response.Content != "")
            {
                tempString = response.Content.ToString();

                dynamic data = JsonConvert.DeserializeObject(tempString);
                tempValue = data;


            }
            return tempValue;
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
