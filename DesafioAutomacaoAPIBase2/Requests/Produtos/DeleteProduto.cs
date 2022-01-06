using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    class DeleteProduto : RequestBase
    {
        public DeleteProduto(string idProduto)
        {
            requestService = $"produtos/{idProduto}";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}
