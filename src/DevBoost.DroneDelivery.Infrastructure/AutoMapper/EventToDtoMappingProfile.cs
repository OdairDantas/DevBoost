using AutoMapper;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Domain.Entities;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class EventToDtoMappingProfile : Profile
    {

        public EventToDtoMappingProfile()
        {
            CreateMap<Drone, DroneAdicionadoEvent>()
                .ForMember(d => d.AggregateRoot, o => o.MapFrom(o => o.Id))
                   .ReverseMap();
        }

    }
}
