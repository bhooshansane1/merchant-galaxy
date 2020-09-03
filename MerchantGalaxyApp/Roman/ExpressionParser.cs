using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using MerchantGalaxyApp.Roman.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp
{
    public class ExpressionParser
    {
        private readonly List<IExpression> expressions;
        private readonly ExpressionValidationHelper helper;

        public ExpressionParser(RomanPseudonymMapper pseudonymMap, IDecimalConverter converter, WordMapper wordMap)
        {
            helper = new ExpressionValidationHelper(pseudonymMap, wordMap);
            expressions = GetExpressions(pseudonymMap, converter, wordMap, helper);
        }

        public void Parse(string input)
        {
            var matchingExpression = expressions.FirstOrDefault(e => e.Match(input));
            if (matchingExpression == null) Console.WriteLine("I have no idea what you are talking about");
            else matchingExpression.Execute(input);
        }

        private List<IExpression> GetExpressions(RomanPseudonymMapper pseudonymMap, IDecimalConverter converter, WordMapper wordMap, ExpressionValidationHelper helper)
        {
            List<IExpression> expressions = new List<IExpression>
            {
                new PseudonymExpression(pseudonymMap),
                new UnitExpression(pseudonymMap, wordMap, converter, helper),
                new PseudonymQuestionExpression(pseudonymMap, converter, helper),
                new UnitQuestionExpression(pseudonymMap, wordMap, converter, helper),
                new WordExpression(pseudonymMap, wordMap, converter, helper)
            };

            return expressions;
        }
    }
}
