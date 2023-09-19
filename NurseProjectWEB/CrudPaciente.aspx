<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrudPaciente.aspx.cs" Inherits="NurseProjectWEB.CrudPaciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Paciente</title>
    
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="text-center">Registro de Paciente</h1>
            <div class="row">
                <div class="col-md-5">
                    <div class="form-group">
                        <label for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellidoPaterno">Apellido Paterno</label>
                        <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtApellidoMaterno">Apellido Materno</label>
                        <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtCi2">Ci</label>
                        <asp:TextBox ID="txtCi" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                        <%--<asp:Calendar ID="TxtFechaNacimiento" runat="server" CssClass="form-control" />--%>
                        <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="TxtFechaNacimiento" ></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <label for="txtDireccion">Dirección</label>
                        <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group" style="display: none;">
                        <label for="txtLat2">Latitud</label>
                        <asp:TextBox ID="txtLat" Text="-17.33059869950836" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group" style="display: none;">
                        <label for="txtLong2">Longitud</label>
                        <asp:TextBox ID="txtLong" Text="-66.22559118521447" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtCelular2">Número de Celular</label>
                        <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtMunicipio">Municipio</label>
                        <asp:TextBox ID="txtMunicipio" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtCorreo">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtHistorial2">Historial Medico</label>
                        <asp:TextBox ID="txtHistorial" CssClass="form-control" runat="server" />
                    </div>
                </div>
                <div class="col-md-5">
                    
                    <div class="form-group">
                        <label for="ddlRol">Rol</label>
                        <asp:DropDownList ID="cbnRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Selecciona un Rol" Value="" />
                            <asp:ListItem Text="Administrador" Value="Rol1" />
                            <asp:ListItem Text="Paciente" Value="Rol2" />
                            <asp:ListItem Text="Enfermera" Value="Rol3" />
                           
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="txtUsuario2">Usuario</label>
                        <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtContrasena2">Contraseña</label>
                        <asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" />
                    </div>
                   
                    <div class="form-group">
                        <asp:Label ID="label" runat="server"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="txtMapa">Mapa</label>
                        <div id="ModalMapPreview" style="width: 100%; height: 300px;"></div>
                    </div>
                    <div class="table-responsive d-flex table table-dark justify-content-center align-items-center">
                        <asp:GridView ID="GridDat" runat="server" CssClass="table table-bordered table-striped">
                            <Columns>
            
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="btn-group">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse"  CssClass="btn btn-primary"  OnClick="btnRegistrar_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Modificar"   CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Borrar"  CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
   
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $.getScript("https://maps.googleapis.com/maps/api/js?key=&libraries=places", function () {
                var map = new google.maps.Map(document.getElementById('ModalMapPreview'), {
                    center: { lat: parseFloat($('#<%=txtLat.ClientID%>').val()), lng: parseFloat($('#<%=txtLong.ClientID%>').val()) },
                    zoom: 18
                });

                var marker = new google.maps.Marker({
                    position: { lat: parseFloat($('#<%=txtLat.ClientID%>').val()), lng: parseFloat($('#<%=txtLong.ClientID%>').val()) },
                    map: map,
                    draggable: true
                });

                google.maps.event.addListener(marker, 'dragend', function (event) {
                    var lat = event.latLng.lat();
                    var lng = event.latLng.lng();

                    $('#<%=txtLat.ClientID%>').val(lat);
                    $('#<%=txtLong.ClientID%>').val(lng);
                });

                google.maps.event.addListener(map, 'click', function (event) {
                    var lat = event.latLng.lat();
                    var lng = event.latLng.lng();

                    marker.setPosition({ lat: lat, lng: lng });

                    $('#<%=txtLat.ClientID%>').val(lat);
                    $('#<%=txtLong.ClientID%>').val(lng);
                });
            });
        });
    </script>
</body>
</html>
