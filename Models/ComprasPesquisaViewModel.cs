using System.Globalization;

namespace LojaSeven.Models
{
    public class ComprasPesquisaViewModel
    {
        public string Search { get; set; }

        public List<AllComprasViewModel> Compras { get; set; }
    }
}
