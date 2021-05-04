using Caiman.interfaceG.usercontrol;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG
{
    public class XboxMainForm : Form
    {

        List<List<Control>> lstControls = new List<List<Control>>();
        XboxController xboxController;

        public XboxUserControl activeControl;


        public string old_input;



        private int position_x;
        private int position_y;

        public const int WIDTH_BUTTON = 75;
        public const int HEIGHT_BUTTON = 23;
        private TextBox tbx_console;
        Timer timer = new Timer();

        public int Position_x { get => position_x; set
            {
                position_x = value;
                if (position_x <= 0)
                {
                    position_x = 0;
                }
                if (position_x >= lstControls.Count)
                {
                    position_x = (lstControls[position_y].Count - 1);
                }

            }
        }
        public int Position_y { get => position_y; set
            {
                position_y = value;
                if (position_y <= 0)
                {
                    position_y = 0;
                }
                if (position_y >= lstControls.Count)
                {
                    position_y = (lstControls[position_y].Count - 1);
                }

            }
        }

        public XboxMainForm()
        {
            InitializeComponent();
            xboxController = new XboxController(this);
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            CreateTestControls();
            activeControl = (XboxUserControl)lstControls[0][0];
            
            InitTimer();
        }

        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(UpdateInterface);
            timer.Tick += new EventHandler(ScanInput);


            timer.Interval = 1;
            timer.Start();
        }

        private void UpdateInterface(object sender, EventArgs e)
        {
            string txt = "";
            txt += xboxController.GetInput();
            txt += "\r\nposition X: " +activeControl.Position_x;
            txt += "\r\nposition Y: " +activeControl.Position_y;
            tbx_console.Text = txt;
        }

        public void ScanInput(object sender, EventArgs e)
        {


            if (xboxController.lstController[0].IsConnected)
            {
                string input = xboxController.lstController[0].GetState().Gamepad.Buttons.ToString();


                if (input != "DPadLeft" && old_input == "DPadLeft")
                {

                   activeControl.Position_x--;
                   activeControl.MoveActivateControl();

                }
                if (input != "DPadRight" && old_input == "DPadRight")
                {
                    activeControl.Position_x++;
                    activeControl.MoveActivateControl();

                }
                if (input != "DPadUp" && old_input == "DPadUp")
                {

                    activeControl.Position_y--;
                    activeControl.MoveActivateControl();
                }
                if (input != "DPadDown" && old_input == "DPadDown")
                {
                    activeControl.Position_y++;
                    activeControl.MoveActivateControl();

                }
                if (input != "A" && old_input == "A")
                {

                }
                old_input = input;
            }
        }
        public void MoveActivateControl()
        {
            activeControl.lstControls[position_x-1][position_y].Focus();
        }



        private void InitializeComponent()
        {
            this.tbx_console = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbx_console
            // 
            this.tbx_console.Location = new System.Drawing.Point(13, 13);
            this.tbx_console.Multiline = true;
            this.tbx_console.Name = "tbx_console";
            this.tbx_console.Size = new System.Drawing.Size(119, 461);
            this.tbx_console.TabIndex = 0;
            // 
            // XboxMainForm
            // 
            this.ClientSize = new System.Drawing.Size(853, 602);
            this.Controls.Add(this.tbx_console);
            this.Name = "XboxMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.XboxMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void CreateTestControls()
        {

            TestSideBarXboxUserControl test1 = new TestSideBarXboxUserControl(this);
            test1.Location = new Point(150, 50);

            TestXboxUserControl test2 = new TestXboxUserControl(this, null, null, null, test1);
            test1.right_form = test2;
            test2.Location = new Point(700, 50);

            

            lstControls[0].Add(test1);
            lstControls[0].Add(test2);
            test1.BringToFront();
            test2.BringToFront();
            Controls.Add(test1);
            Controls.Add(test2);

        }

        public void CreateListButton(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                lstControls.Add(new List<Control>());
            }
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    lstControls[x].Add(new Button());
                }
            }

            for (int a = 0; a < column; a++)
            {
                for (int b = 0; b < row; b++)
                {

                    lstControls[a][b].Text = a + " " + b;
                    lstControls[a][b].Location = new System.Drawing.Point(a * 100 + 15, b * 60 + 15);
                    lstControls[a][b].Height = HEIGHT_BUTTON;
                    lstControls[a][b].Width = WIDTH_BUTTON;
                    lstControls[a][b].BackColor = Color.White;
                    lstControls[a][b].Name = a + " " + b;
                    lstControls[a][b].ForeColor = Color.Black;
                    Controls.Add(lstControls[a][b]);
                }
            }
        }

        private void XboxMainForm_Load(object sender, EventArgs e)
        {
            activeControl.MoveActivateControl();
        }
    }
}
