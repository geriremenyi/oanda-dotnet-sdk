using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    public static class Utilities
    {
        public static int TryToParseNumericAnswer(string answer, uint minValue, uint? maxValue = null)
        {
            var numericAnswer = -1;

            while (numericAnswer < 0)
            {
                try
                {
                    numericAnswer = int.Parse(answer);
                    if (numericAnswer < minValue || (maxValue != null && numericAnswer > maxValue))
                    {
                        throw new ArgumentException("The answer is out of range");
                    }
                }
                catch
                {
                    Console.Write("Invalid selection. Please try again: ");
                    answer = Console.ReadLine();
                    numericAnswer = -1;
                }
            }

            return numericAnswer;
        }
    }
}
