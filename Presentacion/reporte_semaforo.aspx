<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reporte_semaforo.aspx.cs" Inherits="reporte_semaforo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
    <style type="text/css">
        /* Semáforo CSS */

        /* Semáforo CSS vertical */

        ul.semaforo {
            float: left;
            position: relative;
            width: 100px;
            margin: 0 50px;
            padding: 5px;
            list-style-type: none;
            border: 2px;
            background: black;
            border-radius: 5%;
            box-shadow: 0px 0px 0 10px rgba(0,0,0,.8);
        }

            ul.semaforo li {
                position: relative;
                width: 60px;
                height: 60px;
                border-radius: 50%;
                background: lightgrey;
                margin: 5px 15px;
                background: linear-gradient(top, rgba(187,187,187,1) 0%,rgba(119,119,119,1) 99%);
                box-shadow: inset 0 -5px 15px rgba(255,255,255,0.4), inset -2px -1px 40px rgba(0,0,0,0.4), 0 0 1px #000;
            }

                ul.semaforo li:after {
                    content: "";
                    width: 30px;
                    height: 15px;
                    position: absolute;
                    left: 20px;
                    top: 10px;
                    box-shadow: 0px 0 20px 35px rgba(20,20,20,.1);
                    border-radius: 30px / 15px;
                }

        .Rojo li:nth-of-type(1) {
            background: #ff0000;
        }

        .Amarillo li:nth-of-type(2) {
            background: #FFFF00;
        }

        .Verde li:nth-of-type(3) {
            background: #00ff00;
        }

        table, th, td {
            width: 200px !important;
            vertical-align: top !important;
            p;
        }
    </style>
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
                    <h5>
                        <asp:Label ID="lblTituloSemaforo" runat="server" Text="" CssClass="text"></asp:Label>
                    </h5>
                    <br />
                </div>

                <asp:Repeater ID="rptSemaforo" runat="server">
                    <ItemTemplate>

                        <div class="row">

                            <div class="container800">

                                <table cellpadding="0" cellspacing="0" width="500">
                                    <tr>
                                        <td>
                                            <ul class="semaforo <%#Eval("color") %>">
                                                <li></li>
                                                <li></li>
                                                <li></li>
                                            </ul>
                                        </td>
                                        <td align="left" valign="top">
                                            <table cellpadding="0" cellspacing="0" width="250">
                                                <tr>
                                                    <td><b>INDICADOR:</b>  <%#Eval("Momento") %></td>
                                                </tr>
                                                <tr>
                                                    <td><b>PROMEDIO:</b> <%#Eval("PromedioMomento") %>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                </table>




                            </div>


                        </div>



                    </ItemTemplate>
                </asp:Repeater>


                <div class="row center">
                    <div class="bottom-buttons">
                        <a class="waves-effect waves-light btn" href="javascript:window.history.back();">VOLVER</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
    <script src="js/menu.js"></script>
</asp:Content>

