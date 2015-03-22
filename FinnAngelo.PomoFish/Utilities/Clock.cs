using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinnAngelo.PomoFish.Utilities
{
    internal interface IClock
    {
        DateTime Now { get; }

    }
    internal class Clock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
