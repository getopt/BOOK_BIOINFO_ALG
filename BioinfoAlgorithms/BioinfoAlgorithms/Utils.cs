using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    public class Cell
    {
        public Cell() { }
        public int Row;
        public int Column;
        public int Weight;
    }

    // from http://stackoverflow.com/questions/972307/can-you-loop-through-all-enum-values
    public static class EnumUtil {
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public class CellList
    {
        public List<Cell> InternalList = new List<Cell>();
        
        /// <summary>
        /// Add Cell object to cell list. No checking if cell 
        /// with such row and columns already exists.
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="column">column index</param>
        /// <param name="weight">value to put in the cell</param>
        public void Add(int row, int column, int weight)
        {
            InternalList.Add(new Cell() {Row = row, Column = column, Weight = weight});
        }
        public static IEnumerable<int> GetWeight(CellList cellList, int i, int j)
        {
            var weights = from cell in cellList.InternalList
                where cell.Row == i
                where cell.Column == j
                select cell.Weight;

            return weights;
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
