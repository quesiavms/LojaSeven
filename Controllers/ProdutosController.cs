using LojaSeven.Entidades;
using LojaSeven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojaSeven.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ConnectionDB _connection;

        public ProdutosController(ConnectionDB connection)
        {
            _connection = connection;
        }
        public IActionResult Index()
        {
            List<Produtos> produtosList = _connection.Produtos.ToList();

            List<ProdutosViewModel> produtosViewModels = produtosList.Select( x => new ProdutosViewModel
            {
                nome_produto = x.nome_produto,
                valor_produto = x.valor_produto
            }).ToList();

            return View(produtosViewModels);
        }
    }
}
