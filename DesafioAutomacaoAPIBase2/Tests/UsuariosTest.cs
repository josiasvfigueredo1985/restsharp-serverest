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

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

            Assert.True(response.IsSuccessful);
            Assert.IsTrue(jsondata.quantidade.Value >= quantidade);
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

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

            Assert.True(response.IsSuccessful);
            Assert.IsTrue(jsonData.quantidade == qtd);
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
            post.SetJsonBody(nome, email, password, administrador);

            IRestResponse response = post.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
            string id = jsonData._id.Value;

            Console.WriteLine(jsonData);

            //Deletar usuário cadastrado
            UsuarioStep.DeletarUsuarioPorId(id);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(mensagem, jsonData.message.Value);
        }

        [Test]
        public void BuscarUsuarioPorID()
        {
            string nome = "Pleonário Silvestre";

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

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

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuarioPorId(id);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(nome, jsonData.nome.Value);
        }

        [Test]
        public void EditarUsuario()
        {
            string nome = "Felisbino Plauzébio";
            string email = "felis_2002@yahoo.com.br";
            string password = "adm2000";
            bool administrador = false;

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

            string mensagem = "Registro alterado com sucesso";
            //Criar usuário
            UsuarioStep.CriarUsuario();

            // Pegar os dados do primeiro usuário que foi armazenado no banco de dados
            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            string id = dadosUsuario[4];

            // Atualizar usuário
            PutUsuario put = new PutUsuario(id);
            put.SetJsonBody(nome, email, password, administrador);
            IRestResponse response = put.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(jsonData);

            // Deleta usuário criado
            UsuarioStep.DeletarUsuarioPorId(id);

            Assert.AreEqual(mensagem, jsonData.message.Value);
            Assert.True(response.IsSuccessful);
        }

        [Test]
        public void ExcluirUsuario()
        {
            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

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

            //Deleta usuário criado no banco de dados
            UsuarioStep.DeletarUsuarioPorIdBancoDados(id);

            Assert.True(response.IsSuccessful);
            Assert.AreEqual(mensagem, jsonData.message.Value);
        }
        #endregion


        #region Testes Negativos
        [Test]
        public void CadastrarUsuarioDuplicado()
        {
            string mensagem = "Este email já está sendo usado";
            string nome = "Valdonildo Clauzedias Santífico";
            string email = "valdo_1973_@msn.com.br";
            string pwd = "valdo_1975";
            bool adm = false;
            // Cadastrar 1x
            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome, email, pwd, adm);

            IRestResponse response1 = post.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response1.Content.ToString());
            string id = jsonData._id.Value;

            // Cadastrar 2x
            IRestResponse response2 = post.ExecuteRequest();
            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content.ToString());
            string msgAtual = jsonData2.message.Value;

            // Excluir o usuário criado
            UsuarioStep.DeletarUsuarioPorId(id);

            Assert.AreEqual(mensagem, msgAtual);
            Assert.True((int)response2.StatusCode == 400);
        }

        [Test]
        public void BuscarUsuarioPorIDInexistente()
        {
            string id = "idInexistente";
            string msg = "Usuário não encontrado";

            GetUsuarioPorId get = new GetUsuarioPorId(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.AreEqual(msg, jsonData.message.Value);
        }

        [Test]
        public void EditarUsuarioEmailExistente()
        {
            string msg = "Este email já está sendo usado";
            string nome = "Joâo Silva";
            string email = "pleo_1982@hotmail.com";
            string pwd = "adm1234";
            bool adm = false;

            //Deletar usuários cadastrados
            UsuarioStep.DeletarUsuariosCriados();

            // Criar um usuário
            dynamic idUsuarioCriado = JsonConvert.DeserializeObject(UsuarioStep.CriarUsuario().Content);

            //Criar o usuário para atualizar
            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome, "atualizado" + email, pwd, adm);
            var responsePost = post.ExecuteRequest();
            dynamic jsonData1 = JsonConvert.DeserializeObject(responsePost.Content);
            string id = jsonData1._id.Value;

            // Atualizar com o mesmo email criado pelo step
            PutUsuario put = new PutUsuario(id);
            put.SetJsonBody(nome, email, pwd, adm);

            IRestResponse response = put.ExecuteRequest();

            Console.WriteLine(response.Content);

            dynamic jsonData2 = JsonConvert.DeserializeObject(response.Content);

            //Deletar os usuários criados
            UsuarioStep.DeletarUsuarioPorId(id);
            UsuarioStep.DeletarUsuarioPorId(idUsuarioCriado._id.Value);

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.AreEqual(msg, jsonData2.message.Value);
        }

        [Test]
        public void ExcluirUsuarioComCarrinhoCadastrado()
        {
            string msg = "Não é permitido excluir usuário com carrinho cadastrado";
            string id = JsonBuilder.ReturnParameterAppSettings("USER_ID");

            // Criar carrinho
            dynamic jsonCar = JsonConvert.DeserializeObject(CarrinhosStep.CriarCarrinhoUnicoProduto().Content);
            string idCar = jsonCar._id.Value;

            // Tentar excluir o usuário
            DeleteUsuario delete = new DeleteUsuario(id);
            IRestResponse response = delete.ExecuteRequest();

            Console.WriteLine(response.Content);

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            // Excluir carrinho criado
            CarrinhosStep.DeletarCarrinhoCancelarCompra();

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.AreEqual(idCar,jsonData.idCarrinho.Value);
            Assert.AreEqual(msg, jsonData.message.Value);
        }
        #endregion
    }
}
