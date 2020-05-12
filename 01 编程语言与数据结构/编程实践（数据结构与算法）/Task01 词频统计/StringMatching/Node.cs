using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class Node:IComparable<Node>
    {
        public int Count { get; set; }
        public string SubString { get; set; }

        public Node(string substr)
        {
            Count = 0;
            SubString = substr;
        }

        public int CompareTo(Node other)
        {
            if (Count == other.Count)
                return 0;
            else if (Count < other.Count)
                return -1;
            else
                return 1;
        }
    }
}
