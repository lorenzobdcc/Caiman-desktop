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

        public TestTopPannelXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {

        }

        public void CreateListNavButton()
        {

            
                lstControls.Add(new List<Control>());

            for (int i = 0; i < 5; i++)
            {
                lstControls[0].Add(new Button());
            }
            List<string> lst_navbar = new List<string>();
            lst_navbar.Add("Zelda");
            lst_navbar.Add("Mario");
            lst_navbar.Add("Super_Smash");
            lst_navbar.Add("Kingdom_Hearts");
            lst_navbar.Add("Dragon_Ball");


            for (int a_row = 0; a_row <= 4; a_row++)
            {


                lstControls[0][a_row].Text = lst_navbar[a_row];
                lstControls[0][a_row].Location = new System.Drawing.Point(a_row * 100 + 15, 0 * 60 + 15);
                lstControls[0][a_row].Height = HEIGHT_BUTTON;
                lstControls[0][a_row].Width = 100;
                lstControls[0][a_row].BackColor = Color.White;
                lstControls[0][a_row].Name = "btn_" + lst_navbar[a_row];
                lstControls[0][a_row].ForeColor = Color.Black;
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
