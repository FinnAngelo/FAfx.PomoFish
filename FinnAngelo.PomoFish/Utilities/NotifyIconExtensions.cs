using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsIcon = System.Drawing.Icon;

namespace FinnAngelo.PomoFish
{
    internal static class NotifyIconExtensions
    {
        ////http://msdn.microsoft.com/en-us/library/system.drawing.bitmap.gethicon%28v=vs.110%29.aspx
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        public static void SetDefault(this NotifyIcon notifyIcon)
        {
            var pfIcon = Resources.PomofishIcon;
            using (var img = new Bitmap(pfIcon.ToBitmap(),16,16))
            {
                img.Save("trashmenow.bmp");
                var hicon = img.GetHicon();
                SetIcon(notifyIcon, hicon);

            }
        }

        public static void SetText(this NotifyIcon notifyIcon, string text)
        {
            SetText(notifyIcon: notifyIcon, text: text, foregroundColor: Color.White, backgroundColor: Color.DarkGreen);
        }

        private static void SetText(NotifyIcon notifyIcon, string text, Color foregroundColor, Color backgroundColor)
        {
            using (var img = new Bitmap(16, 16))
            {
                using (var drawing = Graphics.FromImage(img))
                {
                    drawing.Clear(backgroundColor);

                    using (Brush textBrush = new SolidBrush(foregroundColor))
                    {
                        //using (var font = new Font(FontFamily.GenericSansSerif, 14f, FontStyle.Regular, GraphicsUnit.Pixel))
                        using (var font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Regular, GraphicsUnit.Pixel))
                        {
                            //http://msdn.microsoft.com/en-us/library/332kzs7c%28v=vs.110%29.aspx
                            using (var stringFormat =
                                new StringFormat()
                                {
                                    Alignment = StringAlignment.Far,
                                    LineAlignment = StringAlignment.Far
                                }
                                )
                            {
                                var rect = new Rectangle(0, 0, 16, 16);
                                drawing.DrawString(text, font, textBrush, rect, stringFormat);
                                drawing.DrawRectangle(new Pen(backgroundColor), rect);
                                drawing.Save();
                            }
                        }
                    }
                }
                var hicon = img.GetHicon();
                SetIcon(notifyIcon, hicon);

            }
        }

        private static void SetIcon(NotifyIcon notifyIcon, IntPtr hicon)
        {
            if (notifyIcon != null) notifyIcon.Icon = WindowsIcon.FromHandle(hicon);
            DestroyIcon(hicon);
        }

        public static void Notify(this NotifyIcon notifyIcon, string header, string body)
        {
            SystemSounds.Exclamation.Play();

            notifyIcon.Text = header;
            notifyIcon.BalloonTipText = body;
            notifyIcon.ShowBalloonTip(500);
        }
    }
}
