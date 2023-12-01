<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NurseReport.aspx.cs" Inherits="NurseProjectWEB.NurseReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Css/style.css" />
    <link rel="stylesheet" href="Css/Style_Registrar_Paciente.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <main class="table">
            <section class="table__header">
                <h1>Reporte Paciente</h1>
                <div class="input-group">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Style="display: none" />
                    <input runat="server" id="Cuadro_busqueda" type="search" placeholder="Buscar nombre" onkeydown="buscarConEnter(event)" />
                    <img src="Images/search.png" alt="" />
                </div>
                <div class="export__file">
                    <label for="export-file" class="export__file-btn" title="Export File"></label>
                    <input type="checkbox" id="export-file" />
                    <div class="export__file-options">
                        <label>Exportar a &nbsp; &#10140;</label>
                        <asp:Button ID="btnExportPDF" runat="server" Text="PDF" OnClick="btnExportPDF_Click" Style="display: none" />
                        <label for="btnExportPDF" id="toPDF">
                            Exportar a PDF
                            <img src="Images/pdf.png" alt="" /></label>
                    </div>
                </div>
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>id </th>
                            <th>Nombre </th>
                            <th>Apellido Paterno </th>
                            <th>Apellido Materno </th>
                            <th>Fecha de nacimiento </th>
                            <th>Celular </th>
                            <th>CI </th>
                            <th>Correo </th>
                            <th>Direccion </th>
                            <th>Rol </th>
                            <th>Especialidad </th>
                            <th>Año de Titulacion </th>
                            <th>Titulo Profesional </th>
                            <th>CV </th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                    </tbody>
                </table>
            </section>


            <div id="input-box-fecha-titulacion" class="input-box">
                <label class="details" for="txtTitulacion">Fecha de Titulacion</label>
                <asp:TextBox ID="txtInicio" CssClass="form-control" TextMode="Date" runat="server" />
            </div>

            <div id="input-box-fecha-titulacion" class="input-box">
                <label class="details" for="txtTitulacion">Fecha de Titulacion</label>
                <asp:TextBox ID="txtFin" CssClass="form-control" TextMode="Date" runat="server" />
            </div>

            <asp:Button ID="btnFecha" runat="server" Text="PDF" Style="display: none" OnClick="btnFecha_Click"/>

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

        </main>
    </form>

    <script type="text/javascript">
        function buscarConEnter(event) {
            if (event.key === "Enter") {
            <%= Page.ClientScript.GetPostBackEventReference(btnBuscar, "") %>;
            }
        }
    </script>
</body>
</html>
