using Caiman.database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public void Login(string username, string password)
        {
            User value = callAPI.CallLogin(username, password);
            id = value.id;
            username = value.username;
            apitoken = value.apitoken;
            email = value.email;
            
        }

    }
}
