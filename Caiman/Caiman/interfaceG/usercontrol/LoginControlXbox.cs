/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to display the login menu
 */
using Caiman.interfaceG.XboxControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    class LoginControlXbox : XboxUserControl
    {
        public TextBox tbx_username;
        public TextBox tbx_password;
        public Label lbl_error;

        /// <summary>
        /// contrucot with next panel specify
        /// </summary>
        /// <param name="xboxMain"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        public LoginControlXbox(XboxMainForm xboxMain, XboxUserControl top, XboxUserControl bottom, XboxUserControl right, XboxUserControl left) : base(xboxMain, top, bottom, right, left)
        {
            InitializeComponent();
            CreateLoginForm();
        }

        /// <summary>
        /// Initialise the panel
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // 
            // testContextUC
            // 
            this.Name = "login form";
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width ,Screen.PrimaryScreen.Bounds.Height);
            this.BackColor = Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        /// <summary>
        /// Create the view to log in 
        /// </summary>
        public void CreateLoginForm()
        {
            int position_first_column = ((Screen.PrimaryScreen.Bounds.Width/ 2 ) -150);
            int position_first_row = (Screen.PrimaryScreen.Bounds.Height / 2) ;

            PictureBox logoCaiman = new PictureBox();
            Image img = new Bitmap(Caiman.Properties.Resources.logo_caiman_big);
            logoCaiman.Location = new System.Drawing.Point(position_first_column - 250, position_first_row -400);
            logoCaiman.Width = 700;
            logoCaiman.Height = 300;
            logoCaiman.BackgroundImageLayout = ImageLayout.Stretch;
            logoCaiman.SizeMode = PictureBoxSizeMode.StretchImage;
            logoCaiman.Image = img;
            Controls.Add(logoCaiman);

            Label lbl_configuration = new Label();
            lbl_configuration.Text = "Username";
            lbl_configuration.Location = new System.Drawing.Point((position_first_column -75), (position_first_row));
            lbl_configuration.Width = 150;
            lbl_configuration.Font = new Font("Arial", 14);
            lbl_configuration.ForeColor = Color.White;
            Controls.Add(lbl_configuration);

            Label lbl_login = new Label();
            lbl_login.Text = "Password";
            lbl_login.Location = new System.Drawing.Point((position_first_column -75), (position_first_row + 100));
            lbl_login.Width = 150;
            lbl_login.Font = new Font("Arial", 14);
            lbl_login.ForeColor = Color.White;
            Controls.Add(lbl_login);

            tbx_username = new TextBox();
            tbx_username.Text = "";
            tbx_username.Location = new System.Drawing.Point((position_first_column + 100), (position_first_row));
            tbx_username.Width = 150;
            tbx_username.Font = new Font("Arial", 14);
            Controls.Add(tbx_username);

            tbx_password = new TextBox();
            tbx_password.Text = "";
            tbx_password.PasswordChar = '*';
            tbx_password.Location = new System.Drawing.Point((position_first_column + 100), (position_first_row + 100));
            tbx_password.Width = 150;
            tbx_password.Font = new Font("Arial", 14);
            Controls.Add(tbx_password);

            lbl_error = new Label();
            lbl_error.Text = "";
            lbl_error.Location = new System.Drawing.Point((position_first_column - 75), (position_first_row + 200));
            lbl_error.Width = 150;
            lbl_error.Font = new Font("Arial", 14);
            lbl_error.ForeColor = Color.Red;
            Controls.Add(lbl_error);



            lstControls.Add(new List<Control>());
            lstControls.Add(new List<Control>());




            XboxButton btn_login = new XboxButton("login", 2, 2, 0);
            
            btn_login.Text = "Login";
            btn_login.Width = 150;
            btn_login.Location = new System.Drawing.Point((position_first_column -75), (position_first_row + 150  ));
            Controls.Add(btn_login);
            btn_login.Click += new System.EventHandler(bouton_Click);

            XboxButton btn_newAccount = new XboxButton("newAccount", 2, 2, 0);
            btn_newAccount.Text = "New account";
            btn_newAccount.Width = 150;
            btn_newAccount.Location = new System.Drawing.Point((position_first_column + 100), (position_first_row + 150));
            Controls.Add(btn_newAccount);
            btn_newAccount.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(btn_login);
            lstControls[0].Add(btn_newAccount);

            XboxButton btn_quit = new XboxButton("quit", 0, 2, 1);

            btn_quit.Text = "Quit";
            btn_quit.Width = 150;
            btn_quit.Location = new System.Drawing.Point((position_first_column - 75), (position_first_row + 250));
            Controls.Add(btn_quit);
            btn_quit.Click += new System.EventHandler(bouton_Click);


            lstControls[1].Add(btn_quit);


        }
        /// <summary>
        /// override the default click event to send the context to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void  bouton_Click(object sender, EventArgs e)
        {
            
            XboxButton tempXboxButton = (XboxButton)sender;
            tempXboxButton.contextInfos.optionalString1 = tbx_username.Text;
            tempXboxButton.contextInfos.optionalString2 = tbx_password.Text;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }
    }
}
