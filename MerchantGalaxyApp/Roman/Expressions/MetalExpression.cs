using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp.Roman.Expressions
{
    public class WordExpression : IExpression
    {
        private readonly RomanPseudonymMapper _pseudonymMap;
        private readonly WordMapper _wordMap;
        private readonly IDecimalConverter _converter;
        private readonly ExpressionValidationHelper _helper;

        public WordExpression(RomanPseudonymMapper pseudonymMap, WordMapper wordMap, IDecimalConverter converter, ExpressionValidationHelper helper)
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

            string[] preIsWords = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] postIsWords = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string sourceWord = postIsWords.Skip(postIsWords.Length - 1).ToString();
            string destinationWord = preIsWords[2];

            string[] aliases = postIsWords.Take(postIsWords.Length - 1).ToArray();

            StringBuilder sb = new StringBuilder();

            //Create Roman Numeral from aliases
            for (int i = 0; i < aliases.Length - 1; i++)
            {
                sb.Append(_pseudonymMap.GetValueForPseudonym(aliases[i]));
            }

            double sourceWordPrice = _wordMap.GetPriceByWord(sourceWord);
            double destinationWordPrice = _wordMap.GetPriceByWord(destinationWord);

            //Convert Roman to Decimal
            double? totalUnits = _converter.ToDecimal(sb.ToString());
            if (totalUnits.HasValue)
            {
                double totalSourceCommodity = sourceWordPrice * totalUnits.Value;
                Console.WriteLine(String.Format("{0} is {1} {2}", parts[1], (totalSourceCommodity / destinationWordPrice), destinationWord));
            }
        }

        public bool Match(string input)
        {
            //Remove question mark
            input = input.Substring(0, input.Length - 1).ToLower();

            bool isQuestion = (input.StartsWith("how many", StringComparison.OrdinalIgnoreCase));
            if (!isQuestion) return false;

            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] preIsWords = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (preIsWords.Length < 3) return false;

            string[] postIsWords = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (postIsWords.Length < 2) return false;

            return _helper.AreWordsValidCommodities(preIsWords.Skip(preIsWords.Length - 1).ToArray()) &&
                    _helper.AreWordsValidCommodities(postIsWords.Skip(postIsWords.Length - 1).ToArray()) &&
                    _helper.AreWordsValidAliases(postIsWords.Take(postIsWords.Length - 1).ToArray());
        }
    }
}
