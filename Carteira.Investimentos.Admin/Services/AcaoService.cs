using AutoMapper;
using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;
using Carteira.Investimentos.Admin.Repositories;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Carteira.Investimentos.Admin.Services
{
    public class AcaoService : IAcaoService
    {
        private readonly IAcaoRepository _acaoRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public AcaoService(
            HttpClient httpClient,
            IAcaoRepository acaoRepository,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _acaoRepository = acaoRepository;
            _mapper = mapper;
        }

        public async Task Incluir(AcaoCadastroPostRequest request)
        {
            var acao = _mapper.Map<Acao>(request);

            await _acaoRepository.Incluir(acao);
        }

        public async Task<AcaoCotacaoGetResponse> ObterCotacao(string codigoAcao)
        {
            var cotacao = await ObterCotacaoApi(codigoAcao);

            if (cotacao is not null)
            {
                return _mapper.Map<AcaoCotacaoGetResponse>(cotacao.quoteResponse.result.FirstOrDefault());
            }

            return null;
        }

        private async Task<YahooFinanceApiGetResponse> ObterCotacaoApi(string codigoAcao)
        {
            var parametros = new Dictionary<string, string> {
                { "lang", "en" },
                { "region", "US" },
                { "symbols", codigoAcao }
            };

            var url = QueryHelpers.AddQueryString($"{_httpClient.BaseAddress}v6/finance/quote", parametros);

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {            
                var content = await response.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<YahooFinanceApiGetResponse>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }

            return null;
        }

        public async Task<bool> VerificarExistencia(string codigoAcao)
        {
            return await _acaoRepository.VerificarExistencia(codigoAcao);
        }

    }
}
