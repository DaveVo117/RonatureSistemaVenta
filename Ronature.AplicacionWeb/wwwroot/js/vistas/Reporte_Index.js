 let tablaData;

$(document).ready(function () {

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({ dateFormat: "dd/mm/yy" })
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" })




    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Reporte/ReporteVenta?fechaIni=01/01/1991&fechaFin=01/01/1991', /*Para obtener la URL se ejecuta el proyecto(data)*/
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fechaRegistro" },
            { "data": "txtNumeroVenta" },
            { "data": "txtTipoDocumento" },
            { "data": "txtDocumentoCliente" },
            { "data": "txtNombreCliente" },
            { "data": "subTotalVenta" },
            { "data": "impuestoTotalVenta" },
            { "data": "producto" },
            { "data": "cantidad" },
            { "data": "precio" },
            { "data": "total" },
        ],
        order: [[0, "asc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Ventas',
                //exportOptions: {
                //    columns: [1, 2] //se omite para exportar todas las columnas
                //}
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json" //lenguajede Datatable
        },
    });

})




$("#btnBuscar").click(function () {

    if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
        toastr.warning("", "Debe ingresar ambas fechas")
        return;
    }

    let fechaIni = $("#txtFechaInicio").val().trim()
    let fechaFin = $("#txtFechaFin").val().trim()

    let nueva_url = `/Reporte/ReporteVenta?fechaIni=${fechaIni}&fechaFin=${fechaFin}`;

    tablaData.ajax.url(nueva_url).load();

})