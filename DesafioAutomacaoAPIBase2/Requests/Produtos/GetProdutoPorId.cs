using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    class GetProdutoPorId : RequestBase
    {
        public GetProdutoPorId(string idProduto)
        {
            requestService = $"produtos/{idProduto}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
