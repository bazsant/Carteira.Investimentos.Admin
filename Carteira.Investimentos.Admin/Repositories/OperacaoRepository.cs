using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Filters;
using Carteira.Investimentos.Admin.Models.JoinResults;
using Carteira.Investimentos.Admin.Models.Responses;
using Dapper;

namespace Carteira.Investimentos.Admin.Repositories
{
    public class OperacaoRepository : BaseRepository, IOperacaoRepository
    {
        public OperacaoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task Incluir(Operacao operacao)
        {
            var sql = @"
                INSERT INTO [dbo].[OPERACAO]
                       ([OPE_ACA_CODIGO]
                       ,[OPE_TIPO]
                       ,[OPE_DATA]
                       ,[OPE_QUANTIDADE]
                       ,[OPE_PRECO]
                       ,[OPE_VALOR_TOTAL])
                 VALUES
                       (@CodigoAcao
                       ,@Tipo
                       ,@Data
                       ,@Quantidade
                       ,@Preco
                       ,@ValorTotal)
            ";

            var parametros = operacao;

            await ExecuteAsync(sql, parametros);
        }

        public async Task<IEnumerable<OperacaoListarResult>> Listar(OperacaoListarFilter filtro)
        {
            var sql = @"
                SELECT [OPE_ACA_CODIGO] CodigoAcao
                      ,[ACA_RAZAO_SOCIAL] RazaoSocial
                      ,[OPE_TIPO] TipoOperacao
                      ,[OPE_DATA] DataOperacao
                      ,[OPE_QUANTIDADE] Quantidade
                      ,[OPE_PRECO] Preco
                      ,[OPE_VALOR_TOTAL] ValorTotal
                  FROM [dbo].[OPERACAO]
                  INNER JOIN [dbo].[ACAO] ON [ACA_CODIGO] = [OPE_ACA_CODIGO]
                  /**where**/
            ";

            var builder = new SqlBuilder();

            var selector = builder.AddTemplate(sql);

            if (!string.IsNullOrEmpty(filtro.CodigoAcao))
                builder.Where("[OPE_ACA_CODIGO] = @CodigoAcao", new { CodigoAcao = filtro.CodigoAcao });

            return await QueryAsync<OperacaoListarResult>(selector.RawSql, selector.Parameters);
        }
    }
}
