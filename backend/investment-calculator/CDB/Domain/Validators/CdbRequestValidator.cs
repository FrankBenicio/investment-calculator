using Cdb.Domain.Dto;
using FluentValidation;

namespace Cdb.Domain.Validators
{
    public class CdbRequestValidator : AbstractValidator<CdbRequest>
    {
        public CdbRequestValidator()
        {
            RuleFor(x => x.InvestmentValue)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o valor de investimento.");

            RuleFor(x => x.Month)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Por favor, informe o prazo que seu dinheiro ficará investido.");
        }
    }
}
