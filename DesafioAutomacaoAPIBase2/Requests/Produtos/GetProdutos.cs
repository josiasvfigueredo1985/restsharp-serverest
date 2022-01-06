using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    class GetProdutos : RequestBase
    {
        public GetProdutos()
        {
            requestService = "produtos";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
