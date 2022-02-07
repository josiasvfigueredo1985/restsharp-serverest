using DesafioAutomacaoRestSharp.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Collections.Generic;

namespace DesafioAutomacaoRestSharp.Steps
{
    public class DataDrivenStep
    {
        public static List<TestCaseData> RetornaDadosUsuarios
        {
            get
            {
                var testCases = new List<TestCaseData>();

                using (var fs = File.OpenRead(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Usuarios.csv"))
                using (var sr = new StreamReader(fs))
                {
                    string headerLine = sr.ReadLine();

                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();

                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ';' },
                                StringSplitOptions.None);

                            // Qualquer arquivo CSV com 4 colunas pode fazer uso deste método
                            string param1 = Convert.ToString(split[0]);
                            string param2 = Convert.ToString(split[1]);
                            string param3 = Convert.ToString(split[2]);
                            string param4 = Convert.ToString(split[3]);
                            var testCase = new TestCaseData(param1, param2, param3, param4);
                            testCases.Add(testCase);
                        }
                    }
                }

                return testCases;
            }
        }

        public static List<TestCaseData> RetornaDadosProdutos
        {
            get
            {
                var testCases = new List<TestCaseData>();
                using (var fs = File.OpenRead(GeneralHelpers.ReturnProjectPath() + @"DataDriven\Produtos.csv"))
                using (var sr = new StreamReader(fs))
                {
                    string headerLine = sr.ReadLine();

                    string line = string.Empty;
                    while (line != null)
                    {
                        line = sr.ReadLine();

                        if (line != null)
                        {
                            string[] split = line.Split(new char[] { ';' },
                                StringSplitOptions.None);

                            string param1 = Convert.ToString(split[0]); //nome
                            string param2 = Convert.ToString(split[1]); //preco
                            string param3 = Convert.ToString(split[2]); //descricao
                            string param4 = Convert.ToString(split[3]); //quantidade
                            var testCase = new TestCaseData(param1, param2, param3, param4);
                            testCases.Add(testCase);
                        }
                    }
                }
                return testCases;
            }
        }
    }
}