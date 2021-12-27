using AutoMapper;
using Carteira.Investimentos.Admin.Models.Entities;
using Carteira.Investimentos.Admin.Models.Requests;
using Carteira.Investimentos.Admin.Models.Responses;

namespace Carteira.Investimentos.Admin.Profiles
{
    public class AcaoProfile : Profile
    {
        public AcaoProfile()
        {
            CreateMap<AcaoCadastroPostRequest, Acao>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.CodigoAcao.ToUpperInvariant()))
                .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocialEmpresa));

            CreateMap<Result, AcaoCotacaoGetResponse>();
        }
    }
}
