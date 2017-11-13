<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>
<script type="text/javascript"  src="js/Inicio.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" id="btnEntrar" value="Entrar" class="button" onclick="javascript:IniciarSesion();"/>
            <br />
            <hr />
            <div id="caja" runat="server"></div>
        </div>
    </form>
</body>
</html>
