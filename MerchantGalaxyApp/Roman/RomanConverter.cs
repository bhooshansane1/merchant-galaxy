using System;
using System.Collections.Generic;
using MerchantGalaxyApp.Contract;
using System.Text;
using System.Linq;
using MerchantGalaxyApp.Rules;
using MerchantGalaxyApp.Mapper;
using MerchantGalaxyApp.Roman.Rules;

namespace MerchantGalaxyApp
{
    public class RomanConverter : IDecimalConverter
    {
        readonly List<IRule> toDecimalRules;
        readonly RomanToDecimalMapper romanToDecimalMapper;

        public RomanConverter()
        {
            toDecimalRules = GetRules();
            romanToDecimalMapper = new RomanToDecimalMapper();
        }

        /// <summary>
        /// This method validates and converts a Roman Numeral given as a string to a decimal.
        /// </summary>
        /// <param name="input">Roman Numeral given as a string</param>
        /// <returns>If the input is valid it returns an int otherwise it returns null</returns>
        public double? ToDecimal(string input)
        {
            if (!ValidateToDecimal(input)) return null;
            return CalculateDecimalValue(input);
        }

        /// <summary>
        /// This method validates the Roman Numeral.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns a boolean based on whether the input is a valid Roman Numeral or not.</returns>
        private bool ValidateToDecimal(string input)
        {
            return toDecimalRules.All(rule => { return rule.Execute(input); });
        }

        /// <summary>
        /// This method performs the actual conversion from Roman numeral to decimal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns the decimal value of a given Roman numeral.</returns>
        private double CalculateDecimalValue(string input)
        {
            double current, next = 0, total = 0;

            for (int i = 0; i < input.Length; i++)
            {
                current = Convert.ToDouble(romanToDecimalMapper.GetValue(input[i].ToString()));
                if (i < input.Length - 1) 
                    next = Convert.ToDouble(romanToDecimalMapper.GetValue(input[i+1].ToString()));

                if (current < next)
                {
                    total += next - current;
                    i++;
                }
                else { total += current; }
            }

            return total;
        }

        private List<IRule> GetRules()
        {
            List<IRule> rules = new List<IRule>
            {
                new InvalidRepetitionRule(),
                new InvalidFourRepetitionRule(),
                new SingleSubtractionRule(),
                new SubtractionRule(),
                new InvalidSubtractionRule()
            };

            return rules;
        }
    }

}
