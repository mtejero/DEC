﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class reporte_detalle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            bool login = Session["login"] != null ? true : false;

            if (login)
            {
                gviCargarResultadoReporte();
                btnVolver.NavigateUrl = "reporte.aspx?empId=" + Request.Params["empId"].ToString();

                DataTable dt = CapaDatos.EjecutarReader(@"select empNombre from Empresa where empId= " + Request.Params["empId"].ToString());
                if (dt.Rows != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lblTituloReporte.Text = "Reporte  " + Request.Params["repDescripcion"].ToString() + ": " + dt.Rows[i]["empNombre"].ToString();
                    }
                }
                else
                {
                    lblTituloReporte.Text = "Reporte  " + Request.Params["repDescripcion"].ToString();
                }
            }
            else
            {
                Response.Redirect("error.aspx?e=2", true);
            }


        }
    }
    protected void gviCargarResultadoReporte()
    {
        try
        {
            int idRep = Convert.ToInt32(Request.Params["repId"].ToString());
            int idEmp = Convert.ToInt32(Request.Params["empId"].ToString());
            string ProcEjec = "";

            if (idRep == 5)
            {
                ProcEjec = "General";
            }

            object[,] oParametros = {
                        {"@RepId", idRep},
                        {"@EmpId", idEmp },
                        {"@ProcEjec", ProcEjec}
                    };

            DataTable dt = CapaDatos.EjecutarReader("[ReporteAGenerar]", oParametros);

            gviResultadoReporte.DataSource = dt;
            gviResultadoReporte.DataBind();

        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }

}