# Develop-webapp-on-Docker
**标签:** 微服务, 数据存储, 安全认证及授权, 微服务间的同步&amp；异步通信.

注意，所有的例子都部署运行在我们的本地主机和本地Docker上.  所有对于微服务的访问都是通过  http:// localhost:<port\>/\<controller name\>/...  or http://\<microservices name\>:\<port\>/\<controller name\>/... 的形式来访问的。因此，很多问题可以通过使用公网ip或域名的方式轻松解决。

这个仓库一共包括5个解决方案，每个解决方案都在上一个的基础上添加了新的功能板块。所以的解决方案都是在e .net core 2.0 & EF core 2.0/2.1 的环境中进行开发的。

Each solution contains several projects(which are microservices from the view of Docker). **Before you run or view each solution, don't forget to browse Readme.md in it for matters and attention.**

### **MovieWebsite(v1.0)**  
MovieWebsite(v1.0) is an example of a movie list, which you can create, view details, delete, update movies. **Here we will also discuss about the cases when functions/microservices/projects are in different solutions or different hosts.**

### **MovieWebsite(v2.0)**  
MovieWebsite(V2.0) is added with the Authentication function, Using Asp.net core Identy & IdentityServer4 frameworks, you can easily register, log in and log out.

### **MovieWebsite(v3.0)**  
MovieWebsite(v3.0)  appends the basket function. You must be authenticated before you are authorized to access your own basket. You can add movies to the basket or delete movies from basket. However, movie infos will not changed accordingly when its infos are changed in the list.

### **MovieWebsite(v4.0)**    
MovieWebsite(v4.0) use broadcast-subscribe model(EventBus framework from [https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus]( https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus) ) implemented by RabbitMQ to address the synchronization of infos changing in list and basket. In a nutshell, when you changed info of a movie or delete a movie in the list, the corresponding movie in the basket will also be changed or deleted.  However, In some exceptional cases, the database of the list has been changed whereas the changed in the basket fails. We will never know that bad situation happens because we don't have any record.

### **MovieWebsite(v5.0)**  
MovieWebsite(v5.0) add a table that save the status of the infos changing message(including ready to publish, published). Database operations of list and status recording are viewed as one transaction, which prevents the inconformity of data and status. If the database of basket failed to update, the transaction will be rolled back to ensure the consistency. **By now, we are in a dilemma and can't solved one problem, so this part is to be continued.(see [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v5.0)/Readme.md
) for details)**

