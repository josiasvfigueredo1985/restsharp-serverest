using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
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