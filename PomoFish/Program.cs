using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (var timer = new Timer())
                {
                    Application.Run(new frmTimer(timer));
                }
            }
            catch (Exception Ex)
            {
                Debug.Assert(Ex != null, Ex.Message);
                throw;
            }
        }
    }
}
