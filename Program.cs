using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Threading;

namespace DX1Utility
{



    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (Mutex mutex = new Mutex(false, "Global\\ewDx1Utility"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Dx1Utility is already running","Error");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form1 form = new Form1();
                Application.Run(form);
            }

        }

    }
}
