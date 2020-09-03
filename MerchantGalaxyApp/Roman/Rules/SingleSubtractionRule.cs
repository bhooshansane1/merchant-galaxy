using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Roman;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Rules
{
    public class SingleSubtractionRule : IRule
    {
        public bool Execute(string input)
        {
            if (input.Length < 3) return true;
            if (!RomanSymbol.IsValidSymbol(input.ToUpper()))
            {
                Console.WriteLine("SingleSubtraction Rule has been violated");
                return false;
            }

            for (int i = input.Length - 1; i >= 2; i--)
            {
                if (RomanSymbol.IsSmaller(input[i - 1].ToString().ToUpper(), input[i].ToString().ToUpper()) &&
                        RomanSymbol.IsSmaller(input[i - 2].ToString().ToUpper(), input[i].ToString().ToUpper()))
                {
                    Console.WriteLine("SingleSubtraction Rule has been violated");
                    return false;
                }
            }
            return true;
        }

    }
}
