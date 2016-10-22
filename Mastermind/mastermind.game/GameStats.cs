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
    public class GameStats
    {
        private IBlackBox _blackbox;

        public GameStats(IBlackBox blackBox)
        {
            _blackbox = blackBox;
        }

        public void Create_game_stats()
        {
            var winner = Get_winner();
            var duration = Get_game_duration();
            OnCreated(new Stats(winner, duration));
        }

        private string Get_winner()
        {
            return _blackbox.Player.ForEvent(typeof(GameEnded).Name).Play().Last().Data;
        }

        private TimeSpan Get_game_duration()
        {
            var start = _blackbox.Player.ForEvent(typeof(SecretCodeGenerated).Name).Play().Last().Timestamp;
            var end = _blackbox.Player.ForEvent(typeof(GameEnded).Name).Play().Last().Timestamp;
            return end - start;
        }

        public event Action<Stats> OnCreated;
    }
}
