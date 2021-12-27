using System.Net;

namespace Carteira.Investimentos.Admin.Controllers.Shared
{
    public class BaseResult
    {
        public HttpStatusCode Status { get; private set; }
        public bool Sucesso { get; private set; }
        public object Dados { get; private set; }
        public IEnumerable<string> Erros { get; private set; }

        public BaseResult(HttpStatusCode status, bool sucesso)
        {
            Status = status;
            Sucesso = sucesso;
        }

        public BaseResult(HttpStatusCode status, bool sucesso, IEnumerable<string> erros) : this(status, sucesso)
        {
            Erros = erros;
        }

        public BaseResult(HttpStatusCode status, bool sucesso, object dados) : this(status, sucesso)
        {
            Dados = dados;
        }
    }
}
