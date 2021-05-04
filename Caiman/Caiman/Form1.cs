using Caiman.interfaceG;
using Caiman.interfaceG.usercontrol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman
{
    public partial class Form1 : Form
    {
        List<UserControl> lstuser = new List<UserControl>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

        }
        

        private void button1_Click(object sender, EventArgs e)
        {


            XboxMainForm tuc = new XboxMainForm();
            tuc.Left = 15;
            tuc.Top = 15;
            this.panel1.Controls.Add(tuc);
            tuc.BringToFront();
        }

        private void btm_pannel_2_Click(object sender, EventArgs e)
        {


            XboxMainForm tuc = new XboxMainForm();
            tuc.Left = 15;
            tuc.Top = 15;
            this.panel2.Controls.Add(tuc);
            tuc.BringToFront();
        }

        
    }
}
