using Caiman.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models
{
    public class User
    {
        public int id;
        public string username;
        public string apitoken;
        public string email;
        CallAPI callAPI = new CallAPI();


        public User(int idp, string usernamep, string apitokenp, string emailp )
        {
            id = idp;
            username = usernamep;
            apitoken = apitokenp;
            email = emailp;
        }

        public User()
        {
            id = 0;
            username = "default_username";
            apitoken = "default_apitoken";
            email = "default@email.test";
        }

        public void Login(string usernamep, string password)
        {
            User value = callAPI.CallLogin(usernamep, password);
            id = value.id;
            username = value.username;
            apitoken = value.apitoken;
            email = value.email;
            CreateUserFolder();
            
        }

        public void CreateUserFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userPath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\");
            var savePath = Path.Combine(userPath, @"Save\");
            var configFile = Path.Combine(userPath, @"config.ini");
            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            if (!File.Exists(configFile))
            {
                File.Create(configFile);
            }
        }

    }
}
