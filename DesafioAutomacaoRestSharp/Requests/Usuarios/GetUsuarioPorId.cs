using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Usuarios
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