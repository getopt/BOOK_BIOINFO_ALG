using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    public static class Data
    {
        public static List<Cell> Right = new List<Cell>
         {
             new Cell() {Row = 0, Column = 0, Score = 0},
             new Cell() {Row = 0, Column = 1, Score = 3},
             new Cell() {Row = 0, Column = 2, Score = 2},
             new Cell() {Row = 0, Column = 3, Score = 4},
             new Cell() {Row = 0, Column = 4, Score = 0},
             
             new Cell() {Row = 1, Column = 0, Score = 0},
             new Cell() {Row = 1, Column = 1, Score = 3},
             new Cell() {Row = 1, Column = 2, Score = 2},
             new Cell() {Row = 1, Column = 3, Score = 4},
             new Cell() {Row = 1, Column = 4, Score = 2},

             new Cell() {Row = 2, Column = 0, Score = 0},
             new Cell() {Row = 2, Column = 1, Score = 0},
             new Cell() {Row = 2, Column = 2, Score = 7},
             new Cell() {Row = 2, Column = 3, Score = 3},
             new Cell() {Row = 2, Column = 4, Score = 4},

             new Cell() {Row = 3, Column = 0, Score = 0},
             new Cell() {Row = 3, Column = 1, Score = 3},
             new Cell() {Row = 3, Column = 2, Score = 3},
             new Cell() {Row = 3, Column = 3, Score = 0},
             new Cell() {Row = 3, Column = 4, Score = 2},

             new Cell() {Row = 4, Column = 0, Score = 0},
             new Cell() {Row = 4, Column = 1, Score = 1},
             new Cell() {Row = 4, Column = 2, Score = 3},
             new Cell() {Row = 4, Column = 3, Score = 2},
             new Cell() {Row = 4, Column = 4, Score = 2},
         };

         public static List<Cell> Down = new List<Cell>
         {
             new Cell() {Row = 0, Column = 0, Score = 0},
             new Cell() {Row = 0, Column = 1, Score = 0},
             new Cell() {Row = 0, Column = 2, Score = 0},
             new Cell() {Row = 0, Column = 3, Score = 0},
             new Cell() {Row = 0, Column = 4, Score = 0},
             
             new Cell() {Row = 1, Column = 0, Score = 1},
             new Cell() {Row = 1, Column = 1, Score = 0},
             new Cell() {Row = 1, Column = 2, Score = 2},
             new Cell() {Row = 1, Column = 3, Score = 4},
             new Cell() {Row = 1, Column = 4, Score = 3},

             new Cell() {Row = 2, Column = 0, Score = 4},
             new Cell() {Row = 2, Column = 1, Score = 6},
             new Cell() {Row = 2, Column = 2, Score = 5},
             new Cell() {Row = 2, Column = 3, Score = 2},
             new Cell() {Row = 2, Column = 4, Score = 1},

             new Cell() {Row = 3, Column = 0, Score = 4},
             new Cell() {Row = 3, Column = 1, Score = 4},
             new Cell() {Row = 3, Column = 2, Score = 5},
             new Cell() {Row = 3, Column = 3, Score = 2},
             new Cell() {Row = 3, Column = 4, Score = 1},

             new Cell() {Row = 4, Column = 0, Score = 5},
             new Cell() {Row = 4, Column = 1, Score = 6},
             new Cell() {Row = 4, Column = 2, Score = 8},
             new Cell() {Row = 4, Column = 3, Score = 5},
             new Cell() {Row = 4, Column = 4, Score = 3},
         };
    }
}
