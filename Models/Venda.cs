using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagamentoAPI.Models;

namespace PagamentoAPI.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public Vendedor Vendedor { get; set; }
        public List<string> Itens {get;set;}
        
        public StatusVenda Status {get;set;}
    }
}