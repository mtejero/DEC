using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class dashboard : System.Web.UI.Page
{

    private string _Grafico;
    public string Grafico
    {
        get
        {
            return _Grafico;
        }
        set
        {
            _Grafico = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string empId = Request.QueryString["empId"];
        try
        {
            if (!IsPostBack)
            {


                if ((bool)Session["login"])
                {

                    CargarComboEmpresa();

                    if (string.IsNullOrEmpty(empId))
                    {
                        divGrafico1.Visible = false;
                        divGrafico2.Visible = false;
                        lblTitulo.Text = "";
                    }
                    else
                    {
                        DataTable dt = CapaDatos.EjecutarReader(@"select empNombre from Empresa where empId= " + Request.Params["empId"].ToString());
                        if (dt.Rows != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                lblTitulo.Text = "Gráficos Dashboard: " + dt.Rows[i]["empNombre"].ToString();
                            }
                        }
                        else
                        {
                            lblTitulo.Text = "";
                        }
                        IniciarGrafico();
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

    private static string CrearGraficoComun(string sTitulo, string tipo, DataSet oDs, string DivDestino)
    {
        string sGrafico = string.Empty;
        #region Inicializa el grafico

        //int divNumbre= int.
        //$(function () {
        sGrafico = @" <div id='" + DivDestino + @"'></div><script type='text/javascript'>
                        var chart;
                        $(document).ready(function() {
                            chart = new Highcharts.Chart({
                                chart: {
                                    renderTo: '" + DivDestino + @"',
                                    type: '" + tipo + @"'
                                    },";

        #endregion

        #region Agrega el titulo

        sGrafico += @" title: {
                      text: '" + sTitulo + @"'
                  },";

        #endregion

        #region Agrega xAxis

        sGrafico += @"xAxis: {
                                type: 'category'
                            },
                          legend:   {
                                        enabled: false
                                    },";

        #endregion

        #region Agrega credits

        sGrafico += @"credits: {
            text: 'ProContactWeb',
            href: 'http://www.procontact.com.ar/'
        },";


        #endregion

        #region Agrega yAxis


        #endregion

        #region Agrega plotOptions

        sGrafico += @"plotOptions: {
            series: {
                dataLabels: {
                    enabled: true
                }
            }
        },";


        #endregion

        #region Agrega DrillDrown

        int j = 1;
        sGrafico += @" drilldown: {";
        sGrafico += @" series: [";
        foreach (DataRow row in oDs.Tables[0].Rows)
        {

            if (row["DrillDown"].ToString() == "si")
            {
                sGrafico += @"{
                                        id: '" + row["Tipo"].ToString() + "', name: '" + row["Tipo"].ToString() + "',";
                sGrafico += "data:[";
                foreach (DataRow rowD in oDs.Tables[j].Rows)
                {
                    sGrafico += "['" + rowD["Tipo"].ToString() + "(" + rowD["Cantidad"].ToString() + ")'," + rowD["Cantidad"].ToString() + "],";
                }
                sGrafico += "]";

                sGrafico += @"},";

                j++;
            }

        }

        sGrafico += @" ]},";

        #endregion

        #region Agrega Series

        sGrafico += @" series: [{
                                name: 'Bloque',
                                colorByPoint: true,
                                data: [";

        foreach (DataRow row in oDs.Tables[0].Rows)
        {
            sGrafico += @"{";
            sGrafico += @" name: '" + row["Tipo"].ToString() + "(" + row["Cantidad"].ToString() + ")',";
            sGrafico += @" y: " + row["Cantidad"].ToString() + ",";

            if (row["DrillDown"].ToString() == "si")
            {
                sGrafico += @" drilldown: '" + row["Tipo"].ToString() + "',";
            }
            sGrafico += @" },";

        }

        sGrafico += @"]}]";

        #endregion

        #region Cierre de Script

        sGrafico += @" }); }); </script>";


        #endregion

        return sGrafico;
    }

    protected void IniciarGrafico()
    {
        string selectedValueG1 = rbGraficoGrupo1.SelectedValue;
        string selectedValueG2 = rbGraficoGrupo2.SelectedValue;
        string sGraficoG1 = string.Empty;
        string sGraficoG2 = string.Empty;
        string sTituloG1 = "Valorización Por Bloque y Factores";
        string sTituloG2 = "Composición por edad, sexo, nivel de estudio, antigüedad y pregunta de formación por si o por no";

        int idEmp = Convert.ToInt32(Request.Params["empId"].ToString());

        object[,] oParametros = {
                               {"@EmpId", idEmp },
                            };

        DataSet dtG1 = CapaDatos.DevolverDS("[GraficoValorizacionxBloqueyFactor]", oParametros);
        DataSet dtG2 = CapaDatos.DevolverDS("[GraficoReporteComposicionEdadSNivelEst]", oParametros);

        if (selectedValueG1 == "Torta")
        {
            sGraficoG1 = CrearGraficoComun(sTituloG1, "pie", dtG1, "grafico1");
            grafico1.InnerHtml = sGraficoG1;

        }
        else if (selectedValueG1 == "Barra")
        {
            sGraficoG1 = CrearGraficoComun(sTituloG1, "bar", dtG1, "grafico1");
            grafico1.InnerHtml = sGraficoG1;
        }
        else if (selectedValueG1 == "Columna")
        {
            sGraficoG1 = CrearGraficoComun(sTituloG1, "column", dtG1, "grafico1");
            grafico1.InnerHtml = sGraficoG1;
        }

        if (selectedValueG2 == "Torta")
        {
            sGraficoG2 = CrearGraficoComun(sTituloG2, "pie", dtG2, "grafico2");
            grafico2.InnerHtml = sGraficoG2;
        }
        else if (selectedValueG2 == "Barra")
        {
            sGraficoG2 = CrearGraficoComun(sTituloG2, "bar", dtG2, "grafico2");
            grafico2.InnerHtml = sGraficoG2;
        }
        else if (selectedValueG2 == "Columna")
        {
            sGraficoG2 = CrearGraficoComun(sTituloG2, "column", dtG2, "grafico2");
            grafico2.InnerHtml = sGraficoG2;
        }


    }

    protected void rbGraficoGrupo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = rbGraficoGrupo1.SelectedValue;
        string sGrafico = string.Empty;
        string sTitulo = "Valorización Por Bloque y Factores";

        int idEmp = Convert.ToInt32(Request.Params["empId"].ToString());

        object[,] oParametros = {
                               {"@EmpId", idEmp },
                            };

        DataSet dt = CapaDatos.DevolverDS("[GraficoValorizacionxBloqueyFactor]", oParametros);

        if (selectedValue == "Torta")
        {
            sGrafico = CrearGraficoComun(sTitulo, "pie", dt, "grafico1");
            grafico1.InnerHtml = sGrafico;

        }
        else if (selectedValue == "Barra")
        {
            sGrafico = CrearGraficoComun(sTitulo, "bar", dt, "grafico1");
            grafico1.InnerHtml = sGrafico;
        }
        else if (selectedValue == "Columna")
        {
            sGrafico = CrearGraficoComun(sTitulo, "column", dt, "grafico1");
            grafico1.InnerHtml = sGrafico;
        }
    }

    protected void rbGraficoGrupo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = rbGraficoGrupo2.SelectedValue;

        string sGrafico = string.Empty;
        string sTitulo = "Composición por edad, sexo, nivel de estudio, antigüedad y pregunta de formación por si o por no";

        int idEmp = Convert.ToInt32(Request.Params["empId"].ToString());

        object[,] oParametros = {
                               {"@EmpId", idEmp },
                            };

        DataSet dt = CapaDatos.DevolverDS("[GraficoReporteComposicionEdadSNivelEst]", oParametros);


        if (selectedValue == "Torta")
        {
            sGrafico = CrearGraficoComun(sTitulo, "pie", dt, "grafico2");
            grafico2.InnerHtml = sGrafico;
        }
        else if (selectedValue == "Barra")
        {
            sGrafico = CrearGraficoComun(sTitulo, "bar", dt, "grafico2");
            grafico2.InnerHtml = sGrafico;
        }
        else if (selectedValue == "Columna")
        {
            sGrafico = CrearGraficoComun(sTitulo, "column", dt, "grafico2");
            grafico2.InnerHtml = sGrafico;
        }
    }
    /// <param name="dt"></param>
    private void CargarComboEmpresa()
    {
        try
        {

            ddlEmpresaDash.Items.Clear();
            ListItem wItem = new ListItem();
            wItem = new ListItem("Empresa", "-1");
            ddlEmpresaDash.Items.Add(wItem);

            DataTable dt = CapaDatos.EjecutarReader("select * from Empresa");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wItem = new ListItem(dt.Rows[i]["empNombre"].ToString(), dt.Rows[i]["empId"].ToString());
                ddlEmpresaDash.Items.Add(wItem);
            }
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
    }

    protected void ddlEmpresaDash_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            int empId = Convert.ToInt32(ddlEmpresaDash.SelectedItem.Value);
            Response.Redirect("dashboard.aspx?empId=" + empId);
        }
        catch (Exception ex)
        {
            string eror = ex.Message;
        }
    }
}


