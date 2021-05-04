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
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Name = "MainForm";
            this.Controls.SetChildIndex(this.activeControl, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
