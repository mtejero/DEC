<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Encuesta.aspx.cs" Inherits="Encuesta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <title>DEC - Encuesta</title>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Catamaran" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Permanent+Marker" rel="stylesheet">
    <link href="css/materialize.min.css" type="text/css" rel="stylesheet" media="screen,projection" />
    <link href="css/style.css" type="text/css" rel="stylesheet" media="screen,projection" />
</head>
<body>
    <div class="top-logo">
        <img class="logo-image" src="images/topheader3lines.jpg">
    </div>
    <div class="container logo-container">
        <img class="logo-image" src="images/logo.png">
    </div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" ScriptMode="Release" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" AsyncPostBackTimeout="9000">
        </asp:ScriptManager>

        <style type="text/css">
            .modalBackground {
                background-color: Gray;
                filter: alpha(opacity=50);
                opacity: 0.5;
            }
        </style>
        <!-- start: PAGE CONTENT -->
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="alert alert-info">
                    Cargando, por favor espere...
                    <img width="220px" height="19px" src="img/activity.gif" alt="Cargando..." />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="formUpdatePanel" runat="server">
            <ContentTemplate>
                <div id="swipeable">
                    <div class="tabs-container">
                        <div class="container-fluid">
                            <div class="row center">
                                <ul id="tabs-swipe-no" class="tabs">
                                    <li class="tab col s1 tabsEncuesta"><a class="active" href="#" id="aswipe1" runat="server">Perfil</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe2" runat="server">Segmentar</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe3" runat="server">Atraer</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe4" runat="server">Adquirir</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe5" runat="server">Atender</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe6" runat="server">Maximizar</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe7" runat="server">Fidelizar</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe8" runat="server">Retener</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe9" runat="server">Recuperar</a></li>
                                    <li class="tab col s1 tabsEncuesta"><a href="#" id="aswipe10" runat="server">Anexo</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Perfil-->
                    <div id="swipe1" class="swipe-fluid" runat="server">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-00 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row">

                                    <asp:Image ID="imgLogo" runat="server" CssClass="logo-encuesta" />
                                    <h4 class="text">¡Bienvenido!</h4>

                                    <p><b>En este momento usted participará de un proceso de medición de las capacidades que tiene nuestra  Empresa, para mejorar la <u>Experiencia del Cliente</u>.</b></p>
                                    <p><b>El resultado de este diagnóstico nos permitirá conocer nuestras fortalezas y oportunidades de mejora, con el objetivo de potenciar nuestras capacidades y que la experiencia de nuestros clientes se vuelva un diferencial de nuestra Empresa.</b></p>
                                    <p><b>Su participación es de un gran valor. MUCHAS GRACIAS</b></p>

                                </div>

                                <div class="row">
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">business</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlDepartamento" runat="server" Style="display: inline;" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">home</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlSubDepartamento" runat="server" Style="display: inline;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">star</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlAntiguedad" runat="server" Style="display: inline;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">restore</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlEdad" runat="server" Style="display: inline;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">swap_calls</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlNivel" runat="server" Style="display: inline;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">work</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <asp:DropDownList ID="ddlNivelEstudios" runat="server" Style="display: inline;">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col l6 s12 perfil-campo">

                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">perm_identity</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <p class="input-field">¿Ha recibido algún tipo de formación específica en Experiencia de Cliente?</p>
                                        </div>
                                        <div class="input-field col s11 offset-s1">
                                            <asp:RadioButton ID="chk_formacionsi" CssClass="with-ga checkboxes" runat="server" GroupName="formacion" Text="Si" />
                                            <asp:RadioButton ID="chk_formacionno" CssClass="with-ga checkboxes" runat="server" GroupName="formacion" Text="No" />
                                        </div>

                                    </div>
                                    <div class="col l6 s12 perfil-campo">
                                        <div class="input-field col s1">
                                            <i class="small_22 material-icons">assignment_ind</i>
                                        </div>
                                        <div class="input-field col s11">
                                            <p class="input-field">Sexo</p>
                                        </div>
                                        <div class="input-field col s11 offset-s1">
                                            <asp:RadioButton ID="chk_sexomasculino" CssClass="with-ga checkboxes" runat="server" GroupName="sexo" Text="Masculino" />
                                            <asp:RadioButton ID="chk_sexofemenino" CssClass="with-ga checkboxes" runat="server" GroupName="sexo" Text="Femenino" />
                                        </div>
                                    </div>


                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP1" runat="server" onserverclick="btnSiguienteP1_click">Siguiente</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Segmentar-->
                    <div id="swipe2" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-01 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>1_</b> Contamos con herramientas que permiten la identificación individual de nuestros clientes y su historial.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group11" type="radio" id="group111" runat="server" />
                                            <label for="group111">Siempre</label>
                                            <input class="with-gap" name="group11" type="radio" id="group112" runat="server" />
                                            <label for="group112">Muchas veces</label>
                                            <input class="with-gap" name="group11" type="radio" id="group113" runat="server" />
                                            <label for="group113">Pocas veces</label>
                                            <input class="with-gap" name="group11" type="radio" id="group114" runat="server" />
                                            <label for="group114">Casi Nunca</label>
                                            <input class="with-gap" name="group11" type="radio" id="group115" runat="server" />
                                            <label for="group115">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>2_</b> Incorporamos procesos analíticos  para conocer a nuestros cliente, sus comportamientos, gustos y preferencias</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group12" type="radio" id="group121" runat="server" />
                                            <label for="group121">Siempre</label>
                                            <input class="with-gap" name="group12" type="radio" id="group122" runat="server" />
                                            <label for="group122">Muchas veces</label>
                                            <input class="with-gap" name="group12" type="radio" id="group123" runat="server" />
                                            <label for="group123">Pocas veces</label>
                                            <input class="with-gap" name="group12" type="radio" id="group124" runat="server" />
                                            <label for="group124">Casi Nunca</label>
                                            <input class="with-gap" name="group12" type="radio" id="group125" runat="server" />
                                            <label for="group125">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>3_</b> Buscamos continuamente  nuevas formas de vincularnos con los clientes y generar nuevas oportunidades</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group13" type="radio" id="group131" runat="server" />
                                            <label for="group131">Siempre</label>
                                            <input class="with-gap" name="group13" type="radio" id="group132" runat="server" />
                                            <label for="group132">Muchas veces</label>
                                            <input class="with-gap" name="group13" type="radio" id="group133" runat="server" />
                                            <label for="group133">Pocas veces</label>
                                            <input class="with-gap" name="group13" type="radio" id="group134" runat="server" />
                                            <label for="group134">Casi Nunca</label>
                                            <input class="with-gap" name="group13" type="radio" id="group135" runat="server" />
                                            <label for="group135">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>4_</b>Analizamos las oportunidades de las últimas tendencias del mercado y estudiamos la evolución en relación al servicio al cliente</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group14" type="radio" id="group141" runat="server" />
                                            <label for="group141">Siempre</label>
                                            <input class="with-gap" name="group14" type="radio" id="group142" runat="server" />
                                            <label for="group142">Muchas veces</label>
                                            <input class="with-gap" name="group14" type="radio" id="group143" runat="server" />
                                            <label for="group143">Pocas veces</label>
                                            <input class="with-gap" name="group14" type="radio" id="group144" runat="server" />
                                            <label for="group144">Casi Nunca</label>
                                            <input class="with-gap" name="group14" type="radio" id="group145" runat="server" />
                                            <label for="group145">Nunca</label>
                                        </p>

                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP2" runat="server" onserverclick="btnSiguienteP2_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP2" runat="server" onserverclick="btnAnteriorP2_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Atraer-->
                    <div id="swipe3" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-02 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>1_</b>Hemos desarrollado diferencias relevantes (que pueden ser consideradas ventajas competitivas) frente a otros competidores a la hora de proporcionar nuestro servicio que son transmitidas de manera clara y convincente</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group21" type="radio" id="group211" runat="server" />
                                            <label for="group211">Siempre</label>
                                            <input class="with-gap" name="group21" type="radio" id="group212" runat="server" />
                                            <label for="group212">Muchas veces</label>
                                            <input class="with-gap" name="group21" type="radio" id="group213" runat="server" />
                                            <label for="group213">Pocas veces</label>
                                            <input class="with-gap" name="group21" type="radio" id="group214" runat="server" />
                                            <label for="group214">Casi Nunca</label>
                                            <input class="with-gap" name="group21" type="radio" id="group215" runat="server" />
                                            <label for="group215">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>2_</b>Nuestra empresa realiza acciones de marketing para el  posicionamiento de la marca mostrando los beneficios de nuestra oferta</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group22" type="radio" id="group221" runat="server" />
                                            <label for="group221">Siempre</label>
                                            <input class="with-gap" name="group22" type="radio" id="group222" runat="server" />
                                            <label for="group222">Muchas veces</label>
                                            <input class="with-gap" name="group22" type="radio" id="group223" runat="server" />
                                            <label for="group223">Pocas veces</label>
                                            <input class="with-gap" name="group22" type="radio" id="group224" runat="server" />
                                            <label for="group224">Casi Nunca</label>
                                            <input class="with-gap" name="group22" type="radio" id="group225" runat="server" />
                                            <label for="group225">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>3_</b> Somos innovadores a la hora de implementar acciones que mejoran nuestra oferta</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group23" type="radio" id="group231" runat="server" />
                                            <label for="group231">Siempre</label>
                                            <input class="with-gap" name="group23" type="radio" id="group232" runat="server" />
                                            <label for="group232">Muchas veces</label>
                                            <input class="with-gap" name="group23" type="radio" id="group233" runat="server" />
                                            <label for="group233">Pocas veces</label>
                                            <input class="with-gap" name="group23" type="radio" id="group234" runat="server" />
                                            <label for="group234">Casi Nunca</label>
                                            <input class="with-gap" name="group23" type="radio" id="group235" runat="server" />
                                            <label for="group235">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>4_</b>Entendemos con claridad que Experiencia de Cliente es poner al cliente en el centro del negocio, hacerlo sentir único, sorprenderlo</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group24" type="radio" id="group241" runat="server" />
                                            <label for="group241">Siempre</label>
                                            <input class="with-gap" name="group24" type="radio" id="group242" runat="server" />
                                            <label for="group242">Muchas veces</label>
                                            <input class="with-gap" name="group24" type="radio" id="group243" runat="server" />
                                            <label for="group243">Pocas veces</label>
                                            <input class="with-gap" name="group24" type="radio" id="group244" runat="server" />
                                            <label for="group244">Casi Nunca</label>
                                            <input class="with-gap" name="group24" type="radio" id="group245" runat="server" />
                                            <label for="group245">Nunca</label>
                                        </p>

                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP3" runat="server" onserverclick="btnSiguienteP3_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP3" runat="server" onserverclick="btnAnteriorP3_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                    <!-- contenido tab Adquirir-->
                    <div id="swipe4" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-03 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>1_</b> Nuestros clientes reconocen nuestros productos y servicios como soluciones de alto valor agregado.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group31" type="radio" id="group311" runat="server" />
                                            <label for="group311">Siempre</label>
                                            <input class="with-gap" name="group31" type="radio" id="group312" runat="server" />
                                            <label for="group312">Muchas veces</label>
                                            <input class="with-gap" name="group31" type="radio" id="group313" runat="server" />
                                            <label for="group313">Pocas veces</label>
                                            <input class="with-gap" name="group31" type="radio" id="group314" runat="server" />
                                            <label for="group314">Casi Nunca</label>
                                            <input class="with-gap" name="group31" type="radio" id="group315" runat="server" />
                                            <label for="group315">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>2_</b>Realizamos seguimientos de nuestro cliente durante y luego de la implementación  para garantizar el cumplimiento de cada compromiso identificando problemas para solucionarlos personalmente.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group32" type="radio" id="group321" runat="server" />
                                            <label for="group321">Siempre</label>
                                            <input class="with-gap" name="group32" type="radio" id="group322" runat="server" />
                                            <label for="group322">Muchas veces</label>
                                            <input class="with-gap" name="group32" type="radio" id="group323" runat="server" />
                                            <label for="group323">Pocas veces</label>
                                            <input class="with-gap" name="group32" type="radio" id="group324" runat="server" />
                                            <label for="group324">Casi Nunca</label>
                                            <input class="with-gap" name="group32" type="radio" id="group325" runat="server" />
                                            <label for="group325">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>3_</b> Consideramos la Experiencia del Cliente como un propósito estratégico de largo plazo que debe transformar nuestra organización</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group33" type="radio" id="group331" runat="server" />
                                            <label for="group331">Siempre</label>
                                            <input class="with-gap" name="group33" type="radio" id="group332" runat="server" />
                                            <label for="group332">Muchas veces</label>
                                            <input class="with-gap" name="group33" type="radio" id="group333" runat="server" />
                                            <label for="group333">Pocas veces</label>
                                            <input class="with-gap" name="group33" type="radio" id="group334" runat="server" />
                                            <label for="group334">Casi Nunca</label>
                                            <input class="with-gap" name="group33" type="radio" id="group335" runat="server" />
                                            <label for="group335">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>4_</b> Generamos vínculos cercanos con los clientes por los distintos canales de contacto.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group34" type="radio" id="group341" runat="server" />
                                            <label for="group341">Siempre</label>
                                            <input class="with-gap" name="group34" type="radio" id="group342" runat="server" />
                                            <label for="group342">Muchas veces</label>
                                            <input class="with-gap" name="group34" type="radio" id="group343" runat="server" />
                                            <label for="group343">Pocas veces</label>
                                            <input class="with-gap" name="group34" type="radio" id="group344" runat="server" />
                                            <label for="group344">Casi Nunca</label>
                                            <input class="with-gap" name="group34" type="radio" id="group345" runat="server" />
                                            <label for="group345">Nunca</label>
                                        </p>

                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP4" runat="server" onserverclick="btnSiguienteP4_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP4" runat="server" onserverclick="btnAnteriorP4_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Servicio-->
                    <div id="swipe5" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-04 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>1_</b> a.	Existe un área dedicada de "experiencia de cliente" (o con otro nombre) responsable de definir la visión global de la Experiencia de Cliente, desarrollarla, hacer seguimiento de la misma y garantizar que esté alineada en toda la compañía.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group41" type="radio" id="group411" runat="server" />
                                            <label for="group411">Siempre</label>
                                            <input class="with-gap" name="group41" type="radio" id="group412" runat="server" />
                                            <label for="group412">Muchas veces</label>
                                            <input class="with-gap" name="group41" type="radio" id="group413" runat="server" />
                                            <label for="group413">Pocas veces</label>
                                            <input class="with-gap" name="group41" type="radio" id="group414" runat="server" />
                                            <label for="group414">Casi Nunca</label>
                                            <input class="with-gap" name="group41" type="radio" id="group415" runat="server" />
                                            <label for="group415">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>2_</b> Nuestros clientes pueden comunicarse con nosotros por el canal que desee encontrando una respuesta consistente y rápida.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group32" type="radio" id="group421" runat="server" />
                                            <label for="group421">Siempre</label>
                                            <input class="with-gap" name="group32" type="radio" id="group422" runat="server" />
                                            <label for="group422">Muchas veces</label>
                                            <input class="with-gap" name="group32" type="radio" id="group423" runat="server" />
                                            <label for="group423">Pocas veces</label>
                                            <input class="with-gap" name="group32" type="radio" id="group424" runat="server" />
                                            <label for="group424">Casi Nunca</label>
                                            <input class="with-gap" name="group32" type="radio" id="group425" runat="server" />
                                            <label for="group425">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>3_</b> Frente a un pedido/reclamo de un cliente asumimos un compromiso genuino y damos seguimiento personalmente hasta su cierre/solución.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group33" type="radio" id="group431" runat="server" />
                                            <label for="group431">Siempre</label>
                                            <input class="with-gap" name="group33" type="radio" id="group432" runat="server" />
                                            <label for="group432">Muchas veces</label>
                                            <input class="with-gap" name="group33" type="radio" id="group433" runat="server" />
                                            <label for="group433">Pocas veces</label>
                                            <input class="with-gap" name="group33" type="radio" id="group434" runat="server" />
                                            <label for="group434">Casi Nunca</label>
                                            <input class="with-gap" name="group33" type="radio" id="group435" runat="server" />
                                            <label for="group435">Nunca</label>
                                        </p>
                                        <div class="enunciado">
                                            <p><b>4_</b>Los circuitos que vive el cliente con la empresa (consultas, pedidos, facturación, actualizaciones, etc..) son simples y fáciles de transitar.</p>
                                        </div>
                                        <p class="respuestas">
                                            <input class="with-gap" name="group34" type="radio" id="group441" runat="server" />
                                            <label for="group441">Siempre</label>
                                            <input class="with-gap" name="group34" type="radio" id="group442" runat="server" />
                                            <label for="group442">Muchas veces</label>
                                            <input class="with-gap" name="group34" type="radio" id="group443" runat="server" />
                                            <label for="group443">Pocas veces</label>
                                            <input class="with-gap" name="group34" type="radio" id="group444" runat="server" />
                                            <label for="group444">Casi Nunca</label>
                                            <input class="with-gap" name="group34" type="radio" id="group445" runat="server" />
                                            <label for="group445">Nunca</label>
                                        </p>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP5" runat="server" onserverclick="btnSiguienteP5_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP5" runat="server" onserverclick="btnAnteriorP5_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Maximizar-->
                    <div id="swipe6" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-05 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <div class="enunciado">
                                                <p><b>1_</b> Nuestra empresa destina presupuesto a iniciativas que tienen como objetivo generar impacto en Experiencia del Cliente.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group41" type="radio" id="group511" runat="server" />
                                                <label for="group511">Siempre</label>
                                                <input class="with-gap" name="group41" type="radio" id="group512" runat="server" />
                                                <label for="group512">Muchas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group513" runat="server" />
                                                <label for="group513">Pocas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group514" runat="server" />
                                                <label for="group514">Casi Nunca</label>
                                                <input class="with-gap" name="group41" type="radio" id="group515" runat="server" />
                                                <label for="group515">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>2_</b> La escucha activa y la recolección de la voz del cliente son procesos estructurados y valiosos en nuestra organización para dar feedback a las distintas áreas y generar planes de acción.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group32" type="radio" id="group521" runat="server" />
                                                <label for="group521">Siempre</label>
                                                <input class="with-gap" name="group32" type="radio" id="group522" runat="server" />
                                                <label for="group522">Muchas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group523" runat="server" />
                                                <label for="group523">Pocas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group524" runat="server" />
                                                <label for="group524">Casi Nunca</label>
                                                <input class="with-gap" name="group32" type="radio" id="group525" runat="server" />
                                                <label for="group525">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>3_</b> El cliente percibe una consistencia clara entre lo que prometemos y lo que entregamos en todos los puntos de contacto.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group33" type="radio" id="group531" runat="server" />
                                                <label for="group531">Siempre</label>
                                                <input class="with-gap" name="group33" type="radio" id="group532" runat="server" />
                                                <label for="group532">Muchas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group533" runat="server" />
                                                <label for="group533">Pocas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group534" runat="server" />
                                                <label for="group534">Casi Nunca</label>
                                                <input class="with-gap" name="group33" type="radio" id="group535" runat="server" />
                                                <label for="group535">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>4_</b>Entendemos como  con nuestra tarea diaria aportamos al cumplimiento de los objetivos de satisfacción/experiencia de cliente y nos motiva generar experiencias memorables.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group34" type="radio" id="group541" runat="server" />
                                                <label for="group541">Siempre</label>
                                                <input class="with-gap" name="group34" type="radio" id="group542" runat="server" />
                                                <label for="group542">Muchas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group543" runat="server" />
                                                <label for="group543">Pocas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group544" runat="server" />
                                                <label for="group544">Casi Nunca</label>
                                                <input class="with-gap" name="group34" type="radio" id="group545" runat="server" />
                                                <label for="group545">Nunca</label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP6" runat="server" onserverclick="btnSiguienteP6_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP6" runat="server" onserverclick="btnAnteriorP6_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Fidelizar-->
                    <div id="swipe7" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-06 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <div class="enunciado">
                                                <p><b>1_</b> Somos consciente de que nuestro actuar afecta  la Experiencia de Cliente, tanto en términos racionales (servicio, técnicos, funcionales) como emocionales  (percepción, estados de ánimo, sentimientos, etc.).</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group41" type="radio" id="group611" runat="server" />
                                                <label for="group611">Siempre</label>
                                                <input class="with-gap" name="group41" type="radio" id="group612" runat="server" />
                                                <label for="group612">Muchas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group613" runat="server" />
                                                <label for="group613">Pocas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group614" runat="server" />
                                                <label for="group614">Casi Nunca</label>
                                                <input class="with-gap" name="group41" type="radio" id="group615" runat="server" />
                                                <label for="group615">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>2_</b> Generamos continuamente campañas de fidelización conectada a cada perfil de Cliente.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group32" type="radio" id="group621" runat="server" />
                                                <label for="group621">Siempre</label>
                                                <input class="with-gap" name="group32" type="radio" id="group622" runat="server" />
                                                <label for="group622">Muchas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group623" runat="server" />
                                                <label for="group623">Pocas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group624" runat="server" />
                                                <label for="group624">Casi Nunca</label>
                                                <input class="with-gap" name="group32" type="radio" id="group625" runat="server" />
                                                <label for="group625">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>3_</b>Nuestro personal de contacto directo presencial y no presencial tiene las habilidades necesarias para dar un excelente experiencia a nuestros clientes.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group33" type="radio" id="group631" runat="server" />
                                                <label for="group631">Siempre</label>
                                                <input class="with-gap" name="group33" type="radio" id="group632" runat="server" />
                                                <label for="group632">Muchas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group633" runat="server" />
                                                <label for="group633">Pocas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group634" runat="server" />
                                                <label for="group634">Casi Nunca</label>
                                                <input class="with-gap" name="group33" type="radio" id="group635" runat="server" />
                                                <label for="group635">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>4_</b> Nuestros líderes reconocen a los colaboradores que marcan la diferencia en la visión de nuestros clientes.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group34" type="radio" id="group641" runat="server" />
                                                <label for="group641">Siempre</label>
                                                <input class="with-gap" name="group34" type="radio" id="group642" runat="server" />
                                                <label for="group642">Muchas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group643" runat="server" />
                                                <label for="group643">Pocas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group644" runat="server" />
                                                <label for="group644">Casi Nunca</label>
                                                <input class="with-gap" name="group34" type="radio" id="group645" runat="server" />
                                                <label for="group645">Nunca</label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP7" runat="server" onserverclick="btnSiguienteP7_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP7" runat="server" onserverclick="btnAnteriorP7_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Retener-->
                    <div id="swipe8" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-07 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <div class="enunciado">
                                                <p><b>1_</b>Comprobamos que todas las políticas de recursos humanos (ej. selección, formación, promoción etc) estén alineadas con la Experiencia de Cliente que queremos transmitir.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group41" type="radio" id="group711" runat="server" />
                                                <label for="group711">Siempre</label>
                                                <input class="with-gap" name="group41" type="radio" id="group712" runat="server" />
                                                <label for="group712">Muchas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group713" runat="server" />
                                                <label for="group713">Pocas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group714" runat="server" />
                                                <label for="group714">Casi Nunca</label>
                                                <input class="with-gap" name="group41" type="radio" id="group715" runat="server" />
                                                <label for="group715">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>2_</b> Tenemos objetivos de resolución en el primer contacto y este forma parte de nuestros objetivos de desempeño.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group32" type="radio" id="group721" runat="server" />
                                                <label for="group721">Siempre</label>
                                                <input class="with-gap" name="group32" type="radio" id="group722" runat="server" />
                                                <label for="group722">Muchas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group723" runat="server" />
                                                <label for="group723">Pocas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group724" runat="server" />
                                                <label for="group724">Casi Nunca</label>
                                                <input class="with-gap" name="group32" type="radio" id="group725" runat="server" />
                                                <label for="group725">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>3_</b>Somos flexibles para brindar diferentes alternativas que cambien el resultado frente a la decisión de un cliente de interrumpir el servicio.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group33" type="radio" id="group731" runat="server" />
                                                <label for="group731">Siempre</label>
                                                <input class="with-gap" name="group33" type="radio" id="group732" runat="server" />
                                                <label for="group732">Muchas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group733" runat="server" />
                                                <label for="group733">Pocas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group734" runat="server" />
                                                <label for="group734">Casi Nunca</label>
                                                <input class="with-gap" name="group33" type="radio" id="group735" runat="server" />
                                                <label for="group735">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>4_</b>Cuando estamos frente al cliente tenemos todas las herramientas y conocimientos necesarios para resolver.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group34" type="radio" id="group741" runat="server" />
                                                <label for="group741">Siempre</label>
                                                <input class="with-gap" name="group34" type="radio" id="group742" runat="server" />
                                                <label for="group742">Muchas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group743" runat="server" />
                                                <label for="group743">Pocas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group744" runat="server" />
                                                <label for="group744">Casi Nunca</label>
                                                <input class="with-gap" name="group34" type="radio" id="group745" runat="server" />
                                                <label for="group745">Nunca</label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP8" runat="server" onserverclick="btnSiguienteP8_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP8" runat="server" onserverclick="btnAnteriorP8_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Recuperar-->
                    <div id="swipe9" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-08 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <div class="enunciado">
                                                <p><b>1_</b> Tenemos procesos de actualización de nuestras bases de datos de clientes para cambios e incorporación de nuevos interlocutores.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group41" type="radio" id="group811" runat="server" />
                                                <label for="group811">Siempre</label>
                                                <input class="with-gap" name="group41" type="radio" id="group812" runat="server" />
                                                <label for="group812">Muchas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group813" runat="server" />
                                                <label for="group813">Pocas veces</label>
                                                <input class="with-gap" name="group41" type="radio" id="group814" runat="server" />
                                                <label for="group814">Casi Nunca</label>
                                                <input class="with-gap" name="group41" type="radio" id="group815" runat="server" />
                                                <label for="group815">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>2_</b>Analizamos profundamente las causas de las bajas de clientes y diseñamos campañas de retención con ofertas específicas.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group32" type="radio" id="group821" runat="server" />
                                                <label for="group821">Siempre</label>
                                                <input class="with-gap" name="group32" type="radio" id="group822" runat="server" />
                                                <label for="group822">Muchas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group823" runat="server" />
                                                <label for="group823">Pocas veces</label>
                                                <input class="with-gap" name="group32" type="radio" id="group824" runat="server" />
                                                <label for="group824">Casi Nunca</label>
                                                <input class="with-gap" name="group32" type="radio" id="group825" runat="server" />
                                                <label for="group825">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>3_</b> Asumimos un compromiso responsable de servicio cuando reactivamos un cliente con visión de largo plazo.</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group33" type="radio" id="group831" runat="server" />
                                                <label for="group831">Siempre</label>
                                                <input class="with-gap" name="group33" type="radio" id="group832" runat="server" />
                                                <label for="group832">Muchas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group833" runat="server" />
                                                <label for="group833">Pocas veces</label>
                                                <input class="with-gap" name="group33" type="radio" id="group834" runat="server" />
                                                <label for="group834">Casi Nunca</label>
                                                <input class="with-gap" name="group33" type="radio" id="group835" runat="server" />
                                                <label for="group835">Nunca</label>
                                            </p>
                                            <div class="enunciado">
                                                <p><b>4_</b>Tenemos canales de feedbck internos y la información es considerada oportunidad de aprendizaje y no crítica</p>
                                            </div>
                                            <p class="respuestas">
                                                <input class="with-gap" name="group34" type="radio" id="group841" runat="server" />
                                                <label for="group841">Siempre</label>
                                                <input class="with-gap" name="group34" type="radio" id="group842" runat="server" />
                                                <label for="group842">Muchas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group843" runat="server" />
                                                <label for="group843">Pocas veces</label>
                                                <input class="with-gap" name="group34" type="radio" id="group844" runat="server" />
                                                <label for="group844">Casi Nunca</label>
                                                <input class="with-gap" name="group34" type="radio" id="group845" runat="server" />
                                                <label for="group845">Nunca</label>
                                            </p>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnSiguienteP9" runat="server" onserverclick="btnSiguienteP9_click">Siguiente</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP9" runat="server" onserverclick="btnAnteriorP9_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Anexo-->
                    <div id="swipe10" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-09 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">

                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>1_</b> Considera que somos una empresa enfocada en el Cliente?</p>
                                        </div>
                                        <div class="left">
                                            <!-- Switch -->
                                            <div class="switch">
                                                <label>
                                                    No
                                                <input type="checkbox" name="enfocada" runat="server" id="chkEnfocada">
                                                    <span class="lever"></span>
                                                    Si
                                                </label>
                                            </div>
                                            <p class="respuestas">&nbsp;</p>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>2_</b> Cuáles son las tres cosas que considera que hacemos bien para ser una empresa orientada al Cliente?</p>
                                        </div>
                                        <div class="row">
                                            <div class="input-field col s12">
                                                <input name="considero" id="considero" type="text" class="validate" runat="server">
                                                <label for="considero">Considero...</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="enunciado">
                                            <p><b>3_</b> Cuáles son las tres cosas que cambiaría para mejorar este foco?</p>
                                        </div>
                                        <div class="row">
                                            <div class="input-field col s12">
                                                <input name="cambiaria" id="cambiaria" type="text" class="validate" runat="server">
                                                <label for="cambiaria">Las tres cosas...</label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col l12 s12">
                                        <div class="input-field col s12">
                                            <div class="bottom-buttons">
                                                <a class="waves-effect waves-light btn" id="btnEnviar" runat="server" onserverclick="btnEnviar_click">Enviar</a>
                                                <a class="waves-effect waves-light btn anterior" id="btnAnteriorP10" runat="server" onserverclick="btnAnteriorP10_click">Anterior</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- contenido tab Finalizado Exitoso-->
                    <div id="swipe11" class="swipe-fluid" runat="server" visible="false">
                        <div class="row center">
                            <div class="top-banner">
                                <div class="top-banner-09 top-banner-inner"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row center">
                                    <div style="height: 500px;"><strong><b>Encuesta finalizada exitosamente!! Muchas gracias.</b></strong></div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <input type="hidden" name="test" value="test">
                <asp:TextBox ID="txtencId" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtempId" runat="server" Visible="false"></asp:TextBox>

                <asp:Panel ID="panelMensajes" runat="server" Style="display: none; top: -100px;">
                    <div id="swipe12" class="swipe-fluid" style="background-color: White;">
                        <div class="row">
                            <span id="tituloPanel" runat="server" style="font-weight: bold; padding-top: 10px; margin-top: 10px;">&nbsp;&nbsp;INFORMACION</span>
                        </div>
                        <div class="row">
                            <div class="container container800">
                                <div class="row">
                                    <asp:Literal ID="litMensajes" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="row center">
                                <button id="btnCerrarPopUp" class="btn btn-danger" data-toggle="tooltip" title="Cerrar" runat="server" onserverclick="btnCancelar_Click"><i class="fa fa-times">Cerrar</i></button>
                                <a id="btnObservacionesAbrir" runat="server" href="#" title="" style="height: 0px; width: 0px;"></a>
                                <a id="btnObservacionesCancelar" runat="server" href="#" title="" style="height: 0px; width: 0px;"></a>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="mpeMensajes" runat="server"
                    TargetControlID="btnObservacionesAbrir"
                    PopupControlID="panelMensajes"
                    CancelControlID="btnObservacionesCancelar"
                    DropShadow="false"
                    BackgroundCssClass="modalBackground" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <footer class="page-footer">
        <div class="container">
            <div class="row center">
                <div class="col l12 s12">
                    <p class="white-text">© 2017 Todos los derechos reservados.</p>
                </div>
            </div>
        </div>
        <div class="footer-copyright">
            <div class="container"></div>
        </div>
    </footer>
    <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script>
    <script src="js/materialize.min.js"></script>
</body>
</html>
