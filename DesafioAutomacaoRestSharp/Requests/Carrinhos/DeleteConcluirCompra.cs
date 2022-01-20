using DesafioAutomacaoAPIBase2.Bases;
using DesafioAutomacaoAPIBase2.Helpers;
using RestSharp;

namespace DesafioAutomacaoAPIBase2.Requests.Carrinhos
{
    public class DeleteConcluirCompra : RequestBase
    {
        public DeleteConcluirCompra()
        {
            //Ao concluir uma compra o carrinho é excluído.
            //O carrinho excluído será o vinculado ao usuário do token utilizado.
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "carrinhos/concluir-compra";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}