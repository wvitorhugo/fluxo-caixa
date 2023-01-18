using FluentValidation;
using FluxoCaixa.Movimentacao.Domain.Entities;
using System;

namespace FluxoCaixa.Movimentacao.Infra.Validators
{

    public class LancamentoValidator : AbstractValidator<Lancamento>
    {
        public LancamentoValidator()
        { 
            RuleFor(x => x.Data).LessThan(p => DateTime.Now).WithMessage("A Data deve estar no passado");
            RuleFor(x => x.Data).GreaterThan(p => new DateTime(2021, 01, 01)).WithMessage("A Data deve ser posterior a 01/01/2021"); 
            RuleFor(x => x.Tipo).NotNull().WithMessage("O Tipo do Lançamento não pode ser nulo");
            RuleFor(x => x.Valor).NotNull();
            RuleFor(x => x.Valor).NotEqual(0).WithMessage("O Lançamento deve ser diferente de 0");
            RuleFor(x => x.idUsuario).NotNull().WithMessage("O Lançamento deve possuir um Usuário vinculado");
        }
    }

}