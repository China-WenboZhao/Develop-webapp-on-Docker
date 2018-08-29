![图片2.png](https://github.com/China-WenboZhao/Develop-webapp-on-Docker/blob/master/图片2.png)


**Connection:** RabbitMQ Client 到Server的连接.  
**Channel:** Client到server的连接，实际中在代码中实际使用的连接.  
**Producer:** 发布消息的人.  
**Exchange:** 类似交换机的作用.  
**Queue:** 接收消息的队列.  
**Consumer:** 接受并处理消息的人.  

无论发布还是订阅，均事先需要以下步骤：
1. 利用connectionfactory生成连接connection
2. 利用connection生成channel。
3. Channel.ExchangeDeclare声明交换机

而后发布订阅所需流程：

- 发布流程：
1. 直接channel.BasicPublish即可发布。

- 订阅流程：
1. 生成并且绑定Queue。
2. 生成并且绑定消费者/订阅者。（包括制定收到消息时执行的函数）
3. 直接channel.BasicConsume 进行订阅


注：详细代码参考[http://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html](http://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html)
