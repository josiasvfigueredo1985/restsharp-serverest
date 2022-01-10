﻿using DesafioAutomacaoAPIBase2.Requests;
using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Requests.Carrinhos;
using DesafioAutomacaoAPIBase2.Requests.Produtos;
using System.Text.RegularExpressions;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;
using Newtonsoft.Json;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class CarrinhosTests : TestBase
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
            IRestResponse response = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine("Carrinho(s) cadastrado(s): {0}", response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(jsonData.quantidade.Value >= quantidade);

        }

        [Test]
        public void CadastrarCarrinho()
        {
            string mensagem = "Cadastro realizado com sucesso";
            //Criar produto para cadastrar no carrinho
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse response = post.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(jsonData.message.Value == mensagem);

            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            //Deletar produto
            DeleteProduto del = new DeleteProduto(idProd);
            del.ExecuteRequest();
        }

        [Test]
        public void BuscarCarrinhoPorID()
        {
            //Criar produto para inserir no carrinho
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse responseCarrinho = post.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(responseCarrinho.Content);

            // Consultar o carrinho criado
            GetCarrinhoPorId getById = new GetCarrinhoPorId(jsonData._id.Value);
            IRestResponse response = getById.ExecuteRequest();

            Console.WriteLine(response.Content);

            Assert.IsTrue((int)response.StatusCode==200);
            Assert.IsTrue(response.IsSuccessful);

            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            //Deletar produto
            DeleteProduto del = new DeleteProduto(idProd);
            del.ExecuteRequest();
        }

        [Test]
        public void ExcluirCarrinhoCancelarCompra()
        {
            string mensagem = "Registro excluído com sucesso. Estoque dos produtos reabastecido";
            //Criar produto para inserir no carrinho
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Deletar carrinhos criados anteriormente
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse responseCarrinho = post.ExecuteRequest();

            //Deletar carrinho
            DeleteCancelarCompra cancelarCompra = new DeleteCancelarCompra();
            IRestResponse response = cancelarCompra.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine("Response cancelar compra carrinho: "+jsonData);

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);

            //Deletar produto após deletar o carrinho
            DeleteProduto del = new DeleteProduto(idProd);
        }

        [Test]
        public void ExcluirCarrinhoConcluirCompra()
        {
            string mensagem = "Registro excluído com sucesso";
            //Criar produto para inserir no carrinho
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Deletar carrinhos criados anteriormente
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            // Criar novo carrinho
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            IRestResponse responseCarrinho = post.ExecuteRequest();

            //Deletar carrinho
            DeleteConcluirCompra concluiCompra = new DeleteConcluirCompra();
            IRestResponse response = concluiCompra.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine("Response concluir compra carrinho: " + jsonData);

            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);

            //Deletar produto após deletar o carrinho
            DeleteProduto del = new DeleteProduto(idProd);
        }
        #endregion

        #region Testes Negativos

        [Test]
        public void CadastrarCarrinhoDuplicado()
        {

            string mensagem = "Não é permitido ter mais de 1 carrinho";
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(idProd, 1);
            post.ExecuteRequest();
            IRestResponse response = post.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(jsonData.message.Value == mensagem);

            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            //Deletar produto
            DeleteProduto del = new DeleteProduto(idProd);
        }

        [Test]
        public void CadastrarCarrinhoComProdutoDuplicado()
        {

            string mensagem = "Não é permitido possuir produto duplicado";
            string jsonPath = GeneralHelpers.ReturnProjectPath() + "Jsons/CarrinhoDataSet.json";
            List<string> carrinhoDup = new List<string>();

            //Cadastrar novo produto
            var responseProd = ProdutosStep.CriarProduto();
            dynamic jsonProd = JsonConvert.DeserializeObject(responseProd.Content);
            var idProd = jsonProd._id.Value;

            // Criando lista com o mesmo produto
            carrinhoDup.Add(idProd);
            carrinhoDup.Add(idProd);

            // Deletar carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            //Criando json com o produto duplicado
            CarrinhoDataSet.CriarJsonDatSet(carrinhoDup,1);

            //Criar novo carrinho inserindo produtos duplicados
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(jsonPath);
            IRestResponse response = post.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.IsTrue((int)response.StatusCode==400);
            Assert.IsTrue(jsonData.message.Value == mensagem);

            //Deletar carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            //Deletar produto
            DeleteProduto del = new DeleteProduto(idProd);

        }

        [Test]
        public void BuscarCarrinhoPorIDInexistente()
        {
            string id = "idInexistente";
            string mensagem = "Carrinho não encontrado";

            GetCarrinhoPorId get = new GetCarrinhoPorId(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine("Response buscar carrinho por id inexistente: "+response.Content);

            Assert.False(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode==400);
            Assert.IsTrue(mensagem==jsonData.message.Value);
        }
        #endregion
    }
}
