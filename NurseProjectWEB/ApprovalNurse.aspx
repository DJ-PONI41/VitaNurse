<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="ApprovalNurse.aspx.cs" Inherits="NurseProjectWEB.ApprovalNurse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Enfermera</title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Css/Style_Registrar_Enfermera.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>
        <div class="container">
            <div class="title">Registro de Enfermera</div>
            <form id="form1" runat="server">
                <div class="user-details">

                    <div id="input-box-nombre" class="input-box">
                        <label class="details" for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-direccion" class="input-box">
                        <label class="details" for="txtDireccion">Dirección</label>
                        <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-correo" class="input-box">
                        <label class="details" for="txtCorreo">Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreo" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-apellido-p" class="input-box">
                        <label class="details" for="txtApellidoPaterno">Apellido Paterno</label>
                        <asp:TextBox ID="txtApellidoPaterno" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-municipio" class="input-box">
                        <label class="details" for="txtMunicipio">Municipio</label>
                        <asp:TextBox ID="txtMunicipio" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-especialidad" class="input-box">
                        <label class="details" for="txtEspecialidad">Especialidad</label>
                        <asp:TextBox ID="txtEspecialidad" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-apellido-m" class="input-box">
                        <label class="details" for="txtApellidoMaterno">Apellido Materno</label>
                        <asp:TextBox ID="txtApellidoMaterno" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

                    </div>
                    <div id="input-box-mapa" class="input-box">
                        <label class="details" for="txtMapa">Mapa</label>
                        <div id="ModalMapPreview" class="box-mapa">
                        </div>
                    </div>

                    <div id="input-box-fecha-titulacion" class="input-box">
                        <label class="details" for="txtTitulacion">Fecha de Titulacion</label>
                        <asp:TextBox ID="txtTitulacion" CssClass="form-control" TextMode="Date" runat="server" />
                    </div>

                    <div id="input-box-ci" class="input-box">
                        <label class="details" for="txtCi">Ci</label>
                        <asp:TextBox ID="txtCi" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-profecional" class="input-box">
                        <span class="details">Titulo profecional</span>
                        <div id="input-box-file-2" class="input-box-file-type">
                            <label class="texto" for="fileUpload">Sube tu titulo profesional</label>
                            <asp:FileUpload ID="fileTitulo" runat="server" />
                        </div>
                    </div>

                    <div id="input-box-fecha" class="input-box">
                        <label class="details" for="txtFechaNacimiento">Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" TextMode="Date" runat="server" />
                    </div>


                    <div id="input-box-archivo" class="input-box">
                        <label class="details" for="fileUpload">Subir Tu fotografia</label>
                        <div class="box-archivo">
                            <asp:Image ID="imgPreview" runat="server" />
                        </div>
                    </div>

                    <div id="input-box-celular" class="input-box">
                        <label class="details" for="txtCelular">Número de Celular</label>
                        <asp:TextBox ID="txtCelular" CssClass="form-control" runat="server" />
                    </div>

                    <div id="input-box-file" class="input-box-file-type">
                        <p class="texto">Selecionar archivo</p>
                        <asp:FileUpload ID="fileUpload" runat="server" />
                    </div>

                    <div id="input-box-profecional-2" class="input-box">
                        <label class="details" for="fileUpload">Subir CV</label>
                        <div id="input-box-file-2" class="input-box-file-type">
                            <p class="texto">Selecionar archivo</p>
                            <asp:FileUpload ID="fileCvc" runat="server" />
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
                </div>
                <div class="button">                    
                    <asp:Button ID="btnRegistrar" runat="server" Text="Aceptar" OnClick="btnRegistrar_Click"/>                    
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive" style="max-height: 300px; overflow-y: auto; max-width: auto;">
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


</asp:Content>
