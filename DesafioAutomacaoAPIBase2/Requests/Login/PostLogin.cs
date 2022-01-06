using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.IO;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Login
{
    class PostLogin : RequestBase
    {
        public PostLogin()
        {
            requestService = "login";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }
        public void SetJsonBody(string email, string password)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath()+"Jsons/Login.json");
            jsonBody = jsonBody.Replace("$email",email);
            jsonBody = jsonBody.Replace("$password",password);
  //          {
  //              "email": "$email",
  //"password": "$password"
  //          }
        }
    }
}
