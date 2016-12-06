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

            switch (excercise)
            {
                case "_RecursiveChange":
                    List<int> coins = new List<int>() {5,4,1};
                    int money = 12;
                    Console.WriteLine(chapter.RecursiveChange(money, coins));
                    Console.ReadLine();
                    break;
                case "5B":
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
