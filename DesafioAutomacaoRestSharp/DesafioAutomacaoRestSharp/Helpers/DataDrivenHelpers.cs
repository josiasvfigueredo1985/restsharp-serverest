using ClosedXML.Excel;
using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;


public class DataDrivenHelpers
{
    /*
        public List<TestCaseData> RetornaDadosExcel(string excelFilePath, string sheet)
        {
            string cmdText = $"SELECT * FROM [{sheet}$]";
            string connectionStr = ConnectionStringExcel(excelFilePath);
            var ret = new List<TestCaseData>();

            using (var connection = new OleDbConnection(connectionStr))
            {
                connection.Open();
                var command = new OleDbCommand(cmdText, connection);
                var reader = command.ExecuteReader();
                if (reader == null)
                    throw new Exception(string.Format("Arquivo Excel sem dados, nome do arquivo:{0}", excelFilePath));

                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        var row = new List<string>();
                        var fieldCnt = reader.FieldCount;

                        for (var i = 0; i < fieldCnt; i++)

                            row.Add(reader.GetValue(i).ToString());

                        ret.Add(new TestCaseData(row.ToArray()));
                    }
                }
                return ret;
            }
        }
    */
    /*
    public static String ConnectionStringExcel(string excelFilePath)
    {
        if (!File.Exists(excelFilePath))
            throw new Exception(string.Format("File name: {0}", excelFilePath), new FileNotFoundException());
        //  string connectionStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\";", excelFilePath);
        string connectionStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", excelFilePath);

        return connectionStr;
    }
    */

    public List<TestCaseData> RetornaDadosExcel(string excelFilePath, int numFolha)
    {
        using var wbook = new XLWorkbook(excelFilePath);

        var ws1 = wbook.Worksheet(numFolha);

        List<string> row = new List<string>();
        var ret = new List<TestCaseData>();
        // Lista de colunas com conteúdo
        int col = ws1.ColumnsUsed().Count();
        // Arrays de títulos de coluna
        string[] cl = { "A", "B", "C", "D", "E", "F" };

        for (int i = 0; i < col; i++)
        {
            // Índice a partir da segunda linha para excluir os títulos da coluna que estão na primeira linha
            int ind = 2;
            // Dados da coluna e linha
            string cel = $"{cl[i]}{ind}";
            // Dados adicionados lidos da linha
            var data = ws1.Cell(cel).GetValue<string>();
            // Dados adicionadis na Lista
            row.Add(data.ToString());
            //Console.WriteLine(data);
        }
        // Adição dos casos de testes pela lista de dados lidas do Excel
        ret.Add(new TestCaseData(row.ToArray()));

        return ret;
    }
}

