1. 尽管 Logout() 方法会在我们创建项目并选择‘个人用户账户’时自动创建，我们仍然需要添加一个'`Logout(string logoutid)`' 方法(代码请看 [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v2.0)/IdentityServer/Controllers/AccountController.cs)) 因为identityServer4认证服务器在我们从客户端登出时会重定向到http://identityserver:5001/Accout/Logout(logoutid)


2. 在这个结局方案中，我们添加了认证板块，**但是这里仍有一个问题残余：**  
在[WebMVC/Startup.cs](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v2.0)/WebMVC/Startup.cs)中，你会看到'`options.Authority=...`'。在这里我们设置了IdentityServer的路径，但是这个路径同时起到了重定向和容器间通信的作用。如果我们想要保留重定向的作用，路径应该配置成http://localhost:5001/ 。 相反，如果我们想保留容器间通信的作用，路径配置成 http://identityservice:5001/ 。在这里，我们使用的是第二种，所以当从客户端点击登陆进行跳转时我们需要手动将url从http://identityserver:5001/connect/authorize?... 更改为http://localhost:5001/connect/authorize?... 。查看更多信息，请到[https://github.com/aspnet/Security/issues/1175#issuecomment-293376825](https://github.com/aspnet/Security/issues/1175#issuecomment-293376825).


