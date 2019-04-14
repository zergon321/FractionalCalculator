using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FractionalCalculator
{
    static class Operations
    {
        /// <summary>
        /// Precision of division operation.
        /// </summary>
        public const int DivisionLimit = 16;

        /// <summary>
        /// Performs the division and prints a detailed solution.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepByStep">Indicates whether the operation should be performed in step-by-step mode controlled by user.</param>
        public static void Division(double x, double y, bool stepByStep)
        {
            Console.Write($"{x} / {y} = ");

            int degree = NumberWork.GetMostRoundingDegree(x, y);

            // Make the operands whole.
            long xArg = (long)(x * NumberWork.Pow(10, degree));
            long yArg = (long)(y * NumberWork.Pow(10, degree));

            Console.WriteLine($"{xArg} / {yArg}");
            Console.WriteLine();
            Console.WriteLine($"   {xArg}|{yArg}");

            // Get cursor position for the result.
            var (answerI, answerJ) = (Console.CursorLeft + 3 + NumberWork.NumLength(xArg), Console.CursorTop);
            // Get an initial value for division.
            var (div, remaining) = NumberWork.GetSufficientPartForDivision(xArg, yArg);

            Console.SetCursorPosition(answerI, answerJ);
            Console.Write("|");
            answerI++;

            var position = 3;
            // For putting a dot.
            var trigger = false;

            for (int i = 0; i < DivisionLimit; i++)
            {
                if (stepByStep)
                    Control.WaitForUserKey();

                // Compute the next digit of the result and write to the result position.
                var result = div / yArg;
                var (savedI, savedJ) = (Console.CursorLeft, Console.CursorTop + (i == 0 ? 0 : 1));

                Console.SetCursorPosition(answerI, answerJ);
                Console.Write(result);
                answerI++;
                Console.SetCursorPosition(savedI, savedJ);

                var rev = result * yArg;
                var divLength = NumberWork.NumLength(div);
                var revLength = NumberWork.NumLength(rev);

                Console.SetCursorPosition(position + divLength - revLength, Console.CursorTop);
                Console.Write(rev);
                Console.SetCursorPosition(position, Console.CursorTop + 1);
                Console.Write(String.Join("", Enumerable.Repeat("-", divLength)));

                // Compute and write difference.
                div -= rev;
                position += divLength - NumberWork.NumLength(div);

                // If it's zero, quit the loop.
                if (div == 0)
                {
                    Console.SetCursorPosition(position, Console.CursorTop + 1);
                    Console.Write(div);

                    break;
                }

                while (true)
                {
                    if (stepByStep)
                        Control.WaitForUserKey();

                    long firstDigit;

                    if (remaining == -1)
                    {
                        firstDigit = 0;

                        // If it's time to put a dot in the result.
                        if (!trigger)
                        {
                            (savedI, savedJ) = (Console.CursorLeft, Console.CursorTop);

                            Console.SetCursorPosition(answerI, answerJ);
                            Console.Write('.');
                            answerI++;
                            Console.SetCursorPosition(savedI, savedJ);

                            trigger = true;
                        }
                    }
                    else
                        (firstDigit, remaining) = NumberWork.GetFirstDigit(remaining);

                    // Add the next digit (or zero if there's no unused digits anymore).
                    div = NumberWork.Concat(div, firstDigit);

                    if (div / yArg == 0)
                    {
                        (savedI, savedJ) = (Console.CursorLeft, Console.CursorTop);

                        Console.SetCursorPosition(answerI, answerJ);
                        Console.Write(0);
                        answerI++;
                        Console.SetCursorPosition(savedI, savedJ);

                        i++;

                        if (i >= DivisionLimit)
                            break;
                    }
                    else
                        break;
                }

                Console.SetCursorPosition(position, Console.CursorTop + 1);
                Console.Write(div);
            }
        }

        /// <summary>
        /// Performs the addition and prints a detailed solution.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepByStep">Indicates whether the operation should be performed in step-by-step mode controlled by user.</param>
        public static void Addition(double x, double y, bool stepByStep)
        {
            // Make the arguments whole.
            int degree = NumberWork.GetMostRoundingDegree(x, y);
            long xArg = (long)(x * NumberWork.Pow(10, degree));
            long yArg = (long)(y * NumberWork.Pow(10, degree));
            int maxLength = NumberWork.GetMaxLength(xArg, yArg);

            if (NumberWork.NumLength(xArg) < NumberWork.NumLength(yArg))
                Swap(ref xArg, ref yArg);

            Console.WriteLine($"   {xArg}");
            Console.WriteLine($"   " + String.Join("", Enumerable.Repeat(" ", maxLength - NumberWork.NumLength(yArg))) + yArg);
            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));

            // Get components of the digits (see GetNumberComponents documentstion).
            long[] xComps = NumberWork.GetNumberComponents(xArg);
            long[] yComps = NumberWork.GetNumberComponents(yArg);

            for (int i = 0; i < maxLength; i++)
            {
                if (stepByStep)
                    Control.WaitForUserKey();

                // Add two digits (components). 
                var result = xComps[i] + (i < yComps.Length ? yComps[i] : 0);
                var strResult = result.ToString();

                // Write the result of the addition.
                Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - strResult.Length)));
                Console.WriteLine(strResult);
            }

            // Write the shrinked result.
            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));
            Console.WriteLine("  " + (xArg + yArg) * Math.Pow(10, -degree));
        }

        /// <summary>
        /// Performs the multiplication and prints a detailed solution.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepByStep">Indicates whether the operation should be performed in step-by-step mode controlled by user.</param>
        public static void Multiplication(double x, double y, bool stepByStep)
        {
            // Make the numbers whole.
            int degree = NumberWork.GetMostRoundingDegree(x, y);
            long xArg = (long)(x * NumberWork.Pow(10, degree));
            long yArg = (long)(y * NumberWork.Pow(10, degree));
            int maxLength = NumberWork.NumLength(xArg) + NumberWork.NumLength(yArg);

            if (NumberWork.NumLength(xArg) < NumberWork.NumLength(yArg))
                Swap(ref xArg, ref yArg);

            // Write them.
            Console.Write(String.Join("", Enumerable.Repeat(' ', maxLength - NumberWork.NumLength(xArg))));
            Console.WriteLine($"   {xArg}");
            Console.Write(String.Join("", Enumerable.Repeat(' ', maxLength - NumberWork.NumLength(yArg))));
            Console.WriteLine($"   {yArg}");
            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));

            // Get the components of the numbers.
            long[] xComps = NumberWork.GetNumberComponents(xArg);
            long[] yComps = NumberWork.GetNumberComponents(yArg);

            // Multiply each digit by each other.
            foreach (var xComp in xComps)
                foreach (var yComp in yComps)
                {
                    var result = xComp * yComp;
                    var strResult = result.ToString();

                    // Skip zeroes.
                    if (result != 0)
                    {
                        if (stepByStep)
                            Control.WaitForUserKey();

                        // Write interim result.
                        Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - strResult.Length)));
                        Console.WriteLine(strResult + $" = {xComp} * {yComp}");
                    }
                }

            // Write the final result.
            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));
            Console.Write(String.Join("", Enumerable.Repeat(' ', maxLength - NumberWork.NumLength(xArg * yArg))));
            Console.WriteLine($"   {xArg * yArg * Math.Pow(10, -degree * 2)}");
        }

        /// <summary>
        /// Performs the subtraction and prints a detailed solution.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stepByStep">Indicates whether the operation should be performed in step-by-step mode controlled by user.</param>
        public static void Subtraction(double x, double y, bool stepByStep)
        {
            // Make the numbers whole.
            int degree = NumberWork.GetMostRoundingDegree(x, y);
            long xArg = (long)(x * NumberWork.Pow(10, degree));
            long yArg = (long)(y * NumberWork.Pow(10, degree));
            int maxLength = NumberWork.GetMaxLength(xArg, yArg);

            if (NumberWork.NumLength(xArg) < NumberWork.NumLength(yArg))
                Swap(ref xArg, ref yArg);

            Console.Write(String.Join("", Enumerable.Repeat(' ', maxLength - NumberWork.NumLength(xArg))));
            Console.WriteLine($"   {xArg}");
            Console.Write(String.Join("", Enumerable.Repeat(' ', maxLength - NumberWork.NumLength(yArg))));
            Console.WriteLine($"   {yArg}");
            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));

            long[] yComps = NumberWork.GetNumberComponents(yArg);

            Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - NumberWork.NumLength(xArg))));
            Console.WriteLine(xArg);

            // Subtract each digit (component) from x and write interim results.
            foreach (var comp in yComps)
            {
                if (stepByStep)
                    Control.WaitForUserKey();

                Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - NumberWork.NumLength(comp))));
                Console.WriteLine(comp);
                Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));
                Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - NumberWork.NumLength(xArg - comp))));
                Console.WriteLine(xArg - comp);

                xArg -= comp;
            }

            Console.WriteLine("   " + String.Join("", Enumerable.Repeat("-", maxLength)));
            Console.Write(String.Join("", Enumerable.Repeat(' ', 3 + maxLength - NumberWork.NumLength(xArg) - 1)));
            Console.WriteLine(xArg * Math.Pow(10, -degree));
        }

        /// <summary>
        /// Computes the composite expression with a detailed solution.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="stepByStep"></param>
        public static void ComputeExpression(string expression, bool stepByStep)
        {
            // Cast the expression to reverse polish notation and remove all meaningless spaces.
            string reversed = ReversePolishNotation.GetExpression(expression).Trim();
            // Get all the tokens delimited by spaces.
            string[] tokens = reversed.Split(' ');
            // Stack to store operands and results.
            Stack<double> stack = new Stack<double>();
            // Stage counter.
            var i = 0;

            // Traverse all tokens in the expression.
            foreach (var token in tokens)
            {
                if (token == "")
                    continue;

                // If token is a number, push it to the stack right away.
                if (NumberWork.IsDouble(token))
                {
                    stack.Push(double.Parse(token));

                    continue;
                }

                // If token is an operation.
                // Retrieve operaands from the stack.
                var second = stack.Pop();
                var first = stack.Pop();
                double result = default;

                switch (token)
                {
                    case "+":
                        Console.WriteLine($"Stage {i}: {first} + {second}");
                        Console.WriteLine();

                        Addition(first, second, stepByStep);
                        result = first + second;

                        break;

                    case "-":
                        Console.WriteLine($"Stage {i}: {first} - {second}");
                        Console.WriteLine();

                        Subtraction(first, second, stepByStep);
                        result = first - second;

                        break;

                    case "*":
                        Console.WriteLine($"Stage {i}: {first} * {second}");
                        Console.WriteLine();

                        Multiplication(first, second, stepByStep);
                        result = first * second;

                        break;

                    case "/":
                        Console.WriteLine($"Stage {i}: {first} / {second}");
                        Console.WriteLine();

                        Division(first, second, stepByStep);
                        result = first / second;

                        break;
                }

                // Push the interim result to the stack.
                stack.Push(result);

                Console.WriteLine();

                i++;

                if (stepByStep)
                    Control.WaitForUserKey();
            }

            // Retrireve the final result from the stack.
            var answer = stack.Pop();

            Console.WriteLine($"Result: {answer}");
        }

        /// <summary>
        /// Swaps values of the given variables.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Swap<T>(ref T x, ref T y) where T: struct
        {
            var temp = x;

            x = y;
            y = temp;
        }
    }
}
