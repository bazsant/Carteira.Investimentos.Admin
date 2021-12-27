using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;

namespace Carteira.Investimentos.Admin.Services
{
    public interface IOperacaoService
    {
        Task Comprar(OperacaoCompraPostRequest request);
        Task Vender(OperacaoVendaPostRequest request);
        Task<IEnumerable<OperacaoGetResponse>> Listar(OperacaoGetRequest request);
    }
}
