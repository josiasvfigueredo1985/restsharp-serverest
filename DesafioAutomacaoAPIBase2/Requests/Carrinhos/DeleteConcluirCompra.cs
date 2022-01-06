using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    class DeleteConcluirCompra : RequestBase
    {
        public DeleteConcluirCompra()
        {
            //Ao concluir uma compra o carrinho é excluído.
            //O carrinho excluído será o vinculado ao usuário do token utilizado.
            requestService = "carrinhos/concluir-compra";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}
