using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Newtonsoft;
using DesafioAutomacaoAPIBase2.Requests.Usuarios;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json.Linq;
using ClosedXML.Excel;
using System.Linq;
using DesafioAutomacaoAPIBase2.DBSteps;
using System.Data;
using Newtonsoft.Json;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class UsuarioStep
    {
        public static List<IRestResponse> CriarUsuariosPlanilhaExcel()
        {
            List<IRestResponse> responses = new List<IRestResponse>();

            List<string> statusCodes = new List<string>();
            var xls = new XLWorkbook(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "Usuarios");
            var totalLinhas = planilha.Rows().Count();

            PostUsuario usuario = new PostUsuario();
            IRestResponse response;

            //A primeira linha é o cabecalho, o contador das linhas dos itens se inicia na linha 1
            try
            {
                for (int l = 2; l <= totalLinhas; l++)
                {
                    var nome = planilha.Cell($"A{l}").Value.ToString();
                    var email = planilha.Cell($"B{l}").Value.ToString();
                    var password = planilha.Cell($"C{l}").Value.ToString();
                    var administrador = planilha.Cell($"D{l}").Value.ToString().ToLower();

                    usuario.SetJsonBody(nome, email, password, Convert.ToBoolean(administrador));
                    response = usuario.ExecuteRequest();

                    dynamic jsonData = JObject.Parse(response.Content);
                    string idUsuario = jsonData._id;

                    // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
                    SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador, idUsuario);


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

        public static List<IRestResponse> DeletarUsuariosCriados()
        {

            var dadosUsuario = SolicitacaoDBSteps.BuscarUsuariosCriados();
            List<IRestResponse> responses = new List<IRestResponse>();

            for (int i = 4; i <= dadosUsuario.Count;)
            {

                string id = dadosUsuario[i];
                DeleteUsuario delete = new DeleteUsuario(id);
                IRestResponse response = delete.ExecuteRequest();
                responses.Add(response);
                i = i + 5;
            }
            //Deleta os dados do banco de dados
            SolicitacaoDBSteps.DeletarTodosUsuariosCriados();

            return responses;
        }

        public static IRestResponse DeletarUsuarioPorId(string id)
        {
            DeleteUsuario delete = new DeleteUsuario(id);
            IRestResponse response = delete.ExecuteRequest();
            return response;
        }

        public static IRestResponse CriarUsuario()
        {
            string nome = "Pleonário Silvestre";
            string email = "pleo_1982@hotmail.com";
            string password = "w1nn3r_1982";
            bool administrador = true;

            PostUsuario post = new PostUsuario();
            post.SetJsonBody(nome,email,password,administrador);
            IRestResponse response = post.ExecuteRequest();

            dynamic jsonData = JObject.Parse(response.Content);
            string idUsuario = jsonData._id;

            // Inserção dos dados do usuário criado em cada response na tabela "usuarios" no banco de dados
            SolicitacaoDBSteps.InserirIdUsuarioCriadoDB(nome, email, password, administrador.ToString().ToLower(), idUsuario);
            return response;
        }
    }
}
