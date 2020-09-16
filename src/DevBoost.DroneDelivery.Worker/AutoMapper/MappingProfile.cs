using AutoMapper;
using DevBoost.DroneDelivery.Worker.Events;

namespace DevBoost.DroneDelivery.Worker.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<PagamentoSolicitadoEvent, PedidoSolicitadoEvent>()
                    .ForMember(d => d.NumeroCartao, o => o.MapFrom(o => o.NumeroCartao))
                    .ForMember(d => d.Bandeira, o => o.MapFrom(o => o.BandeiraCartao))
                    .ForMember(d => d.AnoVencimento, o => o.MapFrom(o => o.AnoVencimentoCartao))
                    .ForMember(d => d.MesVencimento, o => o.MapFrom(o => o.MesVencimentoCartao))
                    .ForMember(d => d.AggregateRoot, o => o.MapFrom(o => o.PedidoId))
                    .ForMember(d => d.Valor, o => o.MapFrom(o => o.Valor)).ReverseMap();
                    
        }
    }
}
