

第19期 Datawhale 组队学习活动马上就要开始啦！

本次组队学习的内容为：

- [推荐系统实践（新闻推荐）](https://github.com/datawhalechina/team-learning-rs/tree/master/RecommandNews)
- [编程实践（Numpy）下](https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy)

其中，[推荐系统实践（新闻推荐）](https://github.com/datawhalechina/team-learning-rs/tree/master/RecommandNews)的内容来自Datawhale与天池联合发起的 [零基础入门推荐系统 - 新闻推荐](https://tianchi.aliyun.com/competition/gameList/coupleList) 学习赛的第一场。

大家可以根据我们的开源内容进行自学，也可以加入我们的组队学习一起来学。


---
# 推荐系统实践（新闻推荐）

开源学习内容：https://github.com/datawhalechina/team-learning-rs/tree/master/RecommandNews

## 基本信息
- 贡献人员：王贺、罗如意、吴忠强、李万业、陈琰钰、张汉隆
- 学习周期：15天
- 学习形式：理论学习+练习
- 人群定位：有一定的数据分析基础，了解推荐系统基本算法，了解机器学习算法流程
- 先修内容：[Python编程语言](https://github.com/datawhalechina/team-learning-program/tree/master/PythonLanguage)；[编程实践（Pandas）](https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToPandas)；[编程实践（Numpy）](https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy)；[推荐系统基础](https://github.com/datawhalechina/team-learning-rs/tree/master/RecommendationSystemFundamentals)。
- 难度系数：中

## 学习目标

### 熟悉推荐系统竞赛的基本流程

- 掌握数据分析方法
- 了解多路召回策略
- 了解冷启动策略
- 了解排序特征的构造方法
- 了解常见的排序模型
- 了解模型融合


### 新闻推荐入门赛学习内容汇总：

![](https://img-blog.csdnimg.cn/20201117153943695.png)


## 任务安排

### Task00：熟悉规则（1天）

- 组队、修改群昵称
- 熟悉打开规则

### Task01：赛题理解+Baseline（3天）

- 理解赛题数据和目标，理解评分指标，了解赛题的解题思路
- 完成赛题报名和数据下载，跑通Baseline并成功提交结果

### Task02：数据分析（2天）

- 了解数据中不同文件所包含的信息，不同数据文件之间的关系
- 分析点击数据中用户的点击环境、点击偏好，点击的文章属性等分布
- 分析点击数据中文章的基本属性，文章的热门程度，文章的共现情况等
- 分析文章属性文件中(embedding文件和属性特征文件)，文章的基本信息


### Task03：多路召回（3天）

- 熟悉常见的召回策略，如：itemcf, usercf以及深度模型召回等
- 了解当前场景下的冷启动问题，及常见解决策略，了解如何将多路召回的结果进行合并
- 完成多种策略的召回，冷启动及多路召回合并
- 完成召回策略的调参和召回效果的评估


### Task04：特征工程（3天）

- 了解排序数据标签的构建，训练数据的负采样，排序特征的常用构造思路
- 完成用户召回文章与历史文章相关性的特征构造
- 完成用户历史兴趣的相关特征的提取，文章本身属性特征的提取



### Task05：排序模型+模型融合（3天）

- 了解基本的排序模型，模型的训练和测试，常用的模型融合策略
- 完成LGB分类模型，LGB排序模型，及深度模型中的DIN模型的训练、验证及调参
- 完成加权融合与Staking融合两种融合策略



---
# 编程实践（Numpy）下

开源学习内容：https://github.com/datawhalechina/team-learning-program/tree/master/IntroductionToNumpy



## 基本信息
- 贡献人员：韩绘锦、王彦淳、马燕鹏
- 学习周期：9天，每天平均花费时间3小时-5小时不等，根据个人学习接受能力强弱有所浮动。
- 学习形式：理论学习 + 练习
- 人群定位：有一定python编程的基础。
- 先修内容：[Python编程语言](https://github.com/datawhalechina/team-learning-program/tree/master/PythonLanguage)
- 难度系数：中




## 学习目标

本开源内容是Python基础的进阶，主要目标是学习numpy的基本数据类型，了解numpy各类函数的应用；以便为后期学习pandas和sklearn奠定坚实基础。

## 任务安排

### Task00：熟悉规则（1天）

- 组队、修改群昵称
- 熟悉打开规则


### Task01：输入输出（2天）
- 熟悉 Numpy 如何处理二进制文件和文本文件。


### Task02：随机抽样（2天）

- 熟悉 Numpy 常用的随机函数
- 熟悉 Numpy 如何处理离散型随机变量的分布，如二项分布、泊松分布、超几何分布
- 熟悉 Numpy 如何处理连续型随机变量的分布，如均匀分布、正态分布、指数分布


### Task03：统计相关（2天）
- 熟悉 Numpy 如何处理次序统计，如最大值、最小值、极差、百分位数等
- 熟悉 Numpy 如何处理均值、方差、标准差、协方差等
- 熟悉 Numpy 如何绘制直方图等


### Task04：线性代数（2天）

- 熟悉 Numpy 如何处理矩阵乘法以及向量的内积
- 熟悉 Numpy 如何处理矩阵的特征值和特征向量
- 熟悉 Numpy 如何处理矩阵的各种分解，如SVD、QR、Cholesky分解
- 熟悉 Numyp 如何处理矩阵的范数、行列式和秩
- 熟悉 Numpy 如何处理逆矩阵和线性方程组求解

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

