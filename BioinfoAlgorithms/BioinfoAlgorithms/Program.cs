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

    public static class EnumUtil {
        // from http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }    
    class Program
    {
        public const int AlpabetLength = 4;

        static void Main(string[] args)
        {
            var run_2A = new RunChapter02("2A"); 

            // var run_1N = new RunChapter01("1N");
            // var run_1M = new RunChapter01("1M");
            // var run_1L = new RunChapter01("1L");
        }
    }
}
