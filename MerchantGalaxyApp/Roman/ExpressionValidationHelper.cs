using MerchantGalaxyApp.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp
{
    public class ExpressionValidationHelper
    {
        private readonly RomanPseudonymMapper _pseudonymMap;
        private readonly WordMapper _wordMap;

        public ExpressionValidationHelper(RomanPseudonymMapper pseudonymMap, WordMapper wordMap)
        {
            _pseudonymMap = pseudonymMap;
            _wordMap = wordMap;
        }

        public bool AreWordsValidAliases(string[] words)
        {
            foreach (string word in words) { if (!_pseudonymMap.Exists(word)) { return false; } }
            return true;
        }

        public bool AreWordsValidCommodities(string[] words)
        {
            foreach (string word in words) { if (!_wordMap.Exists(word)) { return false; } }
            return true;
        }
    }
}
