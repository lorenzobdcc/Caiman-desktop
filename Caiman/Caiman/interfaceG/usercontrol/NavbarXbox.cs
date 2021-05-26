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
        public string actualGameName = "";
        Label lbl_game_actual = new Label();
        Timer timer = new Timer();

        /// <summary>
        /// appel diférentes fonctions a un interval régulier
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(UpdateData);
            timer.Interval = 100;
            timer.Start();
        }
        private void UpdateData(object sender, EventArgs e)
        {
            if (actualGameName != "")
            {
                lbl_game_actual.Text = " Now playing: " + actualGameName + "  "+ xboxMainForm.emulatorsManager.gameTimer.TimeInGame();
            }
            else
            {
                lbl_game_actual.Text = "";
            }
        }
        public NavbarXbox(XboxMainForm xboxMain) : base(xboxMain)
        {
            Height = HEIGHT;
            Width = Screen.PrimaryScreen.Bounds.Width;
            CreateNavButton();
            InitTimer();
        }

        public void CreateNavButton()
        {
            lstControls.Add(new List<Control>());
            int first_position = Screen.PrimaryScreen.Bounds.Width - 350;

            Label lbl_configuration_actual = new Label();
            lbl_configuration_actual.Text = xboxMainForm.emulatorsManager.user.username;
            lbl_configuration_actual.Location = new System.Drawing.Point((first_position -150) , OFFSET+3);
            lbl_configuration_actual.AutoSize = true;
            lbl_configuration_actual.Height = 40;
            lbl_configuration_actual.Font = new Font("Arial", 18);
            lbl_configuration_actual.Anchor = AnchorStyles.Right;
            lbl_configuration_actual.ForeColor = Color.White;
            Controls.Add(lbl_configuration_actual);

            lbl_game_actual = new Label();
            lbl_game_actual.Text = actualGameName;
            lbl_game_actual.Location = new System.Drawing.Point(20, OFFSET + 3);
            lbl_game_actual.AutoSize = true;
            lbl_game_actual.Height = 40;
            lbl_game_actual.Font = new Font("Arial", 18);
            lbl_game_actual.Anchor = AnchorStyles.Right;
            lbl_game_actual.ForeColor = Color.White;
            Controls.Add(lbl_game_actual);

            XboxNavbarButton home = new XboxNavbarButton("home", Caiman.Properties.Resources.green_home, 0, 0, 0);
            home.Location = new System.Drawing.Point((first_position + 50),  OFFSET);
            Controls.Add(home);
            home.Click += new System.EventHandler(bouton_Click);

            XboxNavbarButton configuration = new XboxNavbarButton("configurationMenu", Caiman.Properties.Resources.green_gear, 0, 0, 1);
            configuration.Location = new System.Drawing.Point((first_position + 100),  OFFSET);
            Controls.Add(configuration);
            configuration.Click += new System.EventHandler(bouton_Click);

            XboxNavbarButton download = new XboxNavbarButton("downloadList", Caiman.Properties.Resources.download, 0, 0, 2);
            download.Location = new System.Drawing.Point((first_position + 150),  OFFSET);
            Controls.Add(download);
            download.Click += new System.EventHandler(bouton_Click);

            XboxNavbarButton minimize = new XboxNavbarButton("minimize", Caiman.Properties.Resources.minimize, 0, 0, 4);
            minimize.Location = new System.Drawing.Point((first_position + 250), OFFSET);
            Controls.Add(minimize);
            minimize.Click += new System.EventHandler(bouton_Click);

            XboxNavbarButton quit = new XboxNavbarButton("quitMenu", Caiman.Properties.Resources.green_onoff, 0, 0, 3);
            quit.Location = new System.Drawing.Point((first_position + 300),  OFFSET);
            Controls.Add(quit);
            quit.Click += new System.EventHandler(bouton_Click);




            lstControls[0].Add(home);
            lstControls[0].Add(configuration);
            lstControls[0].Add(download);
            lstControls[0].Add(minimize);
            lstControls[0].Add(quit);

        }
        private new void bouton_Click(object sender, EventArgs e)
        {
            XboxNavbarButton tempXboxButton = (XboxNavbarButton)sender;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }
    }
}
