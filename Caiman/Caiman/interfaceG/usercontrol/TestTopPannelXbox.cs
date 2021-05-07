/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Test class used to create a top panel
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
    class TestTopPannelXbox : XboxUserControl
    {
        public TestTopPannelXbox(XboxMainForm xboxMain) : base(xboxMain)
        {
            Height = 100;
            Width = Screen.PrimaryScreen.Bounds.Width;
            CreateListNavButton();
        }

        /// <summary>
        /// Contructor to spécifiy border panel
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public TestTopPannelXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {

        }

        /// <summary>
        /// Create a list of button to test the deplacement of the cursor
        /// </summary>
        public void CreateListNavButton()
        {

            
                lstControls.Add(new List<Control>());

            for (int i = 0; i < 5; i++)
            {
                lstControls[0].Add(new XboxButton());
            }
            List<string> lst_navbar = new List<string>();
            lst_navbar.Add("Nav 1");
            lst_navbar.Add("Nav 2");
            lst_navbar.Add("Nav 3");



            for (int a_row = 0; a_row < lst_navbar.Count(); a_row++)
            {

                XboxButton tempButton = new XboxButton(("btn_" + a_row), a_row, 0, a_row);
                lstControls[0][a_row] = tempButton;
                lstControls[0][a_row].Text = lst_navbar[a_row];
                lstControls[0][a_row].Location = new System.Drawing.Point(a_row * 200 + (Screen.PrimaryScreen.Bounds.Width/4), 0 * 60 + 50);
                lstControls[0][a_row].Width = 100;
                lstControls[0][a_row].Name = "btn_" + lst_navbar[a_row];
                lstControls[0][a_row].Tag = a_row;
                Controls.Add(lstControls[0][a_row]);
                lstControls[0][a_row].Click += new System.EventHandler(bouton_Click);

            }
        }

        private void bouton_Click(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }
    }
}
