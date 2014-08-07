using FAfx.Utilities;
using System;
using System.Diagnostics;
using System.Windows.Forms;

//http://mike.woelmer.com/2009/04/dealing-with-unhandled-exceptions-in-winforms/
namespace FAfx.PomoFish
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region IoC.Register
            IoC.Register<TraceSource>(null,() => new TraceSource("FAfx.PomoFish"));
            IoC.Register<IConfigManager>(null, () => new ConfigManager());
            IoC.Register<IIconManager>(null, () => new IconManager());
            IoC.Register<IScreenshotManager>(null, () => new ScreenshotManager());
            IoC.Register<DateTime>("Now", () => DateTime.Now, ResolveFunc.EveryTime);
            
            #endregion

            IoC.Resolve<TraceSource>().LogEvents(new object[] { }, () =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MyApplicationContext());
            });
        }
    }
}