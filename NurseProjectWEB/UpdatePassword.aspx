<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="NurseProjectWEB.UpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualizar Contraseña</title>   
    <link rel="stylesheet" href="Css/Style_update_password.css"/>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>

    
</head>
<body>
    <%--<form id="form1" runat="server">
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
    </form>--%>



    <div class="container">
      <header>Actualizar Contraseña</header>
      <div class="progress-bar">
        <div class="step">
          <p>Paso 1</p>
          <div class="bullet">
            <span>1</span>
          </div>
          <div class="check fas fa-check"></div>
        </div>
        <div class="step">
          <p>Paso 2</p>
          <div class="bullet">
            <span>2</span>
          </div>
          <div class="check fas fa-check"></div>
        </div>
        <div class="step">
          <p>Fin</p>
          <div class="bullet">
            <span>3</span>
          </div>
          <div class="check fas fa-check"></div>
        </div>
      </div>
      <div class="form-outer">
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
            </div>
          <div class="page slide-page">
            <div class="title">Contraseña actual</div>
            <div class="field">
              <asp:TextBox ID="txtActual" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="field">
              <button class="firstNext next">Siguiente</button>
            </div>
              <div class="field btns">
              <asp:Button  runat="server" Text="Cancelar" Class="submit" OnClick="btnVolver_Click" />
            </div>
          </div>


          <div class="page">
            <div class="title">Nueva Contraseña</div>
            <div class="field">
              <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="field btns">
              <button class="prev-2 prev">Atrás</button>
              <button class="next-2 next">Siguiente</button>
            </div>
              <div class="field btns">
              <asp:Button  runat="server" Text="Cancelar" Class="submit" OnClick="btnVolver_Click" />
            </div>
          </div>

          <div class="page">
            <div class="title">Repetir Contraseña</div>
            <div class="field">
              <asp:TextBox ID="txtRepetir" runat="server" TextMode="Password" CssClass="form-control" />
            </div>          
            <div class="field btns">
              <button class="prev-3 prev">Atrás</button>
                <asp:Button ID="btnActualizarContraseña" runat="server" Text="Aceptar" Class="submit" OnClick="btnActualizarContraseña_Click" />
            </div>
              <div class="field btns">
              <asp:Button  runat="server" Text="Cancelar" Class="submit" OnClick="btnVolver_Click" />
            </div>
          </div>
        </form>
      </div>
    </div>
    <script src="assets/js/script_update_password.js"></script>
</body>
</html>
