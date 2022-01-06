using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using DesafioAutomacaoAPIBase2.Helpers;
using System.IO;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    class PostCarrinho : RequestBase
    {
        public PostCarrinho()
        {
            requestService = "carrinhos";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string idProduto, int quantidade)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath()+"Jsons/Carrinho.json");
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
