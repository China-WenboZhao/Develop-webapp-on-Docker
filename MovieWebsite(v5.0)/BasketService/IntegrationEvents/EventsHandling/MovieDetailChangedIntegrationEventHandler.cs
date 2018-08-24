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
    public class MovieDetailChangedIntegrationEventHandler : IIntegrationEventHandler<MovieDetailChangedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        public MovieDetailChangedIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(MovieDetailChangedIntegrationEvent @event)
        {
            var userIds = _repository.GetUsers();

            foreach (var id in userIds)
            {
                var basket = await _repository.GetorCreateBasketAsync(id);

                await UpdateBasketItems(@event.MovieID, @event.basketitem, basket);
            }
        }

        public async Task UpdateBasketItems(int MovieID, BasketItem basketitem, Basket basket)
        {
            var itemsToUpdate = basket?.Items?.Where(x => x.Id == MovieID).ToList();

            if (itemsToUpdate != null)
            {
                foreach (var item in itemsToUpdate)
                {
                    item.Id = basketitem.Id;
                    item.Price = basketitem.Price;
                    item.Quantity = basketitem.Quantity;
                    item.Title = basketitem.Title;
                    item.CompanyName = basketitem.CompanyName;
                }
                await _repository.UpdateBasketAsync(basket);
            }
        }

    }
}
 