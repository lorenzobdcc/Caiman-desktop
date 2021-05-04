using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class TestSideBarXboxUserControl : XboxUserControl
    {
        public TestSideBarXboxUserControl(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
        }
        public TestSideBarXboxUserControl(XboxMainForm xboxMain) : base(xboxMain)
        {
            CreateListButton(10,1);
            base.Size = new Size(200,800);
        }


        public void CreateListButton(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                lstControls.Add(new List<Control>());
            }
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < column; y++)
                {
                    lstControls[x].Add(new Button());
                }
            }

            for (int a_row = 0; a_row <= (row - 1); a_row++)
            {
                for (int b_column = 0; b_column <= (column-1); b_column++)
                {

                    lstControls[a_row][b_column].Text = (a_row+1) + " " + (b_column+1);
                    lstControls[a_row][b_column].Location = new System.Drawing.Point(b_column * 100 + 15, a_row * 60 + 15);
                    lstControls[a_row][b_column].Height = HEIGHT_BUTTON;
                    lstControls[a_row][b_column].Width = WIDTH_BUTTON;
                    lstControls[a_row][b_column].BackColor = Color.White;
                    lstControls[a_row][b_column].Name = a_row + " " + b_column;
                    lstControls[a_row][b_column].ForeColor = Color.Black;
                    Controls.Add(lstControls[a_row][b_column]);
                }
            }
        }

        public void CreateCategoryButton()
        {
            for (int i = 0; i < 5; i++)
            {
                lstControls.Add(new List<Control>());
            }
            
            for (int a = 0; a < 5; a++)
            {
                lstControls[a].Add(new Button());
                lstControls[a][0].Text = a + " " ;
                lstControls[a][0].Location = new System.Drawing.Point(0 * 100 + 15, a * 60 + 15);
                lstControls[a][0].Height = HEIGHT_BUTTON;
                lstControls[a][0].Width = WIDTH_BUTTON;
                lstControls[a][0].BackColor = Color.White;
                lstControls[a][0].Name = a + " ";
                lstControls[a][0].ForeColor = Color.Black;
                Controls.Add(lstControls[a][0]);
            }
        }
    }
}
