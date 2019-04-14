using System;
using System.Collections.Generic;
using System.Text;

namespace FractionalCalculator
{
    class Control
    {
        public static void WaitForUserKey()
        {
            var (savedI, savedJ) = (Console.CursorLeft, Console.CursorTop);

            Console.ReadKey();

            Console.SetCursorPosition(savedI, savedJ);
        }
    }
}
