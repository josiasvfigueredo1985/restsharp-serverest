using DesafioAutomacaoAPIBase2.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAutomacaoAPIBase2.DBSteps
{
    public class SolicitacaoDBSteps
    {
        #region Produtos

        public static void InserirProdutoCriadoDB(string produto_id)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/InserirIDProdutos.sql", Encoding.UTF8);
            query = query.Replace("$produto_id", produto_id);

            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: produto_id = " + produto_id);
        }

        public static void DeletarTodosIdsProdutos()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/DeleteIdProdutos.sql", Encoding.UTF8);

            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Coluna: produto_id");
        }

        public static List<string> BuscarIdsProdutos()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/BuscaIdProdutos.sql", Encoding.UTF8);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Coluna: produto_id");
            return DBHelpers.RetornaDadosQueryMySQL(query);
        }

        public static void DeletarIdProduto(string idProduto)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/DeleteProdutoById.sql", Encoding.UTF8);
            query = query.Replace("$produto_id", idProduto);

            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, "PARAMETERS: Coluna: produto_id");
        }

        #endregion Produtos

        #region Usuários

        public static void InserirIdUsuarioCriadoDB(string nome, string email, string password, string administrador, string usuarioId)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/InserirDadosUsuario.sql", Encoding.UTF8);
            query = query.Replace("$nome", nome);
            query = query.Replace("$email", email);
            query = query.Replace("$password", password);
            query = query.Replace("$administrador", administrador);
            query = query.Replace("$id", usuarioId);
            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, $"PARAMETERS: Dados do usuário: \nNome: {nome}\nID: {usuarioId}");
        }

        public static List<string> BuscarUsuariosCriados()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/BuscaUsuariosCriados.sql", Encoding.UTF8);

            ExtentReportHelpers.AddTestInfo(2, $"PARAMETERS: Query executada: {query}");
            return DBHelpers.RetornaDadosQueryMySQL(query);
        }

        public static void DeletarUsuarioById(string idUsuario)
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/DeletarUsuarioPorId.sql", Encoding.UTF8);
            query = query.Replace("$id", idUsuario);

            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, $"PARAMETERS: Query executada: {query}");
        }

        public static void DeletarTodosUsuariosCriados()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/DeletarTodosUsuariosCriados.sql", Encoding.UTF8);

            DBHelpers.ExecuteQueryMySQL(query);

            ExtentReportHelpers.AddTestInfo(2, $"PARAMETERS: Query executada: {query}");
        }

        #endregion Usuários
    }
}