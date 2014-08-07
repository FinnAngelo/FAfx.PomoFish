using System;
using System.Collections;
using System.Configuration;

namespace FAfx.PomoFish
{
    interface IConfigManager
    {
        bool PlaySound { get; }
        int RestPeriodInMinutes { get; }
        int SnapshotPeriodInSeconds { get; }
        bool TakeSnapshots { get; }
        int WorkingPeriodInMinutes { get; }
    }

    internal class ConfigManager : IConfigManager
    {
        Int32 _WorkingPeriodInMinutes;
        public Int32 WorkingPeriodInMinutes { get { return _WorkingPeriodInMinutes; } }
        Int32 _RestPeriodInMinutes;
        public Int32 RestPeriodInMinutes { get { return _RestPeriodInMinutes; } }
        bool _PlaySound;
        public bool PlaySound { get { return _PlaySound; } }
        bool _TakeSnapshots;
        public bool TakeSnapshots { get { return _TakeSnapshots; } }
        Int32 _SnapshotPeriodInSeconds;
        public Int32 SnapshotPeriodInSeconds { get { return _SnapshotPeriodInSeconds; } }

        internal ConfigManager(Hashtable configSection = null)
        {
            var cs = configSection ?? (Hashtable)ConfigurationManager.GetSection("FAfx.PomoFish");
            if (!Int32.TryParse(cs["WorkingPeriodInMinutes"] as string, out _WorkingPeriodInMinutes)) _WorkingPeriodInMinutes = 25;
            if (!Int32.TryParse(cs["RestPeriodInMinutes"] as string, out _RestPeriodInMinutes)) _RestPeriodInMinutes = 5;
            if (!Boolean.TryParse(cs["PlaySound"] as string, out _PlaySound)) _PlaySound = false;
            if (!Boolean.TryParse(cs["TakeSnapshots"] as string, out _TakeSnapshots)) _TakeSnapshots = false;
            if (!Int32.TryParse(cs["SnapshotPeriodInSeconds"] as string, out _SnapshotPeriodInSeconds)) _SnapshotPeriodInSeconds = 60;
        }
    }
}
