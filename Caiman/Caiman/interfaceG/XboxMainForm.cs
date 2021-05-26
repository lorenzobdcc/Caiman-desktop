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
using Caiman.logique;
using Caiman.models;

namespace Caiman.interfaceG
{
    public class XboxMainForm : Form
    {
        const int HEIGHT_NAVBAR = 60;

        List<List<Control>> lstControls = new List<List<Control>>();
        XboxController xboxController;

        public EmulatorsManager  emulatorsManager = new EmulatorsManager();

        private XboxUserControl activeControl1;
        public XboxUserControl old_activeControl;

        public List<ContextInformations> lstOldContexte = new List<ContextInformations>();
        public ContextInformations activeContexte;

        public CallAPI callAPI = new CallAPI();

        public string old_input;

        bool old_leftAnalogUp = false;
        bool old_leftAnalogDown = false;
        bool old_leftAnalogLeft = false;
        bool old_leftAnalogRight = false;

        XboxUserControl mainPanel;
        XboxUserControl topPanel;
        XboxUserControl sidePanel;


        Timer timer = new Timer();

        

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
            CreateLoginControls();
            ActiveControl1 = (XboxUserControl)lstControls[0][0];

            lstOldContexte.Add(new ContextInformations("home", 0, 0, 0));
            
            InitTimer();
        }

        /// <summary>
        /// Initialise a timer who is gonna call the function used to upade tehe interface and scan the user input
        /// </summary>
        public void InitTimer()
        {
            timer = new Timer();
            timer.Tick += new EventHandler(ScanInput);


            timer.Interval = 2;
            timer.Start();
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
                case "play":
                    emulatorsManager.StartGame(contexte.id_contexte);
                    break;
                case "home":
                    LoadNewHomePanel();
                    FocusToMainPanel();
                    break;
                case "favorite":
                    LoadNewListGamesFromUserFavorite();
                    FocusToMainPanel();
                    break;
                case "downloadedGames":
                    LoadNewListGamesDownloadedGames();
                    FocusToMainPanel();
                    break;
                case "category":
                    LoadNewListGamesFromCategory(contexte.id_contexte);
                    FocusToMainPanel();
                    break;
                case "quitMenu":
                    LoadNewQuitMenu();
                    FocusToMainPanel();
                    break;
                case "downloadList":
                    LoadNewDownloadList();
                    FocusToMainPanel();
                    break;
                case "game":
                    LoadNewGameDetails(contexte.id_contexte);
                    FocusToMainPanel();
                    break;
                case "configurationMenu":
                    LoadNewConfigurationMenu();
                    FocusToMainPanel();
                    break;
                case "updateGlobalConfiguration":
                    emulatorsManager.ApplyGlobalConfiguration(contexte.optionalString1);
                    ContextInformations tempContexteConfigurationGlobal = new ContextInformations();
                    tempContexteConfigurationGlobal.contexte = "configurationMenu";
                    tempContexteConfigurationGlobal.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteConfigurationGlobal, null);
                    break;
                case "updateFullscreenConfiguration":
                    emulatorsManager.ApplyFullscreenConfiguration(contexte.id_contexte);
                    ContextInformations tempContexteConfigurationFullscreen = new ContextInformations();
                    tempContexteConfigurationFullscreen.contexte = "configurationMenu";
                    tempContexteConfigurationFullscreen.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteConfigurationFullscreen, null);
                    break;
                case "updateFormatConfiguration":
                    emulatorsManager.ApplyFormatConfiguration(contexte.id_contexte);
                    ContextInformations tempContexteConfigurationFormat = new ContextInformations();
                    tempContexteConfigurationFormat.contexte = "configurationMenu";
                    tempContexteConfigurationFormat.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteConfigurationFormat, null);
                    break;
                case "addFavorite":
                    callAPI.AddGameToFavorite(contexte.id_contexte, emulatorsManager.user.id);
                    ContextInformations tempContexteAdd = new ContextInformations();
                    tempContexteAdd.contexte = "game";
                    tempContexteAdd.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteAdd, null);
                    break;
                case "removeFavorite":
                    callAPI.RemoveGameFromFavorite(contexte.id_contexte, emulatorsManager.user.id);
                    ContextInformations tempContexteRemove = new ContextInformations();
                    tempContexteRemove.contexte = "game";
                    tempContexteRemove.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteRemove, null);
                    break;
                case "download":
                    emulatorsManager.downloadManager.CreateDownload(contexte.id_contexte,emulatorsManager.user.apitoken);
                    emulatorsManager.downloadManager.StartDownload();
                    ContextInformations tempContexte = new ContextInformations();
                    tempContexte.contexte = "downloadList";
                    this.ContexteHandler(tempContexte, null);
                    break;
                case "delete":
                    emulatorsManager.downloadManager.DeleteGame(contexte.id_contexte);
                    ContextInformations tempContexteDelete = new ContextInformations();
                    tempContexteDelete.contexte = "game";
                    tempContexteDelete.id_contexte = contexte.id_contexte;
                    this.ContexteHandler(tempContexteDelete, null);
                    break;
                case "login":
                    emulatorsManager.user.Login(contexte.optionalString1, contexte.optionalString2);

                    if (emulatorsManager.user.id == 0)
                    {
                        LoginControlXbox tempLogin = (LoginControlXbox)mainPanel;
                        tempLogin.lbl_error.Text = "Invalid login";
                    }else
                    {
                        CreateBaseControl();
                        FocusToMainPanel();
                    }
                    break;
                case "newAccount":
                    System.Diagnostics.Process.Start("http://caiman.cfpt.info/");
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
        /// Load the quit menu
        /// </summary>
        public void LoadNewQuitMenu()
        {
            QuitMenuXbox temp = new QuitMenuXbox(this, topPanel, null, null, sidePanel);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
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

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load the configuration menu
        /// </summary>
        public void LoadNewDownloadList()
        {
            DownloadListXbox temp = new DownloadListXbox(this, topPanel, null, null, sidePanel);
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }


        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewListGamesFromCategory(int idCategory)
        {
            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = callAPI.CallGamesFromCategory(idCategory);
            temp.Width = (Screen.PrimaryScreen.Bounds.Width - 200);
            temp.Height = (Screen.PrimaryScreen.Bounds.Height - 150);
            temp.CreateListGames();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewGameDetails(int idGame)
        {
            GameDetailsXbox temp = new GameDetailsXbox(this, topPanel, null, null, sidePanel);
            temp.Width = (Screen.PrimaryScreen.Bounds.Width - 200);
            temp.Height = (Screen.PrimaryScreen.Bounds.Height - 150);
            temp.LoadGameDetail(idGame);
            temp.CreateViewGame();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewListGamesFromUserFavorite()
        {
            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = callAPI.CallUserFavoriteGames(emulatorsManager.user.id);
            temp.CreateListGames();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;

        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewListGamesDownloadedGames()
        {
            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = new List<Game>();
            foreach (var idGamesString in emulatorsManager.gamesListConfigFile.GetAllValueInList())
            {
                temp.lst_games.Add(callAPI.CallOneGame(Convert.ToInt32(idGamesString)));
            }
            temp.CreateListGames();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;
            
            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;

        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewPanelAllGames()
        {
            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = callAPI.CallAllGames();
            temp.CreateListGames();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
            Controls.Add(MainPanel);

            sidePanel.right_form = MainPanel;
            topPanel.bottom_form = MainPanel;


        }

        /// <summary>
        /// Load a spécific categorie
        /// </summary>
        public void LoadNewHomePanel()
        {

            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = callAPI.CallAllGames();
            temp.CreateListGames();
            Controls.Remove(mainPanel);
            mainPanel.Dispose();
            MainPanel = temp;

            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);
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
        public void CreateLoginControls()
        {

            MainPanel = new LoginControlXbox(this, null, null, null, null);
            MainPanel.Location = new Point(0, 0);


            lstControls[0].Add(MainPanel);

            MainPanel.BringToFront();
            Controls.Add(MainPanel);

        }

        public void CreateBaseControl()
        {
            
            sidePanel = new SideBarXbox(this);
            sidePanel.Location = new Point(0, HEIGHT_NAVBAR);

            topPanel = new NavbarXbox(this);
            topPanel.Location = new Point(0, 0);

            Controls.Remove(mainPanel);
            //Create main pannel
            ListGameXbox temp = new ListGameXbox(this, topPanel, null, null, sidePanel);
            temp.lst_games = callAPI.CallAllGames();
            temp.CreateListGames();

            mainPanel = temp;
            sidePanel.right_form = MainPanel;
            MainPanel.Location = new Point(270, HEIGHT_NAVBAR);

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
            FocusToMainPanel();
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
