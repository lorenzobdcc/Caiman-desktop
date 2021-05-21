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
        public int idGame;
        string apiKey;
        public string filename;
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
                Uri uri = new Uri("http://api.caiman.cfpt.info/games/?idGame="+idGame+"&apiKey="+apiKey);
                webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                webClient.DownloadFileAsync(uri,pathToFolder+filename);
            }
        }

        void  wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            percentage = e.ProgressPercentage;
            
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
