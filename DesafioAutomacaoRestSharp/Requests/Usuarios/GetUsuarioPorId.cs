using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    public class GetUsuarioPorId : RequestBase
    {
        public GetUsuarioPorId(string usuarioId)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"usuarios/{usuarioId}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}