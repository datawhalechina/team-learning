using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class HeapSelectSort : SortStrategy
    {
        private static void Restore<T>(T[] nodes, int j, int vCount) where T : IComparable<T>
        {
            //构建以结点j为根,一共有vCount个结点的大根堆
            while (j <= vCount / 2)
            {
                int m = (2 * j + 1 <= vCount && nodes[2 * j + 1].CompareTo(nodes[2 * j]) > 0) ? 2 * j + 1 : 2 * j;
                if (nodes[m].CompareTo(nodes[j]) > 0)
                {
                    T temp = nodes[m];
                    nodes[m] = nodes[j];
                    nodes[j] = temp;
                    j = m;
                }
                else
                {
                    break;
                }
            }
        }

        public override void Sort<T>(List<T> nodes)
        {
            int vCount = nodes.Count;
            T[] tempKey = new T[vCount + 1];

            for (int i = 0; i < vCount; i++)
                tempKey[i + 1] = nodes[i];

            for (int i = vCount / 2; i >= 1; i--)
                Restore(tempKey, i, vCount);

            for (int i = vCount; i > 1; i--)
            {
                T temp = tempKey[i];
                tempKey[i] = tempKey[1];
                tempKey[1] = temp;
                Restore(tempKey, 1, i - 1);
            }

            for (int i = 0; i < vCount; i++)
                nodes[i] = tempKey[i + 1];
        }
    }
}
