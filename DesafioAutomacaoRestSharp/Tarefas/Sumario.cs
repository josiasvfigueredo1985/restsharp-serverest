using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Requests.Pagamento;
using DesafioAutomacaoAPIBase2.Steps;

namespace DesafioAutomacaoAPIBase2.Tarefas
{
    public class Sumario
    {
        private Sumario()
        {
            /// Desafio Automação - APIs REST ///

            //Ferramentas permitidas: SoapUI, Postman ou código fonte com alguma biblioteca que manipula APIs como o RestSharp, RestAssured.
            //Ferramenta utilizada: RestSharp com NUnit.

            //400 Pontos
            //1) Implementar 50 scripts de testes que manipulem uma aplicação cuja interface é uma API REST.
            // 51 testes implementados nas classe de testes

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
            // Caminho do report gerado: DesafioAutomacaoAPIBase2_DEV\DesafioAutomacaoAPIBase2\bin\Debug\netcoreapp3.1\Reports

            //8) Implementar pelo menos dois ambientes(desenvolvimento / homologação)
            // Dev
            // https://josiasvfigueredo.visualstudio.com/Praticas_De_Testes/_git/DesafioAutomacaoRestSharp?path=%2F&version=GBDEV&_a=contents
            // QA
            // https://josiasvfigueredo.visualstudio.com/Praticas_De_Testes/_git/DesafioAutomacaoRestSharp?path=%2F&version=GBQA&_a=contents
            // Stage
            //https://josiasvfigueredo.visualstudio.com/Praticas_De_Testes/_git/DesafioAutomacaoRestSharp?path=%2F&version=GBSTAGE&_a=contents

            //9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados.
            // Massa de dados inseridas no banco de dados MySQL hospedado em https://www.freemysqlhosting.net/

            // Se usar WireMock(http://wiremock.org/) a massa será tratada implicitamente.
            // API´s mockadas em https://mockapi.io/projects/61b52c030e84b70017331a8a
            // Collection em anexo na pasta Postman
            //Classes de requests mockadas
            CreatePagamento create = new CreatePagamento();
            GetAllPagamento getall = new GetAllPagamento();
            GetByIdPagamento getById = new GetByIdPagamento("");
            DeletePagamento delete = new DeletePagamento("");
            UpdatePagamento update = new UpdatePagamento("");

            //10) Executar testes em paralelo.Pelo menos duas threads (25 testes cada).
            //Configurado nos ambientes de QA
            //11) Testes deverão ser agendados pelo Azure DevOps, Gitlab-CI, Jenkins, CircleCI, TFS ou outra ferramenta de CI que preferir.
            // Agendamento configurado com envio de email: 
        }
    }
}