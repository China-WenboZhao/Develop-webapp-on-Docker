using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using MoviesService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesService.IntegrationEvents.Events
{
    public class MovieDetailChangedIntegrationEvent : IntegrationEvent
    {
        public int MovieID { get; private set; }

        public BasketItem Basketitem { get; private set; }
        public MovieDetailChangedIntegrationEvent(int MovieID, BasketItem Basketitem)
        {
            this.MovieID = MovieID;
            this.Basketitem = Basketitem;
        }

    }
}
