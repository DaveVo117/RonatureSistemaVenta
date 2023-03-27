const MODELO_BASE={
    idUsuario: 0,
    txtNombre: "",
    txtCorreo: "",
    txtTelefono: "",
    idRol: 0,
    snActivo: 1,
    txtUrlFoto:""
}//Utilizar inicial minuscula

let tablaData;

//$JQuery, id de tabla de la vista Index
$(document).ready(function () {

    fetch("/Usuario/ListaRoles")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboRol").append(
                        $("<option>").val(item.idRol).text(item.txtDescripcion)
                    )
                })
            }
        })



    tablaData =   $('#tbdata').DataTable({
        responsive: true,
         "ajax": {
             "url": '/Usuario/Lista', /*Para obtener la URL se ejecuta el proyecto*/
             "type": "GET",
             "datatype": "json"
         },
         "columns": [
             { "data": "idUsuario" ,"visible":false,"searchable":false},
             {
                 "data": "txtUrlFoto", render: function (data) {
                    return `<img atyle="height:60px" src=${data} class="rounded mx-auto d-block"/ >`
                 }
             },
             { "data": "txtNombre" },
             { "data": "txtCorreo" },
             { "data": "txtTelefono" },
             { "data": "txtNombreRol" },
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
         order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Usuarios',
                exportOptions: {
                    columns: [2,3,4,5,6] //especificar columnas que se descargarán inicia en 0
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
})


function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idUsuario)
    $("#txtNombre").val(modelo.txtNombre)
    $("#txtCorreo").val(modelo.txtCorreo)
    $("#txtTelefono").val(modelo.txtTelefono)
    $("#cboRol").val(modelo.idRol == 0 ? $("#cboRol option:first").val() : modelo.idRol)
    $("#cboEstado").val(modelo.snActivo)
    $("#txtFoto").val("")
    $("#imgUsuario").attr("src",modelo.txtUrlFoto)

    $("#modalData").modal("show")
}

$("#btnNuevo").click(function (){
    mostrarModal()
})

$("#btnGuardar").click(function () {

    //debugger;

    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idUsuario"] = parseInt($("#txtId").val())
    modelo["txtNombre"] = $("#txtNombre").val()
    modelo["txtCorreo"] = $("#txtCorreo").val()
    modelo["txtTelefono"] = $("#txtTelefono").val()
    modelo["idRol"] = $("#cboRol").val()
    modelo["snActivo"] = $("#cboEstado").val()

    const inputFoto = document.getElementById("txtFoto")

    const formData = new FormData();

    formData.append("foto", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idUsuario == 0) {

        fetch("/Usuario/Crear", {
            method: "POST",
            body:formData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false)
                    $("#modalData").modal("hide")
                    swal("Listo!", "El usuario fue creado", "success")
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error")
                }
            })

    }



})