using DesafioAutomacaoAPIBase2.Helpers;
using DesafioAutomacaoAPIBase2.Steps;
using RestSharp;
using System.Collections.Generic;

namespace DesafioAutomacaoAPIBase2.Bases
{
    public class RequestBase
    {
        #region Parameters

        protected string jsonBody = null;

        //A URL é carregada em cada request em separado por se tratar de 2 API's diferentes
        protected string url = JsonBuilder.ReturnParameterAppSettings("URL");

        protected string requestService = null;

        protected Method method;

        protected bool httpBasicAuthenticator = false;

        protected bool ntlmAuthenticator = false;

        protected bool isFileRequest = false;

        protected FileTypes fileType;

        protected string fileName = null;

        protected string fileExtension = null;

        protected string filePath = null;

        protected string[] parametersArray = null;

        protected IDictionary<string, string> headers = new Dictionary<string, string>()
        {
            {"Authorization", LoginStep.RetornaBearerToken()},// API Token
            //Dicionário de headeres deve ser iniciado com os headers comuns a todos os métodos da API
            {"Content-Type", "application/json"},
        };

        protected IDictionary<string, string> cookies = new Dictionary<string, string>()
        {
            //Dicionário de cookies deve ser iniciado com os headers comuns à todas os métodos da API
        };

        protected IDictionary<string, string> parameters = new Dictionary<string, string>();

        protected bool parameterTypeIsUrlSegment = true;

        #endregion Parameters

        #region Actions

        public IRestResponse<dynamic> ExecuteRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);
            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response, false);

            return response;
        }

        public IRestResponse<dynamic> ExecuteFileRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteFileRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, fileType);
            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response, true);

            return response;
        }

        public IRestResponse<dynamic> ExecuteFileUploadRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteFileUploadRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, fileName, fileExtension, filePath);
            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response, true);

            return response;
        }

        public void RemoveHeader(string header)
        {
            headers.Remove(header);
        }

        public void RemoveCookie(string cookie)
        {
            cookies.Remove(cookie);
        }

        public void RemoveParameter(string parameter)
        {
            parameters.Remove(parameter);
        }

        public void SetMethod(Method method)
        {
            this.method = method;
        }

        #endregion Actions
    }
}