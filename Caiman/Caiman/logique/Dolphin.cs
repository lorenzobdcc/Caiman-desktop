using Caiman.database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    class Dolphin : Emulator
    {
        const string EXE_NAME = @"Dolphin.exe";
        const string PROCESS_NAME = "Dolphin";
        const string PATH_FOLDER_CONFIG_FILE_EMULATOR = @"..\..\emulators\Dolphin\User\Config\";
        private CallAPI callAPI = new CallAPI();
        string dolphinFolder = "";



        //fichiers de configuration de Dolphin
        private ConfigFileEditor configFileDolphin;
        private ConfigFileEditor configFileGFX;


        /// <summary>
        /// le constructeur n'a pas besoin de paramètre
        /// </summary>
        public Dolphin()
        {
            dolphinFolder = AppDomain.CurrentDomain.BaseDirectory;
            configFileDolphin = new ConfigFileEditor(dolphinFolder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "Dolphin.ini");
            configFileGFX = new ConfigFileEditor(dolphinFolder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "GFX.ini"); ;

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                dolphinFolder += @"\..\..\..\emulators\Dolphin\";
            }
            else
            {
                //dolphinFolder += @"emulators\Dolphin\";

                dolphinFolder += @"\..\..\..\emulators\Dolphin\";
            }

        }

        /// <summary>
        /// Lance l'éxécution d'un jeu grace a une ligne de commande pour l'émulateur
        /// certain paramètres sont appliqué si besoin
        /// </summary>
        public override void Execute(int idGame)
        {
            string path = @"C:\Caiman\" + callAPI.CallFolderNameGame(idGame) + @"\";
            string filename = callAPI.CallFileNameGame(idGame);
            
            UpdateConfigurationFile();
            int process = Process.GetProcessesByName(PROCESS_NAME).Length;

            if (process == 0)
            {
                //param pour ne pas mettre de gui et en fullscreen --portable
                string param = " --batch";

                processEmulator = Process.Start(dolphinFolder + EXE_NAME, param + " --exec \"" + path + filename);

            }
            else
            {
                Close();
                Execute(idGame);
            }
        }

        /// <summary>
        /// Sert a mettre a jour les fichiers de configurations de Dolphin pour appliquer les paramètres global de l'application
        /// </summary>
        public override void UpdateConfigurationFile()
        {
            configFileDolphin.UpdateProperties("Fullscreen", fullScreen.ToString());
            configFileGFX.UpdateProperties("InternalResolution", definition.ToString());
            configFileGFX.UpdateProperties("MaxAnisotropy", filtrageAnioscopique.ToString());

            if (formatSeizeNeuvieme)
            {
                configFileGFX.UpdateProperties("AspectRatio", "1");
            }
            else
            {
                configFileGFX.UpdateProperties("AspectRatio", "2");
            }
            
        }

    }
}
