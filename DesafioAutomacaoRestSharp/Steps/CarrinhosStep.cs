using DesafioAutomacaoRestSharp.DBSteps;
using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Requests.Carrinhos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioAutomacaoRestSharp.Steps
{
    // As Thread.Sleep adicionadas foram necessárias para executar os testes em paralelo, devido ao uso simultâneo de recursos do appsettings.json
    public class CarrinhosStep
    {
        public static IRestResponse<dynamic> CriarCarrinhoComProdutosDataSet()
        {
            string jsonPath = GeneralHelpers.ReturnProjectPath() + "Jsons/CarrinhoDataSet.json";

            //Busca os IDs dos produtos inseridos no banco de dados
            List<string> idProdutos = SolicitacaoDBSteps.BuscarIdsProdutos();

            // Cria um json inserindo todos os produtos já cadastrados
            CarrinhoDataSet.CriarJsonDatSet(idProdutos, 1);

            //Executa a requisição passando somente o json já criado
            PostCarrinho post = new PostCarrinho();
            post.SetJsonBody(jsonPath);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse<dynamic> CriarCarrinhoUnicoProduto()
        {
            DeletarCarrinhoCancelarCompra();

            //Pegar o id gerado
            string idProduto = ProdutosStep.CriarProduto().Data._id;

            //Criar o novo carrinho com o id do produto
            PostCarrinho carrinho = new PostCarrinho();
            carrinho.SetJsonBody(idProduto, 1);

            IRestResponse<dynamic> response = carrinho.ExecuteRequest();

            Console.WriteLine("Carrinho criado: " + response.Content.ToString());

            return response;
        }

        public static IRestResponse<dynamic> CriarCarrinhoUnicoProduto(string idProduto)
        {
            //Criar o novo carrinho com o id do produto informado
            PostCarrinho carrinho = new PostCarrinho();
            carrinho.SetJsonBody(idProduto, 1);
            IRestResponse<dynamic> response = carrinho.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse<dynamic> DeletarCarrinhoConcluirCompra()
        {
            DeleteConcluirCompra delCompra = new DeleteConcluirCompra();
            IRestResponse<dynamic> response = delCompra.ExecuteRequest();

            // Console.WriteLine("Carrinho deletado - Concluir compra: {0}", response.Content.ToString());
            Thread.Sleep(1000);
            return response;
        }

        public static IRestResponse<dynamic> DeletarCarrinhoCancelarCompra()
        {
            Thread.Sleep(1000);
            DeleteCancelarCompra delCancel = new DeleteCancelarCompra();
            IRestResponse<dynamic> response = delCancel.ExecuteRequest();

            // Console.WriteLine("Carrinho deletado - Cancelar compra: {0}", response.Content.ToString());

            return response;
        }

        public static IRestResponse<dynamic> ConsultarCarrinho()
        {
            // Exemplo da otimização de código utilizando o atributo Data do objeto dinâmico tratado no método Executerequest (RestSharpHelpers)
            //dynamic jsonData = JsonConvert.DeserializeObject(CriarCarrinhoUnicoProduto().Content);
            //string id = jsonData._id.Value;
            GetCarrinhoPorId get = new GetCarrinhoPorId(CriarCarrinhoUnicoProduto().Data.id);
            IRestResponse<dynamic> response = get.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }
    }
}