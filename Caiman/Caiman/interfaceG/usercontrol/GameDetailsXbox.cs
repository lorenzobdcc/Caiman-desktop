/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display the informations of a game
 */
using Caiman.database;
using Caiman.interfaceG.XboxControl;
using Caiman.models;
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
    class GameDetailsXbox : XboxUserControl
    {
        private const string PATH_IMG_CAIMAN = @"Caiman\img\";

        public Game game = new Game();
        CallAPI callAPI = new CallAPI();

        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public GameDetailsXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
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
            this.BackColor = Color.Transparent;
            AutoScroll = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Create the view of the detail of a game
        /// If the game is present on the disk the user can start it if not he can download it
        /// </summary>
        public void CreateViewGame()
        {
            int rowCounter = 0;
            lstControls.Add(new List<Control>());

            Label lbl_title = new Label();
            lbl_title.Text = game.name; ;
            lbl_title.Location = new System.Drawing.Point(500, 60);
            lbl_title.Width = 472;
            lbl_title.Height = 50;
            lbl_title.Font = new Font("Arial", 16);
            lbl_title.ForeColor = Color.FromArgb(40, 167, 69);
            lbl_title.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(lbl_title);

            Label lbl_timeInGame = new Label();
            TimeInGame timeInGame = callAPI.CallTimeInGameUser(game.id, xboxMainForm.emulatorsManager.user.id);
            if (timeInGame.minutes != 0)
            {
                lbl_timeInGame.Text = "Time played: " + timeInGame.TimeHoursMinutes;
            }
            else
            {
                lbl_timeInGame.Text = "Time played: 00h00";
            }
            lbl_timeInGame.Location = new System.Drawing.Point(500, 110);
            lbl_timeInGame.Width = 472;
            lbl_timeInGame.Height = 50;
            lbl_timeInGame.Font = new Font("Arial", 16);
            lbl_timeInGame.ForeColor = Color.FromArgb(40, 167, 69);
            lbl_timeInGame.TextAlign = ContentAlignment.MiddleLeft;
            Controls.Add(lbl_timeInGame);

            PictureBox pictureBox = new PictureBox();
            string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            Image img = new Bitmap((imgPath + game.imageName));
            pictureBox.Location = new System.Drawing.Point(15, 50);
            pictureBox.Width = 472;
            pictureBox.Height = 700;
            pictureBox.TabStop = false;
            pictureBox.ForeColor = Color.White;
            pictureBox.BackColor = Color.FromArgb(48, 51, 56);
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = img;
            Controls.Add(pictureBox);

            Label lbl_description = new Label();
            lbl_description.Text = game.description;
            lbl_description.Location = new System.Drawing.Point(500, 175);
            lbl_description.MaximumSize = new Size((this.Width / 3), 500);
            lbl_description.AutoSize = true;
            lbl_description.Font = new Font("Arial", 14);
            lbl_description.ForeColor = Color.White;
            Controls.Add(lbl_description);

            this.callAPI = new CallAPI();
            var gamePath = @"C:\Caiman\" + this.callAPI.CallFolderNameGame(game.id) + @"\" + this.callAPI.CallFileNameGame(game.id);

            if (callAPI.CheckIfGameIsInFavorite(game.id, xboxMainForm.emulatorsManager.user.id) == false)
            {
                XboxButton btn_addTofavorite = new XboxButton("addFavorite", game.id, 0, 0);
                btn_addTofavorite.Text = "Add to favorite";
                btn_addTofavorite.Location = new System.Drawing.Point(500, 550);
                btn_addTofavorite.Click += new System.EventHandler(bouton_Click);
                lstControls[rowCounter].Add(btn_addTofavorite);
                Controls.Add(btn_addTofavorite);
                lstControls.Add(new List<Control>());
                rowCounter++;
            }
            else
            {
                XboxButton btn_removeFromfavorite = new XboxButton("removeFavorite", game.id, 0, 0);
                btn_removeFromfavorite.Text = "Remove from favorite";
                btn_removeFromfavorite.Location = new System.Drawing.Point(500, 550);
                btn_removeFromfavorite.Click += new System.EventHandler(bouton_Click);
                lstControls[rowCounter].Add(btn_removeFromfavorite);
                Controls.Add(btn_removeFromfavorite);
                lstControls.Add(new List<Control>());
                rowCounter++;
            }


            if (xboxMainForm.emulatorsManager.downloadManager.CheckIfDownloadIsActive(game.id))
            {
                XboxButton btn_inDownload = new XboxButton("downloadList", game.id, 0, 0);
                btn_inDownload.Text = "In download: " + game.name;
                btn_inDownload.Location = new System.Drawing.Point(500, 650);
                btn_inDownload.Click += new System.EventHandler(bouton_Click);
                lstControls[rowCounter].Add(btn_inDownload);
                Controls.Add(btn_inDownload);
                rowCounter++;
                lstControls.Add(new List<Control>());
            }
            else
            {
                if (!File.Exists(gamePath))
                {
                    XboxButton btn_download = new XboxButton("download", game.id, 0, 0);
                    btn_download.Text = "Download: " + game.name;
                    btn_download.Location = new System.Drawing.Point(500, 650);
                    btn_download.Click += new System.EventHandler(bouton_Click);
                    lstControls[rowCounter].Add(btn_download);
                    Controls.Add(btn_download);
                    rowCounter++;
                    lstControls.Add(new List<Control>());
                }
                else
                {
                    lstControls.Add(new List<Control>());

                    XboxButton btn_play = new XboxButton("play", game.id, 0, 0);
                    btn_play.Text = "Play: " + game.name;
                    btn_play.Location = new System.Drawing.Point(500, 650);
                    btn_play.Click += new System.EventHandler(bouton_Click);
                    lstControls[rowCounter].Add(btn_play);
                    Controls.Add(btn_play);
                    rowCounter++;
                    lstControls.Add(new List<Control>());

                    Label lbl_quit = new Label();
                    lbl_quit.Text = "Press: 'Select, RB, LB' to quit game";
                    lbl_quit.Location = new System.Drawing.Point(500, 600);
                    lbl_quit.MaximumSize = new Size((this.Width / 3), 500);
                    lbl_quit.AutoSize = true;
                    lbl_quit.Font = new Font("Arial", 14);
                    lbl_quit.ForeColor = Color.White;
                    Controls.Add(lbl_quit);

                    XboxButton btn_delete = new XboxButton("delete", game.id, 0, 1);
                    btn_delete.Text = "Delete: " + game.name;
                    btn_delete.Location = new System.Drawing.Point(500, 700);
                    btn_delete.Click += new System.EventHandler(bouton_Click);
                    lstControls[rowCounter].Add(btn_delete);
                    Controls.Add(btn_delete);
                    rowCounter++;
                    lstControls.Add(new List<Control>());

                }
            }

        }
        /// <summary>
        /// use to call the api to get the details
        /// </summary>
        /// <param name="idGame"></param>
        public void LoadGameDetail(int idGame)
        {
            game = callAPI.CallOneGame(idGame);
        }



    }
}
