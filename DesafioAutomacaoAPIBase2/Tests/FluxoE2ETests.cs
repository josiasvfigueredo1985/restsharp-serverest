using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Carrinhos;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Helpers;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class FluxoE2ETests : TestBase
    {
        /// <Fluxo_E2E>
        /// 1-usuarios > cadastrar > pegar o id gerado 
        /// 2-produtos > cadastrar > pegar o id gerado 
        /// 3-carrinho > inserir os produtos no carrinho por id produto // Verificar se a quatidade 
        /// do produto iserido diminui em produtos> pegar o id gerado
        /// 4-carrinho > consultar o carrinho e verificar os produtos inseridos no mesmo através 
        /// dos id´s dos produtos inseridos anteriormente
        /// 5-mock- pagamento > cadastrar pagamento usando o valor total informado na consulta do carrinho, 
        /// utilizar o id do usuário no post> pegar o id gerado
        /// </>
        [Test]
        public void RealizarCompraPix()
        {


        }

        [Test]
        public void RealizarCompraBoleto()
        {


        }

        [Test]
        public void RealizarCompraCartaoCredito()
        {


        }
    }
}
