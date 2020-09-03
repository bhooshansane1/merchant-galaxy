using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Roman.Expressions
{
    public class PseudonymExpression : IExpression
    {
        private readonly RomanPseudonymMapper _pseudonymMap;

        public PseudonymExpression(RomanPseudonymMapper pseudonymMap)
        {
            _pseudonymMap = pseudonymMap;
        }

        public void Execute(string input)
        {
            input = input.ToLower();
            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);

            _pseudonymMap.AddPseudonym(parts[0], parts[1]);
        }

        public bool Match(string input)
        {
            string romanAlphabet = RomanSymbol.GetAlphabet().ToLower();
            input = input.ToLower();
            string[] parts = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) return false;

            string roman = parts[1];
            bool found = false;

            for (int i = 0; i < romanAlphabet.Length; i++)
            {
                if (String.Equals(roman, romanAlphabet[i].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            return found;
        }
    }
}
