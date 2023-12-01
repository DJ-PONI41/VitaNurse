<%@ Page Title="" Language="C#" MasterPageFile="~/Formularios_registro.Master" AutoEventWireup="true" CodeBehind="Formulario_registro_enfermera.aspx.cs" Inherits="NurseProjectWEB.Formulario_registro_enfermera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <h1 class="text-center">Registro de Enfermera</h1>

            <div class="text-center" style="margin-top: 20px;">
                <label class="switch">
                    <a href="RegisterPaciente.aspx" class="btn btn-primary">Registro de Paciente</a>
                </label>

            </div>

            <div class="image-container set-full-height">

                 <div class="form-group" style="display: none;">
                        <label for="txtLat">Latitud</label>
                        <asp:TextBox ID="txtLat" Text="-17.33059869950836" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group" style="display: none;">
                        <label for="txtLong">Longitud</label>
                        <asp:TextBox ID="txtLong" Text="-66.22559118521447" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

    <div class="container">
        <div class="row">
        <div class="col-sm-8 col-sm-offset-2">

            <div class="wizard-container">
                <div class="card wizard-card" data-color="orange" id="wizardProfile">
                    	<div class="wizard-header">
                        	<h3> Registrar Nueva Enfermera </h3>
                    	</div>
                    <div class="form-group">
                        <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

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
                                </div>
                            </div>
                            <!--fin tab 2 -->

                            <div class="tab-pane" id="new-step">
                              <div class="row">
                                  <div class="col-sm-10 col-sm-offset-1">
                                      <div class="form-group">
                                          <label>CORREO ELECTRONICO</label>
                                          <asp:TextBox type="email" name="txtCorreo" id="txtCorreo" class="form-control" placeholder="Correo Electronico" runat="server" />
                                      </div>
                                  </div>
                                  <div class="col-sm-10 col-sm-offset-1">
                                    <div class="form-group">
                                        <label>ESPECIALIDAD</label>
                                         <asp:TextBox type="text" name="txtEspecialidad" id="txtEspecialidad" class="form-control" placeholder="Especialidad" runat="server" />
                                    </div>
                                  </div>
                                  <div class="col-sm-10 col-sm-offset-1">
                                    <div class="form-group">
                                      <label>FECHA DE TITULACION</label>
                                       <asp:TextBox TextMode="Date" name="txtTitulacion" id="txtTitulacion" class="form-control" runat="server" />
                                    </div>
                                  </div>
                              </div>
                          </div>

                          <!--fin tab 3 -->

                          <div class="tab-pane" id="new-step2">
                            <div class="row">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="form-group">
                                        <label>TITULO PROFESIONAL</label>
                                        <asp:FileUpload name="fileUpload_type" type="file" id="fileTitulo" class="real-file" runat="server"/>
                                        <button type="button" class="custom-butto" id="custom-button">Selecciona un Archivo</button>
                                        <span id="custom-text">Ningun archivo selecionado.</span>
                                        
                                    </div>
                                </div>    
                                <div class="col-sm-10 col-sm-offset-1">
                                  <div class="form-group">
                                      <label>SUBE TU CVC</label>
                                      <asp:FileUpload name="fileUpload_type2" type="file" id="fileCvc" class="real-file" runat="server"/>
                                      <button type="button" class="custom-butto" id="custom-button2">Selecciona un Archivo</button>
                                      <span id="custom-text2">Ningun archivo selecionado.</span>
                                      
                                  </div>
                              </div>
                              <div class="col-sm-10 col-sm-offset-1">
                                <div class="form-group">
                                    <label>Sube tu fotografia</label>
                                    <asp:FileUpload name="fileUpload_type3" type="file" id="fileUpload" class="real-file" runat="server"/>
                                    <button type="button" class="custom-butto" id="custom-button3">Selecciona un Archivo</button>
                                    <span id="custom-text3">Ningun archivo selecionado.</span>
                                    
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
                                    <input type='submit' class='btn btn-finish btn-fill btn-warning btn-wd btn-sm' name='finish' value='Fin' />
                                </div>

                                <div class="pull-left">
                                    <input type='button' class='btn btn-previous btn-fill btn-default btn-wd btn-sm' name='previous' value='Atrás' />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <br>
                            </div>
                        </div>
                </div>
            </div> 
        </div>
    </div>
</div> 
</div>
</asp:Content>
