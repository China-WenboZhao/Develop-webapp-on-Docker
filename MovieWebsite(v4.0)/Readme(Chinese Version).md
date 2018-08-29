1. 如果你足够细心的话，你会发现在我们之前的解决方案中我们使用所有的微服务中使用的都是If 'Mysql.Data.EntityFrameworkCore' 框架(还有 EF core 2.0/2.1)。  
在这个解决方案中,我们仍旧在 IdentityServer中使用 'Mysql.Data.EntityFrameworkCore' 框架，但是在MoviesService中我们使用的却是'Pomelo.EntityFrameworkCore.MySql' 框架。而'`options.UseMySQL`' 方法也更改为了'`options.UseMySql`'(点击 [这里](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v4.0)/MoviesService/Startup.cs) 来查看代码).为什么会这样呢？   
我们不是十分确定是哪一个地方的代码触发了
We are not quite sure which snippet trigger a bug of 'Mysql.Data.EntityFrameworkCore' framework but it does after doing Dependency Injection of EventBus, you will see error like this 'Method not found: '`Void Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory..ctor(Microsoft.EntityFrameworkCore.Diagnostics.IDiagnosticsLogger 1<Command>, Microsoft.EntityFrameworkCore.Storage.IRelationalTypeMapper)'.'` According to general reflection, this is a Oracle's bug. See [https://github.com/aspnet/EntityFrameworkCore/issues/11078](https://github.com/aspnet/EntityFrameworkCore/issues/11078) for details.
  (When you see this, the bug may has been fixed.) 


