using LojaSeven.Entidades;
using LojaSeven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaSeven.Controllers
{
    public class AllComprasController : Controller
    {
        private readonly ConnectionDB _connection;

        public AllComprasController(ConnectionDB connection)
        {
            _connection = connection;
        }
        public IActionResult Index()
        {
            var compras = (from compra in _connection.Compra
                           join produto in _connection.Produtos
                           on compra.IdProduto equals produto.id_produto
                           join pagamento in _connection.TipoPagamento
                           on compra.IdTipoPagamento equals pagamento.IdPagamento
                           select new AllComprasViewModel
                           {
                               NomePessoa = compra.Nome,
                               NomeProduto = produto.nome_produto,
                               ValorProduto = produto.valor_produto,
                               TipoDoPagamento = pagamento.Tipo
                           }).ToList();

            var viewModel = new ComprasPesquisaViewModel
            {
                Search = "",
                Compras = compras
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetSearchRecord(string SearchText)
        {
            var viewModel = new ComprasPesquisaViewModel
            {
                Search = SearchText
            };

            var query = _connection.Compra
                .Include(c => c.Produtos)
                .Include(c => c.TipoPagamento)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(x =>
                    x.Nome.Contains(SearchText) ||
                    x.Produtos.nome_produto.Contains(SearchText) ||
                    x.TipoPagamento.Tipo.Contains(SearchText));
            }

            viewModel.Compras = query.Select(x => new AllComprasViewModel
            {
                NomePessoa = x.Nome,
                NomeProduto = x.Produtos.nome_produto,
                ValorProduto = x.Produtos.valor_produto,
                TipoDoPagamento = x.TipoPagamento.Tipo
            }).ToList();

            return PartialView("SearchPartial", viewModel);
        }
    }
}
