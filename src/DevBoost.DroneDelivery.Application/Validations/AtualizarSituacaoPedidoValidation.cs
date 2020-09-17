using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DevBoost.DroneDelivery.Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class AtualizarSituacaoPedidoValidation: AbstractValidator<AtualizarSituacaoPedidoCommand>
    {
        public AtualizarSituacaoPedidoValidation()
        {

        }
    }
}
