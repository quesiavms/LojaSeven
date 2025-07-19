$(document).ready(function () {
    $('#btnFilter').on('click', function () {
        btnFilter();
    });

    $('#btnExport').on('click', function () {
        btnExport();
    });
});

const btnFilter = () => {
    $.ajax({
        type: 'POST',
        url: 'AllCompras/Filter',
        data: {
            name: $("#name").val(),
            produtoId: $("#produtosSelect").val(),
            pagamentoId: $("#pagamentoSelect").val(),
            dataInicio: $("#startDate").val(),
            dataFim: $("#endDate").val()
        },
        success: function (response) {
            $('#tableBody').html(response);
        },
        error: function (err) {
            alert('Erro ao filtrar');
            console.log(err);
        }
    });
}

const btnExport = () => {
    const form = $('<form action="AllCompras/ExportToExcel" method="post" target="_blank"></form>');

    form.append(`<input type="hidden" name="name" value="${$("#name").val()}">`);
    form.append(`<input type="hidden" name="produtoId" value="${$("#produtosSelect").val()}">`);
    form.append(`<input type="hidden" name="pagamentoId" value="${$("#pagamentoSelect").val()}">`);
    form.append(`<input type="hidden" name="dataInicio" value="${$("#startDate").val()}">`);
    form.append(`<input type="hidden" name="dataFim" value="${$("#endDate").val()}">`);

    $('body').append(form);
    form.submit();
    form.remove();
}