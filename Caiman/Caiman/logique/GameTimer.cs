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
        int minutes = 0;
        int counter = 0;
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




        public void UpdateTimer(object sender, EventArgs e)
        {
            counter++;

            if (counter >= 60)
            {
                minutes++;
                counter = 0;

                callAPI.AddOneMinuteToGame(game.id, emulatorsManager.user.id);
            }

        }
        public string TimeInGame()
        {
            string time = "";
            int hours = this.minutes ;
            int minutesInt = this.counter % 60;
            if (minutesInt == 60)
            {
                hours++;
                minutesInt = 0;
            }
            string minutesString = minutesInt.ToString();
            string hoursString = hours.ToString();
            if (minutesInt < 10)
            {
                minutesString = "0" + minutesInt;
            }
            if (hours < 10)
            {
                hoursString = "0" + hours;
            }
            time = hoursString + "m" + minutesString;
            return time;
        }


    }
}
