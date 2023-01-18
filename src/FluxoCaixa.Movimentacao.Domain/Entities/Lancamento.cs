using System;

namespace FluxoCaixa.Movimentacao.Domain.Entities
{
    public class Lancamento : BaseEntity
    { 
        public Guid idUsuario { get; set; }
        public DateTime Data { get; set; }
        public char Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public decimal ValorTotalAtualizado { get; set; }
    }
}
