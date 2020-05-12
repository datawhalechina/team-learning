using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public abstract class SortStrategy
    {
        public abstract void Sort<T>(List<T> nodes) where T : IComparable<T>;
    }
}
