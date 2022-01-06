using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Steps;

namespace DesafioAutomacaoAPIBase2.Tests
{
    class PagamentoTests
    {
        #region Testes Positivos
        [Test]
        public void ListarPagamentosCadastrados()
        {


        }

        [Test]
        public void CadastrarPagamentoPix()
        {



        }

        [Test]
        public void CadastrarPagamentoBoleto()
        {



        }

        [Test]
        public void CadastrarPagamentoCartaoCredito()
        {



        }

        [Test]
        public void BuscarPagamentoPorIDPix()
        {



        }


        [Test]
        public void BuscarPagamentoPorIDBoleto()
        {



        }


        [Test]
        public void BuscarPagamentoPorIDCredito()
        {



        }

        [Test]
        public void EditarPagamentoPixParaBoleto()
        {



        }

        [Test]
        public void EditarPagamentoPixParaCredito()
        {



        }

        [Test]
        public void EditarPagamentoBoletoParaPix()
        {



        }

        [Test]
        public void EditarPagamentoBoletoParaCredito()
        {



        }

        [Test]
        public void EditarPagamentoCreditoParaPix()
        {



        }
        [Test]
        public void EditarPagamentoCreditoParaBoleto()
        {



        }


        [Test]
        public void ExcluirPagamento()
        {



        }
        #endregion

        #region Testes Negativos
        [Test]
        public void CadastrarPagamentoSemInformarTipo()
        {



        }

        [Test]
        public void BuscarPagamentoPorIDInexistente()
        {



        }

        [Test]
        public void ExcluirPagamentoSemInformarID()
        {



        } 
        #endregion

    }
}
