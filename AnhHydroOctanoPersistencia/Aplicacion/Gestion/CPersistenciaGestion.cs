using System;
using System.Configuration;
using System.Data;
using AnhHydroOctanoPersistencia.Entidades;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AnhHydroOctanoPersistencia.Aplicacion.Gestion
{
    public class CPersistenciaGestion
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

        public EPResultado TareasMovimientos(decimal decFiltro, decimal decReintentos, decimal decDato1,
                decimal decDato2, decimal decDato3, decimal decDato4, decimal decDato5)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_TAREAS_MOVIMIENTOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decReintentos,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decDato1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decDato2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decDato3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decDato4,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, decDato5,
                            ParameterDirection.Input));
                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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

        //borrar si se utiliza el servicio
        public EPResultado RegistraControlesUsuario(decimal decFechaInicio, decimal decFechaFin, decimal decEstado,
                decimal decTipoControl, string strObservaciones, decimal decIdUsuario, decimal decIdTipoActividad,
                decimal decIdUbicacion, decimal decAppFechaRegistro, decimal decAppIdUsuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_CONTROLES_USUARIO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, decFechaInicio,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decFechaFin,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decEstado,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decTipoControl,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Varchar2, strObservaciones,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decIdUsuario,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, decIdTipoActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, decIdUbicacion,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Int32, decAppFechaRegistro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, decAppIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("11", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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
        public EPResultado EliminaControlesUsuario(string strCredencial, decimal decIdControlUsuario, decimal decAppFechaRegistro, decimal decAppIdUsuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ELIMINA_CONTROLES_USUARIO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdControlUsuario,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decAppFechaRegistro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decAppIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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
        //

        public EPResultado RegistraReporteVariable(
            string strCredencial,
            string strObservaciones,
            decimal decIdReportePrinc,
            decimal decIdTipoActividad,
            decimal decOrden,
            string strCodigo,
            string strNombre,
            string strDescripcion,
            decimal decAppIdUsuario,
            decimal decAppFechaRegistro
            )
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_REPORTES_VARIABLES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, strObservaciones,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdReportePrinc,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdTipoActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decOrden,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, strCodigo,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Varchar2, strNombre,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, strDescripcion,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Int32, decAppIdUsuario,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, decAppFechaRegistro,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("11", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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


        public EPResultado RegistraRecepcionBsisa(
            decimal I_ID_INTEGRACION,
            decimal I_ID_CONSUMIDOR,
            decimal I_ID_TIPO_ACTIVIDAD,
            decimal I_ID_TIPO_MOVIMIENTO,
            decimal I_FECHA_OPERACION,
            string I_IDENTIFICADOR,
            decimal I_ID_PRODUCTO_HYDRO,
            decimal I_CANTIDAD,
            string I_ID_TRANSACCION,
            decimal I_BARCODE,
            decimal I_ID_CODIGO_PAIS,
            decimal I_FECHA_ENVIO,
            string I_BINARIO_CONTROL,
            string I_USUARIO,
            string I_CI_USUARIO,
            string I_COD_TANQUE,
            string I_PRECINTOS,
            string I_CONDUCTOR,
            string I_CI_CONDUCTOR,
            decimal I_FECHA_DESPACHO,
            string I_NRO_DESPACHO,
            string I_NRO_ORDEN,
            string I_DESTINO,
            decimal I_VOLUMEN_PLANTA,
            decimal I_ID_TIPO_TRANSACCION,
            decimal I_NIT_CONSUMIDOR,
            decimal I_NIT_EMISOR,
            decimal I_NRO_TRANSACCION,
            decimal I_NRO_AUTORIZACION,
            string I_CODIGO_CONTROL,
            string I_RAZON_SOCIAL,
            decimal I_PRECIO_UNITARIO,
            decimal I_PRECIO_TOTAL,
            decimal I_FECHA_TRANSACCION,
            string I_OBSERVACIONES)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_RECEPCIONES_BSISA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, I_ID_INTEGRACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, I_ID_CONSUMIDOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, I_ID_TIPO_ACTIVIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, I_ID_TIPO_MOVIMIENTO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, I_FECHA_OPERACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, I_IDENTIFICADOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, I_ID_PRODUCTO_HYDRO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, I_CANTIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, I_ID_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, I_BARCODE,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, I_ID_CODIGO_PAIS,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, I_FECHA_ENVIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Varchar2, I_BINARIO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Varchar2, I_USUARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("15", OracleDbType.Varchar2, I_CI_USUARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("16", OracleDbType.Varchar2, I_COD_TANQUE,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("17", OracleDbType.Varchar2, I_PRECINTOS,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("18", OracleDbType.Varchar2, I_CONDUCTOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("19", OracleDbType.Varchar2, I_CI_CONDUCTOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("20", OracleDbType.Int32, I_FECHA_DESPACHO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("21", OracleDbType.Varchar2, I_NRO_DESPACHO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("22", OracleDbType.Varchar2, I_NRO_ORDEN,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("23", OracleDbType.Varchar2, I_DESTINO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("24", OracleDbType.Int32, I_VOLUMEN_PLANTA,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("25", OracleDbType.Int32, I_ID_TIPO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("26", OracleDbType.Int32, I_NIT_CONSUMIDOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("27", OracleDbType.Int32, I_NIT_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("28", OracleDbType.Int32, I_NRO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("29", OracleDbType.Int32, I_NRO_AUTORIZACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("30", OracleDbType.Varchar2, I_CODIGO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("31", OracleDbType.Varchar2, I_RAZON_SOCIAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("32", OracleDbType.Int32, I_PRECIO_UNITARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("33", OracleDbType.Int32, I_PRECIO_TOTAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("34", OracleDbType.Int32, I_FECHA_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("35", OracleDbType.Varchar2, I_OBSERVACIONES,
                            ParameterDirection.Input));
                        //var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        //cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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

        public EPResultado RegistraTransAdicBsisa(
            decimal I_ID_MOVIMIENTO,
            decimal I_ID_CONSUMIDOR,
            decimal I_ID_INTEGRACION,
            decimal I_ID_TIPO_TRANSACCION,
            decimal I_NRO_TRANSACCION,
            string I_NRO_AUTORIZACION,
            decimal I_TIPO_CREDITO_FISCAL,
            decimal I_PRECIO_UNITARIO,
            decimal I_IMPORTE,
            decimal I_ID_FORMA_PAGO,
            decimal I_ID_TIPO_EMISOR,
            string I_DETALLE_DOC,
            decimal I_FECHA_TRANSACCION,
            decimal I_FECHA_ENVIO,
            string I_OBSERVACIONES,
            decimal I_ID_TIPO_ACTIVIDAD,
            decimal I_FECHA_OPERACION,
            string I_CODIGO)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_TRANS_ADIC_BSISA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, I_ID_MOVIMIENTO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, I_ID_CONSUMIDOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, I_ID_INTEGRACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, I_ID_TIPO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, I_NRO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, I_NRO_AUTORIZACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, I_TIPO_CREDITO_FISCAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, I_PRECIO_UNITARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Int32, I_IMPORTE,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, I_ID_FORMA_PAGO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, I_ID_TIPO_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Varchar2, I_DETALLE_DOC,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, I_FECHA_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, I_FECHA_ENVIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("15", OracleDbType.Varchar2, I_OBSERVACIONES,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("16", OracleDbType.Int32, I_ID_TIPO_ACTIVIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("17", OracleDbType.Int32, I_FECHA_OPERACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("18", OracleDbType.Varchar2, I_CODIGO,
                            ParameterDirection.Input));
                        //var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        //cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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

        public EPResultado RegistraTransaccionBsisa(
            decimal I_ID_TRANSACCION_PADRE,
            decimal I_ID_MOVIMIENTO,
            decimal I_ID_TIPO_TRANSACCION,
            decimal I_ID_ESTADO_TRANSACCION,
            decimal I_FECHA_HORA_EMISION,
            decimal I_NIT_CONSUMIDOR,
            decimal I_NIT_EMISOR,
            string I_NRO_TRANSACCION,
            decimal I_NRO_AUTORIZACION,
            string I_CODIGO_CONTROL,
            string I_RAZON_SOCIAL,
            decimal I_PRECIO_UNITARIO,
            decimal I_PRECIO_TOTAL,
            decimal I_CODIGO_SEGURIDAD,
            string I_USUARIO_TRANSACCION,
            string I_OBSERVACIONES,
            decimal I_ID_TIPO_CREDITO_FISCAL,
            decimal I_ID_FORMA_PAGO,
            decimal I_ID_TIPO_MONEDA,
            decimal I_ID_TIPO_EMISOR,
            decimal I_ID_AUTORIZACION_DATO,
            string I_BINARIO_CONTROL,
            decimal I_ID_TIPO_ACTIVIDAD,
            decimal I_FECHA_OPERACION,
            string I_CODIGO,
            decimal I_APP_ID_USUARIO,
            decimal I_APP_FECHA_REGISTRO)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_TRANSACCION_BSISA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, I_ID_TRANSACCION_PADRE,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, I_ID_MOVIMIENTO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, I_ID_TIPO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, I_ID_ESTADO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, I_FECHA_HORA_EMISION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, I_NIT_CONSUMIDOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, I_NIT_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, I_NRO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Int32, I_NRO_AUTORIZACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Varchar2, I_CODIGO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Varchar2, I_RAZON_SOCIAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, I_PRECIO_UNITARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, I_PRECIO_TOTAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, I_CODIGO_SEGURIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("15", OracleDbType.Varchar2, I_USUARIO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("16", OracleDbType.Varchar2, I_OBSERVACIONES,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("17", OracleDbType.Int32, I_ID_TIPO_CREDITO_FISCAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("18", OracleDbType.Int32, I_ID_FORMA_PAGO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("19", OracleDbType.Int32, I_ID_TIPO_MONEDA,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("20", OracleDbType.Int32, I_ID_TIPO_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("21", OracleDbType.Int32, I_ID_AUTORIZACION_DATO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("22", OracleDbType.Varchar2, I_BINARIO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("23", OracleDbType.Int32, I_ID_TIPO_ACTIVIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("24", OracleDbType.Int32, I_FECHA_OPERACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("25", OracleDbType.Varchar2, I_CODIGO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("26", OracleDbType.Int32, I_APP_ID_USUARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("27", OracleDbType.Int32, I_APP_FECHA_REGISTRO,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("28", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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


        public EPResultado RegistraTransaccionIndividual(
            decimal I_ID_TRANSACCION_PADRE,
            string I_ID_MOVIMIENTO,
            decimal I_ID_TIPO_TRANSACCION,
            decimal I_ID_ESTADO_TRANSACCION,
            decimal I_FECHA_HORA_EMISION,
            decimal I_NIT_CONSUMIDOR,
            decimal I_NIT_EMISOR,
            string I_NRO_TRANSACCION,
            decimal I_NRO_AUTORIZACION,
            string I_CODIGO_CONTROL,
            string I_RAZON_SOCIAL,
            decimal I_PRECIO_UNITARIO,
            decimal I_PRECIO_TOTAL,
            decimal I_CODIGO_SEGURIDAD,
            string I_USUARIO_TRANSACCION,
            string I_OBSERVACIONES,
            decimal I_ID_TIPO_CREDITO_FISCAL,
            decimal I_ID_FORMA_PAGO,
            decimal I_ID_TIPO_MONEDA,
            decimal I_ID_TIPO_EMISOR,
            decimal I_ID_AUTORIZACION_DATO,
            string I_BINARIO_CONTROL,
            decimal I_APP_ID_USUARIO,
            decimal I_APP_FECHA_REGISTRO,
            decimal I_FECHA_OPERACION,
            decimal I_ID_TIPO_ACTIVIDAD,
            string I_CODIGO)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_TRANSACCION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, I_ID_TRANSACCION_PADRE,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, I_ID_MOVIMIENTO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, I_ID_TIPO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, I_ID_ESTADO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, I_FECHA_HORA_EMISION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, I_NIT_CONSUMIDOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, I_NIT_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, I_NRO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Int32, I_NRO_AUTORIZACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Varchar2, I_CODIGO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Varchar2, I_RAZON_SOCIAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, I_PRECIO_UNITARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, I_PRECIO_TOTAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, I_CODIGO_SEGURIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("15", OracleDbType.Varchar2, I_USUARIO_TRANSACCION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("16", OracleDbType.Varchar2, I_OBSERVACIONES,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("17", OracleDbType.Int32, I_ID_TIPO_CREDITO_FISCAL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("18", OracleDbType.Int32, I_ID_FORMA_PAGO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("19", OracleDbType.Int32, I_ID_TIPO_MONEDA,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("20", OracleDbType.Int32, I_ID_TIPO_EMISOR,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("21", OracleDbType.Int32, I_ID_AUTORIZACION_DATO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("22", OracleDbType.Varchar2, I_BINARIO_CONTROL,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("23", OracleDbType.Int32, I_APP_ID_USUARIO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("24", OracleDbType.Int32, I_APP_FECHA_REGISTRO,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("25", OracleDbType.Int32, I_FECHA_OPERACION,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("26", OracleDbType.Int32, I_ID_TIPO_ACTIVIDAD,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("27", OracleDbType.Varchar2, I_CODIGO,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("28", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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
        public EPResultado RegistraIncidentes(string strgCredencial,
            decimal decIdOperador,
            decimal decIdActividad,
            decimal decIdTipoIncidente,
            decimal decIdAreaUnidad,
            string strAreaUnidad,
            string strIncidente,
            DateTime dtFechaInicioSuceso,
            DateTime dtFechaFinSuceso,
            DateTime dtFechaReinicio,
            decimal decAppIdUsuario,
            decimal decIdPeriodo,
            decimal decGestion,
            decimal decIdCalsificador
            )
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_INCIDENTE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdOperador, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdActividad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdTipoIncidente, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decIdAreaUnidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, strAreaUnidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Varchar2, strIncidente, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Date, dtFechaInicioSuceso, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Date, dtFechaFinSuceso, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Date, dtFechaReinicio, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, decAppIdUsuario, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, decIdPeriodo, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, decGestion, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, decIdCalsificador, ParameterDirection.Input));
                        var op0 = new OracleParameter("14", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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
        public EPResultado ModificarIncidentes(string strgCredencial,
            decimal decIdIncidente,
            decimal decIdOperador,
            decimal decIdActividad,
            decimal decIdTipoIncidente,
            decimal decIdAreaUnidad,
            string strAreaUnidad,
            string strIncidente,
            DateTime dtFechaInicioSuceso,
            DateTime dtFechaFinSuceso,
            DateTime dtFechaReinicio,
            decimal decAppFechaRegistro,
            decimal decAppIdUsuario,
            decimal decGestion,
            decimal decIdCalsificador)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_MODIFICA_INCIDENTE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdIncidente, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdOperador, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdActividad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decIdTipoIncidente, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decIdAreaUnidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Varchar2, strAreaUnidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, strIncidente, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Date, dtFechaInicioSuceso, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Date, dtFechaFinSuceso, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Date, dtFechaReinicio, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, decAppFechaRegistro, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, decAppIdUsuario, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, decGestion, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("15", OracleDbType.Int32, decIdCalsificador, ParameterDirection.Input));
                        var op0 = new OracleParameter("16", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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
        public EPResultado EliminaIncidentes(string strCredencial, decimal decIdIncidente, decimal decAppIdUsuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ELIMINA_INCIDENTE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdIncidente,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decAppIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
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
        public EPResultado RegistraRendmientos(string strgCredencial, O_RENDIMIENTO_VOL_CTY objRegistroRendimiento)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_RENDIMIENTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, objRegistroRendimiento.ID_OPERADOR, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Date, objRegistroRendimiento.FECHA_REGISTRO, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, objRegistroRendimiento.JSON_REGIS_MENSUALES, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, objRegistroRendimiento.PERIODO, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, objRegistroRendimiento.GESTION, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, objRegistroRendimiento.APP_USUARIO, ParameterDirection.Input));
                        var op0 = new OracleParameter("8", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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
        public EPResultado ModificarRendimientos(string strgCredencial, O_RENDIMIENTO_VOL_CTY objRegistroRendimiento)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_MODIFICA_RENDIMIENTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, objRegistroRendimiento.ID_RENDIMIEN_VOLUMET, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, objRegistroRendimiento.ID_OPERADOR, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Date, objRegistroRendimiento.FECHA_REGISTRO, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, objRegistroRendimiento.JSON_REGIS_MENSUALES, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, objRegistroRendimiento.PERIODO, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, objRegistroRendimiento.GESTION, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, objRegistroRendimiento.APP_USUARIO, ParameterDirection.Input));
                        var op0 = new OracleParameter("16", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        //using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        //{
                        //    while (re.Read())
                        //    {
                        //        if (re.HasRows)
                        //        {
                        //var reg = new O_RESULTADO_CTY();
                        //reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                        //reg.MENSAJE_ERROR = re[2].ToString();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
                        //    }
                        //}
                        //re.Close();
                        //op0.Dispose();
                        //}
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
        public EPResultado EliminaRendmientos(string strCredencial, decimal decIdRendimiento, decimal decAppIdUsuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ELIMINA_RENDIMIENTO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdRendimiento,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decAppIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
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

        public O_DATO_RESULTADO_CTY ActualizarSaldoMesDepartamento(decimal decFiltro, decimal decIdDepartamento)
        {
            var objDatos = new List<O_REPORTE_ENT_ACTDEP_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            var reg = new O_DATO_RESULTADO_CTY();

            try
            {
               

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ACTUALIZA_SALDO_AC";
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, decFiltro, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdDepartamento, ParameterDirection.Input));
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    
                                    reg.ID_TABLA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.RESULTADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.MENSAJE_ERROR = re[2].ToString();
                                    /*_resultado.intCodigo = 1;
                                    _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                                    _resultado.oResultado = null;*/
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        return reg;
                        //return _resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                reg.ID_TABLA = -100;
                reg.RESULTADO = -100;
                reg.MENSAJE_ERROR = ex.Message;

                /*_resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;*/
                return reg;
            }
        }

        public EPResultado ImportaMovimientoBSA(string strCredencial, decimal decIdEess, decimal decIdConsumidor, decimal decFechaIni, decimal decFechaFin)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_INTEGRACION.P_SINCRONIZAR_BSA_MOV";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdEess,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdConsumidor,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decFechaIni,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decFechaFin,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("6", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();                        
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "ACCIÓN EJECUTADA CORRECTAMENTE";
                        _resultado.oResultado = null;
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
        public EPResultado ImportaProgramacionDPE(List<lstImportacionDatos> ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {                              
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_PROGRAMACION_PDE";
                                 
                        cmd.CommandType = CommandType.StoredProcedure;  
                        
                        var lcount = ob.Count;
                        cmd.ArrayBindCount = lcount;

                        var q01 = ob.Select(q => q.decIdActividad).ToArray();
                        var q02 = ob.Select(q => q.decIdConsumidor).ToArray();
                        var q03 = ob.Select(q => q.decPeriodo).ToArray();
                        var q04 = ob.Select(q => q.decIdProducto).ToArray();
                        var q05 = ob.Select(q => q.decTipoOperacion).ToArray();
                        var q06 = ob.Select(q => q.decCantidad).ToArray();
                        var q07 = ob.Select(q => q.decUsuarioAplicacion).ToArray();
                        var q08 = ob.Select(q => q.decConsumidorOrigen).ToArray(); 
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, q01, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, q02, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, q03, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, q04, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, q05, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Double, q06, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, q07, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, q08, ParameterDirection.Input));
                        var op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();                

                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "SE REGISTRO LA PROGRAMACION PRODE CORRECTAMENTE";
                        _resultado.oResultado = null;
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
        public EPResultado RegistrarRecepcionesDJ(decimal decFiltro, decimal decReintentos, decimal decDato1, decimal decDato2, decimal decDato3, decimal decDato4, decimal decDato5)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_TAREAS_MOVIMIENTOS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decReintentos,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decDato1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decDato2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decDato3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decDato4,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, decDato5,
                            ParameterDirection.Input));
                        cmd.ExecuteNonQuery();
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = null;
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

        public EPResultado ActualizaRecepcionesEstado(string strCredencial, decimal decFiltro, decimal decEstado, decimal decFechaIni, decimal decFechaFin, decimal decIdConsumidor, decimal decIdActividad, decimal decTipoOperacion, string strObs, decimal decAppFechaReg, decimal decIdusuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ACTUALIZA_RECEPCIONES_ESTADO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                          ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decEstado,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decFechaIni,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decFechaFin,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decIdConsumidor,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, decIdActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, decTipoOperacion,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, strObs,
                           ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, decAppFechaReg,
                           ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, decIdusuario,
                           ParameterDirection.Input));
                        var op0 = new OracleParameter("12", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();              
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    _resultado.intCodigo = re.GetInt32(0);
                                    _resultado.strMensaje = re[1].ToString(); 
                                    _resultado.oResultado = re[2].ToString(); 
                       
                                }
                            }
                            re.Close();
                            op0.Dispose();
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

        public EPResultado PdeCierrePeriodoEstado(string strCredencial, decimal decFiltro, decimal decCodigoBarras, decimal decIdActividad, decimal decPeriodo, decimal decAppEstado, decimal decAppIdUsuario)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_CERRAR_PERIODO_PDE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial,
                          ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decCodigoBarras,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decPeriodo,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, decAppEstado,
                            ParameterDirection.Input));                
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, decAppIdUsuario,
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
                                    _resultado.intCodigo = re.GetInt32(0);
                                    _resultado.strMensaje = re[1].ToString();
                                    _resultado.oResultado = re[2].ToString();

                                }
                            }
                            re.Close();
                            op0.Dispose();
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
         public EPResultado RegistraPrecioImpBSA(string strCredencial, 
                                                 decimal decIdEess, 
                                                 decimal decIdEntidad, 
                                                 decimal decIdProducto, 
                                                 double decVolumen,
                                                 double decPrecio, 
                                                 string strImportadora, 
                                                 string strCodDispenser, 
                                                 string strCodManguera,
                                                 decimal decFechaDesde, 
                                                 decimal decFechaHasta, 
                                                 string strObs,
                                                 decimal decEstado
                                                 )
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_REGISTRA_PRECIO_BSA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdEess, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdEntidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdProducto, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Double, decVolumen, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Double, decPrecio, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Varchar2, strImportadora, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, strCodDispenser, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, strCodManguera, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Int32, decFechaDesde, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, decFechaHasta, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Varchar2, strObs, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Int32, decEstado, ParameterDirection.Input));
                        var op0 = new OracleParameter("14", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    _resultado.intCodigo = re.GetInt32(0);
                                    _resultado.strMensaje = re[1].ToString();
                                    _resultado.oResultado = re[2].ToString();

                                }
                            }
                            re.Close();
                            op0.Dispose();
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

        public EPResultado ActualizaPrecioImpBSA(string strCredencial,
                                              decimal decIdPrecioImp, 
                                              decimal decIdEess, 
                                              decimal decIdEntidad, 
                                              decimal decIdProducto, 
                                              double decVolumen,
                                              double decPrecio, 
                                              string strImportadora, 
                                              string strCodDispenser, 
                                              string strCodManguera,
                                              decimal decFechaDesde, 
                                              decimal decFechaHasta, 
                                              string strObs,
                                              decimal decEstado)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ACTUALIZA_PRECIO_BSA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdPrecioImp, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decIdEess, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decIdEntidad, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, decIdProducto, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Double, decVolumen, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Double, decPrecio, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, strImportadora, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, strCodDispenser, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Varchar2, strCodManguera, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("11", OracleDbType.Int32, decFechaDesde, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("12", OracleDbType.Int32, decFechaHasta, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("13", OracleDbType.Varchar2, strObs, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("14", OracleDbType.Int32, decEstado, ParameterDirection.Input));
                        var op0 = new OracleParameter("15", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    _resultado.intCodigo = re.GetInt32(0);
                                    _resultado.strMensaje = re[1].ToString();
                                    _resultado.oResultado = re[2].ToString();

                                }
                            }
                            re.Close();
                            op0.Dispose();
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
        public EPResultado EliminarPrecioImpBSA(string strCredencial, decimal decIdPrecioImp)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_GESTION.P_ELIMINA_PRECIO_BSA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strCredencial, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdPrecioImp, ParameterDirection.Input));
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    _resultado.intCodigo = re.GetInt32(0);
                                    _resultado.strMensaje = re[1].ToString();
                                    _resultado.oResultado = re[2].ToString();

                                }
                            }
                            re.Close();
                            op0.Dispose();
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
