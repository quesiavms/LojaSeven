using System.ComponentModel.DataAnnotations;
namespace LojaSeven.Models
{
    public class ProdutosViewModel
    {
        public int id_produto { get; set; }
        public string nome_produto { get; set; }
        public string valor_produto { get; set; }
    }
}
