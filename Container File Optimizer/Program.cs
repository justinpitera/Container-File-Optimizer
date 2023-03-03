using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace Container_File_Optimizer
{

    internal static class Program
    { 
        static Mutex mutex = new Mutex(true, "Container-File-Optimizer");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // If the mutex is not owned by another process, WaitOne() will return true
            // indicating that the current process now owns the mutex.
            // At this point, the application can start running normally.

            // If mutex.WaitOne returns true = not owned by another process
            // if mutex.WaitOne returns false = owned by another process (an instance of the application is already running).
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // No other instance is running
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main());
                }
                finally
                {
                    //Release the mutex so that other instances of the application may aquire it 
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                MessageBox.Show("Another instance of Container File Optimizer is already running. \n" +
                     "Only one instance of Container File Optimizer may run at a time.");
                return;
            }

        }
    }
}
