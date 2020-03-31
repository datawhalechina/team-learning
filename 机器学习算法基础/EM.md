### 前言
EM算法是机器学习十大算法之一，它很简单，但是也同样很有深度，简单是因为它就分两步求解问题，
- E步：求期望（expectation）
- M步：求极大（maximization)

深度在于它的数学推理涉及到比较繁杂的概率公式等，所以本文会介绍很多概率方面的知识，不懂的同学可以先去了解一些知识，当然本文也会尽可能的讲解清楚这些知识，讲的不好的地方麻烦大家评论指出，后续不断改进完善。

### EM算法引入
概率模型有时候既含有观测变量，又含有隐变量或潜在变量，如果概率模型的变量都是观测变量，那么给定数据，可以直接用极大似然估计法，或贝叶斯估计方法估计模型参数，但是<font color="#F00" size = "4px">当模型含有隐变量时，就不能简单的使用这些方法，EM算法就是含有隐变量的概率模型参数的极大似然估计法，或极大后验概率估计法，</font>我们讨论极大似然估计，极大后验概率估计与其类似。
参考统计学习方法书中的一个例子来引入EM算法， 假设有3枚硬币，分别记做A、B、C，这些硬币正面出现的概率分别是$\pi$、$p$、$q$，进行如下实验：

- 先掷硬币A，根据结果选出硬币B和硬币C，正面选硬币B，反面选硬币C
- 通过选择出的硬币，掷硬币的结果出现正面为1，反面为0
如此独立地重复n次实验，我们当前规定n=10，则10次的结果如下所示：
$$
1,1,0,1,0,0,1,0,1,1
$$
假设只通过观测到掷硬币的结果，不能观测掷硬币的过程，问如何估计三个硬币出现正面的概率？
我们来构建这样一个三硬币模型：
$$
\begin{aligned}
P(y|\theta) &=\sum_{z}P(y,z|\theta)=\sum_{z}P(z|\theta)P(y|z,\theta) \\
  &=\pi p^{y}(1-p)^{1-y}+(1-\pi)q^{y}(1-q)^{1-y}
\end{aligned}
$$

- 若$y=1$，表示这此看到的是正面，这个正面有可能是B的正面，也可能是C的正面，则$P(1|\theta)=\pi p+(1-\pi)q$
- 若$y=0$，则$P(0|\theta)=\pi (1-p)+(1-\pi)(1-q)$

y是观测变量，表示一次观测结果是1或0，z是隐藏变量，表示掷硬币A的结果，这个是观测不到结果的，$\theta=(\pi,p,q)$表示模型参数，将观测数据表示为$Y=(Y_1,Y_2,...,Y_n)^{T}$，未观测的数据表示为$Z=(Z_1,Z_2,...,Z_n)^{T}$，则观测函数的似然函数是：
$$
\begin{aligned}
P(Y|\theta)&=\sum_{Z}P(Z|\theta)P(Y|Z,\theta)\\
&=\prod_{i=0} ( \pi p^{y_i}(1-p)^{1-y_{i}}+(1-\pi)q^{y_{i}}(1-q)^{1-y_{i}})
\end{aligned}
$$
考虑求模型参数$\theta=(\pi,p,q)$的极大似然估计，即：
$$
\hat{\theta}=arg\max_{\theta}logP(Y|\theta)
$$
这个问题没有解析解，只有通过迭代方法来求解，EM算法就是可以用于求解这个问题的一种迭代算法，下面给出EM算法的迭代过程：
- 首先选取初始值，记做$\theta^{0}=(\pi^{0},p^{0},q^{0})$，第i次的迭代参数的估计值为$\theta^{i}=(\pi^{i},p^{i},q^{i})$
- E步：计算在模型参数$\pi^{i}，p^{i}，q^{i}$下观测变量$y_i$来源于硬币B的概率：
$$
\mu^{i+1}=\frac{\pi^{i}(p^{i})^{y_i}(1-p^i)^{1-y_i}}{\pi^{i}(p^{i})^{y_i}(1-p^i)^{1-y_i}+(1-\pi^{i})(q^{i})^{y_i}(1-p^i)^{1-y_i}}
$$
备注一下：这个公式的分母是$P(Y|\theta)$，分子表示是来源与B硬币的概率。

- M步：计算模型参数的新估计值：
$$
\pi^{i+1}=\frac{1}{n}\sum_{j=1}^{n}\mu_{j}^{i+1} 
$$
因为B硬币A硬币出现正面的结果，所以A硬币概率就是$\mu_{j}$的平均值。
$$
p^{i+1}=\frac{\sum_{j=1}^{n}\mu_{j}^{i+1}y_j}{\sum_{j=1}^{n}\mu_{j}^{i+1}}
$$
分子乘以$y_{i}$，所以其实是计算B硬币出现正面的概率。
$$
q^{i+1}=\frac{\sum_{j=1}^{n}(1-\mu_{j}^{i+1})y_j}{\sum_{j=1}^{n}(1-\mu_{j}^{i+1})}
$$
$(1-\mu_{j}^{i+1})$表示出现C硬币的概率。

闭环形成，从$P(Y|\theta)$ 到 $\pi、p、q$一个闭环流程，接下来可以通过迭代法来做完成。针对上述例子，我们假设初始值为$\pi^{0}=0.5，p^{0}=0.5，q^{0}=0.5$，因为对$y_i=1$和$y_i=0$均有$\mu_j^{1}=0.5$，利用迭代公式计算得到$\pi^{1}=0.5，p^{1}=0.6，q^{1}=0.6$，继续迭代得到最终的参数：
$$\widehat{\pi^{0}}=0.5，\widehat{p^{0}}=0.6，\widehat{q^{0}}=0.6$$
如果一开始初始值选择为：$\pi^{0}=0.4，p^{0}=0.6，q^{0}=0.7$，那么得到的模型参数的极大似然估计是$\widehat{\pi}=0.4064，\widehat{p}=0.5368，\widehat{q}=0.6432$，这说明EM算法与初值的选择有关，选择不同的初值可能得到不同的参数估计值。

这个例子中你只观察到了硬币抛完的结果，并不了解A硬币抛完之后，是选择了B硬币抛还是C硬币抛，这时候概率模型就存在着隐含变量！
### EM算法 
输入：观测变量数据Y，隐变量数据Z，联合分布$P(Y,Z|\theta)$，条件分布$P(Z|Y,\theta)$；
输出：模型参数$\theta$
- (1)选择参数的初值$\theta^0$，开始迭代
- (2) E步：记$\theta^i$为第i次迭代参数$\theta$的估计值，在第i+1次迭代的E步，计算
$$
\begin{aligned}
Q(\theta,\theta^i)&=E_{Z}[logP(Y,Z|\theta)|Y,\theta^i]\\
&=\sum_{Z}logP(Y,Z|\theta)P(Z|Y,\theta^i)
\end{aligned}
$$
这里，$P(Z|Y,\theta^i)$是在给定观测数据Y和当前的参数估计$\theta^i$下隐变量数据Z的条件概率分布；

- (3) M步：求使$Q(\theta,\theta^i)$极大化的$\theta$，确定第i+1次迭代的参数的估计值$\theta^{i+1}$，
$$
\theta^{i+1}=arg \max \limits_{\theta}Q(\theta,\theta^{i})
$$
$Q(\theta,\theta^{i})$是EM算法的核心，称为Q函数(Q function)，这个是需要自己构造的。
- (4) 重复第(2)步和第(3)步，直到收敛，收敛条件：
$$
|| \theta^{i+1}-\theta^{i} || < \varepsilon_1
$$
或者：
$$
||Q(\theta^{i+1},\theta^{i})-Q(\theta^{i},\theta^{i})|| <\varepsilon_2
$$
收敛迭代就结束了。我们来拆解一下这个M步骤，

### 推导逼近
主要讲解Jensen不等式，这个公式在推导和收敛都用到，主要是如下的结论：
- $f(x)$是凸函数
$$
f(E(X)) \le E(f(x))
$$
- $f(x)$是凹函数
$$
f(E(X)) \ge E(f(x))
$$

推导出Em算法可以近似实现对观测数据的极大似然估计的办法是找到E步骤的下界，让下届最大，通过逼近的方式实现对观测数据的最大似然估计。统计学习基础中采用的是相减方式，我们来看下具体的步骤。
- 增加隐藏变量
$$
L(\theta)=\sum_{Z}logP(Y|Z,\theta)P(Z,\theta)
$$
则$L(\theta)-L(\theta^{i})$为：
$$
\begin{aligned}
L(\theta)-L(\theta^{i})=log(\sum_{Z} P(Y|Z,\theta^i)\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i)})-L(\theta^{i})\\
\ge \sum_{Z} P(Y|Z,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i)})-L(\theta^{i})
\end{aligned}
$$
$\ge$这一个步骤就是采用了凹函数的Jensen不等式做转换。因为$Z$是隐藏变量，所以有$\sum_{Z} P(Y|Z,\theta^i)==1，P(Y|Z,\theta^i)>0$，于是继续变：

$$
\begin{aligned}
L(\theta)-L(\theta^{i})&=log(\sum_{Z} P(Y|Z,\theta^i)\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i)})-L(\theta^{i})\\
&\ge \sum_{Z} P(Z|Y,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Z|Y,\theta^i)})-L(\theta^{i})\\
&=\sum_{Z} P(Z|Y,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Z|Y,\theta^i)})-\sum_{Z} P(Z|Y,\theta^i)L(\theta^{i})\\
&= \sum_{Z} P(Z|Y,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Z|Y,\theta^i) (P(Y|\theta^{i})}) \\
& \ge0
\end{aligned}
$$
也就是：$L(\theta)\ge L(\theta^{i})+ \sum_{Z} P(Z|Y,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i) L(\theta^{i})})$，有下界，最大化下界，来得到近似值。这里有一个细节：$P(Y|Z,\theta^i)$ 变为$P(Z|Y,\theta^i)$？如果要满足Jensen不等式的等号，则有：
$$
\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i)} = c
$$
c为一个常数，而$\sum_{Z}P(Y|Z,\theta^i)=1$则：
$$
\begin{aligned}
\sum_{Z}P(Y|Z,\theta)P(Z,\theta)= c\sum_{Z}P(Y|Z,\theta^i)&=c\\
&=\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Y|Z,\theta^i)}\\
P(Y|Z,\theta)=\frac{P(Y|Z,\theta)P(Z,\theta)}{\sum_{Z}P(Y|Z,\theta)P(Z,\theta)}=\frac{P(Y,Z,\theta)}{P(Y,\theta)}=P(Z|Y,\theta)
\end{aligned}
$$

大家是不是很奇怪$P(Y|Z,\theta)P(Z,\theta)$加上$\sum$之后等于什么，其实有的博客这里使用$P(Z,\theta) = P(Y^i,Z^i,\theta^i)$来替代$P(Y|Z,\theta)$参与计算，这样$\sum_{Z}P(Y^i,Z^i,\theta^i)$，这样就方便理解来了。

于是最大化如下：

$$
\begin{aligned}
\theta^{i+1}&=arg \max_{\theta}\sum_{Z} P(Z|Y,\theta^i)log(\frac{P(Y|Z,\theta)P(Z,\theta)}{P(Z|Y,\theta^i)})\\
&=arg \max_{\theta}\sum_{Z} P(Z|Y,\theta^i)log(P(Y|Z,\theta)P(Z,\theta))\\
& =arg \max_{\theta}\sum_{Z} P(Z|Y,\theta^i)log(P(Y,Z|\theta))\\ 
&=arg \max_{\theta}Q(\theta,\theta^i)
\end{aligned} 
$$
其中$log$分母提出来是关于$Z$的$\sum_{Z} P(Z|Y,\theta^i)logP(Z|Y,\theta^i)$，可以去掉。当然也有博客写的形式是：
$$
arg \max_{\theta}\sum_{i=1}^{M}\sum_{Z^{i}} P(Z^{i}|Y^{i},\theta^i)log(P(Y^{i},Z^{i};\theta))\\ 
$$
形式其实一样，表示的不一样而已。
### 证明收敛
我们知道已知观测数据的似然函数是$P(Y,\theta)$，对数似然函数为：
$$
\begin{aligned}
L()=\sum_{i=1}^{M}logP(y^{i},\theta) &=\sum_{i=1}^{M}log(\frac{P(y^i,Z|\theta)}{P(Z|y^i,\theta)})\\
&=\sum_{i=1}^{M}logP(y^i,Z|\theta) - \sum_{i=1}^{M}logP(Z|y^i,\theta)
\end{aligned} 
$$
要证明收敛，就证明单调递增，$\sum_{i=1}^{M}logP(y^{i},\theta^{j+1})>\sum_{i=1}^{M}logP(y^{i},\theta^{j})$
由上文知道：
$$
\begin{aligned}
Q(\theta,\theta^i)&=\sum_{Z}logP(Y,Z|\theta)P(Z|Y,\theta^i)\\
&=\sum_{i=1}^{M}\sum_{Z^j}logP(y^i,Z^j|\theta)P(Z^j|y^i,\theta^i)
\end{aligned}
$$
我们构造一个函数$H$，让他等于：
$$
H(\theta,\theta^{i})=\sum_{i=1}^{M}\sum_{Z^j}log(P(Z|y^i,\theta)P(Z|y^i,\theta^i))
$$
让$Q(\theta,\theta^i)-H(\theta,\theta^{i})$：
$$
\begin{aligned}
Q(\theta,\theta^i)-H(\theta,\theta^{i})&=\sum_{i=1}^{M}\sum_{Z^j}logP(y^i,Z^j|\theta)P(Z^j|y^i,\theta^i) - \sum_{i=1}^{M}\sum_{Z^j}log(P(Z^j|y^i,\theta)P(Z^j|y^i,\theta^i)) \\
&=\sum_{i=1}^{M}\sum_{Z^j}log\bigg(P(y^i,Z^j|\theta)-P(Z^j|y^i,\theta)\bigg) \\
&=\sum_{i=1}^{M}logP(y^{i},\theta) 
\end{aligned}
$$所以：
$$
\sum_{i=1}^{M}logP(y^{i},\theta^{j+1})-\sum_{i=1}^{M}logP(y^{i},\theta^{j}) \\
= Q(\theta^{i+1},\theta^i)-H(\theta^{i+1},\theta^{i}) - (Q(\theta^{i},\theta^{i})-H(\theta^{i},\theta^{i}))\\
= Q(\theta^{i+1},\theta^i)- Q(\theta^{i},\theta^{i}) -( H(\theta^{i+1},\theta^{i}) - H(\theta^{i},\theta^{i}))
$$
该公式左边已经被证明是大于0，证明右边：$H(\theta^{i+1},\theta^{i}) - H(\theta^{i},\theta^{i})<0$：
$$
\begin{aligned}
H(\theta^{i+1},\theta^{i}) - H(\theta^{i},\theta^{i}) &=\sum_{Z^j}\bigg(log(\frac{P(Z^j|Y,\theta^{i+1})}{P(Z^j|Y,\theta^i)}) \bigg)P(Z^j|Y,\theta^i) \\
&=log\bigg(\sum_{Z^j}\frac{P(Z^j|Y,\theta^{i+1})}{P(Z^j|Y,\theta^i)}P(Z^j|Y,\theta^i) \bigg)\\
&=logP(Z|Y,\theta^{i+1})=log1=0
\end{aligned}
$$
其中不等式是由于Jensen不等式，由此证明了$\sum_{i=1}^{M}logP(y^{i},\theta^{j+1})>\sum_{i=1}^{M}logP(y^{i},\theta^{j})$，证明了EM算法的收敛性。但不能保证是全局最优，只能保证局部最优。
### 高斯混合分布
EM算法的一个重要应用场景就是高斯混合模型的参数估计。高斯混合模型就是由多个高斯模型组合在一起的混合模型（可以理解为多个高斯分布函数的线性组合，理论上高斯混合模型是可以拟合任意类型的分布），例如对于下图中的数据集如果用一个高斯模型来描述的话显然是不合理的：
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTklQUIlOTglRTYlOTYlQUYlRTYlQjclQjclRTUlOTAlODglRTUlOEQlOTUlRTUlODglODYlRTUlQjglODMucG5n?x-oss-process=image/format,png)

两个高斯模型可以拟合数据集，如图所示：
![](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9yYXcuZ2l0aHVidXNlcmNvbnRlbnQuY29tL0tsYXVzemhhby9waWN0dXJlL21hc3Rlci9waWN0dXJlL2NvbW1vbi8lRTklQUIlOTglRTYlOTYlQUYlRTYlQjclQjclRTUlOTAlODglRTUlODglODYlRTUlQjglODMtJUU1JThGJThDJUU1JTg4JTg2JUU1JUI4JTgzLnBuZw?x-oss-process=image/format,png)

如果有多个高斯模型，公式表示为：
$$
P(y|\theta)=\sum_{k=1}^{K}a_k\phi(y|\theta_{k}) \\
\phi(y|\theta_{k})=\frac{1}{\sqrt{2\pi}\delta_{k}}exp(-\frac{(y-\mu_{k})^2}{2 \delta_{k}^{2}}) \\
a_k>0,\sum a_k =1
$$
$\phi(y|\theta_{k})$表示为第k个高斯分布密度模型，定义如上，其中$a_k$表示被选中的概率。在本次模型$P(y|\theta)$中，观测数据是已知的，而观测数据具体来自哪个模型是未知的，有点像之前提过的三硬币模型，我们来对比一下，A硬币就像是概率$a_k$，用来表明具体的模型，而B、C硬币就是具体的模型，只不过这里有很多个模型，不仅仅是B、C这两个模型。我们用$\gamma_{jk}$来表示，则：
$$
\gamma_{jk} =
\begin{cases}
1& \text{第j个观测数据来源于第k个模型}\\
0& \text{否则}
\end{cases}
$$
所以一个观测数据$y_j$的隐藏数据$(\gamma_{j1},\gamma_{j2},...,\gamma_{jk})$，那么完全似然函数就是：

$$
P(y,\gamma|\theta)= \prod_{k=1}^{K}\prod_{j=1}^{N}[a_{k}\phi(y|\theta_{k})]^{\gamma_{jk}}
$$

取对数之后等于：

$$
\begin{aligned}
log(P(y,\gamma|\theta))&=log( \prod_{k=1}^{K}\prod_{j=1}^{N}[a_{k}\phi(y|\theta_{k})]^{\gamma_{jk}})\\
&=\sum_{K}^{k=1}\bigg(\sum_{j=1}^{N}(\gamma_{jk}) log(a_k)+\sum_{j=1}^{N}( \gamma_{jk})\bigg[log(\frac{1}{\sqrt{2\pi}})-log(\delta_{k})-\frac{(y_i-\mu_{k})^2}{2 \delta_{k}^{2}}\bigg]\bigg)
\end{aligned}
$$

- E 步 ：
$$
\begin{aligned}
Q(\theta.\theta^i) &= E[log(P(y,\gamma|\theta))]\\
&=\sum_{K}^{k=1}\bigg(\sum_{j=1}^{N}(E\gamma_{jk}) log(a_k)+\sum_{j=1}^{N}(E\gamma_{jk})\bigg[log(\frac{1}{\sqrt{2\pi}})-log(\delta_{k})-\frac{(y_i-\mu_{k})^2}{2 \delta_{k}^{2}}\bigg]\bigg)
\end{aligned}
$$
其中我们定义$\hat{\gamma_{jk}}$：
$$
\hat{\gamma_{jk}} = E(\gamma_{jk}|y,\theta)=\frac{a_k\phi(y_i|\theta_{k})}{\sum_{k=1}^{K}a_k\phi(y_i|\theta_{k}) }\\
j=1,2,..,N；k=1,2,...,K\\
n_k=\sum_{j=i}^{N}E\gamma_{jk}
$$
于是化简得到：
$$
\begin{aligned}
Q(\theta.\theta^i) &= \sum_{K}^{k=1}\bigg(n_k log(a_k)+\sum_{j=1}^{N}(E\gamma_{jk})\bigg[log(\frac{1}{\sqrt{2\pi}})-log(\delta_{k})-\frac{(y_i-\mu_{k})^2}{2 \delta_{k}^{2}}\bigg]\bigg)
\end{aligned}
$$

E 步 在代码设计上只有$\hat{\gamma_{jk}}$有用，用于M步的计算。


- M步，
$$
\theta^{i+1}=arg \max_{\theta}Q(\theta,\theta^i)
$$
对$Q(\theta,\theta^i)$求导，得到每个未知量的偏导，使其偏导等于0，求解得到：
$$
\hat{\mu_k}=\frac{\sum_{j=1}^{N}\hat{\gamma_{jk}}y_i}{\sum_{j=1}^{N}\hat{\gamma_{jk}}}
\\
\\
\hat{\delta_k}=\frac{\sum_{j=1}^{N}\hat{\gamma_{jk}}(y_i-\mu_k)^2}{\sum_{j=1}^{N}\hat{\gamma_{jk}}}
\\
\\
\\
\hat{a_k}=\frac{\sum_{j=1}^{N}\hat{\gamma_{jk}} }{N}
$$
给一个初始值，来回迭代就可以求得值内容。这一块主要用到了$Q(\theta.\theta^i)$的导数，并且用到了E步的$\hat{\gamma_{jk}}$。

### 总结
这里其实还有很多问题没讲，这一块概念突然不太想学了，有点任性的我就不继续了，大家想了解的可以去学习统计学习方法这本书，讲解的还是挺全的，可能之后我也会继续更新，哈哈。

### 参考博客
[统计学习基础]()
[EM算法 - 期望极大算法](https://blog.csdn.net/randompeople/article/details/93711747)
