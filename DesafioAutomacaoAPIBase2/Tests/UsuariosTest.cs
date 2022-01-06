using NUnit.Framework;
using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Requests.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Helpers;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesafioAutomacaoAPIBase2.Tests
{
    [TestFixture]
    class UsuariosTests : TestBase
    {
        DeleteUsuario deleteUsuario;
        GetUsuarioPorId getUsuario;
        GetUsuarios getUsuarios;
        GetUsuarioPorId getUsuarioPorId;
        PostUsuario postUsuario;
        PutUsuario putUsuario;
        #region Testes Positivos

        [Test]
        public void ListarUsuariosCadastrados()
        {


        }

        [Test]
        public void CadastrarUsuario()
        {



        }

        [Test]
        public void BuscarUsuarioPorID()
        {



        }

        [Test]
        public void EditarUsuario()
        {



        }

        [Test]
        public void ExcluirUsuario()
        {



        }
        #endregion


        #region Testes Negativos
        [Test]
        public void CadastrarUsuarioDuplicado()
        {



        }

        [Test]
        public void CadastrarUsuarioEmailInvalido()
        {



        }

        [Test]
        public void BuscarUsuarioPorIDInexistente()
        {



        }

        [Test]
        public void EditarUsuarioInexistente()
        {



        }

        [Test]
        public void ExcluirUsuarioIdInexistente()
        {



        }
        #endregion
    }
}
