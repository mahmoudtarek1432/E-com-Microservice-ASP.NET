using AutoMapper;
using Basket.API.Entites;
using EventBus.Messages.Events;

namespace Basket.API.mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
