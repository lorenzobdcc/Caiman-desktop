﻿/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to test to move the user cursor in a list of button where some buttons are missing
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
    class testNavigationUserControl : XboxUserControl
    {

        public testNavigationUserControl(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
        }


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

        }
        /// <summary>
        /// Create a list of button witch hole in it to test the comportement of the user's cursor
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
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
