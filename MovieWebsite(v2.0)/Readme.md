1. Although The IdentityServer is generated automatically when we choose 'Idividual User Accounts' when create new project, we still need to add a '`Logout(string logoutid)`' method(see [here](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v2.0)/IdentityServer/Controllers/AccountController.cs)) because the IdentityServer redirect to http://identityserver:5001/Accout/Logout(logoutid) when we log out from the client. 


2. In this solution, we add Authentication, **but here is one more problem remained:**  
In [WebMVC/Startup.cs](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v2.0)/WebMVC/Startup.cs), you can see '`options.Authority=...`'. Here we set the IdentityServer path, but this path is used both for redirect and containers communications. If we want to remain redirect. the path should be http://localhost:5001/. In converse, If we want to remain cotainers communication, the path should be http://identityservice:5001/. By now, we use second  path, so we need to change the url manually from http://identityserver:5001/connect/authorize?... to http://localhost:5001/connect/authorize?... .  
For more details, go to [https://github.com/aspnet/Security/issues/1175#issuecomment-293376825](https://github.com/aspnet/Security/issues/1175#issuecomment-293376825).

