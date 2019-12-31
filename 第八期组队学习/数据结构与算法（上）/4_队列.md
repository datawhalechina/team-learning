
# Task04：队列（2天）

队列也是我们经常使用的一种数据结构，如下图所示，购物结账，去食堂打饭等都需要排队，而结账或打饭的顺序与我们排队的顺序是相同的，即谁先排队就为谁先服务。

![](https://img-blog.csdnimg.cn/20191223190948974.png)

比如我们发送邮件、打印资料，这些都是队列的具体应用。我们把需要发送的邮件先放到发送队列中，然后按照放入的顺序进行发送，把需要打印的文件先放到打印队列中， 然后按照放入的顺序进行打印。下面我们就来详细介绍“队列”这种数据结构。

## 1. 队列的定义与操作

**1.1 队列的定义**

插入（入队）在一端（队尾）进行而删除（出队）在另一端（队首）进行的线性表。即先进先出（First In First Out）的线性表。

例1 ：线性表`a0,a1,...,an`入队与出队演示。

![顺序表模拟入队、出队](https://img-blog.csdnimg.cn/20191223191501199.png)

![单链表模拟入队、出队](https://img-blog.csdnimg.cn/20191223191548730.png)

如上所示，队列也存在两种实现方式，这两种实现方式的对比已经在栈与递归部分进行了解释，在这里就不再赘述了。

**1.2 队列的操作**

- 入队操作：将数据元素插入队尾。
- 出队操作：移除队首的数据元素。
- 是否为空：判断队中是否包含数据元素。
- 得到队长：获取队中实际包含数据元素的个数。
- 清空操作：移除队中的所有数据元素。
- 获取队首元素。

![队列接口](https://img-blog.csdnimg.cn/20191223191753857.png)

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 队列的抽象数据类型
    /// </summary>
    /// <typeparam name="T">队列中元素的类型</typeparam>
    public interface IQueue<T> where T : IComparable<T>
    {
        /// <summary>
        /// 获取队列中实际包含元素的个数
        /// </summary>
        int Length { get; }

        /// <summary>
        /// 获取队首元素
        /// </summary>
        T QueueFront { get; }

        /// <summary>
        /// 数据元素入队
        /// </summary>
        /// <param name="data">要入队的元素</param>
        void EnQueue(T data);

        /// <summary>
        /// 数据元素出队
        /// </summary>
        void DeQueue();

        /// <summary>
        /// 判断队列中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        bool IsEmpty();

        /// <summary>
        /// 从队列中移除所有元素
        /// </summary>
        void Clear();
    }
}
```


## 2. 队列的存储与实现

**2.1 顺序存储**

<u>顺序队列</u>

顺序队列：（Sequence Queue）：利用顺序表实现的队列。

实现：

![顺序队列](https://img-blog.csdnimg.cn/20191223192315765.png)

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用顺序存储结构实现的队列--顺序队列
    /// </summary>
    /// <typeparam name="T">顺序队列中元素的类型</typeparam>
    public class SeqQueue<T> : IQueue<T> where T : IComparable<T>
    {
        private readonly SeqList<T> _lst;

        /// <summary>
        /// 初始化SeqQueue类的新实例
        /// </summary>
        /// <param name="max">SeqQueue中最多包含元素的个数</param>
        public SeqQueue(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException();
            _lst = new SeqList<T>(max);
        }

        /// <summary>
        /// 获取SeqQueue中最多包含元素的个数
        /// </summary>
        public int MaxSize
        {
            get { return _lst.MaxSize; }
        }

        /// <summary>
        /// 获取SeqQueue中实际包含元素的个数
        /// </summary>
        public int Length
        {
            get { return _lst.Length; }
        }

        /// <summary>
        /// 获取SeqQueue中的队首元素
        /// </summary>
        public T QueueFront
        {
            get
            {
                if (_lst.IsEmpty())
                    throw new Exception("队列为空,不能得到队首元素.");
                return _lst[0];
            }
        }

        /// <summary>
        /// 数据元素入队
        /// </summary>
        /// <param name="data">要入队的元素</param>
        public void EnQueue(T data)
        {
            if (_lst.Length == _lst.MaxSize)
                throw new Exception("队列已满,不能入队.");
            _lst.Insert(_lst.Length, data);
        }

        /// <summary>
        /// 数据元素出队
        /// </summary>
        public void DeQueue()
        {
            if (_lst.IsEmpty())
                throw new Exception("队列为空,不能出队.");
            _lst.Remove(0);
        }

        /// <summary>
        /// 判断队列中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return _lst.IsEmpty();
        }

        /// <summary>
        /// 从队列中移除所有元素
        /// </summary>
        public void Clear()
        {
            _lst.Clear();
        }
    }
}
```
<u>循环队列</u>

循环队列（Circular Sequence Queue）：利用数组采用循环的方式实现的队列。

![循环队列过程演示](https://img-blog.csdnimg.cn/20191223192517344.png)


实现：

![循环队列](https://img-blog.csdnimg.cn/20191223192722855.png)

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用顺序存储结构实现的队列--循环队列
    /// </summary>
    /// <typeparam name="T">循环队列中元素的类型</typeparam>
    public class CSeqQueue<T> : IQueue<T> where T : IComparable<T>
    {
        private int _pFront;
        private int _pRear;
        private readonly T[] _dataset;

        /// <summary>
        /// 获取CSeqQueue中实际包含元素的个数
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// 获取CSeqQueue中最多包含元素的个数
        /// </summary>
        public int MaxSize { get; }

        /// <summary>
        /// 获取CSeqQueue中的队首元素
        /// </summary>
        public T QueueFront
        {
            get
            {
                if (Length == 0)
                    throw new Exception("队列为空不能得到队首元素.");

                return _dataset[_pFront];
            }
        }

        /// <summary>
        /// 初始化CSeqQueue类的新实例
        /// </summary>
        /// <param name="max">CSeqQueue中最多包含元素的个数</param>
        public CSeqQueue(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException();
            MaxSize = max;
            Length = 0;
            _dataset = new T[MaxSize];
            _pFront = 0;
            _pRear = 0;
        }

        /// <summary>
        /// 数据元素入队
        /// </summary>
        /// <param name="data">要入队的元素</param>
        public void EnQueue(T data)
        {
            if (Length == MaxSize)
                throw new Exception("队列已满,不能入队.");
            _dataset[_pRear] = data;
            _pRear = (_pRear + 1)%MaxSize;
            Length++;
        }

        /// <summary>
        /// 数据元素出队
        /// </summary>
        public void DeQueue()
        {
            if (Length == 0)
                throw new Exception("队列为空,不能出队.");
            _pFront = (_pFront + 1)%MaxSize;
            Length--;
        }

        /// <summary>
        /// 判断队列中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return Length == 0;
        }

        /// <summary>
        /// 从队列中移除所有元素
        /// </summary>
        public void Clear()
        {
            _pFront = 0;
            _pRear = 0;
            Length = 0;
        }
    }
}
```


**2.2 链式存储（链队）**

链队：利用单链表实现的队列。

实现：


![链队](https://img-blog.csdnimg.cn/20191223192940470.png)

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 用链式存储结构实现的队列
    /// </summary>
    /// <typeparam name="T">队列中元素的类型</typeparam>
    public class LinkQueue<T> : IQueue<T> where T : IComparable<T>
    {
        private readonly SLinkList<T> _lst;

        /// <summary>
        /// 获取LinkQueue中实际包含元素的个数
        /// </summary>
        public int Length
        {
            get { return _lst.Length; }
        }

        /// <summary>
        /// 获取LinkQueue中的队首元素
        /// </summary>
        public T QueueFront
        {
            get
            {
                if (_lst.IsEmpty())
                    throw new Exception("队列为空,不能取队首元素.");
                return _lst[0];
            }
        }

        /// <summary>
        /// 初始化LinkQueue类的新实例
        /// </summary>
        public LinkQueue()
        {
            _lst = new SLinkList<T>();
        }

        /// <summary>
        /// 数据元素入队
        /// </summary>
        /// <param name="data">要入队的元素</param>
        public void EnQueue(T data)
        {
            _lst.InsertAtRear(data);
        }

        /// <summary>
        /// 数据元素出队
        /// </summary>
        public void DeQueue()
        {
            if (_lst.IsEmpty())
                throw new Exception("队列为空,不能出队.");

            _lst.Remove(0);
        }

        /// <summary>
        /// 判断队列中是否包含元素
        /// </summary>
        /// <returns>如果包含元素返回false,否则返回true.</returns>
        public bool IsEmpty()
        {
            return _lst.IsEmpty();
        }

        /// <summary>
        /// 从队列中移除所有元素
        /// </summary>
        public void Clear()
        {
            _lst.Clear();
        }
    }
}
```


## 3. 练习参考答案

**1. 模拟银行服务的程序代码**

以下代码为`C#`版本：

表示银行队列的接口
```c
using LinearStruct;

namespace BankQueue
{
    public interface IBankQueue : IQueue<int>
    {
        /// <summary>
        /// 获得服务号码
        /// </summary>
        /// <returns>服务号码</returns>
        int GetCallnumber();
        int MaxSize { get; }
    }
}
```

利用链式存储结构存储银行队列
```c
using LinearStruct;

namespace BankQueue
{
    public class LinkBankQueue : LinkQueue<int>, IBankQueue
    {
        public int Callnumber { get; private set; }
        public int MaxSize { get; }
        public int GetCallnumber()
        {
            if (IsEmpty() && Callnumber == 0)
            {
                Callnumber = 1;
            }
            else
            {
                Callnumber++;
            }
            return Callnumber;
        }
        public LinkBankQueue()
        {
            MaxSize = default(int);
            Callnumber = 0;
        }
    }
}
```

利用顺序存储结构存储银行队列
```c
using LinearStruct;

namespace BankQueue
{
    public class CSeqBankQueue : CSeqQueue<int>, IBankQueue
    {
        public int Callnumber { get; private set; }

        public CSeqBankQueue(int size) : base(size)
        {
            Callnumber = 0;
        }
        public int GetCallnumber()
        {
            if (IsEmpty() && Callnumber == 0)
            {
                Callnumber = 1;
            }
            else
            {
                Callnumber++;
            }
            return Callnumber;
        }
    }
}
```

服务窗口
```c
using System;
using System.Threading;

namespace BankQueue
{
    public class ServiceWindow
    {
        //服务队列属性
        public IBankQueue BankQ { get; set; }

        //线程方法
        public void Service()
        {
            while (true)
            {
                lock (BankQ)
                {
                    Thread.Sleep(2000);
                    if (!BankQ.IsEmpty())
                    {
                        Console.WriteLine();
                        Console.WriteLine("请{0}号到{1}号窗口!", BankQ.QueueFront, Thread.CurrentThread.Name);
                        BankQ.DeQueue();
                    }
                }
            }
        }
    }
}
```

客户端
```c
using System;
using System.Threading;

namespace BankQueue
{
    class BankQueueApp
    {
        public static void Main(string[] args)
        {

            try
            {
                IBankQueue bankQueue = null;
                Console.WriteLine("请选择存储结构的类型：1.顺序队列 2.链队列");
                string seleflag = Console.ReadLine();
                switch (seleflag)
                {
                    case "1":
                        Console.Write("请输入队列可容纳人数：");
                        int count = Convert.ToInt32(Console.ReadLine());
                        bankQueue = new CSeqBankQueue(count);
                        break;
                    case "2":
                        bankQueue = new LinkBankQueue();
                        break;
                }

                int windowcount = 3;
                ServiceWindow[] serviceWindows = new ServiceWindow[windowcount];
                Thread[] serviceThread = new Thread[windowcount];
                for (int i = 0; i < windowcount; i++)
                {
                    serviceWindows[i] = new ServiceWindow();
                    serviceWindows[i].BankQ = bankQueue;
                    serviceThread[i] = new Thread(serviceWindows[i].Service);
                    serviceThread[i].Name = (i + 1).ToString();
                    serviceThread[i].Start();
                }
                while (true)
                {
                    Console.Write("请点击触摸屏获取号码：");
                    Console.ReadLine();
                    if (bankQueue != null && (bankQueue.Length < bankQueue.MaxSize || seleflag == "2"))
                    {
                        int callnumber = bankQueue.GetCallnumber();
                        Console.WriteLine("您的号码是：{0}，你前面有{1}位，请等待！",
                            callnumber, bankQueue.Length);
                        bankQueue.EnQueue(callnumber);
                    }
                    else
                        Console.WriteLine("现在业务繁忙，请稍后再来！");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
```