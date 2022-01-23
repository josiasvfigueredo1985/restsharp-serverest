using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Steps;
using RestSharp;
using DesafioAutomacaoAPIBase2.Helpers;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesafioAutomacaoAPIBase2.Tests
{
    class PagamentoTests : TestBase
    {

        #region Testes Positivos
        [Test]
        public void ListarPagamentosCadastrados()
        {
            string modelo = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Schemas/Pagamento.json");

            //Deletar pagamentos criados anteiormente
            PagamentoStep.DeletarPagamentos();
            // Criar um pagamento
            PagamentoStep.CriarNovoPagamento("pix");

            // Listar os pagamentos criados
            GetAllPagamento get = new GetAllPagamento();
            IRestResponse response = get.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            // Validação de contrato
            bool shema = GeneralHelpers.ValidaContrato(modelo, response.Content.ToString());

            Assert.IsTrue(((int)response.StatusCode) == 200);
            Assert.True(shema);
        }

        [Test]
        public void CadastrarPagamentoPix()
        {
            //Deletar pagamentos criados anteiormente
            PagamentoStep.DeletarPagamentos();

            string pag = "false";
            string status = "Created";
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool pix = true;

            // Dados trazidos do carrinho criado
            dynamic jsonData1 = JsonConvert.DeserializeObject(CarrinhosStep.ConsultarCarrinho().Content.ToString());
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;
            string idCarrinho = jsonData1._id.Value;

            CreatePagamento create = new CreatePagamento();
            create.SetJsonBodyPix(dataPagamento, pix, precoTotal, quantidadeTotal, idUsuario, idCarrinho);
            IRestResponse response = create.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 201);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pix.ToString().ToLower());
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void CadastrarPagamentoBoleto()
        {
            //Deletar pagamentos criados anteiormente
            PagamentoStep.DeletarPagamentos();

            string pag = "false";
            string status = "Created";
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool boleto = true;

            // Dados trazidos do carrinho criado
            dynamic jsonData1 = JsonConvert.DeserializeObject(CarrinhosStep.ConsultarCarrinho().Content.ToString());
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;
            string idCarrinho = jsonData1._id.Value;

            CreatePagamento create = new CreatePagamento();
            create.SetJsonBodyBoleto(dataPagamento, boleto, precoTotal, quantidadeTotal, idUsuario, idCarrinho);
            IRestResponse response = create.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 201);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == boleto.ToString().ToLower());
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void CadastrarPagamentoCartaoCredito()
        {
            //Deletar pagamentos criados anteiormente
            PagamentoStep.DeletarPagamentos();

            string pag = "true";
            string status = "Created";
            string dataPagamento = DateTime.Today.ToShortDateString();
            bool cartao = true;
            int parcelas = 2;
            bool juros = true;

            // Dados trazidos do carrinho criado
            dynamic jsonData1 = JsonConvert.DeserializeObject(CarrinhosStep.ConsultarCarrinho().Content.ToString());
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;
            string idCarrinho = jsonData1._id.Value;

            CreatePagamento create = new CreatePagamento();
            create.SetJsonBodyCartao(dataPagamento, cartao, parcelas, juros, precoTotal, quantidadeTotal, idUsuario, idCarrinho);
            IRestResponse response = create.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 201);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == cartao.ToString().ToLower());
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void BuscarPagamentoPorIDPix()
        {
            string status = "OK";
            string tipo = "pix";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por pix e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;

            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }


        [Test]
        public void BuscarPagamentoPorIDBoleto()
        {
            string status = "OK";
            string tipo = "boleto";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por boleto e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;

            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }


        [Test]
        public void BuscarPagamentoPorIDCartao()
        {
            string status = "OK";
            string tipo = "cartao";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por cartão de crédito e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;

            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse response = get.ExecuteRequest();

            dynamic jsonData = JsonConvert.DeserializeObject(response.Content.ToString());

            Console.WriteLine(response.Content.ToString());

            Assert.True(response.IsSuccessful);
            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.AreEqual(status, response.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void EditarPagamentoPixParaBoleto()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "pix";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Pix e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Boleto
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyBoleto(dataPagamento, true, precoTotal, quantidadeTotal, idUsuario, id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(responseGet.Content.ToString());


            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());
            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }

        [Test]
        public void EditarPagamentoPixParaCartao()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "pix";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Pix e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            int qteParcelas = 12;
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Cartão de crédito
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyCartao(dataPagamento, true,qteParcelas,true,precoTotal,quantidadeTotal,idUsuario,id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(responseGet.Content.ToString());

            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());

            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void EditarPagamentoBoletoParaPix()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "boleto";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Boleto e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Pix
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyPix(dataPagamento, true, precoTotal, quantidadeTotal, idUsuario, id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            dynamic jsonData = JObject.Parse(responseGet.Content.ToString());


            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());
            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }

        [Test]
        public void EditarPagamentoBoletoParaCartao()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "boleto";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Boleto e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            int qteParcelas = 12;
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Cartão de crédito
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyCartao(dataPagamento, true, qteParcelas, true, precoTotal, quantidadeTotal, idUsuario, id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(responseGet.Content.ToString());


            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());
            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag);
        }

        [Test]
        public void EditarPagamentoCartaoParaPix()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "cartao";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Boleto e pegar o ID
            string cont = PagamentoStep.CriarNovoPagamento(tipo).Content.ToString();
            dynamic jsonData1 = JsonConvert.DeserializeObject(cont);
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Pix
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyPix(dataPagamento, true, precoTotal, quantidadeTotal, idUsuario, id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            string contGet = responseGet.Content.ToString();
            dynamic jsonData = JsonConvert.DeserializeObject(contGet);


            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());
            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }
        [Test]
        public void EditarPagamentoCartaoParaBoleto()
        {
            string status = "OK";
            string msg = "\"Pagamento atualizado com sucesso!\"";
            string tipo = "cartao";
            string pag = "true";
            string pag2 = "false";

            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por Boleto e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;
            string dataPagamento = jsonData1.dataPagamento.Value;
            int precoTotal = Convert.ToInt32(jsonData1.precoTotal.Value);
            int quantidadeTotal = Convert.ToInt32(jsonData1.quantidadeTotal.Value);
            string idUsuario = jsonData1.idUsuario.Value;

            // Atualizar para Boleto
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyBoleto(dataPagamento, true, precoTotal, quantidadeTotal, idUsuario, id);
            IRestResponse responseUp = up.ExecuteRequest();

            // Get para verificar a atualização
            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();
            dynamic jsonData = JsonConvert.DeserializeObject(responseGet.Content.ToString());


            Console.WriteLine("Dados atualizados: " + responseGet.Content.ToString());
            Console.WriteLine("Response do update: " + responseUp.Content.ToString());
            // Assertions do Update
            Assert.True(responseUp.IsSuccessful);
            Assert.AreEqual(msg, responseUp.Content.ToString());
            Assert.IsTrue((int)responseUp.StatusCode == 200);

            // Assertions do get
            Assert.True(responseGet.IsSuccessful);
            Assert.IsTrue((int)responseGet.StatusCode == 200);
            Assert.AreEqual(status, responseGet.StatusDescription);
            Assert.IsTrue(jsonData.formaPagamento.pix.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.boleto.Value == pag);
            Assert.IsTrue(jsonData.formaPagamento.credito.credito_status.Value == pag2);
            Assert.IsTrue(jsonData.formaPagamento.credito.juros.Value == pag2);
        }


        [Test]
        public void ExcluirPagamento()
        {
            string tipo = "pix";
            string msg = "\"Sua opção de pagamento foi excluída, retorne para o carrinho para prosseguir.\"";
            string notFound = "\"Not found\"";
            //Deletar todos os pagamentos criados anteriormente
            PagamentoStep.DeletarPagamentos();

            // Criar um pagamento por pix e pegar o ID
            dynamic jsonData1 = JsonConvert.DeserializeObject(PagamentoStep.CriarNovoPagamento(tipo).Content.ToString());
            string id = jsonData1.idPagamento.Value;

            // Excluir o pagamento pelo id
            DeletePagamento del = new DeletePagamento(id);
            IRestResponse response = del.ExecuteRequest();

            // Buscar o pagamento pelo id
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse responseGet = get.ExecuteRequest();

            //Assertions do Delete
            Assert.IsTrue((int)response.StatusCode == 200);
            Assert.AreEqual(msg,response.Content.ToString());
            //Assertions do Get
            Assert.IsTrue((int)responseGet.StatusCode == 404);
            Assert.AreEqual(notFound, responseGet.Content.ToString());
        }
        #endregion

        #region Testes Negativos
        [Test]
        public void AtualizarPagamentoPorIdInexistente()
        {
            string notFound = "\"Not found\"";
            string id = "inexistente";

            //Atualizar um pagamento informando um id inexistente
            UpdatePagamento up = new UpdatePagamento(id);
            up.SetJsonBodyBoleto("11/12/2022",true,1200,1,id,id);
            IRestResponse response = up.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue((int)response.StatusCode == 404);
            Assert.AreEqual(notFound, response.Content.ToString());
        }

        [Test]
        public void BuscarPagamentoPorIdInexistente()
        {
            string notFound = "\"Not found\"";
            string id = "inexistente";

            //Buscar um pagamento informando um id inexistente
            GetByIdPagamento get = new GetByIdPagamento(id);
            IRestResponse response = get.ExecuteRequest();

            Console.WriteLine(response.Content.ToString());

            Assert.IsTrue((int)response.StatusCode == 404);
            Assert.AreEqual(notFound, response.Content.ToString());
        }

        [Test]
        public void ExcluirPagamentoPorIdInexistente()
        {
            string notFound = "\"Not found\"";
            string id = "inexistente";

            //Excluir um pagamento informando um id inexistente
            DeletePagamento get = new DeletePagamento(id);
            IRestResponse response = get.ExecuteRequest();

            Console.WriteLine(response.Content);

            Assert.IsTrue((int)response.StatusCode == 404);
            Assert.AreEqual(notFound, response.Content.ToString());
        }
        #endregion

    }
}
