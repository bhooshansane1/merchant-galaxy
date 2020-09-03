using MerchantGalaxyApp;
using System;
using Xunit;

namespace MerchantGalaxyTest
{
    public class RomanConverterTest
    {
        [Fact]
        public void TestRomanConverter()
        {
            RomanConverter converter = new RomanConverter();
            Assert.Equal<double>(1944, converter.ToDecimal("MCMXLIV").Value);
            Assert.Equal<double>(39, converter.ToDecimal("XXXIX").Value);
            Assert.Equal<double>(246, converter.ToDecimal("CCXLVI").Value);
            Assert.Equal<double>(789, converter.ToDecimal("DCCLXXXIX").Value);
            Assert.Equal<double>(2421, converter.ToDecimal("MMCDXXI").Value);
        }

    }
}
