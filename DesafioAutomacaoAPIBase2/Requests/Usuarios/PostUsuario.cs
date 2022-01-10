using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using DesafioAutomacaoAPIBase2.Helpers;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    class PostUsuario : RequestBase
    {
        public PostUsuario()
        {
            requestService = "usuarios";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string nome, string email, string password, bool administrador)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath()+"Jsons/Usuario.json");
            jsonBody = jsonBody.Replace("$nome",nome);
            jsonBody = jsonBody.Replace("$email",email);
            jsonBody = jsonBody.Replace("$password",password);
            jsonBody = jsonBody.Replace("$administrador",administrador.ToString().ToLower());
            //          {
            //              "nome": "$nome",
            //"email": "$email",
            //"password": "$password",
            //"administrador": "$administrador"
            //          }
        }
    }
}
