using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Newtonsoft;
using DesafioAutomacaoAPIBase2.Requests.Login;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class LoginStep
    {
        // Execução em hardcode para evitar erros de objetos não instanciados pela arquitetura
        public static string RetornaBearerToken()
        {
            try
            {
                if (VerificaValidadeToken().IsSuccessful)
                {
                    return JsonBuilder.ReturnParameterAppSettings("TOKEN");
                }
                else
                {
                    RealizarLogin();
                    return JsonBuilder.ReturnParameterAppSettings("TOKEN");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no método: {MethodBase.GetCurrentMethod().Name}\nDescrição do erro:\n{e}");
                throw;
            }
        }

        private static IRestResponse VerificaValidadeToken()
        {
            try
            {
                //Execução de um delete sem dados válidos, somente para validar a response
                var url = JsonBuilder.ReturnParameterAppSettings("URL");
                var client = new RestClient($"{url}produtos/0uxuPY0cbmQhpEz1");
                var request = new RestRequest(Method.DELETE);
                string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
                request.AddHeader("Authorization", token);
                IRestResponse response = client.Execute(request);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no método: {MethodBase.GetCurrentMethod().Name}\nDescrição do erro:\n{e}");
                throw;
            }
        }

        private static void RealizarLogin()
        {
            try
            {
                var url = JsonBuilder.ReturnParameterAppSettings("URL");
                string email = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER");
                string pwd = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD");

                var client = new RestClient($"{url}login");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new
                {
                    email = email,
                    password = pwd
                });

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    dynamic jsonData = JObject.Parse(response.Content);
                    JsonBuilder.UpdateParameterAppSettings("TOKEN", jsonData.authorization.Value);
                    RetornaBearerToken();
                }
                else
                {
                    CriarUsuario();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no método: {MethodBase.GetCurrentMethod().Name}\nDescrição do erro:\n{e}");
                throw;
            }
        }

        private static void CriarUsuario()
        {
            try
            {
                var url = JsonBuilder.ReturnParameterAppSettings("URL");
                string email = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER");
                string pwd = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD");
                string nome = JsonBuilder.ReturnParameterAppSettings("NAME");
                string adm = JsonBuilder.ReturnParameterAppSettings("ADMINISTRATOR").ToLower();

                var client = new RestClient($"{url}usuarios");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new
                {
                    nome = nome,
                    email = email,
                    password = pwd,
                    administrador = adm
                }); ;

                IRestResponse response = client.Execute(request);
                dynamic jsonData = JsonConvert.DeserializeObject(response.Content);
                JsonBuilder.UpdateParameterAppSettings("USER_ID", jsonData._id.Value);
                
                RealizarLogin();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro no método: {MethodBase.GetCurrentMethod().Name}\nDescrição do erro:\n{e}");
                throw;
            }
        }
    }
}
