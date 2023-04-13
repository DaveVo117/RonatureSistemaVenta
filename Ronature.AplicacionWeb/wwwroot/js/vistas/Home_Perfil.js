$(document).ready(function () {

    $(".container-fluid").LoadingOverlay("show");

    fetch("/Home/ObtenerUsuario")
        .then(response => {
            $(".container-fluid").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson)

            if (responseJson.estado) {
                const d = responseJson.objeto

                $("#imgFoto").attr("src", d.txtUrlFoto)
                $("#txtNombre").val(d.txtNombre)
                $("#txtCorreo").val(d.txtCorreo)
                $("#txtTelefono").val(d.txtTelefono)
                $("#txtRol").val(d.txtNombreRol)


            } else {
                swal("Lo sentimos ", responseJson.mensaje, "error")
            }


        })


})





$("#btnGuardarCambios").click(function () {

    //validación de campos
    if ($("#txtCorreo").val().trim() == "") {
        toastr.warning("", "Debe completar el campo : correo")
        $("#txtCorreo").focus()
        return;
    }

    if ($("#txtTelefono").val().trim() == "") {
        toastr.warning("", "Debe completar el campo : telefono")
        $("#txtTelefono").focus()
        return;
    }



    swal({
        title: "¿Desea guardar los cambios?",
/*        text: `Eliminar la categoria "${data.txtDescripcion}"`,*/
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-primary",
        confirmButtonText: "Si",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true
    },

        function (respuesta) {

            if (respuesta) {

                $(".showSweetAlert").LoadingOverlay("show");

                let modelo = {
                    txtCorreo: $("#txtCorreo").val().trim(),
                    txtTelefono: $("#txtTelefono").val().trim()
                    }

                fetch("/Home/GuardarPerfil", {
                    method: "POST",
                    headers: { "Content-Type": "application/json; charset=utf-8" },
                    body: JSON.stringify(modelo)
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {

                        if (responseJson.estado) {

                            swal("Listo!", "Los cambios fueron guardados", "success")
                        } else {
                            swal("Lo sentimos", responseJson.mensaje, "error")
                        }
                    })


            }

        }
    )



})





$("#btnCambiarClave").click(function () {

    //validación de campos
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }


    if ($("#txtClaveNueva").val().trim() != $("#txtConfirmarClave").val().trim()) {
        toastr.warning("", "Las contraseñas no coinciden")
        $("#txtCorreo").focus()
        return;
    }


    let modelo = {
        claveActual: $("#txtClaveActual").val().trim(),
        claveNueva: $("#txtClaveNueva").val().trim()
        }


    fetch("/Home/CambiarClave", {
        method: "POST",
        headers: { "Content-Type": "application/json; charset=utf-8" },
        body: JSON.stringify(modelo)
    })
        .then(response => {
            $(".showSweetAlert").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado) {

                swal("Listo!", "Su contraseña ha sido actualizada", "success")
                $("input.input-validar").val("");//Limpia campos con esa clase
            } else {
                swal("Lo sentimos", responseJson.mensaje, "error")
            }
        })

})