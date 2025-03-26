using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaSeven.Entidades
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_produto { get; set; }
        public string nome_produto { get; set; }
        public string valor_produto { get; set; }
    }
}
