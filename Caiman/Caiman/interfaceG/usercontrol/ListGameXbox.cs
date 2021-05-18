/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to test if i can load an image from the web
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
            // testContextUC
            // 
            this.Name = "testContextUC";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.AutoScroll = true;

        }

        public void CreateListGames()
        {

            string imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            XboxImage tempXboxImage = new XboxImage();
            int maxWidth = Width;
            int max_rank = Width / tempXboxImage.Width;
            int tempPos_x = 0;
            int tempPos_y = 0;
            lstControls.Add(new List<Control>());
            foreach (var game in lst_games)
            {
                lstControls[tempPos_y].Add(new XboxImage());
                Image img = new Bitmap((imgPath+ game.imageName));
                XboxImage tempButton = new XboxImage("game", img, game.id, 0, 0);
                lstControls[tempPos_y][tempPos_x] = tempButton;
                lstControls[tempPos_y][tempPos_x].Location = new System.Drawing.Point(tempPos_x * 350 + 15, tempPos_y * 150 + 15);

                Controls.Add(lstControls[tempPos_y][tempPos_x]);


                if (tempPos_x == max_rank)
                {
                    lstControls.Add(new List<Control>());
                    tempPos_y ++;
                    tempPos_x = 0;
                }
                tempPos_x++;
            }
        }

        /// <summary>
        /// Used to cheate a list of images
        /// The images comme from the website caiman.cfpt.info
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void CreateListImages(int row, int column)
        {
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < row; i++)
                {
                    lstControls.Add(new List<Control>());
                }
                for (int x = 0; x < row; x++)
                {
                    
                    for (int y = 0; y < column; y++)
                    {
                    
                        
                        lstControls[x].Add(new XboxImage());
                    }
                }
                
                //for (int a_row = 0; a_row <= (row - 1); a_row++)
                //{
                //    if (!File.Exists(@"C:\image" + a_row + ".jpg"))
                //    {
                //        client.DownloadFile(new Uri("http://caiman.cfpt.info/img/games/THE_LEGEND_OF_ZELDA_THE_WIND_WAKER.jpg"), @"C:\image" + a_row + ".jpg");
                //    }
                    
                //    for (int b_column = 0; b_column <= (column - 1); b_column++)
                //    {
                        
                //        Image img = new Bitmap(@"C:\image" + a_row + ".jpg");
                //        XboxImage tempButton = new XboxImage(("btn_" + a_row),img, a_row, a_row, b_column);
                //        lstControls[a_row][b_column] = tempButton;
                //        lstControls[a_row][b_column].Text = (a_row + 1) + " " + (b_column + 1);
                //        lstControls[a_row][b_column].Location = new System.Drawing.Point(b_column * 350 + 15, a_row * 150 + 15);
                //        lstControls[a_row][b_column].Name = a_row + " " + b_column;

                //        Controls.Add(lstControls[a_row][b_column]);
                //    }
                //}
            }


        }
    }
}
