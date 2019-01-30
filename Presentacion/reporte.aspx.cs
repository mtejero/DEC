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

public partial class reporte : System.Web.UI.Page
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
                    CargarComboEmpresa();


                    if (Request.QueryString["empId"] != null)
                    {
                        int empId = Convert.ToInt32(Request.QueryString["empId"]);
                        gviCargar(empId);
                    }
                }
                else
                {
                    Response.Redirect("error.aspx?e=2", true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }



    /// <param name="dt"></param>
    private void CargarComboEmpresa()
    {
        try
        {
            ddlEmpresa.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Empresa", "-1");
            ddlEmpresa.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Empresa");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["empNombre"].ToString(), dt.Rows[i]["empId"].ToString());
                ddlEmpresa.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }
    protected void gviCargar(int empId)
    {
        try
        {
            #region Campana ObtenerxFiltros
            DataTable dt2 = CapaDatos.EjecutarReader(@"select empId,empNombre, repId, repDescripcion ,repArchivo
                                    from Reportes, Empresa where empid = " + empId
                                    );
            #endregion
            gviReporte.DataSource = "";
            gviReporte.DataSource = dt2;
            gviReporte.PageIndex = 0;
            gviReporte.DataBind();

            btnSemaforo.Visible = true;
            btnSemaforo.NavigateUrl = "reporte_semaforo.aspx?empId=" + empId;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    protected void gviCargarReporte(object sender, EventArgs e)
    {
        try
        {

            gviCargar(Convert.ToInt32(ddlEmpresa.SelectedItem.Value));

        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }

    protected void gvi_RowCommandReporte(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Sort")
            {

                string[] arg = new string[4];
                arg = e.CommandArgument.ToString().Split(';');
                Session["repId"] = arg[0];
                Session["empId"] = arg[1];
                Session["repArchivo"] = arg[2];
                Session["repDescripcion"] = arg[3];

                int idRepSeleccionado = Convert.ToInt32(Session["repId"]);
                int idEmpSeleccionado = Convert.ToInt32(Session["empId"]);
                string NombreArchivoSeleccionado = Convert.ToString(Session["repArchivo"]);
                string NombreReporteSeleccionado = Convert.ToString(Session["repDescripcion"]);

                if (e.CommandName == "Seleccionar")
                {
                    if (File.Exists(Server.MapPath("upload/" + NombreArchivoSeleccionado.ToString() + idEmpSeleccionado.ToString() + ".xls")))
                        File.Delete(Server.MapPath("upload/" + NombreArchivoSeleccionado.ToString() + idEmpSeleccionado.ToString() + ".xls"));

                    SQLToExcelReporte("ReporteAGenerar", idRepSeleccionado.ToString() + ',' + idEmpSeleccionado.ToString(), Server.MapPath("upload/" + NombreArchivoSeleccionado.ToString() + idEmpSeleccionado.ToString() + ".xls"));

                    ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open( 'upload/" + NombreArchivoSeleccionado.ToString() + idEmpSeleccionado.ToString() + ".xls');", true);

                }

                else if (e.CommandName == "Ver Reporte")
                {
                    Response.Redirect("reporte_detalle.aspx?" + "repId=" + idRepSeleccionado + "&" + "empId=" + idEmpSeleccionado + "&" + "repDescripcion=" + NombreReporteSeleccionado);
                }

            }
        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }

    protected void gvi_RowDataBoundReporte(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow Fila = ((System.Data.DataRowView)e.Row.DataItem).Row;
                ImageButton _myButton = (ImageButton)e.Row.FindControl("imgVer");
                ImageButton _myButtonSeleccionar = (ImageButton)e.Row.FindControl("imgSeleccionar");

                _myButtonSeleccionar.ImageUrl = "images/a-editar02.png";


                if (Fila["repId"].ToString().ToLower() == "5")
                {
                    _myButton.ImageUrl = "images/view_page.png";
                    _myButton.ToolTip = "Ver en Pantalla";
                    _myButton.CommandName = "Ver";
                    _myButton.Visible = true;
                }
                else
                {
                    _myButton.ImageUrl = "images/view_page.png";
                    _myButton.ToolTip = "Ver en Pantalla";
                    _myButton.CommandName = "Ver Reporte";
                    _myButton.Visible = true;

                }
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

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, true, Encoding.Default))
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