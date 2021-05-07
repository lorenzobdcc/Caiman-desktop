/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a button with an image in backGround
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.XboxControl
{
    class XboxImage: Button
    {

        public ContextInformations btn_contexte;
        public XboxImage(string contexte, Image img, int id_contexte, int position_y, int position_x): this()
        {
            this.btn_contexte = new ContextInformations(contexte, id_contexte, position_y, position_x);
            this.BackgroundImage = img;

        }

        /// <summary>
        /// Create a button with specific design and an image in background
        /// </summary>
        public XboxImage()
        {
            this.btn_contexte = null;
            this.TabStop = false;
            this.FlatStyle = FlatStyle.Flat;
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(48, 51, 56);
            this.FlatAppearance.BorderSize = 2;
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
            this.Height = 400;
            this.Width = 270;
            this.Tag = new List<string>();
            this.BackgroundImageLayout = ImageLayout.Stretch;


            this.Font = new Font("Arial", 14);
            this.AutoSize = true;
        }

        /// <summary>
        /// Updated onclick event where xou tel to the main form which button has clicked in a list of button
        /// </summary>
        /// <param name="e"></param>
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


        /// <summary>
        /// event when the button has focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FlatAppearance.BorderSize = 3;

        }

        /// <summary>
        /// Change the button disign when the button is not focused anymore
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FlatAppearance.BorderSize = 2;
        }
    }
}
