/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Model for user and fonction to syc syve and login
 */
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
        private const string PATH_TO_MEMCARDS_PLAYSTATION2 = @"..\..\..\emulators\PCSX2\memcards\";
        private const string PATH_TO_MEMCARDS_A_GAMECUBE = @"..\..\..\emulators\Dolphin\User\GC\EUR\Card A\";
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
        public EmulatorsManager emulatorsManager;


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
        /// <summary>
        /// start the timer who will check if the save has been updated
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(CheckIfSaveIsUpdated);
            timer.Interval = 3000;
            timer.Start();
        }

        /// <summary>
        /// Create the save moanager for alla the emuators
        /// </summary>
        /// <param name="emulatorsManagerp"></param>
        public void CreateSaveManagers(EmulatorsManager emulatorsManagerp)
        {

            downloadSaveManager = new DownloadSaveManager(this);
            var SavePath = Environment.CurrentDirectory;

            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var savePathPlaystation = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\Playstation2\");
            var savePathGamecubeWii = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\GamecubeWii\");


            downloadSaveManager.CreateDownload(1,  apitoken);
            downloadSaveManager.CreateDownload(2,  apitoken);
            downloadSaveManager.StartDownload();
            saveManagerPlaystation2 = new SaveManager(savePathPlaystation, SavePath + PATH_TO_MEMCARDS_PLAYSTATION2, false, emulatorsManagerp);
            saveManagerGamecubeWii = new SaveManager(savePathGamecubeWii, SavePath + PATH_TO_MEMCARDS_A_GAMECUBE, false, emulatorsManagerp);

            InitTimer();
        }
        /// <summary>
        /// Login function to with the API
        /// </summary>
        /// <param name="usernamep"></param>
        /// <param name="password"></param>
        /// <param name="emulatorsManagerp"></param>
        public void Login(string usernamep, string password, EmulatorsManager emulatorsManagerp)
        {
            User value = callAPI.CallLogin(usernamep, password, emulatorsManagerp);
            id = value.id;
            username = value.username;
            apitoken = value.apitoken;
            caimanToken = value.caimanToken;
            email = value.email;
        }
        /// <summary>
        /// Check if the save has benn updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIfSaveIsUpdated(object sender, EventArgs e)
        {
            saveManagerGamecubeWii.ScanFolder();
            saveManagerPlaystation2.ScanFolder();
        }
        /// <summary>
        /// Move file from appdata to emulator saves folders
        /// </summary>
        public void MoveFileFromUserFolderToEmulatorFolder()
        {
            if (saveManagerGamecubeWii == null)
            {
                var SavePath = Environment.CurrentDirectory;

                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                var savePathPlaystation = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\Playstation2\");
                var savePathGamecubeWii = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\Save\GamecubeWii\");
                saveManagerPlaystation2 = new SaveManager(savePathPlaystation, SavePath + @"..\..\..\emulators\PCSX2\memcards\", false, emulatorsManager);
                saveManagerGamecubeWii = new SaveManager(savePathGamecubeWii, SavePath + @"..\..\..\emulators\Dolphin\User\GC\EUR\Card A\", false, emulatorsManager);
            }
            saveManagerPlaystation2.MoveSaveFileFromUserFolderToEmulatorSaveFolder();
            saveManagerGamecubeWii.MoveSaveFileFromUserFolderToEmulatorSaveFolder();
        }

        /// <summary>
        /// Create the users folder and the config files
        /// </summary>
        public void CreateUserFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var userPath = Path.Combine(appDataPath, @"Caiman\users\" + username + @"\");
            var savePath = Path.Combine(userPath, @"Save\");
            var savePathPlaystation = Path.Combine(savePath, @"Playstation2\");
            var savePathGamecubeWii = Path.Combine(savePath, @"GamecubeWii\");

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

        }

    }
}
