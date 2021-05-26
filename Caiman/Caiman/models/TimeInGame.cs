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
                int minutes = this.minutes % 60;
                if (minutes == 60)
                {
                    hours++;
                    minutes = 0;
                }
                string minutesString = "";
                string hoursString = "";
                if (minutes <10)
                {
                    minutesString += "0" + minutes;
                }
                if (hours < 10)
                {
                    hoursString += "0" + hours;
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
