using DesafioAutomacaoRestSharp.Helpers;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DesafioAutomacaoRestSharp.Steps
{
    class Ambiente_Step
    {
        public static void SetURL()
        {
            string selec = JsonBuilder.ReturnParameterAppSettings("ENV");

            if (selec == "1")
            {
                #region Encerrar processos abertos pela execução local

                foreach (var process in Process.GetProcessesByName("node"))
                {
                    process.Kill();
                }

                foreach (var process in Process.GetProcessesByName("cmd"))
                {
                    process.Kill();
                }

                foreach (var process in Process.GetProcessesByName("chrome"))
                {
                    process.Kill();
                }
                #endregion

                // Executar o ServeRestLocal através da bat
                string bat = GeneralHelpers.ReturnProjectPath() + "ServeRest/StartServeRest.bat";
                Process.Start(bat); 
            }
        }
    }
}
