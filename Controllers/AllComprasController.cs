using System.Globalization;
using LojaSeven.Entidades;
using LojaSeven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

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
            var comprasList = _connection.Compra
                .Include(c => c.Produtos)
                .Include(c => c.TipoPagamento)
                .ToList();

            var produtosList = _connection.Produtos.ToList();
            var pagamentosList = _connection.TipoPagamento.ToList();

            var viewModel = new AllComprasViewModel
            {
                Compra = comprasList,
                Produtos = produtosList,
                TipoPagamento = pagamentosList
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Filter(string? name, int? produtoId, int? pagamentoId, DateTime? dataInicio, DateTime? datafim)
        {
            var comprasFiltradas = ObterComprasFiltradas(name, produtoId, pagamentoId, dataInicio, datafim);

            return PartialView("_TabelaAllComprasPartial", comprasFiltradas);
        }

        [HttpPost]
        public IActionResult ExportToExcel(string? name, int? produtoId, int? pagamentoId, DateTime? dataInicio, DateTime? datafim)
        {
            var compras = ObterComprasFiltradas(name, produtoId, pagamentoId, dataInicio, datafim);
            var arquivoExcel = GerarExcelCompras(compras);

            return File(arquivoExcel,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"ReportGloryStoryGerado_{DateTime.Now:ddMMyyyy}.xlsx");
        }

        public List<Compra> ObterComprasFiltradas(string? name, int? produtoId, int? pagamentoId, DateTime? dataInicio, DateTime? datafim)
        {
            var query = _connection.Compra
                .Include(c => c.Produtos)
                .Include(c => c.TipoPagamento)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Nome.Contains(name));
            }

            if (produtoId.HasValue && produtoId > 0)
            {
                query = query.Where(c => c.IdProduto == produtoId);
            }

            if (pagamentoId.HasValue && pagamentoId > 0)
            {
                query = query.Where(c => c.IdTipoPagamento == pagamentoId);
            }

            if (dataInicio.HasValue)
            {
                query = query.Where(c => c.DataCompra >= dataInicio.Value);
            }

            if (datafim.HasValue)
            {
                query = query.Where(c => c.DataCompra <= datafim.Value);
            }

            return query.ToList();
        }

        private byte[] GerarExcelCompras(List<Compra> compras)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Compras");

            worksheet.Cell(1, 1).Value = "Nome do Cliente"; // linha 1, coluna 1
            worksheet.Cell(1, 2).Value = "Produto"; // linha 1, coluna 2
            worksheet.Cell(1, 3).Value = "Valor"; // linha 1, coluna 3
            worksheet.Cell(1, 4).Value = "Forma de Pagamento"; // linha 1, coluna 4
            worksheet.Cell(1, 5).Value = "Data da Compra"; // linha 1, coluna 5

            var headerRange = worksheet.Range("A1:E1"); // mudando cor da primeira linha, header
            headerRange.Style.Fill.BackgroundColor = XLColor.Black;
            headerRange.Style.Font.FontColor = XLColor.White;

            int row = 2;
            foreach (var compra in compras)
            {
                worksheet.Cell(row, 1).Value = compra.Nome;
                worksheet.Cell(row, 2).Value = compra.Produtos?.nomeProduto ?? "N/A";
                worksheet.Cell(row, 3).Value = compra.Produtos?.valorProduto ?? "N/A";
                worksheet.Cell(row, 4).Value = compra.TipoPagamento ?.Tipo?? "N/A";
                worksheet.Cell(row, 5).Value = compra.DataCompra.ToString("dd/MM/yyyy");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return stream.ToArray();
        }
    }
}
