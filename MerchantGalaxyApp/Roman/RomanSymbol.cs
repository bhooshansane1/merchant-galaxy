using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Roman
{
    public class RomanSymbol
    {
        private const string ALPHABETS = "IVXLCDM";
        private static readonly string[] INVALID_VALUES = new string[] { "IVI", "IVII", "IVIII","IXI", "IXIII", "IXIII",
            "XLX", "XLXX", "XLXXX","XCX", "XCXX", "XCXXX","CDC","CDCC","CDCCC","CMC","CMCC","CMCCC" };
        private const string NOSUBTRACTION_VALUES = "VLD";
        public static string GetAlphabet()
        {
            return ALPHABETS;
        }

        public static bool IsSmaller(string first, string second)
        {
            return ALPHABETS.IndexOf(first, StringComparison.OrdinalIgnoreCase) <
                    ALPHABETS.IndexOf(second, StringComparison.OrdinalIgnoreCase);
        }


        public static bool IsSubtractionAllowed(string inputStr)
        {
            return !(NOSUBTRACTION_VALUES.Contains(inputStr));
        }

        public static bool IsValidSymbol(string input)
        {
            for (int i = 0; i < INVALID_VALUES.Length; i++)
            {
                if (input.Contains(INVALID_VALUES[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
