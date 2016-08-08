using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BioinfoAlgorithms
{
    class RunChapter02
    {
        public RunChapter02(string excercise)
        {
            Chapter02 chapter = new Chapter02();

            List<string> dnaStrings;
            int k;
            int d;
            switch (excercise)
            {
                case "2A":
                    dnaStrings = new List<string>
                    {
                        "AAAAAAAAATTTAAAAAAA",
                        "AAAAATATTTAAAAAA",
                        "AAAAATTTTAAAAA",
                    };
                    k = 4;
                    d = 0;
                    Console.WriteLine(string.Join("\n", chapter.MotifEnumerator(dnaStrings, k, d)));
                    Console.ReadLine();
                    break;
                case "2B":
                    dnaStrings = new List<string>
                    {
                        "TCTCTCTCTC",
                        "TGTGTGTGTG",
                        "TTTTTTTTTT",
                    };
                    k = 5;
                    Console.WriteLine(chapter.MedianString(dnaStrings, k));
                    Console.ReadLine();
                    break;
                case "2C":
                    k = 12;
                    List<ProfileMatrixEntry> profileMatrix = new List<ProfileMatrixEntry>();
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.A, Pos = 0, Prob = 0.2});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.C, Pos = 0, Prob = 0.1});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.G, Pos = 0, Prob = 0.0});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.T, Pos = 0, Prob = 0.7});

                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.A, Pos = 1, Prob = 0.2});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.C, Pos = 1, Prob = 0.6});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.G, Pos = 1, Prob = 0.0});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.T, Pos = 1, Prob = 0.2});

                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.A, Pos = 2, Prob = 0.0});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.C, Pos = 2, Prob = 0.0});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.G, Pos = 2, Prob = 1.0});
                    profileMatrix.Add(new ProfileMatrixEntry {BaseN = (int)BioinfoAlgorithms.Alphabet.T, Pos = 2, Prob = 0.0});

                    int [] entry = new int[2];
                    entry[0] = 3;
                    entry[1] = 0;
                    Console.WriteLine(chapter.MatrixEntry(profileMatrix, entry));
                    Console.ReadLine();
                    break;
                case "2H":
                    dnaStrings = new List<string>
                    {
                        "AAAAAAAAATTTAAAAAAA",
                        "AAAAATATTTAAAAAAGCG",
                        "AAAAATTTTAAAAAGGATT",
                    };
                    string pattern = "AAAAG";
                    Console.WriteLine(chapter.DistanceBetweenPatternAndStrings(pattern, dnaStrings).ToString());
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Cannot interpret exercise number");
                    Console.ReadLine();
                    break;
            }
        }
    }

    class Chapter02:Chapter01
    {
        public double MatrixEntry( List<ProfileMatrixEntry> profileMatrix, int[] coords)
        {
            IEnumerable<double> entries = from a in profileMatrix
                where a.BaseN == coords[0]
                where a.Pos == coords[1]
                select a.Prob;

            return entries.First();
        }

        public string MedianString(List<string> dnaStrings, int k)
        {
            string median = "";

            int distance = 1000000;
            for (int i = 0; i < Math.Pow(EnumUtil.GetValues<BioinfoAlgorithms.Alphabet>().Count(), k) ; i++)
            {
                string pattern = NumberToPattern(i, k);
                int currentDist = DistanceBetweenPatternAndStrings(pattern, dnaStrings);
                if (distance > currentDist)
                {
                    distance = currentDist;
                    median = pattern;
                }

            }

            return median;
        }
        public int DistanceBetweenPatternAndStrings(string pattern, List<string> dnaStrings)
        {
            int k = pattern.Length;
            int distance = 0;

            foreach (string text in dnaStrings)
            {
                int hammingDistance = 1000000;
                List<int[]> windows = StringSlidingWindows(text, k);
                foreach (int[] window in windows)
                {
                    string patternP = text.Substring(window[0], k);
                    int latestHammingDist = HammingDistance(pattern, patternP); 
                    if (hammingDistance > latestHammingDist)
                    {
                        hammingDistance = latestHammingDist;
                    }
                }

                distance += hammingDistance;
            }

            return distance;
        }

        public List<string> MotifEnumerator(List<string> dnaStrings, int k, int d)
        {   
            var patternsDict = new MyListDictionary();

            foreach (string dna in dnaStrings)
            {
                List<int[]> windows = StringSlidingWindows(dna, k);
                foreach (int[] window in windows)
                {
                    string pattern = dna.Substring(window[0], k);
                    List<string> neighbors = Neighbors(pattern, d);
                    foreach (string neighbor in neighbors)
                    {
                        foreach (string dnaP in dnaStrings)
                        {
                            List<int[]> windowsP = StringSlidingWindows(dnaP, k);
                            foreach (int[] windowP in windowsP)
                            {
                                string patternP = dnaP.Substring(windowP[0], k);
                                if (neighbor == patternP)
                                {
                                    patternsDict.Add(pattern,dnaP);
                                }
                            }
                        } 
                    } 
                }
            }

            var patternsList = new List<string>();
            var patterns = new List<string>(patternsDict.InternalDictionary.Keys);
            foreach (string pattern in patterns)
            {
                if (patternsDict.InternalDictionary[pattern].Count == dnaStrings.Count)
                {
                   patternsList.Add(pattern); 
                }
                
            }

            return patternsList;
        }


        public List<int[]> StringSlidingWindows(string foo, int k)
        {
            List<int[]> windows = new List<int[]>();
            for (int i = 0; i <= foo.Length - k; i++)
            {
                int[] coords = new int[2];
                coords[0] = i;
                coords[1] = i + k;
                windows.Add(coords);
            }
            return windows;
        }
    }
}
