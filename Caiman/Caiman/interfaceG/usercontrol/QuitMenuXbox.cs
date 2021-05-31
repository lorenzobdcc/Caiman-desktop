/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display the quit menu
 */
using Caiman.interfaceG.XboxControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class QuitMenuXbox : XboxUserControl
    {


        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public QuitMenuXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
            CreateNavButton();
        }

        /// <summary>
        /// Initialise the panel
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // 
            // testContextUC
            // 
            this.Name = "testContextUC";
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width -200,Screen.PrimaryScreen.Bounds.Height-200);
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        /// <summary>
        /// Create the log out and the quit button
        /// </summary>
        public void CreateNavButton()
        {
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());

            int position_width = ((Screen.PrimaryScreen.Bounds.Width / 2) - 200);

            XboxButton quit = new XboxButton("quit", 0, 0, 0);
            quit.Text = "Close Caiman";
            quit.Width = 100;
            quit.Location = new System.Drawing.Point(position_width, ((Screen.PrimaryScreen.Bounds.Height / 2) - 200));
            Controls.Add(quit);
            quit.Click += new System.EventHandler(bouton_Click);

            XboxButton logOut = new XboxButton("logOut", 0, 1, 0);
            logOut.Text = "Log out and quit Caiman";
            logOut.Width = 100;
            logOut.Location = new System.Drawing.Point(position_width, ((Screen.PrimaryScreen.Bounds.Height / 2) - 100));
            Controls.Add(logOut);
            logOut.Click += new System.EventHandler(bouton_Click);


            lstControls[1].Add(quit);
            lstControls[0].Add(logOut);

        }

    }
}
