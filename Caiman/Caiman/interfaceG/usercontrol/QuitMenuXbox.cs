/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to test if i can load an image from the web
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
        private System.Windows.Forms.Label label1;


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
            this.Controls.Add(this.label1);
            this.Name = "testContextUC";
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width -200,Screen.PrimaryScreen.Bounds.Height-200);
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void CreateNavButton()
        {
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());

            int position_width = ((Screen.PrimaryScreen.Bounds.Width / 2) - 185);

            XboxButton minimize = new XboxButton("minimize", 0, 0, 0);
            minimize.Text = "Minimise";
            minimize.Width = 100;
            minimize.Location = new System.Drawing.Point(position_width,( (Screen.PrimaryScreen.Bounds.Height/2) -300));
            Controls.Add(minimize);
            minimize.Click += new System.EventHandler(bouton_Click);

            XboxButton quit = new XboxButton("quit", 0, 1, 0);
            quit.Text = "Quit";
            quit.Width = 100;
            quit.Location = new System.Drawing.Point(position_width, ((Screen.PrimaryScreen.Bounds.Height / 2) - 200));
            Controls.Add(quit);
            quit.Click += new System.EventHandler(bouton_Click);

            XboxButton logOut = new XboxButton("logOut", 0, 2, 0);
            logOut.Text = "LogOut";
            logOut.Width = 100;
            logOut.Location = new System.Drawing.Point(position_width, ((Screen.PrimaryScreen.Bounds.Height / 2) - 100));
            Controls.Add(logOut);
            logOut.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(minimize);
            lstControls[1].Add(quit);
            lstControls[2].Add(logOut);


        }
        private void bouton_Click(object sender, EventArgs e)
        {
            XboxButton tempXboxButton = (XboxButton)sender;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }
    }
}
