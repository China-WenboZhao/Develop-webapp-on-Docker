using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Services;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Utilities;
using MoviesService.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesService.IntegrationEvents
{
    public class MovieIntegrationEventService : IMovieIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly MovieContext _movieContext;
        private readonly IIntegrationEventLogService _eventLogService;
        public MovieIntegrationEventService(IEventBus eventBus, MovieContext movieContext, 
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _movieContext = movieContext ?? throw new ArgumentNullException(nameof(movieContext));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            DbConnection conn = _movieContext.Database.GetDbConnection();
            _eventLogService = _integrationEventLogServiceFactory(conn);
        }
        public async Task  PublishThroughEventBusAsync(Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events.IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
            await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        public async Task SaveEventAndMovieContextChangesAsync(Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events.IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_movieContext)
                .ExecuteAsync(async () =>
                {
                    // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                    await _movieContext.SaveChangesAsync();
                    await _eventLogService.SaveEventAsync(evt, _movieContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }

}
