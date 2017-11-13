//---------Funciones--------//
//--------------------------//
function IniciarSesion() {
    alert("hola");
    SetIniciarSesion();
}

//-----------AJAX-----------//
//--------------------------//
function SetIniciarSesion() {
    var pRequest = "{'pUsuario':'a','pContrasena':'b'}";

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/LoadWeb",
        data: pRequest,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (pRespuesta) {
            console.log("Hola2");
            console.log(pRespuesta);
        }
    });
}