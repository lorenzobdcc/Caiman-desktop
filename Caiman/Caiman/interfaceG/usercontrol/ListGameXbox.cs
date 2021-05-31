/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display a list of game
 */
using Caiman.database;
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
using Newtonsoft.Json;
using Caiman.models;

namespace Caiman.interfaceG.usercontrol
{
    class ListGameXbox : XboxUserControl
    {

        private const string PATH_IMG_CAIMAN = @"Caiman\img\";
        public List<Game> lst_games;
        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public ListGameXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
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
            // ListGameXbox
            // 
            this.Name = "ListGameXbox";
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AutoScroll = true;
            //this.DoubleBuffered = true;

            Width = (Screen.PrimaryScreen.Bounds.Width - 250);
            Height = (Screen.PrimaryScreen.Bounds.Height - 60);

        }
        /// <summary>
        /// Create the list of game receive
        /// this function does work with a list so no nedd to change the code for diferents lists of games
        /// </summary>
        public void CreateListGames()
        {

            string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            XboxImage tempXboxImage = new XboxImage();
            int max_rank = Width / (315);
            int tempPos_x = 0;
            int tempPos_y = 0;
            lstControls.Add(new List<Control>());


            foreach (var game in lst_games)
            {
                if (tempPos_x == max_rank)
                {
                    lstControls.Add(new List<Control>());
                    tempPos_y++;
                    tempPos_x = 0;
                }

                lstControls[tempPos_y].Add(new XboxImage());
                Image img = new Bitmap((imgPath+ game.imageName));
                XboxImage tempButton = new XboxImage("game", img, game.id, tempPos_x, tempPos_y);
                lstControls[tempPos_y][tempPos_x] = tempButton;
                lstControls[tempPos_y][tempPos_x].Location = new System.Drawing.Point(tempPos_x * 300 + 15, tempPos_y * 430 + 15);

                Controls.Add(lstControls[tempPos_y][tempPos_x]);
                tempButton.Click += new System.EventHandler(bouton_Click);



                tempPos_x++;
            }
        }
        /// <summary>
        /// Override the default clic event to send the contexte to the nain form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void bouton_Click(object sender, EventArgs e)
        {
            XboxImage tempXboxButton = (XboxImage)sender;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }

    }
}
