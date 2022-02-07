using ClosedXML.Excel;
using DesafioAutomacaoRestSharp.DBSteps;
using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Requests.Usuarios;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DesafioAutomacaoRestSharp.Steps
{
    public class UsuarioStep
    {
        public static List<IRestResponse> CriarUsuariosPlanilhaExcel()
        {
            List<IRestResponse> responses = new List<IRestResponse>();

            List<string> statusCodes = new List<string>();
            var xls = new XLWorkbook(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "Usuarios");
            var totalLinhas = planilha.Rows().Count();
            var totalColunas = planilha.ColumnsUsed().Count();
            PostUsuario usuario = new PostUsuario();
            IRestResponse<dynamic> response;

            //A primeira linha é o cabecalho, o contador das linhas dos itens se inicia na linha 1
            try
            {
                for (int l = 2; l <= totalLinhas; l++)
                {
                    string[] colunas = { "A", "B", "C", "D", "E", "F" };
                    int i = 0;
                    var nome = planilha.Cell($"{colunas[i++]}{l}").Value.ToString();
                    var email = planilha.Cell($"{colunas[i++]}{l}").Value.ToString();
                    var password = planilha.Cell($"{colunas[i++]}{l}").Value.ToString();
                    var administrador = planilha.Cell($"{colunas[i++]}{l}").Value.ToString().ToLower();

                    usuario.SetJsonBody(nome, email, password, Convert.ToBoolean(administrador));
                    response = usuario.ExecuteRequest();

                    // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
                    SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador, Convert.ToString(response.Data._id));

                    // Lista para armazenar cada status code gerado pela requisição
                    responses.Add(response);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro na execução das requisições: {0}", erro);
                throw;
            }
            var result = statusCodes;
            return responses;
        }

        public static void DeletarUsuariosCriados()
        {
            //Deleta os ids salvos do banco de dados
            SolicitacaoDBSteps.DeletarTodosUsuariosCriados();

            Thread.Sleep(1000);

            //Deletar todos os usuários criados
            DeletarTodosUsuarios();

            Thread.Sleep(500);
        }

        public static IRestResponse<dynamic> DeletarUsuarioPorId(string id)
        {
            DeleteUsuario delete = new DeleteUsuario(id);
            IRestResponse<dynamic> response = delete.ExecuteRequest();

            // Deletar usuário do banco de dados
            SolicitacaoDBSteps.DeletarUsuarioById(id);

            return response;
        }

        public static void DeletarUsuarioPorIdBancoDados(string id)
        {
            // Deletar usuário do banco de dados
            SolicitacaoDBSteps.DeletarUsuarioById(id);
        }

        public static IRestResponse<dynamic> CriarUsuario()
        {
            // Exemplo de uso de criação de emails dinâmicos
            Random r = new Random();
            string[] emails = { "yahoo", "bol", "ig", "globo", "gov", "gmail", "ambev", "abril", "msn", "outlook", "hotmail" };

            string nome = "Pleonário Silvestre";
            string email = $"pleo_{r.Next(9999)}@{emails[r.Next(10)]}.com";
            string password = $"w1nn3r_1982";
            bool administrador = true;

            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome, email, password, administrador);
            IRestResponse<dynamic> response = post.ExecuteRequest();

            // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
            SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador.ToString().ToLower(), Convert.ToString(response.Data._id.Value));

            return response;
        }

        public static void DeletarTodosUsuarios()
        {
            CarrinhosStep.DeletarCarrinhoCancelarCompra();
            CarrinhosStep.DeletarCarrinhoConcluirCompra();

            GetUsuarios get = new GetUsuarios();
            IRestResponse<dynamic> response = get.ExecuteRequest();
            int data = response.Data.quantidade;

            for (int i = 0; i < data; i++)
            {
                DeleteUsuario del = new DeleteUsuario(response.Data.usuarios[i]._id.Value);
                var response2 = del.ExecuteRequest();

                if (!response2.IsSuccessful)
                {
                    i++;
                }
                Console.WriteLine(response2.Data);
            }
        }

        public static string RetornaEmailUsuario(string idUsuario)
        {
            GetUsuarioPorId get = new GetUsuarioPorId(idUsuario);
            IRestResponse<dynamic> response = get.ExecuteRequest();

            return response.Data.email;
        }
    }
}