using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using DesafioAutomacaoAPIBase2.Helpers;
using System.IO;

namespace DesafioAutomacaoAPIBase2.Steps
{
    class DataDrivenStep
    {
        public static IEnumerable<TestCaseData> RetornaDadosNovosProduto
        {
            get
            {
                string folha = "Produtos";
                var testCases = new List<TestCaseData>();
                testCases = new DataDrivenHelpers().RetornaDadosExcel(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx", folha);

                if (testCases != null)
                    foreach (TestCaseData testCaseData in testCases)
                        yield return testCaseData;
            }
        }

        public static List<TestCaseData> ReturnDataUsingDataBase()
        {
            string query = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Queries/BuscaIdProdutos.sql");
            var testCases = DataDrivenHelpers.ReturnDataUsingDataBase(query);
            if (testCases.Count < 1)
            {
                return null;
            }
            else
            {
                return testCases;
            }
        }
    }
}
