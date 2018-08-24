using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.Extensions.Options;
using BasketService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
using BasketService.IntegrationEvents.EventsHandling;
using RabbitMQ.Client;
using BasketService.IntegrationEvents.Events;
using Autofac.Extensions.DependencyInjection;

namespace BasketService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = Configuration["Server"];
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddTransient<IBasketRepository, RedisBasketRepository>();
            services.AddMvc();
            ConfigueAuthService(services);
            ConfigueIntegrationServices(services);
            ConfigueEventBus(services);
            // Autofac Injection
            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            UseEventBus(app);

        }
        protected virtual void UseEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<MovieDetailChangedIntegrationEvent, MovieDetailChangedIntegrationEventHandler>();
            eventBus.Subscribe<MovieDeleteIntegrationEvent, MovieDeleteIntegrationEventHandler>();
        }

        private void ConfigueAuthService(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = "http://identityserver:5001";
                options.RequireHttpsMetadata = false;
                options.Audience = "basket";
            });


        }

        private void ConfigueEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<MovieDetailChangedIntegrationEventHandler>();
            services.AddTransient<MovieDeleteIntegrationEventHandler>();
        }

        private void ConfigueIntegrationServices(IServiceCollection services)
        {


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBusConnection"]
                    //HostName = "rabbitmq"
                };

                if (!string.IsNullOrEmpty(Configuration["EventBusUserName"].ToString()))
                {
                    factory.UserName = Configuration["EventBusUserName"];

                }
                // factory.UserName = "zwb";
                if (!string.IsNullOrEmpty(Configuration["EventBusPassword"].ToString()))
                {
                    factory.Password = Configuration["EventBusPassword"];

                }
                // factory.Password = "zwb";
                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }
                // factory.CreateConnection();
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });


        }
    }
}
