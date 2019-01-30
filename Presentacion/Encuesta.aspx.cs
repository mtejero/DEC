using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Encuesta : System.Web.UI.Page
{
    #region Eventos
    /// <summary>
    /// Evento inical del Formularo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.Params["encId"] != null && Request.Params["encId"].ToString() != "")
                {
                    txtencId.Text = Request.Params["encId"].ToString();
                    if (Request.Params["empId"] != null && Request.Params["empId"].ToString() != "")
                    {

                        string sCodigo = Request.Params["empId"].ToString();


                        DataTable dt = CapaDatos.EjecutarReader("select * from Empresa where empActivo = 1 and empDescripcion like '%empId=" + sCodigo + "%'");
                        if (dt.Rows.Count == 0)
                        {
                            Response.Redirect("Error.aspx", true);
                        }
                        else
                        {
                            txtempId.Text = dt.Rows[0]["empId"].ToString();
                            CargarComboDepartamento();
                            CargarComboJerarquia();
                            CargarComboEdad();
                            CargarComboEstudio();
                            CargarComboAntiguedad();

                            ListItem wItem = new ListItem();
                            wItem = new ListItem("Sub-Departamento (solo para Departamento Producción/Operaciones)", "-1");
                            ddlSubDepartamento.Items.Add(wItem);

                            imgLogo.ImageUrl = dt.Rows[0]["empLogo"].ToString();
                            //switch (dt.Rows[0]["empId"].ToString())
                            //{
                            //    case "1":
                            //        imgLogo.ImageUrl = "images/vocus.png";
                            //        break;
                            //    case "2":
                            //        imgLogo.ImageUrl = "images/deelo.png";
                            //        break;
                            //    default:
                            //        imgLogo.Visible = false;

                            //        break;
                            //}


                        }
                    }
                    else
                    {
                        Response.Redirect("Error.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("Error.aspx", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Error.aspx", true);
        }
    }

    /// <summary>
    /// Carga Sub-Departamento si Corresponde
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartamento.SelectedItem.Text == "Producción/Operaciones")
            {
                ddlSubDepartamento.Items.Clear();
                ListItem wItem = new ListItem();
                wItem = new ListItem("Sub-Departamento (solo para Departamento Producción/Operaciones)", "-1");
                ddlSubDepartamento.Items.Add(wItem);

                wItem = new ListItem("Operación", "1");
                ddlSubDepartamento.Items.Add(wItem);

                wItem = new ListItem("Calidad", "2");
                ddlSubDepartamento.Items.Add(wItem);

                wItem = new ListItem("Planeamiento", "3");
                ddlSubDepartamento.Items.Add(wItem);

                wItem = new ListItem("Control de gestión", "4");
                ddlSubDepartamento.Items.Add(wItem);
            }
            else
            {
                ddlSubDepartamento.Items.Clear();
                ListItem wItem = new ListItem();
                wItem = new ListItem("Sub-Departamento (solo para Departamento Producción/Operaciones)", "-1");
                ddlSubDepartamento.Items.Add(wItem);
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Cerrar PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            mpeMensajes.Hide();
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Grabar Encuesta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviar_click(object sender, EventArgs e)
    {
        try
        {
            if (considero.Value == "" || cambiaria.Value == "")
            {
                litMensajes.Text = "Falta completar Preguntas para poder finalizar la Encuesta";
                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = true;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "active");

            if (txtencId.Text == "")
                txtencId.Text = "1";
            if (txtempId.Text == "")
                txtempId.Text = "1";

            #region Grabar Gestion
            object[,] arr = {
                        {"@gesFyH", DateTime.Now },
                        {"@cliId", int.Parse("2") },
                        {"@gesActivo", int.Parse("1") },
                        {"@segId", int.Parse("-1") },
                        {"@empId", int.Parse(txtempId.Text) },
                        {"@gesFechaInicio", DateTime.Now },
                        {"@gesObservaciones", "" }
                    };
            int gesId = CapaDatos.EjecutarScalar("[Gestion Insertar Cus]", arr);
            #endregion

            #region Grabar Gestion_Encuesta
            if (gesId > 0)
            {
                #region P1

                #region ED1
                object[,] arrED1 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("1") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED1);

                if (ddlDepartamento.SelectedItem.Text == "Producción/Operaciones")
                {
                    object[,] arrED2 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("2") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlDepartamento.SelectedItem.Text + " | " +  ddlSubDepartamento.SelectedItem.Text},
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED2);
                }
                else
                {
                    object[,] arrED2 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("2") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlDepartamento.SelectedItem.Text },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED2);
                }
                #endregion

                #endregion

                #region P2
                #region ED3
                object[,] arrED3 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("3") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED3);

                object[,] arrED4 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("4") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlEdad.SelectedItem.Text },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED4);
                #endregion

                #endregion

                #region P3
                #region ED5
                object[,] arrED5 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("5") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED5);

                object[,] arrED6 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("6") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlNivelEstudios.SelectedItem.Text },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED6);
                #endregion

                #endregion

                #region P4
                #region ED8
                object[,] arrED8 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("8") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED8);

                if (chk_formacionno.Checked)
                {
                    object[,] arrED9 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("9") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED9);
                }
                else
                {
                    object[,] arrED10 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("10") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED10);
                }
                #endregion

                #endregion

                #region P5
                #region ED11
                object[,] arrED11 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("11") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED11);

                object[,] arrED12 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("12") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlNivel.SelectedItem.Text },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED12);
                #endregion

                #endregion

                #region P6
                #region ED13
                object[,] arrED13 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("13") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED13);

                if (chk_sexomasculino.Checked)
                {
                    object[,] arrED14 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("14") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED14);
                }
                else
                {
                    object[,] arrED15 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("15") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED15);
                }
                #endregion

                #endregion

                #region P7
                #region ED16
                object[,] arrED16 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("16") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED16);

                object[,] arrED17 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("17") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", ddlAntiguedad.SelectedItem.Text },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED17);
                #endregion

                #endregion

                #region P8
                #region ED18
                object[,] arrED18 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("18") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED18);

                object[,] arrED19 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("19") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group111.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED19);

                object[,] arrED20 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("20") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group112.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED20);

                object[,] arrED21 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("21") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group113.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED21);

                object[,] arrED22 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("22") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group114.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED22);

                object[,] arrED24 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("24") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group115.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED24);
                #endregion

                #endregion

                #region P9
                #region ED25
                object[,] arrED25 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("25") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED25);

                object[,] arrED26 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("26") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group121.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED26);

                object[,] arrED27 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("27") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group122.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED27);

                object[,] arrED28 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("28") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group123.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED28);

                object[,] arrED29 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("29") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group124.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED29);

                object[,] arrED30 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("30") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group125.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED30);
                #endregion

                #endregion

                #region P10
                #region ED31
                object[,] arrED31 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("31") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED31);

                object[,] arrED32 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("32") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group131.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED32);

                object[,] arrED33 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("33") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group132.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED33);

                object[,] arrED34 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("34") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group133.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED34);

                object[,] arrED35 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("35") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group134.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED35);

                object[,] arrED36 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("36") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group135.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED36);
                #endregion

                #endregion

                #region P11

                #region ED37
                object[,] arrED37 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("37") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED37);

                object[,] arrED38 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("38") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group141.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED38);

                object[,] arrED39 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("39") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group142.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED39);

                object[,] arrED40 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("40") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group143.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED40);

                object[,] arrED41 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("41") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group144.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED41);

                object[,] arrED42 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("42") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group145.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED42);
                #endregion

                #endregion

                #region P12
                #region ED43
                object[,] arrED43 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("43") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED43);

                object[,] arrED44 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("44") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group211.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED44);

                object[,] arrED45 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("45") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group212.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED45);

                object[,] arrED46 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("46") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group213.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED46);

                object[,] arrED47 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("47") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group214.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED47);

                object[,] arrED48 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("48") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group215.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED48);
                #endregion

                #endregion

                #region P13
                #region ED49
                object[,] arrED49 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("49") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED49);

                object[,] arrED50 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("50") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group221.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED50);

                object[,] arrED51 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("51") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group222.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED51);

                object[,] arrED52 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("52") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group223.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED52);

                object[,] arrED53 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("53") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group224.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED53);

                object[,] arrED54 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("54") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group225.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED54);
                #endregion

                #endregion

                #region P14
                #region ED55
                object[,] arrED55 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("55") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED55);

                object[,] arrED56 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("56") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group231.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED56);

                object[,] arrED57 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("57") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group232.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED57);

                object[,] arrED58 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("58") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group233.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED58);

                object[,] arrED59 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("59") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group234.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED59);

                object[,] arrED60 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("60") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group235.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED60);
                #endregion

                #endregion

                #region P15
                #region ED61
                object[,] arrED61 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("61") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED61);

                object[,] arrED62 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("62") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group241.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED62);

                object[,] arrED63 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("63") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group242.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED63);

                object[,] arrED64 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("64") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group243.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED64);

                object[,] arrED65 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("65") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group244.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED65);

                object[,] arrED66 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("66") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group245.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED66);
                #endregion

                #endregion

                #region P16
                #region ED67
                object[,] arrED67 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("67") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED67);

                object[,] arrED68 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("68") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group311.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED68);

                object[,] arrED69 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("69") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group312.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED69);

                object[,] arrED70 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("70") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group313.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED70);

                object[,] arrED71 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("71") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group314.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED71);

                object[,] arrED72 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("72") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group315.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED72);
                #endregion

                #endregion

                #region P17
                #region ED73
                object[,] arrED73 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("73") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED73);

                object[,] arrED74 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("74") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group321.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED74);

                object[,] arrED75 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("75") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group322.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED75);

                object[,] arrED76 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("76") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group323.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED76);

                object[,] arrED77 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("77") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group324.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED77);

                object[,] arrED78 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("78") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group325.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED78);
                #endregion

                #endregion

                #region P18
                #region ED1
                object[,] arrED79 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("79") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED79);

                object[,] arrED80 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("80") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group331.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED80);

                object[,] arrED81 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("81") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group332.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED81);

                object[,] arrED82 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("82") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group333.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED82);

                object[,] arrED83 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("83") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group334.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED83);

                object[,] arrED84 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("84") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group335.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED84);
                #endregion

                #endregion

                #region P19
                #region ED1
                object[,] arrED85 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("85") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED85);

                object[,] arrED86 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("86") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group341.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED86);

                object[,] arrED87 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("87") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group342.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED87);

                object[,] arrED88 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("88") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group343.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED88);

                object[,] arrED89 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("89") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group344.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED89);

                object[,] arrED90 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("90") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group345.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED90);

                #endregion

                #endregion

                #region P20
                #region ED91
                object[,] arrED91 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("91") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED91);

                object[,] arrED92 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("92") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group411.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED92);

                object[,] arrED93 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("93") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group412.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED93);

                object[,] arrED94 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("94") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group413.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED94);

                object[,] arrED95 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("95") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group414.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED95);

                object[,] arrED96 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("96") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group415.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED96);

                #endregion

                #endregion

                #region P21

                #region ED97
                object[,] arrED97 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("97") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED97);

                object[,] arrED98 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("98") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group421.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED98);

                object[,] arrED99 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("99") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group422.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED99);

                object[,] arrED100 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("100") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group423.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED100);

                object[,] arrED101 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("101") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group424.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED101);

                object[,] arrED102 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("102") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group425.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED102);
                #endregion

                #endregion

                #region P22
                #region ED103
                object[,] arrED103 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("103") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED103);

                object[,] arrED104 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("104") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group431.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED104);

                object[,] arrED105 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("105") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group432.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED105);

                object[,] arrED106 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("106") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group433.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED106);

                object[,] arrED107 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("107") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group434.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED107);

                object[,] arrED108 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("108") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group435.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED108);
                #endregion

                #endregion

                #region P23
                #region ED109
                object[,] arrED109 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("109") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED109);

                object[,] arrED110 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("110") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group441.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED110);

                object[,] arrED111 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("111") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group442.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED111);

                object[,] arrED112 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("112") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group443.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED112);

                object[,] arrED113 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("113") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group444.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED113);

                object[,] arrED114 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("114") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group445.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED114);
                #endregion

                #endregion

                #region P24
                #region ED115
                object[,] arrED115 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("115") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED115);

                object[,] arrED116 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("116") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group511.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED116);

                object[,] arrED117 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("117") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group512.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED117);

                object[,] arrED118 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("118") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group513.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED118);

                object[,] arrED119 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("119") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group514.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED119);

                object[,] arrED120 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("120") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group515.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED120);
                #endregion

                #endregion

                #region P25
                #region ED121
                object[,] arrED121 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("121") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED121);

                object[,] arrED122 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("122") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group521.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED122);

                object[,] arrED123 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("123") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group522.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED123);

                object[,] arrED124 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("124") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group523.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED124);

                object[,] arrED125 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("125") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group524.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED125);

                object[,] arrED126 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("126") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group525.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED126);
                #endregion

                #endregion

                #region P26
                #region ED1
                object[,] arrED127 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("127") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED127);

                object[,] arrED128 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("128") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group531.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED128);

                object[,] arrED129 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("129") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group532.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED129);

                object[,] arrED130 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("130") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group533.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED130);

                object[,] arrED131 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("131") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group534.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED131);

                object[,] arrED132 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("132") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group535.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED132);
                #endregion

                #endregion

                #region P27
                #region ED133
                object[,] arrED133 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("133") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED133);

                object[,] arrED134 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("134") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group541.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED134);

                object[,] arrED135 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("135") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group542.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED135);

                object[,] arrED136 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("136") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group543.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED136);

                object[,] arrED137 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("137") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group544.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED137);

                object[,] arrED138 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("138") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group545.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED138);
                #endregion

                #endregion

                #region P28
                #region ED1139
                object[,] arrED139 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("139") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED139);

                object[,] arrED140 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("140") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group611.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED140);

                object[,] arrED141 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("141") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group612.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED141);

                object[,] arrED142 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("142") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group613.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED142);

                object[,] arrED143 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("143") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group614.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED143);

                object[,] arrED144 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("144") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group615.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED144);
                #endregion

                #endregion

                #region P29
                #region ED145
                object[,] arrED145 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("145") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED145);

                object[,] arrED146 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("146") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group621.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED146);

                object[,] arrED147 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("147") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group622.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED147);

                object[,] arrED148 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("148") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group623.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED148);

                object[,] arrED149 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("149") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group624.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED149);

                object[,] arrED150 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("150") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group625.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED150);
                #endregion

                #endregion

                #region P30
                #region ED151
                object[,] arrED151 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("151") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED151);

                object[,] arrED152 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("152") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group631.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED152);

                object[,] arrED153 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("153") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group632.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED153);

                object[,] arrED154 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("154") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group633.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED154);

                object[,] arrED155 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("155") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group634.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED155);

                object[,] arrED156 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("156") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group635.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED156);
                #endregion

                #endregion

                #region P31

                #region ED157
                object[,] arrED157 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("157") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED157);

                object[,] arrED158 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("158") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group641.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED158);

                object[,] arrED159 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("159") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group642.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED159);

                object[,] arrED160 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("160") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group643.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED160);

                object[,] arrED161 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("161") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group644.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED161);

                object[,] arrED162 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("162") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group645.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED162);
                #endregion

                #endregion

                #region P32
                #region ED163
                object[,] arrED163 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("163") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED163);

                object[,] arrED164 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("164") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group711.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED164);

                object[,] arrED165 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("165") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group712.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED165);

                object[,] arrED166 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("166") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group713.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED166);

                object[,] arrED167 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("167") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group714.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED167);

                object[,] arrED168 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("168") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group715.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED168);
                #endregion

                #endregion

                #region P33
                #region ED169
                object[,] arrED169 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("169") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED169);

                object[,] arrED170 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("170") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group721.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED170);

                object[,] arrED171 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("171") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group722.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED171);

                object[,] arrED172 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("172") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group723.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED172);

                object[,] arrED173 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("173") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group724.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED173);

                object[,] arrED174 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("174") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group725.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED174);
                #endregion

                #endregion

                #region P34
                #region ED175
                object[,] arrED175 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("175") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED175);

                object[,] arrED176 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("176") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group731.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED176);

                object[,] arrED177 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("177") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group732.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED177);

                object[,] arrED178 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("178") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group733.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED178);

                object[,] arrED179 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("179") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group734.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED179);

                object[,] arrED180 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("180") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group735.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED180);
                #endregion

                #endregion

                #region P35
                #region ED181
                object[,] arrED181 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("181") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED181);

                object[,] arrED182 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("182") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group741.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED182);

                object[,] arrED183 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("183") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group742.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED183);

                object[,] arrED184 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("184") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group743.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED184);

                object[,] arrED185 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("185") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group744.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED185);

                object[,] arrED186 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("186") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group745.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED186);
                #endregion

                #endregion

                #region P36
                #region ED187
                object[,] arrED187 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("187") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED187);

                object[,] arrED188 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("188") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group811.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED188);

                object[,] arrED189 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("189") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group812.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED189);

                object[,] arrED190 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("190") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group813.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED190);

                object[,] arrED191 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("191") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group814.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED191);

                object[,] arrED192 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("192") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group815.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED192);
                #endregion

                #endregion

                #region P37
                #region ED193
                object[,] arrED193 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("193") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED193);

                object[,] arrED194 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("194") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group821.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED194);

                object[,] arrED195 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("195") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group822.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED195);

                object[,] arrED196 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("196") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group823.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED196);

                object[,] arrED197 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("197") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group824.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED197);

                object[,] arrED198 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("198") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group825.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED198);
                #endregion

                #endregion

                #region P38
                #region ED199
                object[,] arrED199 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("199") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED199);

                object[,] arrED200 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("200") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group831.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED200);

                object[,] arrED201 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("201") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group832.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED201);

                object[,] arrED202 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("202") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group833.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED202);

                object[,] arrED203 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("203") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group834.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED203);

                object[,] arrED204 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("204") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group835.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED204);
                #endregion

                #endregion

                #region P39
                #region ED205
                object[,] arrED205 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("205") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED205);

                object[,] arrED206 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("206") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group841.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED206);

                object[,] arrED207 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("207") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group842.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED207);

                object[,] arrED208 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("208") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group843.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED208);

                object[,] arrED209 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("209") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group844.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED209);

                object[,] arrED210 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("210") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse(group845.Checked ? "1" : "0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED210);
                #endregion

                #endregion

                #region P40
                #region ED211
                object[,] arrED211 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("211") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED211);

                if (!chkEnfocada.Checked)
                {
                    object[,] arrED212 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("212") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED212);
                }
                else
                {
                    object[,] arrED213 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("213") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("1") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                    CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED213);
                }
                #endregion

                #endregion

                #region P41
                #region ED214
                object[,] arrED214 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("214") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED214);

                object[,] arrED215 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("215") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", considero.Value },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED215);
                #endregion

                #endregion

                #region P42
                #region ED216
                object[,] arrED216 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("216") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", "" },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED216);

                object[,] arrED217 = {
                        {"@gesId", gesId},
                        {"@edeId", int.Parse("217") },
                        {"@encId", int.Parse(txtencId.Text) },
                        {"@genAlRta", int.Parse("0") },
                        {"@genPrRta", cambiaria.Value },
                        {"@empId", int.Parse(txtempId.Text) }
                    };
                CapaDatos.EjecutarNonQuery("[Gestion_Encuesta Insertar]", arrED217);
                #endregion

                #endregion
            }
            #endregion
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP1_click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartamento.SelectedValue == "-1" || ddlNivel.SelectedValue == "-1" || ddlEdad.SelectedValue == "-1" ||
                ddlNivelEstudios.SelectedValue == "-1" || ddlAntiguedad.SelectedValue == "-1")
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            if (!chk_sexomasculino.Checked && !chk_sexofemenino.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            if (!chk_formacionno.Checked && !chk_formacionsi.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            if (ddlDepartamento.SelectedItem.Text == "Producción/Operaciones" && ddlSubDepartamento.SelectedValue == "-1")
            {
                litMensajes.Text = "Falta completar Sub-Departamento para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = true;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "active");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP2_click(object sender, EventArgs e)
    {
        try
        {
            if (!group111.Checked && !group112.Checked && !group113.Checked && !group114.Checked && !group115.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group121.Checked && !group122.Checked && !group123.Checked && !group124.Checked && !group125.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group131.Checked && !group132.Checked && !group133.Checked && !group134.Checked && !group135.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group141.Checked && !group142.Checked && !group143.Checked && !group144.Checked && !group145.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = true;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "active");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 3
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP3_click(object sender, EventArgs e)
    {
        try
        {
            if (!group211.Checked && !group212.Checked && !group213.Checked && !group214.Checked && !group215.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group221.Checked && !group222.Checked && !group223.Checked && !group224.Checked && !group225.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group231.Checked && !group232.Checked && !group233.Checked && !group234.Checked && !group235.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group241.Checked && !group242.Checked && !group243.Checked && !group244.Checked && !group245.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = true;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "active");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 4
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP4_click(object sender, EventArgs e)
    {
        try
        {
            if (!group311.Checked && !group312.Checked && !group313.Checked && !group314.Checked && !group315.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group321.Checked && !group322.Checked && !group323.Checked && !group324.Checked && !group325.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group331.Checked && !group332.Checked && !group333.Checked && !group334.Checked && !group335.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group341.Checked && !group342.Checked && !group343.Checked && !group344.Checked && !group345.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = true;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "active");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 5
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP5_click(object sender, EventArgs e)
    {
        try
        {
            if (!group411.Checked && !group412.Checked && !group413.Checked && !group414.Checked && !group415.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group421.Checked && !group422.Checked && !group423.Checked && !group424.Checked && !group425.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group431.Checked && !group432.Checked && !group433.Checked && !group434.Checked && !group435.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group441.Checked && !group442.Checked && !group443.Checked && !group444.Checked && !group445.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = true;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "active");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 6
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP6_click(object sender, EventArgs e)
    {
        try
        {
            if (!group511.Checked && !group512.Checked && !group513.Checked && !group514.Checked && !group515.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group521.Checked && !group522.Checked && !group523.Checked && !group524.Checked && !group525.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group531.Checked && !group532.Checked && !group533.Checked && !group534.Checked && !group535.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group541.Checked && !group542.Checked && !group543.Checked && !group544.Checked && !group545.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = true;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "active");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 7
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP7_click(object sender, EventArgs e)
    {
        try
        {
            if (!group611.Checked && !group612.Checked && !group613.Checked && !group614.Checked && !group615.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group621.Checked && !group622.Checked && !group623.Checked && !group624.Checked && !group625.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group631.Checked && !group632.Checked && !group633.Checked && !group634.Checked && !group635.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group641.Checked && !group642.Checked && !group643.Checked && !group644.Checked && !group645.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = true;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "active");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 8
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP8_click(object sender, EventArgs e)
    {
        try
        {
            if (!group711.Checked && !group712.Checked && !group713.Checked && !group714.Checked && !group715.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group721.Checked && !group722.Checked && !group723.Checked && !group724.Checked && !group725.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group731.Checked && !group732.Checked && !group733.Checked && !group734.Checked && !group735.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group741.Checked && !group742.Checked && !group743.Checked && !group744.Checked && !group745.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = true;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "active");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Sigiente Paso 9
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiguienteP9_click(object sender, EventArgs e)
    {
        try
        {
            if (!group811.Checked && !group812.Checked && !group813.Checked && !group814.Checked && !group815.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group821.Checked && !group822.Checked && !group823.Checked && !group824.Checked && !group825.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group831.Checked && !group832.Checked && !group833.Checked && !group834.Checked && !group835.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }
            if (!group841.Checked && !group842.Checked && !group843.Checked && !group844.Checked && !group845.Checked)
            {
                litMensajes.Text = "Falta completar Preguntas para poder pasar al siguiente paso";

                mpeMensajes.Show();
                return;
            }

            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = true;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "active");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP2_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = true;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "active");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 3
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP3_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = true;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "active");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 4
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP4_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = true;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "active");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 5
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP5_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = true;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "active");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 6
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP6_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = true;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "active");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 7
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP7_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = true;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "active");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 8
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP8_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = true;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "active");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 9
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP9_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = true;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "active");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Anterior Paso 10
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAnteriorP10_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = true;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "active");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso1
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe1_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = true;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "active");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe2_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = true;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "active");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso3
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe3_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = true;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "active");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso4
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe4_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = true;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "active");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso5
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe5_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = true;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "active");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso6
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe6_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = true;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "active");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso7
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe7_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = true;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "active");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso8
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe8_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = true;
            swipe9.Visible = false;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "active");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso9
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe9_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = true;
            swipe10.Visible = false;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "active");
            aswipe10.Attributes.Add("class", "");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Mostrar Paso10
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void aswipe10_click(object sender, EventArgs e)
    {
        try
        {
            swipe1.Visible = false;
            swipe2.Visible = false;
            swipe3.Visible = false;
            swipe4.Visible = false;
            swipe5.Visible = false;
            swipe6.Visible = false;
            swipe7.Visible = false;
            swipe8.Visible = false;
            swipe9.Visible = false;
            swipe10.Visible = true;
            swipe11.Visible = false;

            aswipe1.Attributes.Add("class", "");
            aswipe2.Attributes.Add("class", "");
            aswipe3.Attributes.Add("class", "");
            aswipe4.Attributes.Add("class", "");
            aswipe5.Attributes.Add("class", "");
            aswipe6.Attributes.Add("class", "");
            aswipe7.Attributes.Add("class", "");
            aswipe8.Attributes.Add("class", "");
            aswipe9.Attributes.Add("class", "");
            aswipe10.Attributes.Add("class", "active");
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
    #endregion

    #region Metodos
    /// <summary>
    /// Carga combo Departamento
    /// </summary>
    /// <param name="dt"></param>
    private void CargarComboDepartamento()
    {
        try
        {
            ddlDepartamento.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Departamento al que pertenece", "-1");
            ddlDepartamento.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Departamento where depActivo = 1 and empId = " + txtempId.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["depNombre"].ToString(), dt.Rows[i]["depId"].ToString());
                ddlDepartamento.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Carga combo Jerarquia
    /// </summary>
    /// <param name="dt"></param>
    private void CargarComboJerarquia()
    {
        try
        {
            ddlNivel.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Nivel jerárquico", "-1");
            ddlNivel.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Jerarquia where jerActivo = 1 and empId = " + txtempId.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["jerNombre"].ToString(), dt.Rows[i]["jerId"].ToString());
                ddlNivel.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Carga combo Edad
    /// </summary>
    /// <param name="dt"></param>
    private void CargarComboEdad()
    {
        try
        {
            ddlEdad.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Edad", "-1");
            ddlEdad.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Edad where edaActivo = 1 and empId = " + txtempId.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["edaNombre"].ToString(), dt.Rows[i]["edaId"].ToString());
                ddlEdad.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Carga combo Estudio
    /// </summary>
    /// <param name="dt"></param>
    private void CargarComboEstudio()
    {
        try
        {
            ddlNivelEstudios.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Nivel de estudios", "-1");
            ddlNivelEstudios.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Estudio where estActivo = 1 and empId = " + txtempId.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["estNombre"].ToString(), dt.Rows[i]["estId"].ToString());
                ddlNivelEstudios.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Carga combo Antiguedad
    /// </summary>
    /// <param name="dt"></param>
    private void CargarComboAntiguedad()
    {
        try
        {
            ddlAntiguedad.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Antiguedad de la compañía", "-1");
            ddlAntiguedad.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Antiguedad where antActivo = 1 and empId = " + txtempId.Text);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["antNombre"].ToString(), dt.Rows[i]["antId"].ToString());
                ddlAntiguedad.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
    #endregion
}