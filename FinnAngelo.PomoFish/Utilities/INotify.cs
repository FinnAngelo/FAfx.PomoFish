using FinnAngelo.PomoFish.Modules;
using System;
using System.Drawing;

namespace FinnAngelo.PomoFish.Utilities
{
    internal interface INotify
    {
        event NotifyHandler Notify;
    }

    internal class NotifyEventArgs : EventArgs
    {
        public NotifyAlert Alert { get; set; }
        public INotifyIconDetails IconDetails { get; set; }
    }

    internal class NotifyAlert
    {
        public string Header{get;set;}
        public string Body{get;set;}
    }

    internal interface INotifyIconDetails
    {
        Color ForeColor { get; }
        Color BackgroundColor { get; }
        string Text { get; }
    }

    internal delegate void NotifyHandler(object sender, NotifyEventArgs e);
}