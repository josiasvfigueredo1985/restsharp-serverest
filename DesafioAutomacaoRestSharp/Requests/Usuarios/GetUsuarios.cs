using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Usuarios
{
    public class GetUsuarios : RequestBase
    {
        public GetUsuarios()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "usuarios";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
            //usuarios?_id=id&nome=nome&email=email&password=senha&administrador=true
        }

        public GetUsuarios(string id, string nome, string email, string password, bool administrador)
        {
            requestService = "usuarios";
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            method = Method.GET;
            parameters.Add("_id", id);
            parameters.Add("nome", nome);
            parameters.Add("email", email);
            parameters.Add("password", password);
            parameters.Add("administrador", administrador.ToString().ToLower());
            parameterTypeIsUrlSegment = false;
        }

        public GetUsuarios(string[] dadosUsuarios)
        {
            // _id = id & nome = nome & email = email & password = senha & administrador = true
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "usuarios";
            method = Method.GET;
            for (int i = 0; i < dadosUsuarios.Length; i++)
            {
                parameters.Add(dadosUsuarios[i], dadosUsuarios[i + 1]);
            }
            parameterTypeIsUrlSegment = false;
        }
    }
}