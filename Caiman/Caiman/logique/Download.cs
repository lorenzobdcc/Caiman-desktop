using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Caiman.logique
{
    public class Download
    {
        string pathToFolder;
        int idGame;
        string apiKey;
        WebClient webClient;
        public int percentage = 0;

        public Download(string pathToFolderp, int idGamep, string apiKeyp)
        {
            pathToFolder = pathToFolderp;
            idGame = idGamep;
            apiKey = apiKeyp;

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
                Uri uri = new Uri("http://api.caiman.cfpt.info/games/");
                string body = "id = " + idGame;
                body += " apiKey = " + apiKey;
                webClient.DownloadFileAsync(uri,pathToFolder);
            }
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
            if (File.Exists(pathToFolder))
            {
                isPresent = true;
            }
            return isPresent;
        }
    }
}
