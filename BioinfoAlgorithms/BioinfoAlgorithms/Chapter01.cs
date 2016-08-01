using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    class RunChapter01
    {
        public RunChapter01(string excercise)
        {
            switch (excercise)
            {
                case "1L":
                    Chapter01 chapter = new Chapter01();

                    int result = chapter.PatternToNumber("TTT");

                    Console.WriteLine(result);
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Cannot interpret exercise number");
                    Console.ReadLine();
                    break;
            }
        }
        
    }
    class Chapter01
    {
        public static Dictionary<string, int> Alphabet = new Dictionary<string, int>();

        public Chapter01()
        {
            Alphabet.Add("A", (int)BioinfoAlgorithms.Alphabet.A);
            Alphabet.Add("C", (int)BioinfoAlgorithms.Alphabet.C);
            Alphabet.Add("G", (int)BioinfoAlgorithms.Alphabet.G);
            Alphabet.Add("T", (int)BioinfoAlgorithms.Alphabet.T);
        }
        public int PatternToNumber(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return 0;
            }

            string symbol = LastSymbol(pattern);
            string prefix = Prefix(pattern);

            return 4 * PatternToNumber(prefix) + Alphabet[symbol];
        }

        public string LastSymbol(string pattern)
        {
            return pattern[pattern.Length - 1].ToString();
        }

        public string Prefix(string pattern)
        {
            return pattern.Length != 1 ? pattern.Substring(0, pattern.Length - 1) : "".ToString();
        }
    }
}
