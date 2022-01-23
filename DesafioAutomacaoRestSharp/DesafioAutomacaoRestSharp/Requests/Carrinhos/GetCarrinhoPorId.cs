using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    public class GetCarrinhoPorId : RequestBase
    {
        public GetCarrinhoPorId(string idCarrinho)
        {
            //Listar carrinhos cadastrados por parâmetros
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"carrinhos/{idCarrinho}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}