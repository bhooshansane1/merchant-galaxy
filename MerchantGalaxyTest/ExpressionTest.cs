using MerchantGalaxyApp;
using MerchantGalaxyApp.Mapper;
using MerchantGalaxyApp.Roman.Expressions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace MerchantGalaxyTest
{
    public class ExpressionTest
    {
        [Fact]
        public void PseudonymExpressionTest()
        {
            RomanPseudonymMapper pseudonymMap = new RomanPseudonymMapper();
            PseudonymExpression expression = new PseudonymExpression(pseudonymMap);
            Assert.True(expression.Match("glob is I"));
            Assert.False(expression.Match("glob is N"));
            expression.Execute("glob is I");
            Assert.True(pseudonymMap.Exists("glob"));
            Assert.Equal("I", pseudonymMap.GetValueForPseudonym("glob"));
        }

        [Fact]
        public void UnitExpressionTest()
        {
            RomanPseudonymMapper pseudonymMap = new RomanPseudonymMapper();
            RomanConverter converter = new RomanConverter();
            WordMapper wordMap = new WordMapper();
            pseudonymMap.AddPseudonym("glob", "I");
            pseudonymMap.AddPseudonym("pish", "X");
            ExpressionValidationHelper helper = new ExpressionValidationHelper(pseudonymMap, wordMap);
            UnitExpression expression = new UnitExpression(pseudonymMap, wordMap, converter, helper);
            expression.Execute("pish glob Iron is 110 Credits");
            Assert.True(wordMap.Exists("Iron"));
            Assert.Equal<double>(10, wordMap.GetPriceByWord("Iron"));
            expression.Execute("glob pish Iron is 6300 Credits");
            Assert.Equal<double>(700, wordMap.GetPriceByWord("Iron"));
        }
    }
}
