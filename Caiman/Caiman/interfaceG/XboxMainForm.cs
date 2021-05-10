/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Main classe of the project
 */
using Caiman.database;
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

        public List<ContextInformations> lstOldContexte = new List<ContextInformations>();
        public ContextInformations activeContexte;
        

        public string old_input;

        bool old_leftAnalogUp = false;
        bool old_leftAnalogDown = false;
        bool old_leftAnalogLeft = false;
        bool old_leftAnalogRight = false;

        XboxUserControl mainPanel;
        XboxUserControl topPanel;
        XboxUserControl sidePanel;


        Timer timer = new Timer();

        public AccessDatabase testDatabase = new AccessDatabase();
        

        public XboxUserControl ActiveControl1 { get => activeControl1;set
            {
                old_activeControl = ActiveControl1;
                activeControl1 = value;
            }
        }

        public XboxUserControl MainPanel { get => mainPanel; set
            {
                mainPanel = value;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        
        /// <summary>
        /// Default contructor used to chreate the test form
        /// </summary>
        public XboxMainForm()
        {
            InitializeComponent();
            xboxController = new XboxController(this);
            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());
            CreateBaseControl();
            ActiveControl1 = (XboxUserControl)lstControls[0][0];

            lstOldContexte.Add(new ContextInformations("home", 0, 0, 0));
            
            InitTimer();
            //testDatabase.Select("SELECT * FROM user");
        }

        /// <summary>
        /// Initialise a timer who is gonna call the function used to upade tehe interface and scan the user input
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(UpdateInterface);
            timer.Tick += new EventHandler(ScanInput);


            timer.Interval = 2;
            timer.Start();
        }

        /// <summary>
        /// update the "tbx_console" content 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInterface(object sender, EventArgs e)
        {
            string txt = "";
            txt += xboxController.GetInput();
            txt += "\r\nposition X: " +ActiveControl1.Position_x;
            txt += "\r\nposition Y: " +ActiveControl1.Position_y;
        }

        /// <summary>
        /// Used to know if the application is focused by the user or not
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Used to know what input is pressed by the user
        /// The function will alsa trigger event depend on the user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanInput(object sender, EventArgs e)
        {

            // if the user 1 controller is connected
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
                        ActiveControl1.MoveActivateControl("down");
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
                        ActiveControl1.MoveActivateControl("down");

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


        /// <summary>
        /// Initialise the main form
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XboxMainForm));
            this.SuspendLayout();

            // 
            // XboxMainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.ControlBox = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XboxMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.XboxMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        /// <summary>
        /// Used to modifiy the content of the application by getting the button input values
        /// This function will load diferent windows updated for the right contexte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="addToLst"></param>
        public void ContexteHandler(object sender, EventArgs e, bool addToLst = false)
        {

            ContextInformations contexte = (ContextInformations)sender;
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
                case "testNavigation":
                    LoadNewTestPanel();
                    FocusToMainPanel();
                    break;
                case "testImages":
                    LoadNewImagesPanel();
                    FocusToMainPanel();
                    break;
                case "quitMenu":
                    LoadNewQuitMenu();
                    FocusToMainPanel();
                    break;
                case "configurationMenu":
                    LoadNewConfigurationMenu();
                    FocusToMainPanel();
                    break;
                case "quit":
                    Application.Exit();
                    break;
                case "minimize":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                default:
                    break;
            }
           
        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        /// <param name="btn_context"></param>
        public void LoadNewCategoriePanel(ContextInformations btn_context)
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

        /// <summary>
        /// Load the quit menu
        /// </summary>
        public void LoadNewQuitMenu()
        {
            QuitMenuXbox temp = new QuitMenuXbox(this, topPanel, null, null, sidePanel);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load the configuration menu
        /// </summary>
        public void LoadNewConfigurationMenu()
        {
            ConfigurationMenuXbox temp = new ConfigurationMenuXbox(this, topPanel, null, null, sidePanel);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewTestPanel()
        {
            testNavigationUserControl temp = new testNavigationUserControl(this, topPanel, null, null, sidePanel);
            temp.CreateListBrokenButton(6, 6);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewImagesPanel()
        {
            TestImageUserControl temp = new TestImageUserControl(this, topPanel, null, null, sidePanel);
            temp.CreateListImages(1, 2);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, 120);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
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

        /// <summary>
        /// Used to focus the main panel at position 0,0
        /// </summary>
        public void FocusToMainPanel()
        {
            activeControl1 = this.mainPanel;
            activeControl1.position_y = 0;
            activeControl1.position_x = 0;

            XboxMainForm topMainForm = this;
        }


        /// <summary>
        /// load the previous panel
        /// </summary>
        public void LoadOldMainPanel()
        {

            if (lstOldContexte.Count > 0)
            {
                int lastContext = (lstOldContexte.Count() - 2);
                if (activeContexte.contexte != "home")
                {

                    ContexteHandler(lstOldContexte[lastContext], new EventArgs());
                    lstOldContexte.RemoveAt((lastContext +1));
                    activeControl1.position_x = 0;
                    activeControl1.position_y = 0;
                    ActiveControl1.MoveActivateControl();

                }
            }

        }

        /// <summary>
        /// Used to create the main form content and set the position of each panel
        /// </summary>
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

        public void CreateBaseControl()
        {
            sidePanel = new TestSideBarXboxUserControl(this);
            sidePanel.Location = new Point(0, 100);

            topPanel = new NavbarXbox(this);
            topPanel.Location = new Point(0, 0);

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

       
        /// <summary>
        /// Event call when the main form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XboxMainForm_Load(object sender, EventArgs e)
        {
            ActiveControl1.MoveActivateControl();
        }
    }
}
