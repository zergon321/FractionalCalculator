using System;
using Xunit;
using FractionalCalculator;

namespace Test
{
    public class NumberWorkTest
    {
        [Fact]
        public void GetFirstDigitTest()
        {
            var (digit, remaining) = NumberWork.GetFirstDigit(3458977);

            Assert.Equal(3, digit);
            Assert.Equal(458977, remaining);

            (digit, remaining) = NumberWork.GetFirstDigit(7);

            Assert.Equal(7, digit);
            Assert.Equal(-1, remaining);

            (digit, remaining) = NumberWork.GetFirstDigit(0);

            Assert.Equal(0, digit);
            Assert.Equal(-1, remaining);
        }

        [Fact]
        public void ConcatTest()
        {
            Assert.Equal(346, NumberWork.Concat(3, 46));
            Assert.Equal(346, NumberWork.Concat(34, 6));
            Assert.Equal(35_468, NumberWork.Concat(35, 468));
        }

        [Fact]
        public void NumLengthTest()
        {
            Assert.Equal(3, NumberWork.NumLength(567));
        }

        [Fact]
        public void GetMaxLengthTest()
        {
            Assert.Equal(6, NumberWork.GetMaxLength(345, 867_498));
            Assert.Equal(3, NumberWork.GetMaxLength(0, 756));
        }

        [Fact]
        public void DivisionTest()
        {
            var (div, remaining) = NumberWork.GetSufficientPartForDivision(1306, 16);

            Assert.Equal(130, div);
            Assert.Equal(6, remaining);

            (div, remaining) = NumberWork.GetSufficientPartForDivision(3, 2);

            Assert.Equal(3, div);
            Assert.Equal(-1, remaining);
        }

        [Fact]
        public void RoundingDegreeTest()
        {
            Assert.Equal(2, NumberWork.GetMostRoundingDegree(13.6, 12.75));
            Assert.Equal(1, NumberWork.GetMostRoundingDegree(13.6, 10));
            Assert.Equal(0, NumberWork.GetMostRoundingDegree(10, 20));
        }
    }
}
