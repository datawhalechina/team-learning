
# Task01：数组（1天）
## 1. 数组的定义

数组是具有一定顺序关系的若干对象组成的集合，组成数组的对象称为数组元素。

例如：
- 向量对应一维数组
- 矩阵对应二维数组


数组名表示群体的共性，即具有同一种数据类型；下标表示个体的个性，即各自占有独立的单元。

## 2. 数组的存储

**2.1 n维数组的定义**

下标由n个数组成的数组称为n维数组。

例如：
- `int[] a = new int[10]; //一维数组（线）`
- `int[ , ] a = new int[2,3];//二维数组 （面）`
- `int[ , , ] a = new int[2,3,4];//三维数组 （体）；类比：书（体）【2.页码 3.行 4.列】` 

**2.2 数组存储的特点**
- 数组元素在内存中按顺序连续存储。
- 数组的存储分配按照行（C、C++、C#等）或列（Forturn等）进行。
- 数组名表示该数组的首地址，是常量。

**2.3 常用数组的存储**

<u>一维数组`a[n]`</u>

各元素按下角标依次存放。

例：`int[] a = new int[5];`

![一维数组存储](https://img-blog.csdnimg.cn/20191218195949938.png)

<u>二维数组`a[m,n]`</u>

例：`int[ , ] a = new int[2,3];`

![二维数组存储](https://img-blog.csdnimg.cn/20191218200106993.png)


<u>三维数组`a[m,n,l]`</u>

第一维下标变化最慢，第三维（最后一维）下标变化最快。

例：`int[ , , ] a = new int[2,3,4];`

![三维数组的存储](https://img-blog.csdnimg.cn/20191218200209131.png)

## 3. 静态数组与动态数组

**3.1 静态数组**

在程序编译时分配空间的数组。

例：
`int[] a = new int[10];//静态数组（声明之后数组长度不可改变）`

**3.2 动态数组**

在程序运行时分配空间的数组（声明之后数组长度可根据问题而调整）。

![动态数组类图](https://img-blog.csdnimg.cn/2019121820102094.png)


以下代码为`C#`版本：

```c
using System;

namespace LinearStruct
{
    /// <summary>
    /// 动态数组的抽象数据类型实现
    /// </summary>
    /// <typeparam name="T">动态数组中元素的类型</typeparam>
    public class DArray<T> where T : IComparable<T>
    {
        private T[] _array;

        /// <summary>
        /// 获取动态数组的当前长度
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// 初始化DArray类的新实例
        /// </summary>
        /// <param name="size">动态数组的初始长度</param>
        public DArray(int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException();

            Size = size;
            _array = new T[Size];
        }

        /// <summary>
        /// 改变动态数组的长度
        /// </summary>
        /// <param name="newSize">动态数组的新长度</param>
        public void ReSize(int newSize)
        {
            if (newSize <= 0)
                throw new ArgumentOutOfRangeException();

            if (Size != newSize)
            {
                T[] newArray = new T[newSize];
                int n = newSize < Size ? newSize : Size;
                for (int i = 0; i < n; i++)
                {
                    newArray[i] = _array[i];
                }
                _array = newArray;
                Size = newSize;
            }
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
                if (index < 0 || index > Size - 1)
                    throw new IndexOutOfRangeException();
                return _array[index];
            }
            set
            {
                if (index < 0 || index > Size - 1)
                    throw new IndexOutOfRangeException();
                _array[index] = value;
            }
        }
    }
}
```

## 4. 练习参考答案

**1. 利用动态数组解决数据存放问题。**

以下代码为`C#`版本：

```c
using System;
using LinearStruct;

namespace SampleDArray
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("N=");
            string s = Console.ReadLine();
            int n;
            if (int.TryParse(s, out n))
            {
                DArray<int> arr = new DArray<int>(10);
                int j = 0;
                for (int i = 2; i <= n; i++)
                {
                    if (i%5 == 0 || i%7 == 0)
                    {
                        if (j == arr.Size)
                            arr.ReSize(arr.Size + 10);

                        arr[j] = i;
                        j++;
                    }
                }
                for (int i = 0; i < j; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }
        }
    }
}
```


**2. 托普利茨矩阵问题**

以下为`python`版本

```python
class Solution(object):
    """
    所有对角线上的元素都满足 a_1 = a_2, a_2 = a_3, a_{k-1} = a_ka
    对于对角线上的元素来说，如果当前元素不是第一个出现的元素，
    那么它前面的元素一定在当前元素的左上角。
    可以推出，对于位于坐标 (r, c) 上的元素，只需要检查 r == 0 OR c == 0 OR matrix[r-1][c-1] == matrix[r][c] 就可以了。

    """

    def isToeplitzMatrix(self, matrix):
        # all()表示所有都为true才返回true
        # python里面可以在列表推导式里面使用双层for循环，但不建议超过两层
        return all(r == 0 or c == 0 or matrix[r - 1][c - 1] == val
                   for r, row in enumerate(matrix)
                   for c, val in enumerate(row))


if __name__ == '__main__':
    matrix = [
        [1, 2, 3, 4],
        [5, 1, 2, 3],
        [9, 5, 1, 2]
    ]
    solution = Solution()
    output = solution.isToeplitzMatrix(matrix)
    print(output)
```






**3. 三数之和**

以下代码为`C#`版本：

```c
public class Solution {
    public IList<IList<int>> ThreeSum(int[] nums) {
        IList<IList<int>> result = new List<IList<int>>();
        if (nums == null || nums.Length < 3)
            return result;

        nums = nums.OrderBy(a => a).ToArray();
        int len = nums.Length;
        
        for (int i = 0; i < len; i++)
        {
            if (nums[i] > 0) // 如果当前数字大于0，则三数之和一定大于0，所以结束循环
                break;

            if (i > 0 && nums[i] == nums[i - 1])
                continue; // 去重

            int l = i + 1;
            int r = len - 1;

            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                if (sum == 0)
                {
                    result.Add(new List<int>() {nums[i], nums[l], nums[r]});
                    while (l < r && nums[l] == nums[l + 1]) l++; // 去重
                    while (l < r && nums[r - 1] == nums[r]) r--; //去重
                    l++;
                    r--;
                }
                else if (sum < 0)
                {
                    l++;
                }
                else if (sum > 0)
                {
                    r--;
                }
            }
        }
        return result;    
    }
}
```
