/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a button for the navbar witch is useable with a controller
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
    public class XboxNavbarButton : Button
    {

        public ContextInformations contextInfos;
        public XboxNavbarButton(string contexte ,Image img,int id_contexte, int position_y, int position_x): this()
        {
            this.contextInfos = new ContextInformations(contexte, id_contexte, position_y, position_x);
            this.BackgroundImage = img;
            
        }

        public XboxNavbarButton(Image img): this()
        {
            this.BackgroundImage = img;
        }

        /// <summary>
        /// Create a button with specific design
        /// </summary>
        public XboxNavbarButton()
        {
            this.contextInfos = null;
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(33, 38, 46);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
            this.Height = 35;
            this.Width = 35;
            this.Font = new Font("Arial", 14);
            this.AutoSize = false;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        /// <summary>
        /// Updated onclick event where xou tel to the main form which button has clicked in a list of button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            //tell to the topMainForm which control is active
            XboxUserControl xboxUserControl = (XboxUserControl)this.Parent;
            xboxUserControl.position_x = this.contextInfos.position_x;
            xboxUserControl.position_y = this.contextInfos.position_y;

            XboxMainForm topMainForm = (XboxMainForm)this.TopLevelControl;
            topMainForm.ActiveControl1 = xboxUserControl;
        }

        /// <summary>
        /// event when the button has focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FlatAppearance.BorderSize = 2;

        }

        /// <summary>
        /// Change the button disign when the button is not focused anymore
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FlatAppearance.BorderSize = 0;
        }




    }
}
