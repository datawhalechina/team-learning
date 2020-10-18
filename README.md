
# Datawhale 组队学习



第18期 Datawhale 组队学习活动马上就要开始啦！

本次组队学习的内容为：

- [推荐系统基础](https://github.com/datawhalechina/team-learning-rs/tree/master/RecommendationSystemFundamentals)
- [编程实践（Numpy）上](https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy)
- [深度强化学习基础](https://github.com/datawhalechina/leedeeprl-notes)


大家可以根据我们的开源内容进行自学，也可以加入我们的组队学习一起来学。



---
# 推荐系统基础

开源内容：https://github.com/datawhalechina/team-learning-rs/tree/master/RecommendationSystemFundamentals

## 基本信息
- 贡献人员：何世福、罗如意、梁家晖、徐何军、陈锴、吴忠强
- 学习周期：12天
- 学习形式：理论学习 + 练习
- 人群定位：对机器学习有一定的了解，会使用常见的数据分析工具（Numpy，Pandas），了解向量检索工具（faiss）的学习者。
- 先修内容：[Python编程语言](https://github.com/datawhalechina/team-learning-program/tree/master/PythonLanguage)；[编程实践（Pandas）](https://github.com/datawhalechina/joyful-pandas)；[编程实践（Numpy）](https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy)。
- 难度系数：中


## 学习目标

本次课程是由Datawhale推荐系统小组内部成员共同完成，是针对推荐系统小白的一入门课程。学习本课程需要学习者对机器学习有一定的了解，会使用常见的数据分析工具（Numpy，Pandas），了解向量检索工具（faiss）。

本次课程内容的设计参考了项亮老师的《推荐系统实践》、王喆老师的《深度学习推荐系统》以及大量的技术博客，选择了在推荐系统算法发展中比较重要的几个算法作为本次课程的核心内容，对于每个算法都进行了细致的分析以及必要的代码的演示，便于学习者们深刻理解推荐算法的本质。除此之外，每个算法都会在一个完整的数据集上从头到尾的重新把算法实现一遍，以便于学习者们可以快速的使用这些算法。在这些完整的代码中，我们给出了详细的代码注释，尽量让学习者们不会因为看不懂代码而感到烦恼。

**传统推荐系统及深度学习推荐系统的演化关系图（图来自《深度学习推荐系统》）**

传统推荐系统：

![](http://ryluo.oss-cn-chengdu.aliyuncs.com/Javaimage-20200923143443499.png)

深度学习推荐系统:

![](http://ryluo.oss-cn-chengdu.aliyuncs.com/Javaimage-20200923143559968.png)


**本开源内容的目标是掌握以下算法：**
- 协同过滤算法
- 矩阵分解算法
- FM（Factorization Machines）算法
- Wide&Deep
- GBDT+LR


**推荐系统组队学习内容汇总：**

![](https://img-blog.csdnimg.cn/20201011094520518.png)


## 任务安排

### Task01：推荐系统简介（1天）

了解推荐系统常用的评测指标、召回的策略和作用等。


### Task02：协同过滤（3天）

掌握协同过滤算法，包括基于用户的协同过滤（UserCF）和基于商品的协同过滤（ItemCF），这是入门推荐系统的人必看的内容，因为这些算法可以让初学者更加容易的理解推荐算法的思想。


### Task03：矩阵分解和FM（3天）

掌握矩阵分解和FM算法。

矩阵分解算法通过引入了隐向量的概念，加强了模型处理稀疏矩阵的能力，也为后续深度学习推荐系统算法中Embedding的使用打下了基础。

FM（Factorization Machines）算法属于对逻辑回归（LR）算法应用在推荐系统上的一个改进，在LR模型的基础上加上了特征交叉项，该思想不仅在传统的推荐算法中继续使用过，在深度学习推荐算法中也对其进行了改进与应用。

### Task04：Wide&Deep（2天）

从深度学习推荐系统的演化图中可以看出Wide&Deep模型处在最中间的位置，可以看出该模型在推荐系统发展中的重要地位，此外该算法模型的思想与实现都比较的简单，非常适合初学深度学习推荐系统的学习者们去学习。

### Task05：GBDT+LR（3天）

该模型仍然是对LR模型的改进，使用树模型做特征交叉，相比于FM的二阶特征交叉，树模型可以对特征进行深度的特征交叉，充分利用了特征之间的相关性。


---
# 编程实践（Numpy）上

开源学习内容：https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy



## 基本信息
- 贡献人员：韩绘锦、左秉文、王彦淳
- 学习周期：13天，每天平均花费时间3小时-5小时不等，根据个人学习接受能力强弱有所浮动。
- 学习形式：理论学习 + 练习
- 人群定位：有一定python编程的基础。
- 先修内容：[Python编程语言](https://github.com/datawhalechina/team-learning-program/tree/master/PythonLanguage)
- 难度系数：中




## 学习目标

本开源内容是Python基础的进阶，主要目标是学习numpy的基本数据类型，了解numpy各类函数的应用；以便为后期学习pandas和sklearn奠定坚实基础。

## 任务安排


### Task01：数据类型及数组创建（2天）
- 熟悉基础常量、常见数据类型，以及时间日期和时间增量的处理。
- 掌握数组的创建和数组的属性。


### Task02：索引（3天）

- 掌握数组的索引与切片，熟悉数组迭代。


### Task03：数组的操作（2天）
- 掌握数组的各种操作，比如：更改形状，数组转置，更改维度，数组组合，数组拆分，数组平铺，添加和删除元素等。


### Task04：数学函数及逻辑函数（3天）

- 掌握numpy中常用的数学函数及逻辑函数。
- 数学函数，比如：数学运算，三角函数，指数和对数，加法函数及乘法函数，四舍五入等。
- 逻辑函数，比如：真值测试，数组内容，逻辑函数等。

### Task05：排序搜索计数及集合操作（3天）
- 掌握numpy中排序搜索计数的相关函数。
- 掌握numpy中关于集合的操作，比如：如何构建集合，集合的交并差集及异或操作等。



---
# 深度强化学习基础

开源内容：https://github.com/datawhalechina/leedeeprl-notes


## 基本信息
- 贡献人员：王琦、杨毅远、江季
- 学习周期：15天
- 学习形式：理论学习 + 练习 + 项目 
- 人群定位：有一定机器学习的基础
- 先修内容：[机器学习算法基础](https://github.com/datawhalechina/team-learning-data-mining/tree/master/MachineLearningFundamentals)
- 难度系数：中


## 学习目标

本开源内容是深度强化学习的基础，主要目标是学习常见的强化学习算法及其应用。

## 任务安排

### Task1：强化学习基础（2天）
- 了解强化学习的基本概念；
- 掌握 Gym 的使用；
- 对应教程的第一章。


### Task2：马尔可夫决策过程及表格型方法（3天）

- 了解马尔可夫过程、马尔可夫奖励过程、马尔可夫决策过程；
- 掌握 Sarsa 和 Q-learning 算法；
- 对应教程的第二章和第三章。


### Task3：策略梯度及 PPO 算法（3天）
- 掌握策略梯度算法及其实现常用的 tips；
- 掌握 PPO 算法、on-policy、off-policy 和重要性采样；
- 对应教程的第四章和第五章。


### Task4：DQN 算法及 Actor-Critic 算法（3天）

- 掌握 DQN 算法、DQN 的进阶技巧以及针对连续动作的 DQN；
- 掌握 Actor-Critic 算法；
- 对应教程的第六章到第九章。

### Task5：稀疏奖励及模仿学习（2天)
- 掌握稀疏奖励的解决方法，比如：Reward Shaping、Curriculum Learning 等；
- 掌握模仿学习的常见方法，比如 Behavior Cloning、Inverse Reinforcement Learning 等；
- 对应教程的第十章和第十一章。

### Task6：DDPG 算法（2天）

- 掌握 DDPG 算法及其与 DQN 之间的关系；
- 对应教程的第十二章。




---
# 具体规则
- 注册 CSDN、Github 或 B站等账户。
- 按照任务安排进行学习，完成后写学习笔记Blog 或 进行视频直播。
- 在每次任务截止之前在群内填写问卷打卡，遇到问题在群内讨论。
- 未按时打卡的同学视为自动放弃，被抱出学习群。


---
# 备注

有关Datawhale组队学习的开源内容如下：

- [00 组队学习计划](https://github.com/datawhalechina/team-learning)
- [01 编程语言与数据结构](https://github.com/datawhalechina/team-learning-program)
- [02 数据挖掘基础算法](https://github.com/datawhalechina/team-learning-data-mining)
- [03 自然语言处理](https://github.com/datawhalechina/team-learning-nlp)
- [04 计算机视觉](https://github.com/datawhalechina/team-learning-cv)
- [05 推荐系统](https://github.com/datawhalechina/team-learning-rs)
- [06 强化学习](https://github.com/datawhalechina/team-learning-rl)



---
本次组队学习的 PDF 文档可到Datawhale的知识星球下载：

![Datawhale](https://img-blog.csdnimg.cn/2020072621074658.png)


---
![Datawhale](https://img-blog.csdnimg.cn/20200726211045814.png)

