using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BioinfoAlgorithms
{
    class RunChapter01
    {
        public RunChapter01(string excercise)
        {
            Chapter01 chapter = new Chapter01();

            switch (excercise)
            {
                case "1L":
                    Console.WriteLine(chapter.PatternToNumber("TTT"));
                    Console.ReadLine();
                    break;
                case "1M":
                    Console.WriteLine(chapter.NumberToPattern(63, 2));
                    Console.ReadLine();
                    break;
                case "1N":
                    Console.WriteLine(string.Join("\n", chapter.Neighbors("ACGT", 2)));
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
        public Dictionary<string, int> AlphabetDict = new Dictionary<string, int>();
        public Dictionary<int, string> ToAlphabetDict = new Dictionary<int, string>();
        public List<string> Alphabet = new List<string>();

        public Chapter01()
        {
            AlphabetDict.Add("A", (int)BioinfoAlgorithms.AlphabetEnum.A);
            AlphabetDict.Add("C", (int)BioinfoAlgorithms.AlphabetEnum.C);
            AlphabetDict.Add("G", (int)BioinfoAlgorithms.AlphabetEnum.G);
            AlphabetDict.Add("T", (int)BioinfoAlgorithms.AlphabetEnum.T);

            ToAlphabetDict.Add((int)BioinfoAlgorithms.AlphabetEnum.A, "A" );
            ToAlphabetDict.Add((int)BioinfoAlgorithms.AlphabetEnum.C, "C" );
            ToAlphabetDict.Add((int)BioinfoAlgorithms.AlphabetEnum.G, "G" );
            ToAlphabetDict.Add((int)BioinfoAlgorithms.AlphabetEnum.T, "T" );

            Alphabet.Add("A");
            Alphabet.Add("C");
            Alphabet.Add("G");
            Alphabet.Add("T");

        }

        public List<string> Neighbors(string pattern, int d)
        {
            if (d == 0)
            {
                List<string> patterns = new List<string> {pattern};
                return patterns;
            }
            if (pattern.Length == 1)
            {
                List<string> patterns = new List<string> {"A","C","G","T"};
                return patterns;
            }

            List<string> neighborhood = new List<string>();
            List<string> suffixNeighbors = Neighbors(pattern.Substring(1, pattern.Length - 1), d);
            foreach (string text in suffixNeighbors)
            {
                if (HammingDistance(pattern.Substring(1, pattern.Length - 1), text) < d)
                {
                    var alphabet = EnumUtil.GetValues<AlphabetEnum>();
                    foreach (var nt in alphabet)
                    {
                        neighborhood.Add(nt.ToString() + text);
                    }
                }
                else
                {
                    neighborhood.Add(pattern[0] + text);
                }
            }

            return neighborhood;

        }

        public int HammingDistance(string pattern, string patternP)
        {
            if (pattern.Length != patternP.Length)
            {
                Console.WriteLine("ERROR: Hamming: Non equal lengths");
                Console.ReadLine();
                Environment.Exit(1);
            }
            int hamming = 0;
            for (int i = 0; i <= pattern.Length - 1; i++)
            {
                if (pattern[i] != patternP[i])
                {
                    hamming++;
                } 
            }
            return hamming;
        }

        public string NumberToPattern(int index, int k)
        {
            if (k == 1)
            {
                if (index > Program.AlpabetLength - 1)
                {
                    Console.WriteLine("Quotient is bigger than 3... is k-mer length too small?");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
                    
                return ToAlphabetDict[index];
            }

            int prefixIndex = index/Program.AlpabetLength;
            int r = index%Program.AlpabetLength;

            string symbol = ToAlphabetDict[r];

            string prefixPattern = NumberToPattern(prefixIndex, k - 1);

            return prefixPattern + symbol;
        }

        public int PatternToNumber(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return 0;
            }

            string symbol = LastSymbol(pattern);
            string prefix = Prefix(pattern);

            return 4 * PatternToNumber(prefix) + AlphabetDict[symbol];
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
