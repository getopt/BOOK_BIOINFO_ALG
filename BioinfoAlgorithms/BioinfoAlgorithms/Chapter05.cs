using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    class RunChapter05
    {
        Data data = new Data();
        public RunChapter05(string excercise)
        {
            Chapter05 chapter = new Chapter05();
            
            List<int> coins = new List<int>() {1,4,5};
            int money = 7;

            string v = "ACGT";
            string w = "TTGT";    

            switch (excercise)
            {
                case "_RecursiveChange":
                    Console.WriteLine(chapter.RecursiveChange(money, coins));
                    Console.ReadLine();
                    break;
                case "5A_DPChange":
                    Console.WriteLine(chapter.DPChange(money, coins));
                    Console.ReadLine();
                    break;
                case "5B_ManhattanTourist":
                    int pathLength = chapter.ManhattanTourist(2, 2, data.Down, data.Right);
                    Console.WriteLine(pathLength);
                    Console.ReadLine();
                    break;
                case "5C_OutputLCS":
                    CellList backtrack = chapter.LCSBactrack(v, w);
                    chapter.OutputLCS(backtrack, v, 4, 4);
                    Console.WriteLine(pathLength);
                    Console.ReadLine();
                    break;
                default:
                    Console.Write("Cannot interpret exercise number");
                    Console.ReadLine();
                    break;
            }
        }
    }
    class Chapter05
    {
        public int DPChange(int money, List<int> coins)
        {
            List<int> minNumCoins = new List<int>();
            minNumCoins.Add(0);
            for (int m = 1; m <= money; m++)
            {
                minNumCoins.Add(10000);
                foreach (int coin in coins)
                {
                    if (m >= coin)
                    {
                        if (minNumCoins[m - coin] + 1  <= minNumCoins[m])
                        {
                            minNumCoins[m] = minNumCoins[m - coin] + 1;
                        }
                    }
                }
            }
            return minNumCoins[money];
        }
        public int RecursiveChange(int money, List<int> coins)
        {
            if (money == 0)
                return 0;
            int minNumCoins = 100000;
            foreach (int coin in coins)
            {
                if (money >= coin)
                {
                    int numCoins = RecursiveChange(money - coin, coins);
                    if (numCoins + 1 < minNumCoins)
                    {
                        minNumCoins = numCoins + 1;
                    }
                }
            }
            return minNumCoins;
        }

        public int ManhattanTourist(int n, int m, CellList down, CellList right)
        {
            CellList path = new CellList();
            path.Add(0,0,0);

            for (int i = 1; i <= n; i++)
            {
                var weightUpSum = (int)CellList.GetWeight(path, i - 1, 0).First();
                var weightDown = (int)CellList.GetWeight(down, i, 0).First();
                path.Add(i, 0, weightUpSum + weightDown);
            }
            for (int j = 1; j <= m; j++)
            {
                var weightToLeft = (int) CellList.GetWeight(path, 0, j - 1).First();
                var weightRight = (int)CellList.GetWeight(right, 0, j).First();
                path.Add(0, j, weightToLeft + weightRight);
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    path.Add(i, j, Math.Max(
                        (int)CellList.GetWeight(path, i-1, j).First() +
                        (int)CellList.GetWeight(down, i, j).First(),
                        (int)CellList.GetWeight(path, i, j-1).First() +
                        (int)CellList.GetWeight(right, i, j).First()
                        )
                    );
                }
            }

            int pathLength = (int)CellList.GetWeight(path, n, m).First();

            return pathLength;
        }

        public CellList LCSBactrack(string v, string w)
        {
            CellList backtrack = new CellList();
            CellList scores = new CellList();
            for (int i = 0; i < v.Length; i++)
                scores.Add(i,0,0); 
            for(int j = 0; j < w.Length; j++)
                scores.Add(0,j,0);

            for (int i = 1; i < v.Length; i++)
            {
                for (int j = 1; j < w.Length; j++)
                {
                    scores.Add(i, j, Math.Max(
                        (int)CellList.GetWeight(path, i-1, j).First() +
                        (int)CellList.GetWeight(down, i, j).First(),
                        (int)CellList.GetWeight(path, i, j-1).First() +
                        (int)CellList.GetWeight(right, i, j).First()
                        )
                    
                }
            }
        }

        public void OutputLCS(CellList backtrack, string s, int i, int i1)
        {
            throw new NotImplementedException();
        }
    }
}
