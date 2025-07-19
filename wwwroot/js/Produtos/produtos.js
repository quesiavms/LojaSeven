$(document).ready(function () {
    $("#btnNovoProduto").click(function () {
        $("#ModalNovoProduto").modal("show");
    });
});

var InserirProduto = function () {
    var dadoProduto = {
        nome_produto: $("#nome_produto").val(),
        valor_produto: $("#valor_produto").val()
    }

    $.ajax({
        type: "POST",
        url: "/Produtos/InserirProduto",
        data: JSON.stringify(dadoProduto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#ModalNovoProduto").modal("hide");
            location.reload();
            alert("Novo Produto adicionado com sucesso !!");
        }
    });
}

var EditarProduto = function (idProduto) {

    $.ajax({
        type: "GET",
        url: "/Produtos/EditarProduto?id_produto=" + idProduto,
        success: function (response) {
            $("#edit_id_produto").val(response.id_produto);
            $("#edit_nome_produto").val(response.nome_produto);
            $("#edit_valor_produto").val(response.valor_produto);

            $("#ModalEditarProduto").modal("show");
        }
    });
}

var AtualizarProduto = function () {
    var dadoProduto = {
        id_produto: $("#edit_id_produto").val(),
        nome_produto: $("#edit_nome_produto").val(),
        valor_produto: $("#edit_valor_produto").val()
    };

    $.ajax({
        type: "POST",
        url: "/Produtos/AtualizarProduto",
        data: JSON.stringify(dadoProduto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#ModalEditarProduto").modal("hide");
            location.reload();
            alert("Produto atualizado com sucesso!");
        },
        error: function (xhr, status, error) {
            alert("Erro ao atualizar o produto.");
            console.log(error);
        }
    });
};

var ExcluirProduto = function (idProduto) {
    $("#id_produto").val(idProduto);
    $("#ModalExcluirProduto").modal("show");
};

var DeletarProduto = function () {
    var id_produto = $("#id_produto").val();

    $.ajax({
        type: "POST",
        url: "/Produtos/DeletarProduto?id_produto=" + id_produto,
        success: function (response) {
            if (response.success) {
                $("#ModalExcluirProduto").modal("hide");
                $("#userRow_" + id_produto).remove();
                alert(response.message);
            } else {
                alert("Erro ao excluir o produto!");
            }
        }
    })
}