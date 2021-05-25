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
        private CallAPI callAPI = new CallAPI();


        //fichiers de configuration de pcsx2
        private ConfigFileEditor configFileGSdx;
        private ConfigFileEditor configFilePCSX2_ui;
        string PCSX2Folder = "";

        /// <summary>
        /// le constructeur n'a pas besoin de paramètre
        /// </summary>
        public PCSX2()
        {

            PCSX2Folder = AppDomain.CurrentDomain.BaseDirectory;
            configFileGSdx = new ConfigFileEditor(PCSX2Folder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "GSdx.ini");
            configFilePCSX2_ui = new ConfigFileEditor(PCSX2Folder + PATH_FOLDER_CONFIG_FILE_EMULATOR, "PCSX2_ui.ini");

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                PCSX2Folder += @"\..\..\..\emulators\PCSX2\";
            }
            else
            {
                //PCSX2Folder += @"\emulators\PCSX2\";
                PCSX2Folder += @"\..\..\..\emulators\PCSX2\";
            }


        }


        /// <summary>
        /// lance l'éxécution d'un jeu pour éxécuter un jeu de lance le .exe de pcsx2 avec en paramétre le fichier iso du jeu qui doit etre éxécuté,
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
                //param pour ne pas afficher l'interface graphique --portable --nogui
                string param = " --nogui --portable";

                processEmulator = Process.Start(PCSX2Folder+EXE_NAME, path + filename + param );

            }
            else
            {
                Close();
                Execute(idGame);
            }
        }

        /// <summary>
        /// met a jour les fichiers de config de pcsx2 avant l'éxécution d'un jeu 
        /// les fichier sont mis a jour celon le fichier personnel de l'utilisateur
        /// </summary>
        public override void UpdateConfigurationFile()
        {
            configFileGSdx.UpdateProperties("upscale_multiplier", definition.ToString());
            configFileGSdx.UpdateProperties("MaxAnisotropy", filtrageAnioscopique.ToString());

            // le format n'est pas booleen dans le fichier de configuration de PCSX2 alors je dois le convertir
            if (fullScreen)
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "enabled");
            }
            else
            {
                configFilePCSX2_ui.UpdateProperties("DefaultToFullscreen", "disabled");
            }

            // le format n'est pas booleen dans le fichier de configuration de PCSX2 alors je dois le convertir
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
