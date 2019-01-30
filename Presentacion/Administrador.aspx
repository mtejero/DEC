<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="administrador.aspx.cs" Inherits="administrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabs" runat="Server">
    <div class="tabs-container">
        <div class="container-fluid">
            <div class="row center">
                <ul id="tabs-swipe-no" class="tabs">
                    <li class="tab col s1" id="btnAdministrador"><a class="active" href="#swipe1" id="aswipe1">ADMINISTRADOR</a></li>
                    <li class="tab col s1" id="btnReportes"><a href="#swipe2" id="aswipe2">REPORTES</a></li>
                    <li class="tab col s1" id="btnDashboard"><a href="#swipe3" id="aswipe3">DASHBOARD</a></li>
                    <li class="tab col s1" id="btnSalir"><a href="#">SALIR</a></li>

                </ul>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContainer" runat="Server">

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="progress">
                <div class="indeterminate"></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="formUpdatePanel" runat="server">

        <ContentTemplate>
            <!-- contenido tab Perfil-->
            <div id="swipe1" class="swipe-fluid">
                <div class="row center">
                    <div class="top-banner">
                        <div class="top-banner-12 top-banner-inner"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="container container800">
                        <div class="row">
                            <h5 class="text">¡Bienvenido al Modulo Administrador!</h5>
                            <p>A continuación se listarán las Empresas asociadas, puede realizar 2 acciones:</p>
                            <p>&nbsp;&nbsp;1) Descargar reporte crudo de Encuestas cargadas</p>
                            <p>&nbsp;&nbsp;2) Desactivar encuesta Empresa</p>

                        </div>
                        <div class="row center">
                            <div style="overflow: auto;">

                                <asp:GridView ID="gvi" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="empId"
                                    OnRowCommand="gvi_RowCommand" OnRowDataBound="gvi_RowDataBound"
                                    CssClass="striped">
                                    <Columns>
                                        <asp:BoundField DataField="empId" HeaderText="Id" Visible="False" />
                                        <asp:BoundField DataField="empNombre" HeaderText="Empresa" SortExpression="camNombre" />
                                        <asp:BoundField DataField="empDescripcion" HeaderText="Datos Acceso" SortExpression="camDescripcion" />
                                        <asp:BoundField DataField="Cant_Enc" HeaderText="Encuestados" SortExpression="Cant_Enc" />
                                        <asp:BoundField DataField="empActivo" HeaderText="Activo" Visible="False" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgSeleccionar" CausesValidation="false" runat="server" CommandArgument='<%# Eval("empId") %>'
                                                    CommandName="Seleccionar" ToolTip="Ver Reporte" />
                                                <asp:ImageButton ID="imgActivar" CausesValidation="false" runat="server" CommandArgument='<%# Eval("empId") %>'
                                                    CommandName="Activar" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                Opciones
                                            </HeaderTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span>Vacio.</span>
                                    </EmptyDataTemplate>


                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphFooter" runat="Server">
    <!-- Remember to include jQuery :) -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.0.0/jquery.min.js"></script>

    <!-- jQuery Modal -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.css" />
    <script src="js/menu.js"></script>
</asp:Content>

