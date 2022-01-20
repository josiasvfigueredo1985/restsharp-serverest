using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Pagamento
{
    public class GetAllPagamento : RequestBase
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