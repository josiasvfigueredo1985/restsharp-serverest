using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using DesafioAutomacaoAPIBase2.Helpers;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    class PutUsuario : RequestBase
    {
        public PutUsuario(string idUsuario)
        {
            requestService = $"usuarios/{idUsuario}";
            method = Method.PUT;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string nome, string email, string password, bool administrador)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Usuario.json");
            jsonBody = jsonBody.Replace("$nome", nome);
            jsonBody = jsonBody.Replace("$email", email);
            jsonBody = jsonBody.Replace("$password", password);
            jsonBody = jsonBody.Replace("$administrador", administrador.ToString().ToLower());
            //          {
            //              "nome": "$nome",
            //"email": "$email",
            //"password": "$password",
            //"administrador": "$administrador"
            //          }
        }
    }
}
