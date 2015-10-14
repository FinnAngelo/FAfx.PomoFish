using FinnAngelo.PomoFish.Modules;
using FinnAngelo.PomoFish.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace FinnAngelo.PomoFish
{

    internal class MyApplicationContext : ApplicationContext
    {
        public NotifyIcon NotifyIcon {get;private set;}
        public MyApplicationContext()
        {
            NotifyIcon = new NotifyIcon();

            var pomodoroManager = new PomodoroManager(NotifyIcon);

            NotifyIcon.Visible = true;
            NotifyIcon.ContextMenu = new ContextMenu(
                new MenuItem[] {
                    GetMenuItem("Restart Pomodoro", pomodoroManager.StartPomodoro), 
                    GetMenuItem("Stop Pomodoro", pomodoroManager.StopPomodoro), 
                    GetMenuItem("Exit", Exit)
                });
            Timer _timer = new Timer() { Interval = 1000 };// Timer will tick every 1 second
            _timer.Tick += new EventHandler(pomodoroManager.Tick);
            _timer.Start();
        }

        private MenuItem GetMenuItem(string text, Action onClick)
        {
            return new MenuItem(text, new EventHandler(new Action<object, EventArgs>((obj, args) => onClick())));
        }        

        public void Exit()
        {
            Application.Exit();
        }
    }
}
