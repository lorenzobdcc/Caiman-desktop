/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to interact with the emulators Dolphin
 */
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
        private const string DOLPHIN_PATH = @"\..\..\..\emulators\Dolphin\";
        private CallAPI callAPI = new CallAPI();
        string dolphinFolder = "";


        private ConfigFileEditor configFileDolphin;
        private ConfigFileEditor configFileGFX;


        /// <summary>
        /// default contructor
        /// </summary>
        public Dolphin()
        {
            dolphinFolder = AppDomain.CurrentDomain.BaseDirectory;
            configFileDolphin = new ConfigFileEditor(dolphinFolder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "Dolphin.ini");
            configFileGFX = new ConfigFileEditor(dolphinFolder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "GFX.ini"); ;


                dolphinFolder += DOLPHIN_PATH;

        }

        /// <summary>
        /// start the execution of the game witout the GUI
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
        /// Used to applied the configuration to the configuration file of Dolphin
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
