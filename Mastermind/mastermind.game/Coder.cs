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
    public class Coder
    {
        private readonly IBlackBox _blackbox;
        private readonly CodeGenerator _code_generator;
        
        public Coder(IBlackBox blackbox, CodeGenerator code_genaerator)
        {
            _blackbox = blackbox;
            _code_generator = code_genaerator;
        }

        public string Build_Secret_Code()
        {
            var secret_code = _code_generator.Get_random_code();
            Register_SecretCode(secret_code);
            return "Geheimer Code wurde erstellt.";
        }

		internal void Register_SecretCode(IEnumerable<Pin> secret_code)
        {
            var game_context = _blackbox.Player.ForEvent(typeof(NewGameStarted).Name).Play().Last();
            var e = new SecretCodeGenerated(game_context.Context, "SecretCodeGenerated", 
												String.Join(",", secret_code));
            _blackbox.Record(e);
        }
    }
}
