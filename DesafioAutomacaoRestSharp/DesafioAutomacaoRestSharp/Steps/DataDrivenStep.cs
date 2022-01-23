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
                int folha = 1;
                var testCases = new List<TestCaseData>();
                testCases = new DataDrivenHelpers().RetornaDadosExcel(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx", folha);

                if (testCases != null)
                    foreach (TestCaseData testCaseData in testCases)
                        yield return testCaseData;
            }
        }

        public static IEnumerable<TestCaseData> RetornaDadosNovosUsuarios
        {
            get
            {
                int folha = 2;
                var testCases = new List<TestCaseData>();
                testCases = new DataDrivenHelpers().RetornaDadosExcel(GeneralHelpers.ReturnProjectPath() + "DataDriven/Serverest.xlsx", folha);

                if (testCases != null)
                    foreach (TestCaseData testCaseData in testCases)
                        yield return testCaseData;
            }
        }
    }
}