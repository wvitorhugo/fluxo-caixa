using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace FluxoCaixa.Relatorio.API.Entities
{
    public class LancamentoConsolidado
    {   [BsonId] 
        public int id { get; set; } 
        public DateTime Data { get; set; }
        public decimal ValorTotalCredito { get; set; }
        public decimal ValorTotalDebito { get; set; }
        public decimal ValorCaixaInicial { get; set; }
        public decimal ValorCaixaFinal { get; set; }
    }
}
