using FluxoCaixa.Relatorio.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Relatorio.API.Services
{
    public class LancamentoServices
    {
        private readonly IMongoCollection<Lancamento> _lancamentoCollection;

        public LancamentoServices(IOptions<FluxoCaixaDatabaseSettings> lancamentoServices)
        {
            var mongoClient = new MongoClient(lancamentoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(lancamentoServices.Value.DatabaseName);

            _lancamentoCollection = mongoDatabase.GetCollection<Lancamento>
                (lancamentoServices.Value.CollectionName);

        }

        public async Task<List<Lancamento>> GetAsync() =>
            await _lancamentoCollection.Find(x => true).ToListAsync();
        public async Task<LancamentoConsolidado> GetLancamentoConsolidado(int ano, int mes, int dia)
        {
            DateTime dataInicial = new DateTime(ano, mes, dia);
            DateTime dataFinal = new DateTime(ano, mes, dia).AddDays(1);

            List<Lancamento> lancamentos = await _lancamentoCollection.Find(x => x.Data > dataInicial && x.Data < dataFinal).ToListAsync();

            decimal valorInicialDia = GetValorInicialDia(dataInicial);

            LancamentoConsolidado lancamentosConsolidados = new LancamentoConsolidado
            {
                Data = dataInicial, 
                ValorCaixaInicial = valorInicialDia
            };

            if (lancamentos.Count() == 0)
            {
                lancamentosConsolidados.ValorCaixaFinal = valorInicialDia;
                return lancamentosConsolidados;
            }

            lancamentosConsolidados.ValorTotalCredito = lancamentos.Where(x => x.Tipo.Equals("C")).Sum(x => x.Valor);
            lancamentosConsolidados.ValorTotalDebito = lancamentos.Where(x => x.Tipo.Equals("D")).Sum(x => x.Valor);
            lancamentosConsolidados.ValorCaixaFinal = lancamentos.LastOrDefault().ValorTotalAtualizado;

            return lancamentosConsolidados;
        }

        private decimal GetValorInicialDia(DateTime dataInicial)
        {
            Lancamento ultimoAnterior = _lancamentoCollection.Find(x => x.Data < dataInicial).SortByDescending(x => x.Data).FirstOrDefault();

            if (ultimoAnterior == null)
                return 0;
            else 
                return ultimoAnterior.ValorTotalAtualizado;
        }

        public async Task<List<Lancamento>> GetLancamentos(int ano, int mes, int dia)
        {
            DateTime dataInicial = new DateTime(ano, mes, dia);
            DateTime dataFinal = new DateTime(ano, mes, dia).AddDays(1);

            return await _lancamentoCollection.Find(x => x.Data > dataInicial && x.Data < dataFinal).ToListAsync();
        }

        public async Task<Lancamento> GetAsync(int id) =>
           await _lancamentoCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Lancamento lancamento) =>
            await _lancamentoCollection.InsertOneAsync(lancamento);
        public async Task UpdateAsync(int id, Lancamento lancamento) =>
           await _lancamentoCollection.ReplaceOneAsync(x => x.id == id, lancamento);
        public async Task RemoveAsync(int id) =>
            await _lancamentoCollection.DeleteOneAsync(x => x.id == id);
    }
}
