using FAfx.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace FAfx.PomoFish
{
    enum Pomodoro
    {
        Working,
        Resting,
        Stopped
    }

    class MyApplicationContext : ApplicationContext
    {
        private static readonly TraceSource _traceSource = IoC.Resolve<TraceSource>();
        private static readonly IConfigManager _config = IoC.Resolve<IConfigManager>();
        private static readonly IIconManager _iconManager = IoC.Resolve<IIconManager>();
        private static readonly IScreenshotManager _screenshotManager = IoC.Resolve<IScreenshotManager>();

        NotifyIcon _notifyIcon = new NotifyIcon();
        Timer _timer = new Timer() { Interval = 1000 };// Timer will tick every 1 second

        Pomodoro _pomodoro = Pomodoro.Stopped;
        DateTime _countdownEnd = DateTime.MinValue;

        internal MyApplicationContext()
        {
            _traceSource.LogEvents(new object[] { }, () =>
            {
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

            });
        }

        bool _screenShotIsEnabled = true;
        SessionSwitchReason _SessionSwitchReason;
        //http://stackoverflow.com/questions/44980/how-can-i-programmatically-determine-if-my-workstation-is-locked
        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e }, () =>
            {
                _traceSource.TraceInformation(e.Reason.ToString());
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
            });
        }

        void DisablePomodoro(object sender, EventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e }, () =>
            {
                ChangePomodoro(Pomodoro.Stopped, DateTime.MaxValue);
            });
        }

        void RestartPomodoro(object sender, EventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e }, () =>
            {
                ChangePomodoro(
                    Pomodoro.Working,
                    IoC.Resolve<DateTime>("Now").Add(new TimeSpan(0, _config.WorkingPeriodInMinutes, 0))
                );
            });
        }

        void RestPomodoro(object sender, EventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e }, () =>
            {
                ChangePomodoro(
                    Pomodoro.Resting,
                    IoC.Resolve<DateTime>("Now").Add(new TimeSpan(0, _config.RestPeriodInMinutes, 0))
                );
            });
        }

        void ChangePomodoro(Pomodoro pomodoro, DateTime countdownEnd)
        {
            _traceSource.LogEvents(new object[] { pomodoro, countdownEnd }, () =>
            {
                _pomodoro = pomodoro;
                _countdownEnd = countdownEnd;

                if (_config.PlaySound) SystemSounds.Exclamation.Play();
                _notifyIcon.Text = _pomodoro.ToString();
                _notifyIcon.BalloonTipText = _pomodoro.ToString();
                _notifyIcon.ShowBalloonTip(1000);
            });
        }

        void timer_Tick(object sender, EventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e, _SessionSwitchReason }, () =>
            {
                var now = IoC.Resolve<DateTime>("Now");
                if (now.Second == 0 && _screenShotIsEnabled)
                {
                    _screenshotManager.WriteScreenShots(Screen.AllScreens, now);
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

            });
        }

        void Exit(object sender, EventArgs e)
        {
            _traceSource.LogEvents(new object[] { sender, e }, () =>
            {
                // We must manually tidy up and remove the icon before we exit.
                // Otherwise it will be left behind until the user mouses over.
                _notifyIcon.Visible = false;
                Application.Exit();
            });
        }
    }
}
