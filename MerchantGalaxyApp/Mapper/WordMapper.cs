using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantGalaxyApp.Mapper
{
    public class WordMapper
    {
        private readonly Dictionary<string, double> wordMap;

        public WordMapper()
        {
            wordMap = new Dictionary<string, double>();
        }

        public void AddWord(string name, double perUnitPrice)
        {
            if (!wordMap.ContainsKey(name)) wordMap.Add(name, perUnitPrice);
            else wordMap[name] = perUnitPrice;
        }

        public double GetPriceByWord(string material)
        {
            return wordMap[material.ToLower()];
        }

        public bool Exists(string material)
        {
            return wordMap.ContainsKey(material.ToLower());
        }
    }
}
