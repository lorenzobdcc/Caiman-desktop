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
        int minutes = 0;
        int counter = 0;
        Game game;

        public GameTimer(Game gamep) :base()
        {
            game = gamep;
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
            }

        }
        public string TimeInGame()
        {
            return this.minutes + ":" + counter;
        }


        }
}
