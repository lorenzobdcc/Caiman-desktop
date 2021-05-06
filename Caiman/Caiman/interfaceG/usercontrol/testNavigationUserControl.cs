using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class testNavigationUserControl : XboxUserControl
    {
        private System.Windows.Forms.Label label1;

        public testNavigationUserControl(XboxMainForm xboxMain) : base(xboxMain)
        {
        }

        public testNavigationUserControl(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
        }
        public testNavigationUserControl(string contexte)
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
        public void CreateListBrokenButton(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                lstControls.Add(new List<Control>());
            }
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    lstControls[x].Add(new XboxButton());
                }
            }

            for (int a_row = 0; a_row <= (row - 1); a_row++)
            {
                for (int b_column = 0; b_column <= (column - 1); b_column++)
                {
                    XboxButton tempButton = new XboxButton(("btn_" + a_row), a_row, a_row, b_column);
                    lstControls[a_row][b_column] = tempButton;
                    lstControls[a_row][b_column].Text = (a_row + 1) + " " + (b_column + 1);
                    lstControls[a_row][b_column].Location = new System.Drawing.Point(b_column * 100 + 15, a_row * 60 + 15);
                    lstControls[a_row][b_column].Name = a_row + " " + b_column;
                    lstControls[4][3] = null;
                    lstControls[3][2] = null;
                    lstControls[0][2] = null;
                    lstControls[0][3] = null;

                    lstControls[1][5] = null;
                    lstControls[2][5] = null;

                    lstControls[4][2] = null;
                    Controls.Add(lstControls[a_row][b_column]);
                }

            }
            

        }
    }
}
