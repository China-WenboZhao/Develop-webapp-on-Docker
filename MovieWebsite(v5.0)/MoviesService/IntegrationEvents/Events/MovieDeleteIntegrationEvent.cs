using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesService.IntegrationEvents.Events
{
    public class MovieDeleteIntegrationEvent : IntegrationEvent
    {
        public int MovieID { get; private set; }

        public MovieDeleteIntegrationEvent(int MovieID){
            this.MovieID = MovieID;
        }

    }
}
