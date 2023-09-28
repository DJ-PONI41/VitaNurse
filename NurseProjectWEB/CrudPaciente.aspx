﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrudPaciente.aspx.cs" Inherits="NurseProjectWEB.CrudPaciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Paciente</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <h1 class="text-center">Registro de Paciente</h1>
            <div class="row">
                <div class="col-md-3">
                    <!-- Columna 1 -->
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
                        <label for="txtCi">Ci</label>
                        <asp:TextBox ID="txtCi" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" runat="server" TextMode="Date" />
                    </div>

                    <div class="form-group">
                        <label for="txtCelular">Número de Celular</label>
                        <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" />
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
                <div class="col-md-3">
                    <!-- Columna 2 -->
                    <div class="form-group">
                        <label for="txtUsuario">Usuario</label>
                        <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <label for="txtContrasena">Contraseña</label>
                        <asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" />
                    </div>

                    <div class="form-group">
                        <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

                    </div>
                    <div class="form-group">
                        <label for="txtMapa">Mapa</label>
                        <div id="ModalMapPreview" style="width: 100%; height: 300px;"></div>
                    </div>
                </div>
                <div class="col-md-3">
                    <!-- Columna 3 -->
                    <div class="form-group">
                        <label for="txtDireccion">Dirección</label>
                        <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
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
                        <label for="txtHistorial">Historial Médico</label>
                        <asp:TextBox ID="txtHistorial" CssClass="form-control" runat="server" />
                    </div>
                </div>
                <!-- Columna 4 -->
                <div class="col-md-3">
                    <asp:Image ID="imgPreview" runat="server" CssClass="img-thumbnail" Width="200px" Height="200px" />
                    <div class="form-group">
                        <label for="fileUpload">Subir Archivo</label>
                        <asp:FileUpload ID="fileUpload" CssClass="form-control" runat="server" />
                    </div>
                    <div class="btn-group">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" class="btn btn-outline-primary" />
                        <button type="button" id="btnObtenerUbicacion" class="btn btn-primary">Obtener Ubicación</button>
                    </div>
                </div>
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
        </div>
    </form>

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
