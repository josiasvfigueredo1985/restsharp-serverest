using DesafioAutomacaoAPIBase2.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    class GetCarrinhos : RequestBase
    {
        public GetCarrinhos()
        {
            //Listar todos os carrinhos cadastrados
            requestService = "carrinhos";
            method = Method.GET;
            parameterTypeIsUrlSegment = false;
        }

        public GetCarrinhos(string id,int precoTotal,int quantidadeTotal,string idUsuario)
        {
            //Listar carrinhos cadastrados por parâmetros
            requestService = "carrinhos";
            method = Method.GET;
            parameters.Add("_id",id);
            parameters.Add("precoTotal", precoTotal.ToString());
            parameters.Add("quantidadeTotal", quantidadeTotal.ToString());
            parameters.Add("idUsuario", idUsuario);
            parameterTypeIsUrlSegment = false;
        }
    }
}
