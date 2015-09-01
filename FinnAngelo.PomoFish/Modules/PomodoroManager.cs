using Common.Logging;
using FinnAngelo.PomoFish;
using FinnAngelo.PomoFish.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish.Modules
{
    internal enum Pomodoro
    {
        Working,
        Resting,
        Stopped
    }

    internal interface IPomodoroManager : INotify
    {
        void ChangePomodoro(Pomodoro pomodoro, int countdownMinutes);
    }

    internal class PomodoroNotifyIconDetails : INotifyIconDetails
    {
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

        public PomodoroNotifyIconDetails(Pomodoro pomodoro, int countdown)
        {
            BackgroundColor = _PaperColor[pomodoro];
            ForeColor = _InkColor[pomodoro];
            Text = (pomodoro == Pomodoro.Stopped ? "++" : String.Format("{0}", countdown));
        }

        public Color ForeColor { get; private set; }
        public Color BackgroundColor { get; private set; }
        public string Text { get; private set; }
    }

    internal class PomodoroManager : IPomodoroManager
    {
        private readonly ILog _log;
        private readonly IClock _clock;
        DateTime _countdownEnd = DateTime.MinValue;
        Timer _timer = new Timer() { Interval = 1000 };// Timer will tick every 1 second

        public event NotifyHandler Notify;

        Pomodoro _pomodoro = Pomodoro.Stopped;

        public PomodoroManager(
            ILog log,
            IClock clock)
        {
            _log = log;
            _clock = clock;

            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();

        }
        public void ChangePomodoro(Pomodoro pomodoro, int countdownMinutes)
        {
            _log.TraceFormat("ChangePomodoro(pomodoro: {0}, countdownMinutes: {1})", pomodoro, countdownMinutes);

            _pomodoro = pomodoro;
            _countdownEnd = _clock.Now.Add(new TimeSpan(0, countdownMinutes, 0));

            Notify(this, new NotifyEventArgs()
            {
                Alert = new NotifyAlert() { Header = _pomodoro.ToString(), Body = _pomodoro.ToString() },
                IconDetails = null
            });
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            _log.Trace("timer_Tick");

            var now = _clock.Now;

            if (now > _countdownEnd)
            {
                switch (_pomodoro)
                {
                    case Pomodoro.Stopped:
                    case Pomodoro.Resting:
                        ChangePomodoro(Pomodoro.Working, 1);
                        break;
                    case Pomodoro.Working:
                        ChangePomodoro(Pomodoro.Resting, 1);
                        break;
                }
            }

            var countDownSpan = _countdownEnd - now;
            var countDown = (countDownSpan.Minutes == 0 ? countDownSpan.Seconds : countDownSpan.Minutes);
            Notify(this, new NotifyEventArgs()
            {
                Alert = new NotifyAlert() { Header = _pomodoro.ToString(), Body = _pomodoro.ToString() },
                IconDetails = new PomodoroNotifyIconDetails(_pomodoro, countDown)
            });
        }
    }
}
