
# Task03：栈与递归（2天）

栈是我们经常使用的一种数据结构，如下图所示，手枪发射子弹的顺序与子弹压入弹夹的顺序是相反，即后压入弹夹的子弹先发射出来。

![](https://img-blog.csdnimg.cn/20191222213300699.png)

比如我们使用的Word、Excel、Photoshop等软件系统中的撤销操作，也是栈的具体应用，最后做的操作，一定是最先撤销的。下面我们就来详细介绍“栈”这种数据结构。


## 1. 栈的定义与操作

**1.1 栈的定义**

插入（入栈）和删除（出栈）操作只能在一端（栈顶）进行的线性表。即先进后出（First In Last Out）的线性表。

例1 ：线性表`(a0,a1,...,an)` 进栈与出栈演示。

![顺序表模拟入栈、出栈](https://img-blog.csdnimg.cn/20191222213645860.png)

![单链表模拟入栈、出栈](https://img-blog.csdnimg.cn/20191222213749217.png)

如上所示，栈有两种实现一种是顺序栈一种是链栈，这两种实现方式有什么区别呢，其实与顺序表和链表是一样的：

- 顺序栈是静态分配的但是链栈是动态分配的，所以比较起来链栈对于空间的利用率更高。因为顺序栈可能申请了较大的空间但是并没有全部都存储元素。
- 顺序栈虽然不用存储指针相比较链栈来说较为节省内存空间，但是链栈却可以将零碎的内存空间利用起来。
- 而且对于存储量未知的情况下，链栈更加适合，因为链栈通常不会出现栈满的情况。
- 对于顺序表和链表来说，链表对于插入和删除效率更高，顺序表对于查找效率更高。但是对于栈来说只能在栈顶进行操作，所以无法体现链表的效率更高。

**1.2 栈的操作**

- 入栈操作：将数据元素值插入栈顶。
- 出栈操作：移除栈顶的数据元素。
- 是否为空：判断栈中是否包含数据元素。
- 得到栈深：获取栈中实际包含数据元素的个数。
- 清空操作：移除栈中的所有数据元素。
- 获取栈顶元素。

![栈接口](https://img-blog.csdnimg.cn/20191222214544845.png)

以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 栈的抽象数据类型
    /// </summary>
    /// <typeparam name="T">栈中元素的类型</typeparam>
    public interface IStack<T> where T : IComparable<T>
    {
        /// <summary>
        /// 获取栈中实际包含元素的个数
        /// </summary>
        int Length { get; }

        /// <summary>
        /// 获取栈顶元素
        /// </summary>
        T StackTop { get; }

        /// <summary>
        /// 数据元素入栈
        /// </summary>
        /// <param name="data">要入栈的元素</param>
        void Push(T data);

        /// <summary>
        /// 数据元素出栈
        /// </summary>
        void Pop();

        /// <summary>
        /// 判断栈中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        bool IsEmpty();

        /// <summary>
        /// 从栈中移除所有元素
        /// </summary>
        void Clear();
    }
}
```






## 2. 栈的存储与实现

**2.1 顺序存储（顺序栈）**

顺序栈：利用顺序表实现的栈。

实现：

![顺序栈](https://img-blog.csdnimg.cn/20191222214937292.png)

以下代码为`C#`版本：
```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用顺序存储结构实现的栈
    /// </summary>
    /// <typeparam name="T">顺序栈中元素的类型</typeparam>
    public class SeqStack<T> : IStack<T> where T : IComparable<T>
    {
        private readonly SeqList<T> _lst;

        /// <summary>
        /// 初始化SeqStack类的新实例
        /// </summary>
        /// <param name="max">SeqStack中最多包含元素的个数</param>
        public SeqStack(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException();
            _lst = new SeqList<T>(max);
        }

        /// <summary>
        /// 获取SeqStack中实际包含元素的个数
        /// </summary>
        public int Length
        {
            get { return _lst.Length; }
        }

        /// <summary>
        /// 获取SeqStack中最多包含元素的个数
        /// </summary>
        public int MaxSize
        {
            get { return _lst.MaxSize; }
        }

        /// <summary>
        /// 获取SeqStack中的栈顶元素
        /// </summary>
        public T StackTop
        {
            get
            {
                if (_lst.IsEmpty())
                    throw new Exception("栈为空.");
                return _lst[0];
            }
        }

        /// <summary>
        /// 数据元素入栈
        /// </summary>
        /// <param name="data">要入栈的元素</param>
        public void Push(T data)
        {
            if (_lst.Length == _lst.MaxSize)
                throw new Exception("栈已达到最大容量.");
            _lst.Insert(0, data);
        }

        /// <summary>
        /// 数据元素出栈
        /// </summary>
        public void Pop()
        {
            if (_lst.IsEmpty())
                throw new Exception("栈为空.");
            _lst.Remove(0);
        }

        /// <summary>
        /// 判断SeqStack中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return _lst.IsEmpty();
        }

        /// <summary>
        /// 从SeqStack中移除所有元素
        /// </summary>
        public void Clear()
        {
            _lst.Clear();
        }
    }
}
```


**2.2 链式存储（链栈）**

链栈：利用单链表实现的栈。

实现：

![链栈](https://img-blog.csdnimg.cn/20191222215336715.png)

以下代码为`C#`版本：
```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用链式存储结构实现的栈
    /// </summary>
    /// <typeparam name="T">栈中元素的类型</typeparam>
    public class LinkStack<T> : IStack<T> where T : IComparable<T>
    {
        private readonly SLinkList<T> _lst;

        /// <summary>
        /// 初始化LinkStack类的新实例
        /// </summary>
        public LinkStack()
        {
            _lst = new SLinkList<T>();
        }

        /// <summary>
        /// 获取LinkStack中实际包含元素的个数
        /// </summary>
        public int Length
        {
            get { return _lst.Length; }
        }

        /// <summary>
        /// 获取LinkStack中的栈顶元素
        /// </summary>
        public T StackTop
        {
            get
            {
                if (_lst.Length == 0)
                    throw new Exception("栈为空.");
                return _lst[0];
            }
        }

        /// <summary>
        /// 数据元素入栈
        /// </summary>
        /// <param name="data">要入栈的元素</param>
        public void Push(T data)
        {
            _lst.InsertAtFirst(data);
        }

        /// <summary>
        /// 数据元素出栈
        /// </summary>
        public void Pop()
        {
            if (_lst.Length == 0)
                throw new Exception("栈为空.");
            _lst.Remove(0);
        }

        /// <summary>
        /// 判断LinkStack中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return _lst.IsEmpty();
        }

        /// <summary>
        /// 从LinkStack中移除所有元素
        /// </summary>
        public void Clear()
        {
            _lst.Clear();
        }
    }
}
```

## 3. 递归

如果一个函数在内部调用自身本身，这个函数就是递归函数。

Sample01：求n的阶乘

`n! = 1 x 2 x 3 x ... x n`

循环：

以下代码为`Python`版本：

```python
n = 5
for k in range(1, 5):
    n = n * k
print(n)  # 120
```


递归：

以下代码为`Python`版本：

```python
def factorial(n):
    if n == 1:
        return 1
    return n * fact(n - 1)


print(factorial(5)) # 120
```

Samp02：斐波那契数列 

`f(n)=f(n-1)+f(n-2), f(0)=0 f(1)=1`

循环：

以下代码为`Python`版本：

```python
i = 0
j = 1
lst = list([i, j])
for k in range(2, 11):
    k = i + j
    lst.append(k)
    i = j
    j = k
print(lst)  
# [0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55]
```

递归：

以下代码为`Python`版本：

```python
def recur_fibo(n):
    if n <= 1:
        return n
    return recur_fibo(n - 1) + recur_fibo(n - 2)


lst = list()
for k in range(11):
    lst.append(recur_fibo(k))
print(lst)  
# [0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55]
```


注意：设置递归的层数，Python默认递归层数为 100

```python
import sys

sys.setrecursionlimit(1000)
```

Sample03：汉诺塔问题

汉诺塔问题源于印度一个古老传说的益智玩具。大梵天创造世界的时候做了三根金刚石柱子，在一根柱子上从下往上按照大小顺序摞着 64 片黄金圆盘。大梵天命令婆罗门把圆盘从下面开始按大小顺序重新摆放在另一根柱子上。并且规定，在小圆盘上不能放大圆盘，在三根柱子之间一次只能移动一个圆盘。

![](https://img-blog.csdnimg.cn/20181218104026718.png)

如果我们要思考每一步怎么移可能会非常复杂，但是可以将问题简化。

我们可以先假设除 a 柱最下面的盘子之外，已经成功地将 a 柱上面的 63个盘子移到了 b 柱，这时我们只要再将最下面的盘子由 a 柱移动到 c 柱即可。

![](https://img-blog.csdnimg.cn/20181218104208856.png)

当我们将最大的盘子由 a 柱移到 c 柱后，b 柱上便是余下的 63 个盘子，a 柱为空。因此现在的目标就变成了将这 63 个盘子由 b 柱移到 c 柱。这个问题和原来的问题完全一样，只是由 a 柱换为了 b 柱，规模由 64 变为了 63。因此可以采用相同的方法，先将上面的 62 个盘子由 b 柱移到 a 柱，再将最下面的盘子移到 c 柱。

以此内推，再以 b 柱为缓冲，将 a 柱上面的 62 个圆盘最上面的 61 个圆盘移动到 b 柱，并将最后一块圆盘移到 c 柱。

我们已经发现规律，我们每次都是以 a 或 b 中一根柱子为缓冲，然后先将除了最下面的圆盘之外的其它圆盘移动到辅助柱子上，再将最底下的圆盘移到 c 柱子上，不断重复此过程。

这个反复移动圆盘的过程就是递归，例如我们每次想解决 n 个圆盘的移动问题，就要先解决（n-1）个盘子进行同样操作的问题。

于是可以编写一个函数，move(n, a, b, c)。可以这样理解：move(盘子数量, 起点, 缓冲, 终点)。

<u>1. a 上只有一个盘子的情况，直接搬到 c，代码如下</u>:

```python
if n == 1:
    print(a, '-->', c)
```
<u>2. a 上不止有一个盘子的情况</u>:

首先，需要把 n-1 个盘子搬到 b 柱子缓冲。打印出的效果是：a --> b。

```python
move(n - 1, a, c, b)
```

再把最大的盘子搬到 c 柱子，也是最大尺寸的一个。打印出：a-->c。

```python
move(1, a, b, c)
```

最后，把剩下 b 柱的 n-1 个盘子搬到 c 上，此时缓冲变成了起点，起点变成了缓冲。

```python
move(n - 1, b, a, c)
```


利用 Python 实现汉诺塔问题

```python
i = 0


def move(n, a, b, c):
    global i
    if (n == 1):
        i += 1
        print('移动第 {0} 次 {1} --> {2}'.format(i, a, c))
        return
    move(n - 1, a, c, b)
    move(1, a, b, c)
    move(n - 1, b, a, c)


move(3, "a", "b", "c")  

# 移动第 1 次 a --> c
# 移动第 2 次 a --> b
# 移动第 3 次 c --> b
# 移动第 4 次 a --> c
# 移动第 5 次 b --> a
# 移动第 6 次 b --> c
# 移动第 7 次 a --> c
```

利用 C# 实现汉诺塔问题

```c
class Program
{
    private static int i = 0;
    static void Move(int n, string a, string b, string c)
    {
        if (n == 1)
        {
            Console.WriteLine("移动第 {0} 次 {1}-->{2}", ++i, a, c);
            return;
        }
        Move(n - 1, a, c, b);
        Move(1, a, b, c);
        Move(n - 1, b, a, c);
    }

    static void Main(string[] args)
    {
        Move(3, "a", "b", "c");
    }
}

// 移动第 1 次 a --> c
// 移动第 2 次 a --> b
// 移动第 3 次 c --> b
// 移动第 4 次 a --> c
// 移动第 5 次 b --> a
// 移动第 6 次 b --> c
// 移动第 7 次 a --> c
```




## 4. 练习参考答案

车辆重排的程序代码，如下（`C#`版本）：

```c
using System;
using LinearStruct;

namespace TrainArrange
{
    class Program
    {
        /// <summary>
        /// 车厢重排算法
        /// </summary>
        /// <param name="p">入轨序列</param>
        /// <param name="k">缓冲轨条数</param>
        /// <returns>重排是否成功</returns>
        static bool RailRoad(int[] p, int k)
        {
            LinkStack<int>[] h = new LinkStack<int>[k];
            for (int i = 0; i < h.Length; i++)
                h[i] = new LinkStack<int>();


            int nowOut = 1; //下一次要输出的车厢号
            int minH = int.MaxValue; //缓冲铁轨中编号最小的车厢
            int minS = -1; //minH号车厢对应的缓冲铁轨

            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == nowOut)
                {
                    Console.WriteLine("移动车厢：{0}从入轨到出轨。", p[i]);
                    nowOut++;
                    //从缓冲铁轨中输出
                    while (minH == nowOut)
                    {
                        Output(ref minH, ref minS, h); //出轨
                        nowOut++;
                    }
                }
                else
                {
                    //将p[i]送入某个缓冲铁轨
                    if (Input(p[i], ref minH, ref minS, h) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 从缓冲轨移除车厢出轨
        /// </summary>
        /// <param name="minH">缓冲铁轨中编号最小的车厢</param>
        /// <param name="minS">minH号车厢对应的缓冲铁轨</param>
        /// <param name="h">缓冲轨道的集合</param>
        static void Output(ref int minH, ref int minS, LinkStack<int>[] h)
        {
            h[minS].Pop(); //从堆栈minS中删除编号最小的车厢minH
            Console.WriteLine("移动车厢：{0}从缓冲轨{1}到出轨。", minH, minS);

            //通过检查所有的栈顶，搜索新的minH和minS
            minH = int.MaxValue;
            minS = -1;
            for (int i = 0; i < h.Length; i++)
            {
                if (h[i].IsEmpty() == false && h[i].StackTop < minH)
                {
                    minH = h[i].StackTop;
                    minS = i;
                }
            }
        }

        /// <summary>
        /// 在一个缓冲铁轨中放入车厢C
        /// </summary>
        /// <param name="c">放入车厢编号</param>
        /// <param name="minH">栈顶编号的最小值</param>
        /// <param name="minS">栈顶编号最小值所在堆栈的编号</param>
        /// <param name="h">缓冲轨道的集合</param>
        /// <returns>如果没有可用的缓冲铁轨，则返回false，否则返回true。</returns>
        static bool Input(int c, ref int minH, ref int minS, LinkStack<int>[] h)
        {
            int bestTrack = -1; //目前最优的铁轨
            int bestTop = int.MaxValue; //最优铁轨上的头辆车厢

            for (int i = 0; i < h.Length; i++)
            {
                if (h[i].IsEmpty() == false)
                {
                    int x = h[i].StackTop;
                    if (c < x && x < bestTop)
                    {
                        bestTop = x;
                        bestTrack = i;
                    }
                }
                else
                {
                    if (bestTrack == -1)
                    {
                        bestTrack = i;
                        break;
                    }
                }
            }
            if (bestTrack == -1)
                return false;

            h[bestTrack].Push(c);
            Console.WriteLine("移动车厢：{0}从入轨到缓冲轨{1}。", c, bestTrack);
            if (c < minH)
            {
                minH = c;
                minS = bestTrack;
            }
            return true;
        }

        static void Main(string[] args)
        {
            int[] p = new int[] {3, 6, 9, 2, 4, 7, 1, 8, 5};
            int k = 1;
            bool result = RailRoad(p, k);
            do
            {
                if (result == false)
                {
                    Console.WriteLine("需要更多的缓冲轨道,请输入需要添加的数量:");
                    k = k + Convert.ToInt32(Console.ReadLine());
                    result = RailRoad(p, k);
                }
            } while (result == false);
        }
    }
}
```


