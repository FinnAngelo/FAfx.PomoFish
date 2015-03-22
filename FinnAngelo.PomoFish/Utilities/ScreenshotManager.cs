using Common.Logging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinnAngelo.Utilities
{
    internal interface IScreenshotManager
    {
        void WriteScreenShots(Screen[] screens, DateTime dateTimeNow);
        void WriteScreenShot(Screen screen, string folder, string fileName);
    }
    internal class ScreenshotManager : IScreenshotManager
    {
        private readonly ILog _log;
        public ScreenshotManager(ILog log)
        {
            _log = log;
        }

        public void WriteScreenShots(Screen[] screens, DateTime dateTimeNow)
        {
            _log.TraceFormat("WriteScreenShots(screens {0}, dateTimeNow {1} )", screens, dateTimeNow);
            Enumerable.Range(0, screens.Length).ToList()
                .ForEach(r =>
                {
                    WriteScreenShot(
                        screens[r],
                        string.Format("{0:yyyyMMdd}", dateTimeNow),
                        string.Format(
                            "{0:yyyyMMdd_hhmmss_ff}_{1}",
                            dateTimeNow,
                            r
                        )
                    );
                });

        }
        public void WriteScreenShot(Screen screen, string folder, string fileName)
        {
            _log.Trace("WriteScreenShot(Screen screen, string folder, string fileName)");

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            Rectangle ScreenSize = screen.Bounds;
            using (Image img = new Bitmap(ScreenSize.Width, ScreenSize.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    try
                    {
                        g.CopyFromScreen(ScreenSize.X, ScreenSize.Y, 0, 0, ScreenSize.Size);
                    }
                    catch (Win32Exception win32Ex)
                    {
                        _log.Error("g.CopyFromScreen Error!!!", win32Ex);
                        throw;
                    }
                    img.Save(folder + "\\" + fileName + ".png", ImageFormat.Png);
                }
            }
        }


    }
}
