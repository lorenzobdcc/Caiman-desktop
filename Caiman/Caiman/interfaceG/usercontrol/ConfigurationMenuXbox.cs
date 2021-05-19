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
    class ConfigurationMenuXbox : XboxUserControl
    {


        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public ConfigurationMenuXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
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

        public void CreateNavButton()
        {
            int postion_first_column = ((Screen.PrimaryScreen.Bounds.Width/ 12 ));


            Label lbl_configuration = new Label();
            lbl_configuration.Text = "Configuration";
            lbl_configuration.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 400));
            lbl_configuration.Width = 150;
            lbl_configuration.Font = new Font("Arial", 14);
            lbl_configuration.ForeColor = Color.White;
            Controls.Add(lbl_configuration);

            Label lbl_configuration_actual = new Label();
            lbl_configuration_actual.Text = "Actual: none";
            lbl_configuration_actual.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 350));
            lbl_configuration_actual.Width = 150;
            lbl_configuration_actual.Font = new Font("Arial", 14);
            lbl_configuration_actual.ForeColor = Color.White;
            Controls.Add(lbl_configuration_actual);

            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());


            XboxButton original = new XboxButton("updateConfig", 0, 0, 0);
            original.Text = "Original";
            original.Width = 100;
            original.Location = new System.Drawing.Point(postion_first_column,( (Screen.PrimaryScreen.Bounds.Height/2) -300));
            Controls.Add(original);
            original.Click += new System.EventHandler(bouton_Click);

            XboxButton fullHD = new XboxButton("updateConfig", 1, 1, 0);
            fullHD.Text = "1080P";
            fullHD.Width = 100;
            fullHD.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 250));
            Controls.Add(fullHD);
            fullHD.Click += new System.EventHandler(bouton_Click);

            XboxButton ultraHD = new XboxButton("updateConfig", 2, 2, 0);
            ultraHD.Text = "4K";
            ultraHD.Width = 100;
            ultraHD.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 200));
            Controls.Add(ultraHD);
            ultraHD.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(original);
            lstControls[1].Add(fullHD);
            lstControls[2].Add(ultraHD);


        }

    }
}
