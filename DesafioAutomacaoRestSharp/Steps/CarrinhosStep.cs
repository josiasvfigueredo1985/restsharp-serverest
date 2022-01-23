using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Requests.Carrinhos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DesafioAutomacaoAPIBase2.Steps
{
    public class CarrinhosStep
    {
        public static IRestResponse CriarCarrinhoComProdutosDataSet()
        {
            string jsonPath = GeneralHelpers.ReturnProjectPath() + "Jsons/CarrinhoDataSet.json";

            //Busca os IDs dos produtos inseridos no banco de dados
            List<string> idProdutos = SolicitacaoDBSteps.BuscarIdsProdutos();

            // Cria um json inserindo todos os produtos já cadastrados
            CarrinhoDataSet.CriarJsonDatSet(idProdutos, 1);

            //Executa a requisição passando somente o json já criado
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(jsonPath);
            IRestResponse response = post.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse CriarCarrinhoUnicoProduto()
        {
            DeletarCarrinhoCancelarCompra();
            // DeletarCarrinhoConcluirCompra();

            //Pegar o id gerado
            dynamic jsonData = JsonConvert.DeserializeObject(ProdutosStep.CriarProduto().Content);
            string idProduto = jsonData._id.Value;

            //Criar o novo carrinho com o id do produto
            PostCarrinho carrinho = new PostCarrinho();
            carrinho.SetJsonBody(idProduto, 1);

            IRestResponse response = carrinho.ExecuteRequest();
            Thread.Sleep(1000);
            Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse CriarCarrinhoUnicoProduto(string idProduto)
        {
            //Criar o novo carrinho com o id do produto informado
            PostCarrinho carrinho = new PostCarrinho();
            carrinho.SetJsonBody(idProduto, 1);
            IRestResponse response = carrinho.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse DeletarCarrinhoConcluirCompra()
        {
            DeleteConcluirCompra delCompra = new DeleteConcluirCompra();
            IRestResponse response = delCompra.ExecuteRequest();

            // Console.WriteLine("Carrinho deletado - Concluir compra: {0}", response.Content.ToString());

            return response;
        }

        public static IRestResponse DeletarCarrinhoCancelarCompra()
        {
            DeleteCancelarCompra delCancel = new DeleteCancelarCompra();
            IRestResponse response = delCancel.ExecuteRequest();

            // Console.WriteLine("Carrinho deletado - Cancelar compra: {0}", response.Content.ToString());

            return response;
        }

        public static IRestResponse ConsultarCarrinho()
        {
            dynamic jsonData = JsonConvert.DeserializeObject(CriarCarrinhoUnicoProduto().Content);
            string id = jsonData._id.Value;
            GetCarrinhoPorId get = new GetCarrinhoPorId(id);
            IRestResponse response = get.ExecuteRequest();
            Thread.Sleep(1000);
            // Console.WriteLine(response.Content.ToString());

            return response;
        }
    }
}