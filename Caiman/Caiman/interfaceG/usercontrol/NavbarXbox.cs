/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Navbar panel
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class NavbarXbox : XboxUserControl
    {

        const int HEIGHT = 60;
        const int OFFSET = 15;
        public NavbarXbox(XboxMainForm xboxMain) : base(xboxMain)
        {
            Height = HEIGHT;
            Width = Screen.PrimaryScreen.Bounds.Width;
            CreateNavButton();
        }

        public void CreateNavButton()
        {
            lstControls.Add(new List<Control>());

            XboxNavabrButton home = new XboxNavabrButton("home", Caiman.Properties.Resources.green_home, 0, 0, 1);
            home.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - 200), 0 * 60 + OFFSET);
            Controls.Add(home);
            home.Click += new System.EventHandler(bouton_Click);

            XboxNavabrButton configuration = new XboxNavabrButton("configurationMenu", Caiman.Properties.Resources.green_gear, 0, 0, 1);
            configuration.Location = new System.Drawing.Point( (Screen.PrimaryScreen.Bounds.Width -150) , 0 * 60 + OFFSET);
            Controls.Add(configuration);
            configuration.Click += new System.EventHandler(bouton_Click);

            XboxNavabrButton quit = new XboxNavabrButton("quitMenu", Caiman.Properties.Resources.green_onoff, 0, 0, 2);
            quit.Location = new System.Drawing.Point((Screen.PrimaryScreen.Bounds.Width - 100), 0 * 60 + OFFSET);
            Controls.Add(quit);
            quit.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(home);
            lstControls[0].Add(configuration);
            lstControls[0].Add(quit);


        }
        private new void bouton_Click(object sender, EventArgs e)
        {
            XboxNavabrButton tempXboxButton = (XboxNavabrButton)sender;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }
    }
}
