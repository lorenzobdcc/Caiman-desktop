/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to interact with the emulators Dolphin
 */
using Caiman.database;
using Caiman.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caiman.logique
{
    public class GameTimer
    {
        Timer timer = new Timer();
        EmulatorsManager emulatorsManager;
        CallAPI callAPI = new CallAPI();
        public int minutes = 0;
        public int counter = 0;
        Game game;

        public GameTimer(Game gamep, EmulatorsManager emulatorsManagerp) : base()
        {
            game = gamep;
            emulatorsManager = emulatorsManagerp;
            InitTimer();
        }
        public GameTimer()
        {

        }

        /// <summary>
        /// Initialise a timer who is gonna call the function used to upade tehe interface and scan the user input
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(UpdateTimer);

            timer.Interval = 1000;
            timer.Start();
        }



        /// <summary>
        /// Update the timer and if the number of secondes excess 60 add a minute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateTimer(object sender, EventArgs e)
        {
            counter++;

            if (counter == 60)
            {
                minutes++;
                counter = 0;

                callAPI.AddOneMinuteToGame(game.id, emulatorsManager.user.id);
            }

        }
        /// <summary>
        /// Get the time in the format 00h00m
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string time = "";
            int minutes = this.minutes ;
            int secondes = counter;

            string secondesString = secondes.ToString();
            string minutesString = minutes.ToString();
            if (secondes < 10)
            {
                secondesString = "0" + secondes;
            }
            if (minutes < 10)
            {
                minutesString = "0" + minutes;
            }
            time = minutesString + "m" + secondesString;
            return time;
        }


    }
}
