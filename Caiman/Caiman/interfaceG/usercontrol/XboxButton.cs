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
        public XboxButton()
        {
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(48, 51, 56);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
        }
        private void XboxButton_GotFocus( EventArgs e)
        {

            MessageBox.Show("You are in the Control.GotFocus event.");
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
