<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">

    <script src="js/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script src="js/highcharts.js" type="text/javascript"></script>
    <script src="js/modules/drilldown.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            margin-left: 40px;
        }

        .auto-style3 {
            color: #607d8b;
            margin-left: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabs" runat="Server">

    <div class="tabs-container">
        <div class="container-fluid">
            <div class="row center">
                <ul id="tabs-swipe-no" class="tabs">
                    <li class="tab col s1" id="btnAdministrador"><a href="#">ADMINISTRADOR</a></li>
                    <li class="tab col s1" id="btnReportes"><a href="#">REPORTES</a></li>
                    <li class="tab col s1" id="btnDashboard"><a class="active" href="swipe3" id="aswipe3">DASHBOARD</a></li>
                    <li class="tab col s1" id="btnSalir"><a href="#">SALIR</a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContainer" runat="Server">

    <div id="swipe3" class="swipe-fluid">
        <div class="row center">
            <div class="top-banner">
                <div class="top-banner-11 top-banner-inner"></div>
            </div>
        </div>
        <div class="row">
            <div class="container container800">
                <div class="row">
                    <h5 class="text">¡Bienvenido al Modulo Dashboard!</h5>
                    <p>
                        A continuación se listarán las empresas a gestionar:
                    </p>

                    <div class="row">
                        <p>Empresa:</p>
                        <asp:DropDownList ID="ddlEmpresaDash" runat="server" Style="display: inline;" AutoPostBack="true" Width="363px" OnSelectedIndexChanged="ddlEmpresaDash_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="container container800">
                <h5 class="text">
                    <asp:Label ID="lblTitulo" runat="server" Text="Gráficos Dashboard"></asp:Label>
                </h5>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="widget" id="divGrafico1" runat="server" visible="true">
                <div class="auto-style1">
                    <asp:RadioButtonList ID="rbGraficoGrupo1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbGraficoGrupo1_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="Torta">Gráfico de Torta</asp:ListItem>
                        <asp:ListItem Value="Barra">Gráfico de Barras</asp:ListItem>
                        <asp:ListItem Value="Columna">Gráfico de Columnas</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />
                    <div id="grafico1" runat="server">
                    </div>
                </div>
            </div>

            <div class="widget" id="divGrafico2" runat="server" visible="true">
                <div class="auto-style1">
                    <asp:RadioButtonList ID="rbGraficoGrupo2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbGraficoGrupo2_SelectedIndexChanged">
                        <asp:ListItem Value="Torta">Gráfico de Torta</asp:ListItem>
                        <asp:ListItem Value="Barra">Gráfico de Barras</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Columna">Gráfico de Columnas</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <br />

                    <div id="grafico2" runat="server">
                    </div>
                </div>
            </div>
        </div>


    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
    <script src="js/menu.js"></script>
</asp:Content>

