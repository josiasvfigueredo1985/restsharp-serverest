using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Carrinhos
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