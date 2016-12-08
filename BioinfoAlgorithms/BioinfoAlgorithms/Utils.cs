using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
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
}
