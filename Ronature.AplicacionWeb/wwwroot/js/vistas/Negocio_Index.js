$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");

    fetch("/Negocio/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson)

            if (responseJson.estado) {
                const d = responseJson.objeto

                $("#txtNumeroDocumento").val(d.txtNumeroDocumento)
                $("#txtRazonSocial").val(d.txtNombre)
                $("#txtCorreo").val(d.txtCorreo)
                $("#txtDireccion").val(d.txtDireccion)
                $("#txtTelefono").val(d.txtTelefono)
                $("#txtImpuesto").val(d.porcentajeImpuesto)
                $("#txtSimboloMoneda").val(d.txtSimboloMoneda)
                $("#imgLogo").attr("src", d.txtUrlLogo)

            } else {
                swal("Lo sentimos ",responseJson.mensaje,"error")
            }


        })


})




$("#btnGuardarCambios").click(function () {

    //Validación de campos
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje)
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus()
        return;
    }


//este modelo se envía al controlador para guardar
    const modelo = { 
        txtNumeroDocumento:$("#txtNumeroDocumento").val(),
        txtNombre: $("#txtRazonSocial").val(),
        txtCorreo: $("#txtCorreo").val(),
        txtDireccion: $("#txtDireccion").val(),
        txtTelefono: $("#txtTelefono").val(),
        porcentajeImpuesto: $("#porcentajeImpuesto").val(),
        txtSimboloMoneda: $("#txtSimboloMoneda").val()
        }

    const inputLogo = document.getElementById("txtLogo")
    const formData = new FormData();

    formData.append("logo",inputLogo.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $(".card-body").LoadingOverlay("show");



    fetch("/Negocio/GuardarCambios", {
        method: "POST",
        body: formData
        })
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            if (responseJson.estado > 0) {
                const d = responseJson.objeto

                $("#imgLogo").attr("src",d.txtUrlLogo)


            } else {
                swal("Lo sentimos ", responseJson.mensaje, "error")
            }


        })

})