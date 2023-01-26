using ClosedXML.Excel;
using DesafioAutomacaoAPIBase2.DBSteps;
using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Requests.Usuarios;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioAutomacaoAPIBase2.Steps
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
            IRestResponse response;

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

                    dynamic jsonData = JObject.Parse(response.Content.ToString());
                    string idUsuario = jsonData._id;

                    // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
                    //SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador, idUsuario);

                    // Lista para armazenar cada status code gerado pela requisição
                    responses.Add(response);
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro na execução das requisições: {0}", erro);
                throw;
            }
            var result = statusCodes;//.ToArray();
            return responses;
        }

        public static void DeletarUsuariosCriados()
        {
            //Deleta os ids salvos do banco de dados
            //SolicitacaoDBSteps.DeletarTodosUsuariosCriados();

            //Deletar todos os usuários criados
            DeletarTodosUsuarios();
            //   return responses;
        }

        public static IRestResponse DeletarUsuarioPorId(string id)
        {
            DeleteUsuario delete = new DeleteUsuario(id);
            IRestResponse response = delete.ExecuteRequest();
            // Deletar usuário do banco de dados
            //SolicitacaoDBSteps.DeletarUsuarioById(id);
            return response;
        }

        public static void DeletarUsuarioPorIdBancoDados(string id)
        {
            // Deletar usuário do banco de dados
            //SolicitacaoDBSteps.DeletarUsuarioById(id);
        }

        public static IRestResponse CriarUsuario()
        {
            Random r = new Random();
            string[] emails = { "yahoo", "bol", "ig", "globo", "gov", "gmail", "ambev", "abril", "msn", "outlook", "hotmail" };

            string nome = "Pleonário Silvestre";
            string email = $"pleo_{r.Next(9999)}@{emails[r.Next(10)]}.com";
            string password = $"w1nn3r_1982";
            bool administrador = true;

            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome, email, password, administrador);
            IRestResponse response = post.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());
            string idUsuario = jsonData._id.Value;

            // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
            //SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador.ToString().ToLower(), idUsuario);

            return response;
        }

        public static void DeletarTodosUsuarios()
        {

            GetUsuarios get = new GetUsuarios();
            IRestResponse response = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            int data = Convert.ToInt32(jsonData.quantidade.Value);

            for (int i = 0; i < data; i++)
            {
                string id = jsonData.usuarios[i]._id.Value;

                DeleteUsuario del = new DeleteUsuario(id);
                response = del.ExecuteRequest();
                Console.WriteLine(response.Content.ToString());
            }
        }

        public static string RetornaEmailUsuario(string idUsuario)
        {
            GetUsuarioPorId get = new GetUsuarioPorId(idUsuario);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            string email = jsonData.email.Value;
            return email;
        }
    }
}