using Common.Logging;
using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using FinnAngelo.PomoFish.Utilities;
using SimpleInjector;
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
            #region SimpleInjector.Register

            var container = new Container();

            container.RegisterSingle<ILog>(() => LogManager.GetCurrentClassLogger());
            container.RegisterSingle<IClock, Clock>();
            container.RegisterSingle<IIconManager, IconManager>();
            container.RegisterSingle<IPomodoroManager, PomodoroManager>();
            container.RegisterSingle<MyApplicationContext, MyApplicationContext>();

            #endregion

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.GetInstance<MyApplicationContext>());
            }
            catch (Exception Ex)
            {
                container.GetInstance<ILog>().Error("FinnAngelo.PomoFish.Program > Main()", Ex);
                throw;
            }


        }
    }
}