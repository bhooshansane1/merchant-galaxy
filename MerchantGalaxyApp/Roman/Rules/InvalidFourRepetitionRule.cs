using MerchantGalaxyApp.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Rules
{
    public class InvalidFourRepetitionRule :IRule
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 4) || !(input.ToUpper().Contains("IIII") || input.ToUpper().Contains("XXXX") || input.ToUpper().Contains("CCCC") || input.ToUpper().Contains("MMMM"));

            if (!result) { Console.WriteLine("InvalidFourRepetitionRule Rule has been violated"); }

            return result;
        }
    }
}
