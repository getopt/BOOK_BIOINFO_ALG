using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    class RunChapter02
    {
        public RunChapter02(string excercise)
        {
            Chapter02 chapter = new Chapter02();

            switch (excercise)
            {
                case "2A":
                    List<string> dnaStrings = new List<string>
                    {
                        "AAAAAAAAATTTAAAAAAA",
                        "AAAAATATTTAAAAAAAAA",
                        "AAAAATTTTAAAAAAAAAA",
                    };
                    int k = 4;
                    int d = 0;
                    Console.WriteLine(string.Join("\n", chapter.MotifEnumerator(dnaStrings, k, d)));
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
        public List<string> MotifEnumerator(List<string> dnaStrings, int k, int d)
        {   
            List<string> patterns = new List<string>();
            for(int i = 0; i < dnaStrings.Count; i++)
            {
                string dna = dnaStrings[i];
                List<int[]> windows = StringSlidingWindows(dna, k);

                for (int j = 0; j < dnaStrings.Count; j++)
                {
                    string dnaP = dnaStrings[j];
                    List<int[]> windowsP = StringSlidingWindows(dnaP, k);

                    if (j == i) { break; }

                    foreach (int[] window in windows)
                    {
                        string pattern = dna.Substring(window[0], k);
                        foreach (int[] windowP in windowsP)
                        {
                            string patternP = dna.Substring(windowP[0], k);
                            if (HammingDistance(patternP, pattern) <= d)
                            {
                                patterns.Add(pattern);
                            }
                        }
                    }
                 }
            }
            return patterns.Distinct().ToList();
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
