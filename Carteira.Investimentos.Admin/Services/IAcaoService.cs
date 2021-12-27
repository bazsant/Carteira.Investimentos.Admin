using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;

namespace Carteira.Investimentos.Admin.Services
{
    public interface IAcaoService
    {
        Task Incluir(AcaoCadastroPostRequest request);
        Task<bool> VerificarExistencia(string codigoAcao);
        Task<AcaoCotacaoGetResponse> ObterCotacao(string codigoAcao);
    }
}
