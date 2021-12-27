using Carteira.Investimentos.Admin.Controllers.Shared;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carteira.Investimentos.Admin.Controllers
{
    [Route("v1/operacoes")]
    public class OperacoesController : ApiController
    {
        private readonly IOperacaoService _operacaoService;

        public OperacoesController(IOperacaoService operacaoService)
        {
            _operacaoService = operacaoService;
        }

        /// <summary>
        /// Registro da compra de uma ação
        /// </summary>
        /// <param name="request">Código da ação, Preço, Quantidade</param>
        /// <returns>Retorna se foi registrado com sucesso</returns>
        /// <response code="201">Cadastrado com sucesso</response>
        /// <response code="400">Um ou mais erros de validação</response>
        /// <response code="500">Erro não esperado</response>
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status500InternalServerError)]
        [HttpPost("compra")]
        public async Task<IActionResult> PostCompra([FromBody] OperacaoCompraPostRequest request)
        {
            try
            {
                await _operacaoService.Comprar(request);

                return ResponseCreated();
            }
            catch (Exception)
            {
                return ResponseInternalServerError("Erro não esperado");
            }
        }

        /// <summary>
        /// Registro da venda de uma ação
        /// </summary>
        /// <param name="request">Código da ação, Preço, Quantidade</param>
        /// <returns>Retorna se foi registrado com sucesso</returns>
        /// <response code="201">Cadastrado com sucesso</response>
        /// <response code="400">Um ou mais erros de validação</response>
        /// <response code="500">Erro não esperado</response>
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status500InternalServerError)]
        [HttpPost("venda")]
        public async Task<IActionResult> PostVenda([FromBody] OperacaoVendaPostRequest request)
        {
            try
            {
                await _operacaoService.Vender(request);

                return ResponseCreated();
            }
            catch (Exception)
            {
                return ResponseInternalServerError("Erro não esperado");
            }
        }

        /// <summary>
        /// Listagem de operações com opção de filtro por código da ação
        /// </summary>
        /// <param name="request">Código da ação</param>
        /// <returns>Retorna as Operações</returns>
        /// <response code="200">Retorna as operações</response>
        /// <response code="400">Código da ação é nulo</response>
        /// <response code="500">Erro não esperado</response>
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResult), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OperacaoGetRequest request)
        {
            try
            {
                return ResponseOk(await _operacaoService.Listar(request));
            }
            catch (Exception)
            {
                return ResponseInternalServerError("Erro não esperado");
            }
        }
    }
}
