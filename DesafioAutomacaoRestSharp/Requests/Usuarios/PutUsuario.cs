using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;
using System.IO;

namespace DesafioAutomacaoRestSharp.Requests.Usuarios
{
    public class PutUsuario : RequestBase
    {
        public PutUsuario(string idUsuario)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
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