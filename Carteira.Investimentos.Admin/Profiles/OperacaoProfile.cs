using AutoMapper;
using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Filters;
using Carteira.Investimentos.Admin.Models.JoinResults;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;

namespace Carteira.Investimentos.Admin.Profiles
{
    public class OperacaoProfile : Profile
    {
        public OperacaoProfile()
        {
            CreateMap<OperacaoCompraPostRequest, Operacao>();
            CreateMap<OperacaoVendaPostRequest, Operacao>();

            CreateMap<OperacaoGetRequest, OperacaoListarFilter>();

            CreateMap<OperacaoListarResult, OperacaoGetResponse>()
                .ForMember(dest => dest.ValorAcao, opt => opt.MapFrom(src => src.Preco));
        }
    }
}
