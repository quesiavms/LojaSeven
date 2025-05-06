using LojaSeven.Entidades;

namespace LojaSeven.Models
{
    public class ComprasViewModel
    {
        public List<Produtos> Produtos { get; set; }

        public List<ProdutosViewModel> ProdutosViewModel { get; set; }
        public List<TipoPagamento> TipoPagamento { get;set; }
    }
}
