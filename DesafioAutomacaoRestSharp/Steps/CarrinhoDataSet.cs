using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DesafioAutomacaoAPIBase2.Helpers
{
    public class CarrinhoDataSet
    {
        public static void CriarJsonDatSet(List<string> idProdutos, int quantidade)
        {
            DataSet dataSet = new DataSet("Carrinho");
            DataTable table = new DataTable();

            table.TableName = "produtos";
            DataColumn idColumn = new DataColumn("idProduto", typeof(string));
            DataColumn itemColumn = new DataColumn("quantidade", typeof(int));

            table.Columns.Add(idColumn);
            table.Columns.Add(itemColumn);
            dataSet.Tables.Add(table);

            foreach (var id in idProdutos)
            {
                DataRow newRow = table.NewRow();
                newRow["idProduto"] = id;
                newRow["quantidade"] = quantidade;
                table.Rows.Add(newRow);
            }

            dataSet.AcceptChanges();
            string path = GeneralHelpers.ReturnProjectPath() + "Jsons/CarrinhoDataSet.json";
            string json = JsonConvert.SerializeObject(dataSet, Formatting.Indented);
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(json);
            sw.Close();

            Console.WriteLine(json);
        }
    }
}