<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="reporte.aspx.cs" Inherits="reporte" %>

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

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="progress">
                <div class="indeterminate"></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="formUpdatePanel" runat="server">

        <ContentTemplate>

            <!-- contenido tab Reportes-->
            <div id="swipe2" class="swipe-fluid">
                <div class="row center">
                    <div class="top-banner">
                        <div class="top-banner-10 top-banner-inner"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="container container800">
                        <div class="row">
                            <h5 class="text">¡Bienvenido al Modulo Reportes!</h5>
                            <p>A continuación se listarán los reportes a realizar:</p>

                            <div class="row">
                                <p>Empresa:</p>
                                <asp:DropDownList ID="ddlEmpresa" runat="server" Style="display: inline;" AutoPostBack="true" Width="363px" OnSelectedIndexChanged="gviCargarReporte">
                                </asp:DropDownList>
                            </div>

                            <asp:HyperLink Visible="false" ID="btnSemaforo" runat="server" CssClass="waves-effect waves-light btn-large"><i class="material-icons right">traffic</i>Ver Semaforo</asp:HyperLink>

                        </div>
                        <div class="row center">
                            <div style="overflow: auto;">
                                <table width="95%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gviReporte" runat="server" CssClass="striped"
                                                AutoGenerateColumns="False" DataKeyNames="repId" GridLines="Vertical"
                                                OnRowCommand="gvi_RowCommandReporte" OnRowDataBound="gvi_RowDataBoundReporte"
                                                CellPadding="0">
                                                <Columns>
                                                    <asp:BoundField DataField="empId" HeaderText="Id" Visible="False" />
                                                    <asp:BoundField DataField="empNombre" HeaderText="Empresa" SortExpression="empNombre" />
                                                    <asp:BoundField DataField="repId" HeaderText="Id" Visible="False" />
                                                    <asp:BoundField DataField="repDescripcion" HeaderText="Reporte" SortExpression="repDescripcion" />
                                                    <asp:BoundField DataField="repArchivo" HeaderText="Archivo" SortExpression="repArchivo" Visible="false" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgSeleccionar" CausesValidation="false" runat="server" CommandArgument='<%# Eval("repId") + ";" + Eval("empId") + ";" + Eval("repArchivo") + ";" + Eval("repDescripcion") %>'
                                                                CommandName="Seleccionar" ToolTip="Ver Reporte" />
                                                            <asp:ImageButton ID="imgVer" CausesValidation="false" runat="server" CommandArgument='<%# Eval("repId") + ";" + Eval("empId") + ";" + Eval("repArchivo") + ";" + Eval("repDescripcion")  %>'
                                                                CommandName="Ver" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <span>Vacio.</span>
                                                </EmptyDataTemplate>
                                                <HeaderStyle Font-Bold="True" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
    <script src="js/menu.js"></script>

</asp:Content>

