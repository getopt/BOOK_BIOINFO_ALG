using System;
using System.Collections.Generic;
using System.Data;
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
                    string dna = "ATTGGTTTAGGTGTTTTCCCGAAGT";
                    int k = 4;
                    int d = 1;
                    Console.WriteLine(chapter.MotifEnumerator(dna, k, d));
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
        public List<string> MotifEnumerator(string dna, int k, int d)
        {
            List<string> patterns = new List<string>();
            var windows = StringSlidingWindows(dna, k);

            foreach (int[] window in windows)
            {
                var patternP = dna.Substring(window[0], k);
                Console.WriteLine(patternP);

                Console.ReadLine();
            }
            return patterns;
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
