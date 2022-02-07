using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Produtos
{
    public class GetProdutos : RequestBase
    {
        public GetProdutos()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "produtos";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}