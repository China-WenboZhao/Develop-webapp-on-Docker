1. 如果你足够细心的话，你会发现在我们之前的解决方案中我们使用所有的微服务中使用的都是If 'Mysql.Data.EntityFrameworkCore' 框架(还有 EF core 2.0/2.1)。  
在这个解决方案中,我们仍旧在 IdentityServer中使用 'Mysql.Data.EntityFrameworkCore' 框架，但是在MoviesService中我们使用的却是'Pomelo.EntityFrameworkCore.MySql' 框架。而'`options.UseMySQL`' 方法也更改为了'`options.UseMySql`'(点击 [这里](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v4.0)/MoviesService/Startup.cs) 来查看代码).为什么会这样呢？   
我们不是十分确定在依赖注入了EventBus中的内容后是哪一个地方的代码触发了 'Mysql.Data.EntityFrameworkCore' 框架的bug，但是你会碰到一下报错
'`Method not found: 'Void Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory..ctor(Microsoft.EntityFrameworkCore.Diagnostics.IDiagnosticsLogger 1<Command>, Microsoft.EntityFrameworkCore.Storage.IRelationalTypeMapper)'.'` 根据网上普遍的反映来看，这是oracle的一个官方错误。 请查看 [https://github.com/aspnet/EntityFrameworkCore/issues/11078](https://github.com/aspnet/EntityFrameworkCore/issues/11078) 以获取详细信息。
  (当你看到这里时，官方说不定已经修复了这个bug) 


