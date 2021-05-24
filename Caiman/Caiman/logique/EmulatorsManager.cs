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

        public EmulatorsManager()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            user = new User();
            downloadManager = new DownloadManager(this);
            
            CreateAppDataFolder();
            gamesListConfigFile = new ConfigFileEditor(gamesPath,"games.ini") ;
            CheckIfGameFileIsPresentOnDisk();
        }

        private void CreateAppDataFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var basePath = Path.Combine(appDataPath, @"Caiman\");
            var caimanConfigPath = Path.Combine(appDataPath, @"Caiman\Caiman\");
            var imgPath = Path.Combine(appDataPath, @"Caiman\img\");
            var gamesPath = Path.Combine(appDataPath, @"Caiman\Caiman\games.ini");
            var configPath = Path.Combine(appDataPath, @"Caiman\Caiman\config.ini");
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
            if (!File.Exists(gamesPath))
            {
                File.Create(gamesPath);
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
    }
}
