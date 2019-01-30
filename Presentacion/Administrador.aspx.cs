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
using System.Text;

public partial class administrador : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bool login = Session["login"] != null ? true : false;

                if (login)
                {

                    gviCargar();
                }
                else
                {
                    Response.Redirect("Error.aspx?e=2", true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    /// <summary>
    /// Consulta los datos de la entidad y carga la Grilla correspondiente
    /// </summary>  
    private void gviCargar()
    {
        try
        {
            #region Campana ObtenerxFiltros
            DataTable dt = CapaDatos.EjecutarReader(@"select empresa.*, 
(select count(distinct gestion.gesId) from gestion, gestion_encuesta where gestion.gesId = gestion_encuesta.gesId and gestion.empId = empresa.empId) as Cant_Enc
from empresa");
            #endregion

            gvi.DataSource = dt;
            gvi.PageIndex = 0;
            gvi.DataBind();
        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }



    /// <summary>
    /// Al presionar algun boton en la Grilla, se llama a este Evento.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort")
            {

                int idSeleccionado = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    string pathReporteEmpresa = Server.MapPath("upload/Reporte_Empresa" + idSeleccionado.ToString() + ".xls");
                    string pathReporte = Server.MapPath("upload/Reporte_" + idSeleccionado.ToString() + ".xls");

                    if (File.Exists(pathReporteEmpresa))
                        File.Delete(pathReporteEmpresa);
                    if (File.Exists(pathReporte))
                        File.Delete(pathReporte);
                    SQLToExcel("ReporteEncuestaStandard", idSeleccionado.ToString(), Server.MapPath("upload/Reporte_" + idSeleccionado.ToString() + ".xls"));



                    ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open( 'upload/Reporte_" + idSeleccionado.ToString() + ".xls');", true);


                }
                else if (e.CommandName == "Activar")
                {
                    CapaDatos.EjecutarNonQuery("update empresa set empActivo = 1 where empId = " + idSeleccionado.ToString());
                }
                else if (e.CommandName == "Desactivar")
                {
                    CapaDatos.EjecutarNonQuery("update empresa set empActivo = 0 where empId = " + idSeleccionado.ToString());
                }


                gviCargar();


            }
        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }



    /// <summary>
    /// Se ejecuta para cada fila de la Grilla. Se asocian imagenes por ejemplo al Actualizar / Desactualizar
    /// segun corresponda.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow Fila = ((System.Data.DataRowView)e.Row.DataItem).Row;
                ImageButton _myButton = (ImageButton)e.Row.FindControl("imgActivar");
                ImageButton _myButtonSeleccionar = (ImageButton)e.Row.FindControl("imgSeleccionar");

                _myButtonSeleccionar.ImageUrl = "images/a-editar02.png";

                if (Fila["empActivo"].ToString().ToLower() == "0")
                {
                    _myButton.ImageUrl = "images/uncheck.png";
                    _myButton.ToolTip = "Activar este Item";
                    _myButton.CommandName = "Activar";
                }
                else
                {
                    _myButton.ImageUrl = "images/check.png";
                    _myButton.ToolTip = "Desactivar este Item";
                    _myButton.CommandName = "Desactivar";
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }


    /// <summary>
    /// Transformar a CSV
    /// </summary>
    /// <param name="query"></param>
    /// <param name="Filename"></param>
    private void SQLToExcel(string sp, string empId, string Filename)
    {
        try
        {
            string connection = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sp, conn);
            cmd.CommandTimeout = 120;
            SqlParameter oParEmpId;
            oParEmpId = new SqlParameter("@empId", empId);
            cmd.Parameters.Add(oParEmpId);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            FileStream Archivo = null;
            Archivo = new FileStream(Filename, FileMode.CreateNew);

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Archivo, Encoding.Default))
            {
                // Loop through the fields and add headers
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                        name = "\"" + name + "\"";

                    fs.Write(name + "\t");
                }
                fs.WriteLine();

                // Loop through the rows and output the data
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = dr[i].ToString();
                        if (value.Contains("\t"))
                            value = "\"" + value + "\"";

                        fs.Write(value + "\t");
                    }
                    fs.WriteLine();
                }

                fs.Close();
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }


    private void SQLToExcelReporte(string sp, string Parametros, string Filename)
    {
        try
        {

            string[] arg = new string[3];
            arg = Parametros.ToString().Split(',');
            Session["repId"] = arg[0];
            Session["empId"] = arg[1];

            int idRep = Convert.ToInt32(Session["repId"]);
            int idEmp = Convert.ToInt32(Session["empId"]);
            string ProcEjec = "";

            if (idRep == 5)
            {
                ProcEjec = "Excel";
            }

            object[,] oParametros = {
                        {"@RepId", idRep},
                        {"@EmpId", idEmp },
                        {"@ProcEjec", ProcEjec}
                    };

            DataTable dt = CapaDatos.EjecutarReader(sp, oParametros);
            DataTableReader dr = new DataTableReader(dt);


            FileStream Archivo = null;
            Archivo = new FileStream(Filename, FileMode.CreateNew);

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Archivo, Encoding.Default))
            {
                // Loop through the fields and add headers
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                        name = "\"" + name + "\"";

                    fs.Write(name + "\t");
                }
                fs.WriteLine();

                // Loop through the rows and output the data
                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = dr[i].ToString();
                        if (value.Contains("\t"))
                            value = "\"" + value + "\"";

                        fs.Write(value + "\t");
                    }
                    fs.WriteLine();
                }

                fs.Close();
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }

    }







}

