using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaSeven.Entidades
{
    [Table("Compra")]
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }
        public string Nome { get; set; }
        public int IdProduto { get; set; }
        public int IdTipoPagamento { get; set; }

        [ForeignKey("IdTipoPagamento")]
        public TipoPagamento TipoPagamento { get; set; }
        [ForeignKey("IdProduto")]
        public Produtos Produtos { get; set; }
    }
}
