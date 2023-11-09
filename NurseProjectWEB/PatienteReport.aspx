<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatienteReport.aspx.cs" Inherits="NurseProjectWEB.PatienteReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Css/Style_Registrar_Paciente.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="container">
                <h1 class="text-center">Reporte de Paciente</h1>

                <div class="row">
                    <div class="form-group col-6">
                        <label for="txtNombre">Nombre</label>
                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click"/>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnDescargar" runat="server" Text="Descargar Reporte" CssClass="btn btn-primary" OnClick="btnDescargar_Click"/>
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
        </div>
    </form>
</body>
</html>
