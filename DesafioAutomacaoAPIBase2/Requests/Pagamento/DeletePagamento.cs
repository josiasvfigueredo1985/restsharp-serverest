using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Pagamento
{
    class DeletePagamento : RequestBase
    {
        public DeletePagamento(string id)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL_MOCK");
            requestService = $"pagamento/tipoPagamento/{id}";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}
