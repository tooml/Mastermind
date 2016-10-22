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
    public class CodeAnalyzer
    {
        private readonly IBlackBox _blackbox;

        public CodeAnalyzer(IBlackBox blackbox)
        {
            _blackbox = blackbox;
        }

        public Result Analyze_try()
        {
            var sperator = new char[] { ',' };

            var game_context = _blackbox.Player.ForEvent(typeof(NewGameStarted).Name).Play().Last();

            var secret_code_event = _blackbox.Player.ForEvent(typeof(SecretCodeGenerated).Name).Play().Last();
            var secret_code = secret_code_event.Data.Split(sperator, StringSplitOptions.RemoveEmptyEntries);

            var last_try_event = _blackbox.Player.ForEvent(typeof(TryPlaced).Name).Play().Last();
            var last_try = last_try_event.Data.Split(sperator, StringSplitOptions.RemoveEmptyEntries);
            Locate_right_placed_Pins(secret_code, last_try, game_context.Context);

            var black_pins_event = _blackbox.Player.ForEvent(typeof(BlackPinsLocated).Name).Play().Last();
            var black_pins = black_pins_event.Data.Split(sperator, StringSplitOptions.RemoveEmptyEntries);
            Locate_right_Color_Pins(secret_code, last_try, black_pins, game_context.Context);

            var white_pins_event = _blackbox.Player.ForEvent(typeof(WhitePinsLocated).Name).Play().Last();
            var white_pins = white_pins_event.Data.Split(sperator, StringSplitOptions.RemoveEmptyEntries);
            return Combine_black_and_white_pins(black_pins, white_pins);
        }

        internal void Locate_right_placed_Pins(IEnumerable<string> secret_code, 
                                                        IEnumerable<string> last_try, string context)
        {
            var matching = secret_code.Zip(last_try, (secret, code) =>
                                         { return (code.Equals(secret)) ? code : String.Empty; }); 
            var black_pins = matching.Where(match => !match.Equals(String.Empty));
            _blackbox.Record(new BlackPinsLocated(context, "BlackPinsLocated", String.Join(",", black_pins)));
        }

        internal void Locate_right_Color_Pins(IEnumerable<string> secret_code,
                                                        IEnumerable<string> last_try, IEnumerable<string>black_pins, string context)
        {
            var white_pins = last_try.Where(pin => secret_code.Contains(pin) 
                                                && !black_pins.Contains(pin)).Distinct();
            _blackbox.Record(new WhitePinsLocated(context, "WhitePinsLocated", String.Join(",", white_pins)));
        }

        internal Result Combine_black_and_white_pins(IEnumerable<string> black_pins, 
                                                            IEnumerable<string> white_pins)
        {
            const int PIN_RESULT_COUNT = 4;

            var black_pin_result = black_pins.Select(pin => Pin.Black );
            var white_pin_result = white_pins.Select(pin => Pin.White);
            var result = black_pin_result.Concat(white_pin_result).ToList();
            for(int i = result.Count(); i < PIN_RESULT_COUNT; i++)
            {
                result.Add(Pin.None);
            }

            return new Result(result);
        }
    }
}
