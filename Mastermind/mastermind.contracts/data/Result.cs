using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts.data
{
    public class Result
    {
        public Pin Place_One { get; private set; }
        public Pin Place_Two { get; private set; }
        public Pin Place_Three { get; private set; }
        public Pin Place_Four { get; private set; }

        public Result(IEnumerable<Pin> result_pins)
        {
            Place_One = result_pins.ElementAt(0);
            Place_Two = result_pins.ElementAt(1);
            Place_Three = result_pins.ElementAt(2);
            Place_Four = result_pins.ElementAt(3);
        }
    }
}
