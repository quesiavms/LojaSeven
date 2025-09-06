using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaSeven.Entidades
{
    [Table("produtos")]
    public class Produtos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public string valorProduto { get; set; }
    }
}
