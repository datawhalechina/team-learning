using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace StringMatching
{
    public class SubstrStatic : Context
    {
        private string _source;
        private readonly List<Node> _substring;

        public SubstrStatic()
        {
            _source = string.Empty;
            _substring = new List<Node>();
        }

        /// <summary>
        /// 读取文件中的内容，把文件内容存入源串。
        /// </summary>
        /// <param name="filePath"></param>
        public void SourceFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentException("filePath");

            string error1 = filePath + "文件不存在.";
            if (File.Exists(filePath) == false)
                throw new ApplicationException(error1);

            try
            {
                string line =  File.ReadAllText(filePath, Encoding.Default);
                line = line.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                _source = line;
            }
            catch (Exception ex)
            {
                string error2 = "读取文件失败,原因:" + ex.Message;
                throw new ApplicationException(error2);
            }

        }

        /// <summary>
        /// 读取目录中的所有txt文件，把文件内容存入源串。
        /// </summary>
        /// <param name="path"></param>
        public void SourceFromPath(string path)
        {
            if (path == null)
                throw new ArgumentException("path");

            string error = path + "路径不存在.";
            if (Directory.Exists(path) == false)
                throw new ApplicationException(error);

            try
            {
                DirectoryInfo folder = new DirectoryInfo(path);
                _source = string.Empty;
                foreach (FileInfo file in folder.GetFiles("*.txt"))
                {
                    string line = File.ReadAllText(file.FullName, Encoding.Default);
                    line = line.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                    _source += line;
                }
            }
            catch (Exception ex)
            {
                string error2 = "读取文件失败,原因:" + ex.Message;
                throw new ApplicationException(error2);
            }
        }

        /// <summary>
        /// 从文件中读取关键词，把这些子串结构存入链表。
        /// </summary>
        /// <param name="filePath"></param>
        public void SubStringFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentException("filePath");

            string error1 = filePath + "文件不存在.";
            if (File.Exists(filePath) == false)
                throw new ApplicationException(error1);

            try
            {
                //string line =  File.ReadAllText(filePath, Encoding.Default);
                string line = string.Empty;
                _substring.Clear();
                StreamReader sr = new StreamReader(filePath, Encoding.Default);
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line) == false)
                        _substring.Add(new Node(line));
                }
            }
            catch (Exception ex)
            {
                string error2 = "读取文件失败,原因:" + ex.Message;
                throw new ApplicationException(error2);
            }

        }

        public List<Node> Run()
        {
            const string error1 = "失败:\n1.待分析文件未被初始化.\n2.待分析文件为空.";

            if (string.IsNullOrEmpty(_source) == true)
                throw new ApplicationException(error1);

            const string error2 = "失败:\n1.词典文件未被初始化.\n2.词典文件为空.";
            if (_substring.Count == 0)
                throw new ApplicationException(error2);

            for (int i = 0; i < _substring.Count; i++)
                _substring[i].Count = base.ContextRunMathing(_source, _substring[i].SubString);

            base.ContextRunSort(_substring);
            return _substring;
        }

        public void Write(List<Node> lst)
        {
            string fileName = Path.GetTempFileName();
            FileInfo myFile = new FileInfo(fileName);

            StreamWriter sw = myFile.CreateText();
            for (int i = lst.Count - 1; i >= 0; i--)
            {
                string str = lst[i].SubString.PadRight(15, '—');
                str += lst[i].Count.ToString(CultureInfo.InvariantCulture);
                sw.WriteLine(str);
            }
            sw.Close();
            System.Diagnostics.Process.Start("notepad.exe", fileName);
        }
    }
}
