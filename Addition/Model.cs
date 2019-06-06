using System;
using System.Collections.Generic;
using System.Text;
using FractionalCalculator;

namespace Addition
{
    class Model
    {
        public double First { get; }
        public double Second { get; }
        public long[] Components { get; }
        public double Result { get; }

        public Model(double first, double second)
        {
            First = first;
            Second = second;
            Result = first + second;
            Components = computeComponents();
        }

        private long[] computeComponents()
        {
            int degree = NumberWork.GetMostRoundingDegree(First, Second);
            long xArg = (long)(First * NumberWork.Pow(10, degree));
            long yArg = (long)(Second * NumberWork.Pow(10, degree));
            int maxLength = NumberWork.GetMaxLength(xArg, yArg);
            long[] components = new long[maxLength];

            if (NumberWork.NumLength(xArg) < NumberWork.NumLength(yArg))
                Util.Swap(ref xArg, ref yArg);

            long[] xComps = NumberWork.GetNumberComponents(xArg);
            long[] yComps = NumberWork.GetNumberComponents(yArg);

            for (int i = 0; i < maxLength; i++)
                components[i] = xComps[i] + (i < yComps.Length ? yComps[i] : 0);

            return components;
        }
    }
}
