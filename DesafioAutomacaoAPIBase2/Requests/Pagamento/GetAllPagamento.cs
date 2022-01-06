using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Pagamento
{
    class GetAllPagamento : RequestBase
    {
        public GetAllPagamento()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL_MOCK");
            requestService = "pagamento/tipoPagamento";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
