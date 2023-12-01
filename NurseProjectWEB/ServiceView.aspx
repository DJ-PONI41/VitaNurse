<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceView.aspx.cs" Inherits="NurseProjectWEB.ServiceView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="Css/Style_Registrar_Enfermera.css" />
</head>
<body>
    <div class="container" style="width:80%; height:auto;">
        <div class="title">Servicios</div>
        <form id="form1" runat="server">
            <div>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive" style="max-height: 300px; overflow-y: auto; max-width: auto;">
                            <asp:GridView ID="GridDat" runat="server" CssClass="table table-borderless table-striped table-transparent table-hover">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        </div>
</body>
</html>
