using AutoMapper;
using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Movimentacao.Domain.Entities;

namespace FluxoCaixa.Movimentacao.Infra.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LancamentoDto, Lancamento>().ReverseMap();

            CreateMap<LancamentoCompletoDto, Lancamento>().ReverseMap();
        }
    }

}