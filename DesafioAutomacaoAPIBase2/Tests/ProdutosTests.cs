using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Produtos;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Steps;
using RestSharp;
using DesafioAutomacaoAPIBase2.DBSteps;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class ProdutosTests : TestBase
    {
        #region Testes Positivos
        [TestCaseSource(typeof(DataDrivenStep), "RetornaDadosNovosProduto"), Order(1)]
        public void CadastrarProdutosDataDrivenExcel(string nome, string preco, string descricao, string quantidade)
        {
            PostProduto post = new PostProduto();
            Random r = new Random();
            nome = nome + " " + r.NextDouble().ToString();
            post.SetJsonBody(nome, int.Parse(preco), descricao, int.Parse(quantidade));
            IRestResponse response = post.ExecuteRequest();

            dynamic jsonData = JObject.Parse(response.Content);

            // Inserção do ID gerado em cada response na tabela "produto" no banco de dados
            SolicitacaoDBSteps.InserirProdutoCriadoDB(jsonData._id.Value);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("Created", response.StatusCode.ToString());
            Assert.AreEqual("Cadastro realizado com sucesso", jsonData.message.Value);
        }

        [TestCaseSource(typeof(DataDrivenStep), "ReturnDataUsingDataBase"), Order(2)]
        public void DeletarProdutosDataDrivenBancoDados(string idProduto)
        {
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response = delete.ExecuteRequest();

            //Deleta cada id do banco para não gerar um test case com argumentos inválidos
            SolicitacaoDBSteps.DeletarIdProduto(idProduto);

            dynamic jsonData = JObject.Parse(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("OK", response.StatusCode.ToString());
            Assert.AreEqual("Registro excluído com sucesso", jsonData.message.Value);
        }


        [Test]
        public void ListarProdutosCadastrados()
        {
            ProdutosStep.DeletarProdutosByIdsBancoDados();

            int count = 11;
            ProdutosStep.CriarProdutosPlanilhaExcel();
            GetProdutos get = new GetProdutos();

            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(response.IsSuccessful);
                Assert.AreEqual("OK", response.StatusCode.ToString());
                Assert.IsTrue(count <= jsonData.quantidade.Value);
            });

        }

        [Test]
        public void CadastrarProduto()
        {
            string mensagem = "Cadastro realizado com sucesso";
            //Criar produto
            dynamic jsonData = JsonConvert.DeserializeObject(ProdutosStep.CriarProduto().Content);

            Console.WriteLine(jsonData);

            Assert.IsTrue(mensagem == jsonData.message.Value);

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(jsonData._id.Value);
            IRestResponse response = delete.ExecuteRequest();
            Console.WriteLine(response.Content);
        }

        [Test]
        public void BuscarProdutoPorID()
        {
            //Criar produto a ser buscado
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);

            //Buscar produto
            GetProdutoPorId get = new GetProdutoPorId(jsonData1._id.Value);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.Multiple(() =>
            {
                StringAssert.Contains("Notebook Asus Ryzen 5", jsonData.nome.Value);
                Assert.IsTrue(jsonData.preco.Value == 2300);
                StringAssert.Contains("Notebook Gamer", jsonData.descricao.Value);
                Assert.IsTrue(jsonData.quantidade.Value == 1);
                Assert.IsTrue(jsonData1._id.Value == jsonData._id.Value);
            });

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(jsonData._id.Value);
            IRestResponse response2 = delete.ExecuteRequest();
            Console.WriteLine(response2.Content);
        }

        [Test]
        public void EditarProduto()
        {
            string mensagem = "Registro alterado com sucesso";
            Random r = new Random();
            //Criar produto para atualizar
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);
            var idProduto = jsonData1._id.Value;

            //Atualizar produto
            PutProduto put = new PutProduto(idProduto);
            put.SetJsonBody("Notebook Dell Basic" + r.NextDouble().ToString(), 2500,
                "Notebook Dell Intel Core i3", 1);
            IRestResponse response = put.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.IsTrue(mensagem == jsonData.message.Value);

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response2 = delete.ExecuteRequest();
            Console.WriteLine(response2.Content);
        }

        [Test]
        public void EditarProdutoComIdInexistente()
        {
            string mensagem = "Cadastro realizado com sucesso";
            string idInexistente = "idInexistente";
            Random r = new Random();
            //Criar produto para atualizar
            ProdutosStep.CriarProduto();

            //Atualizar produto com id inexistente
            PutProduto put = new PutProduto(idInexistente);
            put.SetJsonBody("Notebook Dell Basic " + r.NextDouble().ToString(), 2500,
                "Notebook Dell Intel Core i3", 1);
            IRestResponse response = put.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
            string idProduto = jsonData._id.Value;

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response2 = delete.ExecuteRequest();
            Console.WriteLine(response2.Content);
        }

        [Test]
        public void ExcluirProduto()
        {
            string mensagem = "Registro excluído com sucesso";
            //Criar produto
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);
            var idProduto = jsonData1._id.Value;

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response2 = delete.ExecuteRequest();

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content);

            Console.WriteLine(response2.Content);

            Assert.IsTrue(response2.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData2.message.Value);
        }
        #endregion


        #region Testes Negativos
        [Test]
        public void CadastrarProdutoDuplicado()
        {
            string mensagem = "Já existe produto com esse nome";
            string nome = "Pc da Xuxa";
            string desc = "Novo PC da Xuxa";
            int preco = 2300;
            int qtd = 1;

            //Criar produto 1x
            PostProduto post = new PostProduto();
            post.SetJsonBody(nome, preco, desc, qtd);
            var response1 = post.ExecuteRequest();

            Console.WriteLine(response1.Content);
            // pegar id pra excluir produto criado
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);
            var idProduto = jsonData1._id.Value;

            //Criar produto 2x
            post.SetJsonBody(nome, preco, desc, qtd);
            var response2 = post.ExecuteRequest();

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content);

            Assert.IsTrue(response2.StatusCode.ToString() == "BadRequest");
            Assert.IsTrue(mensagem == jsonData2.message.Value);

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response3 = delete.ExecuteRequest();
            Console.WriteLine(response3.Content);
        }

        [Test]
        public void EditarProdutoDuplicado()
        {
            string mensagem = "Já existe produto com esse nome";
            //Criar produto para atualizar
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);
            var idProduto = jsonData1._id.Value;

            //Atualizar produto 1x
            PutProduto put = new PutProduto(idProduto);
            put.SetJsonBody("Notebook Dell Basic", 2500,
                "Notebook Dell Intel Core i3", 1);
            put.ExecuteRequest();

            //Atualizar produto 2x
            put.SetJsonBody("Notebook Dell Basic", 2500,
                "Notebook Dell Intel Core i3", 1);
            IRestResponse response2 = put.ExecuteRequest();

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content);

            Console.WriteLine(response2.Content);

            Assert.IsTrue(mensagem == jsonData2.message.Value);
            Assert.AreEqual("BadRequest", response2.StatusCode.ToString());

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response3 = delete.ExecuteRequest();
            Console.WriteLine(response3.Content);
        }

        [Test]
        public void BuscarProdutoPorIDInexistente()
        {
            string idInexistente = "idInexistente";
            string mensagem = "Produto não encontrado";

            GetProdutoPorId get = new GetProdutoPorId(idInexistente);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine(response.Content);

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(jsonData.message == mensagem);
        }

        [Test]
        public void ExcluirProdutoIdInexistente()
        {
            string idProduto = "idInexistente";
            string mensagem = "Nenhum registro excluído";

            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response = delete.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);
        }
        [Test]
        public void ExcluirProdutoDeUmCarrinho()
        {
            string mensagem = "Não é permitido excluir produto que faz parte de carrinho";
            // Criar produto
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content);
            var idProduto = jsonData1._id.Value;

            //Criar um carrinho com o produto cadastrado
            CarrinhosStep.CriarCarrinhoUnicoProduto(idProduto);

            //Tentar excluir o produto que foi inserido no carrinho
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response = delete.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            Console.WriteLine("Mensagem ao tentar excluir produto: {0}",response.Content);

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(jsonData.message.Value == mensagem);

            //Excluir carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
   
        }
        #endregion
    }
}
