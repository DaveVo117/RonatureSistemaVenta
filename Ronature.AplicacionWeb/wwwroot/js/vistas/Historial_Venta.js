

const VISTA_BUSQUEDA = {
    busquedaFecha: () => {
        $("#txtFechaInicio").val("")
        $("#txtFechaFin").val("")
        $("#txtNumeroVenta").val("")

        $(".busqueda-fecha").show()
        $(".busqueda-venta").hide()
    },

        busquedaVenta: () => {
        $("#txtFechaInicio").val("")
        $("#txtFechaFin").val("")
        $("#txtNumeroVenta").val("")

        $(".busqueda-fecha").hide()
        $(".busqueda-venta").show()
    }
}






$(document).ready(function () {
    VISTA_BUSQUEDA["busquedaFecha"]()

    $.datepicker.setDefaults($.datepicker.regional["es"])

    $("#txtFechaInicio").datepicker({dateFormat : "dd/mm/yy"})
    $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" })

    //$("#txtFechaInicio").val(new Date(new Date().getFullYear(), new Date().getMonth(), 1).toLocaleDateString());
    //$("#txtFechaFin").val(new Date().toLocaleDateString());
 /*   $("#txtFechaFin").datepicker({ dateFormat: "dd/mm/yy" })*/

})

{ dateFormat: "dd/mm/yy" }



$("#cboBuscarPor").change(function () {

    if ($("#cboBuscarPor").val() == "fecha") {
        VISTA_BUSQUEDA["busquedaFecha"]()
    } else {
        VISTA_BUSQUEDA["busquedaVenta"]()
    }

})





$("#btnBuscar").click(function () {

    if ($("#cboBuscarPor").val() == "fecha") {

        if ($("#txtFechaInicio").val().trim() == "" || $("#txtFechaFin").val().trim() == "") {
            toastr.warning("", "Debe ingresar ambas fechas")
            return;
        }

    } else {

        if ($("#txtNumeroVenta").val().trim() == "") {
            toastr.warning("", "Debe ingresar el número de venta")
            return;
        }
    }

    let numeroVenta = $("#txtNumeroVenta").val()
    let fechaInicio = $("#txtFechaInicio").val()
    let fechaFin = $("#txtFechaFin").val()


    $(".card-body").find("div.row").LoadingOverlay("show");

    fetch(`/Venta/Historial?numeroVenta=${numeroVenta}&fechaIni=${fechaInicio}&fechaFin=${fechaFin}`)
        .then(response => {
            $(".card-body").find("div.row").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            //<tr>
            //    <td>27/04/2022</td>
            //    <td>00001</td>
            //    <td>Boleta</td>
            //    <td>101010</td>
            //    <td>jose</td>
            //    <td>2000</td>
            //    <td><button class="btn btn-info btn-sm" data-toggle="modal" data-target="#modalData"><i class="fas fa-eye"></i> Ver Detalle</button></td>
            //</tr>

            $("#tbVenta tbody").html("");

            if (responseJson.length > 0) {

                responseJson.forEach((venta) => {

                    $("#tbVenta tbody").append(
                        $("<tr>").append(
                            $("<td>").text(venta.fechaRegistro),
                            $("<td>").text(venta.txtNumeroVenta),
                            $("<td>").text(venta.txtTipoDocumentoVenta),
                            $("<td>").text(venta.txtDocumentoCliente),
                            $("<td>").text(venta.total),
                            $("<td>").append($("<button>Detalle&nbsp;&nbsp;</button>").addClass("btn btn-info btn.sm").append($("<i>").addClass("fas fa-eye")).data("venta",venta))
                        )

                    )

                })

            }

        })


})





$("#tbVenta tbody").on("click", ".btn-info", function(){

    let d = $(this).data("venta")

    $("#txtFechaRegistro").val(d.fechaRegistro)
    $("#txtNumVenta").val(d.txtNumeroVenta)
    $("#txtUsuarioRegistro").val(d.txtUsuario)
    $("#txtTipoDocumento").val(d.txtTipoDocumentoVenta)
    $("#txtDocumentoCliente").val(d.txtDocumentoCliente)
    $("#txtNombreCliente").val(d.txtNombreCliente)
    $("#txtSubTotal").val(d.subTotal)
    $("#txtIGV").val(d.impuestoTotal)
    $("#txtTotal").val(d.total)

    $("#tbProductos tbody").html("");

    d.detalleVenta.forEach((item) => {

        $("#tbProductos tbody").append(
            $("<tr>").append(
                $("<td>").text(item.txtDescripcionProducto),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total)

            )

        )

    })


    $("#linkImprimir").attr("href",`/Venta/MostrarPDFVenta?numeroVenta=${d.txtNumeroVenta}`)

    $("#modalData").modal("show")


})




