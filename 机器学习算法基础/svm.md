### 学习内容
- SVM 硬间隔原理
- SVM 软间隔
- SMO 求解SVM
- 代码设计


### 1、硬间隔

本文是需要一定基础才可以看懂的，建议先看看参考博客，一些疑惑会在文中直接提出，大家有额外的疑惑可以直接评论，有问题请直接提出，相互交流。
### SVM-统计学习基础
一开始讲解了<code>最小间距超平面：所有样本到平面的距离最小</code>。而距离度量有了函数间隔和几何间隔，函数间隔与法向量$w$和$b$有关，$w$变为$2w$则函数间距变大了，于是提出了几何距离，就是对$w$处理，除以$||w||$，除以向量长度，从而让几何距离不受影响。

但是支持向量机提出了最大间隔分离超平面，这似乎与上面的分析相反，其实这个最大间隔是个什么概念呢？通过公式来分析一下，正常我们假设超平面公式是：
$$
w^{T}x+b=0  // 超平面
$$
$$
\max \limits_{w,b}   \quad  \gamma \\
s.t. \quad y_i(\frac{w}{||w||}x_i+\frac{b}{||w||}) > \gamma
$$
也就是说对于所有的样本到超平面距离 都大于$\gamma$，那这个$\gamma$如何求解，文中约定了概念支持向量：正负样本最近的两个点，这两个点之间的距离就是$\gamma$，那么问题来了，这中间的超平面有无数个，如何确定这个超平面呢？于是我们可以约束这个超平面到两个最近的点的距离是一样的。
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTglQjYlODUlRTUlQjklQjMlRTklOUQlQTIucG5n?x-oss-process=image/format,png)
上图中两个红色菱形点与一个蓝色实心圆点就是支持向量，通过这个求解目标，以及约束条件来求解这个超平面。书中有完整的公式装换以及证明这个超平面的唯一性。

这里要讲解一个样本点到直线的距离，
正常我们可能难以理解公式里$y$去哪里了，拿二维空间做例子，正常我们说一个线性方程都是$y=ax+b$，其中a和b都是常量，这个线性方程中有两个变量$x$和$y$，转换公式就是$y-ax-b=0$，从线性矩阵的角度来思考问题就是 $y$是$x_1$，$x$是$x_2$，用一个$w^T$来表示这两者的系数，用$b$代替$-b$，所以公式就变为了：
$$
w^{T}x+b=0
$$
于是任意一个样本点到超平面的距离是：
$$
r = \frac{|w^{T}x+b|}{||w||}
$$
也就是说约束条件中要求$>\gamma$，其实就是大于支持向量到超平面的距离。

通过一个例子来看看：
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTYlOTQlQUYlRTYlOEMlODElRTUlOTAlOTElRTklODclOEYlRTYlOUMlQkEtJUU0JUJFJThCJUU1JUFEJTkwJUU2JTg4JUFBJUU1JTlCJUJFLnBuZw?x-oss-process=image/format,png)
这里例子中有$w_1,w_2$，这是因为坐标点是二维的，相当于样本特征是两个，分类的结果是这两个特征的结果标签，所以这里的$w$就是一个二维的，说明在具体的应用里需要根据特征来确定$w$的维度。

##### 对偶讲解
其实原始问题是这样的：
$$
\max \limits_{w,b}   \quad  \gamma \\
s.t. \quad y_i(\frac{w}{||w||}x_i+\frac{b}{||w||}) > \gamma
$$
利用几何距离与函数距离的关系$\gamma = \frac{\hat{ \gamma}}{||w||}$将公式改为：
$$
\max \limits_{w,b}   \quad   \frac{\hat{ \gamma}}{||w||} \\
s.t. \quad y_i(wx_i+b) > \hat{\gamma}
$$
函数间隔是会随着$w与b$的变化而变化，同时将$w与b$变成$\lambda w与\lambda b$，则函数间隔也会变成$\lambda  \gamma$，所以书中直接将$\gamma=1$来转换问题。同样的问题又改为：
$$
\max \limits_{w,b}   \quad   \frac{1}{||w||} \\
s.t. \quad y_i(wx_i+b) >1
$$
求解最大值改为另一个问题，求解最小值：
$$
\min   \quad   \frac{1}{2} ||w||^2 \\
s.t. \quad y_i(wx_i+b) >1 
$$
这就是一个对偶问题的例子，也是书中支持向量机模型的一个目标函数转换的过程，大家可以看看了解一下这个思路。其实书中利用拉格朗日乘子来求解条件极值，这一块在<code>高等数学中多元函数的极值及求解方法</code>中有提到。
为了加深记忆，手写了后面的推导过程：
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi9TVk0tJUU2JThFJUE4JUU1JUFGJUJDLmpwZw?x-oss-process=image/format,png)

### 软间隔
硬间隔是方便用来分隔线性可分的数据，如果样本中的数据是线性不可分的呢？也就是如图所示：
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTglQkQlQUYlRTklOTclQjQlRTklOUElOTQtJUU1JTlCJUJFLnBuZw?x-oss-process=image/format,png)
有一部分红色点在绿色点那边，绿色点也有一部分在红色点那边，所以就不满足上述的约束条件：$s.t. \quad y_i(x_i+b) >1$，软间隔的最基本含义同硬间隔比较区别在于允许某些样本点不满足原约束，从直观上来说，也就是“包容”了那些不满足原约束的点。软间隔对约束条件进行改造，迫使某些不满足约束条件的点作为损失函数，如图所示：
![软间隔公式](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTglQkQlQUYlRTklOTclQjQlRTklOUElOTQtJUU1JTg1JUFDJUU1JUJDJThGNy5QTkc?x-oss-process=image/format,png)
这里要区别非线性情况，非线性的意思就是一个圆圈，圆圈里是一个分类结果，圆圈外是一个分类结果。这就是非线性的情况。

其中当样本点不满足约束条件时，损失是有的，但是满足条件的样本都会被置为0，这是因为加入了转换函数，使得求解min的条件会专注在不符合条件的样本节点上。

但截图中的损失函数非凸、非连续，数学性质不好，不易直接求解，我们用其他一些函数来代替它，叫做替代损失函数（surrogate loss）。后面采取了松弛变量的方式，来使得某些样本可以不满足约束条件。
![软间隔公式](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTglQkQlQUYlRTklOTclQjQlRTklOUElOTQtJUU1JUI4JUI4JUU3JTk0JUE4JUU2JThEJTlGJUU1JUE0JUIxJUU1JTg3JUJEJUU2JTk1JUIwLSVFOCVCRiU4NyVFNiVCQiVBNCVFNCVCOCU4RCVFNyVBQyVBNiVFNSU5MCU4OCVFNyVCQSVBNiVFNiU5RCU5RiVFNiU5RCVBMSVFNCVCQiVCNiVFNyU5QSU4NCVFNyU4MiVCOS5QTkc?x-oss-process=image/format,png)
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTUlQkMlOTUlRTUlODUlQTUlRTYlOUQlQkUlRTUlQkMlOUIlRTUlOEYlOTglRTklODclOEYlRTglQkQlQUYlRTklOTclQjQlRTklOUElOTQlRTUlODUlQUMlRTUlQkMlOEYuUE5H?x-oss-process=image/format,png)

这里思考一个问题：既然是线性不可分，难道最后求出来的支持向量就不是直线？某种意义上的直线？
其实还是直线，不满足条件的节点也被错误的分配了，只是尽可能的求解最大间隔，

### 核函数
引入核函数可以解决非线性的情况：<font color="#F00" size = "5px">将样本从原始空间映射到一个更高为的特征空间，使得样本在这个特征空间内线性可分</font>。图片所示：
![SVM-非线性样本可分图.PNG	
](https://raw.githubusercontent.com/Klauszhao/picture/master/picture/common/SVM-%E9%9D%9E%E7%BA%BF%E6%80%A7%E6%A0%B7%E6%9C%AC%E5%8F%AF%E5%88%86%E5%9B%BE.PNG)

粉红色平面就是超平面，椭圆形空间曲面就是映射到高维空间后的样本分布情况，为了将样本转换空间或者映射到高维空间，我们可以引用一个映射函数，将样本点映射后再得到超平面。这个技巧不仅用在SVM中，也可以用到其他统计任务。
但映射函数并不是最重要的，核函数是重要的，看到《统计学习方法》中提到的概念：
![核函数定义](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTYlQTAlQjglRTUlODclQkQlRTYlOTUlQjAlRTUlQUUlOUElRTQlQjklODkucG5n?x-oss-process=image/format,png)

其中映射函数与核函数之间有函数关系，一般我们显示的定义核函数，而不显示的定义映射函数，一方面是因为计算核函数比映射函数简单，我们对一个二维空间做映射，选择的新空间是原始空间的所有一阶和二阶的组合，得到了五个维度；如果原始空间是三维，那么我们会得到 19 维的新空间，这个数目是呈爆炸性增长的，这给 的计算带来了非常大的困难，而且如果遇到无穷维的情况，就根本无从计算了。所以就需要 Kernel 出马了。这样，<font color="#F00" size = "5px">一个确定的核函数，都不能确定特征空间和映射函数，同样确定了一个特征空间，其映射函数也可能是不一样的</font>。举个例子：
![核函数与映射函数](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTYlQTAlQjglRTUlODclQkQlRTYlOTUlQjAlRTQlQjglOEUlRTYlOTglQTAlRTUlQjAlODQlRTUlODclQkQlRTYlOTUlQjAlRTQlQjklOEIlRTklOTclQjQucG5n?x-oss-process=image/format,png)
上述例子很好说明了核函数和映射函数之间的关系。这就是核技巧，将原本需要确定映射函数的问题转换为了另一个问题，从而减少了计算量，也达到了线性可分的目的，
```
原始方法：  样本X   ---->  特征空间Z  ---- >   内积求解超平面
核函数：    样本X   ---- >   核函数 求解超平面
```
但是我一直很疑惑，为什么这个核函数就正好是映射函数的内积？他为什么就可以生效？核函数的参数是哪里来的？就是样本中的两个样本点 ?
查看了一些博客之后，慢慢理解，得到以下的内容。
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi9TVk0lRTYlQTAlQjglRTUlODclQkQlRTYlOTUlQjAlRTclOTAlODYlRTglQTclQTMuanBn?x-oss-process=image/format,png)
这也说明核函数是作用在两个样本点上的，以下来自如某一篇博客里：
> **最理想的情况下，我们希望知道数据的具体形状和分布，从而得到一个刚好可以将数据映射成线性可分的 ϕ(⋅) ，然后通过这个 ϕ(⋅) 得出对应的 κ(⋅,⋅) 进行内积计算。然而，第二步通常是非常困难甚至完全没法做的。不过，由于第一步也是几乎无法做到，因为对于任意的数据分析其形状找到合适的映射本身就不是什么容易的事情，所以，人们通常都是“胡乱”选择映射的，所以，根本没有必要精确地找出对应于映射的那个核函数，而只需要“胡乱”选择一个核函数即可——我们知道它对应了某个映射，虽然我们不知道这个映射具体是什么。由于我们的计算只需要核函数即可，所以我们也并不关心也没有必要求出所对应的映射的具体形式。**

#### 常用的核函数及对比：
- Linear Kernel 线性核
   $$
   k(x_i,x_j)=x_i^{T}x_j
   $$
  线性核函数是最简单的核函数，主要用于线性可分，它在原始空间中寻找最优线性分类器，具有参数少速度快的优势。 如果我们将线性核函数应用在KPCA中，我们会发现，推导之后和原始PCA算法一模一样，这只是线性核函数偶尔会出现等价的形式罢了。
- Polynomial Kernel 多项式核
      $$
   k(x_i,y_j)=(x_i^{T}x_j)^d
   $$
也有复杂的形式：
      $$
   k(x_i,x_j)=(ax_i^{T}x_j+b)^d
   $$
其中$d\ge1$为多项式次数，参数就变多了，多项式核实一种非标准核函数，它非常适合于正交归一化后的数据，多项式核函数属于全局核函数，可以实现低维的输入空间映射到高维的特征空间。其中参数d越大，映射的维度越高，和矩阵的元素值越大。故易出现过拟合现象。

- 径向基函数 高斯核函数 Radial Basis Function（RBF）

$$
   k(x_i,x_j)=exp(-\frac{||x_i-x_j||^2}{2\sigma^2})
   $$

$\sigma>0$是高斯核带宽，这是一种经典的鲁棒径向基核，即高斯核函数，鲁棒径向基核对于数据中的噪音有着较好的抗干扰能力，其参数决定了函数作用范围，超过了这个范围，数据的作用就“基本消失”。高斯核函数是这一族核函数的优秀代表，也是必须尝试的核函数。对于大样本和小样本都具有比较好的性能，因此在多数情况下不知道使用什么核函数，优先选择径向基核函数。

- Laplacian Kernel 拉普拉斯核
$$
   k(x_i,x_j)=exp(-\frac{||x_i-x_j||}{\sigma})
   $$

- Sigmoid Kernel Sigmoid核
$$
   k(x_i,x_j)=tanh(\alpha x^Tx_j+c)
   $$
采用Sigmoid核函数，支持向量机实现的就是一种多层感知器神经网络。


其实还有很多核函数，在参考博客里大家都可以看到这些核函数，对于核函数如何选择的问题，吴恩达教授是这么说的：
- 如果Feature的数量很大，跟样本数量差不多，这时候选用LR或者是Linear Kernel的SVM
- 如果Feature的数量比较小，样本数量一般，不算大也不算小，选用SVM+Gaussian Kernel
- 如果Feature的数量比较小，而样本数量很多，需要手工添加一些feature变成第一种情况






### 2、软间隔


###  前言
之前写的一偏文章主要是[SVM的硬间隔](https://blog.csdn.net/randompeople/article/details/90020648)，结合[SVM拉格朗日对偶问题](https://blog.csdn.net/randompeople/article/details/92083294)可以求解得到空间最大超平面，但是如果样本中与较多的异常点，可能对样本较敏感，不利于模型泛化，于是有了软间隔的支持向量机形式，本文来了解一下此问题。

### 软间隔最大化
引入松弛变量，使得一部分异常数据也可以满足约束条件：$y_i(x_i+b) >=1 - \varepsilon_i$，既然约束条件引入了松弛变量，那么点到超平面的距离是不是也要改变，于是调整为：
$$
\min   \quad   \frac{1}{2} ||w||^2+C\sum_{i}^{N}\varepsilon_i \\
s.t. \quad y_i(x_i+b) \ge 1 - \varepsilon_i \qquad  \text{i=1,2...,n}\\
\varepsilon_i  \ge 0
$$
- C：表示惩罚因子，这个值大小表示对误分类数据集的惩罚，调和最大间距和误分类点个数之间的关系。
- $\varepsilon_i$：也作为代价。

这也是一个凸二次规划问题，可以求解得到$w$，但b的求解是一个区间范围，让我们来看看是怎么回事，求解流程跟硬间隔没差别，直接得到拉格朗日对偶问题：


$$
\max_{a_i>0,\mu>0} \min_{w_i,b,\varepsilon}   \quad   L(w,b,\varepsilon,a,\mu)= \frac{1}{2} ||w||^2+C\sum_{i}^{N}\varepsilon_i+\sum_{i=1}^{N}a_{i}[1-y_i(wx_i+b)+\varepsilon_i]+\sum_{i}^{N} \mu_i \varepsilon_i
$$
继续按照流程走：
- 对w、b、$\varepsilon$ 求偏导，让偏导等于0，结果为：
$$
w = \sum_{i}a_iy_ix_i \\
\sum_{i}a_iy_i = 0 \\
C-a_i-u_i =0
$$
- 代入上面的方程得到：

$$
\max_{a_i>0,\mu>0} \quad  L(w,b,\varepsilon,a,\mu) =  -\frac{1}{2}\sum_{i} \sum_{j}a_{i}a_{j}y_{i}y_{j}(x_i * x_j) + \sum_{i}a_i \\
s.t. \quad  \sum_{i}^{N}a_iy_i=0 \\
\quad  0\le a_i\le C
$$
去掉符号，将max 转换为 min ：
$$
\min_{a_i>0,\mu>0} \quad  L(w,b,\varepsilon,a,\mu) =  \frac{1}{2}\sum_{i} \sum_{j}a_{i}a_{j}y_{i}y_{j}(x_i * x_j) - \sum_{i}a_i \\
s.t. \quad  \sum_{i}^{N}a_iy_i=0 \\
\quad  0\le a_i\le C
$$
这里代入之后就只有一个因子$a_i$，对此方程求解$a_i$
- w、b:
$$
w = \sum_{i}a_iy_ix_i \\
$$
b的计算就需要思考了，选取满足$\quad  0\le a_i\le C$的$a_i$，利用这些点来求解b：
$$
b = y_j-\sum_{i}a_iy_i(x_i*x_j)
$$
当然符合这个条件的也不只有一个，存在多个条件。求解平均值作为一个唯一值。

- 超平面
$$
y = wx+b
$$

和上一篇的硬间隔最大化的线性可分SVM相比，多了一个约束条件：$0\le a_i \le C$。




### 3、SMO求解SVM



### 4、代码实现


### 参考博客
[统计学习基础]()
[支持向量机SVM：原理讲解+手写公式推导+疑惑分析](https://blog.csdn.net/randompeople/article/details/90020648)
[支持向量机 - 软间隔最大化](https://blog.csdn.net/randompeople/article/details/104031825)
