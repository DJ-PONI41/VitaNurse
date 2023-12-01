<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatienteReport.aspx.cs" Inherits="NurseProjectWEB.PatienteReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="Css/style.css"/>
    <link rel="stylesheet" href="Css/Style_Registrar_Paciente.css" />
</head>
<body>
    <form id="form1" runat="server">
        <main class="table">
        <section class="table__header">
            <h1>Reporte Paciente</h1>
            <div class="input-group">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" style="display:none" />
                <input runat="server" id="Cuadro_busqueda" type="search" placeholder="Buscar nombre" onkeydown="buscarConEnter(event)" />
                <img src="Images/search.png" alt=""/>
            </div>
            <div class="export__file">
                <label for="export-file" class="export__file-btn" title="Export File"></label>
                <input type="checkbox" id="export-file"/>
                <div class="export__file-options">
                    <label>Exportar a &nbsp; &#10140;</label>
                    <asp:Button ID="btnExportPDF" runat="server" Text="PDF" OnClick="btnExportPDF_Click" style="display:none" />
                    <label for="btnExportPDF" id="toPDF">Exportar a PDF <img src="Images/pdf.png" alt=""/></label>
                </div>
            </div>
        </section>
        <section class="table__body">
            <table>
                <thead>
                    <tr>
                        <th> id </th>
                        <th> Nombre </th>
                        <th> Apellido Paterno </th>
                        <th> Apellido Materno </th>
                        <th> Fecha de nacimiento </th>
                        <th> Celular </th>
                        <th> CI </th>
                        <th> Correo </th>
                        <th> Direccion </th>
                        <th> Rol </th>
                        <th> Historial Medico </th>
                    </tr>
                </thead>
                <tbody id="tableBody" runat="server">
                </tbody>
            </table>
        </section>
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
