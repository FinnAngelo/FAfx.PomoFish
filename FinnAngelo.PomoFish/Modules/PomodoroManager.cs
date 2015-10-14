using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish.Modules
{
    [Flags]
    [Serializable]
    [TypeConverter(typeof(EnumConverter))]
    public enum OnComplete : int
    {
        DoNothing = 1,
        Alert = 1 << 1,
        LockScreen = 1 << 2,
        SomeThingElse = 1 << 3
    }

    internal class PomodoroManager
    {
        readonly Func<DateTime> GetNow;
        readonly Func<int> GetDurationInMinutes;

        readonly Action SetIconDefault;
        readonly Action<string> SetIconText;
        readonly Action<string, string> Notify;
        readonly Action LockPC;

        DateTime _countdownEnd = DateTime.MinValue;        

        public PomodoroManager(NotifyIcon notifyIcon)
            : this(
                getNow: () => DateTime.Now,
                getDurationInMinutes: () => Settings.Default.PomodoroLength,
                setIconDefault: () => notifyIcon.SetDefault(),
                setIconText: (s) => notifyIcon.SetText(s),
                notify: (h, b) => notifyIcon.Notify(h, b),
                lockPC: () => { }
                )
        {
        }

        public PomodoroManager(
            Func<DateTime> getNow,
            Func<int> getDurationInMinutes,
            Action setIconDefault,
            Action<string> setIconText,
            Action<string, string> notify,
            Action lockPC
            )
        {
            GetNow = getNow;
            GetDurationInMinutes = getDurationInMinutes;
            SetIconDefault = setIconDefault;
            SetIconText = setIconText;
            Notify = notify;
            LockPC = lockPC;

            SetIconDefault();
        }

        public void StartPomodoro()
        {
            _countdownEnd = GetNow().AddMinutes(GetDurationInMinutes());
        }

        public void StopPomodoro()
        {
            Notify("Stopped", "Stopped");
            _countdownEnd = DateTime.MinValue;
        }
        
        internal void Tick(object sender, EventArgs e)
        {
            var now = GetNow();

            if (now > _countdownEnd)
            {
                StopPomodoro();
            }
            else
            {
                var countDownSpan = _countdownEnd - now;
                var countDown = (countDownSpan.Minutes == 0 ? countDownSpan.Seconds : countDownSpan.Minutes);
                SetIconText(countDown.ToString());
            }
        }
    }
}
