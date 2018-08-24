In the [docker-compose.yml](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/MovieWebsite(v1.0)/docker-compose.yml), you can see there are some annotations about network. In fact the network is used for the situation when microservices are deployed in different Solutions or Host.

By default, when you use docker, all microservices/containers defined in one docker-compose.yml are set in the same network segment. That falicitate the access from one container to anoter. you can access container in one segment directly using temporary ip or alias. However, when containers are defined in different docker-compose.yml, while on the same host, you can custom your own network. If you set all containers in the same network, then you can successly access containers defined in other docker-compose.yml.   Here are two steps you need to do:  
1.run 'docker network create  --attachable net1' commond in docker CLI.  
2.set all containers belongs to 'net1' in docker-compose.yml(you can also create a new file to configue network).

When containers are deployed on different host, however, you need to use swarm/k8s and overlay network to let them communicate.
See more details on [https://docs.docker.com/network/overlay/](https://docs.docker.com/network/overlay/) and [https://docs.docker.com/network/network-tutorial-overlay/](https://docs.docker.com/network/network-tutorial-overlay/).
