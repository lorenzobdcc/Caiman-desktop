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

        XboxUserControl mainPanel;
        XboxUserControl topPanel;
        XboxUserControl sidePanel;

        private int position_x;
        private int position_y;


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


            timer.Interval = 2;
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
                    SendKeys.Send("{ENTER}");
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
            this.tbx_console.Location = new System.Drawing.Point(12, 510);
            this.tbx_console.Multiline = true;
            this.tbx_console.Name = "tbx_console";
            this.tbx_console.Size = new System.Drawing.Size(799, 80);
            this.tbx_console.TabIndex = 0;
            // 
            // XboxMainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.ControlBox = false;
            this.Controls.Add(this.tbx_console);
            this.Name = "XboxMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.XboxMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void ButtonHandler(object sender, EventArgs e)
        {
            mainPanel.Dispose();
            
            XboxButton button = (XboxButton)sender;
            List<string> tag = (List<string>)button.Tag;
            testContextUC temp = new testContextUC(this, topPanel, null, null, sidePanel);
            temp.CreateListButton(5,5);
            mainPanel = temp;
            mainPanel.Location = new Point(270, 120);

            Controls.Add(mainPanel);

            sidePanel.right_form = mainPanel;
            topPanel.bottom_form = mainPanel;
        }

        public void CreateTestControls()
        {

            sidePanel = new TestSideBarXboxUserControl(this);
            sidePanel.Location = new Point(0,100);

            topPanel = new TestTopPannelXbox(this);
            topPanel.Location = new Point(0,0);

            mainPanel = new TestXboxUserControl(this, topPanel, null, null, sidePanel);
            sidePanel.right_form = mainPanel;
            mainPanel.Location = new Point(270, 120);

            mainPanel.top_form = topPanel;
            sidePanel.top_form = topPanel;
            topPanel.bottom_form = mainPanel;
            topPanel.left_form = sidePanel;


            lstControls[0].Add(sidePanel);
            lstControls[0].Add(mainPanel);
            lstControls[0].Add(topPanel);
            sidePanel.BringToFront();
            mainPanel.BringToFront();
            topPanel.BringToFront();
            Controls.Add(sidePanel);
            Controls.Add(mainPanel);
            Controls.Add(topPanel);

        }

       

        private void XboxMainForm_Load(object sender, EventArgs e)
        {
            activeControl.MoveActivateControl();
            tbx_console.SetBounds((300), (this.Height - 250), 400, 150);
        }
    }
}
