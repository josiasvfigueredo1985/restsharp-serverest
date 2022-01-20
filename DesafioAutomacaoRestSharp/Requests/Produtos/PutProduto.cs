using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;
using System.IO;

namespace DesafioAutomacaoAPIBase2.Requests.Produtos
{
    public class PutProduto : RequestBase
    {
        public PutProduto(string idProduto)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = $"produtos/{idProduto}";
            method = Method.PUT;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string nome, int preco, string descricao, int quantidade)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Produto.json");
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