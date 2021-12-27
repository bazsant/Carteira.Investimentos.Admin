namespace Carteira.Investimentos.Admin.Models.JoinResults
{
    public class OperacaoListarResult
    {
        public string CodigoAcao { get; set; }
        public string RazaoSocial { get; set; }
        public string TipoOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
