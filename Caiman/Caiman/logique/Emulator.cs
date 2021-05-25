using Caiman.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.logique
{
    public abstract class Emulator
    {
        public Game actualGame;
        public Process processEmulator;

        // variables d'affichage graphique
        protected bool fullScreen;
        protected int definition;
        protected bool formatSeizeNeuvieme;
        protected bool noGui;
        protected int filtrageAnioscopique;


        public Emulator()
        {

        }

        /// <summary>
        /// tue le processus de l'émulateur si un émulateur est lancé.
        /// </summary>
        public void Close()
        {
            if (Process.GetProcessesByName(processEmulator.ProcessName).Length != 0)
            {
                processEmulator.Kill();
            }
        }



        /// <summary>
        /// charge la configuration global dans les variables de l'émulateur
        /// </summary>
        /// <param name="fullscreenp"></param>
        /// <param name="definitionp"></param>
        /// <param name="noGuip"></param>
        /// <param name="formatSeizeNeuviemmep"></param>
        /// <param name="filtragep"></param>
        public void SetConfiguration(bool fullscreenp, int definitionp, bool formatSeizeNeuviemmep, int filtragep)
        {
            fullScreen = fullscreenp;
            definition = definitionp;
            formatSeizeNeuvieme = formatSeizeNeuviemmep;
            filtrageAnioscopique = filtragep;
        }



        /// <summary>
        /// retourne l'information si le processus lancé précedament est toujours en cours
        /// </summary>
        /// <returns></returns>
        public bool GetEmulatorProcessLife()
        {
            return processEmulator.HasExited;
        }

        public abstract void Execute(int idGame);
        public abstract void UpdateConfigurationFile();
    }
}
