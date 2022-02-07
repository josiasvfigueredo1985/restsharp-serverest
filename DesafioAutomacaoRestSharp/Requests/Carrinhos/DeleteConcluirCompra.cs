using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Carrinhos
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