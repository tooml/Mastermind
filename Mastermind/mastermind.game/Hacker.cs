using mastermind.contracts.data;
using mastermind.contracts.events;
using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.game
{
    public class Hacker
    {
        private readonly IBlackBox _blackbox;

        public Hacker(IBlackBox blackbox)
        {
            _blackbox = blackbox;
        }

        public void Register_Hacker_Code(Try place_try)
        {
            var game_context = _blackbox.Player.ForEvent(typeof(NewGameStarted).Name).Play().Last();
            var e = new TryPlaced(game_context.Context, "TryPlaced", place_try.ToString());
            _blackbox.Record(e);
        }
    }
}
