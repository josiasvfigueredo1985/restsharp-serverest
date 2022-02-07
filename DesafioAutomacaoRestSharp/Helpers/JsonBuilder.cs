using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;

namespace DesafioAutomacaoRestSharp.Helpers
{
    public class JsonBuilder
    {
        public static IConfigurationRoot configuration { get; set; } = null;

        public static string ReturnParameterAppSettings(string param)
        {
            // Este método não estava retornando os dados do arquivo atualizado
            //var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //configuration = builder.Build();

            //return configuration[param].ToString();

            //Método atualizado
            string json = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "appsettings.json");
            return JObject.Parse(json)[param].ToString();
        }

        public static void UpdateParameterAppSettings(string parameter, string newValue)
        {
            string path = GeneralHelpers.ReturnProjectPath() + "appsettings.json";
            var json = File.ReadAllText(path);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj[parameter] = newValue;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            StreamWriter sw = new StreamWriter(path);
            //Write a line of text
            sw.WriteLine(output);
            //Close the file
            sw.Close();
        }
    }
}