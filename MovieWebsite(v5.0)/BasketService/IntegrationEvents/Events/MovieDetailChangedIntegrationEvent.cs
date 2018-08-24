using BasketService.Models;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.IntegrationEvents.Events
{
    public class MovieDetailChangedIntegrationEvent:IntegrationEvent
    {
        public int MovieID { get; private set; }

        public BasketItem basketitem { get; private set; }
        public MovieDetailChangedIntegrationEvent(int MovieID, BasketItem basketitem)
        {
            this.MovieID = MovieID;
            this.basketitem = basketitem;
        }
    }
}
