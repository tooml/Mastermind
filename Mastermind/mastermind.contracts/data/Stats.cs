using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts.data
{
    public class Stats
    {
        public string Winner { get; private set; }
        public TimeSpan Duration { get; private set; }

        public Stats(string winner, TimeSpan duration)
        {
            Winner = winner;
            Duration = duration;
        }
    }
}
