using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using DesafioAutomacaoAPIBase2.Steps;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPIBase2.Tarefas
{
    class Sumario
    {
        private Sumario()
        {
            /// Desafio Automação - APIs REST ///     

            //Ferramentas permitidas: SoapUI, Postman ou código fonte com alguma biblioteca que manipula APIs como o RestSharp, RestAssured.
            //Ferramenta utilizada: RestSharp com NUnit.

            //400 Pontos
            //1) Implementar 50 scripts de testes que manipulem uma aplicação cuja interface é uma API REST.
            // Implementados na classe Tests

            //2) Alguns scripts devem ler dados de uma planilha Excel para implementar Data-Driven.
            //Planilha Serverest.xlsx
            //Método estático implementado para inserir produtos: 
            ProdutosStep.CriarProdutosPlanilhaExcel();

            //3) --Notem que 50 scripts podem cobrir mais de 50 casos de testes se usarmos Data-Driven.
            //Em outras palavras, implementar 50 CTs usando data-driven não é a mesma coisa que implementar 50 scripts.

            //4) O projeto deve tratar autenticação.Exemplo: OAuth2.
            //Método implementado para retornar um Bearer Token na Request Base.
            LoginStep.RetornaBearerToken();

            //5) Pelo menos um teste deve fazer a validação usando REGEX(Expressões Regulares).
            // Método estático implementado para realizar asserção de status code com sucesso para 200 e 201
            GeneralHelpers.RegexStatusCodesSucesso("OK");
            GeneralHelpers.RegexStatusCodesSucesso("Created");

            //6) Pelo menos um script deve usar código Groovy / Node.js ou outra linguagem para fazer scripts.
            // Não implementado

            // 800 Pontos (Metas 1 até 4 + as metas abaixo)
            //7) O projeto deverá gerar um relatório de testes automaticamente.
            // Caminho do report gerado: \DesafioAutomacaoAPIRest\DesafioAutomacaoAPIBase2\DesafioAutomacaoAPIBase2\bin\Debug\netcoreapp3.1\Reports

            //8) Implementar pelo menos dois ambientes(desenvolvimento / homologação)
            // Dev
            // https://josiasvfigueredo.visualstudio.com/TreinamentoRestAssured/_git/DesafioAutomacaoAPIRest_Dev
            // QA
            // https://josiasvfigueredo.visualstudio.com/TreinamentoRestAssured/_git/DesafioAutomacaoAPIRest_QA
            // Stage
            // https://josiasvfigueredo.visualstudio.com/TreinamentoRestAssured/_git/DesafioAutomacaoAPIRest_Stage

            //9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados.
            // Massa de dados inseridas no banco de dados MySQL hospedado em https://www.freemysqlhosting.net/

            // Se usar WireMock(http://wiremock.org/) a massa será tratada implicitamente.
            // API´s mockadas em https://mockapi.io/projects/61b52c030e84b70017331a8a
            //Classes de requests mockadas 
            CreatePagamento create = new CreatePagamento();
            GetAllPagamento getall = new GetAllPagamento();
            GetByIdPagamento getById = new GetByIdPagamento("");
            DeletePagamento delete = new DeletePagamento("");
            UpdatePagamento update = new UpdatePagamento("");

            //10) Executar testes em paralelo.Pelo menos duas threads (25 testes cada).

            //11) Testes deverão ser agendados pelo Azure DevOps, Gitlab-CI, Jenkins, CircleCI, TFS ou outra ferramenta de CI que preferir.

        }

    }
}
