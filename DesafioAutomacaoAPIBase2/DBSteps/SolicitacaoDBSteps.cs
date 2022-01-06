using DesafioAutomacaoAPIBase2.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAutomacaoAPIBase2.DBSteps
{
    public class SolicitacaoDBSteps
    {
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
    }
}
