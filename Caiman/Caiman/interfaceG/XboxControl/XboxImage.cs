/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to create a button with an image in background
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

        public ContextInformations contextInfos;
        public XboxImage(string contexte, Image img, int id_contexte, int position_y, int position_x): this()
        {
            this.contextInfos = new ContextInformations(contexte, id_contexte, position_y, position_x);
            this.BackgroundImage = img;

        }

        /// <summary>
        /// Create a button with specific design and an image in background
        /// </summary>
        public XboxImage()
        {
            this.contextInfos = null;
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
        /// Updated onclick event where you tel to the main form which button has clicked in a list of button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);


        }


        /// <summary>
        /// event when the button has focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FlatAppearance.BorderColor = Color.FromArgb(222, 62, 4);
            this.FlatAppearance.BorderSize = 3;

        }

        /// <summary>
        /// Change the button disign when the button is not focused anymore
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FlatAppearance.BorderColor = Color.FromArgb(40, 167, 69);
            this.FlatAppearance.BorderSize = 2;
        }



    }
}
