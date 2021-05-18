/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to test if i can load an image from the web
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

        public void CreateLoginForm()
        {
            int postion_first_column = ((Screen.PrimaryScreen.Bounds.Width/ 2 ));


            Label lbl_configuration = new Label();
            lbl_configuration.Text = "Username";
            lbl_configuration.Location = new System.Drawing.Point((postion_first_column -75), ((Screen.PrimaryScreen.Bounds.Height / 2) ));
            lbl_configuration.Width = 150;
            lbl_configuration.Font = new Font("Arial", 14);
            lbl_configuration.ForeColor = Color.White;
            Controls.Add(lbl_configuration);

            Label lbl_login = new Label();
            lbl_login.Text = "Password";
            lbl_login.Location = new System.Drawing.Point((postion_first_column -75), ((Screen.PrimaryScreen.Bounds.Height / 2) + 100));
            lbl_login.Width = 150;
            lbl_login.Font = new Font("Arial", 14);
            lbl_login.ForeColor = Color.White;
            Controls.Add(lbl_login);

            tbx_username = new TextBox();
            tbx_username.Text = "lorenzo1227";
            tbx_username.Location = new System.Drawing.Point((postion_first_column + 100), ((Screen.PrimaryScreen.Bounds.Height / 2 )));
            tbx_username.Width = 150;
            tbx_username.Font = new Font("Arial", 14);
            Controls.Add(tbx_username);

            tbx_password = new TextBox();
            tbx_password.Text = "Super2016";
            tbx_password.PasswordChar = '*';
            tbx_password.Location = new System.Drawing.Point((postion_first_column + 100), ((Screen.PrimaryScreen.Bounds.Height / 2) +100));
            tbx_password.Width = 150;
            tbx_password.Font = new Font("Arial", 14);
            Controls.Add(tbx_password);

            lbl_error = new Label();
            lbl_error.Text = "";
            lbl_error.Location = new System.Drawing.Point((postion_first_column - 75), ((Screen.PrimaryScreen.Bounds.Height / 2) + 200));
            lbl_error.Width = 150;
            lbl_error.Font = new Font("Arial", 14);
            lbl_error.ForeColor = Color.Red;
            Controls.Add(lbl_error);



            lstControls.Add(new List<Control>());




            XboxButton btn_login = new XboxButton("login", 2, 2, 0);
            
            btn_login.Text = "Login";
            btn_login.Width = 100;
            btn_login.Location = new System.Drawing.Point((postion_first_column -75), ((Screen.PrimaryScreen.Bounds.Height / 2) + 150  ));
            Controls.Add(btn_login);
            btn_login.Click += new System.EventHandler(bouton_Click);


            lstControls[0].Add(btn_login);


        }
        private void bouton_Click(object sender, EventArgs e)
        {
            
            XboxButton tempXboxButton = (XboxButton)sender;
            tempXboxButton.contextInfos.optionalString1 = tbx_username.Text;
            tempXboxButton.contextInfos.optionalString2 = tbx_password.Text;
            ContextInformations tempButtonContext = tempXboxButton.contextInfos;
            xboxMainForm.ContexteHandler(tempButtonContext, e, true);
        }
    }
}
