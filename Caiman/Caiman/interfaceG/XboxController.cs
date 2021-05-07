/** BDCC
 *  -------
 *  @author Lorenzo Bauduccio <lorenzo.bdcc@eduge.ch>
 *  @file
 *  @copyright Copyright (c) 2021 BDCC
 *  @brief Used to comunicate with all the DirectX input controller connected to the user pc
 */
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
        public List<Controller> lstController = new List<Controller>();
        public List<String> lstInput = new List<String>();
        private string controllers;

        public int valueXboxController;

        public Control overlay;

        public string Controllers { get => controllers; set => controllers = value; }

        /// <summary>
        /// Contructor where you specify the main form of your program
        /// </summary>
        /// <param name="mainFormp"></param>
        public XboxController(Control mainFormp)
        {
            overlay = mainFormp;
            valueXboxController = 0;
            lstController.Add(new Controller(UserIndex.One));
            lstInput.Add("");

        }

        /// <summary>
        /// send input pressed by the users
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// scan the controller connected to the user pc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
