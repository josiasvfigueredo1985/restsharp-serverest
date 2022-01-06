using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    class DeleteUsuario : RequestBase
    {
        public DeleteUsuario(string idUsuario)
        {
            requestService = $"usuario/{idUsuario}";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}
