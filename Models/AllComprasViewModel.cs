using LojaSeven.Entidades;

namespace LojaSeven.Models
{
    public class AllComprasViewModel
    {
        public List<Produtos> Produtos { get; set; }
        public List<ProdutosViewModel> ProdutosViewModel { get; set; }
        public List<TipoPagamento> TipoPagamento { get;set; }
        public List<Compra> Compra { get; set; }
        public string NomePessoa { get; set; }
        public string NomeProduto { get; set; }
        public string ValorProduto { get; set; }
        public string TipoDoPagamento { get; set; }
    }
}
