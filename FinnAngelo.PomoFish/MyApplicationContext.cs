using Common.Logging;
using FinnAngelo.PomoFish.Properties;
using FinnAngelo.PomoFish.Utilities;
using FinnAngelo.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish
{
    enum Pomodoro
    {
        Working,
        Resting,
        Stopped
    }

    internal class MyApplicationContext : ApplicationContext
    {
        private readonly ILog _log;
        private readonly ISettings _settings;
        private readonly IIconManager _iconManager;
        private readonly IScreenshotManager _screenshotManager;
        private readonly IClock _clock;

        NotifyIcon _notifyIcon = new NotifyIcon();
        Timer _timer = new Timer() { Interval = 1000 };// Timer will tick every 1 second

        Pomodoro _pomodoro = Pomodoro.Stopped;
        DateTime _countdownEnd = DateTime.MinValue;

        public MyApplicationContext(
            ILog log,
            ISettings settings,
            IIconManager iconManager,
            IScreenshotManager screenshotManager,
            IClock clock)
        {
            _log = log;
            _settings = settings;
            _iconManager = iconManager;
            _screenshotManager = screenshotManager;
            _clock = clock;

            _log.Trace("MyApplicationContext(ILog log, IConfigManager config, IIconManager iconManager, IScreenshotManager screenshotManager, IDateTimeFactory dateTimeFactory)");

            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenu = new ContextMenu(
                new MenuItem[] {
                    new MenuItem("Restart Pomodoro", new EventHandler(RestartPomodoro)), 
                    new MenuItem("Disable Pomodoro", new EventHandler(DisablePomodoro)), 
                    new MenuItem("Exit", new EventHandler(Exit)) 
                });

            DisablePomodoro(null, null);

            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();

            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        bool _screenShotIsEnabled = true;
        SessionSwitchReason _SessionSwitchReason;
        //http://stackoverflow.com/questions/44980/how-can-i-programmatically-determine-if-my-workstation-is-locked
        public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            _log.Trace("SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)");
            _log.TraceFormat("e.Reason: {0}", e.Reason);
            _SessionSwitchReason = e.Reason;
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _screenShotIsEnabled = false;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _screenShotIsEnabled = true;
                    break;
            }
        }

        public void DisablePomodoro(object sender, EventArgs e)
        {
            _log.Trace("DisablePomodoro");

            ChangePomodoro(Pomodoro.Stopped, DateTime.MaxValue);

        }

        public void RestartPomodoro(object sender, EventArgs e)
        {
            _log.Trace("RestartPomodoro");
            ChangePomodoro(
                Pomodoro.Working,
                _clock.Now.Add(new TimeSpan(0, _settings.WorkingPeriodInMinutes, 0))
            );
        }

        public void RestPomodoro(object sender, EventArgs e)
        {
            _log.Trace("RestPomodoro");
            ChangePomodoro(
                Pomodoro.Resting,
                _clock.Now.Add(new TimeSpan(0, _settings.RestPeriodInMinutes, 0))
            );

        }

        public void ChangePomodoro(Pomodoro pomodoro, DateTime countdownEnd)
        {
            _log.TraceFormat("ChangePomodoro(pomodoro: {0}, countdownEnd: {1})", pomodoro, countdownEnd);

            _pomodoro = pomodoro;
            _countdownEnd = countdownEnd;

            if (_settings.PlaySound) SystemSounds.Exclamation.Play();
            _notifyIcon.Text = _pomodoro.ToString();
            _notifyIcon.BalloonTipText = _pomodoro.ToString();
            _notifyIcon.ShowBalloonTip(1000);

        }

        public void timer_Tick(object sender, EventArgs e)
        {
            _log.Trace("timer_Tick");

            var now = _clock.Now;
            if (now.Second == 0 && _screenShotIsEnabled)
            {
                try
                {
                    _screenshotManager.WriteScreenShots(Screen.AllScreens, now);
                }
                catch
                {
                    _log.ErrorFormat("_SessionSwitchReason: {0}", _SessionSwitchReason);
                    throw;
                }
            }

            if (now > _countdownEnd)
            {
                switch (_pomodoro)
                {
                    case Pomodoro.Stopped:
                    case Pomodoro.Resting:
                        RestartPomodoro(sender, e);
                        break;
                    case Pomodoro.Working:
                        RestPomodoro(sender, e);
                        break;
                }
            }

            var countDownSpan = _countdownEnd - now;
            var countDown = (countDownSpan.Minutes == 0 ? countDownSpan.Seconds : countDownSpan.Minutes);
            _iconManager.SetNotifyIcon(_notifyIcon, _pomodoro, countDown);
        }

        public void Exit(object sender, EventArgs e)
        {
            _log.Trace("Exit");

            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
