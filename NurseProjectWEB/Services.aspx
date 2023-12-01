<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="NurseProjectWEB.Services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Css/Style_Registrar_Enfermera.css" />
</head>
<body>
    <div class="container" style="width: 80%; height: auto;">
        <div class="title">Servicios</div>
        <form id="form1" runat="server">
            <div>
                <table border="1" style="border-radius: 5px; border: 1px solid #E9EFF4; width: 80%; height: 70%; flex-shrink: 0; margin: auto; text-align: center; display: none;">
                    <tr>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Precio</th>
                    </tr>
                    <tr>
                        <td>Servicio de Inyecciones</td>
                        <td>Administración de medicamentos por vía intramuscular o subcutánea</td>
                        <td>30</td>
                    </tr>
                    <tr>
                        <td>Cambio de Vendajes</td>
                        <td>Cambio de apósitos en heridas para prevenir infecciones y promover la cicatrización.</td>
                        <td>20</td>
                    </tr>


                    <tr>
                        <td>Cuidado de Heridas Postoperatorias </td>
                        <td>Limpieza y curación de heridas quirúrgicas para prevenir complicaciones</td>
                        <td>40</td>
                    </tr>
                    <tr>
                        <td>Asistencia en la Administración de Medicamentos </td>
                        <td>Ayuda para tomar medicamentos según la prescripción del médico</td>
                        <td>20</td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes con Enfermedades Crónicas </td>
                        <td>Monitoreo y apoyo para pacientes con condiciones médicas crónicas</td>
                        <td>varia de acuerdo al tipo de paciente</td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes Postoperatorios</td>
                        <td>Asistencia y cuidado para pacientes después de una cirugía</td>
                        <td>varia de acuerdo al tipo de paciente </td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes Paliativos </td>
                        <td>Brindar confort y apoyo a pacientes en cuidados paliativos y a sus familias</td>
                        <td>varia de acuerdo al tipo de paciente</td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes con Trastornos Respiratorios</td>
                        <td>Asistencia a pacientes con trastornos respiratorios, como el asma o la enfermedad pulmonar obstructiva crónica</td>
                        <td>80</td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes en General</td>
                        <td>atención integral y personalizada a pacientes de todas las edades y condiciones de salud</td>
                        <td>varia de acuerod al tipo de paciene</td>
                    </tr>
                    <tr>
                        <td>Cuidado de Pacientes con Enfermedades Transmisible </td>
                        <td>Cuidado de Pacientes con Enfermedades Transmisibles</td>
                        <td>80</td>
                    </tr>

                </table>

            </div>
            <div id="ServiceForm">
                <div id="input-box-ci" class="input-box">
                    <label class="details" for="txtName">Nombre</label>
                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" />
                </div>
                <div id="input-box-description" class="input-box">
                    <label class="details" for="txtDescription">Descripcion</label>
                    <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" />
                </div>
                <div id="input-box-price" class="input-box">
                    <label class="details" for="txtPrice">Precio</label>
                    <asp:TextBox ID="txtPrice" CssClass="form-control" runat="server" />
                </div>

                <div class="button, btn-group">
                    <asp:Button ID="btnAtras" Visible="false" runat="server" Text="Volver" OnClick="btnAtras_Click" />
                    <asp:Button ID="btnRegistrar" runat="server" Visible="true" Text="Registrar Servicio" OnClick="btnRegistrar_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Modificar" Visible="false" Style="color: white; background-color: #dec521;" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                </div>
                <div class="form-group">
                    <asp:Label ID="label1" CssClass="alert alert-success" runat="server" Style="display: none;"></asp:Label>

                </div>
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
    <script>
        function ConfirmDelete() {
            return confirm('¿Estás seguro de que deseas eliminar este elemento?');
        }
        function ocultarForm() {
            document.getElementById("ServiceForm").style.display = "none";
        }

    </script>
</body>
</html>
