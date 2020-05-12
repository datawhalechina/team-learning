using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public abstract class StringMatchingStrategy
    {
        public abstract int StringMatchingAlgorithm(string source, string substr);
    }
}
