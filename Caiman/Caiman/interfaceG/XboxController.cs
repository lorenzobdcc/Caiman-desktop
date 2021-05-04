using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caiman.interfaceG
{
    class XboxController
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);


        public List<Controller> lstController = new List<Controller>();
        public List<String> lstInput = new List<String>();
        string old_input = "";
        private string controllers;

        public int valueXboxController;

        public Control overlay;

        public string Controllers { get => controllers; set => controllers = value; }

        public XboxController(Control mainFormp)
        {
            overlay = mainFormp;
            valueXboxController = 0;
            lstController.Add(new Controller(UserIndex.One));

            lstInput.Add("");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanInput(object sender, EventArgs e)
        {
            for (int i = 0; i < lstController.Count; i++)
            {
                if (lstController[i].IsConnected)
                {
                    lstInput[i] = lstController[i].GetState().Gamepad.Buttons.ToString();
                }
            }

            if (lstController[0].IsConnected)
            {
                string input = lstController[0].GetState().Gamepad.Buttons.ToString();





                if (input != "Y" && old_input == "Y")
                {

                }
                if (input != "Start" && old_input == "Start")
                {

                }
                if (input != "DPadLeft" && old_input == "DPadLeft")
                {

                    valueXboxController--;

                }
                if (input != "DPadRight" && old_input == "DPadRight")
                {
                    valueXboxController++;

                }
                if (input != "DPadUp" && old_input == "DPadUp")
                {

                    valueXboxController -= 10;
                }
                if (input != "DPadDown" && old_input == "DPadDown")
                {
                    valueXboxController += 10;

                }
                if (input != "A" && old_input == "A")
                {
                    
                }




                old_input = input;
            }


        }



        public string GetInput()
        {
            string txt = "";
            for (int i = 0; i < lstController.Count; i++)
            {
                if (lstController[i].IsConnected)
                {
                    if (lstController[i].GetState().Gamepad.Buttons.ToString() != "None")
                    {
                        txt += "Controller: " + (i + 1) + " " + lstController[i].GetState().Gamepad.Buttons.ToString();
                    }
                }
            }
            return txt;
        }

        public void ScanController(object sender, EventArgs e)
        {
            Controllers = "";
            foreach (var item in lstController)
            {
                Controllers += "user: " + item.UserIndex + " connected: " + item.IsConnected + "\r\n";
            }
        }


    }
}
