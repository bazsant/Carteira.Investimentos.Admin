using Carteira.Investimentos.Admin.Models.Entities;
using Dapper;

namespace Carteira.Investimentos.Admin.Repositories
{
    public class AcaoRepository : BaseRepository, IAcaoRepository
    {
        public AcaoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Incluir(Acao acao)
        {
            var sql = @"
                INSERT INTO [dbo].[ACAO]
                       ([ACA_CODIGO]
                       ,[ACA_RAZAO_SOCIAL])
                 VALUES
                       (@Codigo
                       ,@RazaoSocial)
            ";

            var parametros = acao;

            await ExecuteAsync(sql, parametros);
        }

        public async Task<bool> VerificarExistencia(string codigoAcao)
        {
            var sql = @"
                SELECT 
                    TOP 1 1
                FROM    
                    [dbo].[ACAO]
                WHERE 
                    [ACA_CODIGO] = @Codigo
            ";

            var parametros = new { Codigo = codigoAcao };

            return await QueryFirstOrDefaultAsync<bool>(sql, parametros);
        }
    }
}
