using AutoFixture;
using Carteira.Investimentos.Admin.Controllers;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData("AAPL", "Apple")]
        [InlineData("LAME3", "Lojas Americanas")]
        public async void CadastrarAcaoRetornandoErro500(string codigoAcao, string razaoSocal)
        {
            var request = _fixture.Create<AcaoCadastroPostRequest>();
            request.CodigoAcao = codigoAcao;
            request.RazaoSocialEmpresa = razaoSocal;

            Task retorno = _fixture.Create<Task>();

            _acaoService.VerificarExistencia(codigoAcao).Returns(false);
            _acaoService.Incluir(request).Throws(new Exception());

            var result = (JsonResult)await _controller.PostCadastro(request);

            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async void GetCotacaoComSucesso()
        {
            var codigoAcao = _fixture.Create<string>();
            var response = _fixture.Create<AcaoCotacaoGetResponse>();

            _acaoService.ObterCotacao(codigoAcao).Returns(response);

            var result = (JsonResult)await _controller.GetCotacao(codigoAcao);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void GetCotacaoNaoEncontrada()
        {
            var codigoAcao = _fixture.Create<string>();

            _acaoService.ObterCotacao(codigoAcao).ReturnsNull();

            var result = (JsonResult)await _controller.GetCotacao(codigoAcao);

            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async void GetCotacaoRetornaErro()
        {
            var codigoAcao = _fixture.Create<string>();

            _acaoService.ObterCotacao(codigoAcao).Throws(new Exception());

            var result = (JsonResult)await _controller.GetCotacao(codigoAcao);

            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
