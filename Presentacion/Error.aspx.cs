using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string IdError = Request.Params["e"];
                switch (IdError)
                {
                    case "1":
                        msgError.Text = "";
                        break;

                    case "2":
                        msgError.Text = " La sesion ha expirado, ingrese nuevamente. <a href='login.aspx' alt='Ingreso al Sistema'> AQUI</a>";
                        break;

                    default:
                        msgError.Text = "Es posible que la Encuesta que está intentando acceder ya no esté activa o que no tenga permisos de acceso.";
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
}