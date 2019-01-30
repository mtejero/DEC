<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Semaforo_Reporte.aspx.cs" Inherits="Reporte" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGauges" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2.Export, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Linear" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Circular" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.State" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGauges.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGauges.Gauges.Digital" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxGauges.Gauges" Assembly="DevExpress.Web.ASPxGauges.v10.2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <title>DEC - Semaforo</title>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Catamaran" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Permanent+Marker" rel="stylesheet">
    <link href="css/materialize.min.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }

        .auto-style2 {
            height: 37px;
        }

        .auto-style6 {
            color: #607d8b;
            margin-left: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" target="_blank">
        <div>
            <div class="row">
                <div>
                    <h5 class="auto-style6">
                        <asp:Label class="blue-grey-text" ID="lblTituloSemaforo" runat="server" Text="Label" Font-Bold="False" Font-Names="Cambria" Font-Overline="False" Font-Size="X-Large" Font-Underline="False" ForeColor="#666666"></asp:Label>
                    </h5>
                    <br />

                </div>
                <p class="text">
                    <table cellpadding="0" cellspacing="0" width="850px">
                        <tr>
                            <td>
                                <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuesta" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                                    <Gauges>
                                        <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                            <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                        </dx:StateIndicatorGauge>
                                    </Gauges>
                                </dx:ASPxGaugeControl>
                            </td>
                            <td align="left">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="auto-style1"><b>INDICADOR:</b> &quot;MOMENTOS&quot;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarillo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerde" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                                    </tr>
                                    <tr>
                                        <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </p>
            </div>

        </div>
        <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.4.min.js"></script>
        <script type="text/javascript" src="js/materialize.min.js"></script>
        <h5 class="auto-style6" style="text-decoration: underline">&nbsp;<asp:Label class="blue-grey-text" ID="lblTituloSemaforo0" runat="server" Text="Detalle:" Font-Bold="False" Font-Names="Cambria" Font-Overline="False" Font-Size="X-Large" Font-Underline="False" ForeColor="#666666"></asp:Label>
        </h5>
        <p class="text">&nbsp;</p>
        <h5 class="text">&nbsp;Resultado Momento &quot;1-SEGMENTACION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaSeg" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;SEGMENTACION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoSeg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloSeg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeSeg" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>


        <h5 class="text">&nbsp;Resultado por Momento &quot;2-ATRACCION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaAtr" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;ATRACCION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoAtr" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloAtr" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeAtr" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>
        <h5 class="text">&nbsp;Resultado por Momento &quot;3-ADQUISICION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaAdq" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;ADQUISICION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoAdq" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloAdq" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeAdq" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>
        <h5 class="text">&nbsp;Resultado por Momento &quot;4-SERVICIO&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaSer" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;SERVICIO&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoSer" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloSer" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeSer" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>


        <h5 class="text">&nbsp;Resultado por Momento &quot;5-MAXIMIZACION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaMax" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;MAXIMIZACION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoMax" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloMax" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeMax" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>


        <h5 class="text">&nbsp;Resultado por Momento &quot;6-FIDELIZACION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaFid" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;FIDELIZACION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoFid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloFid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeFid" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>


        <h5 class="text">&nbsp;Resultado por Momento &quot;7-RETENCION&quot; con semaforo</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaRet" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;RETENCION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoRet" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloRet" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeRet" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>


        <h5 class="text">&nbsp;Resultado por Momento &quot;8-RECUPERACION&quot; con semaforo-</h5>
        <p class="text">
            <table cellpadding="0" cellspacing="0" width="850px">
                <tr>
                    <td>
                        <dx:ASPxGaugeControl ID="ASPxGaugeControlEncuestaRec" runat="server" AutoLayout="False" BackColor="White" Height="260px" Value="0" Width="260px">
                            <Gauges>
                                <dx:StateIndicatorGauge Bounds="6, 6, 248, 248" Name="Gauge0">
                                    <indicators>
                                            <dx:StateIndicatorComponent Center="124, 124" Name="stateIndicatorComponent4" Size="100, 200" StateIndex="0">
                                                <states>
                                                    <dx:IndicatorStateWeb Name="State1" ShapeType="TrafficLight1" />
                                                    <dx:IndicatorStateWeb Name="State2" ShapeType="TrafficLight2" />
                                                    <dx:IndicatorStateWeb Name="State3" ShapeType="TrafficLight3" />
                                                    <dx:IndicatorStateWeb Name="State4" ShapeType="TrafficLight4" />
                                                </states>
                                            </dx:StateIndicatorComponent>
                                        </indicators>
                                </dx:StateIndicatorGauge>
                            </Gauges>
                        </dx:ASPxGaugeControl>
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="auto-style1"><b>INDICADOR:</b> &quot;RECUPERACION&quot;</td>
                            </tr>
                            <tr>
                                <td class="auto-style2"><b>PROMEDIO ROJO:</b>&nbsp;<asp:Label ID="lblEncuestaRojoRec" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO AMARILLO:</b>&nbsp;<asp:Label ID="lblEncuestaAmarilloRec" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>PROMEDIO VERDE:</b>&nbsp;<asp:Label ID="lblEncuestaVerdeRec" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%--<td><b>TOTAL DATOS SEGMENTO:</b> <%=TotalSegmento%></td>--%>
                            </tr>
                            <tr>
                                <%--<td><b>NOMBRE SEGMENTO:</b> <%=NombreSegmento%></td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </p>
    </form>
</body>
</html>
