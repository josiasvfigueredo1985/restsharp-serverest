using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    class GetUsuarioPorId : RequestBase
    {
        public GetUsuarioPorId(string usuarioId)
        {
            requestService = $"usuarios/{usuarioId}";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
