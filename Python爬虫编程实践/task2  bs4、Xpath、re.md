# **Task02 学习内容：** bs4 xpath re
## 2.1 Beautiful Soup库入门
### 目标：
- 2.1.1 Beautiful Soup库的基本元素
- 2.1.2 基于bs4库的HTML内容遍历方法
- 2.1.3 基于bs4库的HTML内容的查找方法
- 2.1.4 实战：中国大学排名定向爬取

## 2.2 学习xpath
### 目标：
- 2.2.1 Xpath常用的路径表达式
- 2.2.2 使用lxml解析
- 2.2.3 实战：爬取丁香园-用户名和回复内容

## 2.3 学习正则表达式re
### 目标：
- 2.3.1 为什么使用正则表达式？
- 2.3.2 正则表达式语法
- 2.3.4 实战：淘宝商品比价定向爬虫

## **2.1 Beautiful Soup库入门**

1. 学习beautifulsoup基础知识。

2. 使用beautifulsoup解析HTML页面。

    - Beautiful Soup 是一个HTML/XML 的解析器，主要用于解析和提取 HTML/XML 数据。 
    - 它基于HTML DOM 的，会载入整个文档，解析整个DOM树，因此时间和内存开销都会大很多，所以性能要低于lxml。 
    - BeautifulSoup 用来解析 HTML 比较简单，API非常人性化，支持CSS选择器、Python标准库中的HTML解析器，也支持 lxml 的 XML解析器。
    - 虽然说BeautifulSoup4 简单容易比较上手，但是匹配效率还是远远不如正则以及xpath的，一般不推荐使用，推荐正则的使用。


- 第一步：pip install beautifulsoup4 ，万事开头难，先安装 beautifulsoup4，安装成功后就完成了第一步。

- 第二步：导入from bs4 import BeautifulSoup

- 第三步：创建 Beautiful Soup对象   soup = BeautifulSoup(html，'html.parser') 


### 2.1.1 Beautiful  Soup库的基本元素

1. Beautiful Soup库的理解：
    Beautiful Soup库是解析、遍历、维护“标签树”的功能库，对应一个HTML/XML文档的全部内容

2. BeautifulSoup类的基本元素:
    - `Tag 标签，最基本的信息组织单元，分别用<>和</>标明开头和结尾；`
    - `Name 标签的名字，<p>…</p>的名字是'p'，格式：<tag>.name;`
    - `Attributes 标签的属性，字典形式组织，格式：<tag>.attrs;`
    - `NavigableString 标签内非属性字符串，<>…</>中字符串，格式：<tag>.string;`
    - `Comment 标签内字符串的注释部分，一种特殊的Comment类型;`


```python
# 导入bs4库
from bs4 import BeautifulSoup
import requests # 抓取页面

r = requests.get('https://python123.io/ws/demo.html') # Demo网址
demo = r.text  # 抓取的数据
demo
```




    '<html><head><title>This is a python demo page</title></head>\r\n<body>\r\n<p class="title"><b>The demo python introduces several python courses.</b></p>\r\n<p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:\r\n<a href="http://www.icourse163.org/course/BIT-268001" class="py1" id="link1">Basic Python</a> and <a href="http://www.icourse163.org/course/BIT-1001870001" class="py2" id="link2">Advanced Python</a>.</p>\r\n</body></html>'




```python
# 解析HTML页面
soup = BeautifulSoup(demo, 'html.parser')  # 抓取的页面数据；bs4的解析器
# 有层次感的输出解析后的HTML页面
print(soup.prettify())
```




    '<html>\n <head>\n  <title>\n   This is a python demo page\n  </title>\n </head>\n <body>\n  <p class="title">\n   <b>\n    The demo python introduces several python courses.\n   </b>\n  </p>\n  <p class="course">\n   Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:\n   <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">\n    Basic Python\n   </a>\n   and\n   <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">\n    Advanced Python\n   </a>\n   .\n  </p>\n </body>\n</html>'



####  `1）标签，用soup.<tag>访问获得:`
- `当HTML文档中存在多个相同<tag>对应内容时，soup.<tag>返回第一个`


```python
soup.a # 访问标签a
```




    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a>




```python
soup.title
```




    <title>This is a python demo page</title>



#### `2）标签的名字:每个<tag>都有自己的名字，通过soup.<tag>.name获取，字符串类型`


```python
soup.a.name
```




    'a'




```python
soup.a.parent.name
```




    'p'




```python
soup.p.parent.name
```




    'body'



#### `3)标签的属性,一个<tag>可以有0或多个属性，字典类型,soup.<tag>.attrs`


```python
tag = soup.a
print(tag.attrs)
print(tag.attrs['class'])
print(type(tag.attrs))
```

    {'href': 'http://www.icourse163.org/course/BIT-268001', 'class': ['py1'], 'id': 'link1'}
    ['py1']
    <class 'dict'>


#### `4)Attributes:标签内非属性字符串,格式：soup.<tag>.string, NavigableString可以跨越多个层次`


```python
print(soup.a.string)
print(type(soup.a.string))
```

    Basic Python
    <class 'bs4.element.NavigableString'>


#### `5）NavigableString:标签内字符串的注释部分，Comment是一种特殊类型(有-->)`


```python
print(type(soup.p.string))
```

    <class 'bs4.element.NavigableString'>


#### 6)  .prettify()为HTML文本<>及其内容增加更加'\n',有层次感的输出
####      .prettify()可用于标签，方法：`<tag>`.prettify()


```python
print(soup.prettify())
```

    <html>
     <head>
      <title>
       This is a python demo page
      </title>
     </head>
     <body>
      <p class="title">
       <b>
        The demo python introduces several python courses.
       </b>
      </p>
      <p class="course">
       Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
       <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">
        Basic Python
       </a>
       and
       <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">
        Advanced Python
       </a>
       .
      </p>
     </body>
    </html>



```python
print(soup.a.prettify())
```

    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">
     Basic Python
    </a>


​    

#### 7)bs4库将任何HTML输入都变成utf‐8编码
#### Python 3.x默认支持编码是utf‐8,解析无障碍


```python
newsoup = BeautifulSoup('<a>中文</a>', 'html.parser')
print(newsoup.prettify())
```

    <a>
     中文
    </a>


### 2.1.2  基于bs4库的HTML内容遍历方法
HTML基本格式:`<>…</>`构成了所属关系，形成了标签的树形结构
- 标签树的下行遍历
    * .contents 子节点的列表，将`<tag>`所有儿子节点存入列表
    * .children 子节点的迭代类型，与.contents类似，用于循环遍历儿子节点
    * .descendants 子孙节点的迭代类型，包含所有子孙节点，用于循环遍历
- 标签树的上行遍
    * .parent 节点的父亲标签
    * .parents 节点先辈标签的迭代类型，用于循环遍历先辈节点
- 标签树的平行遍历
    * .next_sibling 返回按照HTML文本顺序的下一个平行节点标签
    * .previous_sibling 返回按照HTML文本顺序的上一个平行节点标签
    * .next_siblings 迭代类型，返回按照HTML文本顺序的后续所有平行节点标签
    * .previous_siblings 迭代类型，返回按照HTML文本顺序的前续所有平行节点标签
* 详见：https://www.cnblogs.com/mengxiaoleng/p/11585754.html#_label0 

#### 标签树的下行遍历


```python
import requests
from bs4 import BeautifulSoup

r=requests.get('http://python123.io/ws/demo.html')
demo=r.text
soup=BeautifulSoup(demo,'html.parser')
```


```python
print(soup.contents)# 获取整个标签树的儿子节点
```

    [<html><head><title>This is a python demo page</title></head>
    <body>
    <p class="title"><b>The demo python introduces several python courses.</b></p>
    <p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>
    </body></html>]



```python
print(soup.body.content)#返回标签树的body标签下的节点
```

    None



```python
print(soup.head)#返回head标签
```

    <head><title>This is a python demo page</title></head>



```python
for child in soup.body.children:#遍历儿子节点
    print(child)
```


​    
​    <p class="title"><b>The demo python introduces several python courses.</b></p>


​    
​    <p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
​    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>


​    
​    


```python
for child in soup.body.descendants:#遍历子孙节点
    print(child)
```


​    
​    <p class="title"><b>The demo python introduces several python courses.</b></p>
​    <b>The demo python introduces several python courses.</b>
​    The demo python introduces several python courses.


​    
​    <p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
​    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>
​    Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
​    
    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a>
    Basic Python
     and 
    <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>
    Advanced Python
    .


​    
​    

#### 标签树的上行遍历


```python
soup.title.parent
```




    <head><title>This is a python demo page</title></head>




```python
soup.title.parent
```




    <head><title>This is a python demo page</title></head>




```python
soup.parent
```


```python
for parent in soup.a.parents: # 遍历先辈的信息
    if parent is None:
        print(parent)
    else:
        print(parent.name)
```

    p
    body
    html
    [document]


#### 标签树的平行遍历
注意：
- 标签树的平行遍历是有条件的
- 平行遍历发生在同一个父亲节点的各节点之间
- 标签中的内容也构成了节点


```python
print(soup.a.next_sibling)#a标签的下一个标签
```

     and 



```python
print(soup.a.next_sibling.next_sibling)#a标签的下一个标签的下一个标签
```

    <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>



```python
print(soup.a.previous_sibling)#a标签的前一个标签
```

    Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:


​    


```python
print(soup.a.previous_sibling.previous_sibling)#a标签的前一个标签的前一个标签
```

    None



```python
for sibling in soup.a.next_siblings:#遍历后续节点
    print(sibling)
```

     and 
    <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>
    .



```python
for sibling in soup.a.previous_sibling:#遍历之前的节点
    print(sibling)
```

### 2.1.3 基于bs4库的HTML内容的查找方法
- <>.find_all(name, attrs, recursive, string, **kwargs) 
    - 参数：
    - ∙ name : 对标签名称的检索字符串
    - ∙ attrs: 对标签属性值的检索字符串，可标注属性检索
    - ∙ recursive: 是否对子孙全部检索，默认True
    - ∙ string: <>…</>中字符串区域的检索字符串
        - 简写：
        - `<tag>`(..) 等价于 `<tag>`.find_all(..)
        - soup(..) 等价于 soup.find_all(..)
- 扩展方法：
    - <>.find() 搜索且只返回一个结果，同.find_all()参数
    - <>.find_parents() 在先辈节点中搜索，返回列表类型，同.find_all()参数
    - <>.find_parent() 在先辈节点中返回一个结果，同.find()参数
    - <>.find_next_siblings() 在后续平行节点中搜索，返回列表类型，同.find_all()参数
    - <>.find_next_sibling() 在后续平行节点中返回一个结果，同.find()参数
    - <>.find_previous_siblings() 在前序平行节点中搜索，返回列表类型，同.find_all()参数
    - <>.find_previous_sibling() 在前序平行节点中返回一个结果，同.find()参数


```python
import requests
from bs4 import BeautifulSoup

r = requests.get('http://python123.io/ws/demo.html')
demo = r.text
soup = BeautifulSoup(demo,'html.parser')
soup
```




    <html><head><title>This is a python demo page</title></head>
    <body>
    <p class="title"><b>The demo python introduces several python courses.</b></p>
    <p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
    <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>
    </body></html>




```python
# name : 对标签名称的检索字符串
soup.find_all('a')
```




    [<a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a>,
     <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>]




```python
soup.find_all(['a', 'p'])
```




    [<p class="title"><b>The demo python introduces several python courses.</b></p>,
     <p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
     <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>,
     <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a>,
     <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>]




```python
# attrs: 对标签属性值的检索字符串，可标注属性检索
soup.find_all("p","course")
```




    [<p class="course">Python is a wonderful general-purpose programming language. You can learn Python from novice to professional by tracking the following courses:
     <a class="py1" href="http://www.icourse163.org/course/BIT-268001" id="link1">Basic Python</a> and <a class="py2" href="http://www.icourse163.org/course/BIT-1001870001" id="link2">Advanced Python</a>.</p>]




```python
soup.find_all(id="link") # 完全匹配才能匹配到
```




    []




```python
#  recursive: 是否对子孙全部检索，默认True
soup.find_all('p',recursive=False)
```




    []




```python
# string: <>…</>中字符串区域的检索字符串
soup.find_all(string = "Basic Python") # 完全匹配才能匹配到
```




    []



### 2.1.4 实战：中国大学排名定向爬取
- 爬取url：http://www.zuihaodaxue.cn/zuihaodaxuepaiming2019.html
- 爬取思路：
    1. 从网络上获取大学排名网页内容
    2. 提取网页内容中信息到合适的数据结构（二维数组）-排名，学校名称，总分
    3. 利用数据结构展示并输出结果


```python
# 导入库
import requests
from bs4 import BeautifulSoup
import bs4
```

#### 1. 从网络上获取大学排名网页内容


```python
def getHTMLText(url):
    try:
        r = requests.get(url, timeout=30) 
        r.raise_for_status()
        r.encoding = r.apparent_encoding
        return r.text
    except:
        return ""
```

#### 2. 提取网页内容中信息到合适的数据结构（二维数组）
1. 查看网页源代码，观察并定位到需要爬取内容的标签；
2. 使用bs4的查找方法提取所需信息-'排名，学校名称，总分'


```python
def fillUnivList(ulist, html):
    soup = BeautifulSoup(html, "html.parser")
    for tr in soup.find('tbody').children: 
        if isinstance(tr, bs4.element.Tag):
            tds = tr('td')
            # 根据实际提取需要的内容，
            ulist.append([tds[0].string, tds[1].string, tds[3].string])  
```

#### 3. 利用数据结构展示并输出结果


```python
# 对中英文混排输出问题进行优化:对format()，设定宽度和添加参数chr(12288)
def printUnivList(ulist, num=20):
    tplt = "{0:^10}\t{1:{3}^10}\t{2:^10}"
    print(tplt.format('排名', '学校名称', '总分', chr(12288)))
    for i in range(num):
        u = ulist[i]
        print(tplt.format(u[0], u[1], u[2], chr(12288)))
```


```python
u_info = [] # 存储爬取结果的容器
url = 'http://www.zuihaodaxue.cn/zuihaodaxuepaiming2019.html'
```


```python
html = getHTMLText(url)
html
```




​    




```python
fillUnivList(u_info, html) # 爬取
```


```python
printUnivList(u_info, num=30) # 打印输出30个信息
```

        排名    	　　　学校名称　　　	    总分    
        1     	　　　清华大学　　　	   94.6   
        2     	　　　北京大学　　　	   76.5   
        3     	　　　浙江大学　　　	   72.9   
        4     	　　上海交通大学　　	   72.1   
        5     	　　　复旦大学　　　	   65.6   
        6     	　中国科学技术大学　	   60.9   
        7     	　　华中科技大学　　	   58.9   
        7     	　　　南京大学　　　	   58.9   
        9     	　　　中山大学　　　	   58.2   
        10    	　哈尔滨工业大学　　	   56.7   
        11    	　北京航空航天大学　	   56.3   
        12    	　　　武汉大学　　　	   56.2   
        13    	　　　同济大学　　　	   55.7   
        14    	　　西安交通大学　　	   55.0   
        15    	　　　四川大学　　　	   54.4   
        16    	　　北京理工大学　　	   54.0   
        17    	　　　东南大学　　　	   53.6   
        18    	　　　南开大学　　　	   52.8   
        19    	　　　天津大学　　　	   52.3   
        20    	　　华南理工大学　　	   52.0   
        21    	　　　中南大学　　　	   50.3   
        22    	　　北京师范大学　　	   49.7   
        23    	　　　山东大学　　　	   49.1   
        23    	　　　厦门大学　　　	   49.1   
        25    	　　　吉林大学　　　	   48.9   
        26    	　　大连理工大学　　	   48.6   
        27    	　　电子科技大学　　	   48.4   
        28    	　　　湖南大学　　　	   48.1   
        29    	　　　苏州大学　　　	   47.3   
        30    	　　西北工业大学　　	   46.7   


## **2.2 学习xpath**

###  学习目标：

1. 学习xpath，使用lxml+xpath提取内容。

2. 使用xpath提取丁香园论坛的回复内容。

- 抓取丁香园网页：[http://www.dxy.cn/bbs/thread/626626#626626](http://www.dxy.cn/bbs/thread/626626#626626) 。


### 2.2.1 Xpath常用的路径表达式：

- XPath即为XML路径语言（XML Path Language），它是一种用来确定XML文档中某部分位置的语言。
- 在XPath中，有七种类型的节点：元素、属性、文本、命名空间、处理指令、注释以及文档（根）节点。
- XML文档是被作为节点树来对待的。

XPath使用路径表达式在XML文档中选取节点。节点是通过沿着路径选取的。下面列出了最常用的路径表达式：

- nodename       选取此节点的所有子节点。
- /           从根节点选取。
- //	        从匹配选择的当前节点选择文档中的节点，而不考虑它们的位置。
- .	           选取当前节点。
- ..	         选取当前节点的父节点。
- @	           选取属性。
- /text()        提取标签下面的文本内容
    - 如：
    - /标签名               逐层提取
    - /标签名               提取所有名为<>的标签
    - //标签名[@属性=“属性值”]   提取包含属性为属性值的标签
    - @属性名               代表取某个属性名的属性值
    
- 详细学习：https://www.cnblogs.com/gaojun/archive/2012/08/11/2633908.html

### 2.2.2 使用lxml解析

- 导入库：from lxml import etree

- lxml将html文本转成xml对象
    - tree = etree.HTML(html)
    
- 用户名称：tree.xpath(’//div[@class=“auth”]/a/text()’)
- 回复内容：tree.xpath(’//td[@class=“postbody”]’) 因为回复内容中有换行等标签，所以需要用string()来获取数据。
    - string()的详细见链接：https://www.cnblogs.com/CYHISTW/p/12312570.html
    
- Xpath中text()，string()，data()的区别如下：
    - text()仅仅返回所指元素的文本内容。
    - string()函数会得到所指元素的所有节点文本内容，这些文本讲会被拼接成一个字符串。
    - data()大多数时候，data()函数和string()函数通用，而且不建议经常使用data()函数，有数据表明，该函数会影响XPath的性能。


### 2.2.3 实战：爬取丁香园-用户名和回复内容

- 爬取思路：
    1. 获取url的html
    2. lxml解析html
    3. 利用Xpath表达式获取user和content
    4. 保存爬取的内容


```python
# 导入库
from lxml import etree
import requests

url = "http://www.dxy.cn/bbs/thread/626626#626626"
```

#### 1. 获取url的html


```python
req = requests.get(url)
html = req.text
# html
```

#### 2. lxml解析html


```python
tree = etree.HTML(html) 
tree
```




    <Element html at 0x2313e5ac188>



#### 3. 利用Xpath表达式获取user和content（重点）



```python
user = tree.xpath('//div[@class="auth"]/a/text()')
# print(user)
content = tree.xpath('//td[@class="postbody"]')
```

#### 4. 保存爬取的内容


```python
results = []
for i in range(0, len(user)):
    # print(user[i].strip()+":"+content[i].xpath('string(.)').strip())
    # print("*"*80)
    # 因为回复内容中有换行等标签，所以需要用string()来获取数据
    results.append(user[i].strip() + ":  " + content[i].xpath('string(.)').strip())
```


```python
# 打印爬取的结果
for i,result in zip(range(0, len(user)),results):
    print("user"+ str(i+1) + "-" + result)
    print("*"*100)
```

    user1-楼医生:  我遇到一个“怪”病人，向大家请教。她，42岁。反复惊吓后晕厥30余年。每次受响声惊吓后发生跌倒，短暂意识丧失。无逆行性遗忘，无抽搐，无口吐白沫，无大小便失禁。多次跌倒致外伤。婴儿时有惊厥史。入院查体无殊。ECG、24小时动态心电图无殊；头颅MRI示小软化灶；脑电图无殊。入院后有数次类似发作。请问该患者该做何诊断，还需做什么检查，治疗方案怎样？
    ****************************************************************************************************
    user2-lion000:  从发作的症状上比较符合血管迷走神经性晕厥，直立倾斜试验能协助诊断。在行直立倾斜实验前应该做常规的体格检查、ECG、UCG、holter和X-ray胸片除外器质性心脏病。贴一篇“口服氨酰心安和依那普利治疗血管迷走性晕厥的疗效观察”作者：林文华 任自文 丁燕生http://www.ccheart.com.cn/ccheart_site/Templates/jieru/200011/1-1.htm
    ****************************************************************************************************
    user3-xghrh:  同意lion000版主的观点：如果此患者随着年龄的增长，其发作频率逐渐减少且更加支持，不知此患者有无这一特点。入院后的HOLTER及血压监测对此患者只能是一种安慰性的检查，因在这些检查过程中患者发病的机会不是太大，当然不排除正好发作的情况。对此患者应常规作直立倾斜试验，如果没有诱发出，再考虑有无可能是其他原因所致的意识障碍，如室性心动过速等，但这需要电生理尤其是心腔内电生理的检查，毕竟是有一种创伤性方法。因在外地，下面一篇文章可能对您有助，请您自己查找一下。心理应激事件诱发血管迷走性晕厥1例 ，杨峻青、吴沃栋、张瑞云，中国神经精神疾病杂志， 2002 Vol.28 No.2
    ****************************************************************************************************
    user4-keys:  该例不排除精神因素导致的，因为每次均在受惊吓后出现。当然，在作出此诊断前，应完善相关检查，如头颅MIR(MRA),直立倾斜试验等。
    ****************************************************************************************************



```python
results = []
for i in range(0, len(user)):
    # print(user[i].strip()+":"+content[i].xpath('string(.)').strip())
    # print("*"*80)
    # 因为回复内容中有换行等标签，所以需要用string()来获取数据
    results.append(user[i].strip() + ":  " + content[i].xpath('string(.)').strip())
```

## **2.3 学习正则表达式 re**

### 2.3.1 为什么使用正则表达式？
典型的搜索和替换操作要求您提供与预期的搜索结果匹配的确切文本。虽然这种技术对于对静态文本执行简单搜索和替换任务可能已经足够了，但它缺乏灵活性，若采用这种方法搜索动态文本，即使不是不可能，至少也会变得很困难。

通过使用正则表达式，可以：

    - 测试字符串内的模式。
        例如，可以测试输入字符串，以查看字符串内是否出现电话号码模式或信用卡号码模式。这称为数据验证。
    - 替换文本。
        可以使用正则表达式来识别文档中的特定文本，完全删除该文本或者用其他文本替换它。
    - 基于模式匹配从字符串中提取子字符串。
        可以查找文档内或输入域内特定的文本。
可以使用正则表达式来搜索和替换标记。

#### 使用正则表达式的优势是什么？  **简洁**
- 正则表达式是用来简洁表达一组字符串的表达式
- 正则表达式是一种通用的字符串表达框架
- 正则表达式是一种针对字符串表达“简洁”和“特征”思想的工具
- 正则表达式可以用来判断某字符串的特征归属

#### 正则表达式在文本处理中十分常用：
- 同时查找或替换一组字符串
- 匹配字符串的全部或部分(主要)

### 2.3.2 正则表达式语法

正则表达式语法由字符和操作符构成:

- 常用操作符
    - `.` 表示任何单个字符
    - `[ ]` 字符集，对单个字符给出取值范围 ，如`[abc]`表示a、b、c，`[a‐z]`表示a到z单个字符
    - `[^ ]` 非字符集，对单个字符给出排除范围 ，如`[^abc]`表示非a或b或c的单个字符
    - `*` 前一个字符0次或无限次扩展，如abc* 表示 ab、abc、abcc、abccc等 
    - `+` 前一个字符1次或无限次扩展 ，如abc+ 表示 abc、abcc、abccc等 
    - `?` 前一个字符0次或1次扩展 ，如abc? 表示 ab、abc
    - `|` 左右表达式任意一个 ，如abc|def 表示 abc、def

    - `{m}` 扩展前一个字符m次 ，如ab{2}c表示abbc
    - `{m,n}` 扩展前一个字符m至n次（含n） ，如ab{1,2}c表示abc、abbc
    - `^` 匹配字符串开头 ，如^abc表示abc且在一个字符串的开头
    - `$` 匹配字符串结尾 ，如abc$表示abc且在一个字符串的结尾
    - `( )` 分组标记，内部只能使用 | 操作符 ，如(abc)表示abc，(abc|def)表示abc、def
    - `\d` 数字，等价于`[0‐9]`
    - `\w` 单词字符，等价于`[A‐Za‐z0‐9_]`

### 2.3.3 正则表达式re库的使用

- 调用方式：import re
- re库采用raw string类型表示正则表达式，表示为：r'text'，raw string是不包含对转义符再次转义的字符串;

#### re库的主要功能函数：

- re.search() 在一个字符串中搜索匹配正则表达式的第一个位置，返回match对象 
    - re.search(pattern, string, flags=0)
- re.match() 从一个字符串的开始位置起匹配正则表达式，返回match对象
    - re.match(pattern, string, flags=0)
- re.findall() 搜索字符串，以列表类型返回全部能匹配的子串
    - re.findall(pattern, string, flags=0)
- re.split() 将一个字符串按照正则表达式匹配结果进行分割，返回列表类型
    - re.split(pattern, string, maxsplit=0, flags=0)
- re.finditer() 搜索字符串，返回一个匹配结果的迭代类型，每个迭代元素是match对象
    - re.finditer(pattern, string, flags=0)
- re.sub() 在一个字符串中替换所有匹配正则表达式的子串，返回替换后的字符串
    - re.sub(pattern, repl, string, count=0, flags=0)

    -  flags : 正则表达式使用时的控制标记：
        - re.I -->  re.IGNORECASE :  忽略正则表达式的大小写，`[A‐Z]`能够匹配小写字符
        - re.M -->  re.MULTILINE :  正则表达式中的^操作符能够将给定字符串的每行当作匹配开始
        - re.S -->  re.DOTALL   :  正则表达式中的.操作符能够匹配所有字符，默认匹配除换行外的所有字符
        
#### re库的另一种等价用法（编译）

- regex = re.compile(pattern, flags=0)：将正则表达式的字符串形式编译成正则表达式对象

#### re 库的贪婪匹配和最小匹配

- `.*` Re库默认采用贪婪匹配，即输出匹配最长的子串
- `*?` 只要长度输出可能不同的，都可以通过在操作符后增加?变成最小匹配

### 2.3.4 实战：淘宝商品比价定向爬虫

- 爬取网址：https://s.taobao.com/search?q=书包&js=1&stats_click=search_radio_all%25
- 爬取思路：
    1. 提交商品搜索请求，循环获取页面
    2. 对于每个页面，提取商品名称和价格信息
    3. 将信息输出到屏幕上


```python
# 导入包
import requests
import re
```

#### 1. 提交商品搜索请求，循环获取页面


```python
def getHTMLText(url):
    """
    请求获取html，（字符串）
    :param url: 爬取网址
    :return: 字符串
    """
    try:
        # 添加头信息,
        kv = {
            'cookie': 'thw=cn; v=0; t=ab66dffdedcb481f77fd563809639584; cookie2=1f14e41c704ef58f8b66ff509d0d122e; _tb_token_=5e6bed8635536; cna=fGOnFZvieDECAXWIVi96eKju; unb=1864721683; sg=%E4%B8%8B3f; _l_g_=Ug%3D%3D; skt=83871ef3b7a49a0f; cookie1=BqeGegkL%2BLUif2jpoUcc6t6Ogy0RFtJuYXR4VHB7W0A%3D; csg=3f233d33; uc3=vt3=F8dBy3%2F50cpZbAursCI%3D&id2=UondEBnuqeCnfA%3D%3D&nk2=u%2F5wdRaOPk21wDx%2F&lg2=VFC%2FuZ9ayeYq2g%3D%3D; existShop=MTU2MjUyMzkyMw%3D%3D; tracknick=%5Cu4E36%5Cu541B%5Cu4E34%5Cu4E3F%5Cu5929%5Cu4E0B; lgc=%5Cu4E36%5Cu541B%5Cu4E34%5Cu4E3F%5Cu5929%5Cu4E0B; _cc_=WqG3DMC9EA%3D%3D; dnk=%5Cu4E36%5Cu541B%5Cu4E34%5Cu4E3F%5Cu5929%5Cu4E0B; _nk_=%5Cu4E36%5Cu541B%5Cu4E34%5Cu4E3F%5Cu5929%5Cu4E0B; cookie17=UondEBnuqeCnfA%3D%3D; tg=0; enc=2GbbFv3joWCJmxVZNFLPuxUUDA7QTpES2D5NF0D6T1EIvSUqKbx15CNrsn7nR9g%2Fz8gPUYbZEI95bhHG8M9pwA%3D%3D; hng=CN%7Czh-CN%7CCNY%7C156; mt=ci=32_1; alitrackid=www.taobao.com; lastalitrackid=www.taobao.com; swfstore=97213; x=e%3D1%26p%3D*%26s%3D0%26c%3D0%26f%3D0%26g%3D0%26t%3D0%26__ll%3D-1%26_ato%3D0; uc1=cookie16=UtASsssmPlP%2Ff1IHDsDaPRu%2BPw%3D%3D&cookie21=UIHiLt3xThH8t7YQouiW&cookie15=URm48syIIVrSKA%3D%3D&existShop=false&pas=0&cookie14=UoTaGqj%2FcX1yKw%3D%3D&tag=8&lng=zh_CN; JSESSIONID=A502D8EDDCE7B58F15F170380A767027; isg=BMnJJFqj8FrUHowu4yKyNXcd2PXjvpa98f4aQWs-RbDvsunEs2bNGLfj8BYE6lWA; l=cBTDZx2mqxnxDRr0BOCanurza77OSIRYYuPzaNbMi_5dd6T114_OkmrjfF96VjWdO2LB4G2npwJ9-etkZ1QoqpJRWkvP.; whl=-1%260%260%261562528831082',
            'user-agent': 'Mozilla/5.0'
        }
        r = requests.get(url, timeout=30, headers=kv)
        # r = requests.get(url, timeout=30)
        # print(r.status_code)
        r.raise_for_status()
        r.encoding = r.apparent_encoding
        return r.text
    except:
        return "爬取失败"
```

#### 2. 对于每个页面，提取商品名称和价格信息


```python
def parsePage(glist, html):
    '''
    解析网页，搜索需要的信息
    :param glist: 列表作为存储容器
    :param html: 由getHTMLText()得到的
    :return: 商品信息的列表
    '''
    try:
        # 使用正则表达式提取信息
        price_list = re.findall(r'\"view_price\"\:\"[\d\.]*\"', html)
        name_list = re.findall(r'\"raw_title\"\:\".*?\"', html)
        for i in range(len(price_list)):
            price = eval(price_list[i].split(":")[1])  #eval（）在此可以去掉""
            name = eval(name_list[i].split(":")[1])
            glist.append([price, name])
    except:
        print("解析失败")
```

#### 3. 将信息输出到屏幕上


```python
def printGoodList(glist):
    tplt = "{0:^4}\t{1:^6}\t{2:^10}"
    print(tplt.format("序号", "商品价格", "商品名称"))
    count = 0
    for g in glist:
        count = count + 1
        print(tplt.format(count, g[0], g[1]))
```


```python
# 根据页面url的变化寻找规律，构建爬取url
goods_name = "书包"  # 搜索商品类型
start_url = "https://s.taobao.com/search?q=" + goods_name
info_list = []
page = 3  # 爬取页面数量
```


```python
count = 0
for i in range(page):
    count += 1
    try:
        url = start_url + "&s=" + str(44 * i)
        html = getHTMLText(url)  # 爬取url
        parsePage(info_list, html) #解析HTML和爬取内容
        print("\r爬取页面当前进度: {:.2f}%".format(count * 100 / page), end="")  # 显示进度条
    except:
        continue
```

    爬取页面当前进度: 100.00%


```python
printGoodList(info_list)
```

    



```python

```
