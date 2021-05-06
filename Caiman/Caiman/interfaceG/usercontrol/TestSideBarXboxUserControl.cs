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
            //CreateListButton(10,1);
            CreateListNavButton();
            base.Size = new Size(200, 800);
            Width = 250;
            Height = (Screen.PrimaryScreen.Bounds.Height-100);
        }



        public void CreateListNavButton()
        {
            List<string> lst_navbar = new List<string>();
            lst_navbar.Add("Brocken navigation");
            lst_navbar.Add("Images");
            lst_navbar.Add("Super_Smash");
            lst_navbar.Add("Kingdom_Hearts");
            lst_navbar.Add("Dragon_Ball");
            lst_navbar.Add("Metroid");
            for (int i = 0; i < lst_navbar.Count; i++)
            {
                lstControls.Add(new List<Control>());
                lstControls[i].Add(new XboxButton());
            }



            for (int a_row = 0; a_row <= (lst_navbar.Count -1); a_row++)
            {

                List<string> lstString = new List<string>();
                XboxButton tempButton = new XboxButton("side", a_row, a_row, 0);
                lstControls[a_row][0] = tempButton;
                lstControls[a_row][0].Text = lst_navbar[a_row];
                lstControls[a_row][0].Location = new System.Drawing.Point(0 * 100 + 15, a_row * 120 + 15);
                lstControls[a_row][0].Width = 200;
                lstControls[a_row][0].Name =  "btn_"+ lst_navbar[a_row];
                lstControls[a_row][0].Font = new Font("French Script MT", 14);


                Controls.Add(lstControls[a_row][0]);
                lstControls[a_row][0].Click += new System.EventHandler(bouton_Click);
                


            }
            XboxButton Brocken = (XboxButton)lstControls[0][0];
            Brocken.btn_contexte.contexte = "testNavigation";
            XboxButton Images = (XboxButton)lstControls[1][0];
            Images.btn_contexte.contexte = "testImages";
        }

        protected void bouton_Click(object sender, EventArgs e)
        {
            XboxButton tempXboxButton = (XboxButton)sender;
            ButtonContext tempButtonContext = tempXboxButton.btn_contexte;
            xboxMainForm.ButtonHandler(tempButtonContext, e , true);
        }
    }
}
