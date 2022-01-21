using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Steps
{
    public class LoginStep
    {
        // Execução em hardcode para evitar erros de objetos não instanciados pela arquitetura
        public static string RetornaBearerToken()
        {
            //Executa delete para verificar a validade do token
            if (DeletarProduto().IsSuccessful)
            {
                // Retornar o token
                string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
                return token;
            }
            // Se login falhar ...
            else if (RealizarLogin().IsSuccessful)
            {
                // Retornar o token
                string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
                return token;
            }
            else if (CriarUsuario().IsSuccessful)
            {
                // Realizar login novamente e retornar token
                RealizarLogin();
                string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
                return token;
            }
            else
            {
                // Deletar usuário
                DeletarUsuario();

                //Criar usuário novamente
                CriarUsuario();

                // Realizar login novamente e retornar token
                RealizarLogin();
                string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
                return token;
            }
        }

        private static IRestResponse RealizarLogin()
        {
            string email = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER");
            string pwd = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD");
            string url = JsonBuilder.ReturnParameterAppSettings("URL");
            var client = new RestClient($"{url}login");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                email = email,
                password = pwd
            });

            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content.ToString());

            if (response.IsSuccessful)
            {
                dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());
                JsonBuilder.UpdateParameterAppSettings("TOKEN", jsonData.authorization.Value);
                return response;
            }
            else
            {
                return response;
            }
        }

        private static IRestResponse CriarUsuario()
        {
            string email = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER");
            string pwd = JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD");
            string nome = JsonBuilder.ReturnParameterAppSettings("NAME");
            string adm = JsonBuilder.ReturnParameterAppSettings("ADMINISTRATOR").ToLower();
            string url = JsonBuilder.ReturnParameterAppSettings("URL");

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
            //   Console.WriteLine(response.Content.ToString());

            if (response.IsSuccessful)
            {
                dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());
                string idUsuario = jsonData._id.Value;
                JsonBuilder.UpdateParameterAppSettings("USER_ID", idUsuario);
                return response;
            }
            else
            {
                return response;
            }
        }

        public static IRestResponse DeletarUsuario()
        {
            string idusuario = JsonBuilder.ReturnParameterAppSettings("USER_ID");
            string url = JsonBuilder.ReturnParameterAppSettings("URL");

            var client = new RestClient($"{url}usuarios/{idusuario}");
            var request = new RestRequest(Method.DELETE);

            IRestResponse response = client.Execute(request);
            //   Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse DeletarProduto()
        {
            string token = JsonBuilder.ReturnParameterAppSettings("TOKEN");
            string url = JsonBuilder.ReturnParameterAppSettings("URL");
            var client = new RestClient($"{url}produtos/idInexistente");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", token);
            IRestResponse response = client.Execute(request);
            //  Console.WriteLine(response.Content.ToString());

            return response;
        }
    }
}