using AutoFixture;
using AutoMapper;
using Carteira.Investimentos.Admin.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carteira.Investimentos.Admin.Test.Services
{
    public class BaseServiceTest
    {
        public readonly Fixture _fixture;
        public readonly IMapper _mapper;

        public BaseServiceTest()
        {
            _fixture = new Fixture();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AcaoProfile>();
                cfg.AddProfile<OperacaoProfile>();
                cfg.ConstructServicesUsing(x => _mapper);
            });

            _mapper = config.CreateMapper();
        }
    }
}
