using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.interfaceG
{
    public class ButtonContext
    {
        public string contexte;
        public int id_contexte;

        public int position_y;
        public int position_x;

        public ButtonContext()
        {

        }


        public ButtonContext(string contextep, int id_contextep, int position_y_p, int position_x_p)
        {
            contexte = contextep;
            id_contexte = id_contextep;
            position_x = position_x_p;
            position_y = position_y_p;

        }
    }


}
