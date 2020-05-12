using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class PrefixBruteForceMatching : StringMatchingStrategy
    {
        public override int StringMatchingAlgorithm(string source, string substr)
        {
            if (source == null)
                throw new ArgumentException(nameof(source));
            if (substr == null)
                throw new ArgumentException(nameof(substr));

            int count = 0;
            for (int i = 0; i <= source.Length - substr.Length; i++)
            {
                if (source[i] == substr[0])
                {
                    int j = 1;
                    for (; j < substr.Length; j++)
                    {
                        if (substr[j] != source[i + j])
                            break;
                    }
                    if (j == substr.Length)
                        count++;
                }
            }
            return count;
        }
    }
}
