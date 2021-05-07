/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a side pannel for the interface
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
    class TestSideBarXboxUserControl : XboxUserControl
    {

        /// <summary>
        /// Used to specify the main form of the application
        /// </summary>
        /// <param name="xboxMain"></param>
        public TestSideBarXboxUserControl(XboxMainForm xboxMain) : base(xboxMain)
        {
            CreateListNavButton();
            base.Size = new Size(200, 800);
            Width = 250;
            Height = (Screen.PrimaryScreen.Bounds.Height-100);
        }


        /// <summary>
        /// create a list of button to test the navigation
        /// </summary>
        public void CreateListNavButton()
        {
            List<string> lst_navbar = new List<string>();
            lst_navbar.Add("Broken navigation");
            lst_navbar.Add("Images");
            lst_navbar.Add("5 5");
            lst_navbar.Add("Quit App");
            for (int i = 0; i < lst_navbar.Count; i++)
            {
                lstControls.Add(new List<Control>());
                lstControls[i].Add(new XboxButton());
            }

            //update the buttons infos
            for (int a_row = 0; a_row <= (lst_navbar.Count -1); a_row++)
            {

                List<string> lstString = new List<string>();
                XboxButton tempButton = new XboxButton("side", a_row, a_row, 0);
                lstControls[a_row][0] = tempButton;
                lstControls[a_row][0].Text = lst_navbar[a_row];
                lstControls[a_row][0].Location = new System.Drawing.Point(0 * 100 + 15, a_row * 120 + 15);
                lstControls[a_row][0].Width = 200;
                lstControls[a_row][0].Name =  "btn_"+ lst_navbar[a_row];

                Controls.Add(lstControls[a_row][0]);
                lstControls[a_row][0].Click += new System.EventHandler(bouton_Click);

            }
            //set the action of button
            XboxButton Brocken = (XboxButton)lstControls[0][0];
            Brocken.contextInfos.contexte = "testNavigation";
            XboxButton Images = (XboxButton)lstControls[1][0];
            Images.contextInfos.contexte = "testImages";
            XboxButton quit = (XboxButton)lstControls[3][0];
            quit.contextInfos.contexte = "quit";
        }

        /// <summary>
        /// send to the main form what he need to do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bouton_Click(object sender, EventArgs e)
        {
            XboxButton tempXboxButton = (XboxButton)sender;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e , true);
        }
    }
}
