using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.game
{
    public static class RandomExtension
    {
        public static IEnumerable<int> Different_random_numbers(this Random random, int minValue, int maxValue, int count)
        {
            var different_numbers = new int[count];
            var index = 0;
            while (index < count)
            {
                var number = random.Next(minValue, maxValue);
                if(!different_numbers.Contains(number))
                {
                    different_numbers[index] = number;
                    index++;
                }
            }

            return different_numbers;           
        }
    }
}
