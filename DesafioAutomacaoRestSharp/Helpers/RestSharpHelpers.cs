using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DesafioAutomacaoAPIBase2.Helpers
{
    public class RestSharpHelpers
    {
        public static string fileDownloaded;
        public static string fileDownloadedPath;

        public static IRestResponse<dynamic> ExecuteRequest(string url,
                                                            string requestService,
                                                            Method method,
                                                            IDictionary<string, string> headers,
                                                            IDictionary<string, string> cookies,
                                                            IDictionary<string, string> parameters,
                                                            bool parameterTypeIsUrlSegment,
                                                            string jsonBody,
                                                            bool httpBasicAuthenticator,
                                                            bool ntlmAuthenticator)
        {
            RestRequest request = new RestRequest(requestService, method);

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            foreach (var parameter in parameters)
            {
                if (parameterTypeIsUrlSegment)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.UrlSegment);
                }
                else
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            request.JsonSerializer = new JsonSerializer();

            if (jsonBody != null)
            {
                if (GeneralHelpers.IsAJsonArray(jsonBody))
                {
                    request.AddJsonBody(new JArray(jsonBody));
                }
                else
                {
                    request.AddJsonBody(JsonConvert.DeserializeObject<JObject>(jsonBody));
                }
            }

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            client.AddHandler("application/json", new JsonDeserializer());

            return client.Execute<dynamic>(request);
        }

        public static IRestResponse<dynamic> ExecuteFileRequest(string url,
                                                    string requestService,
                                                    Method method,
                                                    IDictionary<string, string> headers,
                                                    IDictionary<string, string> cookies,
                                                    IDictionary<string, string> parameters,
                                                    bool parameterTypeIsUrlSegment,
                                                    string jsonBody,
                                                    bool httpBasicAuthenticator,
                                                    bool ntlmAuthenticator,
                                                    Enum fileType
                                                    )
        {
            RestRequest request = new RestRequest(requestService, method);

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            foreach (var parameter in parameters)
            {
                if (parameterTypeIsUrlSegment)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.UrlSegment);
                }
                else
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            request.JsonSerializer = new JsonSerializer();

            if (jsonBody != null)
            {
                if (GeneralHelpers.IsAJsonArray(jsonBody))
                {
                    request.AddJsonBody(new JArray(jsonBody));
                }
                else
                {
                    request.AddJsonBody(JsonConvert.DeserializeObject<JObject>(jsonBody));
                }
            }

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            client.AddHandler("application/json", new JsonDeserializer());

            IRestResponse<dynamic> response = client.Execute<dynamic>(request);

            try
            {
                var fileBytes = client.DownloadData(request);
                fileDownloaded = ExtentReportHelpers.testNameGlobal + "_Testfile" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "." + fileType;
                fileDownloadedPath = Path.Combine(ExtentReportHelpers.reportPath, fileDownloaded);
                File.WriteAllBytes(fileDownloadedPath, fileBytes);
            }
            catch (Exception e)
            {
                throw new Exception("Cannot download or save the file. " + e);
            }
            return response;
        }

        //----------------------------------------------------
        public static IRestResponse<dynamic> ExecuteFileUploadRequest(string url,
           string requestService,
            Method method,
           IDictionary<string, string> headers,
           IDictionary<string, string> cookies,
           IDictionary<string, string> parameters,
           bool parameterTypeIsUrlSegment,
           string jsonBody,
           bool httpBasicAuthenticator,
           bool ntlmAuthenticator,
           string fileName,
           string fileExtension,
           string filePath)
        {
            RestRequest request = new RestRequest(requestService, method);

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            foreach (var parameter in parameters)
            {
                if (parameterTypeIsUrlSegment)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.UrlSegment);
                }
                else
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            request.JsonSerializer = new JsonSerializer();

            if (jsonBody != null)
            {
                if (GeneralHelpers.IsAJsonArray(jsonBody))
                {
                    request.AddJsonBody(new JArray(jsonBody));
                }
                else
                {
                    request.AddJsonBody(JsonConvert.DeserializeObject<JObject>(jsonBody));
                }
            }

            request.AddFile(fileName, filePath, fileExtension);

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            //client.AddHandler("application/json", new JsonDeserializer());

            IRestResponse<dynamic> response = client.Execute<dynamic>(request);

            return response;
        }

        //----------------------------------------------------

        public static XmlNodeList getElementXml(IRestResponse<dynamic> response, string elementTag)
        {
            XmlDocument responseXml = new XmlDocument();
            responseXml.LoadXml(response.Content.ToString());

            return responseXml.GetElementsByTagName(elementTag);
        }

        public static IRestResponse<dynamic> ExecuteSoapRequest(string url,
                                                            IDictionary<string, string> headers,
                                                            IDictionary<string, string> cookies,
                                                            string bodyXml,
                                                            bool httpBasicAuthenticator,
                                                            bool ntlmAuthenticator)
        {
            RestRequest request = new RestRequest(url, Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Xml;

            foreach (var header in headers)
            {
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }

            foreach (var cookie in cookies)
            {
                request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
            }

            if (bodyXml != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(bodyXml);
                request.AddParameter("text/xml", xmlDoc.OuterXml, ParameterType.RequestBody);
            }

            RestClient client = new RestClient(url);

            if (httpBasicAuthenticator)
            {
                client.Authenticator = new HttpBasicAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            if (ntlmAuthenticator)
            {
                client.Authenticator = new NtlmAuthenticator(JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_USER"), JsonBuilder.ReturnParameterAppSettings("AUTHENTICATOR_PASSWORD"));
            }

            return client.Execute<dynamic>(request);
        }
    }
}