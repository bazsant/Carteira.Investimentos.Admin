using AutoMapper;
using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Filters;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Repositories;

namespace Carteira.Investimentos.Admin.Services
{
    public class OperacaoService : IOperacaoService
    {
        private readonly IOperacaoRepository _operacaoRepository;
        private readonly IMapper _mapper;

        private const string TipoOperacaoCompra = "C";
        private const string TipoOperacaoVenda = "V";
        private const decimal CustoCorretagem = 5m;
        private const decimal Emolumentos = 0.0325m;

        public OperacaoService(IOperacaoRepository operacaoRepository,
            IMapper mapper)
        {
            _operacaoRepository = operacaoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperacaoGetResponse>> Listar(OperacaoGetRequest request)
        {
            var filtro = _mapper.Map<OperacaoListarFilter>(request);

            var result = await _operacaoRepository.Listar(filtro);

            return _mapper.Map<IEnumerable<OperacaoGetResponse>>(result);
        }

        public async Task Comprar(OperacaoCompraPostRequest request)
        {
            var operacao = _mapper.Map<Operacao>(request);

            operacao.Tipo = TipoOperacaoCompra;

            await Incluir(operacao);
        }

        public async Task Vender(OperacaoVendaPostRequest request)
        {
            var operacao = _mapper.Map<Operacao>(request);

            operacao.Tipo = TipoOperacaoVenda;

            await Incluir(operacao);
        }

        private async Task Incluir(Operacao operacao)
        {
            operacao.Data = DateTime.Now;
            operacao.ValorTotal = CalcularValorTotal(operacao.Preco);

            await _operacaoRepository.Incluir(operacao);
        }

        private decimal CalcularValorTotal(decimal valorAcao)
        {
            return valorAcao + CustoCorretagem + (valorAcao * Emolumentos);
        }

    }
}
