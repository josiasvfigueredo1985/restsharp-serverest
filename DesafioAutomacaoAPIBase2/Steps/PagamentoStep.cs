using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Newtonsoft;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json.Linq;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class PagamentoStep
    {
        public IRestResponse CriarNovoPagamento()
        {
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool pix = true;
            bool boleto = false;
            bool cartao = false;
            int parcelas = 0;
            bool juros = false;

            // Dados trazidos do carrinho
            dynamic jsonData1 = JObject.Parse(CarrinhosStep.ConsultarCarrinho().Content);
            int precoTotal = jsonData1.precoTotal;
            int quantidadeTotal = jsonData1.quantidadeTotal;
            string idUsuario = jsonData1.idUsuario;
            string idCarrinho = jsonData1._id;

            CreatePagamento create = new CreatePagamento();
            create.SetJsonBody(dataPagamento, pix, boleto, cartao, parcelas, juros, precoTotal,
                quantidadeTotal, idUsuario, idCarrinho);
            IRestResponse response = create.ExecuteRequest();
            return response;
        }

        public IRestResponse ConsultarPagamentoPorId()
        {
            dynamic jsonData = JObject.Parse(CriarNovoPagamento().Content);
            string idPagamento = jsonData._id;

            GetByIdPagamento get = new GetByIdPagamento(idPagamento);
            IRestResponse response = get.ExecuteRequest();

            return response;
        }

        public IRestResponse AtualizarPagamento()
        {
            dynamic jsonData = JObject.Parse(CriarNovoPagamento().Content);
            string idPagamento = jsonData._id;

            string dataPagamento = DateTime.Today.ToShortDateString();
            bool pix = true;
            bool boleto = false;
            bool cartao = false;
            int parcelas = 0;
            bool juros = false;

            // Dados trazidos do carrinho
            dynamic jsonData1 = JObject.Parse(CarrinhosStep.ConsultarCarrinho().Content);
            int precoTotal = jsonData1.precoTotal;
            int quantidadeTotal = jsonData1.quantidadeTotal;
            string idUsuario = jsonData1.idUsuario;
            string idCarrinho = jsonData1._id;

            UpdatePagamento update = new UpdatePagamento(idPagamento);
            update.SetJsonBody(dataPagamento, pix, boleto, cartao, parcelas, juros, precoTotal,
                quantidadeTotal, idUsuario, idCarrinho);
            IRestResponse response = update.ExecuteRequest();

            return response;
        }

        public IRestResponse DeletarPagamento()
        {
            dynamic jsonData = JObject.Parse(CriarNovoPagamento().Content);
            string idPagamento = jsonData._id;

            DeletePagamento delete = new DeletePagamento(idPagamento);

            IRestResponse response = delete.ExecuteRequest();

            return response;
        }
    }
}
