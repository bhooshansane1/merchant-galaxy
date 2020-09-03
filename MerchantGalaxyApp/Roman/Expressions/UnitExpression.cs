using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp.Roman.Expressions
{
    public class UnitExpression : IExpression
    {
        private readonly RomanPseudonymMapper _pseudonymMap;
        private readonly WordMapper _wordMap;
        private readonly IDecimalConverter _converter;
        private readonly ExpressionValidationHelper _helper;

        public UnitExpression(RomanPseudonymMapper pseudonymMap, WordMapper wordMap, IDecimalConverter converter, ExpressionValidationHelper helper)
        {
            _pseudonymMap = pseudonymMap;
            _wordMap = wordMap;
            _converter = converter;
            _helper = helper;
        }

        public void Execute(string input)
        {
            input = input.ToLower();
            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInFirstPart = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInSecondPart = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            double.TryParse(wordsInSecondPart[0], out double decimalPrice);

            string word = wordsInFirstPart[wordsInFirstPart.Length - 1];

            //Create Roman Numeral from aliases
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < wordsInFirstPart.Length - 1; i++)
            {
                sb.Append(_pseudonymMap.GetValueForPseudonym(wordsInFirstPart[i]));
            }

            //Convert Roman to decimal
            double? totalUnits = _converter.ToDecimal(sb.ToString());

            //Calculate and store per unit price of commodity
            if (totalUnits.HasValue) _wordMap.AddWord(word, decimalPrice / totalUnits.Value);
            else Console.WriteLine("Error occurred while calculating commodity price");
        }

        public bool Match(string input)
        {
            input = input.ToLower();
            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) return false;

            string[] wordsInFirstPart = parts[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] wordsInSecondPart = parts[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return input.EndsWith("credits", StringComparison.OrdinalIgnoreCase) &&
                    !input.StartsWith("how many", StringComparison.OrdinalIgnoreCase) && parts.Length == 2 &&
                    wordsInSecondPart.Length == 2 && Double.TryParse(wordsInSecondPart[0], out _) &&
                    _helper.AreWordsValidAliases(wordsInFirstPart.Take(wordsInFirstPart.Length - 1).ToArray());
        }
    }
}
