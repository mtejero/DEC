using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// Manejo de Capa de Datos
/// </summary>
public class CapaDatos
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CapaDatos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Devuelve un Datatable a partir de un SqlDataReader.    
    /// </summary>
    /// <returns></returns>
    public static DataTable DataReaderToDataTable(SqlDataReader rdrReader)
    {
        try
        {
            //Table Schema
            DataTable schemaTable = new DataTable();
            schemaTable = rdrReader.GetSchemaTable();
            //Data Table
            DataTable dataTable = new DataTable();


            //Now to create the Schema on the DataTable
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                //Current Row
                DataRow dataRow = schemaTable.Rows[i];
                //Current Column Name
                string columnName = dataRow["ColumnName"].ToString();
                //Current Column
                DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);

                //Add Column to the DataTable
                if (!dataTable.Columns.Contains(column.ColumnName))
                    dataTable.Columns.Add(column);
            }

            //Now to fill the table with the reader
            while (rdrReader.Read())
            {
                //New Row
                DataRow dataRow = dataTable.NewRow();
                //Loop the fields
                for (int i = 0; i < rdrReader.FieldCount; i++)
                {
                    //Insert the current value of the DataReader to the DataRow
                    dataRow[rdrReader.GetName(i)] = rdrReader.GetValue(i);
                }

                //Insert the Row into the DataTable
                dataTable.Rows.Add(dataRow);
            }

            //Close the reader
            rdrReader.Close();
            //Return the DataTable
            return dataTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataTable EjecutarReader(string strNombreSP, object[,] arr)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(strNombreSP, oCnx))
                    {
                        DataTable oDT = new DataTable();
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        SqlParameter oPar;
                        Comando.CommandTimeout = 1000;

                        #region Parametros
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Length / 2; i++)
                            {
                                oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                                Comando.Parameters.Add(oPar);

                                oPar = null;
                            }
                        }
                        #endregion

                        Comando.CommandType = CommandType.StoredProcedure;
                        SqlDataReader oR = Comando.ExecuteReader();
                        oDT = DataReaderToDataTable(oR);

                        oR = null;

                        return oDT;
                    }
                }
            }
            catch (SqlException ex)
            {
                //string fileName = "c:\\error.txt";
                //// esto inserta texto en un archivo existente, si el archivo no existe lo crea
                //StreamWriter writer = File.AppendText(fileName);
                //writer.Write(ex.Message);
                //writer.Close();

                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return null;
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataTable EjecutarReaderReportes(string strNombreSP, object[,] arr)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        SqlTransaction Transaccion = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(strNombreSP, oCnx))
                    {
                        DataTable oDT = new DataTable();
                        oCnx.Open();
                        Transaccion = oCnx.BeginTransaction(IsolationLevel.ReadUncommitted);
                        Comando.Transaction = Transaccion;

                        Comando.Parameters.Clear();
                        SqlParameter oPar;
                        Comando.CommandTimeout = 120;

                        #region Parametros
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Length / 2; i++)
                            {
                                oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                                Comando.Parameters.Add(oPar);

                                oPar = null;
                            }
                        }
                        #endregion

                        Comando.CommandType = CommandType.StoredProcedure;
                        SqlDataReader oR = Comando.ExecuteReader();
                        oDT = DataReaderToDataTable(oR);

                        oR = null;
                        Transaccion.Commit();

                        return oDT;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                    try
                    {
                        Transaccion.Rollback();
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    try
                    {
                        Transaccion.Rollback();
                    }
                    catch (Exception)
                    { }
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return null;
    }

    // <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataTable EjecutarReaderReportes2(string strNombreSP, object[,] arr)
    {
        try
        {
            string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            SqlDataAdapter Adaptador;
            SqlCommand Comando;
            SqlConnection Conexion;
            DataSet DS;

            Conexion = new SqlConnection(ConexionCadena);
            Comando = new SqlCommand();
            Adaptador = new SqlDataAdapter();
            DS = new DataSet();

            SqlParameter oPar;
            Comando.Parameters.Clear();
            #region Parametros
            if (arr != null)
            {
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                    Comando.Parameters.Add(oPar);

                    oPar = null;
                }
            }
            #endregion

            Comando.Connection = Conexion;
            Comando.CommandTimeout = 120;
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = strNombreSP;
            Adaptador.SelectCommand = Comando;
            Conexion.Open();

            Adaptador.Fill(DS, "Tabla");

            Conexion.Close();


            return DS.Tables[0];
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Ejecuta SQL desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">SQL</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataTable EjecutarReader(string SQL)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(SQL, oCnx))
                    {
                        DataTable oDT = new DataTable();
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        Comando.CommandType = CommandType.Text;
                        SqlDataReader oR = Comando.ExecuteReader();
                        oDT = DataReaderToDataTable(oR);
                        oR = null;
                        return oDT;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                //string fileName = "c:\\error.txt";
                //// esto inserta texto en un archivo existente, si el archivo no existe lo crea
                //StreamWriter writer = File.AppendText(fileName);
                //writer.Write(e.Message);
                //writer.Close();
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return null;
    }

    /// <summary>
    /// Ejecuta SQL desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">SQL</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataTable EjecutarReaderAudio(string SQL)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionStringAudio"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(SQL, oCnx))
                    {
                        DataTable oDT = new DataTable();
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        Comando.CommandType = CommandType.Text;
                        SqlDataReader oR = Comando.ExecuteReader();
                        oDT = DataReaderToDataTable(oR);
                        oR = null;
                        return oDT;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                //string fileName = "c:\\error.txt";
                //// esto inserta texto en un archivo existente, si el archivo no existe lo crea
                //StreamWriter writer = File.AppendText(fileName);
                //writer.Write(e.Message);
                //writer.Close();
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return null;
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>    
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un entero.</returns>
    public static int EjecutarScalar(string strNombreSP, object[,] arr)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(strNombreSP, oCnx))
                    {
                        int iResul = 0;
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        SqlParameter oPar;
                        Comando.CommandTimeout = 300;

                        #region Parametros
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Length / 2; i++)
                            {
                                oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                                Comando.Parameters.Add(oPar);

                                oPar = null;
                            }
                        }
                        #endregion

                        Comando.CommandType = CommandType.StoredProcedure;
                        iResul = int.Parse(Comando.ExecuteScalar().ToString());

                        return iResul;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return 0;
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>    
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un entero.</returns>
    public static int EjecutarScalar(string SQL)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(SQL, oCnx))
                    {
                        int iResul = 0;
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        Comando.CommandType = CommandType.Text;
                        iResul = int.Parse(Comando.ExecuteScalar().ToString());

                        return iResul;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return 0;
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>    
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un entero.</returns>
    public static int EjecutarScalarAudio(string SQL)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionStringAudio"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(SQL, oCnx))
                    {
                        int iResul = 0;
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        Comando.CommandType = CommandType.Text;
                        iResul = int.Parse(Comando.ExecuteScalar().ToString());

                        return iResul;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return 0;
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>    
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>    
    public static void EjecutarNonQuery(string strNombreSP, object[,] arr)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(strNombreSP, oCnx))
                    {
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        SqlParameter oPar;
                        Comando.CommandTimeout = 300;

                        #region Parametros
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Length / 2; i++)
                            {
                                oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                                Comando.Parameters.Add(oPar);

                                oPar = null;
                            }
                        }
                        #endregion

                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.ExecuteNonQuery().ToString();
                        reintentos = 5;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>    
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>    
    public static void EjecutarNonQuery(string SQL)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(SQL, oCnx))
                    {
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        Comando.CommandType = CommandType.Text;
                        Comando.ExecuteNonQuery();
                        reintentos = 5;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
    }

    /// <summary>
    /// Ejecuta SP desde la presentación.
    /// </summary>
    /// <param name="tipo">Tipo de Ejecución de la Query</param>
    /// <param name="strNombreSP">Nombre del SP</param>
    /// <param name="arr">Array de Parametros de la forma {{"nombreParametro", valorParametro},{...,...},{...,...}}</param>
    /// <returns>Retorna un DataTable con los resultados.</returns>
    public static DataSet DevolverDS(string strNombreSP, object[,] arr)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;
        short reintentos = 0;
        while (reintentos < 5)
        {
            try
            {
                using (oCnx = new SqlConnection(ConexionCadena))
                {
                    using (SqlCommand Comando = new SqlCommand(strNombreSP, oCnx))
                    {
                        DataSet oDS = new DataSet();
                        oCnx.Open();

                        Comando.Parameters.Clear();
                        Comando.CommandTimeout = 300;
                        SqlParameter oPar;
                        SqlDataAdapter Adaptador = new SqlDataAdapter();

                        #region Parametros
                        if (arr != null)
                        {
                            for (int i = 0; i < arr.Length / 2; i++)
                            {
                                oPar = new SqlParameter(arr[i, 0].ToString(), arr[i, 1]);
                                Comando.Parameters.Add(oPar);

                                oPar = null;
                            }
                        }
                        #endregion

                        Comando.CommandType = CommandType.StoredProcedure;
                        Adaptador.SelectCommand = Comando;

                        //Conexion.Open();
                        Adaptador.Fill(oDS, "Catalogo");

                        Adaptador.Dispose();

                        return oDS;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                {
                    reintentos++;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                oCnx.Close();
            }
        }
        return null;
    }

    public static DataSet DevolverDS(string Sql)
    {
        string ConexionCadena = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection oCnx = null;

        try
        {
            using (oCnx = new SqlConnection(ConexionCadena))
            {
                using (SqlCommand Comando = new SqlCommand(Sql, oCnx))
                {
                    DataSet DS = new DataSet();
                    SqlDataAdapter Adaptador = new SqlDataAdapter();
                    Comando.Connection = oCnx;
                    Comando.CommandTimeout = 300;
                    Comando.CommandType = CommandType.Text;
                    Comando.CommandText = Sql;
                    Adaptador.SelectCommand = Comando;

                    oCnx.Open();


                    Adaptador.Fill(DS, "Tabla");
                    Adaptador.Dispose();


                    return DS;
                }
            }
        }
        catch (System.Exception Error)
        {
            throw Error;
        }
        finally
        {
            oCnx.Close();

        }
    }

    /// <summary>
    /// Desencriptar
    /// </summary>
    /// <param name="conectionstring"></param>
    /// <returns></returns>
    public static string GetParValue(string conectionstring)
    {
        string textDecrypt = string.Empty;
        try
        {
            //RijndaelManaged
            string key = "Rdy/obTlKObAUKni/+R+EQ7O0H/vMP66Srsnrr7U9gY=";
            SymmetricAlgorithm algorithm = new RijndaelManaged();

            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;

            ICryptoTransform decryptor = algorithm.CreateDecryptor();

            byte[] data = Convert.FromBase64String(conectionstring);

            byte[] dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);

            textDecrypt = Encoding.Unicode.GetString(dataDecrypted);

            return textDecrypt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
