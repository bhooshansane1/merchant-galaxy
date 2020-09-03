using MerchantGalaxyApp.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Rules
{
    public class SubtractionRule :IRule
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 2) ||
                    !(input.ToUpper().Contains("IL") || input.ToUpper().Contains("IC") || input.ToUpper().Contains("ID") || input.ToUpper().Contains("IM") ||
                    input.ToUpper().Contains("XD") || input.ToUpper().Contains("XM"));

            if (!result) { Console.WriteLine("Subtraction Rule has been violated"); }

            return result;
        }
    }
}
