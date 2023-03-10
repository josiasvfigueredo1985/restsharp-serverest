using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Requests.Produtos;
using DesafioAutomacaoAPIBase2.Steps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Threading;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    public class ProdutosTests : TestBase
    {
        #region Testes Positivos

        [TestCaseSource(typeof(DataDrivenStep), "RetornaDadosNovosProduto")]
        public void CadastrarProdutosDataDrivenExcel(string nome, string preco, string descricao, string quantidade)
        {
            PostProduto post = new PostProduto();
            Random r = new Random();
            nome = nome + " " + r.NextDouble().ToString();
            post.SetJsonBody(nome, int.Parse(preco), descricao, int.Parse(quantidade));
            IRestResponse response = post.ExecuteRequest();

            //Thread.Sleep(1000);

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            // Inserção do ID gerado em cada response na tabela "produto" no banco de dados
           // SolicitacaoDBSteps.InserirProdutoCriadoDB(jsonData._id.Value);

            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual("Created", response.StatusCode.ToString());
            Assert.AreEqual("Cadastro realizado com sucesso", jsonData.message.Value);
        }

        [Test]
        public void ListarProdutosCadastrados()
        {
            // Deleta todos os dados que podem haver no banco
            ProdutosStep.DeletarTodosProdutosCriados();

            int count = 11;
            ProdutosStep.CriarProdutosPlanilhaExcel();
            GetProdutos get = new GetProdutos();

            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            //Deleta novamente os dados criados
            //ProdutosStep.DeletarTodosProdutosCriados();

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
            dynamic jsonData = JsonConvert.DeserializeObject(ProdutosStep.CriarProduto().Content.ToString());

            Console.WriteLine(jsonData);

            //Deletar produto criado
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue(mensagem == jsonData.message.Value);
        }

        [Test]
        public void BuscarProdutoPorID()
        {
            //Criar produto a ser buscado
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());

            //Buscar produto
            GetProdutoPorId get = new GetProdutoPorId(jsonData1._id.Value);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            // Deletar todos os produtos já criados
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.Multiple(() =>
            {
                StringAssert.Contains("Notebook Asus Ryzen 5", jsonData.nome.Value);
                Assert.IsTrue(jsonData.preco.Value == 2300);
                StringAssert.Contains("Notebook Gamer", jsonData.descricao.Value);
                Assert.IsTrue(jsonData.quantidade.Value == 1);
                Assert.IsTrue(jsonData1._id.Value == jsonData._id.Value);
            });
        }

        [Test]
        public void EditarProduto()
        {
            string mensagem = "Registro alterado com sucesso";
            Random r = new Random();

            //Criar produto para atualizar
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());
            var idProduto = jsonData1._id.Value;

            //Atualizar produto
            PutProduto put = new PutProduto(idProduto);
            put.SetJsonBody("Notebook Dell Basic" + r.NextDouble().ToString(), 2500,
                "Notebook Dell Intel Core i3", 1);
            IRestResponse response = put.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            //Deletar produto criado
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue(mensagem == jsonData.message.Value);
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

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());
            string idProduto = jsonData._id.Value;

            Console.WriteLine(response.Content.ToString());

            //Deletar produto criado
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);
        }

        [Test]
        public void ExcluirProduto()
        {
            string mensagem = "Registro excluído com sucesso";

            //Criar produto
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());
            var idProduto = jsonData1._id.Value;

            //Deletar produto criado
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response2 = delete.ExecuteRequest();

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content.ToString());

            Console.WriteLine(response2.Content.ToString());

            // Deletar todos os produtos já criados
            //ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue(response2.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData2.message.Value);
        }

        #endregion Testes Positivos

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

            Console.WriteLine("Primeiro cadastro: " + response1.Content.ToString());

            // pegar id pra excluir produto criado
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());
            var idProduto = jsonData1._id.Value;

            //Criar produto 2x
            post.SetJsonBody(nome, preco, desc, qtd);
            var response2 = post.ExecuteRequest();

            Console.WriteLine("Segundo cadastro: " + response2.Content.ToString());

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content.ToString());

            // Deletar produto criado
            ProdutosStep.DeletarProdutoById(idProduto);

            Assert.IsTrue(response2.StatusCode.ToString() == "BadRequest");
            Assert.IsTrue(mensagem == jsonData2.message.Value);
        }

        [Test]
        public void EditarProdutoDuplicado()
        {
            string mensagem = "Já existe produto com esse nome";

            string nomeAd = "Atualizado";
            string nome = "Notebook Dell Basic";
            string desc = "Notebook Dell Intel Core i3";
            int preco = 2500;
            int qtde = 1;

            // Deletar todos os produtos já criados
            ProdutosStep.DeletarTodosProdutosCriados();

            // Criar produto com mesmo nome da atualização
            ProdutosStep.CriarProdutoUnico("");

            //Criar produto para atualizar
            IRestResponse response1 = ProdutosStep.CriarProdutoUnico(nomeAd);
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());
            string idProduto = jsonData1._id.Value;

            //Atualizar produto com nome do produto criado anteriormente
            PutProduto put = new PutProduto(idProduto);
            put.SetJsonBody(nome, preco, desc, qtde);
            IRestResponse response2 = put.ExecuteRequest();

            dynamic jsonData2 = JsonConvert.DeserializeObject(response2.Content.ToString());

            Console.WriteLine(response2.Content.ToString());

            //Deletar produto criado
            ProdutosStep.DeletarProdutoById(idProduto);
            // Deletar todos os produtos já criados
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue(mensagem == jsonData2.message.Value);
            Assert.AreEqual("BadRequest", response2.StatusCode.ToString());
        }

        [Test]
        public void BuscarProdutoPorIDInexistente()
        {
            string idInexistente = "idInexistente";
            string mensagem = "Produto não encontrado";

            GetProdutoPorId get = new GetProdutoPorId(idInexistente);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());
            Console.WriteLine(response.Content.ToString());

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

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue(response.IsSuccessful);
            Assert.IsTrue(mensagem == jsonData.message.Value);
        }

        [Test]
        public void ExcluirProdutoDeUmCarrinho()
        {
            string mensagem = "Não é permitido excluir produto que faz parte de carrinho";

            // Deletar todos os produtos já criados
            ProdutosStep.DeletarTodosProdutosCriados();
            //Excluir carrinhos criados
            CarrinhosStep.DeletarCarrinhoCancelarCompra();

            // Criar produto
            IRestResponse response1 = ProdutosStep.CriarProduto();
            dynamic jsonData1 = JsonConvert.DeserializeObject(response1.Content.ToString());
            var idProduto = jsonData1._id.Value;

            //Criar um carrinho com o produto cadastrado
            CarrinhosStep.CriarCarrinhoUnicoProduto(idProduto);

            //Tentar excluir o produto que foi inserido no carrinho
            DeleteProduto delete = new DeleteProduto(idProduto);
            IRestResponse response = delete.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine("Mensagem ao tentar excluir produto: " + response.Content.ToString());

            //Excluir carrinho
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            // Deletar todos os produtos já criados
            ProdutosStep.DeletarTodosProdutosCriados();

            Assert.IsTrue((int)response.StatusCode == 400);
            Assert.IsTrue(jsonData.message.Value == mensagem);
        }

        #endregion Testes Negativos
    }
}