/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display all the download
 */
using Caiman.interfaceG.XboxControl;
using Caiman.logique;
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
    class DownloadListXbox : XboxUserControl
    {
        Timer timer;
        List<ProgressBar> lst_progressBar = new List<ProgressBar>();

        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public DownloadListXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
            CreateListDownload();
            InitTimer();
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
        /// Start the timer to refres the data
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(RefreshData);
            timer.Interval = 10;
            timer.Start();
        }
        /// <summary>
        /// Refresh the percentage of each download and show them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshData(object sender = null, EventArgs e = null)
        {
            int counter = 0;
            List<Download> lst_allDownload = new List<Download>();
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_download);
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_activeDownload);
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_finishDownload);
            if (xboxMainForm.emulatorsManager.downloadManager.lst_activeDownload.Count() != 0)
            {
            foreach (var control in lst_progressBar)
            {
                lst_progressBar[counter].Value = lst_allDownload[counter].percentage;
                    if (lst_allDownload[counter].percentage >= 99)
                    {
                        lstControls[counter][0].Text = lst_allDownload[counter].filename + " : Finish";
                        lst_progressBar[counter].Value = 100;
                    }
                    else
                    {
                        lstControls[counter][0].Text = lst_allDownload[counter].filename + " :" + lst_allDownload[counter].percentage + "%";
                    }
                    counter++;
            }
            }

            this.Refresh();

        }
        /// <summary>
        /// Create list of download from all the lists of download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CreateListDownload(object sender = null, EventArgs e = null)
        {
            
                int position_width = ((Screen.PrimaryScreen.Bounds.Width / 12) - 50);
            int counterTotal = 0;
            List<Download> lst_allDownload = new List<Download>();
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_download);
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_activeDownload);
            lst_allDownload.AddRange(xboxMainForm.emulatorsManager.downloadManager.lst_finishDownload);

            if (lst_allDownload.Count() != 0)
            {
                int counterActive = 0;
                foreach (var download in lst_allDownload)
                {
                    lstControls.Add(new List<Control>());

                    
                    XboxButton btn_download = new XboxButton("game", download.idGame, 0, 0);
                    btn_download.Text = download.filename;
                    btn_download.Width = 100;
                    btn_download.Location = new System.Drawing.Point(position_width, (100 + counterTotal * 125 ));
                    Controls.Add(btn_download);
                    btn_download.Click += new System.EventHandler(bouton_Click);
                    lstControls[counterActive].Add(btn_download);

                    ProgressBar PB_download = new ProgressBar();
                        PB_download.Minimum = 0;
                        PB_download.Maximum = 100;
                    PB_download.Width = this.Width/2;
                    PB_download.Value = download.percentage;
                    PB_download.Location = new System.Drawing.Point(position_width, (150 + counterTotal * 125));
                    Controls.Add(PB_download);
                    lst_progressBar.Add(PB_download);
                    counterActive++;
                    counterTotal++;
                }
            }
            else
            {
                lstControls.Add(new List<Control>());
                XboxButton btn_download = new XboxButton("", 0, 0, 0);
                btn_download.Text = "no active download";
                btn_download.Width = 100;
                btn_download.Location = new System.Drawing.Point(position_width, ((Screen.PrimaryScreen.Bounds.Height / 2) - 300));
                Controls.Add(btn_download);
                btn_download.Click += new System.EventHandler(bouton_Click);
                lstControls[0].Add(btn_download);
            }

           



        }

        

    }
}
