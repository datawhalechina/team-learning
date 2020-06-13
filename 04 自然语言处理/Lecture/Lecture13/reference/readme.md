# 浅谈ELMO，GPT，BERT模型

> 作者：江湖散人

[BERT视频讲解：金金金](https://www.bilibili.com/video/BV17t4y1y7mT/)

## 引言

随着AI技术的迅猛发展，越来越多的信息能让机器帮助人类去处理，例如文本，音频，视频等。而文本信息是我们日常大量接触且及其重要的信息载体。

所以在自然语言处理(NLP)任务中，如果让机器更好的去读懂文本信息也是极其有意义且重要的工作。那么本文针对几类常见模型进行相关的介绍，由于笔者知识也不全面，所写之处若有不正确地方欢迎指出且改正，希望和大家一起交流探讨。

## 背景知识

本文主要介绍ELMO, GPT, BERT三个模型，这里还涉及到其他的相关知识，例如：Word2Vec方法的基本原理，Glove模型，FastText模型等。

这里贴出笔者觉得写的很优秀的文章，供大家参考：

Word2Vec：

```
https://blog.csdn.net/yu5064/article/details/79601683
```

Glove模型：

```
https://zhuanlan.zhihu.com/p/113331979
```

当然还有其他很多好文章，这里就不一一列举了。

## ELMO

首先介绍一下ELMo(Embeddings from Language Models)算法，可去观摩[原文](https://arxiv.org/pdf/1802.05365.pdf)查看更详细的内容。在之前word2vec及GloVe的工作中，每个词对应一个vector，对于多义词无能为力，或者随着语言环境的改变，这些vector不能准确的表达相应特征。ELMo的作者认为好的词表征模型应该同时兼顾两个问题：一是词语用法在语义和语法上的复杂特点；二是随着语言环境的改变，这些用法也应该随之改变。

ELMo算法过程为：

1. 先在大语料上以language model为目标训练出bidirectional LSTM模型；
2. 然后利用LSTM产生词语的表征；

ELMo模型包含多layer的bidirectional LSTM，可以这么理解:

高层的LSTM的状态可以捕捉词语意义中和语境相关的那方面的特征(比如可以用来做语义的消歧)，而低层的LSTM可以找到语法方面的特征(比如可以做词性标注)。

### Bidirectional language models

![](https://pirctures.oss-cn-beijing.aliyuncs.com/img/1.png)

ELMo模型有两个比较关键的公式：



![gongshi_1](https://pirctures.oss-cn-beijing.aliyuncs.com/img/gongshi_1.png)

![image-20200607195155431](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607195155431.png)

这里可以看出预测句子概率
$$
p(t1,t2,...,tn)
$$
有两个方向：正方向和反方向。

(t1,t2,...tn)是一系列的tokens 。

设输入token的表示为![image-20200607201058404](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607201058404.png),在每一个位置k，每一层LSTM上都输出相应的context-dependent的表征![image-20200607201217757](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607201217757.png)，这里 j 代表LSTM的某层layer，例如顶层的LSTM的输出可以表示为：![image-20200607201432357](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607201432357.png)，我们对最开始的两个概率求对数极大似然估计，即：

![image-20200607201838599](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607201838599.png)

这里的Θ*x*代表token embedding, Θ*s*代表softmax layer的参数。

### word feature

对于每一个token，一个L层的biLM模型要计算出共2*L*+1个表征： 

![image-20200607202244219](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607202244219.png)第二个“=”可以理解为：

当 j=0 时，代表token层。当 j>0 时，同时包括两个方向的hidden表征。

## GPT

这部分介绍GPT模型([原文](https://www.cs.ubc.ca/~amuham01/LING530/papers/radford2018improving.pdf)).

GPT的训练分为两个阶段：1）无监督预训练语言模型；2）各个任务的微调。

模型结构图：

![](https://pirctures.oss-cn-beijing.aliyuncs.com/img/2.png)

### 1.无监督pretrain

使用语言模型最大化下面的式子：

![image-20200607204843715](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607204843715.png)

其中 *k* 是上下文窗口大小，*θ* 是语言模型参数，使用一个神经网络来模拟条件概率 *P*

在论文中，使用一个多层的transformer decoder来作为LM(语言模型)，可以看作transformer的变体。将transformer  decoder中Encoder-Decoder Attention层去掉作为模型的主体，然后将decoder的输出经过一个softmax层，来得到目标词的输出分布：

![image-20200607205533296](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607205533296.png)

这里![image-20200607205656843](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607205656843.png)表示 *ui* 的上下文，![image-20200607205825579](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607205825579.png)是词向量矩阵，![image-20200607205855307](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607205855307.png)是位置向量矩阵。

### 2.有监督finetune

在这一步，我们根据自己的任务去调整预训练语言模型的参数 *θ*，

![image-20200607210752637](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607210752637.png)

最后优化的式子为：

![image-20200607210919284](https://pirctures.oss-cn-beijing.aliyuncs.com/img/image-20200607210919284.png)

在自己的任务中，使用遍历式的方法将结构化输入转换成预训练语言模型能够处理的有序序列：

![](https://pirctures.oss-cn-beijing.aliyuncs.com/img/3.png)

## BERT

Bert([原文](https://arxiv.org/abs/1810.04805
))是谷歌的大动作，公司AI团队新发布的BERT模型，在机器阅读理解顶级水平测试SQuAD1.1中表现出惊人的成绩：全部两个衡量指标上全面超越人类，并且还在11种不同NLP测试中创出最佳成绩，包括将GLUE基准推至80.4％（绝对改进7.6％），MultiNLI准确度达到86.7% （绝对改进率5.6％）等。可以预见的是，BERT将为NLP带来里程碑式的改变，也是NLP领域近期最重要的进展。

BERT的全称是Bidirectional Encoder Representation from Transformers，即双向Transformer的Encoder。模型的主要创新点都在pre-train方法上，即用了Masked LM和Next Sentence Prediction两种方法分别捕捉词语和句子级别的representation。

BERT采用了Transformer Encoder的模型来作为语言模型，Transformer模型来自于经典论文《Attention is all you need》, 完全抛弃了RNN/CNN等结构，而完全采用Attention机制来进行input-output之间关系的计算，如下图中左半边部分所示：

![](https://pirctures.oss-cn-beijing.aliyuncs.com/img/4.png)

Bert模型结构如下：

![](https://pirctures.oss-cn-beijing.aliyuncs.com/img/5.png)



BERT模型与OpenAI GPT的区别就在于采用了Transformer Encoder，也就是每个时刻的Attention计算都能够得到全部时刻的输入，而OpenAI GPT采用了Transformer Decoder，每个时刻的Attention计算只能依赖于该时刻前的所有时刻的输入，因为OpenAI GPT是采用了单向语言模型。

## 参考文献

- https://arxiv.org/pdf/1802.05365.pdf
- https://www.cs.ubc.ca/~amuham01/LING530/papers/radford2018improving.pdf
- https://arxiv.org/abs/1810.04805
- https://zhuanlan.zhihu.com/p/113331979
- https://blog.csdn.net/yu5064/article/details/79601683
- https://zhuanlan.zhihu.com/p/46833276
- https://www.jianshu.com/p/4dbdb5ab959b?from=singlemessage
- https://blog.csdn.net/pipisorry/article/details/84951508
- https://blog.csdn.net/triplemeng/article/details/82380202