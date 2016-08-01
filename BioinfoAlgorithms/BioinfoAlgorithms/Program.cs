using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Chapter01 chapter = new Chapter01();

            int result = chapter.PatternToNumber("foo");

            Console.WriteLine(result);
            var name = Console.ReadLine();

        }
    }
}
