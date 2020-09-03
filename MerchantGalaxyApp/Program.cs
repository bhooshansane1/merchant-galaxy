using MerchantGalaxyApp.Contract;
using MerchantGalaxyApp.Mapper;
using System;
using System.IO;

namespace MerchantGalaxyApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("File Details");
            Console.WriteLine("_______________");
            string path = "../../file.txt";
            string readText = File.ReadAllText(path);
            string[] lines = readText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(readText);
            Console.WriteLine();
            RomanPseudonymMapper pseudonymMap = new RomanPseudonymMapper();
            IDecimalConverter converter = new RomanConverter();
            WordMapper wordMap = new WordMapper();
            ExpressionParser parser = new ExpressionParser(pseudonymMap, converter, wordMap);
            foreach (string line in lines)
            {
                parser.Parse(line.ToUpper());
            }
            Console.ReadLine();
        }
    }
}
