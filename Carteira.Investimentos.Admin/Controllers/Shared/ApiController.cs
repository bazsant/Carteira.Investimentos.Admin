using Carteira.Investimentos.Admin.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Carteira.Investimentos.Admin.Controllers.Shared
{
    [ApiController]
    public abstract class ApiController: ControllerBase
    {
        protected IActionResult ResponseCreated() =>
            Response(HttpStatusCode.Created);

        protected IActionResult ResponseOk(object dados) =>
            Response(HttpStatusCode.OK, dados);

        protected IActionResult ResponseConflict(string mensagem) =>
            Response(HttpStatusCode.Conflict, mensagem);

        protected IActionResult ResponseInternalServerError(string mensagem) =>
            Response(HttpStatusCode.InternalServerError, mensagem);

        protected IActionResult ResponseNotFound(string mensagem) =>
            Response(HttpStatusCode.NotFound, mensagem);

        private new JsonResult Response(HttpStatusCode statusCode, object? dados, string? mensagemErro)
        {
            BaseResult? result = null;

            if (string.IsNullOrWhiteSpace(mensagemErro))
            {
                var sucesso = statusCode.IsSuccess();

                if (dados is not null)
                {
                    result = new BaseResult(statusCode, sucesso, dados);
                }
                else
                {
                    result = new BaseResult(statusCode, sucesso);
                }
            }
            else
            {
                var erros = new List<string>() { mensagemErro };

                result = new BaseResult(statusCode, false, erros);
            }

            return new JsonResult(result) { StatusCode = (int)result.Status };
        }
        private new IActionResult Response(HttpStatusCode statusCode) =>
            Response(statusCode, null, null);
        private new IActionResult Response(HttpStatusCode statusCode, object dados) =>
            Response(statusCode, dados, null);
        private new IActionResult Response(HttpStatusCode statusCode, string erros) =>
            Response(statusCode, null, erros);
    }
}
