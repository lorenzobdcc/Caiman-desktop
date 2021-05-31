/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class to manage PCSX2
 */
using Caiman.database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    public class PCSX2 : Emulator
    {
        const string EXE_NAME = @"pcsx2.exe";
        const string PATH_FOLDER_CONFIG_FILE_EMULATOR = @"..\..\emulators\PCSX2\inis\";
        const string PROCESS_NAME = "pcsx2";
        private const string PCSX2_EXE_FOLDER = @"\..\..\..\emulators\PCSX2\";
        private CallAPI callAPI = new CallAPI();


        //fichiers de configuration de pcsx2
        private ConfigFileEditor configFileGSdx;
        private ConfigFileEditor configFilePCSX2_ui;
        string PCSX2Folder = "";

        /// <summary>
        /// Base constructor
        /// </summary>
        public PCSX2()
        {

            PCSX2Folder = AppDomain.CurrentDomain.BaseDirectory;
            configFileGSdx = new ConfigFileEditor(PCSX2Folder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "GSdx.ini");
            configFilePCSX2_ui = new ConfigFileEditor(PCSX2Folder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "PCSX2_ui.ini");

            PCSX2Folder += PCSX2_EXE_FOLDER;

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
                //param pour ne pas afficher l'interface graphique --portable --nogui
                string param = " --nogui --portable";

                processEmulator = Process.Start(PCSX2Folder + EXE_NAME, path + filename + param);

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
            configFileGSdx.UpdateProperties("upscale_multiplier", definition.ToString());
            configFileGSdx.UpdateProperties("MaxAnisotropy", filtrageAnioscopique.ToString());

            // the format is not true or false so i have to format it
            if (fullScreen)
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "enabled");
            }
            else
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "disabled");
            }

            // the format is not true or false so i have to format it 
            if (formatSeizeNeuvieme)
            {
                configFilePCSX2_ui.UpdateProperties("AspectRatio", "16:9");
            }
            else
            {
                configFilePCSX2_ui.UpdateProperties("AspectRatio", "4:3");
            }
        }

    }
}
