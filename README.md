# Develop-webapp-on-Docker（see [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/README(Chinese%20Version).md) for Chinese Version）
**Tags:** Microservices, Data Storage, Secure Authentication &amp; Authorization, Synchronous &amp; Asynchronous communication beween microservices.

All examples are only deployed on our local host, which means all microservices are only running on local Docker. All access to microservices are through  http:// localhost:<port\>/\<controller name\>/...  or http://\<microservices name\>:\<port\>/\<controller name\>/... Thus, some issues are remained here and can be easily solved when using domain or public IP.

This repository contains five solutions, each one is added with new functiion blocks based on the former solutions.
All solutions use .net core 2.0 & EF core 2.0/2.1 as development environment.

Each solution contains several projects(which are microservices from the view of Docker). **Before you run or view each solution, don't forget to browse Readme.md in it for matters and attention.**

### **MovieWebsite(v1.0)**  
MovieWebsite(v1.0) is an example of a movie list, which you can create, view details, delete, update movies. **Here we will also discuss about the cases when functions/microservices/projects are in different solutions（defined in different docker-compose.yml） or different hosts.**

### **MovieWebsite(v2.0)**  
MovieWebsite(V2.0) is added with the Authentication function, Using Asp.net core Identy & IdentityServer4 frameworks, you can easily register, log in and log out.

### **MovieWebsite(v3.0)**  
MovieWebsite(v3.0)  appends the basket function. You must be authenticated before you are authorized to access your own basket. You can add movies to the basket or delete movies from basket. However, movie infos will not changed accordingly when its infos are changed in the list.

### **MovieWebsite(v4.0)**    
MovieWebsite(v4.0) use publish-subscribe model(EventBus framework from [https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus]( https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus) ) implemented by RabbitMQ to address the synchronization of infos changing in list and basket. In a nutshell, when you changed info of a movie or delete a movie in the list, the corresponding movie in the basket will also be changed or deleted.  However, In some exceptional cases, the database of the list has been changed whereas the changed in the basket fails. We will never know that bad situation happens because we don't have any record.

### **MovieWebsite(v5.0)**  
MovieWebsite(v5.0) add a table that save the status of the infos changing message(including ready to publish, published). Database operations of list and status recording are viewed as one transaction, which prevents the inconformity of data and status. If the database of basket failed to update, the transaction will be rolled back to ensure the consistency. **By now, we are in a dilemma and can't solved one problem, so this part is to be continued.(see [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v5.0)/Readme.md
) for details)**

#### **NOTE: There are also some attached tutorials which are wirtten by Chinese. If you are english user, just ignore it.**

