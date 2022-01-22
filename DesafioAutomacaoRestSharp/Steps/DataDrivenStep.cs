using DesafioAutomacaoAPIBase2.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DesafioAutomacaoAPIBase2.Steps
{
    public class DataDrivenStep
    {
        public static IEnumerable<TestCaseData> RetornaDadosNovosProduto
        {
            get
            {
                string folha = "Produtos";
                var testCases = new List<TestCaseData>();
                testCases = new DataDrivenHelpers().RetornaDadosExcel(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xls", folha);

                if (testCases != null)
                    foreach (TestCaseData testCaseData in testCases)
                        yield return testCaseData;
            }
        }

        public static IEnumerable<TestCaseData> RetornaDadosNovosUsuarios
        {
            get
            {
                string folha = "Usuarios";
                var testCases = new List<TestCaseData>();
                testCases = new DataDrivenHelpers().RetornaDadosExcel(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xls", folha);

                if (testCases != null)
                    foreach (TestCaseData testCaseData in testCases)
                        yield return testCaseData;
            }
        }
    }
}