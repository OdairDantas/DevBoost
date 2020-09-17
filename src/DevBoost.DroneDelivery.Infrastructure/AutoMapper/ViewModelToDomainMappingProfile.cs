using AutoMapper;
using DevBoost.DroneDelivery.Application.ViewModels;
using DevBoost.DroneDelivery.Domain.Entities;

namespace DevBoost.DroneDelivery.Infrastructure.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>()
                .ConstructUsing(c => new Cliente(c.Nome, c.Latitude, c.Longitude));

            CreateMap<DroneViewModel, Drone>()
                .ConstructUsing(d => new Drone(d.Capacidade, d.Velocidade, d.Autonomia, d.AutonomiaRestante)).ReverseMap();

            CreateMap<UsuarioViewModel, Usuario>()
               .ConstructUsing(d => new Usuario(d.UserName, d.Password, d.Role, d.ClienteId)).ReverseMap();

            CreateMap<Cliente, ClienteViewModel>()
               .ForMember(d => d.Nome, o => o.MapFrom(o => o.Nome))
                .ForMember(d => d.Id, o => o.MapFrom(o => o.Id))
                .ForMember(d => d.Longitude, o => o.MapFrom(o => o.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(o => o.Latitude));

            CreateMap<Pedido, PedidoViewModel>()
               .ForMember(d => d.Valor, o => o.MapFrom(o => o.Valor))
               .ForMember(d => d.Situacao, o => o.MapFrom(o => o.Status))
               .ForMember(d => d.Cliente, o => o.MapFrom(o => new ClienteViewModel() { Nome = o.Cliente.Nome, Id = o.Cliente.Id, Latitude = o.Cliente.Latitude, Longitude = o.Cliente.Longitude }))
               .ForMember(d => d.Drone, o => o.MapFrom(o => new DroneViewModel() { Autonomia = o.Drone.Autonomia, AutonomiaRestante = o.Drone.AutonomiaRestante, Capacidade = o.Drone.Capacidade, Carga = o.Drone.Carga, Velocidade = o.Drone.Velocidade, Id = o.Drone.Id }))
               .ForMember(d => d.Peso, o => o.MapFrom(o => o.Peso))
               .ForMember(d => d.Id, o => o.MapFrom(o => o.Id));
        }

    }
}
