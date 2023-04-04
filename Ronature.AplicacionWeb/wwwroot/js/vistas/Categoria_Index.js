const MODELO_BASE = {
    idCategoria: 0,
    txtDescripcion: "",
    snActivo: 1
}//Utilizar inicial minuscula



let tablaData;

//$JQuery, id de tabla de la vista Index
$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Categoria/Lista', /*Para obtener la URL se ejecuta el proyecto(data)*/
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idCategoria", "visible": false, "searchable": false },
            {"data": "txtDescripcion"},
            {
                "data": "snActivo", render: function (data) {
                    if (data == 1)
                        return '<pan class="badge badge-info">Activo </span>';
                    else
                        return '<pan class="badge badge-danger">No Activo </span>';

                }
            },
            {
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "asc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Categorías',
                exportOptions: {
                    columns: [1,2] //especificar columnas que se descargarán inicia en 0 (Descripcion y Activo)
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json" //lenguajede Datatable
        },
    });
})





function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idCategoria)
    $("#txtDescripcion").val(modelo.txtDescripcion)
    $("#cboEstado").val(modelo.snActivo)

    $("#modalData").modal("show")
}


$("#btnNuevo").click(function () {
    mostrarModal()
})






$("#btnGuardar").click(function () {

    //validación de campos
    if ($("#txtDescripcion").val().trim() == "") {
        toastr.warning("", "Debe completar el campo : descripcion")
        $("#txtDescripcion").focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idCategoria"] = parseInt($("#txtId").val())
    modelo["txtDescripcion"] = $("#txtDescripcion").val()
    modelo["snActivo"] = $("#cboEstado").val()

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idCategoria == 0) { /*Inserta nuevo registro*/

        fetch("/Categoria/Crear", {
            method: "POST",
            headers: {"Content-Type":"application/json; charset=utf-8"},
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Listo!", "La Categoría fue creada", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    } else {
        //debugger;
        fetch("/Categoria/Editar", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {

                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide")
                    swal("Listo!", "La Categoría fue modificada", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    }



})






let filaSeleccionada;
$("#tbdata tbody").on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();
    //console.log(data)
    mostrarModal(data);
})








$("#tbdata tbody").on("click", ".btn-eliminar", function () {

    let fila;

    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Estas seguro?",
        text: `Eliminar la categoria "${data.txtDescripcion}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Categoria/Eliminar?IdCategoria=${data.idCategoria}`, {
                    method: "DELETE"
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            tablaData.row(fila).remove().draw();

                            swal("Listo!", "La Categoria fue eliminada", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })


            }

        }
    )

})