using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts.data
{
    public class SecretCode
    {
        public Pin Place_One { get; private set; }
        public Pin Place_Two { get; private set; }
        public Pin Place_Three { get; private set; }
        public Pin Place_Four { get; private set; }

        public SecretCode(IEnumerable<string> pins)
        {
            Place_One = (Pin)Enum.Parse(typeof(Pin), pins.ElementAt(0));
            Place_Two = (Pin)Enum.Parse(typeof(Pin), pins.ElementAt(1));
            Place_Three = (Pin)Enum.Parse(typeof(Pin), pins.ElementAt(2));
            Place_Four = (Pin)Enum.Parse(typeof(Pin), pins.ElementAt(3));
        }
    }
}
