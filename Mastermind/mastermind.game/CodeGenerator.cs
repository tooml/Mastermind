using mastermind.contracts.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mastermind.game
{
    public class CodeGenerator
    {
        private const int PIN_COUNT_FOR_CODE = 4;
        private Random _random;

        public CodeGenerator(Random random)
        {
            _random = random;
        }

        public IEnumerable<Pin> Get_random_code()
        {
            //Abhängigkeit 1, 8, PIN_COUNT_FOR_CODE, könnte schlecht sein.
            var random_numbers = _random.Different_random_numbers(1, 9, PIN_COUNT_FOR_CODE);
            var random_code = random_numbers.Select(num => (Pin)num);

            return random_code;
        }
    }
}
