using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinfoAlgorithms
{

    class RunChapter03
    {
        public RunChapter03(string excercise)
        {
            Chapter01 chapter = new Chapter01();

            switch (excercise)
            {
                case "test_graph":
                    Console.WriteLine("Add First:");
                    LinkedList myList1 = new LinkedList();

                    myList1.AddFirst("Hello");
                    myList1.AddFirst("Magical");
                    myList1.AddFirst("World");
                    myList1.PrintAllNodes();

                    Console.WriteLine();

                    Console.WriteLine("Add Last:");
                    LinkedList myList2 = new LinkedList();

                    myList2.AddLast("Hello");
                    myList2.AddLast("Magical");
                    myList2.AddLast("World");
                    myList2.PrintAllNodes();

                    Console.ReadLine();
                    break;
                case "3A":
                    break;
                default:
                    Console.Write("Cannot interpret exercise number");
                    Console.ReadLine();
                    break;
            }
        }
    }

    class Chapter03
    {
    }

    public class Node
    {
        public Node Next;
        public Object Data;
    }

    public class LinkedList
    {
        private Node _head;

        public void PrintAllNodes()
        {
            Node current = _head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        public void AddFirst(object data)
        {
            Node toAdd = new Node();

            toAdd.Data = data;
            toAdd.Next = _head;

            _head = toAdd;
        }

        public void AddLast(object data)
        {
            if (_head == null)
            {
                _head = new Node();

                _head.Data = data;
                _head.Next = null;
            }
            else
            {
                Node toAdd = new Node();
                toAdd.Data = data;

                Node current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toAdd;
            }
        }
    }



}
