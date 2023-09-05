<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NurseProjectWEB.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="hero">

            <div style="height: 350px; overflow: hidden;" class="hero__waves"><svg viewBox="0 0 500 150" preserveAspectRatio="none" style="height: 100%; width: 100%;"><path d="M-130.92,-151.45 C179.46,391.30 322.80,4.47 503.38,3.48 L500.00,150.00 L0.00,150.00 Z" style="stroke: none; fill: #fff;"></path></svg></div>
            <section class="hero__main container">
                <section class="main">        
                    <div class="main__contact">
            
                        <h2 class="main__title">Inicio de Sesión</h2>
            
                        <form runat="server" class="main__form">
                        
                            
                            <asp:TextBox ID="txtUsuario" runat="server" class="main__input" placeholder="Usuario"></asp:TextBox>
                            <%--<input type="text" placeholder="Usuario" class="main__input">--%>
                            
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="main__input" placeholder="Contraseña"></asp:TextBox>
                       
                            <%--<input type="password" placeholder="Contraseña" class="main__input">--%>

                            <p class="main__paragraph">Olvido su contraseña?</p>
            
                            <%--<input type="submit" value="Ingresar" class="main__input main__input--send">--%>
                            <asp:Button ID="btnSignIn" runat="server" Text="Ingresar" class="main__input main__input--send" OnClick="btnSignIn_Click" />
                        </form>
            
                        
            
                    </div>            
                </section>            
                <figure class="hero__picture">
                    <img src="Images/Nuerses_svg_izquierda.svg" class="hero__img">
                </figure>
            </section>
            
        </header>
</asp:Content>
