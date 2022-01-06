using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    class GetCarrinhoPorId : RequestBase
    {
        public GetCarrinhoPorId(string idCarrinho)
        {
            //Listar carrinhos cadastrados por parâmetros
            requestService = $"carrinhos/{idCarrinho}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
