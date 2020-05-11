# Datawhale 零基础入门CV赛事-Task2 数据读取与数据扩增
     
## 2 数据读取与数据扩增     
本章主要内容为数据读取、数据扩增方法和Pytorch读取赛题数据三个部分组成。     
       
### 2.1 学习目标     
- 学习Python和Pytorch中图像读取    
- 学会扩增方法和Pytorch读取赛题数据
      
### 2.2 图像读取           
由于赛题数据是图像数据，赛题的任务是识别图像中的字符。因此我们首先需要完成对数据的读取操作，在Python中有很多库可以完成数据读取的操作，比较常见的有Pillow和OpenCV。     
    
#### 2.2.1 Pillow     
Pillow是Python图像处理函式库(PIL）的一个分支。Pillow提供了常见的图像读取和处理的操作，而且可以与ipython notebook无缝集成，是应用比较广泛的库。      
        
当然上面只演示了Pillow最基础的操作，Pillow还有很多图像操作，是图像处理的必备库。       
Pillow的官方文档：https://pillow.readthedocs.io/en/stable/
     
#### 2.2.2 OpenCV           
OpenCV是一个跨平台的计算机视觉库，最早由Intel开源得来。OpenCV发展的非常早，拥有众多的计算机视觉、数字图像处理和机器视觉等功能。OpenCV在功能上比Pillow更加强大很多，学习成本也高很多。       
       
OpenCV包含了众多的图像处理的功能，OpenCV包含了你能想得到的只要与图像相关的操作。此外OpenCV还内置了很多的图像特征处理算法，如关键点检测、边缘检测和直线检测等。       
OpenCV官网：https://opencv.org/       
OpenCV Github：https://github.com/opencv/opencv      
OpenCV 扩展算法库：https://github.com/opencv/opencv_contrib
       
### 2.3 数据扩增方法          
在上一小节中给大家初步介绍了Pillow和OpenCV的使用，现在回到赛题街道字符识别任务中。在赛题中我们需要对的图像进行字符识别，因此需要我们完成的数据的读取操作，同时也需要完成数据扩增（Data Augmentation）操作。     
       
#### 2.3.1 数据扩增介绍      
在深度学习中数据扩增方法非常重要，数据扩增可以增加训练集的样本，同时也可以有效缓解模型过拟合的情况，也可以给模型带来的更强的泛化能力。      

- #### 数据扩增为什么有用？      
在深度学习模型的训练过程中，数据扩增是必不可少的环节。现有深度学习的参数非常多，一般的模型可训练的参数量基本上都是万到百万级别，而训练集样本的数量很难有这么多。       
其次数据扩增可以扩展样本空间，假设现在的分类模型需要对汽车进行分类，左边的是汽车A，右边为汽车B。如果不使用任何数据扩增方法，深度学习模型会从汽车车头的角度来进行判别，而不是汽车具体的区别。     
       
- #### 有哪些数据扩增方法？            
数据扩增方法有很多：从颜色空间、尺度空间到样本空间，同时根据不同任务数据扩增都有相应的区别。        
对于图像分类，数据扩增一般不会改变标签；对于物体检测，数据扩增会改变物体坐标位置；对于图像分割，数据扩增会改变像素标签。     
        
#### 2.3.2 常见的数据扩增方法     
在常见的数据扩增方法中，一般会从图像颜色、尺寸、形态、空间和像素等角度进行变换。当然不同的数据扩增方法可以自由进行组合，得到更加丰富的数据扩增方法。         
以torchvision为例，常见的数据扩增方法包括：

- transforms.CenterCrop      对图片中心进行裁剪      
- transforms.ColorJitter      对图像颜色的对比度、饱和度和零度进行变换      
- transforms.FiveCrop     对图像四个角和中心进行裁剪得到五分图像     
- transforms.Grayscale      对图像进行灰度变换    
- transforms.Pad        使用固定值进行像素填充     
- transforms.RandomAffine      随机仿射变换    
- transforms.RandomCrop      随机区域裁剪     
- transforms.RandomHorizontalFlip      随机水平翻转     
- transforms.RandomRotation     随机旋转     
- transforms.RandomVerticalFlip     随机垂直翻转   




