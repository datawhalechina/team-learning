using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class QuickExchangeSort: SortStrategy
    {
        private void QuickSort<T>(List<T> nodes, int left, int right) where T : IComparable<T>
        {
            //快速排序递归函数
            if (left < right)
            {
                T current = nodes[left];
                int i = left;
                int j = right;
                while (i < j)
                {
                    while (nodes[j].CompareTo(current) > 0 && i < j)
                        j--;
                    while (nodes[i].CompareTo(current) <= 0 && i < j)
                        i++;
                    if (i < j)
                    {
                        T temp = nodes[i];
                        nodes[i] = nodes[j];
                        nodes[j] = temp;
                    }
                }
                nodes[left] = nodes[j];
                nodes[j] = current;
                if (left < j - 1) QuickSort(nodes, left, j - 1);
                if (right > j + 1) QuickSort(nodes, j + 1, right);
            }
        }
        public override void Sort<T>(List<T> nodes)
        {
            QuickSort(nodes, 0, nodes.Count - 1);
        }
    }
}
