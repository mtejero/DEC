using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Base;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.Web.ASPxGauges.Gauges.State;
using DevExpress.XtraGauges.Base;
using System.Data;

public partial class Reporte : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            DataTable dtTitulo = CapaDatos.EjecutarReader(@"select empNombre from Empresa where empId= " + Request.Params["empId"].ToString());
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

            float ValorVerde =0;
            float ValorAmarillo = 0;
            float ValorRojo = 0;
            float VVerdeAdq = 0;
            float VAmarilloAdq = 0;
            float VRojoAdq = 0;

            float VVerdeSer = 0;
            float VAmarilloSer = 0;
            float VRojoSer = 0;

            float VVerdeAtr = 0;
            float VAmarilloAtr = 0;
            float VRojoAtr = 0;

            float VVerdeFid = 0;
            float VAmarilloFid = 0;
            float VRojoFid = 0;

            float VVerdeMax = 0;
            float VAmarilloMax = 0;
            float VRojoMax = 0;

            float VVerdeRec = 0;
            float VAmarilloRec = 0;
            float VRojoRec = 0;

            float VVerdeRet = 0;
            float VAmarilloRet = 0;
            float VRojoRet = 0;

            float VVerdeSeg = 0;
            float VAmarilloSeg = 0;
            float VRojoSeg = 0;

            int idRep = Convert.ToInt32(Request.Params["repId"].ToString());
            int idEmp = Convert.ToInt32(Request.Params["empId"].ToString());
            string ProcEjec = "";

            if (idRep == 5) {
                ProcEjec = "General";
            }

            object[,] oParametros = {
                        {"@RepId", idRep},
                        {"@EmpId", idEmp },
                        {"@ProcEjec", ProcEjec}
                    };

            DataTable dt = CapaDatos.EjecutarReader("[ReporteAGenerar]", oParametros);

            if (dt.Rows.Count > 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Color"].ToString() == "Rojo")
                    {
                        lblEncuestaRojo.Text = dt.Rows[i]["PromedioMomento"].ToString() + "%";
                        //ASPxGaugeControlEncuesta.Value = "1";
                         ValorRojo = Convert.ToSingle(dt.Rows[i]["PromedioMomento"].ToString());

                    }

                    if (dt.Rows[i]["Color"].ToString() == "Amarillo")
                    {
                        lblEncuestaAmarillo.Text = dt.Rows[i]["PromedioMomento"].ToString() + "%";
                        //ASPxGaugeControlEncuesta.Value = "2";
                         ValorAmarillo = Convert.ToSingle(dt.Rows[i]["PromedioMomento"].ToString());
                    }

                    if (dt.Rows[i]["Color"].ToString() == "Verde")
                    {
                        lblEncuestaVerde.Text = dt.Rows[i]["PromedioMomento"].ToString() + "%";
                       // ASPxGaugeControlEncuesta.Value = "3";
                         ValorVerde = Convert.ToSingle(dt.Rows[i]["PromedioMomento"].ToString());
                    }

                    if (ValorRojo > ValorAmarillo && ValorRojo > ValorVerde)
                    {
                        ASPxGaugeControlEncuesta.Value = "1";
                    }

                    if (ValorAmarillo > ValorRojo && ValorAmarillo > ValorVerde)
                    {
                        ASPxGaugeControlEncuesta.Value = "2";
                    }

                    if (ValorVerde > ValorAmarillo && ValorVerde > ValorRojo)
                    {
                        ASPxGaugeControlEncuesta.Value = "3";
                    }


                }
                
                if (lblEncuestaRojo.Text == "") {

                    lblEncuestaRojo.Text = "0,00 %";
                }
                if (lblEncuestaAmarillo.Text == "")
                {
                    lblEncuestaAmarillo.Text = "0,00 %";
                }
                if (lblEncuestaVerde.Text == "")
                {
                    lblEncuestaVerde.Text = "0,00 %";
                }
            }
            ASPxGaugeControlEncuesta.DataBind();

            //aca iria los ciclos individuales
            if (idRep == 5)
            {
                ProcEjec = "Individual";
            }

            object[,] oParametrosInd = {
                        {"@RepId", idRep},
                        {"@EmpId", idEmp },
                        {"@ProcEjec", ProcEjec}
                    };

            DataTable dtInd = CapaDatos.EjecutarReader("[ReporteAGenerar]", oParametrosInd);
            if (dtInd.Rows.Count > 0) {
                for (int i = 0; i < dtInd.Rows.Count; i++) {

                    if (dtInd.Rows[i]["Momento"].ToString() == "ADQUISICION") {

                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo") {
                            lblEncuestaRojoAdq.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoAdq = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloAdq.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloAdq = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeAdq.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeAdq = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoAdq > VAmarilloAdq && VRojoAdq > VVerdeAdq)
                    {
                        ASPxGaugeControlEncuestaAdq.Value = "1";
                     
                    }

                    if (VAmarilloAdq > VRojoAdq && VAmarilloAdq > VVerdeAdq)
                    {
                        ASPxGaugeControlEncuestaAdq.Value = "2";
                    }

                    if (VVerdeAdq > VAmarilloAdq && VVerdeAdq > VRojoAdq)
                    {
                        ASPxGaugeControlEncuestaAdq.Value = "3";
                    }
                    ASPxGaugeControlEncuestaAdq.DataBind();


                    if (lblEncuestaRojoAdq.Text == "")
                    {
                        lblEncuestaRojoAdq.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloAdq.Text == "")
                    {
                        lblEncuestaAmarilloAdq.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeAdq.Text == "")
                    {
                        lblEncuestaVerdeAdq.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "SERVICIO")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoSer.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoSer = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloSer.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloSer = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeSer.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeSer = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoSer > VAmarilloSer && VRojoSer > VVerdeSer)
                    {
                        ASPxGaugeControlEncuestaSer.Value = "1";                    
                    }

                    if (VAmarilloSer > VRojoSer && VAmarilloSer > VVerdeSer)
                    {
                        ASPxGaugeControlEncuestaSer.Value = "2";
                    }

                    if (VVerdeSer > VAmarilloSer && VVerdeSer > VRojoSer)
                    {
                        ASPxGaugeControlEncuestaSer.Value = "3";
                    }
                    ASPxGaugeControlEncuestaSer.DataBind();

                    if (lblEncuestaRojoSer.Text == "")
                    {
                        lblEncuestaRojoSer.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloSer.Text == "")
                    {
                        lblEncuestaAmarilloSer.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeSer.Text == "")
                    {
                        lblEncuestaVerdeSer.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "ATRACCION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoAtr.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoAtr = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloAtr.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloAtr = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeAtr.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeAtr = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoAtr > VAmarilloAtr && VRojoAtr > VVerdeAtr)
                    {
                        ASPxGaugeControlEncuestaAtr.Value = "1";

                    }

                    if (VAmarilloAtr > VRojoAtr && VAmarilloAtr > VVerdeAtr)
                    {
                        ASPxGaugeControlEncuestaAtr.Value = "2";
                    }

                    if (VVerdeAtr > VAmarilloAtr && VVerdeAtr > VRojoAtr)
                    {
                        ASPxGaugeControlEncuestaAtr.Value = "3";
                    }
                    ASPxGaugeControlEncuestaAtr.DataBind();

                    if (lblEncuestaRojoAtr.Text == "")
                    {
                        lblEncuestaRojoAtr.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloAtr.Text == "")
                    {
                        lblEncuestaAmarilloAtr.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeAtr.Text == "")
                    {
                        lblEncuestaVerdeAtr.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "FIDELIZACION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoFid.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoFid = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloFid.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloFid = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeFid.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeFid  = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                    }

                    if (VRojoFid > VAmarilloFid && VRojoFid > VVerdeFid)
                    {
                        ASPxGaugeControlEncuestaFid.Value = "1";
                    }

                    if (VAmarilloFid > VRojoFid && VAmarilloFid > VVerdeFid)
                    {
                        ASPxGaugeControlEncuestaFid.Value = "2";
                    }

                    if (VVerdeFid > VAmarilloFid && VVerdeFid > VRojoFid)
                    {
                        ASPxGaugeControlEncuestaFid.Value = "3";
                    }
                    ASPxGaugeControlEncuestaFid.DataBind();

                    if (lblEncuestaRojoFid.Text == "")
                    {
                        lblEncuestaRojoFid.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloFid.Text == "")
                    {
                        lblEncuestaAmarilloFid.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeFid.Text == "")
                    {
                        lblEncuestaVerdeFid.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "MAXIMIZACION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoMax.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoMax = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloMax.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloMax = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeMax.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeMax = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoMax > VAmarilloMax && VRojoMax > VVerdeMax)
                    {
                        ASPxGaugeControlEncuestaMax.Value = "1";
                    }

                    if (VAmarilloMax > VRojoMax && VAmarilloMax > VVerdeMax)
                    {
                        ASPxGaugeControlEncuestaMax.Value = "2";
                    }

                    if (VVerdeMax > VAmarilloMax && VVerdeMax > VRojoMax)
                    {
                        ASPxGaugeControlEncuestaMax.Value = "3";
                    }
                    ASPxGaugeControlEncuestaMax.DataBind();

                    if (lblEncuestaRojoMax.Text == "")
                    {
                        lblEncuestaRojoMax.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloMax.Text == "")
                    {
                        lblEncuestaAmarilloMax.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeMax.Text == "")
                    {
                        lblEncuestaVerdeMax.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "RECUPERACION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoRec.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoRec = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloRec.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloRec = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeRec.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeRec = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoRec > VAmarilloRec && VRojoRec > VVerdeRec)
                    {
                        ASPxGaugeControlEncuestaRec.Value = "1";
                    }

                    if (VAmarilloRec > VRojoRec && VAmarilloRec > VVerdeRec)
                    {
                        ASPxGaugeControlEncuestaRec.Value = "2";
                    }

                    if (VVerdeRec > VAmarilloRec && VVerdeRec > VRojoRec)
                    {
                        ASPxGaugeControlEncuestaRec.Value = "3";
                    }
                    ASPxGaugeControlEncuestaRec.DataBind();

                    if (lblEncuestaRojoRec.Text == "")
                    {
                        lblEncuestaRojoRec.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloRec.Text == "")
                    {
                        lblEncuestaAmarilloRec.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeRec.Text == "")
                    {
                        lblEncuestaVerdeRec.Text = "0,00 %";
                    }

                    if (dtInd.Rows[i]["Momento"].ToString() == "RETENCION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoRet.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoRet = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloRet.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloRet = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeRet.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeRet = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                    }

                    if (VRojoRet > VAmarilloRet && VRojoRet > VVerdeRet)
                    {
                        ASPxGaugeControlEncuestaRet.Value = "1";

                    }

                    if (VAmarilloRet > VRojoRet && VAmarilloRet > VVerdeRet)
                    {
                        ASPxGaugeControlEncuestaRet.Value = "2";
                    }

                    if (VVerdeRet > VAmarilloRet && VVerdeRet > VRojoRet)
                    {
                        ASPxGaugeControlEncuestaRet.Value = "3";
                    }
                    ASPxGaugeControlEncuestaRet.DataBind();

                    if (lblEncuestaRojoRet.Text == "")
                    {
                        lblEncuestaRojoRet.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloRet.Text == "")
                    {
                        lblEncuestaAmarilloRet.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeRet.Text == "")
                    {
                        lblEncuestaVerdeRet.Text = "0,00 %";
                    }


                    if (dtInd.Rows[i]["Momento"].ToString() == "SEGMENTACION")
                    {
                        if (dtInd.Rows[i]["Color"].ToString() == "Rojo")
                        {
                            lblEncuestaRojoSeg.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VRojoSeg = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Amarillo")
                        {
                            lblEncuestaAmarilloSeg.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VAmarilloSeg = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }
                        if (dtInd.Rows[i]["Color"].ToString() == "Verde")
                        {
                            lblEncuestaVerdeSeg.Text = dtInd.Rows[i]["PromedioMomento"].ToString() + "%";
                            VVerdeSeg = Convert.ToSingle(dtInd.Rows[i]["PromedioMomento"].ToString());
                        }

                    }

                    if (VRojoSeg > VAmarilloSeg && VRojoSeg > VVerdeSeg)
                    {
                        ASPxGaugeControlEncuestaSeg.Value = "1";
                    }

                    if (VAmarilloSeg > VRojoSeg && VAmarilloSeg > VVerdeSeg)
                    {
                        ASPxGaugeControlEncuestaSeg.Value = "2";
                    }

                    if (VVerdeSeg > VAmarilloSeg && VVerdeSeg > VRojoSeg)
                    {
                        ASPxGaugeControlEncuestaSeg.Value = "3";
                    }
                    ASPxGaugeControlEncuestaSeg.DataBind();

                    if (lblEncuestaRojoSeg.Text == "")
                    {
                        lblEncuestaRojoSeg.Text = "0,00 %";
                    }
                    if (lblEncuestaAmarilloSeg.Text == "")
                    {
                        lblEncuestaAmarilloSeg.Text = "0,00 %";
                    }
                    if (lblEncuestaVerdeSeg.Text == "")
                    {
                        lblEncuestaVerdeSeg.Text = "0,00 %";
                    }
                }
            }



        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }


    }
}