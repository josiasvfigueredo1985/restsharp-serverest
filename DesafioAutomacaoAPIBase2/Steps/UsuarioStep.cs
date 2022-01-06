using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Newtonsoft;
using DesafioAutomacaoAPIBase2.Requests.Usuarios;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json.Linq;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class UsuarioStep
    {
        public IRestResponse CriarNovoUsuario(string nome,string email, string pwd, bool admin )
        {

            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome,email,pwd,admin);
            IRestResponse response = post.ExecuteRequest();
            return response;
        }

        public IRestResponse AtualizarUsuario(string nome, string email, string pwd, bool admin)
        {
            dynamic jsonData = JObject.Parse(CriarNovoUsuario( nome,  email,  pwd,  admin).Content);
            string idUsuario = jsonData._id;

            PutUsuario put = new PutUsuario(idUsuario);
            IRestResponse response = put.ExecuteRequest();
            return response;

        }

        public IRestResponse DeletarUsuario(string idUsuario)
        {
            DeleteUsuario delete = new DeleteUsuario(idUsuario);
            IRestResponse response = delete.ExecuteRequest();
            return response;
        }
    }
}
