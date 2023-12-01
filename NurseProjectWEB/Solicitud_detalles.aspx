<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud_detalles.aspx.cs" Inherits="NurseProjectWEB.Solicitud_detalles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8, initial-scale=1.0"/>
<title>Contactar Enfermera</title>
<link rel="stylesheet" href="Css/main.css"/>
<link href="https://fonts.googleapis.com/css?family=Quicksand" rel="stylesheet"/>
<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous"/>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.7.0/animate.min.css"/>
</head>
<body>
    <form id="form1" runat="server" action="#">

        <div class="content">

            <h1 class="logo">Agendar <span>Visita</span></h1>

            <div class="contact-wrapper animated bounceInUp">

                <div class="contact-form">
                    <div class="detalles">
                        <h3>Detalles</h3>
                    </div>
                    
                    <p>
                        <label>Direccion</label>
                        <asp:TextBox ID="txtdireccion" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>Fecha de la visita</label>
                        <asp:TextBox ID="txtDateVisita" runat="server" TextMode="Date"></asp:TextBox>
                    </p>
                    <p>
                        <label>Nombre del paciente</label>
                        <asp:TextBox ID="txtNamePaciente" runat="server"></asp:TextBox>
                    </p>
                    <p>
                        <label>Municipio</label>
                        <asp:TextBox ID="txtMunicipio" runat="server"></asp:TextBox>
                    </p>
                    <p class="block">
                        <label>Detalles de la visita</label>
                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </p>
                    <p class="block">
                        <asp:Button ID="btnEnviar" runat="server" Text="Aceptar" OnClick="btnEnviar_Click" />
                    </p>
                    <p class="block">
                        <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click" />
                    </p>
                </div>
                <div class="contact-info">
                    <h4>Marque su ubicacion</h4>
                    <div id="ModalMapPreview" class="map-box">
                    </div>
                </div>
            </div>

        </div>

        <div class="form-group" style="display: none;">
            <label for="txtLat">Latitud</label>
            <asp:TextBox ID="txtLat" Text="-17.33059869950836" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="form-group" style="display: none;">
            <label for="txtLong">Longitud</label>
            <asp:TextBox ID="txtLong" Text="-66.22559118521447" CssClass="form-control" runat="server"></asp:TextBox>
        </div> 
    </form>
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script>
        $(document).ready(function () {
            $.getScript("https://maps.googleapis.com/maps/api/js?key=AIzaSyDu4hP-gLcmJIsRfg9Lm8yrU58UC_IWKuw&libraries=places", function () {
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

