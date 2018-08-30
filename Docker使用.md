**注：本文档针对Docker Version 18.03.1-ce-win65 (17513)编写,使用环境 .net core 2.0 编写时间：2018/7/26，编写人：赵文博**
## 1. Docker下载安装及使用前准备
### Docker的下载
- 使用Windows 7、8平台的朋友们强烈建议退坑，更新至Windows10 以上版本再去官网下载Docker CE （win7、8 无法使用Docker CE,只能使用Docker Toolbox. Docker Toolbox难以和vs结合使用并且下载的linux shell十分低级，很多命令都没有）。
- 建议顺带安装Kitematic（安装docker时会提示），比较好用。
###Docker设置
- 建议使用linux container（Docker本来就是针对linux开发的，并且所必须的.net镜像更小）。
- Docker镜像的拉取速度通常十分缓慢（是从国外拉取的镜像), 可以使用青云Docker等国内镜像仓库（一个月免费）。
###Docker运行
- 下载完Docker后，运行Docker,然后打开VS 2017，文件->新建->项目->[ASP.NET] Core Web应用程序，选上Docker支持（之后也可以右键工程-添加-Docker支持。注意这里询问目标OS需与Docker设置中的container平台一致，不要因为是电脑系统Windows10就设成Windows。
###Docker镜像的拉取
- Docker下载完后会自动根据容器拉取.net所需镜像，Windows是: 2.0.0-runtimenanoserver-...，Linux是microsoft/aspnetcore:2.0，强烈建议使用国内镜像仓库。
- MySQL等数据库的镜像也需要拉取，可以在kitematic中搜索或在Docker CLI/cmd中使用： docker search <镜像名>，下载使用： docker pull <镜像名> 。
 ## 2. Docker使用
### Docker CLI 中常用命令
- docker images: 查看docker主机中的所有镜像
- docler ps: 查看docker主机中所有的容器
- docker  exec -it <container id> bash: 以bash的形式进入容器内部
- docker rm <container id>: 删除指定容器
- docker rmi <image id>: 删除指定镜像
### Dockerfile的使用
- Dockerfile定义了一个镜像，Dockerfile不发生改变，镜像就不会改变（再次点击Docker运行时不会重新创建）。
- 通常的Dockerfile如下：
```
FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "MoviesService.dll"]
```
需要注意的是EXPOSE命令是将容器的80端口暴露给其他镜像/容器，并不会暴露给宿主机（也就是说我们通过localhost:80是访问不了的），通常用于容器间的通信等。
### docker-compose.yml的使用
- docker-compose.yml文件在镜像实例化为容器的时候调用，用于组合配置容器，docker-compose不变，容器不变（再次点击Docker运行时不会重新创建）。
一个例子如下：
```
services:
  moviesservice:
    image: moviesservice
    container_name: moviesservice
    ports:
      - "5000:5000"
    build:
      context: ./MoviesService
      dockerfile: Dockerfile
    depends_on:
      - mysql
    restart: always

  webmvc:
    image: webmvc   
    ports:
      - "5001:5001"
    build:
      context: ./WebMVC
      dockerfile: Dockerfile
    restart: always

  mysql:
    image: mysql:5.7
    ports: 
      - "3306:3306"
    environment:
      MYSQL_USER: zwb
      MYSQL_PASSWORD: zwb
      MYSQL_DATABASE: TestDataBase
      MYSQL_ROOT_HOST: 172.*.*.*
      MYSQL_RANDOM_ROOT_PASSWORD: 1
      MYSQL_ONLINE_PASSWORD: 1
    volumes:
      - ./dbdata:/var/lib/mysql
    restart: always
```
 1. services: 定义了服务，image制定了容器对应的镜像，container_name定义了容器的别名，depends-on表明此容器会在所依赖容器创建后再进行创建。ports是将容器内的端口和宿主机端口进行了映射（前面的数字指明了宿主机端口）。所以，当我们在docker中需要两个或以上的数据库的呢？我们该如何映射？
由于ports第二个的端口是容器内端口，对于多个数据库而言都是3306，而ports第一个的端口是映射的宿主机端口，不同数据库映射到宿主机需用不同端口。示例如下：
```
  mysql1:
    image: mysql:5.7
    ports: 
      - "3306:3306"
    #此处省略部分代码
  mysql2:
    image: mysql:5.7
    ports: 
      - "3307:3306"
    #此处省略部分代码
```
 2. 尤其需要注意的是mysql容器，通过enviroment指定了容器的环境变量，MYSQL_USER和MYSQL_PASSWORD指定了MySQL数据库创建时新建的用户和密码， MYSQL_DATABASE会创建并使用数据库，  MYSQL_RANDOM_ROOT_PASSWORD和MYSQL_ONLINE_PASSWORD设定初始化时root密码。
 3. volumes将数据库挂载到宿主机上存储，即使容器销毁，该数据也会始终存在。所以如果在web app中编写了初始化代码初始化表结构，之后更改了初始化的代码想更改表结构时，需要先连接上容器的数据库删掉原先的表（本地挂载的内容也会相应删除）然后再运行代码（感觉其实和连接本机数据库一样）。另外，使用volume需要开启docker的共享盘符（share drives）功能，在docker设置中可以设置。
### docker-compose.ci.build.yml, docker-compose.override.yml
- docker-compose.ci.build.yml定义了编译项目的ci命令，基础镜像版本。（core 2.0/core 2.1）
- docker-compose.yml定义了个项目对应的镜像集合，镜像对应的DockerFile路径及名称
- docker-compose.override.yml 是docker-compose.yml的补充，docker运行时先读取docker-compose.yml,然后默认读取docker-compose.override.yml
- docker-compose.dcproj主要用于指定容器运行的操作系统（windows/linux），建议使用linux系统，因为很多大部分官方或第三方镜像都是linux环境下的。
### Docker容器之间的访问
- 我们都知道，从主机访问docker容器是通过localhost:<容器暴露给宿主机的端口>来访问的。那么，在同一网段下的容器间（同一个docker-compose.yml定义的容器们默认分配到同一网段）访问是通过什么呢? 通过 容器别名:<容器expose给其他容器的端口> 就可以访问了。例如：
http://moviesservice:5000/MoviesService
需要注意的是，在早先的docker版本中，想要实现容器间的访问还需要在docker-compose.yml中加上link(定义在同一个docker-compose中的其他服务）或者external_link(定义在不同docker-compose中的其他服务)，但是新版本Docker已经能自动找到路径，无需此操作，故不再赘述。
- 然而，当微服务定义在不同的docker-compose.yml文件中但是处于同一台主机时，我们可以通过自定义网络的形式来访问容器。只要配置不同docker-compose.yml文件中的容器位于同一网络环境中时，他们之间就可以访问了。要做到以上我们有两步需要完成：
(1). 在docker CLI中运行 '`docker network create  --attachable net1`' 命令.  
(2). 在所有的docker-compose.yml文件中设置容器属于'net1'(你也可以创先一个单独的文件来专门配置网络)。
示例如下：
```
services:
  mysql:
    image: mysql:5.7
    ports: 
      - "3306:3306"
    #此处省略部分代码
    networks:
      - net1
networks:
    net1:
      external: true
```
- 当容器部署在不同主机上的时候，我们就需要首先配置swarn/k8s集群，而后配overlay network来使容器们通信。更多细节参考 [https://docs.docker.com/network/overlay/](https://docs.docker.com/network/overlay/) 和 [https://docs.docker.com/network/network-tutorial-overlay/](https://docs.docker.com/network/network-tutorial-overlay/)。
## 3. 其他
- 关于docker的使用还有很多，比如network的使用（指定局域网和网络模式，默认桥接模式，处于一个网段），mount使用等等，小伙伴们可参照官方网站[https://docs.docker.com/config/formatting/](https://docs.docker.com/config/formatting/)。
- 在运行docker的过程中，也出现了一些奇怪的问题，尚未搞清原因和确切解决方案，比如有时候运行时会出现：
```
You may only use the Microsoft .NET Core Debugger (vsdbg) with Visual Studio
Code, Visual Studio or Visual Studio for Mac software to help you develop and
test your applications.
realpath(): Invalid argument
```
根据Github上的描述[https://github.com/Microsoft/dotnet/issues/294](https://github.com/Microsoft/dotnet/issues/294)，可能是磁盘映射出现的问题，上面未给出明确解决方案。然而重新生成docker在我这里成功解决这个问题。

</br>
</br>


