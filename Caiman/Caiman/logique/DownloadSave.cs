/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class used to download save file
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
using System.IO.Compression;

namespace Caiman.logique
{
    public class DownloadSave
    {
        private const string URL_TO_API_GAMES_ENDPOINT = "http://api.caiman.cfpt.info/games/";
        string pathToFolder;
        public int idEmulator;
        public int idUser;
        string apiKey;
        public string filename;
        WebClient webClient;
        public int percentage = 0;
        public CallAPI callAPI = new CallAPI();
        public DownloadSaveManager downloadManager;
        public string username;

        public bool active = false;

        public DownloadSave(string pathToFolderp, int idEmulatorp, int idUserp, string apiKeyp,string usernamep, DownloadSaveManager downloadManagerp)
        {
            pathToFolder = pathToFolderp;
            idEmulator = idEmulatorp;
            idUser = idUserp;
            apiKey = apiKeyp;
            username = usernamep;
            downloadManager = downloadManagerp;
        }
        public DownloadSave()
        {

        }
        /// <summary>
        /// Start the download of the save file depend on the emulator
        /// </summary>
        public void StartDownload()
        {


            switch (idEmulator)
            {
                case 1:
                    filename = "GamecubeWii.zip";
                    break;
                case 2:
                    filename = "Playstation2.zip";
                    break;
                default:
                    break;
            }
            webClient = new WebClient();
            Uri uri = new Uri(URL_TO_API_GAMES_ENDPOINT + "?idEmulator=" + idEmulator + "&idUser=" + idUser + "&apiKey=" + apiKey);
            webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
            webClient.DownloadFileAsync(uri, pathToFolder + filename);
            active = true;

        }
        /// <summary>
        /// Check if the download is finish and if he is finished unzip the save file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            percentage = e.ProgressPercentage;
            if (percentage == 100 && active == true)
            {
                active = false;
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var savePathZip = "";
                var savePath = "";
                switch (idEmulator)
                {
                    case 1:
                        savePathZip = Path.Combine(appDataPath, @"Caiman\users\" +username + @"\Save\GamecubeWii.zip");
                        savePath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\GamecubeWii\");
                        break;
                    case 2:
                        savePathZip = Path.Combine(appDataPath, @"Caiman\users\" +username + @"\Save\Playstation2.zip");
                        savePath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\Playstation2\");
                        break;
                    default:
                        break;
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(savePath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                ZipFile.ExtractToDirectory(savePathZip, savePath);

            }
        }




    }
}
