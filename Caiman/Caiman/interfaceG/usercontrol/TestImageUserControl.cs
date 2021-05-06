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
    class TestImageUserControl : XboxUserControl
    {
        private System.Windows.Forms.Label label1;

        public TestImageUserControl(XboxMainForm xboxMain) : base(xboxMain)
        {
        }

        public TestImageUserControl(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
        }
        public TestImageUserControl(string contexte)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(76, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // testContextUC
            // 
            this.Controls.Add(this.label1);
            this.Name = "testContextUC";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
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

                for (int a_row = 0; a_row <= (row - 1); a_row++)
                {
                    if (!File.Exists(@"C:\image" + a_row + ".jpg"))
                    {
                        client.DownloadFile(new Uri("http://caiman.cfpt.info/img/games/THE_LEGEND_OF_ZELDA_THE_WIND_WAKER.jpg"), @"C:\image" + a_row + ".jpg");
                    }
                    
                    for (int b_column = 0; b_column <= (column - 1); b_column++)
                    {
                        
                        Image img = new Bitmap(@"C:\image" + a_row + ".jpg");
                        XboxImage tempButton = new XboxImage(("btn_" + a_row),img, a_row, a_row, b_column);
                        lstControls[a_row][b_column] = tempButton;
                        lstControls[a_row][b_column].Text = (a_row + 1) + " " + (b_column + 1);
                        lstControls[a_row][b_column].Location = new System.Drawing.Point(b_column * 350 + 15, a_row * 150 + 15);
                        lstControls[a_row][b_column].Name = a_row + " " + b_column;

                        Controls.Add(lstControls[a_row][b_column]);
                    }
                }
            }


        }
    }
}
