using Common.Logging;
using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using FinnAngelo.PomoFish.Utilities;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish
{
    

    internal interface INotifyEventListener
    {
        void Notify(object sender, NotifyEventArgs e);
    }

    internal class MyApplicationContext : ApplicationContext
    {
        private readonly ILog _log;
        private readonly IPomodoroManager _PomodoroManager;


        private readonly IIconManager _iconManager;
        private readonly NotifyIcon _notifyIcon = new NotifyIcon();

        public MyApplicationContext(
            ILog log,          
            IIconManager iconManager,  
            IPomodoroManager pomodoroManager)
        {
            _log = log;
            _iconManager = iconManager;
            _PomodoroManager = pomodoroManager;
            _PomodoroManager.Notify += new NotifyHandler(this.Notify);

            _log.Trace("MyApplicationContext(ILog log, IConfigManager config, IIconManager iconManager, IScreenshotManager screenshotManager, IDateTimeFactory dateTimeFactory)");

            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenu = new ContextMenu(
                new MenuItem[] {
                    new MenuItem("Restart Pomodoro", new EventHandler(RestartPomodoro)), 
                    new MenuItem("Disable Pomodoro", new EventHandler(DisablePomodoro)), 
                    new MenuItem("Exit", new EventHandler(Exit)) 
                });

            DisablePomodoro(null, null);
        }

        

        public void DisablePomodoro(object sender, EventArgs e)
        {
            _log.Trace("DisablePomodoro");

            _PomodoroManager.ChangePomodoro(Pomodoro.Stopped, int.MaxValue);

        }

        public void RestartPomodoro(object sender, EventArgs e)
        {
            _log.Trace("RestartPomodoro");
            _PomodoroManager.ChangePomodoro(
                Pomodoro.Working,
                1// _settings.WorkingPeriodInMinutes
            );
        }

        public void RestPomodoro(object sender, EventArgs e)
        {
            _log.Trace("RestPomodoro");
            _PomodoroManager.ChangePomodoro(
                Pomodoro.Resting,
                1//_settings.RestPeriodInMinutes
            );
        }

        public void Notify(object sender, NotifyEventArgs e)
        {
            //SystemSounds.Exclamation.Play();

            _notifyIcon.Text = e.Alert.Header.ToString();
            _notifyIcon.BalloonTipText = e.Alert.Body.ToString();
            _notifyIcon.ShowBalloonTip(500);

            if (e.IconDetails != null)
            {
                _iconManager.SetNotifyIcon(_notifyIcon, e.IconDetails);
            }
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
