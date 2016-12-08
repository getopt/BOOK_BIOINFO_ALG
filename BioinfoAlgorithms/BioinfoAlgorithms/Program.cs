using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    public enum AlphabetEnum
    {
        A,
        C,
        G,
        T,
    }
    class Program
    {

        public static Random Rnd = new Random();

        public const int AlpabetLength = 4;

        static void Main(string[] args)
        {
            var run_5B_ManhattanTourist = new RunChapter05("5B_ManhattanTourist");
            // var run_5A_DPChange = new RunChapter05("5A_DPChange");
            // var run_5_RecChange = new RunChapter05("_RecursiveChange");

            // var run_3A = new RunChapter03("test_graph");

            // var run_2H = new RunChapter02("2H"); 
            // var run_2G = new RunChapter02("2G_iterative");
            // var run_2G = new RunChapter02("2G");
            // var run_2F = new RunChapter02("2F_iterative");
            // var run_2F = new RunChapter02("2F");
            // var run_2E = new RunChapter02("2E");
            // var run_2D = new RunChapter02("2D");
            // var run_2C = new RunChapter02("2C");
            // var run_2B = new RunChapter02("2B");
            // var run_2A = new RunChapter02("2A"); 

            // var run_1N = new RunChapter01("1N");
            // var run_1M = new RunChapter01("1M");
            // var run_1L = new RunChapter01("1L");
        }
    }
}
