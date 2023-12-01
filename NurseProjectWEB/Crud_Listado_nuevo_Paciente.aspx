<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Crud_Listado_nuevo_Paciente.aspx.cs" Inherits="NurseProjectWEB.Crud_Listado_nuevo_Paciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="table">
            <section class="table__header">
                <h1>Listado de Pacientes</h1>
                <form id="form1" runat="server">
                    <asp:Button ID="btnAceptar" runat="server" Text="Nuevo Registro" Visible="true" class='btnMasInformacion' OnClick="Registrar_nuevo_paciente_Click"/>
                </form>
                
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
                        <th>Editar</th>
                        <th>Eliminar</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                    </tbody>
                </table>
            </section>
        </main>
</asp:Content>
