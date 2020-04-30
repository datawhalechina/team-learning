# task4：腾讯新闻爬取

1. 了解ajax加载
[ajax教程-菜鸟教程](https://www.w3school.com.cn/ajax/index.asp)
2. 通过chrome的开发者工具，监控网络请求，并分析
[chrome开发者工具使用-谷歌](https://developers.google.cn/web/tools/chrome-devtools/)
3. 用selenium完成爬虫
[selenium with python](https://selenium-python.readthedocs.io/)
4. 具体流程如下：
<br>用selenium爬取https://news.qq.com/ 的热点精选
![1585810800%281%29.png](1585810800%281%29.png)
热点精选至少爬50个出来，存储成csv
每一行如下
标号（从1开始）,标题,链接,...（前三个为必做，后面内容可以自己加）
![1585810759%281%29.png](1585810759%281%29.png)





```python
import time
from  selenium import webdriver
driver=webdriver.Chrome(executable_path="D:\chromedriver\chromedriver.exe")
driver.get("https://news.qq.com")
#了解ajax加载
for i in range(1,100):
    # sleep一段时间，让ajax加载完成
    time.sleep(2)
    # 执行JavaScript代码，让滚动条向下滚动
    driver.execute_script("window.scrollTo(window.scrollX, %d);"%(i*200))
```


```python
from bs4 import BeautifulSoup
#得到经过ajax渲染后的页面的源代码，此时的页面已经包含了热点精选的内容
html=driver.page_source
bsObj=BeautifulSoup(html,"lxml")
```


```python
jxtits=bsObj.find_all("div",{"class":"jx-tit"})[0].find_next_sibling().find_all("li")
```


```python
print("index",",","title",",","url")
for i,jxtit in enumerate(jxtits):
#     print(jxtit)
    
    try:
        text=jxtit.find_all("img")[0]["alt"]
    except:
        text=jxtit.find_all("div",{"class":"lazyload-placeholder"})[0].text
    try:
        url=jxtit.find_all("a")[0]["href"]
    except:
        print(jxtit)
    print(i+1,",",text,",",url) 

```

