/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Abstract base class for emulators
 */
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

        // graphical params
        protected bool fullScreen;
        protected int definition;
        protected bool formatSeizeNeuvieme;
        protected bool noGui;
        protected int filtrageAnioscopique;


        public Emulator()
        {

        }

        /// <summary>
        /// Close the process of the emulator
        /// </summary>
        public void Close()
        {
            try
            {
                if (Process.GetProcessesByName(processEmulator.ProcessName).Length != 0)
                {
                    if (processEmulator != null)
                    {
                        processEmulator.Kill();
                    }
                }
            }
            catch
            {
            }
            
        }



        /// <summary>
        /// Load the global configuration and applied it to the emulator config file
        /// </summary>
        /// <param name="fullscreenp"></param>
        /// <param name="definitionp"></param>
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
        /// Get the emulator process life
        /// </summary>
        /// <returns></returns>
        public bool GetEmulatorProcessLife()
        {
            return processEmulator.HasExited;
        }
        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="idGame"></param>
        public abstract void Execute(int idGame);
        /// <summary>
        /// update the configuration file of the emulator
        /// </summary>
        public abstract void UpdateConfigurationFile();
    }
}
