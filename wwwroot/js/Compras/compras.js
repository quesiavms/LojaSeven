let ultimaQuantidadeAdicionada = 0;

function AddProdutoEscolhido() {
	var select = $("#produtosSelect option:selected");
	var nome = select.text();
	var valor = select.data("valor");

	var quantidade = parseInt($("#qntdProduto").val());

	if (isNaN(quantidade || quantidade <= 0)) {
		alert("Informa uma quantidade valida!");
		return;
	}

	ultimaQuantidadeAdicionada = quantidade;

	for (var i = 0; i < quantidade; i++) {
		var linha = `
				<tr>
					<td>${nome}</td>
					<td>${valor}</td>
				</tr>
			`;

		$("#tabelaProdutos").append(linha);
	}
	$("#qntdProduto").val("");
}

var ConfirmarCompra = function () {
	let html = $("#tabelaProdutos").html();
	$("#tabelaProdutosModal").html(html);

	let total = 0;
	$("#tabelaProdutosModal tr").each(function () {
		let valor = parseFloat($(this).find("td:eq(1)").text().replace("R$", "").replace(",", ".").trim());
		if (!isNaN(valor)) {
			total += valor;
		}
	});

	$("#valorTotal").text("R$" + total.toFixed(2).replace(".", ","));

	$('#ModalListadeCompras').modal('show');
}

function DesfazerUltimoProduto() {
	let linhas = $("#tabelaProdutos tr");
	for (let i = 0; i < ultimaQuantidadeAdicionada; i++) {
		linhas.last().remove();
		linhas = $("#tabelaProdutos tr");
	}

	ultimaQuantidadeAdicionada = 0;
}

function CancelarCompra() {
	$("#tabelaProdutos").html('');
	$("#tabelaProdutosModal").html('');
	$("#valorTotal").text("R$ 0,00");
	$("#ModalListadeCompras").modal('hide');

}

var SalvarCompra = function () {
	var nome = $("input[type='text']").val();
	var metodoPagamento = $("#pagamentoSelect").val();

	var linhas = $("#tabelaProdutos tr");
	var produtos = [];

	linhas.each(function () {
		var nomeProduto = $(this).find("td:eq(0)").text();
		var valorProduto = $(this).find("td:eq(1)").text();
		var idProduto = $("#produtosSelect option").filter(function () {
			return $(this).text() === nomeProduto;
		}).val();

		produtos.push({
			Nome: nome,
			IdProduto: parseInt(idProduto),
			IdTipoPagamento: parseInt(metodoPagamento)
		});
	});

	$.ajax({
		type: "POST",
		url: "/Compras/SalvarCompra",
		contentType: "application/json",
		data: JSON.stringify(produtos),
		success: function (response) {
			alert(response.message);
			location.reload();
		}
	});
};