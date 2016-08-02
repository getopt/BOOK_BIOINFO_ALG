using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    public enum Alphabet
    {
        A,
        C,
        G,
        T,
    }
    class Program
    {
        public const int AlpabetLength = 4;

        static void Main(string[] args)
        {
           var run_1L = new RunChapter01("1L");
           var run_1M = new RunChapter01("1M");
        }
    }
}
