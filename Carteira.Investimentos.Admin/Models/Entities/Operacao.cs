namespace Carteira.Investimentos.Admin.Models.Entities
{
    public class Operacao
    {
        public string CodigoAcao { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        public decimal Quantidade  { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
