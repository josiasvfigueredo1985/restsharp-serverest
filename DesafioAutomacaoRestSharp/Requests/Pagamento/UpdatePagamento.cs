using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;
using System;
using System.IO;

namespace DesafioAutomacaoRestSharp.Requests.Pagamento
{
    public class UpdatePagamento : RequestBase
    {
        public UpdatePagamento(string id)
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL_MOCK");
            requestService = $"pagamento/tipoPagamento/{id}";
            method = Method.PUT;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBodyBoleto(string dataPagamento, bool boleto,
          int precoTotal, int quantidadeTotal, string idUsuario, string _id)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Pagamento.json");
            jsonBody = jsonBody.Replace("$dataPagamento", dataPagamento);
            jsonBody = jsonBody.Replace("$pix", "false");
            jsonBody = jsonBody.Replace("$boleto", boleto.ToString().ToLower());
            jsonBody = jsonBody.Replace("$credito_status", "false");
            jsonBody = jsonBody.Replace("$qteParcelas", "0");
            jsonBody = jsonBody.Replace("$juros", "false");
            jsonBody = jsonBody.Replace("$precoTotal", precoTotal.ToString());
            jsonBody = jsonBody.Replace("$quantidadeTotal", quantidadeTotal.ToString());
            jsonBody = jsonBody.Replace("$idUsuario", idUsuario);
            jsonBody = jsonBody.Replace("$_id", _id);
        }

        public void SetJsonBodyPix(string dataPagamento, bool pix,
            int precoTotal, int quantidadeTotal, string idUsuario, string _id)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Pagamento.json");
            jsonBody = jsonBody.Replace("$dataPagamento", dataPagamento);
            jsonBody = jsonBody.Replace("$pix", pix.ToString().ToLower());
            jsonBody = jsonBody.Replace("$boleto", "false");
            jsonBody = jsonBody.Replace("$credito_status", "false");
            jsonBody = jsonBody.Replace("$qteParcelas", "0");
            jsonBody = jsonBody.Replace("$juros", "false");
            jsonBody = jsonBody.Replace("$precoTotal", precoTotal.ToString());
            jsonBody = jsonBody.Replace("$quantidadeTotal", quantidadeTotal.ToString());
            jsonBody = jsonBody.Replace("$idUsuario", idUsuario);
            jsonBody = jsonBody.Replace("$_id", _id);
        }

        public void SetJsonBodyCartao(string dataPagamento, bool credito_status,
       int qteParcelas, bool juros, int precoTotal, int quantidadeTotal, string idUsuario, string _id)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Pagamento.json");
            jsonBody = jsonBody.Replace("$dataPagamento", dataPagamento);
            jsonBody = jsonBody.Replace("$pix", "false");
            jsonBody = jsonBody.Replace("$boleto", "false");
            jsonBody = jsonBody.Replace("$credito_status", credito_status.ToString().ToLower());
            jsonBody = jsonBody.Replace("$qteParcelas", qteParcelas.ToString());
            jsonBody = jsonBody.Replace("$juros", juros.ToString().ToLower());
            jsonBody = jsonBody.Replace("$precoTotal", precoTotal.ToString());
            jsonBody = jsonBody.Replace("$quantidadeTotal", quantidadeTotal.ToString());
            jsonBody = jsonBody.Replace("$idUsuario", idUsuario);
            jsonBody = jsonBody.Replace("$_id", _id);
        }
    }
}