# Datawhale 计算机视觉基础-图像处理（下）- Task01 Harris特征点检测器-兴趣点检测

## 1.1 简介 
在图像处理领域中，特征点又被称为兴趣点或者角点，它通常具有旋转不变性和光照不变性和视角不变性等优点，是图像的重要特征之一，常被应用到目标匹配、目标跟踪、三维重建等应用中。点特征主要指图像中的明显点，如突出的角点、边缘端点、极值点等等，用于点特征提取的算子称为兴趣点提取（检测）算子，常用的有Harris角点检测、FAST特征检测、SIFT特征检测及SURF特征检测。     
本次任务学习较为常用而且较为基础的Harris角点检测算法，它的思想以及数学理论能够很好地帮助我们了解兴趣点检测的相关原理。

## 1.2 学习目标

* 理解Harris特征点检测算法的思想和数学原理
* 学会利用OpenCV的Harris算子进行兴趣点检测

## 1.3 内容大纲    

 - 基础知识
 - Harris角点检测算法原理
 - OpenCV实现

## 1.4 内容介绍   
      
###  1.4.1 基础知识
### 1.角点
使用一个滑动窗口在下面三幅图中滑动，可以得出以下结论：
* 左图表示一个平坦区域，在各方向移动，窗口内像素值均没有太大变化；
* 中图表示一个边缘特征（Edges），如果沿着水平方向移动(梯度方向)，像素值会发生跳变；如果沿着边缘移动(平行于边缘) ，像素值不会发生变化；
* 右图表示一个角（Corners），不管你把它朝哪个方向移动，像素值都会发生很大变化。        
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200609204249219.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_1,color_FFFFFF,t_70#pic_center)       
            
所以，右图是一个角点。

### 2.角点类型
下图展示了不同角点的类型，可以发现：如果使用一个滑动窗口以角点为中心在图像上滑动，存在朝多个方向上的移动会引起该区域的像素值发生很大变化的现象。         
![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMDQwNTc1NTQtMTEzNDYyNjYwLnBuZw?x-oss-process=image/format,png#pic_center)        
### 3.图像梯度
“*像素值发生很大变化*”这一现象可以用图像梯度进行描述。在图像局部内，图像梯度越大表示该局部内像素值变化越大（灰度的变化率越大）。
而图像的梯度在数学上可用**微分或者导数**来表示。对于数字图像来说，相当于是**二维离散函数求梯度**，并使用差分来近似导数：
$G_x(x,y)=H(x+1,y)-H(x-1,y)$
$G_y(x,y)=H(x,y+1)-H(x,y-1)$
在实际操作中，对图像求梯度通常是考虑图像的每个像素的某个邻域内的灰度变化，因此通常对原始图像中像素某个邻域设置梯度算子，然后采用小区域模板进行卷积来计算，常用的有Prewitt算子、Sobel算子、Robinson算子、Laplace算子等。

### 1.4.2 Harris角点检测算法原理
### 1. 算法思想  
算法的核心是利用局部窗口在图像上进行移动，判断灰度是否发生较大的变化。如果窗口内的灰度值（在梯度图上）都有较大的变化，那么这个窗口所在区域就存在角点。  

这样就可以将 Harris 角点检测算法分为以下三步：

* 当窗口（局部区域）同时向 x （水平）和 y（垂直） 两个方向移动时，计算窗口内部的像素值变化量 $E(x,y)$ ；
* 对于每个窗口，都计算其对应的一个角点响应函数 $R$；
* 然后对该函数进行阈值处理，如果 $R > threshold$，表示该窗口对应一个角点特征。
   
 ### 2. 第一步 — 建立数学模型    
           
 **第一步是通过建立数学模型，确定哪些窗口会引起较大的灰度值变化。**
 让一个窗口的中心位于灰度图像的一个位置$(x,y)$，这个位置的像素灰度值为$I(x,y)$ ，如果这个窗口分别向 $x$ 和 $y$ 方向移动一个小的位移$u$和$v$，到一个新的位置 $(x+u,y+v)$ ，这个位置的像素灰度值就是$I(x+u,y+v)$ 。   
             
$|I(x+u,y+v)-I(x,y)|$就是窗口移动引起的灰度值的变化值。 

设$w(x,y)$为位置$(x,y)$处的窗口函数，表示窗口内各像素的权重，最简单的就是把窗口内所有像素的权重都设为1，即一个均值滤波核。

当然，也可以把 $w(x,y)$设定为以窗口中心为原点的高斯分布，即一个高斯核。如果窗口中心点像素是角点，那么窗口移动前后，中心点的灰度值变化非常强烈，所以该点权重系数应该设大一点，表示该点对灰度变化的贡献较大；而离窗口中心（角点）较远的点，这些点的灰度变化比较小，于是将权重系数设小一点，表示该点对灰度变化的贡献较小。

则窗口在各个方向上移动 $(u,v)$所造成的像素灰度值的变化量公式如下：          

![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMTAyNDQ3NTctMTI4MTA5NjM5NS5wbmc?x-oss-process=image/format,png#pic_center)             
若窗口内是一个角点，则$E(u,v)$的计算结果将会很大。    
        
为了提高计算效率，对上述公式进行简化，利用泰勒级数展开来得到这个公式的近似形式：

对于二维的泰勒展开式公式为：   
$T(x,y)=f(u,v)+(x-u)f_x(u,v)+(y-v)f_y(u,v)+....$             

则$I(x+u,y+v)$ 为：          
$I(x+u,y+v)=I(x,y)+uI_x+vI_y$

其中$I_x$和$I_y$是$I$的微分（偏导），在图像中就是求$x$ 和 $y$ 方向的**梯度图**：     

$I_x=\frac{\partial I(x,y)}{\partial x}$  
                                        
$I_y=\frac{\partial I(x,y)}{\partial y}$   
             
 将$I(x+u,y+v)=I(x,y)+uI_x+vI_y$代入$E(u，v)$可得：             
 
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200610123808434.png)              
       
 提出 u 和 v ，得到最终的近似形式：
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200610123233564.png)        
           
其中矩阵M为：             

![在这里插入图片描述](https://img-blog.csdnimg.cn/20200610123258145.png)           
                
最后是把实对称矩阵对角化处理后的结果，可以把R看成旋转因子，其不影响两个正交方向的变化分量。

经对角化处理后，将两个正交方向的变化分量提取出来，就是 λ1 和 λ2（特征值）。
 这里利用了**线性代数中的实对称矩阵对角化**的相关知识，有兴趣的同学可以进一步查阅相关资料。
       
       
### 3. 第二步—角点响应函数R
现在我们已经得到 $E(u,v)$的最终形式，别忘了我们的目的是要找到会引起较大的灰度值变化的那些窗口。

灰度值变化的大小则取决于矩阵M，M为梯度的协方差矩阵。在实际应用中为了能够应用更好的编程，所以定义了角点响应函数R，通过判定R大小来判断像素是否为角点。 

计算每个窗口对应的得分（角点响应函数R定义）：
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200610124043231.png)                 
其中 $det(M)=\lambda_1\lambda_2$是矩阵的行列式， $trace(M)=\lambda_1+\lambda_2$ 是矩阵的迹。    

$λ1$ 和 $λ2$ 是矩阵$M$的特征值，  $k$是一个经验常数，在范围 (0.04, 0.06) 之间。   

$R$的值取决于$M$的特征值，对于角点$|R|$很大，平坦的区域$|R|$很小，边缘的$R$为负值。

### 4. 第三步—角点判定
根据 R 的值，将这个窗口所在的区域划分为平面、边缘或角点。为了得到最优的角点，我们还可以使用非极大值抑制。   

 注意：Harris 检测器具有旋转不变性，但不具有尺度不变性，也就是说尺度变化可能会导致角点变为边缘。想要尺度不变特性的话，可以关注SIFT特征。    

因为特征值 λ1 和 λ2 决定了 R 的值，所以我们可以用特征值来决定一个窗口是平面、边缘还是角点：

* 平面:：该窗口在平坦区域上滑动，窗口内的灰度值基本不会发生变化，所以 $|R|$ 值非常小，在水平和竖直方向的变化量均较小，即 $I_x$和 $I_y$都较小，那么 λ1 和 λ2 都较小；
* 边缘：$|R|$值为负数，仅在水平或竖直方向有较大的变化量，即 $I_x$和 $I_y$只有一个较大，也就是 λ1>>λ2 或 λ2>>λ1；
* 角点：[公式] 值很大，在水平、竖直两个方向上变化均较大的点，即 $I_x$和 $I_y$ 都较大，也就是 λ1 和 λ2 都很大。    
    
   如下图所示：                                   
                    
![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMTA1NDU5OTEtNDQ0Njk1NTE4LnBuZw?x-oss-process=image/format,png#pic_center)                
                       
Harris 角点检测的结果是带有这些分数 R 的灰度图像，设定一个阈值，分数大于这个阈值的像素就对应角点。
## 1.5 基于OpenCV的实现

### 1. API
在opencv中有提供实现 Harris 角点检测的函数 cv2.cornerHarris，我们直接调用的就可以，非常方便。

函数原型：`cv2.cornerHarris(src, blockSize, ksize, k[, dst[, borderType]])` 
     
 对于每一个像素 (x,y)，在 (blockSize x blockSize) 邻域内，计算梯度图的协方差矩阵 $M(x,y)$，然后通过上面第二步中的角点响应函数得到结果图。图像中的角点可以为该结果图的局部最大值。

即可以得到输出图中的局部最大值，这些值就对应图像中的角点。  

参数解释：
* src - 输入灰度图像，float32类型
* blockSize - 用于角点检测的邻域大小，就是上面提到的窗口的尺寸
* ksize - 用于计算梯度图的Sobel算子的尺寸
* k - 用于计算角点响应函数的参数k，取值范围常在0.04~0.06之间

### 代码示例
```python
import cv2 as cv
from matplotlib import pyplot as plt
import numpy as np

# detector parameters
block_size = 3
sobel_size = 3
k = 0.06

image = cv.imread('Scenery.jpg')

print(image.shape)
height = image.shape[0]
width = image.shape[1]
channels = image.shape[2]
print("width: %s  height: %s  channels: %s"%(width, height, channels))
   
gray_img = cv.cvtColor(image, cv2.COLOR_BGR2GRAY)


# modify the data type setting to 32-bit floating point 
gray_img = np.float32(gray_img)

# detect the corners with appropriate values as input parameters
corners_img = cv.cornerHarris(gray_img, block_size, sobel_size, k)

# result is dilated for marking the corners, not necessary
kernel = cv2.getStructuringElement(cv2.MORPH_RECT,(3, 3))
dst = cv.dilate(corners_img, kernel)

# Threshold for an optimal value, marking the corners in Green
#image[corners_img>0.01*corners_img.max()] = [0,0,255]

for r in range(height):
        for c in range(width):
            pix=dst[r,c]
            if pix>0.05*dst.max():
               cv2.circle(image,(c,r),5,(0,0,255),0)

image = cv.cvtColor(image, cv2.COLOR_BGR2RGB)
plt.imshow(image)
plt.show()
```
     
### 结果：         
![在这里插入图片描述](https://img-blog.csdnimg.cn/2020061019023712.jpg?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70#pic_center)                
![在这里插入图片描述](https://img-blog.csdnimg.cn/20200610190150712.jpg?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_1,color_FFFFFF,t_70#pic_center)         
  

## 1.6 总结 
本小节对Harris角点检测算法进行了学习。通过这次学习我们了解了角点的概念、图像梯度等基本知识，也认识了基本的角点检测算法思想。

Harris角点检测的性质可总结如下：
* **阈值决定角点的数量**
* **Harris角点检测算子对亮度和对比度的变化不敏感（光照不变性）**
  在进行Harris角点检测时，使用了微分算子对图像进行微分运算，而微分运算对图像密度的拉升或收缩和对亮度的抬高或下降不敏感。换言之，对亮度和对比度的仿射变换并不改变Harris响应的极值点出现的位置，但是，由于阈值的选择，可能会影响角点检测的数量。          
![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMTEwNTc1ODUtOTE5ODk5OTcucG5n?x-oss-process=image/format,png#pic_center)             
       
       
* **Harris角点检测算子具有旋转不变性**
   Harris角点检测算子使用的是角点附近的区域灰度二阶矩矩阵。而二阶矩矩阵可以表示成一个椭圆，椭圆的长短轴正是二阶矩矩阵特征值平方根的倒数。当特征椭圆转动时，特征值并不发生变化，所以判断角点响应值也不发生变化，由此说明Harris角点检测算子具有旋转不变性。          
![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMTExNDA1NTQtMTQxNTkyMDkyNi5wbmc?x-oss-process=image/format,png#pic_center)            
    
* **Harris角点检测算子不具有尺度不变性**
尺度的变化会将角点变为边缘，或者边缘变为角点，Harris的理论基础并不具有尺度不变性。                           
![在这里插入图片描述](https://imgconvert.csdnimg.cn/aHR0cHM6Ly9pbWFnZXMyMDE1LmNuYmxvZ3MuY29tL2Jsb2cvNDUxNjYwLzIwMTYwNC80NTE2NjAtMjAxNjA0MjExMTExNTk2NjMtMjA5NDMzNzQyNy5wbmc?x-oss-process=image/format,png#pic_center)        
## 相关技术文档、论文推荐
*  [论文：《C.Harris, M.Stephens. “A Combined Corner and Edge Detector”. Proc. of 4th Alvey Vision Conference》](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.434.4816&rep=rep1&type=pdf)  
* [Harris角点算法](https://www.cnblogs.com/polly333/p/5416172.html)     
* [角点检测：Harris 与 Shi-Tomasi](https://zhuanlan.zhihu.com/p/83064609)    
* [https://www.cnblogs.com/ronny/p/4009425.html](https://www.cnblogs.com/ronny/p/4009425.html)     
           
---
**Task01  Harris特征点检测 END.**

--- ***By: 小武***


>博客：[https://blog.csdn.net/weixin_40647819](https://blog.csdn.net/weixin_40647819)   


**关于Datawhale**：

>Datawhale是一个专注于数据科学与AI领域的开源组织，汇集了众多领域院校和知名企业的优秀学习者，聚合了一群有开源精神和探索精神的团队成员。Datawhale以“for the learner，和学习者一起成长”为愿景，鼓励真实地展现自我、开放包容、互信互助、敢于试错和勇于担当。同时Datawhale 用开源的理念去探索开源内容、开源学习和开源方案，赋能人才培养，助力人才成长，建立起人与人，人与知识，人与企业和人与未来的联结。

