using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using System;
using System.Diagnostics;
using System.Windows.Forms;

//http://mike.woelmer.com/2009/04/dealing-with-unhandled-exceptions-in-winforms/
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
            MyApplicationContext myApplicationContext = null;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                myApplicationContext = new MyApplicationContext();
                Application.Run(myApplicationContext);
            }
            catch (Exception Ex)
            {
                Debug.Assert(Ex != null, Ex.Message);
                throw;
            }
            finally
            {

                // We must manually tidy up and remove the icon before we exit.
                // Otherwise it will be left behind until the user mouses over.
                myApplicationContext.NotifyIcon.Visible = false;
                myApplicationContext.NotifyIcon.Dispose();
            }


        }
    }
}