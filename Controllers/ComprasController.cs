using LojaSeven.Entidades;
using LojaSeven.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaSeven.Controllers
{
    public class ComprasController : Controller
    {
        private readonly ConnectionDB _connection;

        public ComprasController(ConnectionDB connection)
        {
            _connection = connection;
        }
        public IActionResult Index()
        {
            List<Produtos> produtosList = _connection.Produtos.ToList();

            List<ProdutosViewModel> produtosViewModels = produtosList.Select(x => new ProdutosViewModel
            {
                id_produto = x.id_produto,
                nome_produto = x.nome_produto,
                valor_produto = x.valor_produto
            }).ToList();

            return View(produtosViewModels);
        }

        [HttpPost]
        public IActionResult SalvarCompra()
        {

            return Json(new { success = true, message = "Compra realizada com sucesso!" });
        }
    }
}
