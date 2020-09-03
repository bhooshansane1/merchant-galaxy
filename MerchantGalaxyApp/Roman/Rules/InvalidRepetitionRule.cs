using MerchantGalaxyApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerchantGalaxyApp.Rules
{
    public class InvalidRepetitionRule :IRule
    {
        public bool Execute(string input)
        {
            bool result = (input.Length < 2) ||
                    (input.ToUpper().Count(c => c == 'D') <= 1 && input.ToUpper().Count(c => c == 'L') <= 1 && input.ToUpper().Count(c => c == 'V') <= 1);

            if (!result)
            {
                Console.WriteLine("InvalidRepetitionRule Rule has been violated");
            }

            return result;
        }
    }
}
