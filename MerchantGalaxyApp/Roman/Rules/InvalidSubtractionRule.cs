using MerchantGalaxyApp.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Roman.Rules
{
    public class InvalidSubtractionRule : IRule
    {
        public bool Execute(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 1)
                {
                    if (RomanSymbol.IsSmaller(input[i].ToString(), input[i+1].ToString()) && !RomanSymbol.IsSubtractionAllowed(input[i].ToString()))
                    {
                        Console.WriteLine("InvalidSubtraction Rule has been violated");
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}
