



# Task02 顺序表和链表（2天）

## 1. 线性表的定义与操作

**1.1 线性表的定义**

线性表（Linear List）是由`n（n >= 0）`个相同类型的数据元素`a1,a2,...,an` 组成的有序序列。即表中除首尾元素外，其它元素有且仅有一个直接前驱和直接后继。首元素仅有一个直接后继，尾元素仅有一个直接前驱。表中数据元素的个数称为表的长度，记为：`(a1,a2,...,an)`。


**1.2 线性表的操作**
- 随机存取：获取或设置指定索引处的数据元素值。（支持索引器）
- 插入操作：将数据元素值插入到指定索引处。
- 移除操作：移除线性表指定索引处的数据元素。
- 查找操作：寻找具有特征值域的结点并返回其下标。
- 得到表长：获取线性表中实际包含数据元素的个数。
- 是否为空：判断线性表中是否包含数据元素。
- 清空操作：移除线性表中的所有数据元素。

![线性表接口](https://img-blog.csdnimg.cn/20191219081504351.png)

以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 线性表的抽象数据类型
    /// </summary>
    /// <typeparam name="T">线性表中元素的类型</typeparam>
    public interface ILinearList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 获取线性表中实际包含元素的个数
        /// </summary>
        int Length { get; }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">要获取或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// 判断线性表中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        bool IsEmpty();

        /// <summary>
        /// 将元素插入到指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引,应在该位置插入data.</param>
        /// <param name="data">要插入的元素</param>
        void Insert(int index, T data);

        /// <summary>
        /// 移除线性表指定索引处的元素
        /// </summary>
        /// <param name="index">要移除元素从0开始的索引</param>
        void Remove(int index);

        /// <summary>
        /// 在线性表中寻找元素data.
        /// </summary>
        /// <param name="data">要寻找的元素</param>
        /// <returns>如果存在返回该元素在线性表中的位置,否则返回-1.</returns>
        int Search(T data);

        /// <summary>
        /// 从线性表中移除所有元素
        /// </summary>
        void Clear();
    }
}
```

---
## 2. 线性表的存储与实现

**2.1 顺序存储（顺序表）**

定义：利用顺序存储结构（即利用数组）实现的线性表。

特点：逻辑结构与存储结构相同；具有随机存取的特点。

![顺序表存储示意图](https://img-blog.csdnimg.cn/20191219081751681.png)

实现：

![利用顺序存储结构实现线性表](https://img-blog.csdnimg.cn/20191219082422397.png)

以下代码为C#版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用顺序存储结构实现的线性表
    /// </summary>
    /// <typeparam name="T">顺序表中元素的类型</typeparam>
    public class SeqList<T> : ILinearList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        protected readonly T[] Dataset;

        /// <summary>
        /// 获取SeqList中实际包含元素的个数
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 获取SeqList中最多包含元素的个数
        /// </summary>
        public int MaxSize { get; }

        /// <summary>
        /// 初始化SeqList类的新实例
        /// </summary>
        /// <param name="max">SeqList中最多包含元素的个数</param>
        public SeqList(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException();
            MaxSize = max;
            Dataset = new T[MaxSize];
            Length = 0;
        }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                return Dataset[index];
            }
            set
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                Dataset[index] = value;
            }
        }

        /// <summary>
        /// 判断SeqList中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return Length == 0;

        }

        /// <summary>
        /// 将元素插入到指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引,应在该位置插入data.</param>
        /// <param name="data">要插入的元素</param>
        public void Insert(int index, T data)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();
            if (Length == MaxSize)
                throw new Exception("达到最大值");

            for (int i = Length; i > index; i--)
            {
                Dataset[i] = Dataset[i - 1];
            }
            Dataset[index] = data;
            Length++;
        }

        /// <summary>
        /// 移除SeqList指定索引处的元素
        /// </summary>
        /// <param name="index">要移除元素从0开始的索引</param>
        public void Remove(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            for (int i = index; i < Length - 1; i++)
            {
                Dataset[i] = Dataset[i + 1];
            }
            Length--;
        }

        /// <summary>
        /// 在SeqList中寻找元素data.
        /// </summary>
        /// <param name="data">要寻找的元素</param>
        /// <returns>如果存在返回该元素在线性表中的位置,否则返回-1.</returns>
        public int Search(T data)
        {
            int i;
            for (i = 0; i < Length; i++)
            {
                if (Dataset[i].CompareTo(data) == 0)
                    break;
            }
            return i == Length ? -1 : i;
        }

        /// <summary>
        /// 从SeqList中移除所有元素
        /// </summary>
        public void Clear()
        {
            Length = 0;
        }

        /// <summary>
        /// 反转
        /// </summary>
        public void Reverse()
        {
            for (int i = 0; i < Length/2; i++)
            {
                T temp = Dataset[i];
                Dataset[i] = Dataset[Length - 1 - i];
                Dataset[Length - 1 - i] = temp;
            }
        }
    }
}
```

**2.2 链式存储（链表）**

利用指针方式实现的线性表称为链表（单链表、循环链表、双链表）。它不要求逻辑上相邻的数据元素在物理位置上也相邻，即：逻辑结构与物理结构可以相同也可以不相同。

**2.2.1 单链表**

定义：每个结点只含有一个链域（指针域）的链表。即：利用单链域的方式存储线性表的逻辑结构。

结构：

![单链表存储结构](https://img-blog.csdnimg.cn/201912190831277.png)

实现：

对结点的封装：

![对单链表结点的封装](https://img-blog.csdnimg.cn/20191219083410202.png)

以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 单链表结点
    /// </summary>
    /// <typeparam name="T">结点中数据元素的类型</typeparam>
    public class SNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 获取或设置该结点的数据元素
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 获取或设置该结点的后继结点
        /// </summary>
        public SNode<T> Next { get; set; }

        /// <summary>
        /// 初始化SNode类的新实例
        /// </summary>
        /// <param name="data">该结点的数据元素</param>
        /// <param name="next">该结点的后继结点</param>
        public SNode(T data, SNode<T> next = null)
        {
            Data = data;
            Next = next;
        }
    }
}
```


对单链表的封装：

![对单链表的封装](https://img-blog.csdnimg.cn/20191219084222597.png)

以下代码为`C#`版本：

```c
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinearStruct
{
    /// <summary>
    /// 用链式存储结构实现的线性表--单链表
    /// </summary>
    /// <typeparam name="T">单链表中元素的类型</typeparam>
    public class SLinkList<T> : ILinearList<T>, IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// 存储头结点
        /// </summary>
        public SNode<T> PHead { get; protected set; }

        /// <summary>
        /// 获取SLinkList中实际包含元素的个数
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 初始化SLinkList类的新实例
        /// </summary>
        public SLinkList()
        {
            Length = 0;
            PHead = null;
        }

        /// <summary>
        /// 将元素插入到单链表的首部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtFirst(T data)
        {
            PHead = new SNode<T>(data, PHead);
            Length++;
        }

        /// <summary>
        /// 获得指定索引处的结点
        /// </summary>
        /// <param name="index">元素从零开始的索引</param>
        /// <returns>指定索引处的结点</returns>
        private SNode<T> Locate(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            SNode<T> temp = PHead;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            return temp;
        }

        /// <summary>
        /// 将元素插入到单链表的尾部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtRear(T data)
        {
            if (PHead == null)
            {
                PHead = new SNode<T>(data);
            }
            else
            {
                Locate(Length - 1).Next = new SNode<T>(data);
            }
            Length++;
        }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                return Locate(index).Data;
            }
            set
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                Locate(index).Data = value;
            }
        }

        /// <summary>
        /// 判断SLinkList中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return Length == 0;
        }

        /// <summary>
        /// 将元素插入到指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引,应在该位置插入data.</param>
        /// <param name="data">要插入的元素</param>
        public void Insert(int index, T data)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                InsertAtFirst(data);
            }
            else if (index == Length)
            {
                InsertAtRear(data);
            }
            else
            {
                Locate(index - 1).Next = new SNode<T>(data, Locate(index));
                Length++;
            }
        }

        /// <summary>
        /// 移除SLinkList指定索引处的元素
        /// </summary>
        /// <param name="index">要移除元素从0开始的索引</param>
        public void Remove(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                PHead = PHead.Next;
            }
            else
            {
                Locate(index - 1).Next = Locate(index).Next;
            }
            Length--;
        }

        /// <summary>
        /// 在SLinkList中寻找元素data.
        /// </summary>
        /// <param name="data">要寻找的元素</param>
        /// <returns>如果存在返回该元素在线性表中的位置,否则返回-1.</returns>
        public int Search(T data)
        {
            int i;
            SNode<T> temp = PHead;
            for (i = 0; i < Length; i++)
            {
                if (temp.Data.CompareTo(data) == 0)
                    break;
                temp = temp.Next;
            }
            return i == Length ? -1 : i;
        }

        /// <summary>
        /// 从SLinkList中移除所有元素
        /// </summary>
        public void Clear()
        {
            PHead = null;
            Length = 0;
        }

        /// <summary>
        /// 链表的反转
        /// </summary>
        public void Reverse()
        {
            if (Length == 0 || Length == 1)
                return;
            SNode<T> currentNode = PHead;
            SNode<T> newNode = null;
            while (currentNode != null)
            {
                SNode<T> tempNode = currentNode.Next;
                currentNode.Next = newNode;
                newNode = currentNode;
                currentNode = tempNode;
            }
            PHead = newNode;
        }

        /// <summary>
        /// 得到枚举数，用于支持foreach循环
        /// </summary>
        /// <returns>枚举数</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator<T> GetEnumerator()
        {
            SNode<T> current = PHead;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
```



**2.2.2 循环链表**

定义：是一种首尾相连的单链表。即：在单链表中，将尾结点的指针域null改为指向pHead，就得到单链形式的循环链表。

表现形式：

![利用头指针表示循环链表](https://img-blog.csdnimg.cn/20191219084644144.png)

通常情况下，使用尾指针表示循环链表。

![利用尾指针表示循环链表](https://img-blog.csdnimg.cn/20191219084747468.png)

实现：

![对循环链表的封装](https://img-blog.csdnimg.cn/20191219084946540.png)

以下代码为`C#`版本：
```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用链式存储结构实现的线性表--循环链表
    /// </summary>
    /// <typeparam name="T">循环链表中元素的类型</typeparam>
    public class CLinkList<T> : ILinearList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public SNode<T> PRear { get; protected set; }


        /// <summary>
        /// 获取CLinkList中实际包含元素的个数
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                return Locate(index).Data;
            }
            set
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                Locate(index).Data = value;
            }
        }

        /// <summary>
        /// 初始化CLinkList类的新实例
        /// </summary>
        public CLinkList()
        {
            Length = 0;
            PRear = null;
        }

        /// <summary>
        /// 判断CLinkList中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return Length == 0;
        }

        /// <summary>
        /// 将元素插入到循环链表的尾部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtRear(T data)
        {
            if (IsEmpty())
            {
                PRear = new SNode<T>(data);
                PRear.Next = PRear;
            }
            else
            {
                SNode<T> temp = new SNode<T>(data, PRear.Next);
                PRear.Next = temp;
                PRear = temp;
            }
            Length++;
        }

        /// <summary>
        /// 将元素插入到循环链表的首部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtFirst(T data)
        {
            if (IsEmpty())
            {
                PRear = new SNode<T>(data);
                PRear.Next = PRear;
            }
            else
            {
                SNode<T> temp = new SNode<T>(data, PRear.Next);
                PRear.Next = temp;
            }
            Length++;
        }

        /// <summary>
        /// 获得指定索引处的结点
        /// </summary>
        /// <param name="index">元素从零开始的索引</param>
        /// <returns>指定索引处的结点</returns>
        private SNode<T> Locate(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            SNode<T> temp = PRear.Next;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            return temp;
        }

        /// <summary>
        /// 将元素插入到指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引,应在该位置插入data.</param>
        /// <param name="data">要插入的元素</param>
        public void Insert(int index, T data)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                InsertAtFirst(data);
            }
            else if (index == Length)
            {
                InsertAtRear(data);
            }
            else
            {
                SNode<T> temp = Locate(index - 1);
                temp.Next = new SNode<T>(data, temp.Next);
                Length++;
            }
        }

        /// <summary>
        /// 移除CLinkList指定索引处的元素
        /// </summary>
        /// <param name="index">要移除元素从0开始的索引</param>
        public void Remove(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            if (PRear == PRear.Next)
            {
                PRear = null;
            }
            else
            {
                if (index == Length - 1)
                {
                    SNode<T> temp = Locate(Length - 2);
                    temp.Next = PRear.Next;
                    PRear = temp;
                }
                else if (index == 0)
                {
                    PRear.Next = PRear.Next.Next;
                }
                else
                {
                    SNode<T> temp = Locate(index - 1);
                    temp.Next = temp.Next.Next;
                }
            }
            Length--;
        }

        /// <summary>
        /// 在CLinkList中寻找元素data.
        /// </summary>
        /// <param name="data">要寻找的元素</param>
        /// <returns>如果存在返回该元素在线性表中的位置,否则返回-1.</returns>
        public int Search(T data)
        {
            int i;
            SNode<T> temp = PRear;

            for (i = 0; i < Length; i++)
            {
                if (temp.Next.Data.CompareTo(data) == 0)
                    break;
                temp = temp.Next;
            }
            return (i == Length) ? -1 : i;

        }

        /// <summary>
        /// 从CLinkList中移除所有元素
        /// </summary>
        public void Clear()
        {
            Length = 0;
            PRear = null;
        }
    }
}
```


**2.2.3 双链表**

定义：每个结点含有两个链域（指针域）的链表。即：利用双链域的方式存储线性表的逻辑结构。

结构：

![双链表存储结构](https://img-blog.csdnimg.cn/20191219085239419.png)

实现：

对结点的封装：

![对双链表结点的封装](https://img-blog.csdnimg.cn/20191219085534618.png)

以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 双链表结点
    /// </summary>
    /// <typeparam name="T">结点中数据元素的类型</typeparam>
    public class DNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// 获取或设置该结点的前趋结点
        /// </summary>
        public DNode<T> Prior { get; set; }

        /// <summary>
        /// 获取或设置该结点的后继结点
        /// </summary>
        public DNode<T> Next { get; set; }

        /// <summary>
        /// 获取或设置该结点的数据元素
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 初始化DNode类的新实例
        /// </summary>
        /// <param name="data">该结点的数据元素</param>
        /// <param name="prior">该结点的前趋结点</param>
        /// <param name="next">该结点的后继结点</param>
        public DNode(T data, DNode<T> prior = null, DNode<T> next = null)
        {
            Prior = prior;
            Data = data;
            Next = next;
        }
    }
}
```

对双链表的封装：

![对双向链表的封装](https://img-blog.csdnimg.cn/2019121909023162.png)

以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用链式存储结构实现的线性表--双链表
    /// </summary>
    /// <typeparam name="T">结点中数据元素的类型</typeparam>
    public class DLinkList<T> : ILinearList<T> where T : IComparable<T>
    {
        /// <summary>
        /// 存储头结点
        /// </summary>
        public DNode<T> PHead { get; protected set; }

        /// <summary>
        /// 存储尾结点
        /// </summary>
        public DNode<T> PRear { get; protected set; }

        /// <summary>
        /// 获取DLinkList中实际包含元素的个数
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 初始化DLinkList类的新实例
        /// </summary>
        public DLinkList()
        {
            PHead = null;
            PRear = null;
            Length = 0;
        }

        /// <summary>
        /// 将元素插入到双链表的首部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtFirst(T data)
        {
            if (IsEmpty())
            {
                DNode<T> temp = new DNode<T>(data);
                PHead = temp;
                PRear = temp;
            }
            else
            {
                DNode<T> temp = new DNode<T>(data, null, PHead);
                PHead.Prior = temp;
                PHead = temp;
            }
            Length++;
        }

        /// <summary>
        /// 将元素插入到双链表的尾部
        /// </summary>
        /// <param name="data">要插入的元素</param>
        public void InsertAtRear(T data)
        {
            if (IsEmpty())
            {
                DNode<T> temp = new DNode<T>(data);
                PHead = temp;
                PRear = temp;
            }
            else
            {
                DNode<T> temp = new DNode<T>(data, PRear, null);
                PRear.Next = temp;
                PRear = temp;
            }
            Length++;
        }

        /// <summary>
        /// 获得指定索引处的结点
        /// </summary>
        /// <param name="index">元素从零开始的索引</param>
        /// <returns>指定索引处的结点</returns>
        private DNode<T> Locate(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            DNode<T> temp = PHead;
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            return temp;
        }

        /// <summary>
        /// 将元素插入到指定索引处
        /// </summary>
        /// <param name="index">从零开始的索引,应在该位置插入data.</param>
        /// <param name="data">要插入的元素</param>
        public void Insert(int index, T data)
        {
            if (index < 0 || index > Length)
                throw new IndexOutOfRangeException();

            if (index == 0)
            {
                InsertAtFirst(data);
            }
            else if (index == Length)
            {
                InsertAtRear(data);
            }
            else
            {
                DNode<T> temp1 = Locate(index);
                DNode<T> temp2 = new DNode<T>(data, temp1.Prior, temp1);
                temp2.Prior.Next = temp2;
                temp2.Next.Prior = temp2;
                Length++;
            }
        }

        /// <summary>
        /// 移除DLinkList指定索引处的元素
        /// </summary>
        /// <param name="index">要移除元素从0开始的索引</param>
        public void Remove(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();

            if (Length == 1)
            {
                PHead = null;
                PRear = null;
            }
            else
            {
                if (index == 0)
                {
                    PHead = PHead.Next;
                    PHead.Prior = null;
                }
                else if (index == Length - 1)
                {
                    PRear = PRear.Prior;
                    PRear.Next = null;
                }
                else
                {
                    DNode<T> temp = Locate(index);
                    temp.Prior.Next = temp.Next;
                    temp.Next.Prior = temp.Prior;
                }
            }
            Length--;
        }

        /// <summary>
        /// 判断DLinkList中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return Length == 0;
        }

        /// <summary>
        ///  从DLinkList中移除所有元素
        /// </summary>
        public void Clear()
        {
            Length = 0;
            PHead = null;
            PRear = null;
        }

        /// <summary>
        /// 在DLinkList中寻找元素data.
        /// </summary>
        /// <param name="data">要寻找的元素</param>
        /// <returns>如果存在返回该元素在线性表中的位置,否则返回-1.</returns>
        public int Search(T data)
        {
            int i;
            DNode<T> temp = PHead;
            for (i = 0; i < Length; i++)
            {
                if (temp.Data.CompareTo(data) == 0)
                    break;

                temp = temp.Next;
            }
            return i == Length ? -1 : i;
        }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                return Locate(index).Data;
            }
            set
            {
                if (index < 0 || index > Length - 1)
                    throw new IndexOutOfRangeException();
                Locate(index).Data = value;
            }
        }
    }
}
```




---
## 3. 练习参考答案

**1. 合并两个有序链表**

以下代码为`Java`版本：

```java
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     int val;
 *     ListNode next;
 *     ListNode(int x) { val = x; }
 * }
 */
 
/**
 * @param l1 第一个链表
 * @param l2 第二个链表
 * @return 合并后的链表头节点
 */
public ListNode mergeTwoLists(ListNode l1, ListNode l2) {
        if(l1 == null){
            return l2;
        }
        if(l2 == null){
            return l1;
        }
        ListNode head = new ListNode(0);
        head.next = null;
        ListNode temp, q;
        q= head;
        while(l1 != null && l2 != null){
            if(l1.val < l2.val){
                temp = l1;
                l1 = l1.next;
            }else{
                temp = l2;
                l2 = l2.next;
            }
            q.next = temp;
            q = temp;
        }
        ListNode node = l1 == null ? l2 : l1;
        q.next = node;
        return head.next;
    }


class ListNode{
        int val;
        ListNode next;
        ListNode(int x){
            val = x;
        }
}
```

以下代码为`C#`版本：

```c
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode pHead = new ListNode(int.MaxValue);
        ListNode temp = pHead;

        while (l1 != null && l2 != null)
        {
            if (l1.val < l2.val)
            {
                temp.next = l1;
                l1 = l1.next;
            }
            else
            {
                temp.next = l2;
                l2 = l2.next;
            }
            temp = temp.next;
        }

        if (l1 != null)
            temp.next = l1;

        if (l2 != null)
            temp.next = l2;

        return pHead.next;
    }
}
```


**2. 删除链表的倒数第N个节点**

 以下代码为`Java`版本：
 
```java
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     int val;
 *     ListNode next;
 *     ListNode(int x) { val = x; }
 * }
 */
 
/**
 * @param head 待操作链表头节点
 * @param n 要删除倒数第n个节点
 * @return 删除制定节点后的链表头节点
 */
public ListNode removeNthFromEnd(ListNode head, int n) {
        if(head.next == null){
            return null;
        }
        ListNode p, q;
        q = head;
        int i = 1;
        while((i < n+1) && q != null){
            q = q.next;
            i++;
        }
        if(q == null){
            head = head.next;
            return head;
        }
        p = head;
        while(q.next != null){
            q = q.next;
            p = p.next;
        }
        p.next = p.next.next;
        return head;
    }

class ListNode {
      int val;
      ListNode next;
      ListNode(int x) { val = x; }
}
```

以下代码为`C#`版本：

```c
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
 
public class Solution
{
    public ListNode RemoveNthFormEnd(ListNode head, int n)
    {
        int len = GetLength(head);
        int index = len - n;

        if (index == 0)
        {
            head = head.next;
            return head;
        }
        ListNode temp = head;
        for (int i = 0; i < index - 1; i++)
        {
            temp = temp.next;
        }
        temp.next = temp.next.next;
        return head;
    }

    public int GetLength(ListNode head)
    {
        ListNode temp = head;
        int i = 0;
        while (temp != null)
        {
            i++;
            temp = temp.next;
        }
        return i;
    }
}
```

以下代码为`C#`版本：
```c
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode RemoveNthFormEnd(ListNode head, int n)
    {
        ListNode temp1 = head;
        ListNode temp2 = head;
        int len = 0;
        int index = 0;
        while (temp1 != null)
        {
            temp1 = temp1.next;
            len++;
            if (index == n)
            {
                break;
            }
            index++;
        }

        if (len == n)
        {
            head = head.next;
            return head;
        }
            
        while (temp1 != null)
        {
            temp1 = temp1.next;
            temp2 = temp2.next;
        }
            
        temp2.next = temp2.next.next;
        return head;
    }
}
```



**3. 旋转链表**

以下代码为`C#`版本：

```c
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
 
public class Solution
{
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null || k == 0)
            return head;

        int len = GetLength(head);
        int index = len - k%len;

        if (index == len)
            return head;

        ListNode temp1 = head;
        ListNode temp2 = head;
        for (int i = 0; i < index - 1; i++)
        {
            temp1 = temp1.next;
        }
        head = temp1.next;
        temp1.next = null;

        temp1 = head;
        while (temp1.next != null)
        {
            temp1 = temp1.next;
        }
        temp1.next = temp2;
        return head;
    }

    public int GetLength(ListNode head)
    {
        ListNode temp = head;
        int i = 0;
        while (temp != null)
        {
            i++;
            temp = temp.next;
        }
        return i;
    }
} 
```
