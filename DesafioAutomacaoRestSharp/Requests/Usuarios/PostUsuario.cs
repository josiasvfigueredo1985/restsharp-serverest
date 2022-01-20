using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;
using System.IO;

namespace DesafioAutomacaoAPIBase2.Requests.Usuarios
{
    public class PostUsuario : RequestBase
    {
        public PostUsuario()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "usuarios";
            method = Method.POST;
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