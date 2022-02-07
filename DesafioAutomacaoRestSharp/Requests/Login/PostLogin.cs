﻿using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;
using System.IO;

namespace DesafioAutomacaoRestSharp.Requests.Login
{
    public class PostLogin : RequestBase
    {
        public PostLogin()
        {
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "login";
            method = Method.POST;
            parameterTypeIsUrlSegment = false;
        }

        public void SetJsonBody(string email, string password)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Login.json");
            jsonBody = jsonBody.Replace("$email", email);
            jsonBody = jsonBody.Replace("$password", password);
            //          {
            //              "email": "$email",
            //"password": "$password"
            //          }
        }
    }
}