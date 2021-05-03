
namespace Caiman
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_pannel_1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.testUserControl1 = new Caiman.interfaceG.usercontrol.TestUserControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btm_pannel_2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_pannel_1
            // 
            this.btn_pannel_1.Location = new System.Drawing.Point(12, 12);
            this.btn_pannel_1.Name = "btn_pannel_1";
            this.btn_pannel_1.Size = new System.Drawing.Size(92, 23);
            this.btn_pannel_1.TabIndex = 0;
            this.btn_pannel_1.Text = "add pannel 1";
            this.btn_pannel_1.UseVisualStyleBackColor = true;
            this.btn_pannel_1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(140, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 487);
            this.panel1.TabIndex = 2;
            // 
            // testUserControl1
            // 
            this.testUserControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.testUserControl1.Location = new System.Drawing.Point(45, 545);
            this.testUserControl1.Name = "testUserControl1";
            this.testUserControl1.Size = new System.Drawing.Size(239, 164);
            this.testUserControl1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Location = new System.Drawing.Point(668, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 487);
            this.panel2.TabIndex = 3;
            // 
            // btm_pannel_2
            // 
            this.btm_pannel_2.Location = new System.Drawing.Point(12, 64);
            this.btm_pannel_2.Name = "btm_pannel_2";
            this.btm_pannel_2.Size = new System.Drawing.Size(92, 23);
            this.btm_pannel_2.TabIndex = 4;
            this.btm_pannel_2.Text = "add pannel 2";
            this.btm_pannel_2.UseVisualStyleBackColor = true;
            this.btm_pannel_2.Click += new System.EventHandler(this.btm_pannel_2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1525, 747);
            this.Controls.Add(this.btm_pannel_2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.testUserControl1);
            this.Controls.Add(this.btn_pannel_1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_pannel_1;
        private interfaceG.usercontrol.TestUserControl testUserControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btm_pannel_2;
    }
}

