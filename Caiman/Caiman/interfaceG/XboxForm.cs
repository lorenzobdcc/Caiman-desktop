using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG
{
    public class XboxForm : UserControl
    {

        List<List<Control>> lstControls = new List<List<Control>>();

        public XboxForm top_form;
        public XboxForm bottom_form;
        public XboxForm right_form;
        public XboxForm left_form;



        public int position_x;
        public int position_y;

        public const int WIDTH_BUTTON = 75;
        public const int HEIGHT_BUTTON = 23;

        public XboxForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XboxForm
            // 
            this.Name = "XboxForm";
            this.Size = new System.Drawing.Size(300, 300);
            this.ResumeLayout(false);
            CreateListButton(5,5);

        }

        public void CreateListButton(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                lstControls.Add(new List<Control>());
            }
            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    lstControls[x].Add(new Button());
                }
            }

            for (int a = 0; a < column; a++)
            {
                for (int b = 0; b < row; b++)
                {

                    lstControls[a][b].Text = a + " " + b;
                    lstControls[a][b].Location = new System.Drawing.Point(a * 100 + 15, b * 60 + 15);
                    lstControls[a][b].Height = HEIGHT_BUTTON;
                    lstControls[a][b].Width = WIDTH_BUTTON;
                    lstControls[a][b].BackColor = Color.White;
                    lstControls[a][b].Name = a + " " + b;
                    lstControls[a][b].ForeColor = Color.Black;
                    Controls.Add(lstControls[a][b]);
                }
            }
        }
    }
}
