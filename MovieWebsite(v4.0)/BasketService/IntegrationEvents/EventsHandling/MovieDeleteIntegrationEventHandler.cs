using BasketService.Data;
using BasketService.IntegrationEvents.Events;
using BasketService.Models;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.IntegrationEvents.EventsHandling
{
    public class MovieDeleteIntegrationEventHandler : IIntegrationEventHandler<MovieDeleteIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        public MovieDeleteIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task Handle(MovieDeleteIntegrationEvent @event)
        {
            var userIds = _repository.GetUsers();

            foreach (var id in userIds)
            {
                var basket = await _repository.GetorCreateBasketAsync(id);

                await DeleteBasketItems(@event.MovieID,basket);
            }
        }

        public async Task DeleteBasketItems(int MovieID, Basket basket)
        {
            var itemsToDelete = basket?.Items?.Where(x => x.Id == MovieID).ToList();
            if (itemsToDelete != null)
            {
                foreach (var item in itemsToDelete)
                {
                    basket.Items.Remove(item);
                }
                await _repository.UpdateBasketAsync(basket);
            }     
        }

    }
}
