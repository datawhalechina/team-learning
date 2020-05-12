using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class StraightSelectSort : SortStrategy
    {
        public override void Sort<T>(List<T> nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException();

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                int k = i;
                T current = nodes[i];

                for (int j = i + 1; j < nodes.Count; j++)
                {
                    if (nodes[j].CompareTo(current) < 0)
                    {
                        current = nodes[j];
                        k = j;
                    }
                }
                if (k != i)
                {
                    nodes[k] = nodes[i];
                    nodes[i] = current;
                }
            }
        }
    }
}
