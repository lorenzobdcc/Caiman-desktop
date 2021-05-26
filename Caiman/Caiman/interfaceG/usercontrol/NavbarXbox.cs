/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Navbar panel
 */
using System;
using System.Collections.Generic;
using System.Drawing;
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
            int first_position = Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.Bounds.Width / 4;

            Label lbl_configuration_actual = new Label();
            lbl_configuration_actual.Text = xboxMainForm.emulatorsManager.user.username;
            lbl_configuration_actual.Location = new System.Drawing.Point((first_position -150) , OFFSET+3);
            lbl_configuration_actual.Width = 250;
            lbl_configuration_actual.Font = new Font("Arial", 18);
            lbl_configuration_actual.Anchor = AnchorStyles.Right;
            lbl_configuration_actual.ForeColor = Color.White;
            Controls.Add(lbl_configuration_actual);

            XboxNavabrButton home = new XboxNavabrButton("home", Caiman.Properties.Resources.green_home, 0, 0, 0);
            home.Location = new System.Drawing.Point((first_position + 100),  OFFSET);
            Controls.Add(home);
            home.Click += new System.EventHandler(bouton_Click);

            XboxNavabrButton configuration = new XboxNavabrButton("configurationMenu", Caiman.Properties.Resources.green_gear, 0, 0, 1);
            configuration.Location = new System.Drawing.Point((first_position + 150),  OFFSET);
            Controls.Add(configuration);
            configuration.Click += new System.EventHandler(bouton_Click);

            XboxNavabrButton download = new XboxNavabrButton("downloadList", Caiman.Properties.Resources.download, 0, 0, 2);
            download.Location = new System.Drawing.Point((first_position + 200),  OFFSET);
            Controls.Add(download);
            download.Click += new System.EventHandler(bouton_Click);

            XboxNavabrButton quit = new XboxNavabrButton("quitMenu", Caiman.Properties.Resources.green_onoff, 0, 0, 3);
            quit.Location = new System.Drawing.Point((first_position + 250),  OFFSET);
            Controls.Add(quit);
            quit.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(home);
            lstControls[0].Add(configuration);
            lstControls[0].Add(download);
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
