using System;
using System.Collections.Generic;
using System.Text;

namespace StringMatching
{
    public class KmpMatching : StringMatchingStrategy
    {
        private int[] GetNext(string substr)
        {
            if (substr == null)
                throw new ArgumentException();
            
            int i = 0, j = -1;
            int[] nextVal = new int[substr.Length];

            nextVal[0] = -1;

            while (i < substr.Length - 1)
            {
                if (j == -1 || substr[i] == substr[j])
                {
                    i++;
                    j++;
                    if (substr[i] != substr[j])
                        nextVal[i] = j;
                    else
                        nextVal[i] = nextVal[j];
                }
                else
                {
                    j = nextVal[j];
                }
            }
            return nextVal;
        }

        public override int StringMatchingAlgorithm(string source, string substr)
        {
            if (source == null)
                throw new ArgumentException("source");
            if (substr == null)
                throw new ArgumentException("substr");
            
            int[] nextVal = GetNext(substr);
            int i = 0, j = 0;
            int count = 0;
           
            while (i < source.Length)
            {
                if (j == -1 || source[i] == substr[j])
                {
                    if (j == substr.Length-1)
                    {
                        count++;
                        j = -1;
                        continue;
                    }
                    i++;
                    j++;
                }
                else
                {
                    j = nextVal[j];
                }
                //if (j >= substr.Length)
                //{
                //    count++;
                //    j = -1;
                //}
            }

            return count;
        }
    }
}
