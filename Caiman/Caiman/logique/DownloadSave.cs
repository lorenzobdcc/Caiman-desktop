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
            Uri uri = new Uri("http://api.caiman.cfpt.info/games/?idEmulator=" + idEmulator + "&idUser=" + idUser + "&apiKey=" + apiKey);
            webClient.DownloadProgressChanged += wc_DownloadProgressChanged;
            webClient.DownloadFileAsync(uri, pathToFolder + filename);
            active = true;

        }

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
                ZipFile.ExtractToDirectory(savePathZip,savePath);

            }
        }




    }
}
