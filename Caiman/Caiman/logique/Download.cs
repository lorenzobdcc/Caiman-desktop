/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class to download a game
 */
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
        private const string URL_TO_GAMES = "http://api.caiman.cfpt.info/games/";
        string pathToFolder;
        public int idGame;
        string apiKey;
        public string filename;
        WebClient webClient;
        public int percentage = 0;
        public CallAPI callAPI = new CallAPI();
        public DownloadManager downloadManager;

        public bool active = false;

        public Download(string pathToFolderp, int idGamep, string apiKeyp,string filenamep,DownloadManager downloadManagerp)
        {
            pathToFolder = pathToFolderp;
            idGame = idGamep;
            apiKey = apiKeyp;
            filename = filenamep;
            downloadManager = downloadManagerp;
        }
        public Download()
        {
            
        }
        /// <summary>
        /// Start the download of the file
        /// </summary>
        public void StartDownload()
        {
            
            if (!CheckIfFileIsPresent())
            {
                webClient = new WebClient();
                Uri uri = new Uri(URL_TO_GAMES + "?idGame="+idGame+"&apiKey="+apiKey);
                webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
                webClient.DownloadFileAsync(uri,pathToFolder+"temp."+filename);
                active = true;
            }
        }
        /// <summary>
        /// check the percent of the downloading if the percentage is 100 start the next download and rename the file to the right name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void  wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            percentage = e.ProgressPercentage;
            if (percentage == 100 && active == true)
            {
                active = false;
                downloadManager.NextDownload();
                File.Move(pathToFolder + "temp." + filename, pathToFolder + filename);
                downloadManager.emulatorsManager.gamesListConfigFile.AddValue(idGame.ToString());
            }
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
