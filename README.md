# LebiShop 乐彼多语言网上商城系统
采用ASP.NET 4.5（C#）和AJAX技术开发，完全具备搭建超大型网上商城的整体技术框架和应用层次。系统具备安全、稳定、高效、扩展性强、操作简便等众多优点，是您搭建网上商城的不二选择，系统内置四十几种国家语言，前台和后台均支持语言切换。<br/><br/>
采用Lebi多层结构开发，html模板文件和cs文件分离，自动生成aspx文件，不论是开发还是维护都变得更加轻松。<br/>
模板支持标签引用和c#语法，不论是开发人员还是前端设计人员都极易上手，多级的模板结构让个性化重写也变得随意。<br/>
数据库推荐使用MSSQL2012及以上版本。<br/>

# 首次使用：
下载安装不开源版本 http://www.lebi.cn/down/ ，根据安装步骤进行安装，完成后会创建数据库，打开开源版本web.config文件，将数据库连接修改为创建的数据库。<br/>
首次访问后台，需要先在浏览器输入/admin/updatepage.aspx，显示OK后即可访问后台。<br/>
首次访问前台，需要先登录后台，在模板管理页面点击“生成模板”生成前台文件。<br/><br/>

# 工程结构：
Shop 网站页面<br/>
Shop.Bussiness 主要业务处理类库<br/>
Shop.DataAccess 数据访问类库<br/> 
Shop.Model 数据库模型以及其它模型类库<br/>
Shop.Platform 第三方登录相关<br/> 
Shop.Tools 工具类库<br/>
DB.LebiShop 数据层（自动生成）<br/>

# 文件结构：
Shop<br/>
admin 后台文件夹-系统自动生成<br/>
ajax AJAX数据操作文件夹-系统自动生成<br/>
api API文件夹-系统自动生成<br/>
back SQL数据库备份文件夹<br/>
config 系统配置文件文件夹 用于存放系统、插件配置文件<br/>
editor 网页编辑器文件夹<br/>
inc 块文件文件夹<br/>
onlinepay 在线支付接口文件夹<br/>
pagebase 页面 cs 文件夹<br/>


模板结构 http://www.lebi.cn/faq/info.aspx?id=99<br/>
模板语法 http://www.lebi.cn/faq/info.aspx?id=98<br/>
个人学习研究免费使用，商业使用请购买商业授权 http://www.lebi.cn/opensource/<br/>
