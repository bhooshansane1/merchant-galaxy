using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp.Roman.Expressions
{
    public class UnitQuestionExpression:IExpression
    {
        private readonly RomanPseudonymMapper _pseudonymMap;
        private readonly WordMapper _wordMap;
        private readonly IDecimalConverter _converter;
        private readonly ExpressionValidationHelper _helper;

        public UnitQuestionExpression(RomanPseudonymMapper pseudonymMap, WordMapper wordMap, IDecimalConverter converter, ExpressionValidationHelper helper)
        {
            _pseudonymMap = pseudonymMap;
            _wordMap = wordMap;
            _converter = converter;
            _helper = helper;
        }

        public void Execute(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1).ToLower();

            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            string[] words = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string word = words[words.Length - 1];
            StringBuilder sb = new StringBuilder();

            //Create Roman Numeral from aliases
            for (int i = 0; i < words.Length - 1; i++)
            {
                sb.Append(_pseudonymMap.GetValueForPseudonym(words[i]));
            }

            //Convert Roman to Decimal
            double? totalUnits = _converter.ToDecimal(sb.ToString());
            if (totalUnits.HasValue) Console.WriteLine(String.Format("{0} is {1}", parts[1], totalUnits.Value * _wordMap.GetPriceByWord(word)));
        }

        public bool Match(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1).ToLower();

            bool isQuestion = (input.StartsWith("how many", StringComparison.OrdinalIgnoreCase));
            if (!isQuestion) return false;

            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] words = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 1) return false;

            return _helper.AreWordsValidAliases(words.Take(words.Length - 1).ToArray()) &&
                    _helper.AreWordsValidCommodities(words.Skip(words.Length - 1).ToArray());
        }


    }
}
