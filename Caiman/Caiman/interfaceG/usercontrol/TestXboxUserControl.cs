/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a basic userc control usable with a controller
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class TestXboxUserControl : XboxUserControl
    {
        public TestXboxUserControl(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
        }

        /// <summary>
        /// create the panel with  49 button in it
        /// </summary>
        private void InitializeComponent()
        {
            CreateListButton(7, 7);
            this.SuspendLayout();
            // 
            // TestXboxUserControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.Name = "TestXboxUserControl";
            this.Size = new System.Drawing.Size(1000, 1000);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// create a basic list of buttons
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
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
                    Controls.Add(lstControls[a_row][b_column]);
                }
            }
        }
    }
}
