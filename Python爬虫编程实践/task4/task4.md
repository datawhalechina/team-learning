# task4

1. 了解ajax加载
2. 通过chrome的开发者工具，监控网络请求，并分析
3. 用selenium完成爬虫
4. 具体流程如下：
<br>用selenium爬取https://news.qq.com/ 的热点精选
![1585810800%281%29.png](1585810800%281%29.png)
热点精选至少爬50个出来，存储成csv
每一行如下
标号（从1开始）,标题,链接,...（前三个为必做，后面内容可以自己加）
![1585810759%281%29.png](1585810759%281%29.png)

# 进阶加餐-知乎爬虫

链接如下
<br>https://www.zhihu.com/search?q=Datawhale&utm_content=search_history&type=content
<br>用requests库实现，不能用selenium网页自动化，不仅要爬出第一页的内容，需要爬出所有的内容，给的链接仅仅是第一页的内容
<br>提示：
<br>该链接需要登录，可通过github等，搜索知乎登录的代码实现，并理解其中的逻辑，此任务允许复制粘贴代码
<br>与上面ajax加载类似，这次的ajax加载需要用requests完成爬取，最终存储样式随意，但是通过Chrome的开发者工具，分析出ajax的流程需要写出来
![1585811566%281%29.png](1585811566%281%29.png)
