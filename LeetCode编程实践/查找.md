[TOC]
# 一.查找表
## 考虑的基本数据结构

**第一类： 查找有无--set**

元素'a'是否存在，通常用set：集合

set只存储键，而不需要对应其相应的值。 

set中的键不允许重复

**第二类： 查找对应关系(键值对应)--dict**

元素'a'出现了几次：dict-->字典

dict中的键不允许重复

**第三类： 改变映射关系--map**

通过将原有序列的关系映射统一表示为其他

## 算法应用


### LeetCode 349 Intersection Of Two Arrays 1

#### 题目描述
给定两个数组nums,求两个数组的公共元素。

```
如nums1 = [1,2,2,1],nums2 = [2,2]

结果为[2]
结果中每个元素只能出现一次
出现的顺序可以是任意的
```




#### 分析实现
由于每个元素只出现一次，因此不需要关注每个元素出现的次数，用set的数据结构就可以了。记录元素的有和无。

把nums1记录为set，判断nums2的元素是否在set中，是的话，就放在一个公共的set中，最后公共的set就是我们要的结果。

代码如下：

```python
class Solution:
    def intersection(self, nums1: List[int], nums2: List[int]) -> List[int]:
        nums1 = set(nums1)
        return set([i for i in nums2 if i in nums1])
```

也可以通过set的内置方法来实现，直接求set的交集：
```python
class Solution:
    def intersection(self, nums1: List[int], nums2: List[int]) -> List[int]:
        set1 = set(nums1)
        set2 = set(nums2)
        return set2 & set1
```


### LeetCode 350 Intersection Of Two Arrays 2

#### 题目描述

给定两个数组nums,求两个数组的交集。

-- 如nums1=[1,2,2,1],nums=[2,2]

-- 结果为[2,2]

-- 出现的顺序可以是任意的


#### 分析实现
元素出现的次数有用，那么对于存储次数就是有意义的，所以选择数据结构时，就应该选择dict的结构，通过字典的比较来判断；

记录每个元素的同时要记录这个元素的频次。

记录num1的字典，遍历nums2，比较nums1的字典的nums的key是否大于零，从而进行判断。

代码如下：

```python
class Solution:
    def intersect(self, nums1: List[int], nums2: List[int]) -> List[int]:
        from collections import Counter
        nums1_dict = Counter(nums1)
        res = []
        for num in nums2:
            if nums1_dict[num] > 0:
                # 说明找到了一个元素即在num1也在nums2
                res.append(num)
                nums1_dict[num] -= 1
        return res        
```

### LeetCode 242 Intersection Of Two Arrays 2

#### 题目描述 
给定两个字符串 s 和 t ，编写一个函数来判断 t 是否是 s 的字母异位词。

```
示例1:

输入: s = "anagram", t = "nagaram"
输出: true

示例 2:

输入: s = "rat", t = "car"
输出: false
```

#### 分析实现
判断异位词即判断变换位置后的字符串和原来是否相同，那么不仅需要存储元素，还需要记录元素的个数。可以选择dict的数据结构，将字符串s和t都用dict存储，而后直接比较两个dict是否相同。

```python
class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        from collections import Counter
        s = Counter(s)
        t = Counter(t)
        if s == t:
            return True
        else:
            return False
```


### LeetCode 202 Happy number

#### 题目描述

编写一个算法来判断一个数是不是“快乐数”。

一个“快乐数”定义为：对于一个正整数，每一次将该数替换为它每个位置上的数字的平方和，然后重复这个过程直到这个数变为 1，也可能是无限循环但始终变不到 1。如果可以变为 1，那么这个数就是快乐数。

```
示例: 
输入: 19
输出: true
解释: 
1^2 + 9^2 = 82
8^2 + 2^2 = 68
6^2 + 8^2 = 100
1^2 + 0^2 + 0^2 = 1
```




#### 分析实现

这道题目思路很明显，当n不等于1时就循环，每次循环时，将其最后一位到第一位的数依次平方求和，比较求和是否为1。

难点在于，什么时候跳出循环？

开始笔者的思路是，循环个100次，还没得出结果就false，但是小学在算无限循环小数时有一个特征，就是当除的数中，和之前历史的得到的数有重合时，这时就是无限循环小数。

那么这里也可以按此判断，因为只需要判断有或无，不需要记录次数，故用set的数据结构。每次对求和的数进行append，当新一次求和的值存在于set中时，就return false.

代码如下：

```python
class Solution:
    def isHappy(self, n: int) -> bool:
        already = set()
        while n != 1:
            sum = 0
            while n > 0:
                # 取n的最后一位数
                tmp = n % 10   
                sum += tmp ** 2
                # 将n的最后一位截掉
                n //= 10
            # 如果求的和在过程中出现过
            if sum in already:
                return False
            else:
                already.add(sum)
            n = sum
        return True
```

#### tips
```python
#一般对多位数计算的套路是：
#循环从后向前取位数
while n >0 :
#取最后一位： 
tmp = n % 10
#再截掉最后一位：
n = n // 10
```

### LeetCode 290 Word Pattern

#### 题目描述

给出一个模式(pattern)以及一个字符串，判断这个字符串是否符合模式

```
示例1:
输入: pattern = "abba", 
str = "dog cat cat dog"
输出: true

示例 2:
输入:pattern = "abba", 
str = "dog cat cat fish"
输出: false

示例 3:
输入: pattern = "aaaa", str = "dog cat cat dog"
输出: false

示例 4:
输入: pattern = "abba", str = "dog dog dog dog"
输出: false
```



#### 分析实现
抓住变与不变，笔者开始的思路是选择了dict的数据结构，比较count值和dict对应的keys的个数是否相同，但是这样无法判断顺序的关系，如测试用例：'aba','cat cat dog'。

那么如何能**既考虑顺序**，也考虑**键值对应的关系**呢？

抓住变与不变，变的是键，但是不变的是各个字典中，对应的相同index下的值，如dict1[index] = dict2[index]，那么我们可以创建两个新的字典，遍历index对两个新的字典赋值，并比较value。

还有一个思路比较巧妙，既然不同，那么可以考虑怎么让它们相同，将原来的dict通过map映射为相同的key，再比较相同key的dict是否相同。

代码实现如下：

```python
class Solution:
    def wordPattern(self,pattern, str):
        str = str.split()
        return list(map(pattern.index,pattern)) == list(map(str.index,str))
```
#### tips
1. 因为str是字符串，不是由单个字符组成，所以开始需要根据空格拆成字符list：
```python
str = str.split()
```

2. 通过map将字典映射为index的list:
```python
map(pattern.index, pattern)
```
3. map是通过hash存储的，不能直接进行比较，需要转换为list比较list

### LeetCode 205 Isomorphic Strings

#### 题目描述

给定两个字符串 s 和 t，判断它们是否是同构的。

如果 s 中的字符可以被替换得到 t ，那么这两个字符串是同构的。

所有出现的字符都必须用另一个字符替换，同时保留字符的顺序。两个字符不能映射到同一个字符上，但字符可以映射自己本身。

```
示例 1:
输入: s = "egg", t = "add"
输出: true

示例 2:
输入: s = "foo", t = "bar"
输出: false

示例 3:
输入: s = "paper", t = "title"
输出: true
```




#### 分析实现

思路与上题一致，可以考虑通过建两个dict，比较怎样不同，也可以将不同转化为相同。

直接用上题的套路代码：
```python
class Solution:
    def isIsomorphic(self, s: str, t: str) -> bool:
        return list(map(s.index,s)) == list(map(t.index,t))
```

### LeetCode 451 Sort Characters By Frequency

#### 题目描述

给定一个字符串，请将字符串里的字符按照出现的频率降序排列。

```
示例 1:
输入:
"tree"
输出:
"eert"

示例 2:
输入:
"cccaaa"
输出:
"cccaaa"

示例 3:
输入:
"Aabb"
输出:
"bbAa"
```



#### 分析实现

对于相同频次的字母，顺序任意，需要考虑大小写，返回的是字符串。

使用字典统计频率，对字典的value进行排序，最终根据key的字符串乘上value次数，组合在一起输出。

```python
class Solution:
    def frequencySort(self, s: str) -> str:
        from collections import Counter
        s_dict = Counter(s)
        # sorted返回的是列表元组
        s = sorted(s_dict.items(), key=lambda item:item[1], reverse = True)
        # 因为返回的是字符串
        res = ''
        for key, value in s:
            res += key * value   
        return res
```

#### tips
1. 通过sorted的方法进行value排序，对字典排序后无法直接按照字典进行返回，返回的为列表元组：
```python
# 对value值由大到小排序
s = sorted(s_dict.items(), key=lambda item:item[1], reverse = True)

# 对key由小到大排序
s = sorted(s_dict.items(), key=lambda item:item[0])
```
2. 输出为字符串的情况下，可以由字符串直接进行拼接:
```python
# 由key和value相乘进行拼接
's' * 5 + 'd'*2
```

# 二. 对撞指针

# LeetCode 1 Two Sum

## 题目描述

给出一个整型数组nums，返回这个数组中两个数字的索引值i和j，使得nums[i] + nums[j]等于一个给定的target值，两个索引不能相等。

如：nums= [2,7,11,15],target=9
返回[0,1]


## 审题:

需要考虑：

1. 开始数组是否有序；
2. 索引从0开始计算还是1开始计算？
3. 没有解该怎么办？
4. 有多个解怎么办？保证有唯一解。

## 分析实现
## 暴力法O(n^2)
时间复杂度为O(n^2),第一遍遍历数组，第二遍遍历当前遍历值之后的元素，其和等于target则return。


```python
class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        len_nums = len(nums)
        for i in range(len_nums):
            for j in range(i+1,len_nums):
                if nums[i] + nums[j] == target:
                    return [i,j]
```
## 排序+指针对撞(O(n)+O(nlogn)=O(n))

在数组篇的LeetCode 167题中，也遇到了找到两个数使得它们相加之和等于目标数，但那是对于排序的情况，因此也可以使用上述的思路来完成。

因为问题本身不是有序的，因此需要对原来的数组进行一次排序，排序后就可以用O(n)的指针对撞进行解决。

但是问题是，返回的是数字的索引，如果只是对数组的值进行排序，那么数组原来表示的索引的信息就会丢失，所以在排序前要进行些处理。


**错误代码示例--只使用dict来进行保存：**
```python
class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        record = dict()
        for index in range(len(nums)):
            record[nums[index]] = index 
        nums.sort()
        l,r = 0,len(nums)-1
        while l < r:
            if nums[l] + nums[r] == target:
                return [record[nums[l]],record[nums[r]]]
            elif nums[l] + nums[r] < target:
                l += 1
            else:
                r -= 1
```
当遇到**相同的元素的索引**问题时，会不满足条件：

如：[3,3]  6

在排序前先使用一个额外的数组**拷贝**一份原来的数组，对于两个相同元素的索引问题，使用一个**bool型变量**辅助将两个索引都找到，总的时间复杂度为O(n)+O(nlogn) = O(nlogn)

```python
class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        record = dict()
        nums_copy = nums.copy()
        sameFlag = True;
        nums.sort()
        l,r = 0,len(nums)-1
        while l < r:
            if nums[l] + nums[r] == target:
                break
            elif nums[l] + nums[r] < target:
                l += 1
            else:
                r -= 1
        res = []
        for i in range(len(nums)):
            if nums_copy[i] == nums[l] and sameFlag:
                res.append(i)
                sameFlag = False
            elif nums_copy[i] == nums[r]:
                res.append(i)
        return res
```

### 小套路:

如果只是对数组的值进行排序，那么数组原来表示的索引的信息就会丢失的情况，可以在排序前：

### 更加pythonic的实现

通过list(enumerate(nums))开始实现下标和值的绑定，不用专门的再copy加bool判断。

```python
nums = list(enumerate(nums))
nums.sort(key = lambda x:x[1])
i,j = 0, len(nums)-1
while i < j:
    if nums[i][1] + nums[j][1] > target:
        j -= 1
    elif nums[i][1] + nums[j][1] < target:
        i += 1
    else:
        if nums[j][0] < nums[i][0]:
            nums[j],nums[i] = nums[i],nums[j]
        return num[i][0],nums[j][0]
```
**拷贝数组 + bool型变量辅助**

## 查找表--O(n)

遍历数组过程中，当遍历到元素v时，可以只看v前面的元素，是否含有target-v的元素存在。

1. 如果查找成功，就返回解；
2. 如果没有查找成功，就把v放在查找表中，继续查找下一个解。

即使v放在了之前的查找表中覆盖了v，也不影响当前v元素的查找。因为只需要找到两个元素，只需要找target-v的另一个元素即可。


```python
class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        record = dict()
        for i in range(len(nums)):
            complement = target - nums[i]
            # 已经在之前的字典中找到这个值
            if record.get(complement) is not None:
                res = [i,record[complement]]
                return res
            record[nums[i]] = i
```

只进行一次循环，故时间复杂度O(n),空间复杂度为O(n)



## 补充思路：
通过enumerate来把索引和值进行绑定，进而对value进行sort，前后对撞指针进行返回。


```python
class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        nums = list(enumerate(nums))
        # 根据value来排序
        nums.sort(key = lambda x:x[1])
        l,r = 0, len(nums)-1
        while l < r:
            if nums[l][1] + nums[r][1] == target:
                return nums[l][0],nums[r][0]
            elif nums[l][1] + nums[r][1] < target:
                l += 1
            else:
                r -= 1
```

# LeetCode 15 3Sum

## 题目描述

给出一个整型数组，寻找其中的所有不同的三元组(a,b,c)，使得a+b+c=0

注意：答案中不可以包含重复的三元组。

如：nums = [-1, 0, 1, 2, -1, -4]，

结果为：[[-1, 0, 1],[-1, -1, 2]]

## 审题
1. 数组不是有序的；
2. 返回结果为全部解，多个解的顺序是否需要考虑？--不需要考虑顺序
3. 什么叫不同的三元组？索引不同即不同，还是值不同？--题目定义的是，值不同才为不同的三元组
4. 没有解时怎么返回？--空列表

## 分析实现
因为上篇中已经实现了Two Sum的问题，因此对于3Sum，首先想到的思路就是，开始固定一个k，然后在其后都当成two sum问题来进行解决，但是这样就ok了吗？

### 没有考虑重复元素导致错误

直接使用Two Sum问题中的查找表的解法，根据第一层遍历的i，将i之后的数组作为two sum问题进行解决。

```python
class Solution:
    def threeSum(self, nums: [int]) -> [[int]]:
        res = []
        for i in range(len(nums)):
            num = 0 - nums[i]
            record = dict()
            for j in range(i + 1, len(nums)):
                complement = num - nums[j]
                # 已经在之前的字典中找到这个值
                if record.get(complement) is not None:
                    res_lis = [nums[i], nums[j], complement]
                    res.append(res_lis)
                record[nums[j]] = i
        return res
```

但是这样会导致一个错误，错误用例如下:

```
输入：
[-1,0,1,2,-1,-4]
输出：
[[-1,1,0],[-1,-1,2],[0,-1,1]]
预期结果：
[[-1,-1,2],[-1,0,1]]
```

代码在实现的过程中没有把第一次遍历的i的索引指向相同元素的情况排除掉，于是出现了当i指针后面位置的元素有和之前访问过的相同的值，于是重复遍历。

那么可以考虑，开始时对nums数组进行排序，排序后，当第一次遍历的指针k遇到下一个和前一个指向的值重复时，就将其跳过。为了方便计算，在第二层循环中，可以使用**对撞指针**的套路：

```python
# 对撞指针套路
l,r = 0, len(nums)-1
while l < r:
    if nums[l] + nums[r] == target:
        return nums[l],nums[r]
    elif nums[l] + nums[r] < target:
        l += 1
    else:
        r -= 1
```

其中需要注意的是，在里层循环中，也要考虑重复值的情况，因此当值相等时，再次移动指针时，需要保证其指向的值和前一次指向的值不重复，因此可以：
```python
# 对撞指针套路
l,r = 0, len(nums)-1
while l < r:
    sum = nums[i] + nums[l] + nums[r]
    if sum == target:
        res.append([nums[i],nums[l],nums[r])
        l += 1
        r -= 1
        while l < r and nums[l] == nums[l-1]: l += 1
        while l < r and nums[r] == nums[r+1]: r -= 1
    elif sum < target:
        l += 1
    else:
        r -= 1
```

再调整下遍历的范围，因为设了3个索引：i，l，r。边界情况下，r索引指向len-1, l指向len-2，索引i遍历的边界为len-3，故for循环是从0到len-2。

代码实现如下：


### 代码实现

```python
class Solution:
    def threeSum(self, nums: [int]) -> [[int]]:
        nums.sort()
        res = []
        for i in range(len(nums)-2):
            # 因为是排序好的数组，如果最小的都大于0可以直接排除
            if nums[i] > 0: break
            # 排除i的重复值
            if i > 0 and nums[i] == nums[i-1]: continue
            l,r = i+1, len(nums)-1
            while l < r:
                sum = nums[i] + nums[l] + nums[r]
                if sum == 0:
                    res.append([nums[i],nums[l],nums[r]])
                    l += 1
                    r -= 1
                    while l < r and nums[l] == nums[l-1]: l += 1
                    while l < r and nums[r] == nums[r+1]: r -= 1
                elif sum < 0:
                    l += 1
                else:
                    r -= 1
        return res
```

## 小套路

1. 采用**for + while**的形式来处理三索引；
2. 当数组不是有序时需要注意，有序的特点在哪里，有序就可以用哪些方法解决？无序的话不便在哪里？
3. 对撞指针套路：
```python
# 对撞指针套路
l,r = 0, len(nums)-1
while l < r:
    if nums[l] + nums[r] == target:
        return nums[l],nums[r]
    elif nums[l] + nums[r] < target:
        l += 1
    else:
        r -= 1
```
4. 处理重复值的套路：先转换为有序数组，再循环判断其与上一次值是否重复：
```python
# 1.
for i in range(len(nums)):
    if i > 0 and nums[i] == nums[i-1]: continue
# 2.
while l < r:
    while l < r and nums[l] == nums[l-1]: l += 1
```

# LeetCode 18 4Sum
## 题目描述
给出一个整形数组，寻找其中的所有不同的四元组(a,b,c,d)，使得a+b+c+d等于一个给定的数字target。

```
如:
nums = [1, 0, -1, 0, -2, 2]，target = 0

结果为：
[[-1,  0, 0, 1],[-2, -1, 1, 2],[-2,  0, 0, 2]]
```

## 题目分析
4Sum可以当作是3Sum问题的扩展，注意事项仍是一样的，同样是不能返回重复值得解。首先排序。接着从[0,len-1]遍历i，跳过i的重复元素，再在[i+1,len-1]中遍历j，得到i，j后，再选择首尾的l和r，通过对撞指针的思路，四数和大的话r--，小的话l++,相等的话纳入结果list，最后返回。

套用3Sum得代码，在其前加一层循环，对边界情况进行改动即可:

1. 原来3个是到len-2,现在外层循环是到len-3;
2. 在中间层得迭代中，当第二个遍历得值在第一个遍历得值之后且后项大于前项时，认定为重复；
3. 加些边界条件判断：当len小于4时，直接返回；当只有4个值且长度等于target时，直接返回本身即可。

代码实现如下：

```python
class Solution:
    def fourSum(self, nums: List[int], target: int) -> List[List[int]]:
        nums.sort()
        res = []
        if len(nums) < 4: return res
        if len(nums) == 4 and sum(nums) == target:
            res.append(nums)
            return res
        for i in range(len(nums)-3):
            if i > 0 and nums[i] == nums[i-1]: continue
            for j in range(i+1,len(nums)-2):
                if j > i+1 and nums[j] == nums[j-1]: continue
                l,r = j+1, len(nums)-1
                while l < r:
                    sum_value = nums[i] + nums[j] + nums[l] + nums[r]
                    if sum_value == target:
                        res.append([nums[i],nums[j],nums[l],nums[r]])
                        l += 1
                        r -= 1
                        while l < r and nums[l] == nums[l-1]: l += 1
                        while l < r and nums[r] == nums[r+1]: r -= 1
                    elif sum_value < target:
                        l += 1
                    else:
                        r -= 1
        return res
```

还可以使用combinations(nums, 4)来对原数组中得4个元素全排列，在开始sort后，对排列得到得元素进行set去重。但单纯利用combinations实现会超时。

## 超出时间限制
```python
class Solution:
    def fourSum(self, nums: List[int], target: int) -> List[List[int]]:
        nums.sort()
        from itertools import combinations
        res = []
        for i in combinations(nums, 4):
            if sum(i) == target:
                res.append(i)
        res = set(res)
        return res
                
```

# LeetCode 16 3Sum Closest

## 题目描述
给出一个整形数组，寻找其中的三个元素a,b,c，使得a+b+c的值最接近另外一个给定的数字target。

如：给定数组 nums = [-1，2，1，-4], 和 target = 1.

与 target 最接近的三个数的和为 2. (-1 + 2 + 1 = 2).

## 分析实现
这道题也是2sum,3sum等题组中的，只不过变形的地方在于不是找相等的target，而是找最近的。

那么开始时可以随机设定一个三个数的和为结果值，在每次比较中，先判断三个数的和是否和target相等，如果相等直接返回和。如果不相等，则判断三个数的和与target的差是否小于这个结果值时，如果小于则进行则进行替换，并保存和的结果值。

### 伪代码
```python
# 先排序
nums.sort()
# 随机选择一个和作为结果值
res = nums[0] + nums[1] + nums[2]
# 记录这个差值
diff = abs(nums[0]+nums[1]+nums[2]-target)
# 第一遍遍历
for i in range(len(nums)):
    # 标记好剩余元素的l和r
    l,r = i+1, len(nums-1)
    while l < r:
        if 后续的值等于target:
            return 三个数值得和
        else:
            if 差值小于diff:
                更新diff值
                更新res值
            if 和小于target:
                将l移动
            else:(开始已经排除了等于得情况，要判断和大于target)
                将r移动
```


### 3Sum问题两层遍历得套路代码：
```python
nums.sort()
res = []
for i in range(len(nums)-2):
    l,r = i+1, len(nums)-1
    while l < r:
        sum = nums[i] + nums[l] + nums[r]
        if sum == 0:
            res.append([nums[i],nums[l],nums[r]])
        elif sum < 0:
            l += 1
        else:
            r -= 1
```

### 代码实现：


```python
class Solution:
    def threeSumClosest(self, nums: List[int], target: int) -> int:
        nums.sort()
        diff = abs(nums[0]+nums[1]+nums[2]-target)
        res = nums[0] + nums[1] + nums[2]
        for i in range(len(nums)):
            l,r = i+1,len(nums)-1
            t = target - nums[i]
            while l < r:
                if nums[l] + nums[r] == t:
                    return nums[i] + t
                else:
                    if abs(nums[l]+nums[r]-t) < diff:
                        diff = abs(nums[l]+nums[r]-t)
                        res = nums[i]+nums[l]+nums[r]
                    if nums[l]+nums[r] < t:
                        l += 1
                    else:
                        r -= 1
        return res
```
时间复杂度为O(n^2)，空间复杂度为O(1);


# LeetCode 454 4SumⅡ

## 题目描述
给出四个整形数组A,B,C,D,寻找有多少i,j,k,l的组合,使得A[i]+B[j]+C[k]+D[l]=0。其中,A,B,C,D中均含有相同的元素个数N，且0<=N<=500；

输入:

A = [ 1, 2]
B = [-2,-1]
C = [-1, 2]
D = [ 0, 2]

输出:2


## 分析实现

这个问题同样是Sum类问题得变种，其将同一个数组的条件，变为了四个数组中，依然可以用查找表的思想来实现。

首先可以考虑把D数组中的元素都放入查找表，然后遍历前三个数组，判断target减去每个元素后的值是否在查找表中存在，存在的话，把结果值加1。那么查找表的数据结构选择用set还是dict？考虑到数组中可能存在重复的元素，而重复的元素属于不同的情况，因此用dict存储，最后的结果值加上dict相应key的value，代码如下：

### O(n^3)代码

```python
from collections import Counter
record = Counter()
# 先建立数组D的查找表
for i in range(len(D)):
    record[D[i]] += 1
res = 0 
for i in range(len(A)):
    for j in range(len(B)):
        for k in range(len(C)):
            num_find = 0-A[i]-B[j]-C[k]
            if record.get(num_find) != None:
                res += record(num_find)
return res
```

但是对于题目中给出的数据规模：N<=500，如果N为500时，n^3的算法依然消耗很大，能否再进行优化呢？

根据之前的思路继续往前走，如果只遍历两个数组，那么就可以得到O(n^2)级别的算法，但是遍历两个数组，那么还剩下C和D两个数组，上面的值怎么放？

对于查找表问题而言，**很多时候到底要查找什么**，是解决的关键。对于C和D的数组，可以通过dict来记录其中和的个数，之后遍历结果在和中进行查找。代码如下：

### O(n^2)级代码
```python
class Solution:
    def fourSumCount(self, A: List[int], B: List[int], C: List[int], D: List[int]) -> int:
        from collections import Counter
        record = Counter()
        for i in range(len(A)):
            for j in range(len(B)):
                record[A[i]+B[j]] += 1
        res = 0
        for i in range(len(C)):
            for j in range(len(D)):
                find_num = 0 - C[i] - D[j]
                if record.get(find_num) != None:
                    res += record[find_num]
        return res   
```

再使用Pythonic的列表生成式和sum函数进行优化，如下：
```python
class Solution:
    def fourSumCount(self, A: List[int], B: List[int], C: List[int], D: List[int]) -> int:
        record = collections.Counter(a + b for a in A for b in B)
        return sum(record.get(- c - d, 0) for c in C for d in D)
```




# LeetCode 49 Group Anagrams 

## 题目描述

给出一个字符串数组，将其中所有可以通过颠倒字符顺序产生相同结果的单词进行分组。

```
示例:
输入: ["eat", "tea", "tan", "ate", "nat", "bat"],
输出:[["ate","eat","tea"],["nat","tan"],["bat"]]

说明：
所有输入均为小写字母。
不考虑答案输出的顺序。
```



## 分析实现

在之前LeetCode 242的问题中，对字符串t和s来判断，判断t是否是s的字母异位词。当时的方法是通过构建t和s的字典，比较字典是否相同来判断是否为异位词。

在刚开始解决这个问题时，我也局限于了这个思路，以为是通过移动指针，来依次比较两个字符串是否对应的字典相等，进而确定异位词列表，再把异位词列表添加到结果集res中。于是有：

### 错误思路
```python
nums = ["eat", "tea", "tan", "ate", "nat", "bat"]

from collections import Counter
cum = []
for i in range(len(nums)):
    l,r = i+1,len(nums)-1
    i_dict = Counter(nums[i])
    res = []
    if nums[i] not in cum:
        res.append(nums[i])
    while l < r:
        l_dict = Counter(nums[l])
        r_dict = Counter(nums[r])
        if i_dict == l_dict and l_dict == r_dict:
            res.append(nums[l],nums[r])
            l += 1
            r -= 1
        elif i_dict == l_dict:
            res.append(nums[l])
            l += 1
        elif i_dict == r_dict:
            res.append(nums[r])
            r -= 1
        else:
            l += 1
    print(res)
    cum.append(res)
......................................
```

这时发现长长绵绵考虑不完，而且还要注意指针的条件，怎样遍历才能遍历所有的情况且判断列表是否相互间包含。。。

于是立即开始反思是否哪块考虑错了?回顾第一开始的选择数据结构，在dict和list中，自己错误的选择了list来当作数据结构，进而用指针移动来判断元素的情况。而**没有利用题目中不变的条件**。

题目的意思，对异位词的进行分组，同异位词的分为一组，那么考虑对这一组内什么是相同的，且这个相同的也能作为不同组的判断条件。

不同组的判断条件，就可以用数据结构dict中的key来代表，那么什么相同的适合当作key呢？

这时回顾下下LeetCode 242，当时是因为异位字符串中包含的**字符串的字母个数**都是相同的，故把字母当作key来进行判断是否为异位词。

但是对于本题，把每个字符串的字母dict，再当作字符串数组的dict的key，显然不太合适，那么对于异位词，还有什么是相同的？

显然，如果将字符串统一排序，**异位词排序后的字符串**，显然都是相同的。那么就可以把其当作key，把遍历的数组中的异位词当作value，对字典进行赋值，进而遍历字典的value，得到结果list。

需要注意的细节是，**字符串和list之间的转换**：

1. 默认构造字典需为list的字典；
2. 排序使用sorted()函数，而不用list.sort()方法，因为其不返回值；
3. 通过''.join(list)，将list转换为字符串；
4. 通过str.split(',')将字符串整个转换为list中的一项；


```python
class Solution:
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        from collections import defaultdict
        strs_dict = defaultdict(list)
        res = []
        for str in strs:
            key = ''.join(sorted(list(str)))
            strs_dict[key] += str.split(',')
        for v in strs_dict.values():
            res.append(v)
        return res
```

再将能用列表生成式替换的地方替换掉,代码实现如下：

```python
class Solution:
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        from collections import defaultdict
        strs_dict = defaultdict(list)
        for str in strs:
            key = ''.join(sorted(list(str)))
            strs_dict[key] += str.split(',')
        return [v for v in strs_dict.values()]
```

# LeetCode 447 Number of Boomerangs

## 题目描述
给出一个平面上的n个点，寻找存在多少个由这些点构成的三元组(i,j,k)，**使得i,j两点的距离等于i,k两点的距离**。

其中n最多为500,且所有的点坐标的范围在[-10000,10000]之间。

```
输入:
[[0,0],[1,0],[2,0]]

输出:
2
解释:
两个结果为： [[1,0],[0,0],[2,0]] 和 [[1,0],[2,0],[0,0]]
```



## 分析实现

### 原始思路
题目的要求是：使得i,j两点的距离等于i,k两点的距离，那么相当于是比较三个点之间距离的，那么开始的思路就是三层遍历，i从0到len，j从i+1到len，k从j+1到len，然后比较三个点的距离，相等则结果数加一。

显然这样的时间复杂度为O(n^3)，对于这道题目，能否用查找表的思路进行解决优化？

### 查找表
之前的查找表问题，大多是通过**构建一个查找表**，而避免了在查找中再内层嵌套循环，从而降低了时间复杂度。那么可以考虑在这道题中，可以通过查找表进行代替哪两层循环。

当i,j两点距离等于i,k时，用查找表的思路，等价于：对距离key(i,j或i,k的距离)，其值value(个数)为2。

那么就可以做一个查找表，用来查找相同距离key的个数value是多少。遍历每一个节点i，扫描得到其他点到节点i的距离，在查找表中，对应的键就是距离的值，对应的值就是距离值得个数。

在拿到对于元素i的距离查找表后，接下来就是排列选择问题了：

1. 如果当距离为x的值有2个时，那么选择j,k的可能情况有：第一次选择有2种，第二次选择有1种，为2*1；
2. 如果当距离为x的值有3个时，那么选择j,k的可能的情况有：第一次选择有3种，第二次选择有2种，为3*2;
3. 那么当距离为x的值有n个时，选择j,k的可能情况有：第一次选择有n种，第二次选择有n-1种。

### 距离
对于距离值的求算，按照欧式距离的方法进行求算的话，容易产生浮点数，可以将根号去掉，用差的平方和来进行比较距离。

实现代码如下：

```python
class Solution:
    def numberOfBoomerangs(self, points: List[List[int]]) -> int:
        res = 0
        from collections import Counter
        for i in points:
            record = Counter()
            for j in points:
                if i != j:
                    record[self.dis(i,j)] += 1
            for k,v in record.items():
                res += v*(v-1)
        return res
    def dis(self,point1,point2):
        return (point1[0]-point2[0]) ** 2 + (point1[1]-point2[1]) ** 2
```

### 优化
对实现的代码进行优化：

1. 将for循环遍历改为列表生成式;
2. 对sum+=的操作，考虑使用sum函数。
3. 对不同的函数使用闭包的方式内嵌；

```python
class Solution:
    def numberOfBoomerangs(self, points: List[List[int]]) -> int:
        from collections import Counter
        def f(x1, y1):
            # 对一个i下j,k的距离值求和
            d = Counter((x2 - x1) ** 2 + (y2 - y1) ** 2 for x2, y2 in points)
            return sum(t * (t-1) for t in d.values())
        # 对每个i的距离进行求和
        return sum(f(x1, y1) for x1, y1 in points)
```

# LeetCode 149 Max Points on a Line

## 题目描述
给定一个二维平面，平面上有 n 个点，求最多有多少个点在同一条直线上。

```
示例 1:
输入: [[1,1],[2,2],[3,3]]
输出: 3

示例 2:
输入: [[1,1],[3,2],[5,3],[4,1],[2,3],[1,4]]
输出: 4
```



## 分析实现
本道题目的要求是：看有多少个点在同一条直线上，那么判断点是否在一条直线上，其实就等价于判断i,j两点的斜率是否等于i,k两点的斜率。

回顾上道447题目中的要求：使得i,j两点的距离等于i,k两点的距离，那么在这里，直接考虑使用查找表实现，即**查找相同斜率key的个数value是多少**。

在上个问题中，i和j，j和i算是两种不同的情况，但是这道题目中，这是属于相同的两个点，
因此在对遍历每个i,查找与i相同斜率的点时，不能再对结果数res++，而应该取查找表中的最大值。如果有两个斜率相同时，返回的应该是3个点，故返回的是结果数+1。

查找表实现套路如下：

```python
class Solution:
    def maxPoints(self,points):
        res = 0
        from collections import defaultdict
        for i in range(len(points)):
            record = defaultdict(int)
            for j in range(len(points)):
                if i != j:
                    record[self.get_Slope(points,i,j)] += 1
            for v in record.values():
                res = max(res, v)
        return res + 1
    def get_Slope(self,points,i,j):
        return (points[i][0] - points[j][0]) / (points[i][1] - points[j][1])
```

但是这样会出现一个问题，即斜率的求算中，有时会出现直线为垂直的情况，故需要对返回的结果进行判断，如果分母为0，则返回inf，如下：

```python
def get_Slope(self,points,i,j):
    if points[i][1] - points[j][1] == 0:
        return float('Inf')
    else:
        return (points[i][0] - points[j][0]) / (points[i][1] - points[j][1])
```

再次提交，发现对于空列表的测试用例会判断错误，于是对边界情况进行判断，如果初始长度小于等于1,则直接返回len：
```python
if len(points) <= 1:
    return len(points)
```

再次提交，对于相同元素的测试用例会出现错误，回想刚才的过程，当有相同元素时，题目的要求是算作两个不同的点，但是在程序运行时，会将其考虑为相同的点，return回了inf。但在实际运行时，需要对相同元素的情况单独考虑。

于是可以设定samepoint值，遍历时判断，如果相同时，same值++,最后取v+same的值作为结果数。

考虑到如果全是相同值，那么这时dict中的record为空，也要将same值当作结果数返回，代码实现如下：

```python
class Solution:
    def maxPoints(self,points):
        if len(points) <= 1:
            return len(points)
        res = 0
        from collections import defaultdict
        for i in range(len(points)):
            record = defaultdict(int)
            samepoint = 0
            for j in range(len(points)):
                if points[i][0] == points[j][0] and points[i][1] == points[j][1]:
                    samepoint += 1
                else:
                    record[self.get_Slope(points,i,j)] += 1
            for v in record.values():
                res = max(res, v+samepoint)
            res = max(res, samepoint)
        return res
    def get_Slope(self,points,i,j):
        if points[i][1] - points[j][1] == 0:
            return float('Inf')
        else:
            return (points[i][0] - points[j][0]) / (points[i][1] - points[j][1])
```

时间复杂度为O(n^2)，空间复杂度为O(n)


# 总结

遍历时多用索引，而不要直接用值进行遍历；



# 三. 滑动数组
# LeetCode 219 Contains Dupliccate Ⅱ

## 题目描述
给出一个整形数组nums和一个整数k，是否存在索引i和j，使得nums[i]==nums[j]，且i和J之间的差不超过k。

```
示例1:
输入: nums = [1,2,3,1], k = 3
输出: true

示例 2:
输入: nums = [1,2,3,1,2,3], k = 2
输出: false
```



## 分析实现

翻译下这个题目：在这个数组中，如果有两个元素索引i和j，它们对应的元素是相等的，且索引j-i是小于等于k，那么就返回True，否则返回False。

因为对于这道题目可以用暴力解法双层循环，即：
```python
for i in range(len(nums)):
    for j in range(i+1,len(nums)):
        if i == j:
            return True
return False
```

故这道题目可以考虑使用滑动数组来解决：

固定滑动数组的长度为K+1，当这个滑动数组内如果能找到两个元素的值相等，就可以保证两个元素的索引的差是小于等于k的。如果当前的滑动数组中没有元素相同，就右移滑动数组的右边界r,同时将左边界l右移。查看r++的元素是否在l右移过后的数组里，如果不在就将其添加数组，在的话返回true表示两元素相等。

因为滑动数组中的元素是不同的，考虑用set作为数据结构：

```python
class Solution:
    def containsNearbyDuplicate(self, nums: List[int], k: int) -> bool:
        record = set()
        for i in range(len(nums)):
            if nums[i] in record:
                return True
            record.add(nums[i])
            if len(record) == k+1:
                record.remove(nums[i-k])
        return False
```
时间复杂度为O(n)，空间复杂度为O(n)


# LeetCode 220 Contains Dupliccate Ⅲ

## 题目描述
给定一个整数数组，判断数组中是否有两个不同的索引 i 和 j，使得nums [i] 和nums [j]的差的绝对值最大为 t，并且 i 和 j 之间的差的绝对值最大为 ķ。

示例 1:

输入: nums = [1,2,3,1], k = 3, t = 0

输出: true

示例 2:

输入: nums = [1,0,1,1], k = 1, t = 2

输出: true

示例 3:

输入: nums = [1,5,9,1,5,9], k = 2, t = 3

输出: false


## 分析实现
相比较上一个问题，这个问题多了一个限定条件，条件不仅索引差限定k，数值差也限定为了t。

将索引的差值固定，于是问题和上道一样，同样转化为了固定长度K+1的滑动窗口内，是否存在两个值的差距不超过 t，考虑使用**滑动窗口**的思想来解决。

在遍历的过程中，目的是要在“已经出现、但还未滑出滑动窗口”的所有数中查找，是否有一个数与滑动数组中的数的**差的绝对值**最大为 t。对于差的绝对值最大为t，实际上等价于所要找的这个元素v的范围是在v-t到v+t之间，即查找“滑动数组”中的元素有没有[v-t，v+t]范围内的数存在。

因为只需证明是否存在即可，这时判断的逻辑是：如果在滑动数组**查找比v-t大的最小的元素**,如果这个元素小于等于v+t,即可以证明存在[v-t,v+t]。

那么实现过程其实和上题是一致的，只是上题中的判断条件是**在查找表中找到和nums[i]相同的元素**，而这题中的判断条件是**查找比v-t大的最小的元素，判断其小于等于v+t**，下面是实现的框架：

```python
class Solution:
    def containsNearbyDuplicate(self, nums: List[int], k: int) -> bool:
        record = set()
        for i in range(len(nums)):
            if 查找的比v-t大的最小的元素 <= v+t:
                return True
            record.add(nums[i])
            if len(record) == k+1:
                record.remove(nums[i-k])
        return False
```
接下来考虑，如何查找比v-t大的最小的元素呢？

【注：C++中有lower_bound(v-t)的实现，py需要自己写函数】

当然首先考虑可以通过O(n)的解法来完成，如下：
```python
def lower_bound(self,array,v):
    array = list(array)
    for i in range(len(array)):
        if array[i] >= v:
            return i
    return -1
```
但是滑动数组作为set，是有序的数组。对于有序的数组，应该第一反应就是**二分查找**，于是考虑二分查找实现，查找比v-t大的最小的元素：
```python
def lower_bound(self, nums, target):
    low, high = 0, len(nums)-1
    while low<high:
        mid = int((low+high)/2)
        if nums[mid] < target:
            low = mid+1
        else:
            high = mid
    return low if nums[low] >= target else -1
```
整体代码实现如下，时间复杂度为O(nlogn),空间复杂度为O(n):

```python
class Solution:
    def containsNearbyAlmostDuplicate(self, nums, k, t) -> bool:
        record = set()
        for i in range(len(nums)):
            if len(record) != 0:
                rec = list(record)
                find_index = self.lower_bound(rec,nums[i]-t)
                if find_index != -1 and rec[find_index] <= nums[i] + t:
                    return True
            record.add(nums[i])
            if len(record) == k + 1:
                record.remove(nums[i - k])
        return False
    def lower_bound(self, nums, target):
        low, high = 0, len(nums)-1
        while low<high:
            mid = int((low+high)/2)
            if nums[mid] < target:
                low = mid+1
            else:
                high = mid
        return low if nums[low] >= target else -1
```

当然。。。在和小伙伴一起刷的时候，这样写的O(n^2)的结果会比上面要高，讨论的原因应该是上面的步骤存在着大量set和list的转换导致，对于py，仍旧是考虑算法思想实现为主，下面是O(n^2)的代码：

```python
class Solution:
    def containsNearbyAlmostDuplicate(self, nums: List[int], k: int, t: int) -> bool:
        if t == 0 and len(nums) == len(set(nums)):
            return False
        for i in range(len(nums)):
            for j in range(1,k+1):
                if i+j >= len(nums): break
                if abs(nums[i+j]-nums[i]) <= t: return True
        return False
```

## 小套路：
二分查找实现，查找比v-t大的最小的元素：

```python
def lower_bound(self, nums, target):
    low, high = 0, len(nums)-1
    while low<high:
        mid = int((low+high)/2)
        if nums[mid] < target:
            low = mid+1
        else:
            high = mid
    return low if nums[low] >= target else -1
```
二分查找实现，查找比v-t大的最小的元素：
```python
def upper_bound(nums, target):
    low, high = 0, len(nums)-1
    while low<high:
        mid=(low+high)/2
        if nums[mid]<=target:
            low = mid+1
        else:#>
            high = mid
            pos = high
    if nums[low]>target:
        pos = low
    return -1
```
# 四. 二分查找
## 理解
查找在算法题中是很常见的，但是怎么最大化查找的效率和写出bugfree的代码才是难的部分。一般查找方法有顺序查找、二分查找和双指针，推荐一开始可以直接用顺序查找，如果遇到TLE的情况再考虑剩下的两种，毕竟AC是最重要的。

一般二分查找的对象是有序或者由有序部分变化的（可能暂时理解不了，看例题即可），但还存在一种可以运用的地方是按值二分查找，之后会介绍。

## 代码模板
总体来说二分查找是比较简单的算法，网上看到的写法也很多，掌握一种就可以了。
以下是我的写法，参考C++标准库里<algorithm>的写法。这种写法比较好的点在于：
 - 1.即使区间为空、答案不存在、有重复元素、搜索开/闭区间的上/下界也同样适用
 - 2.+-1 的位置调整只出现了一次，而且最后返回lo还是hi都是对的，无需纠结

```python
class Solution:
    def firstBadVersion(self, arr):
        # 第一点
        lo, hi = 0, len(arr)-1
        while lo < hi:
            # 第二点
            mid = (lo+hi) // 2
            # 第三点
            if f(x):
                lo = mid + 1
            else:
                hi = mid
        return lo
```

**解释**：
 - 第一点：lo和hi分别对应搜索的上界和下界，但不一定为0和arr最后一个元素的下标。
 - 第二点：因为Python没有溢出，int型不够了会自动改成long int型，所以无需担心。如果再苛求一点，可以把这一行改成
 ```python
 mid = lo + (hi-lo) // 2
 # 之所以 //2 这部分不用位运算 >> 1 是因为会自动优化，效率不会提升
 ```
 - 第三点：
 比较重要的就是这个f(x)，在带入模板的情况下，写对函数就完了。

那么我们一步一步地揭开二分查找的神秘面纱，首先来一道简单的题。

## LeetCode 35. Search Insert Position

给定排序数组和目标值，如果找到目标，则返回索引。如果不是，则返回按顺序插入索引的位置的索引。 您可以假设数组中没有重复项。

**Example**

```
Example 1:
Input: [1,3,5,6], 5
Output: 2

Example 2:
Input: [1,3,5,6], 2
Output: 1

Example 3:
Input: [1,3,5,6], 7
Output: 4

Example 4:
Input: [1,3,5,6], 0
Output: 0
```

**分析：** 这里要注意的点是 high 要设置为 len(nums) 的原因是像第三个例子会超出数组的最大值，所以要让 lo 能到 这个下标。

```python
class Solution:
    def searchInsert(self, nums: List[int], target: int) -> int:        
        lo, hi = 0, len(nums)
        while lo < hi:
            mid = (lo + hi) // 2
            if nums[mid] < target:
                lo = mid + 1
            else:
                hi = mid
        return lo
```

## LeetCode540. Single Element in a Sorted Array

您将获得一个仅由整数组成的排序数组，其中每个元素精确出现两次，但一个元素仅出现一次。 找到只出现一次的单个元素。

**Example**

```
Example 1:

Input: [1,1,2,3,3,4,4,8,8]
Output: 2

Example 2:

Input: [3,3,7,7,10,11,11]
Output: 10
```

**分析：** 异或的巧妙应用！如果mid是偶数，那么和1异或的话，那么得到的是mid+1，如果mid是奇数，得到的是mid-1。如果相等的话，那么唯一的元素还在这之后，往后找就可以了。

```python
class Solution:
    def singleNonDuplicate(self, nums):
        lo, hi = 0, len(nums) - 1
        while lo < hi:
            mid = (lo + hi) // 2
            if nums[mid] == nums[mid ^ 1]:
                lo = mid + 1
            else:
                hi = mid
        return nums[lo]
```

**是不是还挺简单哈哈，那我们来道HARD难度的题!**



## LeetCode 410. Split Array Largest Sum

给定一个由非负整数和整数m组成的数组，您可以将该数组拆分为m个非空连续子数组。编写算法以最小化这m个子数组中的最大和。

**Example**

```
Input:
nums = [7,2,5,10,8]
m = 2

Output:
18

Explanation:
There are four ways to split nums into two subarrays.
The best way is to split it into [7,2,5] and [10,8],
where the largest sum among the two subarrays is only 18.
```

**分析：**
 - 这其实就是二分查找里的按值二分了，可以看出这里的元素就无序了。但是我们的目标是找到一个合适的最小和，换个角度理解我们要找的值在最小值max(nums)和sum(nums)内，而这两个值中间是连续的。是不是有点难理解，那么看代码吧
 - 辅助函数的作用是判断当前的“最小和”的情况下，区间数是多少，来和m判断
 - 这里的下界是数组的最大值是因为如果比最大值小那么一个区间就装不下，数组的上界是数组和因为区间最少是一个，没必要扩大搜索的范围

```python
class Solution:
    def splitArray(self, nums: List[int], m: int) -> int:

        def helper(mid):
            res = tmp = 0
            for num in nums:
                if tmp + num <= mid:
                    tmp += num
                else:
                    res += 1
                    tmp = num
            return res + 1

        lo, hi = max(nums), sum(nums)
        while lo < hi:
            mid = (lo + hi) // 2
            if helper(mid) > m:
                lo = mid + 1
            else:
                hi = mid
        return lo
```