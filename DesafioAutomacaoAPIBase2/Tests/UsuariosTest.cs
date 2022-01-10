using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Usuarios;
using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Helpers;
using System.Web.UI;
using RestSharp;
using Newtonsoft.Json;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class UsuariosTests : TestBase
    {
        #region Testes Positivos

        [Test]
        public void ListarUsuariosCadastrados()
        {
            int quantidade = 10;
            //Criar usuários
            UsuarioStep.CriarUsuariosPlanilhaExcel();

            //Listar usuários criados
            GetUsuarios get = new GetUsuarios();
            IRestResponse response = get.ExecuteRequest();

            dynamic jsondata = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine($"Usuários cadastrados: {jsondata}");

            Assert.True(response.IsSuccessful);
            Assert.IsTrue(jsondata.quantidade.Value >= quantidade);

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();
        }

        [Test]
        public void ListarUsuarioCadastradoPorParametros()
        {
            int qtd = 1;
            //Criar usuário
            UsuarioStep.CriarUsuario();

            // Pegar os dados do usuário que foram armazenados no banco de dados
            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            string id = dadosUsuario[4];
            string nome = dadosUsuario[0];
            string email = dadosUsuario[1];
            string password = dadosUsuario[2];
            bool administrador = Convert.ToBoolean((dadosUsuario[3]));

            // Listar o usuário conforme os dados informados
            GetUsuarios get = new GetUsuarios(id, nome, email, password, administrador);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(jsonData);
            Assert.True(response.IsSuccessful);
            Assert.IsTrue(jsonData.quantidade == qtd);

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();
        }

        [Test]
        public void CadastrarUsuario()
        {
            string mensagem = "Cadastro realizado com sucesso";
            string nome = "Felisbino Plauzébio";
            string email = "felis_2002@yahoo.com.br";
            string password = "adm2000";
            bool administrador = true;

            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome,email,password,administrador);

            IRestResponse response = post.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
            string id = jsonData._id.Value;

            Console.WriteLine(jsonData);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(mensagem,jsonData.message.Value);

            //Deletar usuário cadastrado
            UsuarioStep.DeletarUsuarioPorId(id);

        }

        [Test]
        public void BuscarUsuarioPorID()
        {
            string nome = "Pleonário Silvestre";
            //Criar usuário
            UsuarioStep.CriarUsuario();

            // Pegar os dados do primeiro usuário que foi armazenado no banco de dados
            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            string id = dadosUsuario[4];

            // Listar o usuário conforme o id informado
            GetUsuarioPorId get = new GetUsuarioPorId(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(jsonData);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(nome,jsonData.nome.Value);
     
            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuarioPorId(id);
        }

        [Test]
        public void EditarUsuario()
        {
            string nome = "Felisbino Plauzébio";
            string email = "felis_2002@yahoo.com.br";
            string password = "adm2000";
            bool administrador = false;

            string mensagem = "Registro alterado com sucesso";
            //Criar usuário
            UsuarioStep.CriarUsuario();

            // Pegar os dados do primeiro usuário que foi armazenado no banco de dados
            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            string id = dadosUsuario[4];

            PutUsuario put = new PutUsuario(id);
            put.SetJsonBody(nome,email,password,administrador);
            IRestResponse response = put.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(jsonData);

            Assert.AreEqual(mensagem,jsonData.message.Value);
            Assert.True(response.IsSuccessful);

            // Deleta usuário criado
            UsuarioStep.DeletarUsuarioPorId(id);
        }

        [Test]
        public void ExcluirUsuario()
        {
            string mensagem = "Registro excluído com sucesso";
            //Criar usuários 
            UsuarioStep.CriarUsuario();

            // Pegar os dados do primeiro usuário que foi armazenado no banco de dados
            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            string id = dadosUsuario[4];

            // Listar o usuário conforme os dados informados
            DeleteUsuario get = new DeleteUsuario(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(jsonData);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(mensagem, jsonData.message.Value);

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();
        }
        #endregion


        #region Testes Negativos
        [Test]
        public void CadastrarUsuarioDuplicado()
        {



        }

        [Test]
        public void CadastrarUsuarioEmailInvalido()
        {



        }

        [Test]
        public void BuscarUsuarioPorIDInexistente()
        {



        }

        [Test]
        public void EditarUsuarioInexistente()
        {



        }

        [Test]
        public void ExcluirUsuarioIdInexistente()
        {



        }
        #endregion
    }
}
