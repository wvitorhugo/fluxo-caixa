using AutoMapper;
using FluxoCaixa.Movimentacao.Application.Interfaces;
using FluxoCaixa.Movimentacao.Domain;
using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Movimentacao.Domain.Entities;
using FluxoCaixa.Movimentacao.Domain.Exceptions;
using FluxoCaixa.Movimentacao.Domain.Enums;
using FluxoCaixa.Movimentacao.Application.Strategy;
using FluxoCaixa.Shared.MessageBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace FluxoCaixa.Movimentacao.Application.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ILancamentoStrategy _ILancamentoStrategy;
        private readonly IMessageProducer _messageProducer;

        public LancamentoService(IUnitOfWork unitOfWork, IMapper mapper, IMessageProducer messageProducer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messageProducer = messageProducer;
        }
        public void SetStrategy(char tipo)
        {
            if (tipo.Equals((char)EnumTipo.Credito))
                this._ILancamentoStrategy = new LancamentoCreditoStrategy();
            else if (tipo.Equals((char)EnumTipo.Debito))
                this._ILancamentoStrategy = new LancamentoDebitoStrategy();
            else
                throw new NotFoundException("Tipo de lançamento incorreto");
        }

        public async Task<LancamentoDto> Add(Guid idUsuario, LancamentoDto lancamentoDto)
        {
            lancamentoDto.Tipo = Char.ToUpper(lancamentoDto.Tipo);
            SetStrategy(lancamentoDto.Tipo);

            Lancamento ultimoLancamento = GetLast(); 
            
            Lancamento lancamento = _mapper.Map<Lancamento>(lancamentoDto);
            lancamento.idUsuario = idUsuario;
            lancamento.Data = DateTime.Now;
            lancamento.ValorTotalAtualizado = this._ILancamentoStrategy.GetValorTotalAtualizado(ultimoLancamento, lancamentoDto);

            await _unitOfWork.LancamentoRepository.AddAsync(lancamento);
            await _unitOfWork.SaveAsync();
            await _messageProducer.EnviaMensagem(lancamento);

            return _mapper.Map<LancamentoDto>(lancamento);
        }
         
        public async Task<LancamentoCompletoDto> Get(int id)
        {
            var exists = await _unitOfWork.LancamentoRepository.ExistAsync(x => x.id == id);
            if (!exists)
                throw new NotFoundException("O lançamento não existe");

            var lancamento = await _unitOfWork.LancamentoRepository.GetByIdAsync(id);
            var lancamentoDto = _mapper.Map<LancamentoCompletoDto>(lancamento);

            return lancamentoDto;
        }

        public async Task<IEnumerable<LancamentoCompletoDto>> GetAll()
        {
            var lancamentos = await _unitOfWork.LancamentoRepository.GetAllAsync();
            var lancamentosDto = _mapper.Map<IEnumerable<LancamentoCompletoDto>>(lancamentos);

            return lancamentosDto;
        }

        public Lancamento GetLast()
        {
            Lancamento lancamento = _unitOfWork.LancamentoRepository.GetLast().Result;

            return lancamento;
        }
    }
}
