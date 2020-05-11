

# Task05：字符串（2天）

我们古人没有电影电视，没有游戏网络，所以文人们就会想出一些文字游戏来娱乐。比如宋代的李禺写了这样一首诗：“枯眼望遥山隔水，往来曾见几心知？壶空怕酌一杯酒，笔下难成和韵诗。途路阻人离别久，讯音无雁寄回迟。孤灯守夜长寥寂，夫忆妻兮父忆儿。”显然这是老公想念老婆和儿子的诗句。曾经和妻儿在一起，享受天伦之乐，现在一个人长久没有回家，也不见书信返回，望着油灯想念亲人，能不伤感吗？

可仔细一读发现，这首诗竟然可以倒过来读：“儿忆父兮妻忆夫，寂寥长夜守灯孤。迟回寄雁无音讯，久别离人阻路途。诗韵和成难下笔，酒杯一酌怕空壶。知心几见曾来往，水隔山遥望眼枯。”这表达了妻子对丈夫的思念。老公离开好久，路途遥远，难以相见。写信不知道写什么，独自喝酒也没什么兴致。只能和儿子夜夜守在家里一盏孤灯下，苦等老公的归来。

这种诗叫做回文诗。它是一种可以倒读或反复回旋阅读的诗体。刚才这首就是正读是丈夫思念妻子，倒读是妻子思念丈夫的古诗。是不是感觉很奇妙呢？
在英文单词中，同样有神奇的地方。“即使是 lover 也有个 over，即使是 friend 也有个 end，即使是 believe 也有个 lie。”你会发现，本来不相干，甚至对立的两个词，却有某种神奇的联系。这可能是创造这几个单词的智者们也没有想到的问题。

今天我们就要来谈谈这些单词或句子组成字符串的相关问题。


## 1. 串的定义与操作

**1.1 串的相关定义**

- 串（string）是由零个或多个字符组成的有限序列，又名字符串，记为`S=”a0a1...an”`。
- 串中包含字符的个数称为串的长度。
- 长度为零的串称为空串（null string）。直接用双引号””表示，在C#中也可用`string.Empty`来表示。

还有一些概念需要解释：
- 空白串：由一个或多个空格组成的串。
- 子串与主串：串中任意连续字符组成的子序列，称为该串的子串。相应的包含子串的串称为主串，即子串是主串的一部分。
- 子串在主串中的位置：子串在主串中第一次出现时，子串第一个字符在主串中的序号。

```
例如：

A=“this is a string”; 
B=“is”;

B在A中的位置为2。
```

- 串相等：长度相等且对应位字符相同。



**1.2 串的操作**

串的逻辑结构和线性表很相似，不同之处在于串针对的是字符集，也就是串中的元素都是字符。因此，对于串的基本操作与线性表是有很大差别的。线性表更关注的是单个元素的操作，比如查找一个元素，插入或删除一个元素，但串中更多的是关注它子串的应用问题，如查找子串位置，得到指定位置子串、替换子串等操作。

关于串的基本操作如下：
- （1）获取串的长度
- （2）获取或设置指定索引处的字符
- （3）在指定位置插入子串
- （4）在指定位置移除给定长度的子串
- （5）在指定位置取子串
- （6）当前串的拷贝
- （7）串连接
- （8）串的匹配

比如C#中，字符串操作还有`ToLower`转小写、`ToUpper`转大写、`IndexOf`从左查找子串位置、`LastIndexOf`从右查找子串位置、`Trim`去除两边空格等比较方便的操作，它们其实就是前面这些基本操作的扩展函数。


![串接口](https://img-blog.csdnimg.cn/20191223194206307.png)


## 2. 串的存储与实现

串的存储结构与线性表相同，分为两种：

- 顺序存储：char类型的数组。由于数组是定长的，就存在一个预定义的最大串长度，它规定在串值后面加一个不计入串长度的结束符，比如`’\0’`来表示串值的终结。
- 链式存储：`SlinkList<char>` （浪费存储空间）


![顺序串类图](https://img-blog.csdnimg.cn/20191223194433900.png)

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 串抽象数据类型的实现--顺序串
    /// </summary>
    public class SeqString : IString
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly char[] CStr; //字符串以'\0'结束

        /// <summary>
        /// 初始化SeqString类的新实例
        /// </summary>
        public SeqString()
        {
            CStr = new char[] {'\0'};
        }

        /// <summary>
        /// 初始化SeqString类的新实例
        /// </summary>
        /// <param name="s">初始字符串</param>
        public SeqString(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            int length = s.Length;
            CStr = new char[length + 1];

            for (int i = 0; i < length; i++)
                CStr[i] = s[i];
            CStr[length] = '\0';
        }

        /// <summary>
        /// 初始化SeqString类的新实例(只能在类的内部使用)
        /// </summary>
        /// <param name="length">串的长度</param>
        protected SeqString(int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException();

            CStr = new char[length + 1];
            CStr[length] = '\0';
        }

        /// <summary>
        /// 右对齐此实例中的字符，在左边用指定的 Unicode 字符填充以达到指定的总长度。
        /// </summary>
        /// <param name="totalWidth">结果字符串中的字符数，等于原始字符数加上任何其他填充字符。</param>
        /// <param name="paddingChar">Unicode 填充字符。</param>
        /// <returns>
        /// 等效于此实例的一个新 IString，但它是右对齐的，并在左边用达到 totalWidth 长度所需数目的 paddingChar 字符进行填充。
        /// 如果totalWidth 小于此实例的长度，则为与此实例相同的新 IString。
        /// </returns>
        /// <remarks>
        /// 异常:
        /// System.ArgumentOutOfRangeException:totalWidth 小于零。
        /// </remarks>
        public IString PadLeft(int totalWidth, char paddingChar)
        {
            if (totalWidth < 0)
                throw new ArgumentOutOfRangeException();
            if (Length >= totalWidth)
                return Clone();

            SeqString result = new SeqString(totalWidth);
            int left = totalWidth - Length;
            for (int i = 0; i < left; i++)
                result.CStr[i] = paddingChar;
            for (int i = 0; i < Length; i++)
                result.CStr[i + left] = CStr[i];

            return result;
        }

        /// <summary>
        /// 获取串的长度
        /// </summary>
        public int Length
        {
            get
            {
                int i = 0;
                while (CStr[i] != '\0')
                    i++;
                return i;
            }
        }

        /// <summary>
        /// 获取或设置指定索引处的字符
        /// </summary>
        /// <param name="index">要获取或设置的字符从零开始的索引</param>
        /// <returns>指定索引处的字符</returns>
        public char this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                return CStr[index];
            }
            set
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                CStr[index] = value;
            }
        }

        /// <summary>
        /// 在指定位置插入子串
        /// </summary>
        /// <param name="startIndex">插入的位置</param>
        /// <param name="s">插入的子串</param>
        /// <returns>插入串后得到的新串</returns>
        public IString Insert(int startIndex, IString s)
        {
            if (s == null)
                throw new ArgumentNullException();

            if (startIndex < 0 || startIndex > Length)
                throw new ArgumentOutOfRangeException();

            SeqString str = new SeqString(s.Length + Length);
            for (int i = 0; i < startIndex; i++)
                str.CStr[i] = CStr[i]; //注意str[i]直接使用是错误的
            for (int i = 0, len = s.Length; i < len; i++)
                str.CStr[i + startIndex] = s[i];
            for (int i = startIndex; i < Length; i++)
                str.CStr[i + s.Length] = CStr[i];

            return str;
        }

        /// <summary>
        /// 在指定位置移除子串
        /// </summary>
        /// <param name="startIndex">移除的位置</param>
        /// <param name="count">移除的长度</param>
        /// <returns>移除后得到的新串</returns>
        public IString Remove(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex > Length - 1)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();

            int left = Length - startIndex; //最多移除字符个数
            count = (left < count) ? left : count; //实际移除字符个数
            SeqString str = new SeqString(Length - count);
            for (int i = 0; i < startIndex; i++)
                str.CStr[i] = CStr[i];
            for (int i = startIndex + count; i < Length; i++)
                str.CStr[i - count] = CStr[i];
            return str;
        }

        /// <summary>
        /// 在指定位置取子串
        /// </summary>
        /// <param name="startIndex">取子串的位置</param>
        /// <param name="count">子串的长度</param>
        /// <returns>取得的子串</returns>
        public IString SubString(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex > Length - 1)
                throw new ArgumentOutOfRangeException();
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            int left = Length - startIndex; //取子串最大长度
            count = (left < count) ? left : count; //子串实际长度
            SeqString str = new SeqString(count);
            for (int i = 0; i < count; i++)
                str.CStr[i] = CStr[i + startIndex];
            return str;
        }

        /// <summary>
        /// 当前串的拷贝
        /// </summary>
        /// <returns>当前串的拷贝</returns>
        public IString Clone()
        {
            SeqString str = new SeqString(Length);
            for (int i = 0; i < Length; i++)
                str.CStr[i] = CStr[i];
            return str;
        }

        /// <summary>
        /// 串连接
        /// </summary>
        /// <param name="s">在尾部要连接的串</param>
        /// <returns>连接后得到的新串</returns>
        public IString Concat(IString s)
        {
            if (s == null)
                throw new ArgumentNullException();
            return Insert(Length, s);
        }

        /// <summary>
        /// 串的匹配
        /// </summary>
        /// <param name="s">要匹配的子串</param>
        /// <returns>子串在主串中的位置,不存在返回-1.</returns>
        public int FindParam(IString s)
        {
            if (s == null || s.Length == 0)
                throw new Exception("匹配字符串为null或空.");
            for (int i = 0; i <= Length - s.Length; i++)
            {
                if (CStr[i] == s[0])
                {
                    int j;
                    for (j = 1; j < s.Length; j++)
                    {
                        if (CStr[j + i] != s[j])
                            break;
                    }
                    if (j == s.Length)
                        return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 串连接运算符的重载
        /// </summary>
        /// <param name="s1">第一个串</param>
        /// <param name="s2">第二个串</param>
        /// <returns>连接后得到的新串</returns>
        public static SeqString operator +(SeqString s1, SeqString s2)
        {
            if (s1 == null || s2 == null)
                throw new ArgumentNullException();
            return s1.Concat(s2) as SeqString;
        }

        /// <summary>
        /// SeqString类的输出字符串
        /// </summary>
        /// <returns>SeqString类的输出字符串</returns>
        public override string ToString()
        {
            string str = string.Empty;
            for (int i = 0; i < Length; i++)
                str += CStr[i];
            return str;
        }

        /// <summary>
        /// 串的匹配
        /// </summary>
        /// <param name="value">要匹配的子串</param>
        /// <returns>子串在主串中的位置,不存在返回-1.</returns>
        public int IndexOf(IString value)
        {
            if (value == null || value.Length == 0)
                throw new Exception("匹配字符串为null或空.");
            return IndexOf(value, 0);
        }

        /// <summary>
        /// 串的匹配
        /// </summary>
        /// <param name="value">要匹配的子串</param>
        /// <param name="startIndex">匹配的起始位置</param>
        /// <returns>子串在主串中的位置,不存在返回-1.</returns>
        public int IndexOf(IString value, int startIndex)
        {
            if (value == null || value.Length == 0)
                throw new Exception("匹配字符串为null或空.");
            if (startIndex < 0 || startIndex > value.Length - 1)
                throw new ArgumentOutOfRangeException();

            for (int i = startIndex; i <= Length - value.Length; i++)
            {
                if (CStr[i] == value[0])
                {
                    int j;
                    for (j = 1; j < value.Length; j++)
                    {
                        if (CStr[j + i] != value[j])
                            break;
                    }
                    if (j == value.Length)
                        return i;
                }
            }
            return -1;

        }

        /// <summary>
        /// 将此实例中的指定 IString 的所有匹配项替换为其他指定的 IString。
        /// </summary>
        /// <param name="oldValue">要替换的 IString。</param>
        /// <param name="newValue">要替换 oldValue 的所有匹配项的 IString。</param>
        /// <returns>等效于此实例，但将 oldValue 的所有实例都替换为 newValue 的 IString。</returns>
        /// <remarks>
        /// 异常:
        /// System.ArgumentException:oldValue 是空字符串 ("")。
        /// </remarks>
        public IString Replace(IString oldValue, IString newValue)
        {
            if (Length == 0)
                throw new ArgumentException("oldValue是空字符串。");

            string str = string.Empty;
            int i = 0;
            while (i < Length)
            {
                if (CStr[i] == oldValue[0])
                {
                    int j;
                    for (j = 1; j < oldValue.Length; j++)
                    {
                        if (CStr[i + j] != oldValue[j])
                        {
                            break;
                        }
                    }
                    if (j == oldValue.Length)
                    {
                        str += newValue;
                        i += oldValue.Length;
                        continue;
                    }
                }
                str += CStr[i];
                i++;
            }
            return new SeqString(str);
        }

        /// <summary>
        /// 移除所有前导空白字符和尾部空白字符。
        /// </summary>
        /// <returns>从当前对象的开始和末尾移除所有空白字符后保留的字符串。</returns>
        public IString Trim()
        {
            //返回移除所有前导空白字符和尾部空白字符后保留的字符串.
            int left;
            int right;
            for (left = 0; left < CStr.Length - 1; left++)
            {
                if (CStr[left] != ' ')
                    break;
            }
            if (left == CStr.Length - 1)
                return new SeqString();

            for (right = CStr.Length - 2; right >= 0; right--)
            {
                if (CStr[right] != ' ')
                    break;
            }
            return SubString(left, right - left + 1);
        }
    }
}
```





## 3. 练习参考答案
**1. 题目解答**

以下为`python`的实现

```
class Solution:
    """
    这道题主要用到思路是：滑动窗口
    什么是滑动窗口？
    其实就是一个队列,比如例题中的 abcabcbb，进入这个队列（窗口）为 abc 满足题目要求，
    当再进入 a，队列变成了 abca，这时候不满足要求。所以，我们要移动这个队列！
    如何移动？
    我们只要把队列的左边的元素移出就行了，直到满足题目要求！
    一直维持这样的队列，找出队列出现最长的长度时候，求出解！
    """
    def lengthOfLongestSubstring(self, s: str) -> int:
        if not s:
            return 0
        left = 0 # 窗口的左侧
        lookup = set()  # 窗口里的字符，初始为空，因为左右都为空
        n = len(s)  # 字符串总长度
        max_len = 0  # 无重复最长子串的长度
        cur_len = 0  # 当前窗口的长度
        for i in range(n):  # 窗口右侧移动
            cur_len += 1  # 移动一次，窗口长度加一
            # 条件判定开始
            while s[i] in lookup:  # s[i]表示本次移动时进入窗口的值
                # 如果右侧新加入的字符在窗口里面有相同的值，则将窗口左侧向右边移动，以下是该动作需要完成的几处更新
                lookup.remove(s[left])  # 移除窗口里的值
                left += 1  # 左侧下标加一
                cur_len -= 1  # 窗口长度减一
            # 条件判定结束
            if cur_len > max_len:  # 如果当前窗口长度大于记录的最大窗口长度，则更新该最大窗口长度
                max_len = cur_len
            lookup.add(s[i])  # 在条件判定完成之后，继续窗口右侧移动的更新操作
        return max_len


if __name__ == '__main__':
    solution = Solution()
    max_length = solution.lengthOfLongestSubstring("abcddsd")
    print(max_length)
```

**2. 题目解答**



以下为`python`的实现

```python
class Solution:
    def findSubstring(self, s, words):
        from collections import Counter
        if not s or not words:
            return []
        one_word = len(words[0])
        all_len = len(words) * one_word  # words里面所有字符组成的字符串长度
        n = len(s)
        words = Counter(words)
        res = []
        for i in range(0, n - all_len + 1):  # 这里是左侧窗口，左侧窗口有个限制
            tmp = s[i:i + all_len]  # 窗口所包含的字符串 i+all_len就是窗口右侧
            # 条件判定
            c_tmp = []
            for j in range(0, all_len, one_word): 
                c_tmp.append(tmp[j:j + one_word])
            if Counter(c_tmp) == words:
                # 判定通过则加一个下标
                res.append(i)
        return res


if __name__ == '__main__':
    solution = Solution()
    s = "barfoothefoobarman"
    words = ["foo", "bar"]
    out = solution.findSubstring(s, words)
    print(out)
```

**3. 替换子串得到平衡字符串**

以下为`python`的实现

```python
class Solution(object):
    def balancedString(self, s):
        """
        python版本
        :type s: str
        :rtype: int
        """
        cnt = collections.Counter(s)
        res = n = len(s)
        i, avg = 0, n//4
        for j, c in enumerate(s):
            cnt[c] -= 1
            while i < n and all(avg >= cnt[x] for x in 'QWER'):
                res = min(res, j - i + 1)
                cnt[s[i]] += 1
                i += 1
        return res
```
