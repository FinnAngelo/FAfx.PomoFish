using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAfx.Utilities
{
    internal interface IScreenshotManager
    {
        void WriteScreenShots(Screen[] screens, DateTime? dateTimeNow = null);
        void WriteScreenShot(Screen screen, string folder, string fileName);
    }
    internal class ScreenshotManager : IScreenshotManager
    {
        private static readonly TraceSource _traceSource = IoC.Resolve<TraceSource>();
        public ScreenshotManager()
        {
        }

        public void WriteScreenShots(Screen[] screens, DateTime? dateTimeNow = null)
        {
            _traceSource.LogEvents(new object[] { screens, dateTimeNow }, () =>
            {
                dateTimeNow = dateTimeNow ?? IoC.Resolve<DateTime>("Now");

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
            });
        }
        public void WriteScreenShot(Screen screen, string folder, string fileName)
        {
            _traceSource.LogEvents(new object[] { screen, folder, fileName }, () =>
            {
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
                            _traceSource.TraceData(TraceEventType.Error, 0,
                                new object[] {
                                    win32Ex.ToLogString()
                                }
                                );
                            _traceSource.TraceData(TraceEventType.Error, 0, ScreenSize.IsEmpty);
                            _traceSource.TraceData(TraceEventType.Error, 0,
                                new object[] {
                                    ScreenSize,
                                    ScreenSize.Bottom,
                                    ScreenSize.Height,
                                    ScreenSize.IsEmpty,
                                    ScreenSize.Left,
                                    ScreenSize.Location,
                                    ScreenSize.Right,
                                    ScreenSize.Top,
                                    ScreenSize.Width,
                                    ScreenSize.X,
                                    ScreenSize.Y
                                });

                            _traceSource.TraceData(TraceEventType.Error, 0, ScreenSize.Size.IsEmpty);
                            _traceSource.TraceData(TraceEventType.Error, 0,
                                new object[] {
                                    ScreenSize.Size,
                                    ScreenSize.Size.Height,
                                    ScreenSize.Size.Width
                                });

                            throw;
                        }
                        img.Save(folder + "\\" + fileName + ".png", ImageFormat.Png);
                    }
                }
            });
        }

        
    }
}
