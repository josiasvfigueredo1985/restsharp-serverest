using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
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