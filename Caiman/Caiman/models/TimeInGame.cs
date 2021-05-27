using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models
{
    public class TimeInGame
    {
        public int minutes = 0;

        public string TimeHoursMinutes
        {
            get
            {
                string time = "";
                int hours = this.minutes / 60;
                int minutesInt = this.minutes % 60;
                if (minutesInt == 60)
                {
                    hours++;
                    minutesInt = 0;
                }
                string minutesString = minutesInt.ToString();
                string hoursString = "";
                if (minutesInt < 10)
                {
                    minutesString = "0" + minutesInt;
                }
                if (hours < 10)
                {
                    hoursString = "0" + hours;
                }
                time = hoursString + "h" + minutesString;
                return time;
            }
        }
        public TimeInGame(int minutesp)
        {
            minutes = minutesp;
        }
        public TimeInGame()
        {

        }
    }
}
