using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringMatching
{
    public class LambdaSort:SortStrategy
    {
        public override void Sort<T>(List<T> nodes)
        {
            nodes.Sort((node1, node2) => node1.CompareTo(node2));            
        }
    }
}
