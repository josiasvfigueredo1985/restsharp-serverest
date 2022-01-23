using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Pagamento
{
    public class DeletePagamento : RequestBase
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