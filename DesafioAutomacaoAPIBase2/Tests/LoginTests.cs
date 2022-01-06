using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Requests.Login;
using DesafioAutomacaoAPIBase2.Steps;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class LoginTests : TestBase
    {
        PostLogin login;

        #region Testes Positivos
        [Test]
        public void LoginComSucesso()
        {

        }
        #endregion

        #region Testes Negativos
        [Test]
        public void LoginEmailInvalido()
        {

        }

        [Test]
        public void LoginSenhaInvalida()
        {

        } 
        #endregion
    }
}
