using Carteira.Investimentos.Admin.Models.Entities;

namespace Carteira.Investimentos.Admin.Repositories
{
    public interface IAcaoRepository
    {
        Task Incluir(Acao acao);
        Task<bool> VerificarExistencia(string codigoAcao);
    }
}
