using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Roman.Expressions
{
    public class PseudonymQuestionExpression : IExpression
    {
        private readonly RomanPseudonymMapper _pseudonymMap;
        private readonly IDecimalConverter _converter;
        private readonly ExpressionValidationHelper _helper;

        public PseudonymQuestionExpression(RomanPseudonymMapper pseudonymMap, IDecimalConverter converter, ExpressionValidationHelper helper)
        {
            _pseudonymMap = pseudonymMap;
            _converter = converter;
            _helper = helper;
        }

        public void Execute(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1).ToLower();

            StringBuilder sb = new StringBuilder();
            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            string[] words = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (!_pseudonymMap.Exists(word))
                {
                    Console.WriteLine(String.Format("Error while processing this input: {0}", input));
                    return;
                }
                sb.Append(_pseudonymMap.GetValueForPseudonym(word));
            }

            Console.WriteLine(String.Format("{0} is {1}", parts[1], _converter.ToDecimal(sb.ToString())));
        }

        public bool Match(string input)
        {
            //Remove question mark from the last alias
            input = input.Substring(0, input.Length - 1).ToLower();

            bool isQuestion = (input.StartsWith("how much", StringComparison.OrdinalIgnoreCase));
            if (!isQuestion) return false;

            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] words = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1) return false;

            return _helper.AreWordsValidAliases(words);
        }
    }

}
