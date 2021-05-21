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

        public EmulatorsManager()
        {
            user = new User();
            downloadManager = new DownloadManager();
            CreateAppDataFolder();
        }

        private void CreateAppDataFolder()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var basePath = Path.Combine(appDataPath, @"Caiman\");
            var imgPath = Path.Combine(appDataPath, @"Caiman\img\");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (!Directory.Exists(imgPath))
            {
                Directory.CreateDirectory(imgPath);
            }

            if (!Directory.Exists(@"C:\Caiman\Playstation2"))
            {
                Directory.CreateDirectory(@"C:\Caiman\Playstation2\");
            }
            if (!Directory.Exists(@"C:\Caiman\GamecubeWii\"))
            {
                Directory.CreateDirectory(@"C:\Caiman\GamecubeWii\");
            }
        }
    }
}
