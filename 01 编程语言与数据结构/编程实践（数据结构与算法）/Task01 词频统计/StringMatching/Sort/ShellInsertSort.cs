using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class ShellInsertSort : SortStrategy
    {
        private void Shell<T>(int delta, List<T> nodes) where T : IComparable<T>
        {
            if (nodes == null)
                throw new ArgumentNullException();

            //带增量的直接插入排序
            for (int i = delta; i < nodes.Count; i++)
            {
                int j = i - delta;
                T current = nodes[i];

                while (j >= 0 && nodes[j].CompareTo(current) > 0)
                {
                    nodes[j + delta] = nodes[j];
                    j = j - delta;
                }
                nodes[j + delta] = current;
            }

        }

        public override void Sort<T>(List<T> nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException();

            for (int delta = nodes.Count / 2; delta > 0; delta = delta / 2)
            {
                Shell(delta, nodes);
            }
        }
    }
}
