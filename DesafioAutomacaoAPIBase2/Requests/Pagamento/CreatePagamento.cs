using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Pagamento
{
    class CreatePagamento : RequestBase
    {
        public CreatePagamento()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL_MOCK");
            requestService = "pagamento/tipoPagamento";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }
        public void SetJsonBody(string dataPagamento, bool pix, bool boleto, bool credito_status,
            int qteParcelas, bool juros, int precoTotal, int quantidadeTotal, string idUsuario, string _id)
        {
            // Simulação da regra do backend
            if (credito_status)
            {
                juros = true;
                pix = false;
                boleto = false;
            }
            else if (pix)
            {
                credito_status = false;
                juros = false;
                boleto = false;
            }
            else if (boleto)
            {
                credito_status = false;
                juros = false;
                pix = false;
            }
            else
            {
                Console.WriteLine("Tipo de pagamento informado inexistente");
            }

            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Pagamento.json");
            jsonBody = jsonBody.Replace("$dataPagamento", dataPagamento);
            jsonBody = jsonBody.Replace("$pix", pix.ToString().ToLower());
            jsonBody = jsonBody.Replace("$boleto", boleto.ToString().ToLower());
            jsonBody = jsonBody.Replace("$credito_status", credito_status.ToString().ToLower());
            jsonBody = jsonBody.Replace("$qteParcelas", qteParcelas.ToString());
            jsonBody = jsonBody.Replace("$juros", juros.ToString().ToLower());
            jsonBody = jsonBody.Replace("$precoTotal", precoTotal.ToString());
            jsonBody = jsonBody.Replace("$quantidadeTotal", quantidadeTotal.ToString());
            jsonBody = jsonBody.Replace("$idUsuario", idUsuario);
            jsonBody = jsonBody.Replace("$_id", _id);
            /*
"dataPagamento" "$dataPagamento"
"pix" "$pix"
"boleto" "$boleto"
"credito_status" "$credito_status"
"qteParcelas" "$qteParcelas"
"juros" "$juros"
"precoTotal" "$precoTotal"
"quantidadeTotal" "$quantidadeTotal"
"idUsuario" "$idUsuario"
"_id": "$_id"
             */
        }
    }
}
