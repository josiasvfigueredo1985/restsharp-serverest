using NUnit.Framework;
using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoRestSharp.Requests.Login;
using DesafioAutomacaoRestSharp.Steps;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using DesafioAutomacaoRestSharp.Requests.Usuarios;

namespace DesafioAutomacaoRestSharp.Tests
{
    [TestFixture]
    class LoginTests : TestBase
    {
        #region Testes Positivos
        [Test]
        public void LoginComSucesso()
        {
            string mensagem = "Login realizado com sucesso";

            //Deletar o usuário
            UsuarioStep.DeletarUsuariosCriados();

            // Criar novo usuário 
            dynamic jsonData1 = JsonConvert.DeserializeObject(UsuarioStep.CriarUsuario().Content.ToString());
            string id = UsuarioStep.CriarUsuario().Data._id;

            // Buscar pelo id do usuário
            GetUsuarioPorId get = new GetUsuarioPorId(id);
            var responseGet = get.ExecuteRequest();

            // pegar o nome e password do usuário criado
            string email = responseGet.Data.email;
            string password = responseGet.Data.password;

            //fazer login com o usuário criado
            PostLogin post = new PostLogin();
            post.SetJsonBody(email, password);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.True(response.IsSuccessful);
            Assert.AreEqual(mensagem, (string)response.Data.message);
            Assert.IsTrue(response.Content.Contains("authorization"));
        }
        #endregion

        #region Testes Negativos
        [Test]
        public void LoginEmailInvalido()
        {
            string mensagem = "email deve ser um email válido";

            //Deletar o usuário
            UsuarioStep.DeletarUsuariosCriados();

            // Criar novo usuário 
            string id = UsuarioStep.CriarUsuario().Data._id.Value;

            // Buscar pelo id do usuário
            GetUsuarioPorId get = new GetUsuarioPorId(id);
            var responseGet = get.ExecuteRequest();

            // pegar o nome e password do usuário criado
            string email = "emailInvalido";
            string password = responseGet.Data.password;

            //fazer login com o usuário criado
            PostLogin post = new PostLogin();
            post.SetJsonBody(email, password);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            //Deletar o usuário criado
            UsuarioStep.DeletarUsuariosCriados();

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.AreEqual(mensagem, response.Data.email.Value);
        }

        [Test]
        public void LoginSenhaInvalida()
        {
            string mensagem = "Email e/ou senha inválidos";

            // Criar novo usuário 
            string id = UsuarioStep.CriarUsuario().Data._id;

            // Buscar pelo id do usuário
            GetUsuarioPorId get = new GetUsuarioPorId(id);
            var responseGet = get.ExecuteRequest();

            // pegar o nome e password do usuário criado
            string email = responseGet.Data.email;
            string password = "senhaInvalida";

            //Fazer login com o usuário criado
            PostLogin post = new PostLogin();
            post.SetJsonBody(email, password);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            //Deletar o usuário
            UsuarioStep.DeletarUsuariosCriados();

            Assert.IsTrue((int)response.StatusCode == 401);
            Assert.AreEqual(mensagem, (string)response.Data.message);
        }
        #endregion
    }
}
