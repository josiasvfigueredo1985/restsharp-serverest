using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Newtonsoft;
using DesafioAutomacaoAPIBase2.Requests.Carrinhos;
using DesafioAutomacaoAPIBase2.Models;
using DesafioAutomacaoAPIBase2.Requests.Produtos;
using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class CarrinhosStep
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

            return response;
        }

        public static IRestResponse CriarCarrinhoUnicoProduto()
        {
            PostCarrinho carrinho = new PostCarrinho();
            //Pegar o id gerado
            dynamic jsonData = JObject.Parse(ProdutosStep.CriarProduto().Content);
            string idProduto = jsonData._id;

            //Criar o novo carrinho com o id do produto
            carrinho.SetJsonBody(idProduto, 1);
            IRestResponse response = carrinho.ExecuteRequest();

            return response;
        }

        public static IRestResponse CriarCarrinhoUnicoProduto(string idProduto)
        {
            //Criar o novo carrinho com o id do produto informado
            PostCarrinho carrinho = new PostCarrinho();
            carrinho.SetJsonBody(idProduto, 1);
            IRestResponse response = carrinho.ExecuteRequest();

            return response;
        }

        public static IRestResponse DeletarCarrinhoConcluirCompra()
        {
            DeleteConcluirCompra delCompra = new DeleteConcluirCompra();
            IRestResponse response = delCompra.ExecuteRequest();
            Console.WriteLine("Carrinho deletado - Concluir compra: {0}",response.Content);
            return response;
        }

        public static IRestResponse DeletarCarrinhoCancelarCompra()
        {
            DeleteCancelarCompra delCancel = new DeleteCancelarCompra();
            IRestResponse response = delCancel.ExecuteRequest();
            Console.WriteLine("Carrinho deletado - Cancelar compra: {0}", response.Content);
            return response;
        }

        public static IRestResponse ConsultarCarrinho()
        {
            dynamic jsonData = JObject.Parse(CriarCarrinhoUnicoProduto().Content);
            GetCarrinhoPorId get = new GetCarrinhoPorId(jsonData._id);
            IRestResponse response = get.ExecuteRequest();

            return response;
        }
    }
}
