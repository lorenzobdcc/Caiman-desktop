/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to specify what the applcation need to load
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.interfaceG
{
    public class ContextInformations
    {
        public string contexte;
        public int id_contexte;
        public string optionalString1;
        public string optionalString2;
        public int optionalInt1;

        public int position_y;
        public int position_x;

        /// <summary>
        /// default contructor
        /// </summary>
        public ContextInformations()
        {

        }

        /// <summary>
        /// contructor where you specified basic informations like which main panel need to be load and an id to specify what need to be load
        /// </summary>
        /// <param name="contextep"></param>
        /// <param name="id_contextep"></param>
        /// <param name="position_y_p"></param>
        /// <param name="position_x_p"></param>
        public ContextInformations(string contextep, int id_contextep, int position_y_p, int position_x_p)
        {
            contexte = contextep;
            id_contexte = id_contextep;
            position_x = position_x_p;
            position_y = position_y_p;

        }
    }


}
