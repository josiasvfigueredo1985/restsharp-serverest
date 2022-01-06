using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    class GetUsuarios : RequestBase
    {
        public GetUsuarios()
        {
            requestService = "usuarios";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }
    }
}
