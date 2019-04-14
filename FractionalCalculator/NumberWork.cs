using System;
using System.Collections.Generic;
using System.Text;

namespace FractionalCalculator
{
    public static class NumberWork
    {
        /// <summary>
        /// Returns length of the given number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int NumLength(long number) => number.ToString().Length;

        /// <summary>
        /// Returns max of lengths of the given numbers.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Max of two lengths.</returns>
        public static int GetMaxLength(long x, long y) => Math.Max(NumLength(x), NumLength(y));

        /// <summary>
        /// Checks if the given string can be parsed as double.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Bool value indicating whether the string can be parsed as double.</returns>
        public static bool IsDouble(string value) => double.TryParse(value, out double dummy);

        /// <summary>
        /// Returns an array of number digits multiplied by degrees of ten.
        /// For example, 2365 = 2000 + 300 + 60 + 5.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static long[] GetNumberComponents(long number)
        {
            List<long> components = new List<long>();

            components.Add(number % 10);

            for (int i = 1; i < NumLength(number); i++)
            {
                var component = number / Pow(10, i) % 10 * Pow(10, i);

                components.Add(component);
            }

            return components.ToArray();
        }

        /// <summary>
        /// Returns the degree of ten which is enough to make both operands whole.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GetMostRoundingDegree(double x, double y)
        {
            var xParts = x.ToString().Split('.');
            var yParts = y.ToString().Split('.');

            var xDegree = xParts.Length == 1 ? 0 : xParts[1].Length;
            var yDegree = yParts.Length == 1 ? 0 : yParts[1].Length;

            return Math.Max(xDegree, yDegree);
        }

        /// <summary>
        /// Returns number^degree.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="degree"></param>
        /// <returns>number^degree</returns>
        public static int Pow(int number, int degree)
        {
            if (degree == 0)
                return 1;

            var product = 1;

            while (degree-- > 0)
                product *= number;

            return product;
        }

        /// <summary>
        /// Gets the part of the given x so it's enough to divide it by y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (long, long) GetSufficientPartForDivision(long x, long y)
        {
            string number = x.ToString();

            for (int i = 0; i < number.Length; i++)
            {
                long subNum = long.Parse(number.Substring(0, i + 1));

                if (subNum / y > 0)
                    return (subNum, NumLength(subNum) == number.Length ? -1 : long.Parse(number.Substring(i + 1)));
            }

            return (x, -1);
        }

        /// <summary>
        /// Gets the first digit of the given number and the rest of the number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static (long, long) GetFirstDigit(long number)
        {
            string strNum = number.ToString();

            if (strNum.Length == 1)
                return (number, -1);

            long firstDigit = long.Parse(strNum[0].ToString());
            long remaining = long.Parse(strNum.Substring(1));

            return (firstDigit, remaining);
        }

        /// <summary>
        /// Write the second number to the end of the given number.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="remaining"></param>
        /// <returns></returns>
        public static long Concat(long number, long remaining)
        {
            string strNumber = number.ToString();

            strNumber += remaining.ToString();

            return long.Parse(strNumber);
        }
    }
}
