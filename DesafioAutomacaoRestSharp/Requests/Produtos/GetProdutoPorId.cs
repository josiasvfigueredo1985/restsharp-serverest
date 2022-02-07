using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Produtos
{
    public class GetProdutoPorId : RequestBase
    {
        public GetProdutoPorId(string idProduto)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"produtos/{idProduto}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}