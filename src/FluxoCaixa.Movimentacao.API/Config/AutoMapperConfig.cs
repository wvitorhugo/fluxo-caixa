using AutoMapper;
using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Movimentacao.Domain.Entities;

namespace FluxoCaixa.Movimentacao.API.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<LancamentoDto, Lancamento>().ReverseMap();

            CreateMap<LancamentoCompletoDto, Lancamento>().ReverseMap();
        }
    }
}
