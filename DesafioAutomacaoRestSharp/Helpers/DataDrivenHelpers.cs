﻿using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

public class DataDrivenHelpers
{
    //Install x64: Microsoft Access Database Engine 2010 Redistributable - https://www.microsoft.com/en-us/download/details.aspx?id=13255
    //Variable "cmdText" reffer your spreadsheet tab that which have testdata, in this case "Tab1" in the NameTelephone.xlsx file
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

    public static String ConnectionStringExcel(string excelFilePath)
    {
        if (!File.Exists(excelFilePath))
            throw new Exception(string.Format("File name: {0}", excelFilePath), new FileNotFoundException());
        string connectionStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", excelFilePath);

        return connectionStr;
    }
}