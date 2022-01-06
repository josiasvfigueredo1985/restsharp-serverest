using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using DesafioAutomacaoAPIBase2.Helpers;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    class PostProduto : RequestBase
    {
        public PostProduto()
        {
            requestService = "produtos";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }
        public void SetJsonBody(string nome, int preco, string descricao, int quantidade)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath()+"Jsons/Produto.json");
            jsonBody = jsonBody.Replace("$nome", nome);
            jsonBody = jsonBody.Replace("$preco", preco.ToString());
            jsonBody = jsonBody.Replace("$descricao", descricao);
            jsonBody = jsonBody.Replace("$quantidade", quantidade.ToString());
            //          {
            //              "nome": "$nome",
            //"preco": "$preco",
            //"descricao": "$descricao",
            //"quantidade": "$quantidade"
            //          }
        }
    }
}
