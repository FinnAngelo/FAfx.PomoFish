using FinnAngelo.PomoFish.Winform.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish.Winform
{
    public partial class Form1 : Form
    {        
        DateTime _countdownEnd;

        public Form1()
        {
            InitializeComponent();

            Timer timer = new Timer() { Interval = 1000 };// Timer will tick every 1 second
            timer.Tick += timer_Tick;

            _countdownEnd = DateTime.Now.AddMinutes(Settings.Default.DurationInMinutes);
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            if (now > _countdownEnd)
            {
                LockWorkStation();
                Application.Exit();
            }
            else
            {
                var countDownSpan = _countdownEnd - now;
                int countDown;
                if (countDownSpan.Minutes == 0 )
                {
                    if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
                    countDown = countDownSpan.Seconds; 
                }
                else 
                {
                    countDown = countDownSpan.Minutes;
                };
                label1.Text = countDown.ToString();
                this.Text = countDown.ToString();
            }
        }

        //http://www.codeproject.com/Questions/337619/Lock-computer-using-csharp-in-window-application
        [DllImport("user32.dll")]
        internal static extern void LockWorkStation();

    }
}