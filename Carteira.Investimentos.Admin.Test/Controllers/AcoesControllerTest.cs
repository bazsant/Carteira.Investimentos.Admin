using AutoFixture;
using Carteira.Investimentos.Admin.Controllers;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Carteira.Investimentos.Admin.Test.Controllers
{
    public class AcoesControllerTest : BaseControllerTest
    {
        private readonly IAcaoService _acaoService;

        private readonly AcoesController _controller;

        public AcoesControllerTest()
        {
            _acaoService = Substitute.For<IAcaoService>();

            _controller = new AcoesController(_acaoService);
        }

        [Theory]
        [InlineData("AAPL", "Apple")]
        [InlineData("LAME3", "Lojas Americanas")]
        [InlineData("PETR4", "Petrobras")]
        public async void CadastrarAcaoComSucesso(string codigoAcao, string razaoSocal)
        {
            var request = _fixture.Create<AcaoCadastroPostRequest>();
            request.CodigoAcao = codigoAcao;
            request.RazaoSocialEmpresa = razaoSocal;

            Task retorno = _fixture.Create<Task>();

            _acaoService.VerificarExistencia(codigoAcao).Returns(false);
            _acaoService.Incluir(request).Returns(retorno);

            var result = (JsonResult)await _controller.PostCadastro(request);

            result.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Theory]
        [InlineData("AAPL", "Apple")]
        [InlineData("LAME3", "Lojas Americanas")]
        public async void CadastrarAcaoJaExistente(string codigoAcao, string razaoSocal)
        {
            var request = _fixture.Create<AcaoCadastroPostRequest>();
            request.CodigoAcao = codigoAcao;
            request.RazaoSocialEmpresa = razaoSocal;

            Task retorno = _fixture.Create<Task>();

            _acaoService.VerificarExistencia(codigoAcao).Returns(true);
            _acaoService.Incluir(request).Returns(retorno);

            var result = (JsonResult)await _controller.PostCadastro(request);

            result.StatusCode.Should().Be(StatusCodes.Status409Conflict);
        }
    }
}
