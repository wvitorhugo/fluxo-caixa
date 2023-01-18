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
    public class LancamentoDto
    {  
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^[D.?C]$", ErrorMessage = "O campo {0} deve ser C - Crédito ou D - Débito")]
        public char Tipo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0.01, 9999999.99, ErrorMessage = "O campo {0} deve estar entre {1} e {2}")] 
        public decimal Valor { get; set; }
        
        public string Descricao { get; set; }
    }
}
