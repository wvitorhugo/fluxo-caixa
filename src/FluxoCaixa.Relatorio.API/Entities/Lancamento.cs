using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace FluxoCaixa.Relatorio.API.Entities
{
    public class Lancamento
    {   [BsonId] 
        public int id { get; set; }
        public Guid idUsuario { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTotalAtualizado { get; set; }
    }
}
