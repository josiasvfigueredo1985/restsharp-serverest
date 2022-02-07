using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Pagamento
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