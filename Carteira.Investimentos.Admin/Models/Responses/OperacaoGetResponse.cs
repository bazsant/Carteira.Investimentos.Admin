namespace Carteira.Investimentos.Admin.Models.Responses
{
    public class OperacaoGetResponse
    {
        public string CodigoAcao { get; set; }
        public string RazaoSocial { get; set; }
        public string TipoOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorAcao { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
