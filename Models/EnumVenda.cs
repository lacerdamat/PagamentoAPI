using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PagamentoAPI.Models
{
        public enum StatusVenda{
            Cancelado,
            Entregue,
            EnviadoParaATransportadora,
            PagamentoAprovado,
            AguardandoPagamento
        }
    
}