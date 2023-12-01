<%@ Page Title="" Language="C#" MasterPageFile="~/AdmMaster.Master" AutoEventWireup="true" CodeBehind="Listado_Crud_Nurse.aspx.cs" Inherits="NurseProjectWEB.Listado_Crud_Nurse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="table">
            <section class="table__header">
                <h1>Solicitudes de Enfermeras</h1>
                
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
                            <th>CV</th>
                            <th>Estado</th>
                            <th>Aceptar</th>
                            <th>Rechasar</th>
                        </tr>
                    </thead>
                    <tbody id="tableBody" runat="server">
                    </tbody>
                </table>
            </section>
        </main>
</asp:Content>
