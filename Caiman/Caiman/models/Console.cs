/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Model for console
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models
{
    class ConsoleModel
    {
        public int id;
        public string name;
        public string folderName;
        public int idEmulator;

        public ConsoleModel(int idp, string namep, string folderNamep, int idEmulatorp)
        {
            id = idp;
            name = namep;
            folderName = folderNamep;
            idEmulator = idEmulatorp;
        }

        public ConsoleModel()
        {

        }
    }
}
