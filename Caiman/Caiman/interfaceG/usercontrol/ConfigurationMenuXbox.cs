/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display the configuration menu
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
        /// <summary>
        /// Create all the buttons and labels for the configuration menu
        /// </summary>
        public void CreateNavButton()
        {
            int postion_first_column = ((Screen.PrimaryScreen.Bounds.Width/ 12 ));




            Label lbl_configuration_actual = new Label();
            lbl_configuration_actual.Text = "Global configuration: "+ xboxMainForm.emulatorsManager.configFile.ReadProperties("configuration");
            lbl_configuration_actual.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 350));
            lbl_configuration_actual.Width = 300;
            lbl_configuration_actual.Font = new Font("Arial", 14);
            lbl_configuration_actual.ForeColor = Color.White;
            Controls.Add(lbl_configuration_actual);



            Label lbl_fullscreen_actual = new Label();

            if (xboxMainForm.emulatorsManager.configFile.ReadProperties("fullscreen") == "true")
            {
                lbl_fullscreen_actual.Text = "Fullscreen: On";
            }
            else
            {
                lbl_fullscreen_actual.Text = "Fullscreen: Off";
            }

            lbl_fullscreen_actual.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 150));
            lbl_fullscreen_actual.Width = 150;
            lbl_fullscreen_actual.Font = new Font("Arial", 14);
            lbl_fullscreen_actual.ForeColor = Color.White;
            Controls.Add(lbl_fullscreen_actual);


            Label lbl_format_actual = new Label();

            if (xboxMainForm.emulatorsManager.configFile.ReadProperties("formatSeizeNeuvieme") == "true")
            {
                lbl_format_actual.Text = "16/9: On";
            }
            else
            {
                lbl_format_actual.Text = "16/9: Off";
            }

            lbl_format_actual.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 50));
            lbl_format_actual.Width = 150;
            lbl_format_actual.Font = new Font("Arial", 14);
            lbl_format_actual.ForeColor = Color.White;
            Controls.Add(lbl_format_actual);

            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());


            XboxButton original = new XboxButton("updateGlobalConfiguration", 0, 0, 0);
            original.contextInfos.optionalString1 = "original";
            original.Text = "Original";
            original.Width = 100;
            original.Location = new System.Drawing.Point(postion_first_column,( (Screen.PrimaryScreen.Bounds.Height/2) -300));
            Controls.Add(original);
            original.Click += new System.EventHandler(bouton_Click);

            XboxButton fullHD = new XboxButton("updateGlobalConfiguration", 0, 1, 0);
            fullHD.contextInfos.optionalString1 = "1080";
            fullHD.Text = "1080P";
            fullHD.Width = 100;
            fullHD.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 250));
            Controls.Add(fullHD);
            fullHD.Click += new System.EventHandler(bouton_Click);

            XboxButton ultraHD = new XboxButton("updateGlobalConfiguration", 0, 2, 0);
            ultraHD.contextInfos.optionalString1 = "4K";
            ultraHD.Text = "4K";
            ultraHD.Width = 100;
            ultraHD.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 200));
            Controls.Add(ultraHD);
            ultraHD.Click += new System.EventHandler(bouton_Click);

            XboxButton fullscreenTrue = new XboxButton("updateFullscreenConfiguration", 1, 3, 0);
            fullscreenTrue.Text = "Fullscreen: On";
            fullscreenTrue.Width = 100;
            fullscreenTrue.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 100));
            Controls.Add(fullscreenTrue);
            fullscreenTrue.Click += new System.EventHandler(bouton_Click);

            XboxButton fullscreenFalse = new XboxButton("updateFullscreenConfiguration", 0, 3, 0);
            fullscreenFalse.Text = "Fullscreen: Off";
            fullscreenFalse.Width = 100;
            fullscreenFalse.Location = new System.Drawing.Point(postion_first_column + 150, ((Screen.PrimaryScreen.Bounds.Height / 2) - 100));
            Controls.Add(fullscreenFalse);
            fullscreenFalse.Click += new System.EventHandler(bouton_Click);


            lstControls.Add(new List<Control>());
            XboxButton formatTrue = new XboxButton("updateFormatConfiguration", 1, 4, 0);
            formatTrue.Text = "16/9: On";
            formatTrue.Width = 100;
            formatTrue.Location = new System.Drawing.Point(postion_first_column, ((Screen.PrimaryScreen.Bounds.Height / 2) - 0));
            Controls.Add(formatTrue);
            formatTrue.Click += new System.EventHandler(bouton_Click);

            XboxButton formatFalse = new XboxButton("updateFormatConfiguration", 0, 4, 0);
            formatFalse.Text = "16/9: Off";
            formatFalse.Width = 100;
            formatFalse.Location = new System.Drawing.Point(postion_first_column + 150, ((Screen.PrimaryScreen.Bounds.Height / 2) - 0));
            Controls.Add(formatFalse);
            formatFalse.Click += new System.EventHandler(bouton_Click);



            lstControls[0].Add(original);
            lstControls[1].Add(fullHD);
            lstControls[2].Add(ultraHD);
            lstControls[3].Add(fullscreenTrue);
            lstControls[3].Add(fullscreenFalse);
            lstControls[4].Add(formatTrue);
            lstControls[4].Add(formatFalse);



        }

    }
}
