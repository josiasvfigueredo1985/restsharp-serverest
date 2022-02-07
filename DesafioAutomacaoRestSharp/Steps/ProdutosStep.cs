using ClosedXML.Excel;
using DesafioAutomacaoRestSharp.DBSteps;
using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Requests.Produtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioAutomacaoRestSharp.Steps
{
    public class ProdutosStep
    {
        public static List<string> CriarProdutosPlanilhaExcel()
        {
            var rnd = TestContext.CurrentContext.Random;

            List<string> statusCodes = new List<string>();
            var xls = new XLWorkbook(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "Produtos");
            var totalLinhas = planilha.Rows().Count();

            PostProduto produto = new PostProduto();
            IRestResponse<dynamic> response;

            //A primeira linha é o cabecalho, o contador das linhas dos itens se inicia na linha 1
            try
            {
                for (int l = 2; l <= totalLinhas; l++)
                {
                    //Adição de´caracter numérico randômico devido à falta de controle de dados da API
                    //Ex. Os dados podem ser apagados ou dupicados por outros usuários da API
                    var nome = planilha.Cell($"A{l}").Value.ToString() + " modelo " + rnd.GetString(6);
                    var preco = planilha.Cell($"B{l}").Value.ToString();
                    var descricao = planilha.Cell($"C{l}").Value.ToString();
                    var quantidade = planilha.Cell($"D{l}").Value.ToString();

                    produto.SetJsonBody(nome, int.Parse(preco), descricao, int.Parse(quantidade));
                    response = produto.ExecuteRequest();

                    // Inserção do ID gerado em cada response na tabela "produto" no banco de dados
                    SolicitacaoDBSteps.InserirProdutoCriadoDB(Convert.ToString(response.Data._id));
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

            var result = statusCodes;
            return result;
        }

        public static void DeletarTodosProdutosCriados()
        {
            DeleteProdutos();

            //Deletar os dados do banco
            SolicitacaoDBSteps.DeletarTodosIdsProdutos();
            Thread.Sleep(1000);
        }

        public static IRestResponse<dynamic> DeletarProdutoById(string idProduto)
        {
            DeleteProduto delete = new DeleteProduto(idProduto);
            var response = delete.ExecuteRequest();

            return response;
        }
        public static IRestResponse<dynamic> CriarProduto()
        {
            Thread.Sleep(500);
            PostProduto postProduto = new PostProduto();

            var rnd = TestContext.CurrentContext.Random;
            string nome = "Notebook Asus Ryzen 5 " + rnd.GetString(6);
            int preco = 2300;
            string descricao = "Notebook Gamer " + rnd.GetString(6);
            int qtde = 1;

            //Criar um produto
            postProduto.SetJsonBody(nome, preco, descricao, qtde);
            IRestResponse<dynamic> response = postProduto.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            //Insere o id do produto cadastrado no banco para ser deletado por outros testes
            SolicitacaoDBSteps.InserirProdutoCriadoDB(Convert.ToString(response.Data._id));

            Thread.Sleep(1000);

            return response;
        }

        public static void DeleteProdutos()
        {
            List<string> ids = new List<string>();
            DeleteProduto delete;
            GetProdutos get = new GetProdutos();
            IRestResponse<dynamic> response = get.ExecuteRequest();

            var data = response.Data.quantidade.Value;
            for (int i = 0; i < data; i++)
            {
                string id = response.Data.produtos[i]._id.Value;
                ids.Add(id);
            }
            foreach (var item in ids)
            {
                delete = new DeleteProduto(item);
                var rest = delete.ExecuteRequest();
                Console.WriteLine("Produto excluído: " + rest.Content.ToString());
            }
            Thread.Sleep(1000);
        }

        public static IRestResponse<dynamic> CriarProdutoUnico(string nomeAdicional)
        {
            PostProduto postProduto = new PostProduto();

            string nome = "Notebook Dell Basic " + nomeAdicional;
            string desc = "Notebook Dell Intel Core i3";
            int preco = 2300;
            int qtde = 1;

            //Criar um produto
            postProduto.SetJsonBody(nome, preco, desc, qtde);
            IRestResponse<dynamic> response = postProduto.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            //Insere o id do produto cadastrado no banco para ser deletado por outros testes
            SolicitacaoDBSteps.InserirProdutoCriadoDB(response.Data._id.Value);

            return response;
        }

        public static IRestResponse<dynamic> AtualizarProduto()
        {
            var rnd = TestContext.CurrentContext.Random;
            string nome = "Notebook Asus Ryzen 5 Atualizado - Versão " + rnd.GetString(6);
            int preco = 2200;
            string descricao = "Notebook Gamer Atualizado - Versão " + rnd.GetString(6);
            int qtde = 1;

            PutProduto put = new PutProduto(CriarProduto().Data._id);
            put.SetJsonBody(nome, preco, descricao, qtde);
            IRestResponse<dynamic> response = put.ExecuteRequest();

            return response;
        }
    }
}