using ClosedXML.Excel;
using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Requests.Produtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioAutomacaoAPIBase2.Steps
{
    public class ProdutosStep
    {
        public static List<string> CriarProdutosPlanilhaExcel()
        {
            List<string> statusCodes = new List<string>();
            var xls = new XLWorkbook(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "Produtos");
            var totalLinhas = planilha.Rows().Count();

            PostProduto produto = new PostProduto();
            IRestResponse response;

            //A primeira linha é o cabecalho, o contador das linhas dos itens se inicia na linha 1
            try
            {
                for (int l = 2; l <= totalLinhas; l++)
                {
                    //Adição de´caracter numérico randômico devido à falta de controle de dados da API
                    //Ex. Os dados podem ser apagados ou dupicados por outros usuários da API
                    var nome = planilha.Cell($"A{l}").Value.ToString() + " modelo " + GeneralHelpers.ReturnRandomNumbersAsString(99, 9999);
                    var preco = planilha.Cell($"B{l}").Value.ToString();
                    var descricao = planilha.Cell($"C{l}").Value.ToString();
                    var quantidade = planilha.Cell($"D{l}").Value.ToString();

                    //   string prd = $"{nome} + {preco} + {descricao} + {quantidade}";

                    produto.SetJsonBody(nome, int.Parse(preco), descricao, int.Parse(quantidade));
                    response = produto.ExecuteRequest();

                    dynamic jsonData = JObject.Parse(response.Content.ToString());
                    string idProduto = jsonData._id;

                    // Inserção do ID gerado em cada response na tabela "produto" no banco de dados
                    //SolicitacaoDBSteps.InserirProdutoCriadoDB(idProduto);
                    string sts = response.StatusCode.ToString();
                    Console.WriteLine("Response Produtos criados: " + response.Content.ToString());
                    // Lista para armazenar cada status code gerado pela requisição
                    statusCodes.Add(sts);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro na execução das requisições: {0}", erro);
                throw;
            }

            var result = statusCodes;//.ToArray();
            return result;
        }

        public static void DeletarTodosProdutosCriados()
        {
            // Deleção dos produtos através dos Id´s que foram armazenados no banco de dados
            //Buscar e armazenar todos os ids dos produtos gerados
            //List<string> idsProdutos = SolicitacaoDBSteps.BuscarIdsProdutos();
            //List<string> statusCode = new List<string>();

            ////Deletar por cada id criado
            //foreach (var id in idsProdutos)
            //{
            //    DeleteProduto delete = new DeleteProduto(id);
            //    var response = delete.ExecuteRequest();
            //    statusCode.Add(response.StatusCode.ToString());
            //}
            // Deletar por cada id criado no serverest
            DeleteProdutos();

            //Deletar os dados do banco
            //SolicitacaoDBSteps.DeletarTodosIdsProdutos();
        }

        public static IRestResponse DeletarProdutoById(string idProduto)
        {
            DeleteProduto delete = new DeleteProduto(idProduto);
            var response = delete.ExecuteRequest();
            return response;
        }

        public static IRestResponse CriarProduto()
        {
            PostProduto postProduto = new PostProduto();

            Random model = new Random();
            string nome = "Notebook Asus Ryzen 5 " + model.Next().ToString();
            int preco = 2300;
            string descricao = "Notebook Gamer " + model.Next().ToString();
            int qtde = 1;

            //Criar um produto
            postProduto.SetJsonBody(nome, preco, descricao, qtde);
            IRestResponse response = postProduto.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            //Insere o id do produto cadastrado no banco para ser deletado por outros testes
            //SolicitacaoDBSteps.InserirProdutoCriadoDB(jsonData._id.Value);

            return response;
        }

        public static void DeleteProdutos()
        {
            List<string> ids = new List<string>();
            DeleteProduto delete;
            GetProdutos get = new GetProdutos();
            IRestResponse response = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            var data = jsonData.quantidade.Value;
            for (int i = 0; i < data; i++)
            {
                string id = jsonData.produtos[i]._id.Value;
                ids.Add(id);
            }
            foreach (var item in ids)
            {
                delete = new DeleteProduto(item);
                var rest = delete.ExecuteRequest();
                Console.WriteLine("Produto excluído: " + rest.Content.ToString());
            }
        }

        public static IRestResponse CriarProdutoUnico(string nomeAdicional)
        {
            PostProduto postProduto = new PostProduto();

            string nome = "Notebook Dell Basic " + nomeAdicional;
            string desc = "Notebook Dell Intel Core i3";
            int preco = 2300;
            int qtde = 1;

            //Criar um produto
            postProduto.SetJsonBody(nome, preco, desc, qtde);
            IRestResponse response = postProduto.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            //Insere o id do produto cadastrado no banco para ser deletado por outros testes
            //SolicitacaoDBSteps.InserirProdutoCriadoDB(jsonData._id.Value);

            return response;
        }

        public static IRestResponse AtualizarProduto()
        {
            Random model = new Random();
            string nome = "Notebook Asus Ryzen 5 Atualizado - Versão " + model.Next().ToString();
            int preco = 2200;
            string descricao = "Notebook Gamer Atualizado - Versão " + model.Next().ToString();
            int qtde = 1;

            dynamic jsonData = JObject.Parse(CriarProduto().Content.ToString());

            PutProduto put = new PutProduto(jsonData._id);
            put.SetJsonBody(nome, preco, descricao, qtde);
            IRestResponse response = put.ExecuteRequest();

            return response;
        }
    }
}