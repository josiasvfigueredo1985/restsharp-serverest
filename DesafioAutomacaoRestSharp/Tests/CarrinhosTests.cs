using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Requests.Carrinhos;
using DesafioAutomacaoRestSharp.Steps;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DesafioAutomacaoRestSharp.Tests
{
    [TestFixture]
    public class CarrinhosTests : TestBase
    {
        #region Testes Positivos

        [Test]
        public void ListarCarrinhosCadastrados()
        {
            int quantidade = 1;
            GetCarrinhos get = new GetCarrinhos();

            //Excluir os carrinhos
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            // Criar os carrinho com produtos
            CarrinhosStep.CriarCarrinhoUnicoProduto();

            //Listar o carrinho cadastrado
            IRestResponse<dynamic> response = get.ExecuteRequest();

            Console.WriteLine("Carrinho(s) cadastrado(s): {0}", response.Content.ToString());

            //Deletar produto
            ProdutosStep.DeletarTodosProdutosCriados();
            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(response.Data.quantidade >= quantidade);
        }

        [Test]
        public void CadastrarCarrinho()
        {
            Thread.Sleep(1000);
            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();

            string mensagem = "Cadastro realizado com sucesso";
            //Criar produto para cadastrar no carrinho
            string idProd = ProdutosStep.CriarProduto().Data._id.Value;

            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(response.Data.message == mensagem);
        }

        [Test]
        public void BuscarCarrinhoPorID()
        {
            //Criar produto para inserir no carrinho
            string idProd = ProdutosStep.CriarProduto().Data._id;//Utilizando os atributos do objeto dinâmico retornados do ExecuteRequest

            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse<dynamic> responseCarrinho = post.ExecuteRequest();

            // Consultar o carrinho criado
            GetCarrinhoPorId getById = new GetCarrinhoPorId(Convert.ToString(responseCarrinho.Data._id));
            IRestResponse response = getById.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            //Deletar produto
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.IsTrue(response.IsSuccessful);
        }

        [Test]
        public void ExcluirCarrinhoCancelarCompra()
        {
            //Deletar produto
            ProdutosStep.DeletarTodosProdutosCriados();

            string mensagem = "Registro excluído com sucesso. Estoque dos produtos reabastecido";
            //Criar produto para inserir no carrinho
            string idProd = ProdutosStep.CriarProduto().Data._id;

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse responseCarrinho = post.ExecuteRequest();

            //Deletar carrinho
            DeleteCancelarCompra cancelarCompra = new DeleteCancelarCompra();
            IRestResponse<dynamic> response = cancelarCompra.ExecuteRequest();

            Console.WriteLine("Response cancelar compra carrinho: " + response.Data);

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == (string)response.Data.message);
        }

        [Test]
        public void ExcluirCarrinhoConcluirCompra()
        {
            //Deletar produto
            ProdutosStep.DeletarTodosProdutosCriados();

            string mensagem = "Registro excluído com sucesso";
            //Criar produto para inserir no carrinho
            string idProd = ProdutosStep.CriarProduto().Data._id;

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse responseCarrinho = post.ExecuteRequest();

            //Deletar carrinho
            DeleteConcluirCompra concluiCompra = new DeleteConcluirCompra();
            IRestResponse<dynamic> response = concluiCompra.ExecuteRequest();

            Console.WriteLine("Response concluir compra carrinho: " + response.Data);

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == (string)response.Data.message);
        }

        #endregion Testes Positivos

        #region Testes Negativos

        [Test]
        public void CadastrarCarrinhoDuplicado()
        {
            Thread.Sleep(1000);
            string mensagem = "Não é permitido ter mais de 1 carrinho";
            string idProd = ProdutosStep.CriarProduto().Data._id;
       
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);

            post.ExecuteRequest();
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(response.Data.message == mensagem);
        }

        [Test]
        public void  CadastrarCarrinhoComProdutoDuplicado()
        {
            Thread.Sleep(500);
            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            string mensagem = "Não é permitido possuir produto duplicado";
            string jsonPath = GeneralHelpers.ReturnProjectPath() + "Jsons/CarrinhoDataSet.json";
            List<string> carrinhoDup = new List<string>();
            //Cadastrar novo produto
            string idProd = ProdutosStep.CriarProduto().Data._id;

            // Criando lista com o mesmo produto
            carrinhoDup.Add(idProd);
            carrinhoDup.Add(idProd);

            //Criando json com o produto duplicado
            CarrinhoDataSet.CriarJsonDatSet(carrinhoDup, 1);

            //Criar novo carrinho inserindo produtos duplicados
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(jsonPath);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(response.Data.message == mensagem);
        }

        [Test]
        public void BuscarCarrinhoPorIDInexistente()
        {
            string id = "idInexistente";
            string mensagem = "Carrinho não encontrado";

            GetCarrinhoPorId get = new GetCarrinhoPorId(id);
            IRestResponse<dynamic> response = get.ExecuteRequest();

            Console.WriteLine("Response buscar carrinho por id inexistente: " + response.Content.ToString());

            Assert.False(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(mensagem == (string)response.Data.message);
        }

        #endregion Testes Negativos
    }
}