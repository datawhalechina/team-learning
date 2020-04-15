# Datawhale 计算机视觉基础-图像处理（上）-Task05 图像分割/二值化

## 5.1 简介 
该部分的学习内容是对经典的阈值分割算法进行回顾，图像阈值化分割是一种传统的最常用的图像分割方法，因其实现简单、计算量小、性能较稳定而成为图像分割中最基本和应用最广泛的分割技术。它特别适用于目标和背景占据不同灰度级范围的图像。它不仅可以极大的压缩数据量，而且也大大简化了分析和处理步骤，因此在很多情况下，是进行图像分析、特征提取与模式识别之前的必要的图像预处理过程。图像阈值化的目的是要按照灰度级，对像素集合进行一个划分，得到的每个子集形成一个与现实景物相对应的区域，各个区域内部具有一致的属性，而相邻区域不具有这种一致属性。这样的划分可以通过从灰度级出发选取一个或多个阈值来实现。


## 5.2 学习目标
* 了解阈值分割基本概念

* 理解最大类间方差法（大津法）、自适应阈值分割的原理

* 掌握OpenCV框架下上述阈值分割算法API的使用


## 5.3 内容介绍

1、最大类间方差法、自适应阈值分割的原理

2、OpenCV代码实践

3、动手实践并打卡（读者完成）

## 5.4 算法理论介绍
### 5.4.1 最大类间方差法（大津阈值法） 
大津法（OTSU）是一种确定图像二值化分割阈值的算法，由日本学者大津于1979年提出。从大津法的原理上来讲，该方法又称作最大类间方差法，因为按照大津法求得的阈值进行图像二值化分割后，前景与背景图像的类间方差最大。

它被认为是图像分割中阈值选取的最佳算法，计算简单，不受图像亮度和对比度的影响，因此在数字图像处理上得到了广泛的应用。它是按图像的灰度特性，将图像分成背景和前景两部分。因方差是灰度分布均匀性的一种度量,背景和前景之间的类间方差越大,说明构成图像的两部分的差别越大,当部分前景错分为背景或部分背景错分为前景都会导致两部分差别变小。因此,使类间方差最大的分割意味着错分概率最小。

**应用：** 是求图像全局阈值的最佳方法，应用不言而喻，适用于大部分需要求图像全局阈值的场合。

**优点：** 计算简单快速，不受图像亮度和对比度的影响。

**缺点：** 对图像噪声敏感；只能针对单一目标分割；当目标和背景大小比例悬殊、类间方差函数可能呈现双峰或者多峰，这个时候效果不好。

原理非常简单，涉及的知识点就是均值、方差等概念和一些公式推导。为了便于理解，我们从目的入手，反推一下这著名的OTSU算法。

求类间方差：

OTSU算法的假设是存在阈值TH将图像所有像素分为两类C1(小于TH)和C2(大于TH)，则这两类像素各自的均值就为m1、m2，图像全局均值为mG。同时像素被分为C1和C2类的概率分别为p1、p2。因此就有：

<div align=center><img   src ="https://img-blog.csdnimg.cn/20200412211054156.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70"/></div>           

根据原文，式（4）还可以进一步变形：

 <div align=center><img   src ="https://img-blog.csdnimg.cn/20200412210949765.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70"/></div>      

分割：

这个分割就是二值化，OpenCV给了以下几种方式，很简单，可以参考：

!<div align=center><img   src ="https://img-blog.csdnimg.cn/20200412211203976.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70"/></div>  
     

### 5.4.2 自适应阈值
前面介绍了OTSU算法，但这算法属于全局阈值法，所以对于某些光照不均的图像，这种全局阈值分割的方法会显得苍白无力，如下图：

<div align=center><img   src ="https://img-blog.csdnimg.cn/20200412211750915.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70"/></div>  

显然，这样的阈值处理结果不是我们想要的，那么就需要一种方法来应对这样的情况。

这种办法就是自适应阈值法(adaptiveThreshold)，它的思想不是计算全局图像的阈值，而是根据图像不同区域亮度分布，计算其局部阈值，所以对于图像不同区域，能够自适应计算不同的阈值，因此被称为自适应阈值法。(其实就是局部阈值法)

如何确定局部阈值呢？可以计算某个邻域(局部)的均值、中值、高斯加权平均(高斯滤波)来确定阈值。值得说明的是：如果用局部的均值作为局部的阈值，就是常说的移动平均法。


## 5.5 基于OpenCV的实现

* 工具：OpenCV3.1.0+VS2013
* 平台：WIN10
###  函数原型（c++）
**1.最大类间方差法**

```cpp
double cv::threshold	(	InputArray 	src,
                           OutputArray 	dst,
                             double 	thresh,
                             double 	maxval,
                               int      type 
)		
```
参数：

* src —	input array (single-channel, 8-bit or 32-bit floating point).
* dst	—   output array of the same size and type as src.
* thresh —	 threshold value.
* maxval   —	maximum value to use with the THRESH_BINARY and THRESH_BINARY_INV thresholding types.
* type	— thresholding type 参考：[thresholdType](https://docs.opencv.org/3.1.0/d7/d1b/group__imgproc__misc.html#ga72b913f352e4a1b1b397736707afcde3)

**1.自适应阈值**
```cpp
void adaptiveThreshold(InputArray src, OutputArray dst, 
                             double maxValue,
                             int adaptiveMethod,
                             int thresholdType, 
                             int blockSize, double C)
```
参数：
Parameters
* src —	Source 8-bit single-channel image.
* dst	— Destination image of the same size and the same type as src.
* maxValue —	Non-zero value assigned to the pixels for which the condition is satisfied
* adaptiveMethod	— Adaptive thresholding algorithm to use,参考：[cv::AdaptiveThresholdTypes](https://docs.opencv.org/3.1.0/d7/d1b/group__imgproc__misc.html#ga72b913f352e4a1b1b397736707afcde3)
* thresholdType — Thresholding type that must be either THRESH_BINARY or THRESH_BINARY_INV, 可参考：[thresholdType](https://docs.opencv.org/3.1.0/d7/d1b/group__imgproc__misc.html#ga72b913f352e4a1b1b397736707afcde3)
  blockSize	Size of a pixel neighborhood that is used to calculate a threshold value for the pixel: 3, 5, 7, and so on.
* C	— Constant subtracted from the mean or weighted mean (see the details below). Normally, it is positive but may be zero or negative as well.

### 实现示例（c++)
* 1、大津阈值
```cpp
#include <iostream>
#include <opencv2/opencv.hpp>
using namespace std;
using namespace cv;
 
int main(int argc, char* argv[])
{
	Mat img = imread(argv[1], -1);
	if (img.empty())
	{
		cout <<"Error: Could not load image" <<endl;
		return 0;
	}
 
	Mat gray;
	cvtColor(img, gray, CV_BGR2GRAY);
 
	Mat dst;
	threshold(gray, dst, 0, 255, CV_THRESH_OTSU);
 
	imshow("src", img);
	imshow("gray", gray);
	imshow("dst", dst);
	waitKey(0);
 
	return 0;
}
```
* 2、自适应阈值

```cpp
#include <iostream>
#include <opencv2/opencv.hpp>
using namespace std;
using namespace cv;
 
int main(int argc, char* argv[])
{
	Mat img = imread(argv[1], -1);
	if (img.empty())
	{
		cout <<"Error: Could not load image" <<endl;
		return 0;
	}
 
	Mat gray;
	cvtColor(img, gray, CV_BGR2GRAY);
 
	Mat dst;
	cv::adaptiveThreshold(gray,, dst, 255, cv::ADAPTIVE_THRESH_MEAN_C, cv::THRESH_BINARY, 21, 10);;
 
	imshow("src", img);
	imshow("gray", gray);
	imshow("dst", dst);
	waitKey(0);
 
	return 0;
}
```

### 进阶实现(根据原理自己实现)
### 实现示例（c++)
* 1、大津阈值

```cpp
#include <iostream>
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>
 
int Otsu(cv::Mat& src, cv::Mat& dst, int thresh){
	const int Grayscale = 256;
	int graynum[Grayscale] = { 0 };
	int r = src.rows;
	int c = src.cols;
	for (int i = 0; i < r; ++i){
		const uchar* ptr = src.ptr<uchar>(i);
		for (int j = 0; j < c; ++j){        //直方图统计
			graynum[ptr[j]]++;
		}
	}
 
    double P[Grayscale] = { 0 };   
	double PK[Grayscale] = { 0 };
	double MK[Grayscale] = { 0 };
	double srcpixnum = r*c, sumtmpPK = 0, sumtmpMK = 0;
	for (int i = 0; i < Grayscale; ++i){
		P[i] = graynum[i] / srcpixnum;   //每个灰度级出现的概率
		PK[i] = sumtmpPK + P[i];         //概率累计和 
		sumtmpPK = PK[i];
		MK[i] = sumtmpMK + i*P[i];       //灰度级的累加均值                                                                                                                                                                                                                                                                                                                                                                                                        
		sumtmpMK = MK[i];
	}
	
	//计算类间方差
	double Var=0;
	for (int k = 0; k < Grayscale; ++k){
		if ((MK[Grayscale-1] * PK[k] - MK[k])*(MK[Grayscale-1] * PK[k] - MK[k]) / (PK[k] * (1 - PK[k])) > Var){
			Var = (MK[Grayscale-1] * PK[k] - MK[k])*(MK[Grayscale-1] * PK[k] - MK[k]) / (PK[k] * (1 - PK[k]));
			thresh = k;
		}
	}
 
	//阈值处理
	src.copyTo(dst);
	for (int i = 0; i < r; ++i){
	    uchar* ptr = dst.ptr<uchar>(i);
		for (int j = 0; j < c; ++j){
			if (ptr[j]> thresh)
				ptr[j] = 255;
			else
				ptr[j] = 0;
		}
	}
	return thresh;
}
 
 
int main(){
	cv::Mat src = cv::imread("I:\\Learning-and-Practice\\2019Change\\Image process algorithm\\Img\\Fig1039(a)(polymersomes).tif");
	if (src.empty()){
		return -1;
	}
	if (src.channels() > 1)
		cv::cvtColor(src, src, CV_RGB2GRAY);
 
	cv::Mat dst,dst2;
	int thresh=0;
	double t2 = (double)cv::getTickCount();
	thresh=Otsu(src , dst, thresh); //Otsu
	std::cout << "Mythresh=" << thresh << std::endl;
	t2 = (double)cv::getTickCount() - t2;
	double time2 = (t2 *1000.) / ((double)cv::getTickFrequency());
	std::cout << "my_process=" << time2 << " ms. " << std::endl << std::endl;
    double  Otsu = 0;
 
	Otsu=cv::threshold(src, dst2, Otsu, 255, CV_THRESH_OTSU + CV_THRESH_BINARY);
	std::cout << "OpenCVthresh=" << Otsu << std::endl;
 
	cv::namedWindow("src", CV_WINDOW_NORMAL);
	cv::imshow("src", src);
	cv::namedWindow("dst", CV_WINDOW_NORMAL);
	cv::imshow("dst", dst);
	cv::namedWindow("dst2", CV_WINDOW_NORMAL);
	cv::imshow("dst2", dst2);
	//cv::imwrite("I:\\Learning-and-Practice\\2019Change\\Image process algorithm\\Image Filtering\\MeanFilter\\TXT.jpg",dst);
	cv::waitKey(0);
}
```

* 2、自适应阈值

```cpp
#include <iostream>
#include <opencv2/core.hpp>
#include <opencv2/highgui.hpp>
#include <opencv2/imgproc.hpp>
 
enum adaptiveMethod{meanFilter,gaaussianFilter,medianFilter};
 
void AdaptiveThreshold(cv::Mat& src, cv::Mat& dst, double Maxval, int Subsize, double c, adaptiveMethod method = meanFilter){
 
	if (src.channels() > 1)
		cv::cvtColor(src, src, CV_RGB2GRAY);
 
	cv::Mat smooth;
	switch (method)
	{
	case  meanFilter:
		cv::blur(src, smooth, cv::Size(Subsize, Subsize));  //均值滤波
		break;
	case gaaussianFilter:
		cv::GaussianBlur(src, smooth, cv::Size(Subsize, Subsize),0,0); //高斯滤波
		break;
	case medianFilter:
		cv::medianBlur(src, smooth, Subsize);   //中值滤波
		break;
	default:
		break;
	}
 
	smooth = smooth - c;
	
	//阈值处理
	src.copyTo(dst);
	for (int r = 0; r < src.rows;++r){
		const uchar* srcptr = src.ptr<uchar>(r);
		const uchar* smoothptr = smooth.ptr<uchar>(r);
		uchar* dstptr = dst.ptr<uchar>(r);
		for (int c = 0; c < src.cols; ++c){
			if (srcptr[c]>smoothptr[c]){
				dstptr[c] = Maxval;
			}
			else
				dstptr[c] = 0;
		}
	}
 
}
 
int main(){
	cv::Mat src = cv::imread("I:\\Learning-and-Practice\\2019Change\\Image process algorithm\\Img\\Fig1049(a)(spot_shaded_text_image).tif");
	if (src.empty()){
		return -1;
	}
	if (src.channels() > 1)
		cv::cvtColor(src, src, CV_RGB2GRAY);
 
	cv::Mat dst, dst2;
	double t2 = (double)cv::getTickCount();
	AdaptiveThreshold(src, dst, 255, 21, 10, meanFilter);  //
	t2 = (double)cv::getTickCount() - t2;
	double time2 = (t2 *1000.) / ((double)cv::getTickFrequency());
	std::cout << "my_process=" << time2 << " ms. " << std::endl << std::endl;
 
 
	cv::adaptiveThreshold(src, dst2, 255, cv::ADAPTIVE_THRESH_MEAN_C, cv::THRESH_BINARY, 21, 10);
 
 
	cv::namedWindow("src", CV_WINDOW_NORMAL);
	cv::imshow("src", src);
	cv::namedWindow("dst", CV_WINDOW_NORMAL);
	cv::imshow("dst", dst);
	cv::namedWindow("dst2", CV_WINDOW_NORMAL);
	cv::imshow("dst2", dst2);
	//cv::imwrite("I:\\Learning-and-Practice\\2019Change\\Image process algorithm\\Image Filtering\\MeanFilter\\TXT.jpg",dst);
	cv::waitKey(0);
}
```
### 效果
* 1、大津阈值

![Image](https://img-blog.csdnimg.cn/2020041222365393.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70)

* 2、自适应阈值

![Image](https://img-blog.csdnimg.cn/20200412223717279.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDY0NzgxOQ==,size_16,color_FFFFFF,t_70)

###  相关技术文档、博客、教材、项目推荐
opencv文档: [https://docs.opencv.org/3.1.0/d7/d1b/group__imgproc__misc.html#gae8a4a146d1ca78c626a53577199e9c57](https://docs.opencv.org/3.1.0/d7/d1b/group__imgproc__misc.html#gae8a4a146d1ca78c626a53577199e9c57)  
博客：[https://blog.csdn.net/weixin_40647819/article/details/90179953](https://blog.csdn.net/weixin_40647819/article/details/90179953)  
           [https://blog.csdn.net/weixin_40647819/article/details/90213858](https://blog.csdn.net/weixin_40647819/article/details/90213858)     
python版本：[https://www.kancloud.cn/aollo/aolloopencv/267591](https://www.kancloud.cn/aollo/aolloopencv/267591)                    [http://www.woshicver.com/FifthSection/4_3_%E5%9B%BE%E5%83%8F%E9%98%88%E5%80%BC/ ](http://www.woshicver.com/FifthSection/4_3_%E5%9B%BE%E5%83%8F%E9%98%88%E5%80%BC/ )


## 5.6 总结 

该部分对两种经典阈值分割方法进行了介绍，读者可根据提供的资料进行学习，然后参考示例代码自行实现。Otsu的二值化有一些优化方法，读者可以尝试学习并实现。
  
---
**Task05 阈值分割/二值化END.**

--- ***By: 小武***

>博客：[https://blog.csdn.net/weixin_40647819](https://blog.csdn.net/weixin_40647819)


**关于Datawhale**：

>Datawhale是一个专注于数据科学与AI领域的开源组织，汇集了众多领域院校和知名企业的优秀学习者，聚合了一群有开源精神和探索精神的团队成员。Datawhale以“for the learner，和学习者一起成长”为愿景，鼓励真实地展现自我、开放包容、互信互助、敢于试错和勇于担当。同时Datawhale 用开源的理念去探索开源内容、开源学习和开源方案，赋能人才培养，助力人才成长，建立起人与人，人与知识，人与企业和人与未来的联结。

