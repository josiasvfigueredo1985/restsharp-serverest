using DesafioAutomacaoRestSharp.Bases;
using DesafioAutomacaoRestSharp.Helpers;
using RestSharp;

namespace DesafioAutomacaoRestSharp.Requests.Carrinhos
{
    public class DeleteCancelarCompra : RequestBase
    {
        public DeleteCancelarCompra()
        {
            //Ao cancelar uma compra o carrinho é excluído e o estoque dos produtos desse carrinho é reabastecido.
            //O carrinho excluído será o vinculado ao usuário do token utilizado.
            url = JsonBuilder.ReturnParameterAppSettings("URL");
            requestService = "carrinhos/cancelar-compra";
            method = Method.DELETE;
            parameterTypeIsUrlSegment = false;
        }
    }
}