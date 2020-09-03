using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Mapper
{
    public class RomanPseudonymMapper
    {
        private readonly Dictionary<string, string> pseudonymMap;
        public RomanPseudonymMapper()
        {
            pseudonymMap = new Dictionary<string, string>();
        }

        public void AddPseudonym(string pseudonym, string value)
        {
            if (!pseudonymMap.ContainsKey(pseudonym))
                pseudonymMap.Add(pseudonym, value);
            else
                pseudonymMap[pseudonym] = value;
        }

        public string GetValueForPseudonym(string pseudonym)
        {
            return pseudonymMap[pseudonym].ToUpper();
        }

        public bool Exists(string pseudonym)
        {
            return pseudonymMap.ContainsKey(pseudonym);
        }
    }
}
