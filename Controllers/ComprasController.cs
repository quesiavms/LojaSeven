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

            List<TipoPagamento> tiposPagamento = _connection.TipoPagamento.ToList();

            var viewModel = new AllComprasViewModel
            {
                Produtos = produtosList,
                TipoPagamento = tiposPagamento
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SalvarCompra([FromBody] List<CompraViewModel> compra)
        {
            if (compra == null || !compra.Any())
                return BadRequest("Lista Vazia maluco");

            var compraParaSalvar = compra.Select(x => new Compra
            {
                Nome = x.Nome,
                IdProduto = x.idProduto,
                IdTipoPagamento = x.IdTipoPagamento
            }).ToList();

            _connection.Compra.AddRange(compraParaSalvar);
            _connection.SaveChanges();

            return Json(new { success = true, message = "Compra realizada com sucesso!" });
        }
    }
}
