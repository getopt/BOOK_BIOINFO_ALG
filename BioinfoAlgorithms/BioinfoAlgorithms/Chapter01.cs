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
        public Dictionary<string, int> Alphabet = new Dictionary<string, int>();
        public Dictionary<int, string> ToAlphabet = new Dictionary<int, string>();

        public Chapter01()
        {
            Alphabet.Add("A", (int)BioinfoAlgorithms.Alphabet.A);
            Alphabet.Add("C", (int)BioinfoAlgorithms.Alphabet.C);
            Alphabet.Add("G", (int)BioinfoAlgorithms.Alphabet.G);
            Alphabet.Add("T", (int)BioinfoAlgorithms.Alphabet.T);

            ToAlphabet.Add((int)BioinfoAlgorithms.Alphabet.A, "A" );
            ToAlphabet.Add((int)BioinfoAlgorithms.Alphabet.C, "C" );
            ToAlphabet.Add((int)BioinfoAlgorithms.Alphabet.G, "G" );
            ToAlphabet.Add((int)BioinfoAlgorithms.Alphabet.T, "T" );
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
                    var alphabet = EnumUtil.GetValues<Alphabet>();
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
                    
                return ToAlphabet[index];
            }

            int prefixIndex = index/Program.AlpabetLength;
            int r = index%Program.AlpabetLength;

            string symbol = ToAlphabet[r];

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
