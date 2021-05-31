/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Model for category
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.models
{
    public class Category
    {
        public int id;
        public string name;
        public Category(int idp,string namep)
        {
            id = idp;
            name = namep;
        }
        public Category()
        {

        }
    }
}
