using mastermind.contracts.events;
using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.game
{
    public class Referee
    {
        private readonly IBlackBox _blackBox;

        public Referee(IBlackBox blackBox)
        {
            _blackBox = blackBox;
        }

        public void Determine_winner(Action WinnerIsGiven, Action NoWinner)
        {
            var game_context = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play().Last().Context;

            Is_Secrect_Code_hacked( () => { Register_winner(game_context, "Hacker");
                                            WinnerIsGiven(); }, 
                                        () => Is_Hackers_try_consumed(
                                            () => { Register_winner(game_context, "Coder");
                                                    WinnerIsGiven(); }, 
                                            () => { NoWinner(); }, game_context) );
        }

        internal void Is_Secrect_Code_hacked(Action Hacked, Action NotHacked)
        {
            const int PIN_COUNT_FOR_CODE = 4;

            var e = _blackBox.Player.ForEvent(typeof(BlackPinsLocated).Name).Play().Last();
            var black_pins = e.Data.Split(',');

            if (black_pins.Count() == PIN_COUNT_FOR_CODE)
                Hacked();
            else
                NotHacked();
        }

        internal void Is_Hackers_try_consumed(Action Consumed, Action NotConsumed, string context)
        {
            const int MAX_TRYS = 10;

            var trys = _blackBox.Player.ForEvent(typeof(TryPlaced).Name).WithContext(context).Play().Count();

            if (trys < MAX_TRYS)
                NotConsumed();
            else
                Consumed();
        }

        private void Register_winner(string context, string winner)
        {
            _blackBox.Record(new GameEnded(context, "GameEnded", winner));
        }
    }
}
