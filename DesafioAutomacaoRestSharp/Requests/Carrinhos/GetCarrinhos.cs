using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    public class GetCarrinhos : RequestBase
    {
        public GetCarrinhos()
        {
            //Listar todos os carrinhos cadastrados
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "carrinhos";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }

        public GetCarrinhos(string id, int precoTotal, int quantidadeTotal, string idUsuario)
        {
            //Listar carrinhos cadastrados por parâmetros
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "carrinhos";
            method = Method.GET;
            parameters.Add("_id", id);
            parameters.Add("precoTotal", precoTotal.ToString());
            parameters.Add("quantidadeTotal", quantidadeTotal.ToString());
            parameters.Add("idUsuario", idUsuario);
            parameterTypeIsUrlSegment = false;
        }
    }
}