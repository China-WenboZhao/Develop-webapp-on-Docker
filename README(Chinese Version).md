# Develop-webapp-on-Docker
**标签:** 微服务, 数据存储, 安全认证及授权, 微服务间的同步&异步通信.

注意，所有的例子都部署运行在我们的本地主机和本地Docker上.  所有对于微服务的访问都是通过  http:// localhost:<port\>/\<controller name\>/...  or http://\<microservices name\>:\<port\>/\<controller name\>/... 的形式来访问的。因此，很多问题可以通过使用公网ip或域名的方式轻松解决。

这个仓库一共包括5个解决方案，每个解决方案都在上一个的基础上添加了新的功能板块。所以的解决方案都是在e .net core 2.0 & EF core 2.0/2.1 的环境中进行开发的。

每个解决方案都包含了几个项目（对应docker中的微服务）。**在你学习，下载，运行每个解决方案前, 不要忘记先查看里面的readme文件，里面提到了一些可能遇到的问题。**

### **MovieWebsite(v1.0)**  
MovieWebsite(v1.0)是一个电影列表的例子，你也可以新建，查看细节，删除，更新电影。**在这个项目的Readme.md中我们也会讨论当微服务们处在不同解决方案（不同docker-compose.yml文件定义）或不同主机上的情况。**

### **MovieWebsite(v2.0)**  
MovieWebsite(V2.0) 添加了认证功能,使用 Asp.net core Identy 和 IdentityServer4 框架。 通过这两个框架，你可以轻松的注册，登陆和登出。

### **MovieWebsite(v3.0)**  
MovieWebsite(v3.0) 添加了购物车功能。在你获取访问你自己的购物车权限之前你必须先进行登陆。你可以把电影加入购物车或者从购物车中除去。然而，当列表中的电影信息改变时购物车中的电影信息不会相应的进行改变。

### **MovieWebsite(v4.0)**    
MovieWebsite(v4.0) 使用了广播-订阅模式（基于RabbitMQ实现的EventBus框架，链接：[https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus]( https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus) 。此模式很好的解决了列表中和购物车中电影信息更改的同步性。当你在列表中修改电影信息或者删除一个电影的时候，购物车中的电影也会进行相应改变。然而，在某些情况下，当存储电影列表的数据库已经改变，购物车电影的数据库可能更新失败。我们并不会获取到这些情况，因为我们没有做任何的记录和补救。

### **MovieWebsite(v5.0)**  
MovieWebsite(v5.0) add a table that save the status of the infos changing message(including ready to publish, published). Database operations of list and status recording are viewed as one transaction, which prevents the inconformity of data and status. If the database of basket failed to update, the transaction will be rolled back to ensure the consistency. **By now, we are in a dilemma and can't solved one problem, so this part is to be continued.(see [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v5.0)/Readme.md
) for details)**

