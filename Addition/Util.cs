using System;
using System.Collections.Generic;
using System.Text;

namespace Addition
{
    class Util
    {
        public static void Swap<T>(ref T x, ref T y) where T : struct
        {
            var temp = x;

            x = y;
            y = temp;
        }
    }
}
