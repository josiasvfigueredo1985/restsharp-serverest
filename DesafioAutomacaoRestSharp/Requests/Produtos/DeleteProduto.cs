using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    public class DeleteProduto : RequestBase
    {
        public DeleteProduto(string idProduto)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"produtos/{idProduto}";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}