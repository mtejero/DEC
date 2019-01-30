<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContainer" runat="Server">
    <!-- contenido tab Perfil-->
    <div id="swipe1" class="swipe-fluid" runat="server">
        <div class="row center">
            <div class="top-banner">
                <div class="top-banner-13 top-banner-inner"></div>
            </div>
        </div>
        <div class="row">
            <div class="container container800">
                <div class="row">
                    <h5 class="text">¡Oooops!</h5>
                    <br />


                    <asp:Label ID="msgError" runat="server" Text=""></asp:Label>

                    <p>Para consultas, contactese a: <a href="mailto:ynavarro@evoltis.com">Yanina Navarro (ynavarro@evoltis.com)</a> &nbsp;<b>¡Muchas gracias!</b></p>



                    <div class="bottom-buttons">
                        <a class="waves-effect waves-light btn" href="javascript:window.history.back();">VOLVER</a>
                    </div>

                </div>


            </div>
        </div>



    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>

