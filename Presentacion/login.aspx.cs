using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UserName.Value = "";
                UserPassword.Value = "";
                UserName.Focus();
                if (Request.QueryString["log"] == "out")
                {
                    Session["login"] = false;
                    Session["usuario"] = "";
                    Session["clave"] = "";
                }

            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }


    protected void btnIniciarSesion_ServerClick(object sender, EventArgs e)
    {
        ValidarUsuario();

    }

    protected void ValidarUsuario()
    {

        try
        {

            DataTable dt = new DataTable();
            FuncionesUtil util = new FuncionesUtil();

            if (UserName.Value == "" && UserPassword.Value == "")
            {
                Msg.Visible = true;
                Msg.InnerText = "(*) Ingrese usuario y contraseña";
                UserName.Value = "";
                UserPassword.Value = "";
                UserName.Focus();
                return;
            }

            object[,] oParametros = {
                        {"@usuUsuario",UserName.Value}
                    };

            dt = CapaDatos.EjecutarReader("ObtenerUsuario", oParametros);

            string strPasswordEncrypt = util.GetHash(UserPassword.Value.Trim());


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["usuPassword"].ToString() == strPasswordEncrypt)
                {
                    Msg.Visible = true;
                    Msg.Style.Value = "color:cornflowerblue ;  font-weight:bold; font-size:20px";
                    Msg.InnerText = "Ingreso Correcto!! Bienvenido!";
                    Session["PasswordEncriptado"] = strPasswordEncrypt;
                    //valido q pantallas ve el usuario logueado

                    DataTable dtUsu = CapaDatos.EjecutarReader("ObtenerUsuarioPantallas", oParametros);

                    if (dtUsu.Rows.Count > 0)
                    {
                        Session["login"] = true;
                        Session["usuario"] = UserName.Value;
                        Session["clave"] = strPasswordEncrypt;

                        // Response.Redirect("administrador.aspx?usuId=ynavarro&clave=hJsvP2MyI0P93Fo8jajwfkA07k1eshClrZ254ztq7BjeqBg2qH+SJqRjbGx3iTsL00CPbR/iJbsJB8VWqBETVQ==");
                        Response.Redirect("administrador.aspx");

                        //for (int i = 0; i < dtUsu.Rows.Count; i++)
                        //{
                        //    wItem = new ListItem(dtUsu.Rows[i]["modDescripcion"].ToString(), dtUsu.Rows[i]["modId"].ToString());
                        //    ddlModulo.Items.Add(wItem);
                        //}

                        //FldPanelModulo.Disabled = false;
                        //ddlModulo.Enabled = true;
                        //ddlModulo.Focus();
                    }
                    else
                    {
                        Session["login"] = false;
                        Msg.Visible = true;
                        Msg.InnerText = "(*) No cuenta con permisos suficiones.";
                    }

                }
                else
                {
                    Session["login"] = false;
                    Msg.Visible = true;
                    Msg.InnerText = "(*) Password Ingresado incorrecto";
                    UserPassword.Value = "";
                    UserPassword.Focus();
                }
            }
            else
            {
                Msg.Visible = true;
                Msg.InnerText = "(*) No existe el usuario ingresado";
                UserName.Value = "";
                UserPassword.Value = "";
                UserName.Focus();
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }

    }


}