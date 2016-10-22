using mastermind.contracts.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.contracts
{
    public interface IBody
    {
        void Game_preperation(Action<string> OnSucess);
        void Game_flow(Try hackers_try, Action<Result> OnResult, Action OnNewTry, Action<SecretCode> OnWinner);
    }
}
