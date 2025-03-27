using LojaSeven.Entidades;
using LojaSeven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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
                id_produto = x.id_produto,
                nome_produto = x.nome_produto,
                valor_produto = x.valor_produto
            }).ToList();

            return View(produtosViewModels);
        }

        [HttpPost]
        public IActionResult InserirProduto([FromBody] ProdutosViewModel model)
        {
            if(model.id_produto == 0)
            {
                //insert
                Produtos produto = new Produtos
                {
                    id_produto = model.id_produto,
                    nome_produto = model.nome_produto,
                    valor_produto = model.valor_produto
                };

                _connection.Produtos.Add(produto);
                _connection.SaveChanges();
            }
            return Json(new { success = true, message = "Produto inserido com sucesso!" });
        }

        [HttpGet]
        public IActionResult EditarProduto(int id_produto)
        {
            var produto = _connection.Produtos.FirstOrDefault(x => x.id_produto == id_produto);
            if (produto == null)
            {
                return Json(new { success = false, message = "Produto não encontrado." });
            }
            return Json(produto);
        }

        [HttpPost]
        public IActionResult AtualizarProduto([FromBody] ProdutosViewModel model)
        {
            Produtos produto = _connection.Produtos.FirstOrDefault(x => x.id_produto == model.id_produto);
            
            if(produto != null)
            {
                produto.nome_produto = model.nome_produto;
                produto.valor_produto = model.valor_produto;
                _connection.SaveChanges();
                return Json(new { success = true, message = "Produto atualizado com sucesso!" });
            }
            return Json(new { success = false, message = "Produto não encontrado." });
        }

        [HttpPost]
        public IActionResult DeletarProduto (int id_produto)
        {
            var produto = _connection.Produtos.SingleOrDefault(x => x.id_produto == id_produto);
            
            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado!" });
            }

            _connection.Produtos.Remove(produto);
            _connection.SaveChanges();

            return Json(new { success = true, message = "Produto deletado com sucesso!" });
        }
    }
}
