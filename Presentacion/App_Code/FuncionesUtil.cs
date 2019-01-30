using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Reflection;
using System.Data.SqlClient;

/// <summary>
/// Funciones Utiles que usan la mayoria de los Formularios del Sistema
/// </summary>
public class FuncionesUtil
{
    public FuncionesUtil()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #region --[Enumeradores]--
    public enum DateStringFormat
    {
        mmddyyyy,
        mmddyy,
        ddmmyy,
        ddmmyyyy
    }
    public enum UtilityLanguage
    {
        ENG,
        ESP
    }
    #endregion

    #region Metodos
    public bool validarContenido(DataTable dt)
    {
        if (dt.Columns.Contains("Resultado"))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Desencripta queryString
    /// </summary>
    /// <param name="stringToDecrypt"></param>
    /// <returns></returns>
    public string decryptQueryString(string stringToDecrypt)
    {
        try
        {
            byte[] strBytes = System.Convert.FromBase64String(stringToDecrypt);
            string str = System.Text.Encoding.UTF8.GetString(strBytes, 0, strBytes.Length);
            return str;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Encripta queryString
    /// </summary>
    /// <param name="stringToEncrypt"></param>
    /// <returns></returns>
    public string encryptQueryString(string stringToEncrypt)
    {
        try
        {
            byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(stringToEncrypt);
            string str = System.Convert.ToBase64String(strBytes);
            return str;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Imprime Filtros y Grilla con todos los valores. 
    /// </summary>
    /// <param name="stringToEncrypt"></param>
    /// <returns></returns>
    public string ImprimirPantallaConsulta(string pUsuario, string pFiltro, DataView dtImp, GridView gvi, string pNombreReporte, string pValorFiltro, int pCantidadRegistros)
    {
        try
        {
            //*** Variables ***
            string HTMLReturn = string.Empty;

            //*** Crea tablas de la pagina html ***
            HTMLReturn = "<table width='800px'><tr><td align='center' width='180px'>&nbsp;</td>" +
            "<td width='400px' colspan='2' align='center'><font face=Arial size=5>Reporte " +
            pNombreReporte + "</font></td>" +
            "<td align='right' width='220px'><table width='100%'><tr>" +
            "<td align='left' width='220px'><b>&nbsp;Fecha:&nbsp;</b>" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
            "</td></tr></td></tr><tr><td align='left' width='220px'><b>&nbsp;Usuario:&nbsp;</b>" + pUsuario +
            "</td></tr></table></td></tr></table>" +
            "<table width='800px'><tr><td colspan='2'>&nbsp;</td></tr><tr><td style='background:silver;' align='center' width='180px'><b>&nbsp;Filtros Cargados:&nbsp;</b></td>" +
            "<td>&nbsp;</td></tr>";

            //*** Filtros ***
            HTMLReturn += "<tr><td align='right' width='180px'>";
            HTMLReturn += "<b> " + pFiltro + ": </b></td>";
            HTMLReturn += "<td align='left'>&nbsp;" + pValorFiltro + "</td>";
            HTMLReturn += "</tr></table><br>";

            //*** Cantidad de Resultados ***
            HTMLReturn += "<tr><td align='right' width='180px'>";
            HTMLReturn += "<b> Cantidad de Registros: </b></td>";
            HTMLReturn += "<td align='left'>&nbsp;" + pCantidadRegistros.ToString() + "</td>";
            HTMLReturn += "</tr></table><br>";

            //*** Crea Tabla ***
            HTMLReturn += "<TABLE id='Tabla' cellSpacing='1' cellPadding='1' border='1'>";

            //*** Recorre tabla y pinta Cabeceras de Columna
            HTMLReturn += "<tr>";
            for (int j = 0; j < gvi.Columns.Count; j++)
            {
                if (gvi.Columns[j].HeaderStyle.Width.Value > 0)
                {
                    HTMLReturn += "<td align='center' style='background:silver;' width='" + gvi.Columns[j].HeaderStyle.Width + "'><b>";
                    HTMLReturn += gvi.Columns[j].HeaderText;
                    HTMLReturn += "</b></td>";
                }
            }
            HTMLReturn += "</tr>";

            //*** Recorre tabla y pinta columnas y filas
            for (int i = 0; i < dtImp.Count; i++)
            {
                HTMLReturn += "<tr>";
                for (int j = 0; j < gvi.Columns.Count; j++)
                {
                    if (gvi.Columns[j].HeaderStyle.Width.Value > 0)
                    {
                        BoundField bfield = (BoundField)gvi.Columns[j];
                        HTMLReturn += "<td align='center'>";
                        HTMLReturn += dtImp[i][bfield.DataField].ToString();
                        HTMLReturn += "</td>";
                    }
                }
                HTMLReturn += "</tr>";
            }

            //*** Escribe el cierre de la tabla ***
            HTMLReturn += "</TABLE>";

            return HTMLReturn;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>Lee el contenido de un archivo de texto</summary>
    /// <param name="wFile">nombre del archivo. Ej: "a.txt", "carpeta\\b.html"</param>
    /// <returns>string con el contenido del archivo</returns>        
    public static string StringFromTextFile(string fileName)
    {
        try
        {
            StreamReader wSR = File.OpenText(fileName);
            string wFile = wSR.ReadToEnd();
            wSR.Close();
            return wFile;
        }
        catch (Exception)
        {
            return "";
        }
    }

    #region --Varias--
    public static DateTime ConvertStringToDateTime_mmddyyyy(string date_mmddyyyy)
    {
        System.Globalization.CultureInfo wCulture = new System.Globalization.CultureInfo("en-us");
        DateTime wResult = Convert.ToDateTime(date_mmddyyyy, wCulture);
        return wResult;
    }
    public static string ConvertDateTimeToString(DateTime date, DateStringFormat format)
    {
        return ConvertDateTimeToString(date, format, "/");
    }
    public static string ConvertDateTimeToString20(DateTime date, DateStringFormat format)
    {
        return ConvertDateTimeToString20(date, format, "/");
    }
    public static string ConvertDateTimeToString(DateTime date, DateStringFormat format, string separator)
    {
        if (date == DateTime.MinValue)
            return (string.Empty);
        string wSeparator = separator;
        string wResult = string.Empty;
        string wDay = date.Day.ToString();
        string wMonth = date.Month.ToString();
        string wYear = date.Year.ToString();
        switch (format)
        {
            case DateStringFormat.mmddyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wYear = wYear.Substring(2);
                wResult = wMonth + separator + wDay + separator + wYear;
                break;
            case DateStringFormat.mmddyyyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wResult = wMonth + separator + wDay + separator + wYear;
                break;
            case DateStringFormat.ddmmyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wYear = wYear.Substring(2);
                wResult = wDay + separator + wMonth + separator + wYear;
                break;
            case DateStringFormat.ddmmyyyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wResult = wDay + separator + wMonth + separator + wYear;
                break;
        }
        return wResult;
    }
    public static string ConvertDateTimeToString20(DateTime date, DateStringFormat format, string separator)
    {
        if (date == DateTime.MinValue)
            return (string.Empty);
        string wSeparator = separator;
        string wResult = string.Empty;
        string wDay = date.Day.ToString();
        string wMonth = date.Month.ToString();
        string wYear = date.Year.ToString();
        switch (format)
        {
            case DateStringFormat.mmddyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wYear = wYear.Substring(2);
                wResult = wMonth + separator + "20" + separator + wYear;
                break;
            case DateStringFormat.mmddyyyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wResult = wMonth + separator + "20" + separator + wYear;
                break;
            case DateStringFormat.ddmmyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wYear = wYear.Substring(2);
                wResult = "20" + separator + wMonth + separator + wYear;
                break;
            case DateStringFormat.ddmmyyyy:
                if (wMonth.Length == 1)
                    wMonth = "0" + wMonth;
                if (wDay.Length == 1)
                    wDay = "0" + wDay;
                wResult = "20" + separator + wMonth + separator + wYear;
                break;
        }
        return wResult;
    }
    public static string GetMonthName(int monthNumber, UtilityLanguage language)
    {
        if (language == UtilityLanguage.ENG)
        {
            switch (monthNumber)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }
        }
        if (language == UtilityLanguage.ESP)
        {
            switch (monthNumber)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
            }
        }
        return string.Empty;
    }
    public static string GetMonthName(int monthNumber)
    {
        return GetMonthName(monthNumber, UtilityLanguage.ENG);
    }
    public static string GetDayOfWeekName(DateTime date, UtilityLanguage language)
    {
        string wDayOfWeekNameENG = date.DayOfWeek.ToString();
        if (language == UtilityLanguage.ENG)
        {
            return wDayOfWeekNameENG;
        }
        if (language == UtilityLanguage.ESP)
        {
            switch (wDayOfWeekNameENG.ToLower())
            {
                case "monday":
                    return "Lunes";
                case "tuesday":
                    return "Martes";
                case "wednesday":
                    return "Miércoles";
                case "thursday":
                    return "Jueves";
                case "friday":
                    return "Viernes";
                case "saturday":
                    return "Sábado";
                case "sunday":
                    return "Domingo";
            }
        }
        return string.Empty;
    }
    public static string GetDayOfWeekName(DateTime date)
    {
        return string.Empty;
    }
    public static bool IsNumeric(string wExpression)
    {
        bool result = true;
        try
        {
            int wIntValue = Convert.ToInt32(wExpression);
        }
        catch
        {
            result = false;
        }
        return result;
    }
    #endregion

    /// <summary>
    /// Valida los Emails
    /// </summary>
    public bool validarEmail(string email)
    {
        string expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Valida números decimales, si es correcto retorna true
    /// </summary>
    public bool validarDecimal(string numero)
    {
        decimal d;

        if (decimal.TryParse(numero, out d) == false)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida números enteros, si es correcto retorna true
    /// </summary>
    public bool validarEntero(string numero)
    {
        int i;

        if (Int32.TryParse(numero, out i) == false)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida números enteros, si es correcto retorna true
    /// </summary>
    public bool validarFecha(string fecha)
    {
        try
        {
            DateTime f;
            CultureInfo ci = new CultureInfo("es-AR");

            if (DateTime.TryParse(fecha, ci, System.Globalization.DateTimeStyles.None, out f) == false)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Valida CUIT, si es correcto retorna true
    /// </summary>
    public bool validarCuit(string cuit)
    {
        try
        {
            string p2;
            string p8;
            string p1;
            int i;

            p2 = cuit.Substring(0, 2);
            p8 = cuit.Substring(3, 8);
            p1 = cuit.Substring(12, 1);

            if (Int32.TryParse(p2, out i) == false)
            {
                return false;
            }
            if (Int32.TryParse(p8, out i) == false)
            {
                return false;
            }
            if (Int32.TryParse(p1, out i) == false)
            {
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Funcion que encripta un string en base al algoritmo SHA512
    /// </summary>
    /// <param name="clave">Valor string que se desea encriptar</param>
    /// <returns></returns>
    public string GetHash(string pValor)
    {
        UnicodeEncoding encoder = null;
        SHA512Managed SHA512 = null;
        try
        {
            encoder = new UnicodeEncoding();
            SHA512 = new SHA512Managed();
            //- Encripto y retorno el valor correspondiente
            return Convert.ToBase64String(SHA512.ComputeHash(encoder.GetBytes(pValor)));
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Obtiene un nro de Cuit y devuelve con el formato correspondiente
    /// </summary>
    /// <param name="cuit"></param>
    /// <returns></returns>
    public string DevolverNroCuit(string cuit)
    {
        string rta = string.Empty;
        rta = cuit.Substring(0, 2) + "-" + cuit.Substring(2, 8) + "/" + cuit.Substring(10, 1);
        return rta;
    }

    /// <summary>
    /// Imagen en seccion izquierda
    /// </summary>
    /// <param name="nombrePantalla"></param>
    /// <returns></returns>
    public string[] DevolverInfoSeccion(string nombrePantalla)
    {
        string[] infoSeccion = new string[2];

        infoSeccion[0] = "background: url('img/h3_" + nombrePantalla.ToLower() + ".png') no-repeat 8% 0px;";
        infoSeccion[1] = nombrePantalla;

        return infoSeccion;
    }

    /// <summary>
    /// Devolver Contenido Combo Año
    /// </summary>
    /// <param name="nombrePantalla"></param>
    /// <returns></returns>
    public DataTable DevolverContenidoComboAño(int iCantidadAños)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("Valor");
        dt.Columns.Add("Seleccionado");

        //años anteriores
        for (int i = iCantidadAños; i > 0; i--)
        {
            DataRow filaAnterior = dt.NewRow();
            filaAnterior[0] = Convert.ToString(DateTime.Now.Year - i);
            filaAnterior[1] = Convert.ToString(DateTime.Now.Year - i);
            filaAnterior[2] = "0";
            dt.Rows.Add(filaAnterior);
        }

        //año actual
        DataRow fila = dt.NewRow();
        fila[0] = Convert.ToString(DateTime.Now.Year);
        fila[1] = Convert.ToString(DateTime.Now.Year);
        fila[2] = "1";
        dt.Rows.Add(fila);

        //años siguientes
        for (int i = 1; i < iCantidadAños; i++)
        {
            DataRow filaSiguiente = dt.NewRow();
            filaSiguiente[0] = Convert.ToString(DateTime.Now.Year + i);
            filaSiguiente[1] = Convert.ToString(DateTime.Now.Year + i);
            filaSiguiente[2] = "0";
            dt.Rows.Add(filaSiguiente);
        }

        return dt;
    }

    /// <summary>
    /// Devolver Fecha ddMMyyyy
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public string DevolverFecha_ddMMyyyy(string fecha)
    {
        try
        {
            string sFecha = string.Empty;
            CultureInfo ci = new CultureInfo("es-AR");

            DateTime aux = Convert.ToDateTime(fecha, ci);

            if (aux.Day.ToString().Length == 1)
                sFecha = "0" + aux.Day.ToString() + "/";
            else
                sFecha = aux.Day.ToString() + "/";

            if (aux.Month.ToString().Length == 1)
                sFecha += "0" + aux.Month.ToString() + "/" + aux.Year.ToString();
            else
                sFecha += aux.Month.ToString() + "/" + aux.Year.ToString();

            return sFecha;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
        return DateTime.Now.ToShortDateString();
    }

    /// <summary>
    /// Devolver Fecha ddMMyyyy
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public string DevolverFecha_yyyyMMdd(DateTime aux)
    {
        try
        {
            string sFecha = string.Empty;

            if (aux.Month.ToString().Length == 1)
                sFecha = aux.Year.ToString() + "-" + "0" + aux.Month.ToString();
            else
                sFecha = aux.Year.ToString() + "-" + aux.Month.ToString();

            if (aux.Day.ToString().Length == 1)
                sFecha += "-" + "0" + aux.Day.ToString();
            else
                sFecha += "-" + aux.Day.ToString();

            return sFecha;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
        return DateTime.Now.ToShortDateString();
    }

    /// <summary>
    /// Devolver Id Caja
    /// </summary>
    /// <param name="fecha"></param>
    /// <returns></returns>
    public int DevolverCaja(string usuId, string usuIdioma)
    {
        try
        {
            int Id_Caja = 0;
            string strSqlCaja = "";
            if (usuId == "41" || usuId == "42")
                strSqlCaja = "select * from caja, cajadetalle, usuario where abierta = 1 and.caja.id_turno = usuario.usuid and caja.Id_Caja = cajadetalle.Id_Caja and usuario.usuId in (41,42)";
            else
                strSqlCaja = "select * from caja, cajadetalle, usuario where abierta = 1 and.caja.id_turno = usuario.usuid and caja.Id_Caja = cajadetalle.Id_Caja";
            DataTable dtCaja = CapaDatos.EjecutarReader(strSqlCaja);
            if (dtCaja.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                if (dtCaja.Rows.Count == 1)
                {
                    if (usuIdioma == dtCaja.Rows[0]["usuIdioma"].ToString())
                        Id_Caja = int.Parse(dtCaja.Rows[0]["Id_Caja"].ToString());
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    bool banAux = false;
                    for (int i = 0; i < dtCaja.Rows.Count; i++)
                    {
                        if (dtCaja.Rows[i]["Id_Turno"].ToString() == usuId)
                        {
                            Id_Caja = int.Parse(dtCaja.Rows[i]["Id_Caja"].ToString());
                            banAux = true;
                        }
                    }
                    if (!banAux)
                    {
                        for (int i = 0; i < dtCaja.Rows.Count; i++)
                        {
                            if (dtCaja.Rows[i]["ccaCompartida"].ToString() == "1")
                            {
                                if (usuIdioma == dtCaja.Rows[i]["usuIdioma"].ToString())
                                {
                                    Id_Caja = int.Parse(dtCaja.Rows[i]["Id_Caja"].ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return Id_Caja;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
        }
        return 0;
    }


    //public void validarUsuario(bool Login) {
    //    if (Login)
    //    {
    //      http
    //    }
    //}

    #endregion
}
