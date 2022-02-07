using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Requests.Pagamento;
using DesafioAutomacaoRestSharp.Steps;

namespace DesafioAutomacaoRestSharp.Tarefas
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
            //Planilhas DataDriven/Produtos.csv e DataDriven/Usuarios
            //Método estático implementado para testes datadriven de usuários e produtos:
            _ = DataDrivenStep.RetornaDadosProdutos;
            _ = DataDrivenStep.RetornaDadosUsuarios;
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
            // Caminho do report gerado: DesafioAutomacaoRestSharp\DesafioAutomacaoRestSharp\bin\Debug\netcoreapp3.1\Reports\

            //8) Implementar pelo menos dois ambientes
            //Local e Online, o local deve ser alterado no appsettings, o método SetURL realiza as alterações e executa a bat
            // que iniciar o ServeRest local.
            Ambiente_Step.SetURL();

            //9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados.
            // Massa de dados inseridas no banco de dados MySQL hospedado em https://www.freemysqlhosting.net/
            // Outro método adicionado para gerar massa de dados:
            ProdutosStep.CriarProdutosPlanilhaExcel();
            UsuarioStep.CriarUsuariosPlanilhaExcel();

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
            //Configurado em Propertires/AssemblyInfo.cs
            //11) Testes deverão ser agendados pelo Azure DevOps, Gitlab-CI, Jenkins, CircleCI, TFS ou outra ferramenta de CI que preferir.
            // Agendamento configurado com envio de email: 
            // https://josiasvfigueredo.visualstudio.com/Praticas_De_Testes/_apps/hub/ms.vss-ciworkflow.build-ci-hub?_a=edit-build-definition&id=14
        }
    }
}