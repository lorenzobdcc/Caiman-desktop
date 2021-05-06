using Caiman.interfaceG.usercontrol;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG
{
    public class XboxMainForm : Form
    {

        List<List<Control>> lstControls = new List<List<Control>>();
        XboxController xboxController;

        private XboxUserControl activeControl1;
        public XboxUserControl old_activeControl;

        public List<ButtonContext> lstOldContexte = new List<ButtonContext>();
        public ButtonContext activeContexte;
        

        public string old_input;

        bool old_leftAnalogUp = false;
        bool old_leftAnalogDown = false;
        bool old_leftAnalogLeft = false;
        bool old_leftAnalogRight = false;

        XboxUserControl mainPanel;
        XboxUserControl old_mainPanel;
        XboxUserControl topPanel;
        XboxUserControl sidePanel;

        private int position_x;
        private int position_y;

        


        private TextBox tbx_console;
        Timer timer = new Timer();

        public XboxUserControl ActiveControl1 { get => activeControl1;set
            {
                old_activeControl = ActiveControl1;
                activeControl1 = value;
            }
        }

        public XboxUserControl MainPanel { get => mainPanel; set
            {
                old_mainPanel = mainPanel;
                mainPanel = value;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        

        public XboxMainForm()
        {
            InitializeComponent();
            xboxController = new XboxController(this);
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            CreateTestControls();
            ActiveControl1 = (XboxUserControl)lstControls[0][0];

            lstOldContexte.Add(new ButtonContext("home", 0, 0, 0));
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
            txt += "\r\nposition X: " +ActiveControl1.Position_x;
            txt += "\r\nposition Y: " +ActiveControl1.Position_y;
            tbx_console.Text = txt;
        }

        public static bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        public void ScanInput(object sender, EventArgs e)
        {


            if (xboxController.lstController[0].IsConnected)
            {
                string input = xboxController.lstController[0].GetState().Gamepad.Buttons.ToString();
                int inputAnalogLeftX = xboxController.lstController[0].GetState().Gamepad.LeftThumbX;
                int inputAnalogLeftY = xboxController.lstController[0].GetState().Gamepad.LeftThumbY;

                bool leftAnalogUp = false;
                bool leftAnalogDown = false;
                bool leftAnalogLeft = false;
                bool leftAnalogRight = false;

                if (inputAnalogLeftX > 20000)
                {
                    leftAnalogRight = true;
                }

                if (inputAnalogLeftX < -20000)
                {
                    leftAnalogLeft = true;
                }

                if (inputAnalogLeftY > 20000)
                {
                    leftAnalogUp = true;
                }

                if (inputAnalogLeftY < -20000)
                {
                    leftAnalogDown = true;
                }

                if (ApplicationIsActivated())
                {
                    if (leftAnalogLeft == true && old_leftAnalogLeft == false)
                    {
                        ActiveControl1.Position_x--;
                        ActiveControl1.MoveActivateControl();
                    }

                    if (leftAnalogRight == true && old_leftAnalogRight == false)
                    {
                        ActiveControl1.Position_x++;
                        ActiveControl1.MoveActivateControl();
                    }

                    if (leftAnalogDown == true && old_leftAnalogDown == false)
                    {
                        ActiveControl1.Position_y++;
                        ActiveControl1.MoveActivateControl();
                    }

                    if (leftAnalogUp == true && old_leftAnalogUp == false)
                    {
                        ActiveControl1.Position_y--;
                        ActiveControl1.MoveActivateControl();
                    }


                    if (input == "DPadLeft" && old_input != "DPadLeft")
                    {

                        ActiveControl1.Position_x--;
                        ActiveControl1.MoveActivateControl();

                    }
                    if (input == "DPadRight" && old_input != "DPadRight")
                    {
                        ActiveControl1.Position_x++;
                        ActiveControl1.MoveActivateControl();

                    }
                    if (input == "DPadUp" && old_input != "DPadUp")
                    {

                        ActiveControl1.Position_y--;

                        ActiveControl1.MoveActivateControl();
                    }
                    if (input == "DPadDown" && old_input != "DPadDown")
                    {
                        ActiveControl1.Position_y++;
                        ActiveControl1.MoveActivateControl();

                    }
                    if (input == "A" && old_input != "A")
                    {
                        SendKeys.Send("{ENTER}");
                    }
                    if (input == "B" && old_input != "B")
                    {

                        LoadOldMainPanel();
                    }

                    old_leftAnalogUp = leftAnalogUp;
                    old_leftAnalogDown = leftAnalogDown;
                    old_leftAnalogLeft = leftAnalogLeft;
                    old_leftAnalogRight = leftAnalogRight;

                    old_input = input;
                }
            }
        }
        public void MoveActivateControl()
        {
            ActiveControl1.lstControls[position_x-1][position_y].Focus();
            
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
            this.tbx_console.Size = new System.Drawing.Size(500, 80);
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

        public void ButtonHandler(object sender, EventArgs e, bool addToLst = false)
        {

            ButtonContext contexte = (ButtonContext)sender;
            activeContexte = contexte;
            if (addToLst)
            {
                lstOldContexte.Add(contexte);
            }

            switch (contexte.contexte)
            {
                case "side":
                    LoadNewCategoriePanel(contexte);
                    FocusToMainPanel();
                    break;
                case "home":
                    LoadNewHomePanel();
                    FocusToMainPanel();
                    break;
                default:
                    break;
            }
           
        }

        public void LoadNewCategoriePanel(ButtonContext btn_context)
        {
            testContextUC temp = new testContextUC(this, topPanel, null, null, sidePanel);
            temp.CreateListButton(5, 5);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        public void LoadNewHomePanel()
        {
            TestXboxUserControl temp = new TestXboxUserControl(this, topPanel, null, null, sidePanel);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;
        }

        public void FocusToMainPanel()
        {
            activeControl1 = this.mainPanel;
            activeControl1.position_y = 0;
            activeControl1.position_x = 0;

            XboxMainForm topMainForm = this;
        }



        public void LoadOldMainPanel()
        {

            if (lstOldContexte.Count > 0)
            {
                int lastContext = (lstOldContexte.Count() - 2);
                if (activeContexte.contexte != "home")
                {

                    ButtonHandler(lstOldContexte[lastContext], new EventArgs());
                    lstOldContexte.RemoveAt((lastContext +1));
                    activeControl1.position_x = 0;
                    activeControl1.position_y = 0;
                    ActiveControl1.MoveActivateControl();

                }
            }

        }

        public void CreateTestControls()
        {

            sidePanel = new TestSideBarXboxUserControl(this);
            sidePanel.Location = new Point(0,100);

            topPanel = new TestTopPannelXbox(this);
            topPanel.Location = new Point(0,0);

            MainPanel = new TestXboxUserControl(this, topPanel, null, null, sidePanel);
            sidePanel.right_form = MainPanel;
            MainPanel.Location = new Point(270, 120);

            MainPanel.top_form = topPanel;
            sidePanel.top_form = topPanel;
            topPanel.bottom_form = MainPanel;
            topPanel.left_form = sidePanel;


            lstControls[0].Add(sidePanel);
            lstControls[0].Add(MainPanel);
            lstControls[0].Add(topPanel);
            sidePanel.BringToFront();
            MainPanel.BringToFront();
            topPanel.BringToFront();
            Controls.Add(sidePanel);
            Controls.Add(MainPanel);
            Controls.Add(topPanel);

        }

       

        private void XboxMainForm_Load(object sender, EventArgs e)
        {
            ActiveControl1.MoveActivateControl();
            tbx_console.SetBounds((50), (this.Height - 200), 150, 150);
        }
    }
}
