using FinnAngelo.PomoFish.Properties;
using System;
using System.Collections;
using System.Configuration;

namespace FinnAngelo.PomoFish.Properties
{
    interface ISettings
    {
        int WorkingPeriodInMinutes { get; set; }
        int RestPeriodInMinutes { get; set; }
        bool PlaySound { get; set; }
        bool TakeSnapshots { get; set; }
        int SnapshotPeriodInSeconds { get; set; }

        void Save();

    }

    internal sealed partial class Settings : ISettings
    {
    }
}
