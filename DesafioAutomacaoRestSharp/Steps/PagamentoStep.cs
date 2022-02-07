using System;
using System.Collections.Generic;
using RestSharp;
using DesafioAutomacaoRestSharp.Requests.Pagamento;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;

namespace DesafioAutomacaoRestSharp.Steps
{
    class PagamentoStep
    {
        public static IRestResponse<dynamic> CriarNovoPagamento(string pix_boleto_cartao)
        {
            // Dados trazidos do carrinho
            dynamic jsonData1 = CarrinhosStep.ConsultarCarrinho().Data;
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool pix = true;
            int prec = Convert.ToInt32(jsonData1.precoTotal);
            int qtd = Convert.ToInt32(jsonData1.quantidadeTotal);
            int precoTotal = Convert.ToInt32(prec);
            int quantidadeTotal = Convert.ToInt32(qtd);
            string idUsuario = jsonData1.idUsuario;
            string idCarrinho = jsonData1._id;
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
            IRestResponse<dynamic> response = create.ExecuteRequest();

            // Console.WriteLine(response.Content.ToString());

            return response;
        }

        public static IRestResponse<dynamic> ConsultarPagamentoPorId()
        {
            GetByIdPagamento get = new GetByIdPagamento(CriarNovoPagamento("pix").Data._id);
            IRestResponse<dynamic> response = get.ExecuteRequest();

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
            Thread.Sleep(1500);

            GetAllPagamento get = new GetAllPagamento();
            IRestResponse<dynamic> response = get.ExecuteRequest();

            Console.WriteLine("Response Delete: " + response.Data);

            if (response.Content != "[]")
            {
                foreach (var id in response.Data[0].idPagamento.Value)
                {
                    Thread.Sleep(1000);
                    DeletePagamento delete = new DeletePagamento(Convert.ToString(id));
                    IRestResponse resp = delete.ExecuteRequest();
                    Console.WriteLine(resp.Content);
                }
            }
        }
    }
}
