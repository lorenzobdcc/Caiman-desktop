/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Class main class to create component for the interface
 */
using Caiman.interfaceG.usercontrol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG
{
    public class XboxUserControl : UserControl
    {
        public List<List<Control>> lstControls = new List<List<Control>>();

        public XboxMainForm xboxMainForm;

        public XboxUserControl top_form;
        public XboxUserControl bottom_form;
        public XboxUserControl right_form;
        public XboxUserControl left_form;

        public XboxButton activebutton;


        public int position_x;
        public int position_y;

        /// <summary>
        /// Check if the position where the user want to go is valid
        /// if the position is not valid either the cursor will not move or it will go to a valid possition
        /// </summary>
        public int Position_x
        {
            get => position_x; set
            {
                position_x = value;
                if (position_x < 0)
                {
                    position_x = 0;
                    if (left_form != null)
                    {
                        if ((left_form.lstControls[left_form.position_y].Count - 1) > -1)
                        {
                            left_form.position_x = (left_form.lstControls[left_form.position_y].Count - 1);
                        }
                        else
                        {
                            left_form.position_x = 1;
                        }
                        
                        xboxMainForm.ActiveControl1 = left_form;
                    }
                }
                if (position_x >= (lstControls[(position_y)].Count))
                {
                    position_x = (lstControls[position_y].Count -1);
                    if (right_form != null)
                    {

                        right_form.position_x = 0;
                        xboxMainForm.ActiveControl1 = right_form;
                    }
                }

            }
        }

        /// <summary>
        /// Check if the position where the user want to go is valid
        /// if the position is not valid either the cursor will not move or it will go to a valid possition
        /// </summary>
        public int Position_y
        {
            get => position_y; set
            {
                position_y = value;
                if (position_y < 0)
                {
                    position_y = 0;
                    if (top_form != null)
                    {
                        top_form.position_y = (top_form.lstControls.Count()-1);
                        xboxMainForm.ActiveControl1 = top_form;
                        
                    }
                }
                if (position_y >= (lstControls.Count))
                {
                    position_y = ((lstControls.Count -1));
                    if (bottom_form != null)
                    {
                        bottom_form.position_y = 0;
                        xboxMainForm.ActiveControl1 = bottom_form;
                        
                    }
                }

            }
        }

        /// <summary>
        /// default contructor
        /// </summary>
        public XboxUserControl()
        {

        }

        /// <summary>
        /// constructor used to specify the contexte next to the XboxUserControl
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public XboxUserControl(XboxMainForm xboxMain , XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left)
        {
            InitializeComponent();
            
            xboxMainForm = xboxMain;
            top_form = top;
            bottom_form = bottom;
            right_form = right;
            left_form = left;
        }

        /// <summary>
        /// consctructor where you specified the main form of the application
        /// </summary>
        /// <param name="xboxMain"></param>
        public XboxUserControl(XboxMainForm xboxMain)
        {
            InitializeComponent();
            xboxMainForm = xboxMain;
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XboxUserControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(33,38,46);
            this.Name = "XboxUserControl";
            this.Size = new System.Drawing.Size(418, 401);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// move the user cursor to the position required by the user
        /// 
        /// if the position required is not valid the user cursor will be moved to the next valid position
        /// </summary>
        /// <param name="destination"></param>
        public void MoveActivateControl(string destination = "")
        {
            //top = 1
            //right = 2
            //down = 3
            //left = 4
            if (destination == "down")
            {
                if (lstControls[position_y][position_x] == null)
                {
                    int x = position_x;
                    int y = position_y;

                    int y_right_not_disponible;
                    while (x < lstControls[position_y].Count() && lstControls[position_y][x] == null)
                    {
                        if (x > lstControls[position_x-1].Count())
                        {
                            break;
                        }
                        x++;
                    }
                    if (x == lstControls[position_y].Count())
                    {
                        y_right_not_disponible = y;

                        while (y_right_not_disponible>0)
                        {
                            y_right_not_disponible --;
                            if (lstControls[y_right_not_disponible][position_x] != null)
                            {
                                position_y = y_right_not_disponible;
                                lstControls[(position_y)][position_x].Focus();
                                return;
                            }
                        }
                    }
                    lstControls[(position_y)][x].Focus();
                    Position_x = x;
                }
                else
                {
                    lstControls[(position_y)][position_x].Focus();
                }
            }
            else
            {
                if (position_x < lstControls[position_y].Count())
                {


                    if (lstControls[position_y][position_x] == null)
                    {
                        int x = position_x - 1;
                        int y = position_y;
                        while (lstControls[position_y][x] == null)
                        {
                            if (x < 0)
                            {

                                break;
                            }
                            x--;
                        }
                        lstControls[(position_y)][x].Focus();
                        Position_x = x;
                    }
                    else
                    {
                        lstControls[(position_y)][position_x].Focus();
                    }
                }
                else {
                    position_y--;
                }
            }
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
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }


    }
}
