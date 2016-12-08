using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{
    public class Data
    {
        public CellList Right = new CellList();
        public CellList Down  = new CellList();

        public Data()
        {
            PopulateCellLists();    
        }
        public void PopulateCellLists()
        {
            Right.Add(0,0,0);
            Right.Add(0,1,3);
            Right.Add(0,2,2);
            Right.Add(0,3,4);
            Right.Add(0,4,0);

            Right.Add(1,0,0);
            Right.Add(1,1,3);
            Right.Add(1,2,2);
            Right.Add(1,3,4);
            Right.Add(1,4,2);

            Right.Add(2,0,0);
            Right.Add(2,1,0);
            Right.Add(2,2,7);
            Right.Add(2,3,3);
            Right.Add(2,4,4);

            Right.Add(3,0,0);
            Right.Add(3,1,3);
            Right.Add(3,2,3);
            Right.Add(3,3,0);
            Right.Add(3,4,2);

            Right.Add(4,0,0);
            Right.Add(4,1,1);
            Right.Add(4,2,3);
            Right.Add(4,3,2);
            Right.Add(4,4,2);

            Down.Add(0,0,0);
            Down.Add(0,1,0);
            Down.Add(0,2,0);
            Down.Add(0,3,0);
            Down.Add(0,4,0);

            Down.Add(1,0,1);
            Down.Add(1,1,0);
            Down.Add(1,2,2);
            Down.Add(1,3,4);
            Down.Add(1,4,3);

            Down.Add(2,0,4);
            Down.Add(2,1,6);
            Down.Add(2,2,5);
            Down.Add(2,3,2);
            Down.Add(2,4,1);

            Down.Add(3,0,4);
            Down.Add(3,1,4);
            Down.Add(3,2,5);
            Down.Add(3,3,2);
            Down.Add(3,4,1);

            Down.Add(4,0,5);
            Down.Add(4,1,6);
            Down.Add(4,2,8);
            Down.Add(4,3,5);
            Down.Add(4,4,3);
        }

    }
}
