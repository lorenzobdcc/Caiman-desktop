using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caiman.interfaceG
{
    class MainForm : XboxMainForm
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Name = "MainForm";
            this.Controls.SetChildIndex(this.activeControl, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
