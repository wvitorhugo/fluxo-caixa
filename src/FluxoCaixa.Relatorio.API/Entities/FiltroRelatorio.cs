using FluxoCaixa.Shared.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Relatorio.API.Entities
{
    public class FiltroRelatorio
    {
        [YearRange(2020, ErrorMessage = "Ano informado inválido")]
        public int Ano { get; set; }
        [Range(1, 12, ErrorMessage = "Mês informado inválido")]
        public int Mes { get; set; }
        [Range(1, 31, ErrorMessage = "Dia informado inválido")]
        public int Dia { get; set; }
    }
}
