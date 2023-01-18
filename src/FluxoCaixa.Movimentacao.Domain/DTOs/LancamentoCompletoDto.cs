using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.Domain.DTOs
{
    public class LancamentoCompletoDto : LancamentoDto
    {
        public int id { get; set; }
        public Guid idUsuario { get; set; }
        public DateTime Data { get; set; } 
        public decimal ValorTotalAtualizado { get; set; }
    }
}
