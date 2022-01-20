using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;
using System.IO;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    public class PostCarrinho : RequestBase
    {
        public PostCarrinho()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "carrinhos";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string idProduto, int quantidade)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Carrinho.json");
            jsonBody = jsonBody.Replace("$idProduto", idProduto);
            jsonBody = jsonBody.Replace("$quantidade", quantidade.ToString());
            //            {
            //                "produtos": [
            //                  {
            //                    "idProduto": "idProduto",
            //      "quantidade": "quantidade"
            //                  }
            //  ]
            //}
        }

        public void SetJsonBody(string jsonPath)
        {
            jsonBody = File.ReadAllText(jsonPath);
        }
    }
}