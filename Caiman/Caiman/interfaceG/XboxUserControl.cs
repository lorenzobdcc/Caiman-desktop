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

        public XboxUserControl()
        {

        }

        public XboxUserControl(XboxMainForm xboxMain , XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left)
        {
            InitializeComponent();
            
            xboxMainForm = xboxMain;
            top_form = top;
            bottom_form = bottom;
            right_form = right;
            left_form = left;
        }

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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(38)))), ((int)(((byte)(46)))));
            this.Name = "XboxUserControl";
            this.Size = new System.Drawing.Size(418, 401);
            this.ResumeLayout(false);

        }

        public void MoveActivateControl(int destination = 0)
        {
            //top = 1
            //right = 2
            //down = 3
            //left = 4
            if (destination == 3)
            {
                if (lstControls[position_y][position_x] == null)
                {
                    int x = position_x;
                    int y = position_y;
                    while (lstControls[position_y][x] == null)
                    {
                        if (x > lstControls[position_x-1].Count())
                        {
                            break;
                        }
                        x++;
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
                if (lstControls[position_y][position_x] == null)
                {
                    int x = position_x - 1;
                    int y = position_y;
                    while (lstControls[position_x - 1][position_y] == null)
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
        }
    }
}
