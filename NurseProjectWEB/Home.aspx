<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NurseProjectWEB.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="hero">

            <div style="height: 350px; overflow: hidden;" class="hero__waves"><svg viewBox="0 0 500 150" preserveAspectRatio="none" style="height: 100%; width: 100%;"><path d="M-1.12,1.50 C268.62,-3.44 355.52,312.34 588.03,-123.83 L500.00,150.00 L0.00,150.00 Z" style="stroke: none; fill: #fff;"></path></svg></div>
            <section class="hero__main container">
                <div class="hero__texts">
                    <h1 class="hero__title">Por Que Tu Bienestar Nos Importa</h1>
                    <p class="hero__subtitle">La pagina brinda la opcion a ser atendido por una enfermera desde la comodidad de tu hogar  </p>
                    <a href="Login.aspx" class="hero__cta" id="btn_ingres">Ingresar</a>
                    <%--<a href="#" class="hero__cta" id="btn_register" >Registrarse</a>--%>
                    <asp:Button ID="btn_register" runat="server" Text="Registrarse" class="hero__cta" OnClick="btn_register_Click"/>
                    <a href="Imf_service.html" class="hero__cta" id="btn_info">Información del servicio</a>
                </div>
                <figure class="hero__picture">
                    <img src="Images/Nuerses_svg_derecha.svg" class="hero__img">
                </figure>
            </section>
            
        </header>
</asp:Content>
