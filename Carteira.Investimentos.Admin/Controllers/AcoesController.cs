using Carteira.Investimentos.Admin.Controllers.Shared;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carteira.Investimentos.Admin.Controllers
{
    [Route("v1/acoes")]
    public class AcoesController : ApiController
    {
        private readonly IAcaoService _acaoService;

        public AcoesController(IAcaoService acaoService)
        {
            _acaoService = acaoService;
        }

        /// <summary>
        /// Cadastro de ações
        /// </summary>
        /// <param name="request">Código da ação e Razão social da empresa</param>
        /// <returns>Retorna se foi cadastrado com sucesso</returns>
        /// <response code="201">Cadastrado com sucesso</response>
        /// <response code="409">Código da ação já cadastrado</response>
        /// <response code="500">Erro não esperado</response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostCadastro([FromBody] AcaoCadastroPostRequest request)
        {
            try
            {
                var existe = await _acaoService.VerificarExistencia(request.CodigoAcao);

                if (existe)
                    return ResponseConflict("Código da ação já cadastrado");

                await _acaoService.Incluir(request);

                return ResponseCreated();
            }
            catch (Exception)
            {
                return ResponseInternalServerError("Erro não esperado");
            }
            
        }

        /// <summary>
        /// Retorno de cotação de uma ação a partir de fontes públicas
        /// </summary>
        /// <param name="codigoAcao">Código da ação</param>
        /// <response code="200">Cotação encontrada</response>
        /// <response code="400">Código da ação é nulo</response>
        /// <response code="404">Cotação não encontrada</response>
        /// <response code="500">Erro não esperado</response>
        [HttpGet("{codigoAcao}/cotacao")]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCotacao([FromRoute] string codigoAcao)
        {
            try
            {
                var cotacao = await _acaoService.ObterCotacao(codigoAcao);

                if (cotacao is null)
                {
                    return ResponseNotFound("Cotação não encontrada");
                }

                return ResponseOk(cotacao);
            }
            catch (Exception)
            {
                return ResponseInternalServerError("Erro não esperado");
            }
        }

    }
}
