using mastermind.contracts;
using mastermind.contracts.data;
using mastermind.contracts.events;
using mastermind.game;
using nblackbox.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.body
{
    public class Body : IBody
    {
        private readonly IBlackBox _blackBox;
        private readonly Coder _coder;
        private readonly Hacker _hacker;
        private readonly CodeAnalyzer _code_analyzer;
        private readonly Referee _referee;
        private readonly GameStats _gameStats;

        public Body(IBlackBox blackBox, Coder coder, Hacker hacker, 
                                CodeAnalyzer code_analyzer, Referee referee, GameStats gameStats)
        {
            _blackBox = blackBox;
            _coder = coder;
            _hacker = hacker;
            _code_analyzer = code_analyzer;
            _referee = referee;
            _gameStats = gameStats;
        }

        public void Game_preperation(Action<string> OnSucess)
        {
            var games = _blackBox.Player.ForEvent(typeof(NewGameStarted).Name).Play();
            var game_context = games.Count().ToString();
            _blackBox.Record(new NewGameStarted(game_context, "NewGameStarted", ""));

            var message = _coder.Build_Secret_Code();
            OnSucess(message);
        }

        public void Game_flow(Try hackers_try, Action<Result> OnResult, 
                                                Action OnNewTry, Action<SecretCode> OnWinner)
        {
            _hacker.Register_Hacker_Code(hackers_try);
            var result = _code_analyzer.Analyze_try();
            OnResult(result);

            _referee.Determine_winner(() => {
                                    var secret_code = _blackBox.Player.ForEvent(typeof(SecretCodeGenerated).Name).Play().Last();
                                    OnWinner(new SecretCode(secret_code.Data.Split(',')));
                                        _gameStats.Create_game_stats();
                                    }, 
                                    () => OnNewTry());
        }
    }
}
