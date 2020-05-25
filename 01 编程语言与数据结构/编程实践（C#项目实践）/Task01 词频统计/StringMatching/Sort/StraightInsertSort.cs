using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class StraightInsertSort : SortStrategy
    {
        public override void Sort<T>(List<T> nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException();

            for (int i = 1; i < nodes.Count; i++)
            {
                int j = i - 1;
                T current = nodes[i];

                while (j >= 0 && nodes[j].CompareTo(current) > 0)
                {
                    nodes[j + 1] = nodes[j];
                    j--;
                }
                nodes[j + 1] = current;
            }
        }
    }
}
