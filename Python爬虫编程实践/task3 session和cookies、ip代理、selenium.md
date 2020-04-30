# task3： session和cookies IP代理 selenium

# session和cookie

## 前置：动态网页和静态网页

### 静态网页

- 静态网页就是我们上一篇写的那种 html 页面，后缀为 `.html` 的这种文件，直接部署到或者是放到某个 web 容器上，就可以在浏览器通过链接直接访问到了，常用的 web 容器有 Nginx 、 Apache 、 Tomcat 、Weblogic 、 Jboss 、 Resin 等等，很多很多。举个例子：https://desmonday.github.io/，就是静态网页的代表，这种网页的内容是通过纯粹的 HTML 代码来书写，包括一些资源文件：图片、视频等内容的引入都是使用 HTML 标签来完成的。它的好处当然是加载速度快，编写简单，访问的时候对 web 容器基本上不会产生什么压力。但是缺点也很明显，可维护性比较差，不能根据参数动态的显示内容等等。有需求就会有发展么，这时动态网页就应运而生了

### 动态网页

- 大家常用的某宝、某东、拼夕夕等网站都是由动态网页组成的。
- 动态网页可以解析 URL 中的参数，或者是关联数据库中的数据，显示不同的网页内容。现在各位同学访问的网站大多数都是动态网站，它们不再简简单单是由 HTML 堆砌而成，可能是由 JSP 、 PHP 等语言编写的，当然，现在很多由前端框架编写而成的网页小编这里也归属为动态网页。
- 说到动态网页，各位同学可能使用频率最高的一个功能是登录，像各种电商类网站，肯定是登录了以后才能下单买东西。那么，问题来了，后面的服务端是如何知道当前这个人已经登录了呢？

## http1.0

- HTTP1.0的特点是无状态无链接的
- ![](https://cdn.geekdigging.com/python-spider/jd-http-1.png)

- 无状态就是指 HTTP 协议对于请求的发送处理是没有记忆功能的，也就是说每次 HTTP 请求到达服务端，服务端都不知道当前的客户端（浏览器）到底是一个什么状态。客户端向服务端发送请求后，服务端处理这个请求，然后将内容响应回客户端，完成一次交互，这个过程是完全相互独立的，服务端不会记录前后的状态变化，也就是缺少状态记录。这就产生了上面的问题，服务端如何知道当前在浏览器面前操作的这个人是谁？其实，在用户做登录操作的时候，服务端会下发一个类似于 token 凭证的东西返回至客户端（浏览器），有了这个凭证，才能保持登录状态。那么这个凭证是什么？

## session和cookies

- Session 是会话的意思，会话是产生在服务端的，用来保存当前用户的会话信息，而 Cookies 是保存在客户端（浏览器），有了 Cookie 以后，客户端（浏览器）再次访问服务端的时候，会将这个 Cookie 带上，这时，服务端可以通过 Cookie 来识别本次请求到底是谁在访问。

  可以简单理解为 Cookies 中保存了登录凭证，我们只要持有这个凭证，就可以在服务端保持一个登录状态。

  在爬虫中，有时候遇到需要登录才能访问的网页，只需要在登录后获取了 Cookies ，在下次访问的时候将登录后获取到的 Cookies 放在请求头中，这时，服务端就会认为我们的爬虫是一个正常登录用户。

### session

- 那么，Cookies 是如何保持会话状态的呢？

  在客户端（浏览器）第一次请求服务端的时候，服务端会返回一个请求头中带有 `Set-Cookie` 字段的响应给客户端（浏览器），用来标记是哪一个用户，客户端（浏览器）会把这个 Cookies 给保存起来。

  我们来使用工具 PostMan 来访问下某东的登录页，看下返回的响应头：

  ![](https://cdn.geekdigging.com/python-spider/jd_login-http-response.png)

- 当我们输入好用户名和密码时，客户端会将这个 Cookies 放在请求头一起发送给服务端，这时，服务端就知道是谁在进行登录操作，并且可以判断这个人输入的用户名和密码对不对，如果输入正确，则在服务端的 Session 记录一下这个人已经登录成功了，下次再请求的时候这个人就是登录状态了。

  如果客户端传给服务端的 Cookies 是无效的，或者这个 Cookies 根本不是由这个服务端下发的，或者这个 Cookies 已经过期了，那么接下里的请求将不再能访问需要登录后才能访问的页面。

  所以， Session 和 Cookies 之间是需要相互配合的，一个在服务端，一个在客户端。

### cookies

- 我们还是打开某东的网站，看下这些 Cookies到底有哪些内容：

  

  [![小白学 Python 爬虫（10）：Session 和 Cookies](https://cdn.geekdigging.com/python-spider/jd-cookies.png)
  
  ](https://cdn.geekdigging.com/python-spider/jd-cookies.png)

- 具体操作方式还是在 Chrome 中按 F12 打开开发者工具，选择 Application 标签，点开 Cookies 这一栏。

  - Name：这个是 Cookie 的名字。一旦创建，该名称便不可更改。
  - Value：这个是 Cookie 的值。
  - Domain：这个是可以访问该 Cookie 的域名。例如，如果设置为 .jd.com ，则所有以 jd.com ，结尾的域名都可以访问该Cookie。
  - Max Age：Cookie 失效的时间，单位为秒，也常和 Expires 一起使用。 Max Age 如果为正数，则在 Max Age 秒之后失效，如果为负数，则关闭浏览器时 Cookie 即失效，浏览器也不会保存该 Cookie 。
  - Path：Cookie 的使用路径。如果设置为 /path/ ，则只有路径为 /path/ 的页面可以访问该 Cookie 。如果设置为 / ，则本域名下的所有页面都可以访问该 Cookie 。
  - Size：Cookie 的大小。
  - HTTPOnly：如果此项打勾，那么通过 JS 脚本将无法读取到 Cookie 信息，这样能有效的防止 XSS 攻击，窃取 Cookie 内容，可以增加 Cookie 的安全性。
  - Secure：如果此项打勾，那么这个 Cookie 只能用 HTTPS 协议发送给服务器，用 HTTP 协议是不发送的。

  那么有的网站为什么这次关闭了，下次打开的时候还是登录状态呢？

  这就要说到 Cookie 的持久化了，其实也不能说是持久化，就是 Cookie 失效的时间设置的长一点，比如直接设置到 2099 年失效，这样，在浏览器关闭后，这个 Cookie 是会保存在我们的硬盘中的，下次打开浏览器，会再从我们的硬盘中将这个 Cookie 读取出来，用来维持用户的会话状态。

  第二个问题产生了，服务端的会话也会无限的维持下去么，当然不会，这就要在 Cookie 和 Session 上做文章了， Cookie 中可以使用加密的方式将用户名记录下来，在下次将 Cookies 读取出来由请求发送到服务端后，服务端悄悄的自己创建一个用户已经登录的会话，这样我们在客户端看起来就好像这个登录会话是一直保持的。

## 一个重要概念

- 当我们关闭浏览器的时候会自动销毁服务端的会话，这个是错误的，因为在关闭浏览器的时候，浏览器并不会额外的通知服务端说，我要关闭了，你把和我的会话销毁掉吧。

  因为服务端的会话是保存在内存中的，虽然一个会话不会很大，但是架不住会话多啊，硬件毕竟是会有限制的，不能无限扩充下去的，所以在服务端设置会话的过期时间就非常有必要。

  当然，有没有方式能让浏览器在关闭的时候同步的关闭服务端的会话，当然是可以的，我们可以通过脚本语言 JS 来监听浏览器关闭的动作，当浏览器触发关闭动作的时候，由 JS 像服务端发起一个请求来通知服务端销毁会话。

  由于不同的浏览器对 JS 事件的实现机制不一致，不一定保证 JS 能监听到浏览器关闭的动作，所以现在常用的方式还是在服务端自己设置会话的过期时间

# 实战案例：模拟登录163


```python
import time

from selenium import webdriver
from selenium.webdriver.common.by import By
```


```python

"""
使用selenium进行模拟登陆
1.初始化ChromDriver
2.打开163登陆页面
3.找到用户名的输入框，输入用户名
4.找到密码框，输入密码
5.提交用户信息
"""
name = '*'
passwd = '*'
driver = webdriver.Chrome('./chromedriver')
driver.get('https://mail.163.com/')
# 将窗口调整最大
driver.maximize_window()
# 休息5s
time.sleep(5)
current_window_1 = driver.current_window_handle
print(current_window_1)
```


```python
button = driver.find_element_by_id('lbNormal')
button.click()

driver.switch_to.frame(driver.find_element_by_xpath("//iframe[starts-with(@id, 'x-URS-iframe')]"))

```


```python
email = driver.find_element_by_name('email')
#email = driver.find_element_by_xpath('//input[@name="email"]')
email.send_keys(name)
password = driver.find_element_by_name('password')
#password = driver.find_element_by_xpath("//input[@name='password']")
password.send_keys(passwd)
submit = driver.find_element_by_id("dologin")
time.sleep(15)
submit.click()
time.sleep(10)
print(driver.page_source)
driver.quit()
```

## 为什么会出现IP被封

网站为了防止被爬取，会有反爬机制，对于同一个IP地址的大量同类型的访问，会封锁IP，过一段时间后，才能继续访问

## 如何应对IP被封的问题

有几种套路：

1. 修改请求头，模拟浏览器（而不是代码去直接访问）去访问
2. 采用代理IP并轮换
3. 设置访问时间间隔

# 如何获取代理IP地址

- 从该网站获取： https://www.xicidaili.com/
- inspect -> 鼠标定位：
- 要获取的代理IP地址，属于class = "odd"标签的内容：代码如下，获取的代理IP保存在proxy_ip_list列表中


```python
# show your code
```


```python
# 案例代码
from bs4 import BeautifulSoup
import requests
import time

def open_proxy_url(url):
    user_agent = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36'
    headers = {'User-Agent': user_agent}
    try:
        r = requests.get(url, headers = headers, timeout = 20)
        r.raise_for_status()
        r.encoding = r.apparent_encoding
        return(r.text)
    except:
        print('无法访问网页' + url)


def get_proxy_ip(response):
    proxy_ip_list = []
    soup = BeautifulSoup(response, 'html.parser')
    proxy_ips  = soup.select('.odd')#选择标签
    for proxy_ip in proxy_ips:
        ip = proxy_ip.select('td')[1].text
        port = proxy_ip.select('td')[2].text
        protocol = proxy_ip.select('td')[5].text
        if protocol in ('HTTP','HTTPS'):
            proxy_ip_list.append(f'{protocol}://{ip}:{port}')
    return proxy_ip_list

if __name__ == '__main__':
    proxy_url = 'https://www.xicidaili.com/'
    text = open_proxy_url(proxy_url)
    proxy_ip_filename = 'proxy_ip.txt'
    with open(proxy_ip_filename, 'w') as f:
        f.write(text)
    text = open(proxy_ip_filename, 'r').read()
    proxy_ip_list = get_proxy_ip(text)
    print(proxy_ip_list)
```

    ['HTTP://222.95.144.128:3000', 'HTTP://222.85.28.130:40505', 'HTTP://121.237.148.248:3000', 'HTTPS://121.237.149.109:3000', 'HTTP://222.95.144.195:3000', 'HTTP://121.237.149.88:3000', 'HTTP://121.237.148.192:3000', 'HTTPS://121.237.148.183:3000', 'HTTP://121.237.149.56:3000', 'HTTP://121.237.149.163:3000', 'HTTP://122.228.19.8:3389', 'HTTP://116.196.87.86:20183', 'HTTP://112.95.205.137:8888', 'HTTP://61.153.251.150:22222', 'HTTPS://113.140.1.82:53281', 'HTTP://218.27.136.169:8085', 'HTTP://211.147.226.4:8118', 'HTTP://118.114.166.99:8118', 'HTTPS://223.245.35.187:65309', 'HTTPS://163.125.67.66:9797', 'HTTPS://222.95.144.129:3000', 'HTTPS://222.95.144.119:3000', 'HTTPS://222.95.144.200:3000', 'HTTPS://121.237.149.26:3000', 'HTTPS://117.88.176.158:3000', 'HTTPS://122.228.19.9:3389', 'HTTPS://117.88.177.124:3000', 'HTTPS://119.41.206.207:53281', 'HTTPS://121.237.148.59:3000', 'HTTPS://119.254.94.93:46323', 'HTTP://222.95.144.128:3000', 'HTTP://122.228.19.8:3389', 'HTTP://59.38.62.148:9797', 'HTTP://121.237.148.115:3000', 'HTTP://121.237.149.16:3000', 'HTTP://112.247.169.33:8060', 'HTTP://116.196.87.86:20183', 'HTTP://117.88.177.34:3000', 'HTTP://121.237.149.160:3000', 'HTTP://121.237.149.73:3000']


获取如下数据：

获取到代理IP地址后，发现数据缺失很多，再仔细查看elements，发现有些并非class = “odd”，而是…，这些数据没有被获取
class = "odd"奇数的结果，而没有class = "odd"的是偶数的结果

通过bs4的find_all(‘tr’)来获取所有IP：


```python
# show your code
```


```python
def get_proxy_ip(response):
    proxy_ip_list = []
    soup = BeautifulSoup(response, 'html.parser')
    proxy_ips = soup.find(id = 'ip_list').find_all('tr')
    for proxy_ip in proxy_ips:
        if len(proxy_ip.select('td')) >=8:
            ip = proxy_ip.select('td')[1].text
            port = proxy_ip.select('td')[2].text
            protocol = proxy_ip.select('td')[5].text
            if protocol in ('HTTP','HTTPS','http','https'):
                proxy_ip_list.append(f'{protocol}://{ip}:{port}')
    return proxy_ip_list
```

### 使用代理
- proxies的格式是一个字典：
- {‘http’: ‘http://IP:port‘,‘https’:'https://IP:port‘}
- 把它直接传入requests的get方法中即可
- web_data = requests.get(url, headers=headers, proxies=proxies)


```python
# show your code
```


```python
def open_url_using_proxy(url, proxy):
    user_agent = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36'
    headers = {'User-Agent': user_agent}
    proxies = {}
    if proxy.startswith('HTTPS'):
        proxies['https'] = proxy
    else:
        proxies['http'] = proxy
try:
    r = requests.get(url, headers = headers, proxies = proxies, timeout = 10)
    r.raise_for_status()
    r.encoding = r.apparent_encoding
    return (r.text, r.status_code)
except:
    print('无法访问网页' + url)
    return False
url = 'http://www.baidu.com'
text = open_url_using_proxy(url, proxy_ip_list[0])
```

### 确认代理IP地址有效性
- 无论是免费还是收费的代理网站，提供的代理IP都未必有效，我们应该验证一下，有效后，再放入我们的代理IP池中，以下通过几种方式：访问网站，得到的返回码是200真正的访问某些网站，获取title等，验证title与预计的相同访问某些可以提供被访问IP的网站，类似于“查询我的IP”的网站，查看返回的IP地址是什么验证返回码


```python
# show your code
```


```python
def check_proxy_avaliability(proxy):
    url = 'http://www.baidu.com'
    result = open_url_using_proxy(url, proxy)
    VALID_PROXY = False
    if result:
        text, status_code = result
        if status_code == 200:
            print('有效代理IP: ' + proxy)
        else:
            print('无效代理IP: ' + proxy)
```

# 改进：确认网站title


```python
# show you code
```


```python
def check_proxy_avaliability(proxy):
    url = 'http://www.baidu.com'
    text, status_code = open_url_using_proxy(url, proxy)
    VALID = False
    if status_code == 200:
        if r_title:
            if r_title[0] == '<title>百度一下，你就知道</title>':
                VALID = True
    if VALID:
        print('有效代理IP: ' + proxy)
    else:
        print('无效代理IP: ' + proxy)
```

### 完整代码


```python
from bs4 import BeautifulSoup
import requests
import re
import json


def open_proxy_url(url):
    user_agent = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36'
    headers = {'User-Agent': user_agent}
    try:
        r = requests.get(url, headers = headers, timeout = 10)
        r.raise_for_status()
        r.encoding = r.apparent_encoding
        return r.text
    except:
        print('无法访问网页' + url)


def get_proxy_ip(response):
    proxy_ip_list = []
    soup = BeautifulSoup(response, 'html.parser')
    proxy_ips = soup.find(id = 'ip_list').find_all('tr')
    for proxy_ip in proxy_ips:
        if len(proxy_ip.select('td')) >=8:
            ip = proxy_ip.select('td')[1].text
            port = proxy_ip.select('td')[2].text
            protocol = proxy_ip.select('td')[5].text
            if protocol in ('HTTP','HTTPS','http','https'):
                proxy_ip_list.append(f'{protocol}://{ip}:{port}')
    return proxy_ip_list


def open_url_using_proxy(url, proxy):
    user_agent = 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36'
    headers = {'User-Agent': user_agent}
    proxies = {}
    if proxy.startswith(('HTTPS','https')):
        proxies['https'] = proxy
    else:
        proxies['http'] = proxy

    try:
        r = requests.get(url, headers = headers, proxies = proxies, timeout = 10)
        r.raise_for_status()
        r.encoding = r.apparent_encoding
        return (r.text, r.status_code)
    except:
        print('无法访问网页' + url)
        print('无效代理IP: ' + proxy)
        return False


def check_proxy_avaliability(proxy):
    url = 'http://www.baidu.com'
    result = open_url_using_proxy(url, proxy)
    VALID_PROXY = False
    if result:
        text, status_code = result
        if status_code == 200:
            r_title = re.findall('<title>.*</title>', text)
            if r_title:
                if r_title[0] == '<title>百度一下，你就知道</title>':
                    VALID_PROXY = True
        if VALID_PROXY:
            check_ip_url = 'https://jsonip.com/'
            try:
                text, status_code = open_url_using_proxy(check_ip_url, proxy)
            except:
                return

            print('有效代理IP: ' + proxy)
            with open('valid_proxy_ip.txt','a') as f:
                f.writelines(proxy)
            try:
                source_ip = json.loads(text).get('ip')
                print(f'源IP地址为：{source_ip}')
                print('='*40)
            except:
                print('返回的非json,无法解析')
                print(text)
    else:
        print('无效代理IP: ' + proxy)


if __name__ == '__main__':
    proxy_url = 'https://www.xicidaili.com/'
    proxy_ip_filename = 'proxy_ip.txt'
    text = open(proxy_ip_filename, 'r').read()
    proxy_ip_list = get_proxy_ip(text)
    for proxy in proxy_ip_list:
        check_proxy_avaliability(proxy)
```

# 关于http和https代理
- 可以看到proxies中有两个键值对：
- {‘http’: ‘http://IP:port‘,‘https’:'https://IP:port‘}
- 其中 HTTP 代理，只代理 HTTP 网站，对于 HTTPS 的网站不起作用，也就是说，用的是本机 IP，反之亦然。
- 我刚才使用的验证的网站是https://jsonip.com, 是HTTPS网站所以探测到的有效代理中，如果是https代理，则返回的是代理地址
- 如果是http代理，将使用本机IP进行访问，返回的是我的公网IP地址


```python

```

# selenium
- selenium是什么：一个自动化测试工具（大家都是这么说的）
- selenium应用场景：用代码的方式去模拟浏览器操作过程（如：打开浏览器、在输入框里输入文字、回车等），在爬虫方面很有必要
- 准备工作：

1. 安装selenium（pip install selenium）
2. 安装chromedriver（一个驱动程序，用以启动chrome浏览器，具体的驱动程序需要对应的驱动，在官网上可以找到下载地址）
基本步骤：

1、导入模块：


```python
from selenium import webdriver  # 启动浏览器需要用到
from selenium.webdriver.common.keys import Keys  # 提供键盘按键支持（最后一个K要大写）
```

2、创建一个WebDriver实例：


```python
driver = webdriver.Chrome("chromedriver驱动程序路径")
```

3、打开一个页面:


```python

driver.get("http://www.python.org")  # 这个时候chromedriver会打开一个Chrome浏览器窗口，显示的是网址所对应的页面

```

4、关闭页面


```python

driver.close()  # 关闭浏览器一个Tab
# or
driver.quit()  # 关闭浏览器窗口

```

# 高级-查找元素：
- 在打开页面和关闭页面中间，就是各种操作！而查找元素这一点，和爬虫常见的HTML页面解析，定位到具体的某个元素基本一样，只不过，调用者是driver



```python

element = driver.find_element_by_name("q")  

```

# 高级-页面交互：
- 找到元素后，就是进行“交互”，如键盘输入（需提前导入模块）



```python

element.send_keys(“some text”）  # 往一个可以输入对象中输入“some text”
#甚至

element.send_keys(Keys.RETURN）  # 模拟键盘回车
#一般来说，这种方式输入后会一直存在，而要清空某个文本框中的文字，就需要：

element.clear()  # 清空element对象中的文字

```

# 高级-等待页面加载（wait）
- 应用场景：含有ajax加载的page！因为在这种情况下，页面内的某个节点并不是在一开始就出现了，而在这种情况下，就不能“查找元素”，元素选择不到，就不好进行交互操作！等待页面加载这两个模块经常是一起导入的：



```python

from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

```

- 显示等待：触发某个条件后才能够执行后续的代码


```python
driver = webdriver.Firefox()
driver.get("http://somedomain/url_that_delays_loading")
try:    
    element = WebDriverWait(driver, 10).until(           
        EC.presence_of_element_located((By.ID, "myDynamicElement")))
finally:    
    driver.quit()
#其中，presence_of_element_located是条件，By.ID是通过什么方式来确认元素（这个是通过id），"myDynamicElement"这个就是某个元素的ID

```

- 隐示等待：设置某个具体的等待时间


```python
driver = webdriver.Firefox()
driver.implicitly_wait(10) # seconds
driver.get("http://somedomain/url_that_delays_loading")
myDynamicElement = driver.find_element_by_id("myDynami")
```
