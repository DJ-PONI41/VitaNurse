<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudReport.aspx.cs" Inherits="NurseProjectWEB.SolicitudReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="Css/style.css" />
    <link rel="stylesheet" href="Css/Style_Registrar_Paciente.css" />
</head>
<body>
    <form id="form1" runat="server">
        <main class="table">
            <section class="table__header">
                <h1>Reporte Solicitudes</h1>
                <div class="input-group">
                    <%--<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Style="display: none" />--%>
                    <input runat="server" id="Cuadro_busqueda" type="search" placeholder="Buscar nombre" onkeydown="buscarConEnter(event)" />
                    <img src="Images/search.png" alt="" />
                </div>

                <div class="export__file">
                    <asp:Button ID="Reload_table" runat="server" Text="Reload" OnClick="btnRecargar_tabla_Click" style="display:none" />
                    <label for="Reload_table" class="export__file-btn3" title="Recargar"></label>
                </div>

                <div class="export__file">
                    <label for="Type_filtr" class="export__file-btn2" title="Tipos de filtro"></label>
                    <input type="checkbox" id="Type_filtr"/>
                    <div class="export__file-options">
                        <label>Filtrar por &nbsp; &#10140;</label>
                        <asp:Button ID="Filt_acept" runat="server" Text="Aceptados" OnClick="Filt_acept_Click" style="display:none" />
                        <label for="Filt_acept" id="toAcept">Aceptados <img src="Images/icons/Icon clipboard check.png" alt=""/></label>

                        <asp:Button ID="Filt_Rech" runat="server" Text="Aceptados" OnClick="Filt_Rech_Click" style="display:none" />
                        <label for="Filt_Rech" id="toRecha">Rechazados <img src="Images/icons/Icon assignment late.png" alt=""/></label>

                        <asp:Button ID="Filt_Pend" runat="server" Text="Aceptados" OnClick="Filt_Pend_Click" style="display:none" />
                        <label for="Filt_Pend" id="toPend">Pendientes <img src="Images/icons/Icon pending actions.png" alt=""/></label>
                    </div>
                </div>

                <div class="export__file">
                    <label for="export-file" class="export__file-btn" title="Export File"></label>
                    <input type="checkbox" id="export-file" />
                    <div class="export__file-options">
                        <label>Exportar a &nbsp; &#10140;</label>
                        <asp:Button ID="btnExportPDF" runat="server" Text="PDF" OnClick="btnExportPDF_Click" Style="display: none" />
                        <asp:Label runat="server" for="btnExportPDF" id="ExportPDF" CssClass="lab_style" onclick="SimulateButtonClick()">Exportar a PDF<img src="Images/pdf.png" alt="" /></asp:Label>

                        <asp:Button ID="btnExportPDFA" runat="server" Text="PDF" OnClick="btnExportPDFA_Click" Style="display: none" />
                        <asp:Label runat="server" Visible="false" for="btnExportPDFA" id="ExportA" CssClass="lab_style" onclick="SimulateButtonClick2()">Exportar a PDF<img src="Images/pdf.png" alt="" /></asp:Label>

                        <asp:Button ID="btnExportPDFR" runat="server" Text="PDF" OnClick="btnExportPDFR_Click" Style="display: none" />
                        <asp:Label runat="server" Visible="false" for="btnExportPDFR" id="ExportR" CssClass="lab_style" onclick="SimulateButtonClick3()">Exportar a PDF<img src="Images/pdf.png" alt="" /></asp:Label>

                        <asp:Button ID="btnExportPDFP" runat="server" Text="PDF" OnClick="btnExportPDFP_Click" Style="display: none" />
                        <asp:Label runat="server" Visible="false" for="btnExportPDFP" id="ExportP" CssClass="lab_style" onclick="SimulateButtonClick4()">Exportar a PDF<img src="Images/pdf.png" alt="" /></asp:Label>
                    </div>
                </div>
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>id </th>
                            <th>Nombre del Paciente </th>
                            <th>Nombre de la Enfermera </th>
                            <th>Detalles de Solicitud </th>
                            <th>Estado</th>
                            
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                    </tbody>
                </table>
            </section>
        </main>

    </form>
    <%--<script type="text/javascript">
        function buscarConEnter(event) {
            if (event.key === "Enter") {
            <%= Page.ClientScript.GetPostBackEventReference(btnBuscar, "") %>;
            }
        }
    </script>--%>

    <script type="text/javascript">
        function SimulateButtonClick() {
            // Obtener referencia al botón y simular clic en él
            var btnExportPDF = document.getElementById('<%= btnExportPDF.ClientID %>');
            btnExportPDF.click();
        }

        function SimulateButtonClick2() {
            // Obtener referencia al botón y simular clic en él
            var btnExportPDF = document.getElementById('<%= btnExportPDFA.ClientID %>');
            btnExportPDF.click();
        }

        function SimulateButtonClick3() {
            // Obtener referencia al botón y simular clic en él
            var btnExportPDF = document.getElementById('<%= btnExportPDFR.ClientID %>');
            btnExportPDF.click();
        }

        function SimulateButtonClick4() {
            // Obtener referencia al botón y simular clic en él
            var btnExportPDF = document.getElementById('<%= btnExportPDFP.ClientID %>');
            btnExportPDF.click();
        }
    </script>
</body>
</html>
