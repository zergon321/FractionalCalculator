using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FractionalCalculator;

namespace Addition
{
    class ViewBuilder
    {
        public const int Indent = 3;

        public View BuildViewByModel(Model model)
        {
            View view = new View();

            int degree = NumberWork.GetMostRoundingDegree(model.First, model.Second);
            long xArg = (long)(model.First * NumberWork.Pow(10, degree));
            long yArg = (long)(model.Second * NumberWork.Pow(10, degree));
            int maxLength = NumberWork.GetMaxLength(xArg,yArg);

            view.AddLine(makeIndent(Indent) + xArg);
            view.AddLine(makeIndent(Indent + maxLength - NumberWork.NumLength(yArg)) + yArg);
            view.AddLine(makeIndent(Indent + maxLength) + makeLine(maxLength));

            foreach (var component in model.Components)
                view.AddLine(makeIndent(Indent + maxLength - NumberWork.NumLength(component)) + component);

            view.AddLine(makeIndent(Indent) + makeLine(maxLength));
            view.AddLine(makeIndent(Indent) + model.Result);

            return view;
        }

        private string makeIndent(int indent) => String.Join("", Enumerable.Repeat(" ", indent));

        private string makeLine(int length) => String.Join("-", Enumerable.Repeat(" ", length));
    }
}
