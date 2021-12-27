using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Filters;
using Carteira.Investimentos.Admin.Models.JoinResults;
using Carteira.Investimentos.Admin.Models.Responses;

namespace Carteira.Investimentos.Admin.Repositories
{
    public interface IOperacaoRepository
    {
        Task Incluir(Operacao operacao);
        Task<IEnumerable<OperacaoListarResult>> Listar(OperacaoListarFilter filtro);
    }
}
