using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using Caiman.database;
using Caiman.models;

namespace Caiman.logique
{
    public class Download
    {
        string pathToFolder;
        int idGame;
        string apiKey;
        string filename;
        WebClient webClient;
        public int percentage = 0;
        public CallAPI callAPI = new CallAPI();

        public Download(string pathToFolderp, int idGamep, string apiKeyp,string filenamep)
        {
            pathToFolder = pathToFolderp;
            idGame = idGamep;
            apiKey = apiKeyp;
            filename = filenamep;
        }
        public Download()
        {
            
        }

        public void StartDownload()
        {
            
            if (!CheckIfFileIsPresent())
            {
                webClient = new WebClient();
                webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                Uri uri = new Uri("http://api.caiman.cfpt.info/games/?idGame="+idGame+"&apiKey="+apiKey);
                webClient.DownloadFileAsync(uri,pathToFolder+filename);
                
            }
        }



        public  Uri AddParameter( Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
         percentage =  e.ProgressPercentage;
        }

        /// <summary>
        /// Used to know if a file is present on the disk
        /// </summary>
        /// <returns></returns>
        private bool CheckIfFileIsPresent()
        {
            bool isPresent = false;
            if (File.Exists(pathToFolder+filename))
            {
                isPresent = true;
            }
            return isPresent;
        }
    }
}
