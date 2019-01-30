<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTabs" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphContainer" runat="Server">
    <div id="swipe1" class="swipe-fluid" runat="server">
        <div class="row center">
            <div class="top-banner">
                <div class="top-banner-14 top-banner-inner"></div>
            </div>
        </div>

        <div class="row">
            <div class="container container800">
                <fieldset id="fldPanelLogin" runat="server" class="col s4 offset-s4 left-align">
                    <legend>Ingreso sistema</legend>

                    <dl>
                        <dt>
                            <label for="UserName">Usuario:</label></dt>
                        <dd>
                            <input id="UserName" runat="server" class="left" name="user" type="text" />
                        </dd>
                        <dt>
                            <label for="UserPassword">Password:</label></dt>
                        <dd>
                            <input id="UserPassword" runat="server" class="left" name="password" type="password" />
                        </dd>
                        <div class="center-align">
                            <label id="Msg" runat="server" visible="false" style="color: red !important; font-weight: bold; font-size: 15px; padding-bottom: 20px"></label>
                        </div>
                    </dl>

                    <div class="bottom-buttons center-align">
                        <a class="waves-effect waves-light btn" id="btnIniciarSesion" runat="server" onserverclick="btnIniciarSesion_ServerClick" style="margin-bottom: 30px !important">Ingresar </a>
                    </div>

                </fieldset>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>

