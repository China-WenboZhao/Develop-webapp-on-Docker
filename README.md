# Develop-webapp-on-Docker
**Tags:** Microservices, Data Storage, Secure Authentication &amp; Authorization, Synchronous &amp; Asynchronous communication beween microservices.

All examples are only deployed on our local host, which means all microservices are only running on local Docker. All access to microservices are through  http://localhost:<port\>/\<controller name\>/...  or http://\<microservices name\>:\<port\>/\<controller name\>/... Thus, some issues are remained here and can be easily solved when using domain or public IP.

This repository contains five solutions, each one is added with new functiion blocks based on the former solutions.
All solutions use .net core 2.0 & EF core 2.0/2.1 as development environment.

Each solutions contains several projects(which are microservices from the view of Docker). 

### **MovieWebsite(v1.0)**  
MovieWebsite(v1.0) is an example of a movie list, which you can create, view details, delete, update movies.

### **MovieWebsite(V2.0)**  
MovieWebsite(V2.0) is added with the Authentication function, Using Asp.net core Identy & IdentityServer4 frameworks, you can easily register, log in and log out. **Here we will also discuss about the cases when functions/microservices/projects are in different solutions or different hosts.**

### **MovieWebsite(v3.0)**  
MovieWebsite(v3.0)  appends the basket function. You must be authenticated before you are authorized to access your own basket. You can add movies to the basket or delete movies from basket. However, movie infos will not changed accordingly when its infos are changed in the list.

### **MovieWebsite(v4.0)**    
MovieWebsite(v4.0) use broadcast-subscribe model(EventBus framework from [https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus]( https://github.com/dotnet-architecture/eShopOnContainers/tree/dev/src/BuildingBlocks/EventBus) ) implemented by RabbitMQ to address the synchronization of infos changomg in list and changed. However, In some exceptional cases, the database of the list has been changed whereas the changed in the basket fails. We will never know that bad situation happens because we don't have any record.


