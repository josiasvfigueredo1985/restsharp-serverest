using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    class DeleteCancelarCompra : RequestBase
    {
        public DeleteCancelarCompra()
        {
            //Ao cancelar uma compra o carrinho é excluído e o estoque dos produtos desse carrinho é reabastecido.
            //O carrinho excluído será o vinculado ao usuário do token utilizado.
            requestService = "carrinhos/cancelar-compra";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}
