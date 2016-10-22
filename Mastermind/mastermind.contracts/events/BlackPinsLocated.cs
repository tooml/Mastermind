using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts.events
{
    public class BlackPinsLocated : IEvent
    {
        public string Context { get; }
        public string Data { get; }
        public string Name { get; }

        public BlackPinsLocated(string context, string name, string data)
        {
            Context = context;
            Name = name;
            Data = data;
        }
    }
}
