<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reporte_detalle.aspx.cs" Inherits="reporte_detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabs" runat="Server">
    <div class="tabs-container">
        <div class="container-fluid">
            <div class="row center">
                <ul id="tabs-swipe-no" class="tabs">
                    <li class="tab col s1" id="btnAdministrador"><a href="#" id="aswipe1">ADMINISTRADOR</a></li>
                    <li class="tab col s1" id="btnReportes"><a class="active" href="#swipe2" id="aswipe2">REPORTES</a></li>
                    <li class="tab col s1" id="btnDashboard"><a href="#" id="aswipe3">DASHBOARD</a></li>
                    <li class="tab col s1" id="btnSalir"><a href="#">SALIR</a></li>

                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContainer" runat="Server">
    <div id="swipe2" class="swipe-fluid">
        <div class="row center">
            <div class="top-banner">
                <div class="top-banner-10 top-banner-inner"></div>
            </div>
        </div>
        <div class="row">
            <div class="container container800">
                <div class="row">
                    <asp:Label CssClass="text" ID="lblTituloReporte" runat="server" Text=""></asp:Label>
                </div>
                <div class="row center">
                    <div style="overflow: auto;">
                        <table width="95%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gviResultadoReporte" runat="server" CssClass="striped" CellPadding="0">

                                        <FooterStyle BackColor="Tan" />
                                        <HeaderStyle Font-Bold="True" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="bottom-buttons">
                        <asp:HyperLink ID="btnVolver" CssClass="waves-effect waves-light btn" runat="server">VOLVER</asp:HyperLink>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <br />





</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
    <script src="js/menu.js"></script>
</asp:Content>

