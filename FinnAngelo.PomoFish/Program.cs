using Common.Logging;
using FinnAngelo.PomoFish.Properties;
using FinnAngelo.PomoFish.Utilities;
using FinnAngelo.Utilities;
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
            container.RegisterSingle<ISettings, Settings>();
            container.RegisterSingle<IIconManager, IconManager>();
            container.RegisterSingle<IScreenshotManager, ScreenshotManager>();
            container.RegisterSingle<IClock, Clock>();
            container.RegisterSingle<MyApplicationContext, MyApplicationContext>();
            container.RegisterSingle<SettingsForm>(
                () => new SettingsForm(
                    container.GetInstance<ILog>(),
                    container.GetInstance<ISettings>()
                        )
                );

            #endregion

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(container.GetInstance<MyApplicationContext>());
                Application.Run(container.GetInstance<SettingsForm>());
            }
            catch (Exception Ex)
            {
                container.GetInstance<ILog>().Error("FinnAngelo.PomoFish.Program > Main()", Ex);
                throw;
            }


        }
    }
}