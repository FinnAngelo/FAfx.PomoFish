using Common.Logging;
using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish.Utilities
{
    internal interface IIconManager
    {
        void SetNotifyIcon(NotifyIcon notifyIcon, INotifyIconDetails iconDetails);
    }

    internal class IconManager : IIconManager
    {
        private readonly ILog _log;

        public IconManager(ILog log)
        {
            _log = log;
        }
        
        //http://msdn.microsoft.com/en-us/library/system.drawing.bitmap.gethicon%28v=vs.110%29.aspx
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        public void SetNotifyIcon(NotifyIcon notifyIcon, INotifyIconDetails iconDetails)
        {
            _log.TraceFormat("notifyIcon: {0}, iconDetails.Text: {1}, ForeColor: {2}, BackgroundColor: {3}, ", notifyIcon, iconDetails.Text, iconDetails.ForeColor, iconDetails.BackgroundColor);

            
            using (var img = new Bitmap(16, 16))
            {
                using (var drawing = Graphics.FromImage(img))
                {
                    drawing.Clear(iconDetails.BackgroundColor);

                    using (Brush textBrush = new SolidBrush(iconDetails.ForeColor))
                    {
                        using (var font = new Font(FontFamily.GenericSansSerif, 6f))
                        {
                            //http://msdn.microsoft.com/en-us/library/332kzs7c%28v=vs.110%29.aspx
                            using (var stringFormat =
                                new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far }
                                )
                            {
                                var rect = new Rectangle(0, 0, 16, 16);
                                drawing.DrawString(iconDetails.Text, font, textBrush, rect, stringFormat);
                                drawing.DrawRectangle(new Pen(iconDetails.BackgroundColor), rect);
                                drawing.Save();
                            }
                        }
                    }
                }
                var hicon = img.GetHicon();
                if (notifyIcon != null) notifyIcon.Icon = Icon.FromHandle(hicon);
                DestroyIcon(hicon);
            }

        }
    }
}
