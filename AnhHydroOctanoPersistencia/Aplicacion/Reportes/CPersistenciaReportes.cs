using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using AnhHydroOctanoPersistencia.Enumeradores;
using Librerias.Anh.Us;
using Oracle.ManagedDataAccess.Types;
using ServiceStack.Text;
using AnhHydroOctanoPersistencia.Entidades;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.ManagedDataAccess.Client.OracleDataReader;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;


namespace AnhHydroOctanoPersistencia.Aplicacion.Reportes
{
    public class CPersistenciaReportes
    {
        private readonly EPResultado _resultado = new EPResultado();

        private static readonly string ConString = "User Id=" + ConfigurationManager.AppSettings["Usuario"] + "; " +
                                          "password=" + ConfigurationManager.AppSettings["Password"] + "; " +
                                          "Data Source=" + ConfigurationManager.AppSettings["Servidor"] + ":" +
                                          ConfigurationManager.AppSettings["Puerto"] + "/" +
                                          ConfigurationManager.AppSettings["BaseDatos"] + "; " +
                                          "Pooling=true; Min Pool Size=1; Max Pool Size=80; " +
                                          "Incr Pool Size=1; Decr Pool Size=1; " +
                                          "Connection Lifetime=30; Connection Timeout=20; " +     
                                          "Enlist=false;";


        public EPResultado ReporteProyectoProducto(EObjetoEnvio1ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_PROYECTO_PRODUCTO_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_PROYECTO_PRODUCTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));                        
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_PROYECTO_PRODUCTO_CTY();
                                    reg.ID_PROYECTO = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_CONSUMIDOR = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                    reg.COD_HYDRO_PROYECTO = re[2].ToString();
                                    reg.DENOMINACION_PROYECTO = re[3].ToString();
                                    reg.ID_DEPARTAMENTO = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.DEPARTAMENTO = re[5].ToString();
                                    reg.ACTIVIDAD = re[6].ToString();
                                    reg.LICENCIA = re[7].ToString();
                                    reg.PRODUCTO = re[8].ToString();                                    

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos != null && objDatos.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = objDatos;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros.";
                            _resultado.oResultado = null;
                        }
                        return _resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }

        public EPResultado ReporteMovPAHL(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_MOV_PAHL_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_MOV_PAHL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Varchar2, ob.strParametro3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, ob.strParametro4,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Varchar2, ob.strParametro5,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("8", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_MOV_PAHL_CTY();
                                    reg.FECHA_MOV = re[0].ToString(); 
                                    reg.DENOMINACION = re[1].ToString();
                                    reg.DPTO = re[2].ToString();
                                    reg.PRODUCTO = re[3].ToString();
                                    reg.EESS = re[4].ToString();                                  
                                    reg.TIPO_OPERACION = re[5].ToString();
                                    reg.IDENTIFICADOR = re[6].ToString();
                                    reg.INF_TIPO = re[7].ToString();
                                    reg.INF_CODIGO = re[8].ToString();
                                    reg.TOTAL_INGRESO = Convert.ToDecimal(re[9].ToString());
                                    reg.TOTAL_SALIDAS = Convert.ToDecimal(re[10].ToString());
                                    reg.ID_MOVIMIENTO = Convert.ToDecimal(re[11].ToString());
                                    reg.NRO_TRANSACCION = re[12].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos != null && objDatos.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = objDatos;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros.";
                            _resultado.oResultado = null;
                        }
                        return _resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }
    }
}
