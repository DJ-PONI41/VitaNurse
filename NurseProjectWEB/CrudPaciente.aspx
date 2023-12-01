<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrudPaciente.aspx.cs" Inherits="NurseProjectWEB.CrudPaciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Paciente</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Css/Style_Registrar_Paciente.css" />
</head>
<body>
    <div class="container">
        <div class="title">Registro de Paciente</div>
        <form id="form1" runat="server">
            <div class="user-details">
                <div id="input-box-nombre" class="input-box">
                    <label class="details" for="txtNombre">Nombre</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-usuario" class="input-box">
                    <label class="details" for="txtUsuario">Usuario</label>
                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-direccion" class="input-box">
                    <label class="details" for="txtDireccion">Dirección</label>
                    <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-apellido-p" class="input-box">
                    <label class="details" for="txtApellidoPaterno">Apellido Paterno</label>
                    <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-contrasenia" class="input-box">
                    <label class="details" for="txtContrasena">Contraseña</label>
                    <asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-municipio" class="input-box">
                    <label class="details" for="txtMunicipio">Municipio</label>
                    <asp:TextBox ID="txtMunicipio" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-apellido-m" class="input-box">
                    <label class="details" for="txtApellidoMaterno">Apellido Materno</label>
                    <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" runat="server" />
                </div>

                <div class="form-group">
                    <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

                </div>

                <div id="input-box-mapa" class="input-box">
                    <span class="details">Mapa</span>
                    <div id="ModalMapPreview" class="box-mapa">
                    </div>
                </div>

                <div id="input-box-correo" class="input-box">
                    <label class="details" for="txtCorreo">Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreo" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-ci" class="input-box">
                    <label class="details" for="txtCi">Ci</label>
                    <asp:TextBox ID="txtCi" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-historial" class="input-box">
                    <label class="details" for="txtHistorial">Historial Médico</label>
                    <asp:TextBox ID="txtHistorial" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-fecha" class="input-box">
                    <label class="details" for="txtFechaNacimiento">Fecha de Nacimiento</label>
                    <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" runat="server" TextMode="Date" />
                </div>

                <div id="input-box-archivo" class="input-box">
                    <span class="details">Sube tu Fotografia</span>
                    <div class="box-archivo">
                        <asp:Image ID="imgPreview" runat="server" />
                    </div>
                </div>

                <div id="input-box-celular" class="input-box">
                    <label class="details" for="txtCelular">Número de Celular</label>
                    <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" />
                </div>

                <div id="input-box-file" class="input-box-file-type">
                    <p class="texto">Obtener ubicacion</p>
                    <button type="button" id="btnObtenerUbicacion"></button>
                </div>

                <div id="input-box-file" class="input-box-file-type">
                    <label class="texto" for="fileUpload">Sube tu fotografia</label>
                    <asp:FileUpload ID="fileUpload" runat="server" Width="200px" Height="200px" />
                </div>

                <div class="form-group" style="display: none;">
                    <label for="txtLat">Latitud</label>
                    <asp:TextBox ID="txtLat" Text="-17.33059869950836" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="form-group" style="display: none;">
                    <label for="txtLong">Longitud</label>
                    <asp:TextBox ID="txtLong" Text="-66.22559118521447" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="button">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                <asp:Button ID="btnUpdate" runat="server" Text="Actualziar" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnAtras" runat="server" Text="Volver" OnClick="btnAtras_Click" />
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="table-responsive" style="max-height: 300px; overflow-y: auto;">
                        <asp:GridView ID="GridDat" runat="server" CssClass="table table-bordered table-striped table-dark table-sm">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </form>
    </div>





    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script>
        // Función de devolución de llamada para cargar la API de Google Maps
        function initMap() {
            var map;
            var marker;

            // Inicializa el mapa
            function initializeMap() {
                var mapOptions = {
                    center: { lat: parseFloat($('#<%=txtLat.ClientID%>').val()), lng: parseFloat($('#<%=txtLong.ClientID%>').val()) },
                    zoom: 18
                };
                map = new google.maps.Map(document.getElementById('ModalMapPreview'), mapOptions);

                marker = new google.maps.Marker({
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
            }

            // Función para obtener la ubicación en tiempo real
            function getLocation() {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (position) {
                        var lat = position.coords.latitude;
                        var lng = position.coords.longitude;

                        marker.setPosition({ lat: lat, lng: lng });

                        $('#<%=txtLat.ClientID%>').val(lat);
                        $('#<%=txtLong.ClientID%>').val(lng);

                        map.panTo({ lat: lat, lng: lng });
                        map.setZoom(18);
                    });
                } else {
                    alert("Geolocalización no es soportada por este navegador.");
                }
            }

            // Manejador de eventos para el botón "Obtener Ubicación"
            $("#btnObtenerUbicacion").click(function () {
                getLocation();
            });

            // Inicializa el mapa cuando se carga la página
            initializeMap();
        }

        // Carga la API de Google Maps utilizando la función de devolución de llamada
        $(document).ready(function () {
            var script = document.createElement('script');
            script.src = 'https://maps.googleapis.com/maps/api/js?key=&libraries=places&callback=initMap';
            document.body.appendChild(script);
        });
    </script>
</body>
</html>
