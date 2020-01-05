
# Task00 机器学习概述（1天）
## 理论部分
[Task0](./Task0_ml_overvirew.md)

- 机器学习介绍
  机器学习是什么，怎么来的，理论基础是什么，为了解决什么问题（应用）
- 机器学习分类
  按学习方式分：有监督、无监督、半监督
  按任务类型分：回归、分类、聚类、降维
  生成模型与判别模型
- 机器学习方法三要素
  **模型：**
  **策略：**损失函数
  **算法：**梯度下降法、牛顿法、拟牛顿法
  模型评估指标：R2、RMSE、accuracy、precision、recall、F1、ROC、AUC、Confusion Matrix
  复杂度度量：偏差与方差、过拟合与欠拟合、结构风险与经验风险、泛化能力、正则化
  模型选择：正则化、交叉验证
  采样：样本不均衡
  特征处理：归一化、标准化、离散化、one-hot编码
  模型调优：网格搜索寻优、随机搜索寻优


---
# Task01 线性回归（2天）
## 理论部分
[Task1](./Task1_Linear_regression.ipynb)

- 模型建立：线性回归原理、线性回归模型
- 学习策略：线性回归损失函数、代价函数、目标函数
- 算法求解：梯度下降法、牛顿法、拟牛顿法等
- 线性回归的评估指标
- sklearn参数详解
- Lasso回归、岭回归、ElasticNet回归
- 应用背景及优缺点

## 练习部分
github:
[Task1](./Task1_Linear_regression.ipynb)


---


# Task02：逻辑回归（2天）
## 理论部分
[Task2](./Task2_logistic_regression.ipynb)

- 逻辑回归与线性回归的联系与区别
- 模型建立：逻辑回归原理、逻辑回归模型
- 学习策略：逻辑回归损失函数、推导及优化
- 算法求解：批量梯度下降
- 正则化与模型评估指标
- sklearn参数
- 应用背景及优缺点

## 练习部分
github:
[Task2](./Task2_logistic_regression.ipynb)
---

# Task03：决策树（2天）
## 理论部分
[Task3](./Task3_decision_tree.ipynb)

- 特征选择：信息增益（熵、联合熵、条件熵）、信息增益比、基尼系数
- 决策树生成：ID3决策树、C4.5决策树、CART决策树（CART分类树、CART回归树）
- 决策树剪枝
- sklearn参数详解
- 应用背景及优缺点

## 练习部分
github:
[Task3](./Task3_decision_tree.ipynb)
---

# Task04：聚类（2天）
## 理论部分
[Task4](./Task4_cluster_plus.ipynb)

- 常用距离公式：曼哈顿距离、欧式距离、闵可夫斯基距离、切比雪夫距离、夹角余弦、汉明距离、杰卡德相似系数、杰卡德距离
- K-Means聚类：聚类过程和原理、算法流程、算法优化（k-means++、Mini Batch K-Means）、sklearn参数详解
- 层次聚类：Agglomerative Clustering过程和原理、sklearn参数详解
- 密度聚类：DBSCAN过程和原理、sklearn参数详解
- 谱聚类：谱聚类原理（邻接矩阵、度矩阵、拉普拉斯矩阵、RatioCut、Ncut）和过程、sklearn参数详解
- 高斯混合聚类：GMM过程和原理、EM算法原理、利用EM算法估计高斯混合聚类参数、sklearn参数详解

## 练习部分
github:
[Task4](./Task4_cluster_plus.ipynb)


---

# Task05：朴素贝叶斯（2天）
## 理论部分
[Task5](./Task5_bayes_plus.ipynb)

- 朴素贝叶斯基本原理（条件概率公式、乘法公式、全概率公式、贝叶斯定理、特征条件独立假设）、后验概率最大化、拉普拉斯平滑
- 朴素贝叶斯的三种形式（高斯型、多项式型、伯努利型）
- sklearn参数详解
- 应用背景及优缺点

## 练习部分
github:
[Task5](./Task5_bayes_plus.ipynb)

------

# Task06：KNN（2天）
[Task6](./Task6_knn.ipynb)

## 理论部分
- KNN分类原理：分类决策规则、算法过程
- KNN回归原理：回归决策规则、算法过程
- 搜索优化：KD树
- sklearn参数详解
- 应用背景及优缺点

## 练习部分
github:
[Task6](./Task6_knn.ipynb)






















