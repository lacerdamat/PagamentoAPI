using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagamentoAPI.Models;

namespace PagamentoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendasController : ControllerBase
    {
        static Dictionary<int, Venda> Vendas =  new Dictionary<int, Venda>();
        
        #region Publicar Vendas
        [HttpPost]
        public IActionResult PublicarVenda(Venda venda){
            if (Vendas.ContainsKey(venda.IdVenda)){
                return StatusCode(409, "Já existe uma venda com este Id");
            }
            else if (venda.Itens.Count <1){
                return StatusCode(422, "A quantidade de itens devem ser maior que 1");

            }
            venda.Status = StatusVenda.AguardandoPagamento;
            Vendas.Add(venda.IdVenda, venda);
            return Ok(Vendas[venda.IdVenda]);
        }
        #endregion
        #region Obter todas as Vendas

        [HttpGet("Obter todas as vendas")]
        public IActionResult ObterVendas(){
            return Ok(Vendas);
        }
        #endregion
        #region Obter Venda por Id
        [HttpGet]
        public IActionResult BuscarVendaPeloID(int idVenda){
            if (Vendas.ContainsKey(idVenda)){
                return Ok(Vendas[idVenda]);
            }
            return StatusCode(404, "Não achamos uma venda com este Id");

        }
        #endregion
        #region Alterar Status da Venda
        [HttpPatch]
        public IActionResult AtualizarStatus(int idVenda, StatusVenda novoStatus){
            if (Vendas.ContainsKey(idVenda)){
                switch(Vendas[idVenda].Status){
                    case StatusVenda.AguardandoPagamento:
                        if(novoStatus == StatusVenda.PagamentoAprovado || novoStatus == StatusVenda.Cancelado){
                            Vendas[idVenda].Status = novoStatus;
                            return Ok(Vendas[idVenda]);
                        }
                        else{
                            return StatusCode(422,"Essa mudança de status não é permitida");
                        }
                    case StatusVenda.PagamentoAprovado:
                        if(novoStatus == StatusVenda.EnviadoParaATransportadora || novoStatus == StatusVenda.Cancelado){
                            Vendas[idVenda].Status = novoStatus;
                            return Ok(Vendas[idVenda]);
                        }
                        else{
                            return StatusCode(422,"Essa mudança de status não é permitida");
                        }
                    case StatusVenda.EnviadoParaATransportadora:
                        if (novoStatus == StatusVenda.Entregue){ 
                            Vendas[idVenda].Status = novoStatus;
                            return Ok(Vendas[idVenda]);
                        }
                        else{
                            return StatusCode(422,"Essa mudança de status não é permitida");
                        }
                }
            }
            return StatusCode(404, "Não temos vendas com este Id");
        }
        #endregion
    }
}