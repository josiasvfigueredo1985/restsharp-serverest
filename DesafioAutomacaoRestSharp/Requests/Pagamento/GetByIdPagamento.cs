using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Pagamento
{
    public class GetByIdPagamento : RequestBase
    {
        public GetByIdPagamento(string id)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL_MOCK");
            requestService = $"pagamento/tipoPagamento/{id}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}