﻿using Caiman.interfaceG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            XboxMainForm xboxMainForm = new XboxMainForm();
            xboxMainForm.StartPosition = FormStartPosition.Manual;
            xboxMainForm.Location = Screen.PrimaryScreen.Bounds.Location;
            Application.Run(xboxMainForm);

        }
    }
}
