using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Produtos
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