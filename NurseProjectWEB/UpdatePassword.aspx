<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="NurseProjectWEB.UpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualizar Contraseña</title>
    <!-- Agregar referencias a Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Actualizar Contraseña</h2>
            <div class="form-group">
                <label for="txtActual">Contraseña Actual:</label>
                <asp:TextBox ID="txtActual" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtNueva">Nueva Contraseña:</label>
                <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="txtRepetir">Repetir Contraseña:</label>
                <asp:TextBox ID="txtRepetir" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="btn-group">
                <asp:Button ID="btnActualizarContraseña" runat="server" Text="Actualizar Contraseña" CssClass="btn btn-primary" OnClick="btnActualizarContraseña_Click" />
                <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            </div>
        </div>
    </form>
</body>
</html>
