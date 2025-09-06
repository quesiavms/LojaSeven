using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LojaSeven.Entidades
{
    [Table("tipopagamento")]
    public class TipoPagamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPagamento { get; set; }
        public string Tipo { get; set; }
    }
}
