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
            for (int i = 0; i < 5; i++)
            {
                lstControls.Add(new List<Control>());
                lstControls[i].Add(new XboxButton());
            }
            List<string> lst_navbar = new List<string>();
            lst_navbar.Add("Zelda");
            lst_navbar.Add("Mario");
            lst_navbar.Add("Super_Smash");
            lst_navbar.Add("Kingdom_Hearts");
            lst_navbar.Add("Dragon_Ball");


            for (int a_row = 0; a_row <= 4; a_row++)
            {


                lstControls[a_row][0].Text = lst_navbar[a_row];
                lstControls[a_row][0].Location = new System.Drawing.Point(0 * 100 + 15, a_row * 120 + 15);
                lstControls[a_row][0].Height = HEIGHT_BUTTON;
                lstControls[a_row][0].Width = 200;
                lstControls[a_row][0].Name =  "btn_"+ lst_navbar[a_row];
                lstControls[a_row][0].Tag = a_row;
                Controls.Add(lstControls[a_row][0]);
                lstControls[a_row][0].Click += new System.EventHandler(bouton_Click);

            }
        }

        protected void bouton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
            Button btn_sender = (Button)sender;
        }
    }
}
