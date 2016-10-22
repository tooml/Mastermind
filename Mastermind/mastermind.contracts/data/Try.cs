using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts.data
{
    public class Try
    {
        public Pin Place_One { get; private set; }
        public Pin Place_Two { get; private set; }
        public Pin Place_Three { get; private set; }
        public Pin Place_Four { get; private set; }

        public Try(Pin one, Pin two, Pin three, Pin four)
        {
            Place_One = one;
            Place_Two = two;
            Place_Three = three;
            Place_Four = four;
        }

        public override string ToString()
        {
            return String.Concat(Place_One, ",", Place_Two, ",",
                                     Place_Three, ",", Place_Four);
        }
    }
}
