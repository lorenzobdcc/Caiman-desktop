/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a side pannel for the interface
 */
using Caiman.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class SideBarXbox : XboxUserControl
    {

        /// <summary>
        /// Used to specify the main form of the application
        /// </summary>
        /// <param name="xboxMain"></param>
        public SideBarXbox(XboxMainForm xboxMain) : base(xboxMain)
        {
            CreateListNavButton();
            Width = 250;
            Height = (Screen.PrimaryScreen.Bounds.Height-100);
            AutoScroll = true;
        }


        /// <summary>
        /// create a list of button to test the navigation
        /// </summary>
        public void CreateListNavButton()
        {
            List<string> lst_navbar = new List<string>();
            List<Category> lst_category = xboxMainForm.callAPI.CallAllCategories();

            lst_navbar.Add("Favorites games");
            lst_navbar.Add("All Games");

            foreach (var item in lst_category)
            {
                lst_navbar.Add(item.name);
            }
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
                lstControls[a_row][0].Location = new System.Drawing.Point(0 * 100 + 25, a_row * 50 + 15);
                lstControls[a_row][0].Width = 200;
                lstControls[a_row][0].Name =  "btn_"+ lst_navbar[a_row];

                Controls.Add(lstControls[a_row][0]);
                lstControls[a_row][0].Click += new System.EventHandler(bouton_Click);

            }

            //set the action of button
            XboxButton userFavoritesGames = (XboxButton)lstControls[0][0];
            userFavoritesGames.contextInfos.contexte = "favorite";
            XboxButton allGames = (XboxButton)lstControls[1][0];
            allGames.contextInfos.contexte = "home";

            for (int i = 0; i < lst_category.Count; i++)
            {
                XboxButton tempButton = (XboxButton)lstControls[(i+2)][0];
                tempButton.contextInfos.contexte = "category";
                tempButton.contextInfos.id_contexte = (lst_category[i].id);
            }

        }

        ///// <summary>
        ///// send to the main form what he need to do
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void bouton_Click(object sender, EventArgs e)
        //{
        //    XboxButton tempXboxButton = (XboxButton)sender;
        //    ContextInformations tempButtonContext = tempXboxButton.contextInfos;
        //    xboxMainForm.ContexteHandler(tempButtonContext, e , true);
        //}
    }
}
