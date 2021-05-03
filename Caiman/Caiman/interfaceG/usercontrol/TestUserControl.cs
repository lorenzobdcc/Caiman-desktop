using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG.usercontrol
{
    public partial class TestUserControl : UserControl
    {
        public string testString;
        public TestUserControl()
        {
            InitializeComponent();

        }

        public TestUserControl(string name)
        {
            InitializeComponent();
            this.testString = name;
            
            Rename();
        }

        public void Rename()
        {
            button1.Text = testString;
            label1.Text = testString;
        }
    }
}
