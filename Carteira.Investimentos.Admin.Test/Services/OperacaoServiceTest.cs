using AutoFixture;
using AutoMapper;
using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Filters;
using Carteira.Investimentos.Admin.Models.JoinResults;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Repositories;
using Carteira.Investimentos.Admin.Services;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carteira.Investimentos.Admin.Test.Services
{
    public class OperacaoServiceTest: BaseServiceTest
    {
        private readonly IOperacaoRepository _operacaoRepository;

        private readonly OperacaoService _operacaoService;

        public OperacaoServiceTest()
        {
            _operacaoRepository = Substitute.For<IOperacaoRepository>();

            _operacaoService = new OperacaoService(_operacaoRepository, _mapper);
        }

        [Fact]
        public async void ListarComSucesso()
        {
            var request = _fixture.Create<OperacaoGetRequest>();
            var filter = _fixture.Create<OperacaoListarFilter>();

            var response = _fixture.Create<IEnumerable<OperacaoListarResult>>();
            _operacaoRepository.Listar(filter).Returns(response);

            var result = await _operacaoService.Listar(request);

            result.Should().BeOfType<List<OperacaoGetResponse>>();
        }

        [Fact]
        public async void ComprarComSucesso()
        {
            var request = _fixture.Create<OperacaoCompraPostRequest>();            
            var operacao = _fixture.Create<Operacao>();

            var response = _fixture.Create<Task>();
            _operacaoRepository.Incluir(operacao).Returns(response);

            await _operacaoService.Comprar(request);
        }

        [Fact]
        public async void VenderComSucesso()
        {
            var request = _fixture.Create<OperacaoVendaPostRequest>();
            var operacao = _fixture.Create<Operacao>();

            var response = _fixture.Create<Task>();
            _operacaoRepository.Incluir(operacao).Returns(response);

            await _operacaoService.Vender(request);
        }
    }
}
