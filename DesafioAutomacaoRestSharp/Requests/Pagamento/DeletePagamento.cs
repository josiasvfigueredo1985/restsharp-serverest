using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Pagamento
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