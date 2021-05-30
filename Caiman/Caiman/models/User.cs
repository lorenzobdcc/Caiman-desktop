using Caiman.database;
using Caiman.logique;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.models
{
    public class User
    {
        public int id;
        public string username;
        public string apitoken;
        public string caimanToken;
        public string email;
        DownloadSaveManager downloadSaveManager;
        CallAPI callAPI = new CallAPI();
        SaveManager saveManagerPlaystation2;
        SaveManager saveManagerGamecubeWii;
        Timer timer = new Timer();


        public User(int idp, string usernamep, string apitokenp, string caimanTokenp, string emailp )
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
            caimanToken = "0";
            email = "default@email.test";
        }

        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(CheckIfSaveIsUpdated);
            timer.Interval = 3000;
            timer.Start();
        }

        public void CreateSaveManagers()
        {
            downloadSaveManager = new DownloadSaveManager(this);
            var SavePath = Environment.CurrentDirectory;

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePathPlaystation = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\Playstation2\");
            var savePathGamecubeWii = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\GamecubeWii\");
            saveManagerPlaystation2 = new SaveManager(savePathPlaystation, SavePath + @"..\..\..\emulators\PCSX2\memcards\", false);
            saveManagerGamecubeWii = new SaveManager(savePathGamecubeWii, SavePath + @"..\..\..\emulators\Dolphin\User\GC\EUR\Card A\", false);

            downloadSaveManager.CreateDownload(1, id, apitoken);
            downloadSaveManager.CreateDownload(2, id, apitoken);
            downloadSaveManager.StartDownload();

            InitTimer();
        }

        public void Login(string usernamep, string password)
        {
            User value = callAPI.CallLogin(usernamep, password);
            id = value.id;
            username = value.username;
            apitoken = value.apitoken;
            caimanToken = value.caimanToken;
            email = value.email;
        }
        private void CheckIfSaveIsUpdated(object sender, EventArgs e)
        {
            saveManagerGamecubeWii.ScanFolder();
            saveManagerPlaystation2.ScanFolder();
        }

        public void CreateUserFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userPath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\");
            var savePath = Path.Combine(userPath, @"Save\");
            var savePathPlaystation = Path.Combine(savePath, @"Playstation2\");
            var savePathGamecubeWii = Path.Combine(savePath, @"GamecubeWii\");
            var configFile = Path.Combine(userPath, @"config.ini");

            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            if (!Directory.Exists(savePathPlaystation))
            {
                Directory.CreateDirectory(savePathPlaystation);
            }
            if (!Directory.Exists(savePathGamecubeWii))
            {
                Directory.CreateDirectory(savePathGamecubeWii);
            }
            if (!File.Exists(configFile))
            {
                File.Create(configFile);
            }
        }

    }
}
