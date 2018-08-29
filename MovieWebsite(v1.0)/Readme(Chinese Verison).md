1. 在[docker-compose.yml](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v1.0)/docker-compose.yml)中,
你可以看到有一些关于network的注释。实际上network是用于解决当微服务们定义在不同解决方案/docker-compose.yml或不同主机上的情况。默认情况下，
当你使用docker时，所有定义在一个docker-compose.yml中的的微服务/容器都默认被分配到同一个网段下。这使得容器间的通信十分便利，
仅仅需要通过分配给容器的临时IP或者容器的别名（docker-compose.yml中定义的container_name）即可访问容器。
然而，当微服务定义在不同的docker-compose.yml文件中但是处于同一台主机时，我们可以通过自定义网络的形式来访问容器。
当位于不同docker-compose.yml文件中的容器位于同一网络环境中时，他们之间就可以访问了。
要做到以上我们有两步需要完成：
