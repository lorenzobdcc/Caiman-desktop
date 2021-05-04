using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.interfaceG.usercontrol
{
    class testContextUC : XboxUserControl
    {
        public testContextUC(XboxMainForm xboxMain) : base(xboxMain)
        {
        }

        public testContextUC(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
        }


    }
}
