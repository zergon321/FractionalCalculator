using System;

namespace FractionalCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine("by NightGhost, 2019");
            Console.WriteLine("https://github.com/tracer8086");

            bool stepByStepFlag = default;

            if (args.Length > 0 && args[0] == "-s")
                stepByStepFlag = true;

            while (true)
            {
                Console.Write(">");

                try
                {
                    var expression = Console.ReadLine();

                    Operations.ComputeExpression(expression, stepByStepFlag);

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error occured:" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }
        }
    }
}
