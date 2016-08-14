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

    // from http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values
    public static class EnumUtil {
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    // from http://stackoverflow.com/questions/17887407/dictionary-with-list-of-strings-as-value
    public class MyListDictionary
    {

        public Dictionary<string, List<string>> InternalDictionary = new Dictionary<string, List<string>>();

        public void Add(string key, string value)
        {
            if (this.InternalDictionary.ContainsKey(key))
            {
                List<string> list = this.InternalDictionary[key];
                if (list.Contains(value) == false)
                {
                    list.Add(value);
                }
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(value);
                this.InternalDictionary.Add(key, list);
            }

        }
    }

    public class ProfileMatrixEntry
    {
        public string Base { get; set; }
        public int Pos { get; set; }
        public double Prob { get; set; }
    }

    class Program
    {

        public static Random Rnd = new Random();

        public const int AlpabetLength = 4;

        static void Main(string[] args)
        {
            // var run_2H = new RunChapter02("2H"); 
            var run_2F = new RunChapter02("2F");
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
