using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    public class XboxButton : Button
    {

        public ButtonContext btn_contexte;
        public XboxButton(string contexte,int id_contexte, int position_y, int position_x)
        {
            this.btn_contexte = new ButtonContext(contexte, id_contexte, position_y, position_x);
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(48, 51, 56);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
            this.Height = 30;
            this.Tag = new List<string>();
            

            this.Font = new Font("French Script MT", 14);
            this.AutoSize = true;
        }

        public XboxButton()
        {
            this.btn_contexte = null;
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(48, 51, 56);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
            this.Height = 30;
            this.Tag = new List<string>();


            this.Font = new Font("French Script MT", 14);
            this.AutoSize = true;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            //this.FlatAppearance.BorderSize = 2;

            //tell to the topMainForm which control is active
            XboxUserControl xboxUserControl = (XboxUserControl)this.Parent;
            xboxUserControl.position_x = this.btn_contexte.position_x;
            xboxUserControl.position_y = this.btn_contexte.position_y;

            XboxMainForm topMainForm = (XboxMainForm)this.TopLevelControl;
            topMainForm.ActiveControl1 = xboxUserControl;
        }


        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FlatAppearance.BorderSize = 2;

        }

        public void OnClickXbox()
        {
            this.PerformClick();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FlatAppearance.BorderSize = 0;
        }




    }
}
