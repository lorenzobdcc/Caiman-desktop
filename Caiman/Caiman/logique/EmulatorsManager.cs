using Caiman.database;
using Caiman.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    public class EmulatorsManager
    {

        public User user;
        public DownloadManager downloadManager;
        public ConfigFileEditor gamesListConfigFile;
        private CallAPI callAPI = new CallAPI();

        private PCSX2 PCSX2 = new PCSX2();
        private Dolphin dolphin = new Dolphin();

        public ConfigFileEditor configFile;

        public bool fullScreen;
        public int definition;
        public bool formatSeizeNeuvieme;
        public bool noGui;
        public int filtrageAnioscopique;


        public EmulatorsManager()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            user = new User();
            CreateAppDataFolder();
            downloadManager = new DownloadManager(this);
            
            
            gamesListConfigFile = new ConfigFileEditor(gamesPath,"games.ini") ;
            configFile = new ConfigFileEditor(gamesPath, "config.ini");
            CheckIfGameFileIsPresentOnDisk();
            ScanConfiguration();
        }

        private void CreateAppDataFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var basePath = Path.Combine(appDataPath, @"Caiman\");
            var caimanConfigPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            var imgPath = Path.Combine(appDataPath, @"Caiman\img\");
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\games.ini");
            var configPath = Path.Combine(appDataPath, @"Caiman\Caiman\config.ini");
            if (!File.Exists(gamesPath))
            {
                File.Create(gamesPath);
            }
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
            if (!File.Exists(configPath))
            {
                File.Create(configPath);
            }

        }

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

        public void StartGame(int idGame)
        {
            string console = callAPI.CallConsoleNameGame(idGame);

            switch (console)
            {
                case "Nintendo Gamecube":
                    dolphin.SetConfiguration(fullScreen,definition,formatSeizeNeuvieme,filtrageAnioscopique);
                    dolphin.Execute(idGame);
                    break;
                case "Playstation 2":
                    PCSX2.SetConfiguration(fullScreen, definition, formatSeizeNeuvieme, filtrageAnioscopique);
                    PCSX2.Execute(idGame);
                    break;
                case "Wii":
                    dolphin.SetConfiguration(fullScreen, definition, formatSeizeNeuvieme, filtrageAnioscopique);
                    dolphin.Execute(idGame);
                    break;
                default:
                    break;
            }

        }
        public void ScanConfiguration()
        {
            fullScreen = Convert.ToBoolean(configFile.ReadProperties("fullscreen"));
            definition = Convert.ToInt32(configFile.ReadProperties("definition"));
            noGui = Convert.ToBoolean(configFile.ReadProperties("gui"));
            formatSeizeNeuvieme = Convert.ToBoolean(configFile.ReadProperties("formatSeizeNeuvieme"));
            filtrageAnioscopique = Convert.ToInt32(configFile.ReadProperties("filtrageAnioscopique"));
        }

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
                    configFile.UpdateProperties("definition", "4");
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
