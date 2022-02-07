using DesafioAutomacaoRestSharp.Helpers;
using DesafioAutomacaoRestSharp.Steps;
using NUnit.Framework;
using System.Threading;
namespace DesafioAutomacaoRestSharp.Bases
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {      
            // Define qual URL será executada
            Ambiente_Step.SetURL();
            ExtentReportHelpers.CreateReport();
        }

        [SetUp]
        public void SetUp()
        {
            ExtentReportHelpers.AddTest();
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReportHelpers.AddTestResult();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportHelpers.GenerateReport();
        }
    }
}