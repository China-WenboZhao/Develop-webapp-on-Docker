- **Asp net core Identity 本身就是一个认证框架了，然而在我看来有其局限性：**
  1.  前后端没有分离，较难通过外部接口访问实现认证并且跳转回去，对于微服务适用性较差。
  2. 认证过程并不一定安全。

  然而与IdentityServer4结合之后以上两方面得到了弥补，因为其使用了OpenID Connect 和OAuth 2.0，保证了认证过程的安全性。同时identityServer4配置了登陆和登出时重定向路径，使其即使从客户端（微服务）登陆登出返回时也能返回到正确客户端（微服务）。

- **Client 定义了AllowedGrantType**
分别是ClientCredentials(用户凭证)，ResourseOwnerPassword(资源拥有者密钥)，Implicit(隐式流), HybridAndClientCredentials(混合流)
  1. ClientCredentials： 不需要定义用户和密钥，直接通过client自身的凭证进行访问。
  2. ResourseOwnerPassword: 须定义用户和密钥（Config.cs中定义或外部获取），通过用户名密钥访问。
  3. Implicit: 先向认证服务器获取access token,而后用token访问资源，不允许使用refresh token和长时间有效的access token。
  4.  HybridAndClientCredentials: 同样是先向认证服务器获取identity token,而后用token获取access token（更安全的方式获取，以防止暴露），允许使用refresh token和长时间有效的access token。对于需要手动登陆之后访问资源的情况，通常采用此方式。

- **关于登出**  
` public async Task Logout()`   
`{`
`await HttpContext.SignOutAsync("Cookies");`  
`await HttpContext.SignOutAsync("oidc");`  
` }`  
登出时客户端需要写上面的两句代码，第一句清除客户端cookie，第二句跳转至服务器进行登出。然后第二句跳转的函数虽然是Logout，但是却会带上string logoutid的参数。然而在Asp.net core identity 框架中不存在带参数的logout函数，需要自己添加。  
 ![图片1.png](.attachments/图片1-7c7276d4-4766-438f-ada5-542378fcd54f.png)


