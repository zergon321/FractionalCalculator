using System;
using System.Collections.Generic;

namespace FractionalCalculator
{
    public static class ReversePolishNotation
    {
        /// <summary>
        /// Indicates whether the given character is a delimiter.
        /// </summary>
        /// <param name="c">Operation character.</param>
        /// <returns>The value indicating whether the givan character is a delimiter.</returns>
        public static bool IsDelimeter(char c) => " =".Contains(c);

        /// <summary>
        /// Indicates whether the given character is an operator.
        /// </summary>
        /// <param name="c">Operation character.</param>
        /// <returns>The value indicating whether the givan character is an operator.</returns>
        public static bool IsOperator(char c) => "+-/*()".Contains(c);

        /// <summary>
        /// Returns the whole number denoting priority of the given operator.
        /// </summary>
        /// <param name="op">Operation character.</param>
        /// <returns>Priority of the given operator.</returns>
        static private byte GetPriority(char op)
        {
            switch (op)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 4;
                case '/': return 4;
                default: return 6;
            }
        }

        /// <summary>
        /// Transforms the expression in infix notation to an expression in postfix notation (reverse polish notation).
        /// </summary>
        /// <param name="input">An expression in infix notation given as a string.</param>
        /// <returns>An expression in postfix notation.</returns>
        public static string GetExpression(string input)
        {
            string result = string.Empty;
            Stack<char> operationStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                // Skip delimiters.
                if (IsDelimeter(input[i]))
                    continue;

                // Read a number.
                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        result += input[i];
                        i++;

                        if (i == input.Length)
                            break;
                    }

                    result += " ";
                    i--;
                }

                // Read an operator.
                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operationStack.Push(input[i]);
                    // It there's a closing brace, put all read operators to the result string.
                    else if (input[i] == ')')
                    {
                        char s = operationStack.Pop();

                        while (s != '(')
                        {
                            result += s.ToString() + ' ';
                            s = operationStack.Pop();
                        }
                    }
                    else
                    {
                        if (operationStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operationStack.Peek()))
                                result += operationStack.Pop().ToString() + " ";

                        operationStack.Push(char.Parse(input[i].ToString()));

                    }
                }
            }

            // Put all remaining operators tio the result string.
            while (operationStack.Count > 0)
                result += operationStack.Pop() + " ";

            return result;
        }
    }
}
