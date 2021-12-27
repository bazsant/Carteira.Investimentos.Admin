namespace Carteira.Investimentos.Admin.Models.Requests
{
    public class OperacaoCompraPostRequest
    {
        public string CodigoAcao { get; set; }
        public decimal Preco { get; set; }
        public decimal Quantidade { get; set; }
    }
}
