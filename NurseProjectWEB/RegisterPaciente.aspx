<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPaciente.aspx.cs" Inherits="NurseProjectWEB.RegisterPaciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro de Paciente</title>

    <link href="http://netdna.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.css" rel="stylesheet"/>
    <link href="assets/css/bootstrap.min.css" type="text/css" rel="stylesheet"/>
	<link href="assets/css/gsdk-bootstrap-wizard.css" type="text/css" rel="stylesheet"/>
	<link href="assets/css/home.css" type="text/css" rel="stylesheet"/>
    <link href="assets/css/cargando.css" rel="stylesheet" type="text/css"/>
    <link href='https://unpkg.com/boxicons@2.0.9/css/boxicons.min.css' rel='stylesheet'/>
</head>
<body>


    <div class="image-container set-full-height">

                 

    <div class="container">
        <div class="row">
        <div class="col-sm-8 col-sm-offset-2">

            <div class="wizard-container">
                <div class="card wizard-card" data-color="orange" id="wizardProfile">
                    <form id="form1" runat="server">
                    	<div class="wizard-header">
                        	<h3> Registrar Nuevo Paciente </h3>
                    	</div>
                    

      					<div class="wizard-navigation">
          	             <ul>
                            <li><a href="#about" data-toggle="tab">Paso 1</a></li>
                            <li><a href="#account" data-toggle="tab">Paso 2</a></li>
                            <li><a href="#new-step" data-toggle="tab">Paso 3</a></li>
                            <li><a href="#new-step2" data-toggle="tab">Paso 4</a></li>
                            <li><a href="#address" data-toggle="tab">Fin</a></li>
                         </ul>
      					</div>

                        <div class="tab-content">
                            <div class="tab-pane" id="about">
                              <div class="row">
                                  <div class="form-group">
                                        <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>Nombre</label>
                                          <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>APELLIDO PATERNO</label>
                                        <asp:TextBox name="txtApellidoPaterno" ID="txtApellidoPaterno" class="form-control" placeholder="Apellido paterno" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                       <div class="form-group">
                                        <label>APELLIDO MATERNO</label>
                                        <asp:TextBox name="txtApellidoMaterno" id="txtApellidoMaterno" class="form-control" placeholder="Apellido materno" runat="server" />
                                      </div>
                                    </div>

                                    <div class="col-sm-5 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>CELULAR </label>
                                        <asp:TextBox type="number" name="txtCelular" id="txtCelular" class="form-control" placeholder="Celular" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-4 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>CI</label>
                                        <asp:TextBox type="number" name="txtCi" id="txtCi" class="form-control" placeholder="CI" runat="server" />
                                      </div>
                                    </div>
                              </div>
                            </div>
                            <!--fin tab 1 -->


                            <div class="tab-pane" id="account">
                                <div class="row">
                                    
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>DIRECCIÓN</label>
                                        <asp:TextBox type="text" name="txtDireccion" id="txtDireccion" class="form-control" placeholder="Dirección" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>MUNICIPIO</label>
                                        <asp:TextBox type="text" name="txtMunicipio" id="txtMunicipio" class="form-control" placeholder="Municipio" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>FECHA DE NACIMIENTO</label>
                                        <asp:TextBox TextMode="Date" name="txtFechaNacimiento" id="txtFechaNacimiento" class="form-control" runat="server" />
                                      </div>
                                    </div>
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>Historial medico</label>
                                        <asp:TextBox name="txtHistorial" ID="txtHistorial" runat="server" TextMode="MultiLine" Rows="3" class="form-control"></asp:TextBox>
                                      </div>
                                    </div>
                                </div>
                            </div>
                            <!--fin tab 2 -->

                            <div class="tab-pane" id="new-step">
                              <div class="row">
                                  
                                  <div class="col-sm-10 col-sm-offset-1">
                                    <div class="form-group">
                                        <label>USUARIO</label>
                                         <asp:TextBox type="text" name="txtUsuario" id="txtUsuario" class="form-control" placeholder="Usuario" runat="server" />
                                    </div>
                                  </div>
                                  <div class="col-sm-10 col-sm-offset-1">
                                    <div class="form-group">
                                      <label>CONTRASEÑA</label>
                                       <asp:TextBox TextMode="password" name="txtContrasena" id="txtContrasena" class="form-control" runat="server" />
                                    </div>
                                  </div>
                                  <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                          <label>CORREO ELECTRONICO</label>
                                          <asp:TextBox type="email" name="txtCorreo" id="txtCorreo" class="form-control" placeholder="Correo Electronico" runat="server" />
                                      </div>
                                  </div>
                              </div>
                          </div>

                          <!--fin tab 3 -->

                          <div class="tab-pane" id="new-step2">
                            <div class="row">

                                <div class="col-sm-10 col-sm-offset-1">
                                <div class="form-group">

                                  <div class="container_subImg">
                                    <asp:FileUpload name="fileUpload"  type="file" id="fileUpload" accept="image/*" class="real-file" runat="server"/>
                                    <div class="img-area" data-img="">
                                      <i class='bx bxs-cloud-upload icon'></i>
                                      <h3>Sube tu fotografia</h3>
                                      <p>El tamaño maximo aceptado es <span>2MB</span></p>
                                    </div>
                                    <button type="button" class="select-image">Seleciona una imagen</button>
                                  </div>
                                    
                                </div>
                              </div>  


                                
                                                          
                            </div>
                        </div>

                        <!--fin tab 4 -->


                            <div class="tab-pane" id="address">
                                <div class="row">
                                    <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                        <label>MARCA TU UBICACIÓN</label>
                                        <div class="box-map" id="ModalMapPreview"></div>
                                      </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-10 col-sm-offset-1">
                            <div class="wizard-footer height-wizard">
                                <div class="pull-right">
                                    <input type='button' class='btn btn-next btn-fill btn-warning btn-wd btn-sm siguiente' name='next' value='Siguiente'/>
                                     <asp:Button name='finish' ID="btnRegistrar" runat="server" Text="Registrarse" class='btn btn-finish btn-fill btn-warning btn-wd btn-sm' OnClick="btnRegistrar_Click" />
                                </div>

                                <div class="pull-left">
                                    <input type='button' class='btn btn-previous btn-fill btn-default btn-wd btn-sm' name='previous' value='Atrás' />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <br>
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
                </div>
            </div> 
        </div>
    </div>
        <div class="toggleWrapper">
      <input  type="checkbox" class="dn" id="dn"/>
      <label for="dn" class="toggle">
        <span class="toggle__handler">
        </span>
      </label>
    </div>
</div> 
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

        //  API de Google Maps utilizando la función 
        $(document).ready(function () {
            var script = document.createElement('script');
            script.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyDu4hP-gLcmJIsRfg9Lm8yrU58UC_IWKuw&libraries=places&callback=initMap';
            document.body.appendChild(script);
        });

    </script>
    <!--   Core JS Files   -->
	<script src="assets/js/jquery-2.2.4.min.js" type="text/javascript"></script>
	<script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
	<script src="assets/js/jquery.bootstrap.wizard.js" type="text/javascript"></script>

	<!--  Plugin for the Wizard -->
	<script src="assets/js/gsdk-bootstrap-wizard2.js"></script>
	<script src="assets/js/jquery.validate.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(window).load(function () {
                $(".cargando").fadeOut(1000);
            });
        });
    </script>
</body>
</html>
