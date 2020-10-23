namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using System;

    public static class Utilities
    {
        public static int TryParseIntegerValue(string answer, int? minValue = null, int? maxValue = null)
        {
            var valueCorrect = false;
            int numericAnswer = default;

            while (!valueCorrect)
            {
                try
                {
                    numericAnswer = int.Parse(answer);
                    if ((minValue != null && numericAnswer < minValue) || (maxValue != null && numericAnswer > maxValue))
                    {
                        throw new ArgumentException("The answer is out of range");
                    }
                    else 
                    {
                        valueCorrect = true;
                    }
                }
                catch
                {
                    valueCorrect = false;
                    Console.Write("Invalid input. Please try again: ");
                    answer = Console.ReadLine();
                }
            }

            return numericAnswer;
        }

        public static double TryParseDoubleValue(string answer, double? minValue = null, double? maxValue = null)
        {
            var valueCorrect = false;
            double numericAnswer = default;

            while (!valueCorrect)
            {
                try
                {
                    numericAnswer = double.Parse(answer);
                    if ((minValue != null && numericAnswer < minValue) || (maxValue != null && numericAnswer > maxValue))
                    {
                        throw new ArgumentException("The answer is out of range");
                    }
                    else 
                    {
                        valueCorrect = true;
                    }
                }
                catch
                {
                    valueCorrect = false;
                    Console.Write("Invalid input. Please try again: ");
                    answer = Console.ReadLine();
                }
            }

            return numericAnswer;
        }
    }
}
