using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reporte_semaforo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            bool login = Session["login"] != null ? true : false;

            if (login)
            {
                // Request.Params["empId"].ToString()
                int idRep = Convert.ToInt32(Request.Params["empId"].ToString());


                DataTable dtTitulo = CapaDatos.EjecutarReader(@"select empNombre from Empresa where empId= " + idRep);
                if (dtTitulo.Rows != null)
                {
                    for (int i = 0; i < dtTitulo.Rows.Count; i++)
                    {
                        lblTituloSemaforo.Text = "Resultado general por Momentos con semaforo" + ": " + dtTitulo.Rows[i]["empNombre"].ToString();
                    }
                }
                else
                {
                    lblTituloSemaforo.Text = "Resultado general por Momentos con semaforo  ";
                }


                object[,] oParametros = {
                        {"@empId", idRep},
                    };
                DataTable dt = CapaDatos.EjecutarReader("SemaforoResultadoxMomentoIndividual", oParametros);
                DataTableReader dr = new DataTableReader(dt);
                rptSemaforo.DataSource = dt;
                rptSemaforo.DataBind();


            }
            else
            {
                Response.Redirect("Error.aspx?e=2", true);
            }




            //StringBuilder sb = new StringBuilder();

            //while (dr.Read())

            //{

            //    // Given a DataTableReader, display column names.

            //    for (int i = 0; i < dr.FieldCount; i++)

            //    {

            //        sb.Append("\r\n" + dr.GetName(i) + ": ");



            //        if (dr.IsDBNull(i))

            //        {

            //            sb.Append("<NULL>");

            //        }

            //        else

            //        {

            //            try

            //            {

            //                sb.Append(dr.GetValue(i).ToString());

            //            }

            //            catch (InvalidCastException)

            //            {

            //                sb.Append("<Invalid data type>");

            //            }

            //        }

            //    }

            //    sb.Append("\r\n");

            //}





        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}