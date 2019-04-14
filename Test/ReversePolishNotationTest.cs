using System;
using Xunit;
using FractionalCalculator;

namespace Test
{
    public class ReversePolishNotationTest
    {
        [Fact]
        public void ExpressionTest()
        {
            Assert.Equal("3.45 12.65 + 5.98 6 + 11.05 - * 7 / ", ReversePolishNotation.GetExpression("(3.45 + 12.65) * (5.98 + 6 - 11.05) / 7"));
        }
    }
}
