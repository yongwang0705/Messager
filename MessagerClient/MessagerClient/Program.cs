using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool test = false;
            if (test == true)
            {
                Client testclient = new Client();
                testclient.id = 1;
                testclient.name = "wy";
                Socket testsocket = null;
                Application.Run(new FrmUserList(testsocket, testclient));
            }
            else
            {
                Application.Run(new LoginForm());
            }
        } 
    }
}
