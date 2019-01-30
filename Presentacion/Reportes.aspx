<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reportes.aspx.cs" Inherits="Reportes" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2.Export, Version=10.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no"/>
    <title>DEC - Reportes</title>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Catamaran" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Permanent+Marker" rel="stylesheet">
    <link href="css/materialize.min.css" type="text/css" rel="stylesheet" media="screen,projection"/>
    <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection"/>
    <style type="text/css">
        .auto-style1 {
            margin-right: 0px;
        }
        .auto-style2 {
            margin-left: 160px;
        }
        </style>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <h5 class="blue-grey-text">
            &nbsp;&nbsp;&nbsp;
            <asp:Label class="blue-grey-text" ID="lblTituloReporte" runat="server" Text="Label" Font-Bold="False"  Font-Names="Cambria" Font-Overline="False" Font-Size="X-Large" Font-Underline="False" ForeColor="#666666"></asp:Label>
        </h5>
        <br />

    </div>
         <div class="auto-style2">
             <asp:GridView ID="gviResultadoReporte" runat="server"                                                                                                                     
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" ForeColor="Black" CssClass="auto-style1" Height="262px" Width="803px">
                 <AlternatingRowStyle BackColor="PaleGoldenrod" />
                 <FooterStyle BackColor="Tan" />
                 <HeaderStyle BackColor="Tan" Font-Bold="True" />
                 <SortedAscendingCellStyle BackColor="#FAFAE7" />
                 <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                 <SortedDescendingCellStyle BackColor="#E1DB9C" />
                 <SortedDescendingHeaderStyle BackColor="#C2A47B" />
             </asp:GridView>
        </div>
    </form>
</body>
</html>
