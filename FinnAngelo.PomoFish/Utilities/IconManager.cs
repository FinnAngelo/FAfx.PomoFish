using Common.Logging;
using FinnAngelo.PomoFish;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FinnAngelo.Utilities
{
    internal interface IIconManager
    {
        void SetNotifyIcon(NotifyIcon notifyIcon, Pomodoro pomodoro, Int32 countDown);
    }

    internal class IconManager : IIconManager
    {
        private readonly ILog _log;

        #region Colors
        private readonly static Dictionary<Pomodoro, Color> _PaperColor = new Dictionary<Pomodoro, Color>() 
        {
            {Pomodoro.Stopped, SystemColors.GradientInactiveCaption},
            {Pomodoro.Resting, SystemColors.InactiveCaption},
            {Pomodoro.Working, SystemColors.Highlight}
        };

        private readonly static Dictionary<Pomodoro, Color> _InkColor = new Dictionary<Pomodoro, Color>() 
        {
            {Pomodoro.Stopped, SystemColors.InactiveCaptionText},
            {Pomodoro.Resting, SystemColors.InactiveCaptionText},
            {Pomodoro.Working, SystemColors.HighlightText}
        };

        #endregion

        public IconManager(ILog log)
        {
            _log = log;
        }
        
        //http://msdn.microsoft.com/en-us/library/system.drawing.bitmap.gethicon%28v=vs.110%29.aspx
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        public void SetNotifyIcon(NotifyIcon notifyIcon, Pomodoro pomodoro, Int32 countdown)
        {
            _log.TraceFormat("notifyIcon: {0}, pomodoro: {1}, countdown: {2}", notifyIcon, pomodoro, countdown);

            var paper = _PaperColor[pomodoro];
            var ink = _InkColor[pomodoro];
            var text = (pomodoro == Pomodoro.Stopped ? "++" : String.Format("{0}", countdown));

            using (var img = new Bitmap(16, 16))
            {
                using (var drawing = Graphics.FromImage(img))
                {
                    drawing.Clear(paper);

                    using (Brush textBrush = new SolidBrush(ink))
                    {
                        using (var font = new Font(FontFamily.GenericSansSerif, 8f))
                        {
                            //http://msdn.microsoft.com/en-us/library/332kzs7c%28v=vs.110%29.aspx
                            using (var stringFormat =
                                new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far }
                                )
                            {
                                var rect = new Rectangle(0, 0, 16, 16);
                                drawing.DrawString(text, font, textBrush, rect, stringFormat);
                                drawing.DrawRectangle(new Pen(paper), rect);
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
