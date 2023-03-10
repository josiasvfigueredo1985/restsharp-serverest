using System;
using System.Collections.Generic;
using RestSharp;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class PagamentoStep
    {
        public static IRestResponse CriarNovoPagamento(string pix_boleto_cartao)
        {
            // Dados trazidos do carrinho
            dynamic jsonData1 = JsonConvert.DeserializeObject(CarrinhosStep.ConsultarCarrinho().Content);
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool pix = true;
            int prec = Convert.ToInt32(jsonData1.precoTotal.Value);
            int qtd = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            int precoTotal = Convert.ToInt32(prec);
            int quantidadeTotal = Convert.ToInt32(qtd);
            string idUsuario = jsonData1.idUsuario.Value;
            string idCarrinho = jsonData1._id.Value;
            bool boleto = true;
            bool cartao = true;
            int parcelas = 2;
            bool juros = true;

            CreatePagamento create = new CreatePagamento();
            switch (pix_boleto_cartao)
            {
                case "pix":
                    create.SetJsonBodyPix(dataPagamento, pix, precoTotal,
               quantidadeTotal, idUsuario, idCarrinho);
                    break;
                case "boleto":
                    create.SetJsonBodyBoleto(dataPagamento, boleto, precoTotal, quantidadeTotal, idUsuario, idCarrinho);
                    break;
                case "cartao":
                    create.SetJsonBodyCartao(dataPagamento, cartao, parcelas, juros, precoTotal, quantidadeTotal, idUsuario, idCarrinho);
                    break;
            }
            IRestResponse response = create.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse ConsultarPagamentoPorId()
        {
            dynamic jsonData = JObject.Parse(CriarNovoPagamento("pix").Content.ToString());
            string idPagamento = jsonData._id;

            GetByIdPagamento get = new GetByIdPagamento(idPagamento);
            IRestResponse response = get.ExecuteRequest();

            return response;
        }

        public static IRestResponse DeletarPagamento(string idPagamento)
        {
            DeletePagamento delete = new DeletePagamento(idPagamento);

            IRestResponse response = delete.ExecuteRequest();

            return response;
        }

        public static void DeletarPagamentos()
        {
            // Espera adicionada por conta do rate limit da API
            //Thread.Sleep(1800);

            GetAllPagamento get = new GetAllPagamento();
            IRestResponse response = get.ExecuteRequest();

            string cont = Convert.ToString(response.Content);

            Console.WriteLine("Response Delete: " + cont);

            if (response.Content != "[]")
            {
                dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(cont);

                foreach (var id in jsonData[0].idPagamento.Value)
                {
                    //Thread.Sleep(1000);
                    DeletePagamento delete = new DeletePagamento(Convert.ToString(id));
                    IRestResponse resp = delete.ExecuteRequest();
                    Console.WriteLine(resp.Content.ToString());
                }
            }
        }
    }
}
