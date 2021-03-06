/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Main class of the project used to interact with the emulators
 */
using Caiman.database;
using Caiman.interfaceG;
using Caiman.interfaceG.usercontrol;
using Caiman.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.logique
{
    public class EmulatorsManager
    {
        public GameTimer gameTimer;
        public User user;
        public DownloadManager downloadManager;
        public ConfigFileEditor gamesListConfigFile;
        private CallAPI callAPI = new CallAPI();

        private PCSX2 PCSX2 = new PCSX2();
        private Dolphin dolphin = new Dolphin();
        public Emulator actualEmulator;

        private SaveManager saveManagerPlaystation2;
        private SaveManager saveManagerGamecubeWii;
        bool folderAlreadyScaned = false;

        public ConfigFileEditor configFile;
        public ConfigFileEditor loginFile;

        public Game actualGame;
        public XboxMainForm xboxMainForm;
        public enum Etatenum { stop = 0, start = 1 };
        private Etatenum emulatorState;
        Timer timer = new Timer();

        public bool fullScreen;
        public int definition;
        public bool formatSeizeNeuvieme;
        public bool noGui;
        public int filtrageAnioscopique;

        /// <summary>
        /// used to set the name of the game in the navbar
        /// </summary>
        public Etatenum EmulatorState { get => emulatorState; set
            {
                
                if (emulatorState != value)
                {
                    emulatorState = value;
                    if (emulatorState == Etatenum.start)
                    {
                        NavbarXbox tempNavbar = (NavbarXbox)xboxMainForm.topPanel;
                        tempNavbar.actualGameName = actualGame.name;
                    }
                    else
                    {
                        NavbarXbox tempNavbar = (NavbarXbox)xboxMainForm.topPanel;
                        tempNavbar.actualGameName = "";
                        gameTimer = null;
                    }


                }
                
            }
        }
        /// <summary>
        /// Contructor of the Emulator manager
        /// This fonction will start some methode to create the users file, set som,e variables and check if the file of the games previously donwloaded are stil present on the user's disk
        /// </summary>
        /// <param name="xboxMainFormp"></param>
        public EmulatorsManager(XboxMainForm xboxMainFormp)
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //CreateSaveManagerAndScan();
            xboxMainForm = xboxMainFormp;
            EmulatorState = Etatenum.stop;
            user = new User();
            user.emulatorsManager = this;
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            
            
            CreateAppDataFolder();
            downloadManager = new DownloadManager(this);
            configFile = new ConfigFileEditor(gamesPath, "config.ini");
            loginFile = new ConfigFileEditor(gamesPath, "login.ini");

            gamesListConfigFile = new ConfigFileEditor(gamesPath, "games.ini");
            ScanConfiguration();
            CheckIfGameFileIsPresentOnDisk();
            InitTimer();
        }
        /// <summary>
        /// Start some fonction each 100ms
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(ScanEmulatorProcess);
            timer.Tick += new EventHandler(CheckIfSaveIsUpdated);
            timer.Interval = 100;
            timer.Start();
        }

        /// <summary>
        /// Create the save manager for the diférents émulators and scan the folder
        /// </summary>
        public void CreateSaveManagerAndScan()
        {
            user.emulatorsManager = this;
            var SavePath = Environment.CurrentDirectory;
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var savePathPlaystationString = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\Playstation2\");
            var savePathGamecubeWiiString = Path.Combine(appDataPath, @"Caiman\users\" + user.username + @"\Save\GamecubeWii\");
            saveManagerGamecubeWii = new SaveManager(SavePath + @"..\..\..\emulators\Dolphin\User\GC\EUR\Card A\",savePathGamecubeWiiString,true,this);
            saveManagerPlaystation2 = new SaveManager(SavePath + @"..\..\..\emulators\PCSX2\memcards\",savePathPlaystationString,true,this);
            saveManagerGamecubeWii.ScanFolder();
            saveManagerPlaystation2.ScanFolder();

        }

        /// <summary>
        /// Scan the folders of the save to check if the file has been modified since the last check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIfSaveIsUpdated(object sender, EventArgs e)
        {
            if (user != null && user.username != "default_username")
            {
                if (folderAlreadyScaned == false)
                {

                    CreateSaveManagerAndScan();
                    folderAlreadyScaned = true;
                }
                saveManagerGamecubeWii.ScanFolder();
                saveManagerPlaystation2.ScanFolder();
            }

        }

        /// <summary>
        /// Create the folder of the user and the folder of Caiman
        /// </summary>
        private void CreateAppDataFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var basePath = Path.Combine(appDataPath, @"Caiman\");
            var caimanConfigPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            var imgPath = Path.Combine(appDataPath, @"Caiman\img\");
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\games.ini");
            var configPath = Path.Combine(appDataPath, @"Caiman\Caiman\config.ini");
            var loginPath = Path.Combine(appDataPath, @"Caiman\Caiman\login.ini");

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }
            if (!Directory.Exists(caimanConfigPath))
            {
                Directory.CreateDirectory(caimanConfigPath);
            }
            if (!Directory.Exists(@"C:\Caiman\Playstation2"))
            {
                Directory.CreateDirectory(@"C:\Caiman\Playstation2\");
            }
            if (!Directory.Exists(@"C:\Caiman\GamecubeWii\"))
            {
                Directory.CreateDirectory(@"C:\Caiman\GamecubeWii\");
            }
            if (!File.Exists(loginPath))
            {
                using (StreamWriter sw = File.CreateText(loginPath))
                {
                    sw.WriteLine("token = 0");
                }
                loginFile = new ConfigFileEditor(caimanConfigPath, "login.ini");
            }
            if (!File.Exists(gamesPath))
            {
                using (StreamWriter sw = File.CreateText(gamesPath))
                {

                }
                configFile = new ConfigFileEditor(caimanConfigPath, "config.ini");
            }
            if (!File.Exists(configPath))
            {
                //if the file do not exist create it with with the default emulators value
                using (StreamWriter sw = File.CreateText(configPath))
                {
                    sw.WriteLine("configuration = 1080");
                    sw.WriteLine("fullscreen = true");
                    sw.WriteLine("definition = 3");
                    sw.WriteLine("formatSeizeNeuvieme = true");
                    sw.WriteLine("filtrageAnioscopique = 3");
                }
                configFile = new ConfigFileEditor(caimanConfigPath, "config.ini");

            }


        }

        /// <summary>
        /// Check if the file of the previous download is stil here
        /// </summary>
        private void CheckIfGameFileIsPresentOnDisk()
        {
            List<string> lst_idGames = gamesListConfigFile.GetAllValueInList();

            foreach (var idGameString in lst_idGames)
            {
                if (idGameString != "")
                {
                    int idGame = Convert.ToInt32(idGameString);
                    if (!File.Exists(@"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\" + callAPI.CallFileNameGame(idGame)))
                    {
                        gamesListConfigFile.DeleteValue(idGameString);
                    }
                }

            }
        }
        /// <summary>
        /// Start the correct emulator for the game pass in parameter
        /// Set the configuration of the emulator before starting it to be sure the correct parameter are applied
        /// </summary>
        /// <param name="idGame"></param>
        public void StartGame(int idGame)
        {
            //if a game is already started do nothing
            if (emulatorState == Etatenum.stop)
            {


                string console = callAPI.CallConsoleNameGame(idGame);
                actualGame = callAPI.CallOneGame(idGame);
                gameTimer = new GameTimer(actualGame, this);
                user.MoveFileFromUserFolderToEmulatorFolder();
                switch (console)
                {
                    case "Nintendo Gamecube":
                        actualEmulator = dolphin;
                        dolphin.SetConfiguration(fullScreen, definition, formatSeizeNeuvieme, filtrageAnioscopique);
                        dolphin.Execute(idGame);
                        break;
                    case "Playstation 2":
                        actualEmulator = PCSX2;
                        PCSX2.SetConfiguration(fullScreen, definition, formatSeizeNeuvieme, filtrageAnioscopique);
                        PCSX2.Execute(idGame);
                        break;
                    case "Wii":
                        actualEmulator = dolphin;
                        dolphin.SetConfiguration(fullScreen, definition, formatSeizeNeuvieme, filtrageAnioscopique);
                        dolphin.Execute(idGame);
                        break;
                    default:
                        break;
                }
                EmulatorState = Etatenum.start;
            }

        }
        /// <summary>
        /// Close the current game
        /// </summary>
        internal void CloseGame()
        {
            if (actualEmulator != null)
            {
                actualEmulator.Close();
                EmulatorState = Etatenum.stop;
                actualEmulator = null;
            }

        }

        /// <summary>
        /// Scan les processus en cours sur le pc pour savoir si ils ont été fermé ou non
        /// Scan the emulator process to know if a emulators is started or not
        /// </summary>
        private void ScanEmulatorProcess(object sender, EventArgs e)
        {
            if (EmulatorState == Etatenum.start)
            {
                if (actualEmulator.GetEmulatorProcessLife())
                {
                    EmulatorState = Etatenum.stop;
                    actualGame = null;
                    xboxMainForm.Refresh();
                }
            }
        }
        /// <summary>
        /// Get the configuration save in the config file to applied it to the current Caiman
        /// </summary>
        public void ScanConfiguration()
        {
            fullScreen = Convert.ToBoolean(configFile.ReadProperties("fullscreen"));
            definition = Convert.ToInt32(configFile.ReadProperties("definition"));
            formatSeizeNeuvieme = Convert.ToBoolean(configFile.ReadProperties("formatSeizeNeuvieme"));
            filtrageAnioscopique = Convert.ToInt32(configFile.ReadProperties("filtrageAnioscopique"));
        }
        /// <summary>
        /// Applied the configuration depend on the choice of the user
        /// </summary>
        /// <param name="configuration"></param>
        public void ApplyGlobalConfiguration(string configuration)
        {
            switch (configuration)
            {
                case "original":
                    configFile.UpdateProperties("definition", "1");
                    configFile.UpdateProperties("filtrageAnioscopique", "1");
                    configFile.UpdateProperties("configuration", "original");
                    ScanConfiguration();
                    break;
                case "1080":
                    configFile.UpdateProperties("definition", "3");
                    configFile.UpdateProperties("filtrageAnioscopique", "4");
                    configFile.UpdateProperties("configuration", "1080p");
                    ScanConfiguration();
                    break;
                case "4K":
                    configFile.UpdateProperties("definition", "8");
                    configFile.UpdateProperties("filtrageAnioscopique", "4");
                    configFile.UpdateProperties("configuration", "4K");
                    ScanConfiguration();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Write the configuration of the fullscreen param
        /// </summary>
        /// <param name="fullscreen"></param>
        public void ApplyFullscreenConfiguration(int fullscreen)
        {
            if (fullscreen == 1)
            {
                configFile.UpdateProperties("fullscreen", "true");
            }
            else {
                configFile.UpdateProperties("fullscreen", "false");
            }
            ScanConfiguration();
        }
        /// <summary>
        /// Write the configuration of the format param
        /// </summary>
        /// <param name="format"></param>
        public void ApplyFormatConfiguration(int format)
        {
            if (format == 1)
            {
                configFile.UpdateProperties("formatSeizeNeuvieme", "true");
            }
            else
            {
                configFile.UpdateProperties("formatSeizeNeuvieme", "false");
            }
            ScanConfiguration();
        }
    }
}
