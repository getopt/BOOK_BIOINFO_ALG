using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    class RunChapter05
    {
        public RunChapter05(string excercise)
        {
            Chapter05 chapter = new Chapter05();

            List<int> coins = new List<int>() {1,4,5};
            int money = 7;
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
    }
}
