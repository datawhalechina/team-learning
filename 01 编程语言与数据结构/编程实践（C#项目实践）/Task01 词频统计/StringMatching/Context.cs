using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;

namespace StringMatching
{
    public class Context
    {
        private const string AssemblyName = "StringMatching";

        private readonly string _sortClassName =
            AssemblyName + "." + ConfigurationManager.AppSettings["SortClassName"];

        private readonly string _matchingClassName =
            AssemblyName + "." + ConfigurationManager.AppSettings["MatchingClassName"];

        private readonly StringMatchingStrategy _stringMatchingStrategy;
        private readonly SortStrategy _sortStrategy;

        /// <summary>
        /// 获取当前所用的排序算法
        /// </summary>
        public string SortClassName
        {
            get { return _sortClassName; }
        }

        /// <summary>
        /// 获取当前所用的字符串匹配算法
        /// </summary>
        public string MatchingClassName
        {
            get { return _matchingClassName; }
        }


        public Context()
        {
            _sortStrategy =
                Assembly.Load(AssemblyName).CreateInstance(_sortClassName) as SortStrategy;
            _stringMatchingStrategy =
                Assembly.Load(AssemblyName).CreateInstance(_matchingClassName) as StringMatchingStrategy;
        }

        public int ContextRunMathing(string source, string substr)
        {
            return _stringMatchingStrategy.StringMatchingAlgorithm(source, substr);
        }

        public void ContextRunSort(List<Node> list)
        {
            _sortStrategy.Sort(list);            
        }
    }
}
