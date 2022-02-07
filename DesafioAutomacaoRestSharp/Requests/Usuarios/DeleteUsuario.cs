using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Usuarios
{
    public class DeleteUsuario : RequestBase
    {
        public DeleteUsuario(string idUsuario)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"usuarios/{idUsuario}";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}