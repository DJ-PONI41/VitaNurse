<%@ Page Title="" Language="C#" MasterPageFile="~/Nurse_home.Master" AutoEventWireup="true" CodeBehind="Listado_Solicitudes_nurse.aspx.cs" Inherits="NurseProjectWEB.Listado_Solicitudes_nurse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <main class="table">
            <section class="table__header">
                <h1>Solicitudes Pacientes</h1>
            </section>
            <section class="table__body">
                <table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Paciente</th>
                            <th>Municipio</th>
                            <th>Fecha solicitud</th>
                            <th>Estado</th>
                            <th>Más información</th>                     
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                
                    </tbody>
                </table>
            </section>
        </main>
    </form>
</asp:Content>
