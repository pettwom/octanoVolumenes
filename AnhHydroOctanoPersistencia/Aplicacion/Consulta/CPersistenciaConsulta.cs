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

using DTICUIDPPI001.Entidades;
using DTICUIDPPI001.Enumeradores;
using System.Drawing;

namespace AnhHydroOctanoPersistencia.Aplicacion.Consulta
{
    public class CPersistenciaConsulta
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

        public EPResultado ObtenerEntidades(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_OBTIENE_ENTIDADES_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTIENE_ENTIDADES_USUARIO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Varchar2, ob.strParametro4,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, ob.strParametro5,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTIENE_ENTIDADES_CTY();
                                    reg.ID_ENTIDAD_PADRE = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_USUARIO_ASIGNADO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                    reg.USUARIO_ASIGNADO = re[2].ToString();
                                    reg.DENOMINACION_PADRE = re[3].ToString();
                                    reg.ID_ENTIDAD = Convert.ToDecimal(re[4].ToString());
                                    reg.ID_CONSUMIDOR = Convert.ToDecimal(re[5].ToString());
                                    reg.DENOMINACION = re[6].ToString();
                                    reg.NIT = re[7].ToString();
                                    reg.REPRESENTANTE_LEGAL = re[8].ToString();
                                    reg.DEPARTAMENTO = re[9].ToString();
                                    reg.ID_MUNICIPIO = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());
                                    reg.ID_LOCALIDAD = (re[11].ToString() == "") ? 0 : Convert.ToDecimal(re[11].ToString());
                                    reg.ID_DEPARTAMENTO = (re[12].ToString() == "") ? 0 : Convert.ToDecimal(re[12].ToString());
                                    reg.MUNICIPIO = re[13].ToString();
                                    reg.LOCALIDAD = re[14].ToString();
                                    reg.ID_MUNICIPIO_PADRE = (re[15].ToString() == "") ? 0 : Convert.ToDecimal(re[15].ToString());
                                    reg.ID_LOCALIDAD_PADRE = (re[16].ToString() == "") ? 0 : Convert.ToDecimal(re[16].ToString());
                                    reg.MUNICIPIO_PADRE = re[17].ToString();
                                    reg.LOCALIDAD_PADRE = re[18].ToString();
                                    reg.OBJETO = re[19].ToString();
                                    reg.TELEFONOS = re[20].ToString();
                                    reg.AMBITO_OPERACION_PADRE = re[21].ToString();
                                    reg.AMBITO_OPERACION = re[22].ToString();
                                    reg.ID_TIPO_SOCIEDAD = (re[23].ToString() == "") ? 0 : Convert.ToDecimal(re[23].ToString());
                                    reg.TIPO_SOCIEDAD = re[24].ToString();
                                    reg.ID_TIPO_SOCIEDAD_PADRE = (re[25].ToString() == "") ? 0 : Convert.ToDecimal(re[25].ToString());
                                    reg.TIPO_SOCIEDAD_PADRE = re[26].ToString();
                                    reg.NOMBRES_PROPIETARIO = re[27].ToString();
                                    reg.CI_PROPIETARIO = re[28].ToString();
                                    reg.NOMBRES_REPRESENTANTE = re[29].ToString();
                                    reg.CI_REPRESENTANTE = re[30].ToString();
                                    reg.DIRECCION = re[31].ToString();
                                    reg.LATITUD = (re[32].ToString() == "") ? 0 : Convert.ToDecimal(re[32].ToString());
                                    reg.LONGITUD = (re[33].ToString() == "") ? 0 : Convert.ToDecimal(re[33].ToString());
                                    reg.ID_ACTIVIDAD = Convert.ToDecimal(re[34].ToString());
                                    reg.ACTIVIDAD = re[35].ToString();
                                    reg.NRO_LICENCIA = re[36].ToString();
                                    reg.VIGENCIA = re[37].ToString();
                                    reg.LIC_VENCIMIENTO = re[38].ToString();
                                    reg.LIC_PROD = re[39].ToString();
                                    reg.LIC_ID_PROD = re[40].ToString();
                                    reg.INF_PROV_SW = re[41].ToString();
                                    reg.NRO_HYDRO = re[42].ToString();

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

        public EPResultado ObtenerDatosDdjj(EObjetoEnvio1ParametrosProto ob)
        {
            var objDatos = new List<O_DATOS_ADICIONAL_DDJJ_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_DATOS_ADICIONAL_DDJJ";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));

                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_DATOS_ADICIONAL_DDJJ_CTY();

                                    reg.ID_ENTIDAD = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_ENTIDAD_PADRE = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                    reg.ID_AMBITO_OPERACION = (re[2].ToString() == "") ? 0 : Convert.ToDecimal(re[2].ToString());
                                    reg.AMBITO_OPERACION = re[3].ToString();
                                    reg.ID_AMBITO_OPERACION_PADRE = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.AMBITO_OPERACION_PADRE = re[5].ToString();
                                    reg.ID_TIPO_SOCIEDAD = (re[6].ToString() == "") ? 0 : Convert.ToDecimal(re[6].ToString());
                                    reg.TIPO_SOCIEDAD = re[7].ToString();
                                    reg.ID_TIPO_SOCIEDAD_PADRE = (re[8].ToString() == "") ? 0 : Convert.ToDecimal(re[8].ToString());
                                    reg.TIPO_SOCIEDAD_PADRE = re[9].ToString();
                                    reg.ID_MUNICIPIO = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());
                                    reg.MUNICIPIO = re[11].ToString();
                                    reg.ID_MUNICIPIO_PADRE = (re[12].ToString() == "") ? 0 : Convert.ToDecimal(re[12].ToString());
                                    reg.MUNICIPIO_PADRE = re[13].ToString();
                                    reg.ID_LOCALIDAD = (re[14].ToString() == "") ? 0 : Convert.ToDecimal(re[14].ToString());
                                    reg.LOCALIDAD = re[15].ToString();
                                    reg.ID_LOCALIDAD_PADRE = (re[16].ToString() == "") ? 0 : Convert.ToDecimal(re[16].ToString());
                                    reg.LOCALIDAD_PADRE = re[17].ToString();
                                    reg.NOMBRES_PROPIETARIO = re[18].ToString();
                                    reg.CI_PROPIETARIO = re[19].ToString();
                                    reg.REPRESENTANTE_LEGAL = re[20].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerCursoresDdjj(EObjetoEnvio4ParametrosProto ob)
        {
            try
            {
                var lstSaldos = new List<O_SALDO_INICIAL_DIA_CTY>();
                var lstMovAcu = new List<O_OBTENER_MOVIMIENTO_ACUM_CTY>();
                var lstTipoOp = new List<O_OBTENER_TIPO_OPE_ACUM_CTY>();

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_SALDO_INICIAL_DIA";
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

                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_SALDO_INICIAL_DIA_CTY();

                                    reg.ID_PRODUCTO = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.PRODUCTO = re[1].ToString();
                                    reg.CODIGO = re[2].ToString();
                                    reg.ID_UMEDIDA = (re[3].ToString() == "") ? 0 : Convert.ToDecimal(re[3].ToString());
                                    reg.UMEDIDA = re[4].ToString();
                                    reg.TIPO_INFRAESTRUCTURA = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.TIPO_INF = re[6].ToString();
                                    reg.ID_INFRAESTRUCTURA = (re[7].ToString() == "") ? 0 : Convert.ToDecimal(re[7].ToString());
                                    reg.INFRAESTRUCTURA = re[8].ToString();
                                    reg.INFRAESTRUCTURA_ANH = re[9].ToString();
                                    reg.SALDO = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());

                                    //reg.OBSERVACIONES = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());
                                    reg.OBSERVACIONES = "DTIC UID 0030/2020";

                                    lstSaldos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_MOVIMIENTO_ACUM";
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

                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTENER_MOVIMIENTO_ACUM_CTY();
                                    reg.FECHA_OPERACION_DIA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_HIDROCARBURO = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                    reg.ID_INFRAESTRUCTURA = (re[2].ToString() == "") ? 0 : Convert.ToDecimal(re[2].ToString());
                                    reg.TIPO_INFRAESTRUCTURA = (re[3].ToString() == "") ? 0 : Convert.ToDecimal(re[3].ToString());
                                    reg.ID_TIPO_OPERACION = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.CANTIDAD = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.ID_MOVIMIENTO = Convert.ToDecimal(re[6].ToString());
                                    //reg.PRECIO_TOTAL = (re[7].ToString() == "") ? 0 : Convert.ToDecimal(re[7].ToString());        // Esta columna se comento porque ya no devuelve por base -> se reemplazo los paquetes de produccion -> Ojo L.l 

                                    //reg.OBSERVACIONES = re[7].ToString();

                                    lstMovAcu.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_TIPO_OPE_ACUM";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTENER_TIPO_OPE_ACUM_CTY();

                                    reg.ID_TIPO_OPERACION = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.OPERACION_NOMBRE = re[1].ToString();
                                    reg.OPERACION_ORDEN = (re[2].ToString() == "") ? 0 : Convert.ToDecimal(re[2].ToString());
                                    reg.REPORTE_NOMBRE = re[3].ToString();
                                    reg.REPORTE_ORDEN = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.FACTOR_TIPO = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.OBSERVACIONES = "DTIC UID 0030/2020";
                                    lstTipoOp.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }



                var objResultado = new O_CURSORES_DDJJ();

                objResultado.lstSaldos = lstSaldos;
                objResultado.lstMovimientos = lstMovAcu;
                objResultado.lstTipoOp = lstTipoOp;

                if (objResultado.lstSaldos != null)
                {
                    _resultado.intCodigo = 1;
                    _resultado.strMensaje = "OK";
                    _resultado.oResultado = objResultado;
                }
                else
                {
                    _resultado.intCodigo = -1;
                    _resultado.strMensaje = "No se encontraron registros.";
                    _resultado.oResultado = null;
                }





                return _resultado;
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }
        public EPResultado ObtenerCursoresDdjjSaldo(EObjetoEnvio4ParametrosProto ob)
        {
            try
            {
                var lstSaldos = new List<O_SALDO_INICIAL_DIA_CTY>();
                var lstMovAcu = new List<O_OBTENER_MOVIMIENTO_ACUM_SAL>();
                var lstTipoOp = new List<O_OBTENER_TIPO_OPE_ACUM_CTY>();

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_SALDO_INICIAL_DIA";
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

                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_SALDO_INICIAL_DIA_CTY();

                                    reg.ID_PRODUCTO = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.PRODUCTO = re[1].ToString();
                                    reg.CODIGO = re[2].ToString();
                                    reg.ID_UMEDIDA = (re[3].ToString() == "") ? 0 : Convert.ToDecimal(re[3].ToString());
                                    reg.UMEDIDA = re[4].ToString();
                                    reg.TIPO_INFRAESTRUCTURA = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.TIPO_INF = re[6].ToString();
                                    reg.ID_INFRAESTRUCTURA = (re[7].ToString() == "") ? 0 : Convert.ToDecimal(re[7].ToString());
                                    reg.INFRAESTRUCTURA = re[8].ToString();
                                    reg.INFRAESTRUCTURA_ANH = re[9].ToString();
                                    reg.SALDO = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());
                                    //reg.OBSERVACIONES = (re[10].ToString() == "") ? 0 : Convert.ToDecimal(re[10].ToString());
                                    reg.OBSERVACIONES = "DTIC UID 0030/2020";

                                    lstSaldos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_MOVIMIENTO_ACUM_SAL";
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

                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTENER_MOVIMIENTO_ACUM_SAL();
                                    reg.FECHA_OPERACION_DIA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.DIA = Convert.ToDecimal(re[1].ToString());
                                    reg.DESCRIPCION_DIA = re[2].ToString();
                                    reg.MES = re[3].ToString();
                                    reg.GESTION = re[4].ToString();
                                    reg.ID_HIDROCARBURO = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.ID_INFRAESTRUCTURA = (re[6].ToString() == "") ? 0 : Convert.ToDecimal(re[6].ToString());
                                    reg.TIPO_INFRAESTRUCTURA = (re[7].ToString() == "") ? 0 : Convert.ToDecimal(re[7].ToString());
                                    reg.ID_TIPO_OPERACION = (re[8].ToString() == "") ? 0 : Convert.ToDecimal(re[8].ToString());
                                    reg.CANTIDAD = (re[9].ToString() == "") ? 0 : Convert.ToDecimal(re[9].ToString());
                                    reg.ID_MOVIMIENTO = Convert.ToDecimal(re[10].ToString());
                                    reg.PRECIO_TOTAL = (re[11].ToString() == "") ? 0 : Convert.ToDecimal(re[11].ToString());
                                    reg.DESCRIPCION_TANQUES = re[12].ToString();
                                    reg.VOLUMEN_NOMINAL = Convert.ToDecimal(re[13].ToString());
                                    reg.PROGRAMACION = Convert.ToDecimal(re[14].ToString());
                                    reg.RECEPCION_PROGRAMACION = Convert.ToDecimal(re[15].ToString());
                                    reg.VENTA_PROGRAMACION = Convert.ToDecimal(re[16].ToString());

                                    lstMovAcu.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }

                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_TIPO_OPE_ACUM";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTENER_TIPO_OPE_ACUM_CTY();

                                    reg.ID_TIPO_OPERACION = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.OPERACION_NOMBRE = re[1].ToString();
                                    reg.OPERACION_ORDEN = (re[2].ToString() == "") ? 0 : Convert.ToDecimal(re[2].ToString());
                                    reg.REPORTE_NOMBRE = re[3].ToString();
                                    reg.REPORTE_ORDEN = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.FACTOR_TIPO = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.OBSERVACIONES = "DTIC UID 0030/2020";
                                    lstTipoOp.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }



                var objResultado = new O_CURSORES_DDJJ_SAL();

                objResultado.lstSaldos = lstSaldos;
                objResultado.lstMovimientos = lstMovAcu;
                objResultado.lstTipoOp = lstTipoOp;

                if (objResultado.lstSaldos != null)
                {
                    _resultado.intCodigo = 1;
                    _resultado.strMensaje = "OK";
                    _resultado.oResultado = objResultado;
                }
                else
                {
                    _resultado.intCodigo = -1;
                    _resultado.strMensaje = "No se encontraron registros.";
                    _resultado.oResultado = null;
                }





                return _resultado;
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }

        public EPResultado ObtenerDosificacion(int intFactor, EObjetoEnvio3ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    long lngContador = 0;
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REPORTE_DOSIF";

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
                        OracleParameter op0 = new OracleParameter("6", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        var booCtrl = false;

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;

                            if (ob.decFiltro == 1)
                            {
                                var lstRegistros = new List<O_LISTA_REPORTE_DOSIF_CAB_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_REPORTE_DOSIF_CAB_CTY();

                                        op1.TIPO_OPERACION = re[0].ToString();
                                        op1.NRO_AUTORIZACION = re[1].ToString(); //re.GetDecimal(1);
                                        op1.TIPO_EMISOR = re[2].ToString();
                                        op1.TIPO_FACTURACION = re[3].ToString();
                                        op1.MONTO = re.GetDecimal(4);
                                        op1.FACTOR_CALCULO = re.GetDecimal(5);
                                        lstRegistros.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                                lngContador = lstRegistros.Count;
                            }
                            else if (ob.decFiltro == 2)
                            {
                                var lstRegistros = new List<O_LISTA_REPORTE_DOSIF_DET_CTY>();
                                long lngNoFact = 0;

                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        if (lngNoFact == 0)
                                        {
                                            lngNoFact = Convert.ToInt64(re[5].ToString());
                                        }
                                        var strFecha = "";

                                        while (intFactor == -1 && lngNoFact < Convert.ToInt64(re[5].ToString()))
                                        {
                                            var op1 = new O_LISTA_REPORTE_DOSIF_DET_CTY();

                                            strFecha = re[0].ToString();
                                            op1.FECHA_FACTURA = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                            op1.FECHA_FACTURA_DIA = op1.FECHA_FACTURA.Substring(0, 10);
                                            strFecha = re[1].ToString();
                                            op1.FECHA_MOVIMIENTO = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                            op1.NIT = 0;
                                            op1.RAZON_SOCIAL = "SALTO DE CORRELATIVO";
                                            op1.NRO_AUTORIZACION = "0";
                                            op1.NRO_FACTURA = lngNoFact.ToString();
                                            op1.CODIGO_CONTROL = "";
                                            op1.ESTADO = "F";
                                            op1.ID_PROYECTO = 0;
                                            op1.PRODUCTO = "";
                                            op1.CANTIDAD = 0;
                                            op1.PRECIO = 0;
                                            op1.IMPORTE_DOC = 0;
                                            op1.IMPORTE = 0;
                                            op1.ACTIVIDAD = "";
                                            op1.PROYECTO = "";
                                            op1.ID_ACTIVIDAD = 0;
                                            op1.ID_PROYECTO = 0;
                                            op1.ID_MOVIMIENTO = 0;
                                            op1.ID_TIPO_EMISOR = 0;
                                            lstRegistros.Add(op1);
                                            lngNoFact++;
                                            booCtrl = true;
                                        }

                                        var op = new O_LISTA_REPORTE_DOSIF_DET_CTY();

                                        strFecha = re[0].ToString();
                                        op.FECHA_FACTURA = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                        op.FECHA_FACTURA_DIA = op.FECHA_FACTURA.Substring(0, 10);
                                        strFecha = re[1].ToString();
                                        op.FECHA_MOVIMIENTO = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                        op.NIT = re.GetDecimal(2);
                                        op.RAZON_SOCIAL = re[3].ToString();
                                        op.NRO_AUTORIZACION = re[4].ToString();//re.GetDecimal(4);
                                        op.NRO_FACTURA = re[5].ToString();
                                        op.CODIGO_CONTROL = re[6].ToString();
                                        op.ESTADO = re[7].ToString();
                                        op.ID_PROYECTO = re.GetDecimal(8);
                                        op.PRODUCTO = re[9].ToString();
                                        op.CANTIDAD = re.GetDecimal(10);
                                        op.PRECIO = re.GetDecimal(11);
                                        op.IMPORTE_DOC = re.GetDecimal(12);
                                        op.IMPORTE = (re[7].ToString() == "V") ? re.GetDecimal(12) : 0;
                                        op.ACTIVIDAD = re[13].ToString();
                                        op.PROYECTO = re[14].ToString();
                                        op.ID_ACTIVIDAD = re.GetDecimal(15);
                                        op.ID_PROYECTO = re.GetDecimal(16);
                                        op.ID_MOVIMIENTO = re.GetDecimal(17);
                                        op.ID_TIPO_EMISOR = re.GetDecimal(18);
                                        lstRegistros.Add(op);

                                        lngNoFact++;
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                                lngContador = lstRegistros.Count;
                            }
                            else
                            {
                                var lstRegistros = new List<O_LISTA_REPORTE_DOSIF_GRAL_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_REPORTE_DOSIF_GRAL_CTY();

                                        op1.NRO_AUTORIZACION = re[0].ToString(); //re.GetDecimal(0);
                                        op1.NO_MIN = re.GetDecimal(1);
                                        op1.NO_MAX = re.GetDecimal(2);
                                        op1.NO_REG_REAL = re.GetDecimal(3);
                                        op1.NO_REG_CALC = re.GetDecimal(2) - re.GetDecimal(1) + 1;
                                        op1.DIFERENCIA = op1.NO_REG_CALC - op1.NO_REG_REAL;
                                        op1.ESTADO = (op1.DIFERENCIA == 0) ? "OK" : "DIF";
                                        op1.DIF_VAL = (op1.DIFERENCIA < 0) ? 0 : op1.DIFERENCIA;
                                        op1.PORCENTAJE = (op1.DIFERENCIA < 0 || op1.NO_REG_CALC == 0) ? 0 : Math.Round((op1.NO_REG_REAL * 100) / op1.NO_REG_CALC, 2);
                                        lstRegistros.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                                lngContador = lstRegistros.Count;
                            }

                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null && lngContador > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";

                            if (ob.decFiltro == 2 && booCtrl)
                            {
                                _resultado.intCodigo = 2;
                                _resultado.strMensaje = "Existen saltos en la dosificación, se marcaron con un ESTADO => F. Revise su información.";
                            }
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListadoMovimientosCsv(EObjetoEnvio5ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_MOVIMIENTOS_CSV";

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
                        OracleParameter op0 = new OracleParameter("8", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;
                            if (ob.decFiltro == 2)
                            {
                                var lstMovCs1 = new List<O_LISTA_MOV_CSV_EESSDS_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_MOV_CSV_EESSDS_CTY();

                                        var strFecha = re[0].ToString();
                                        op1.FECHA = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                        op1.TIPO_INFRA = (re[1].ToString() == "DESC") ? "NOID" : re[1].ToString();
                                        op1.INFRAESTRUCTURA = re[2].ToString();
                                        op1.IDENTIFICADOR = re[3].ToString();
                                        op1.COPIA_PLACA = re[4].ToString();
                                        op1.CODIGO_BARRA = re.GetDecimal(5);
                                        op1.PAIS = re[6].ToString();
                                        op1.PRODUCTO = re[7].ToString();
                                        op1.CANTIDAD = re.GetDecimal(8);
                                        op1.ESTADO = re[9].ToString();
                                        op1.USUARIO = re[32].ToString();

                                        var intFormaPago = re.GetInt32(11) + re.GetInt32(39);
                                        op1.FORMA_PAGO = (intFormaPago <= 1) ? "EF" : (intFormaPago == 3) ? "VAL" : "PRE";

                                        if (re[15].ToString().Length > 1)
                                        {
                                            op1.NIT = re.GetInt64(12);
                                            op1.RAZON_SOCIAL = re[13].ToString();
                                        }
                                        else
                                        {
                                            op1.NIT = re.GetInt64(40);
                                            op1.RAZON_SOCIAL = re[41].ToString();
                                        }

                                        op1.FACT_NO = re[14].ToString();
                                        op1.FACT_NO_AUTORIZACION = re[15].ToString();
                                        op1.FACT_CODIGO_CONTROL = re[16].ToString();
                                        op1.FACT_PRECIO = re.GetDecimal(17);
                                        op1.FACT_IMPORTE = re.GetDecimal(18);
                                        op1.VALE_NO = re[21].ToString();
                                        op1.VALE_PRECIO = re.GetDecimal(22);
                                        op1.VALE_IMPORTE = re.GetDecimal(23);
                                        op1.VALE_RESPONSABLE = re[24].ToString();
                                        op1.VALE_CI_RESPONSABLE = re.GetInt64(25);
                                        op1.FACTEX_NO = re[26].ToString();
                                        op1.FACTEX_NO_AUTORIZACION = re.GetInt64(27);
                                        op1.FACTEX_CODIGO_CONTROL = re[28].ToString();
                                        op1.FACTEX_PRECIO = re.GetDecimal(29);
                                        op1.FACTEX_IMPORTE = re.GetDecimal(30);
                                        //op1.NMOV_NO = re[31].ToString();
                                        op1.NMOV_NO = (re[31].ToString().Trim().Length == 0) ? strFecha : re[31].ToString();

                                        var intTipoServ = (re.GetInt32(33) == 0) ? "NOID" : (re.GetInt32(33) == 10) ? "SUIC" : (re.GetInt32(33) == 12) ? "DS2243" : "SICEVE";
                                        op1.SRAD_TIPO = intTipoServ;
                                        op1.SRAD_NOREF = re[34].ToString();
                                        op1.SRAD_IMPORTE = re.GetDecimal(35);

                                        var intTipoAutVta = (re.GetInt32(36) == 0) ? "NOID" : (re.GetInt32(36) == 7) ? "SC" : "CC";
                                        op1.AUVT_TIPO = intTipoAutVta;
                                        op1.AUVT_NOREF = re[37].ToString();

                                        lstMovCs1.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstMovCs1;
                            }
                            else
                            {
                                var lstMovCs1 = new List<O_LISTA_MOV_CSV_EESSRC_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_MOV_CSV_EESSRC_CTY();

                                        var strFecha = re[0].ToString();
                                        op1.FECHA = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                        op1.PROYECTO_ORIGEN = re[1].ToString();
                                        op1.TIPO_INFRA = (re[2].ToString() == "DESC") ? "NOID" : re[2].ToString();
                                        op1.INFRAESTRUCTURA = re[3].ToString();
                                        op1.IDENTIFICADOR = re[4].ToString();
                                        op1.COPIA_PLACA = re[5].ToString();
                                        op1.CODIGO_BARRA = re.GetDecimal(6);
                                        op1.CODIGO_TKCS = re[7].ToString();
                                        op1.PRECINTOS = re[8].ToString().Replace(',', '|');
                                        op1.PRODUCTO = re[9].ToString();
                                        op1.CANTIDAD = re.GetDecimal(10);
                                        op1.ESTADO = re[11].ToString();
                                        op1.USUARIO = (re[12].ToString().Trim().Length == 0) ? re[20].ToString() : re[12].ToString();
                                        op1.NIT = re.GetInt64(13);
                                        op1.RAZON_SOCIAL = re[14].ToString();
                                        op1.FACT_NO = re[15].ToString();
                                        op1.FACT_NO_AUTORIZACION = re[16].ToString(); //re.GetInt64(16);
                                        op1.FACT_CODIGO_CONTROL = re[17].ToString();
                                        op1.FACT_PRECIO = re.GetDecimal(18);
                                        op1.FACT_IMPORTE = re.GetDecimal(19);
                                        op1.NMOV_NO = (re[21].ToString().Trim().Length == 0) ? strFecha : re[21].ToString();
                                        op1.NMOV_RESPONSABLE = re[22].ToString();
                                        op1.NMOV_CI_RESP = re.GetInt64(23);
                                        op1.ACL_NO = Convert.ToDecimal(re[24].ToString());

                                        strFecha = re[26].ToString();
                                        if (strFecha.Trim().Length > 0)
                                        {
                                            op1.DDES_FECHA = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(0, 4) + " " + strFecha.Substring(8, 2) + ":" + strFecha.Substring(10, 2) + ":" + strFecha.Substring(12, 2);
                                        }
                                        else
                                        {
                                            op1.DDES_FECHA = "";
                                        }

                                        op1.DDES_NO = re[27].ToString();
                                        op1.DDES_NOOR = re.GetInt64(28);
                                        op1.DDES_CI_COND = re[29].ToString();
                                        op1.DDES_VLPL = re.GetDecimal(30);

                                        lstMovCs1.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstMovCs1;
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ImportaMovimientoDcod(EObjetoEnvio2ParametrosProto ob, string strPeriodo)
        {
            try
            {
                var strCodDcod = "";
                _resultado.oResultado = null;
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_BSISA.P_OBTENER_CONSUMIDOR_DCOD";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        //cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, 1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro2,
                            ParameterDirection.Input));

                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    strCodDcod = re[0].ToString();
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                    }
                }

                if (strCodDcod != "" && strCodDcod != "-1" && strCodDcod != "-999")
                {
                    if (ob.decFiltro == 1)
                    {
                        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString))
                        {
                            con.Open();
                            var qry = "SELECT SALDO_FINAL_GE_TOTAL_MOVIMIENTO, SALDO_FINAL_DO_TOTAL_MOVIMIENTO, SALDO_FINAL_GP_TOTAL_MOVIMIENTO " +
                                      ",ID_TOTAL_MOVIMIENTO, FECHA_REGISTRO_TOTAL_MOVIMIENTO, PERIODO_TOTAL_MOVIMIENTO " +
                                      "FROM TLIQ_TOTAL_MOVIMIENTO " +
                                      "WHERE PERIODO_TOTAL_MOVIMIENTO='" + strPeriodo + "' AND VALIDACION_FORMULARIO_TOTAL_MOVIMIENTO=" + ob.strParametro1 + " AND ID_OPERADOR='" + strCodDcod + "'";
                            using (var command = new SqlCommand(qry, con))
                            {
                                var lstSaldos = new List<O_LISTA_SALDOS_DCOD_CTY>();
                                SqlDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    var objSaldo = new O_LISTA_SALDOS_DCOD_CTY();
                                    objSaldo.ID_MOVIMIENTO = reader.GetInt32(3);
                                    objSaldo.FECHA_REGISTRO = reader.GetDateTime(4);
                                    objSaldo.PERIODO = reader.GetDateTime(5);
                                    objSaldo.ID_PRODUCTO = (decimal)EnumProductos.GasolinaEspecial;
                                    objSaldo.SALDO = Convert.ToDecimal(reader.GetDouble(0).ToString());
                                    lstSaldos.Add(objSaldo);
                                    objSaldo = new O_LISTA_SALDOS_DCOD_CTY();
                                    objSaldo.ID_MOVIMIENTO = reader.GetInt32(3);
                                    objSaldo.FECHA_REGISTRO = reader.GetDateTime(4);
                                    objSaldo.PERIODO = reader.GetDateTime(5);
                                    objSaldo.ID_PRODUCTO = (decimal)EnumProductos.Diesel;
                                    objSaldo.SALDO = Convert.ToDecimal(reader.GetDouble(1).ToString());
                                    lstSaldos.Add(objSaldo);
                                    objSaldo = new O_LISTA_SALDOS_DCOD_CTY();
                                    objSaldo.ID_MOVIMIENTO = reader.GetInt32(3);
                                    objSaldo.FECHA_REGISTRO = reader.GetDateTime(4);
                                    objSaldo.PERIODO = reader.GetDateTime(5);
                                    objSaldo.ID_PRODUCTO = (decimal)EnumProductos.GasolinaPremium;
                                    objSaldo.SALDO = Convert.ToDecimal(reader.GetDouble(2).ToString());
                                    lstSaldos.Add(objSaldo);
                                }
                                reader.Close();
                                _resultado.oResultado = lstSaldos;
                            }
                        }
                    }
                    else
                    {
                        var arrFecha = strPeriodo.Split('|');
                        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString))
                        {
                            con.Open();
                            var qry = "SELECT CONVERT(VARCHAR,R.ID_RECEPCION) ID_RECEPCION," +
                                      "CONVERT(VARCHAR,R.FECHA_DESCARGA_RECEPCION,112)+CONVERT(VARCHAR,R.FECHA_DESCARGA_RECEPCION,108) F_OPERACION," +
                                      "R.PLACA_CISTERNA_RECEPCION PLACA," +
                                      "R.PRECINTO_RECEPCION PRECINTO," +
                                      "CONVERT(VARCHAR,R.NRO_ORDEN_COMPRA_RECEPCION) NRO_ORDEN,CONVERT(VARCHAR,R.NRO_HOJA_RUTA) HOJA_RUTA," +
                                      "CONVERT(VARCHAR,F.FECHA_FACTURA,112)+CONVERT(VARCHAR,F.FECHA_FACTURA,108) FECHA_FACTURA," +
                                      "CONVERT(VARCHAR,F.NUMERO_FACTURA) NUMERO_FACTURA,CONVERT(VARCHAR,F.LITROS_FACTURA) LITROS_FACTURA,F.ID_COMBUSTIBLE " +
                                      "FROM TLIQ_RECEPCION R " +
                                      "INNER JOIN TLIQ_FACTURA F ON R.ID_FACTURA=F.ID_FACTURA " +
                                      "WHERE CAST(FECHA_DESCARGA_RECEPCION AS DATE) BETWEEN CAST('" + arrFecha[0] + "' AS DATE) AND CAST('" + arrFecha[1] + "' AS DATE) " +
                                      "AND R.ID_OPERADOR = '" + strCodDcod + "' " +
                                      "AND F.LITROS_FACTURA>0 " +
                                      "ORDER BY R.ID_OPERADOR, R.FECHA_DESCARGA_RECEPCION";
                            using (var command = new SqlCommand(qry, con))
                            {
                                var lstSaldos = new List<O_LISTA_RECEPCION_DCOD_CTY>();
                                SqlDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    var objSaldo = new O_LISTA_RECEPCION_DCOD_CTY();

                                    //var xx = reader[0];
                                    objSaldo.ID_RECEPCION = Convert.ToDecimal(reader.GetString(0));
                                    objSaldo.F_OPERACION = Convert.ToDecimal(reader.GetString(1).Replace(":", ""));
                                    objSaldo.PLACA = reader.GetString(2);
                                    objSaldo.PRECINTO = reader.GetString(3).Replace(" ", "-").Replace(",", "-").Replace("/", "-").Replace(";", "-");
                                    //objSaldo.PRECINTO = reader.GetString(3);
                                    objSaldo.NRO_ORDEN = reader.GetString(4);
                                    objSaldo.HOJA_RUTA = reader.GetString(5);
                                    objSaldo.FECHA_FACTURA = Convert.ToDecimal(reader.GetString(6).Replace(":", ""));
                                    objSaldo.NUMERO_FACTURA = Convert.ToDecimal(reader.GetString(7));
                                    objSaldo.LITROS_FACTURA = Convert.ToDecimal(reader.GetString(8));
                                    objSaldo.ID_COMBUSTIBLE = (reader.GetString(9) == "DO") ? (decimal)EnumProductos.Diesel : (reader.GetString(9) == "GE") ? (decimal)EnumProductos.GasolinaEspecial : (decimal)EnumProductos.GasolinaPremium;

                                    lstSaldos.Add(objSaldo);
                                }
                                reader.Close();
                                _resultado.oResultado = lstSaldos;
                            }
                        }
                    }

                    if (_resultado.oResultado != null)
                    {
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                    }
                    else
                    {
                        _resultado.intCodigo = -1;
                        _resultado.strMensaje = "No se encontraron registros";
                    }
                }
                else
                {
                    _resultado.intCodigo = -1;
                    _resultado.strMensaje = "El código de la entidad no se encontró";
                }
                return _resultado;
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -100;
                _resultado.strMensaje = ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
        }

        public EPResultado ObtenerMovimiento(string strCredencial, EObjetoEnvio8ParametrosProto ob)
        {
            var lstMovimientos = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_CONSULTA_MOVI_TRANSACCION";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.strCredencial,
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
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, ob.strParametro6,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, ob.strParametro7,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("10", OracleDbType.Varchar2, ob.strParametro8,
                            ParameterDirection.Input));

                        var op0 = new OracleParameter("11", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var mov = new O_CONSULTA_MOVIM_TRAN_CTY();
                                    mov.ID_MOVIMIENTO = re.GetInt32(0);
                                    mov.ID_MOVIMIENTO_PADRE = re.GetInt32(1);
                                    mov.FECHA_OPERACION = re.GetInt64(2);

                                    var dd = mov.FECHA_OPERACION.ToString();
                                    mov.STR_FECHA = dd.Substring(6, 2) + "/" + dd.Substring(4, 2) + "/" + dd.Substring(0, 4) + " " + dd.Substring(8, 2) + ":" + dd.Substring(10, 2) + ":" + dd.Substring(12, 2);

                                    mov.ID_CONSUMIDOR = re.GetInt32(3);
                                    mov.ID_TIPO_ACTIVIDAD = re.GetInt32(4);
                                    mov.ID_TIPO_OPERACION = re.GetInt32(5);
                                    mov.ID_TIPO_FLUJO = re.GetInt32(6);
                                    mov.TIPO_FLUJO = re.GetString(7);
                                    mov.ID_TIPO_MOVIMIENTO = re.GetInt32(8);
                                    mov.TIPO_MOVIMIENTO = re.GetString(9);
                                    mov.ID_UBICACION_OPERACION = re.GetInt32(10);
                                    mov.UBICACION_OPERACION = re.GetString(11);
                                    mov.ID_INFRAESTRUCTURA = re.GetInt32(12);
                                    mov.ID_TIPO_MEDIO_TRANSPORTE = re.GetInt32(13);
                                    mov.TIPO_MEDIO_TRANSAPORTE = re[14].ToString();
                                    mov.ID_MEDIO_TRANSPORTE = re.GetInt32(15);
                                    mov.ID_ENTIDAD = re.GetInt32(16);
                                    mov.ID_TIPO_MEDICION = re.GetInt32(17);
                                    mov.IDENTIFICADOR = re[18].ToString();
                                    mov.SUB_IDENTIFICADOR = re[19].ToString();
                                    mov.ID_FUENTE_ORIGEN = re.GetInt32(20);
                                    mov.COD_FUENTE_ORIGEN = re[21].ToString();
                                    mov.DES_FUENTE_ORIGEN = re[22].ToString();
                                    mov.ID_HIDROCARBURO = re.GetInt32(23);
                                    mov.COD_HIDROCARBURO = re[24].ToString();
                                    mov.DES_HIDROCARBURO = re[25].ToString();
                                    mov.ID_UNIDAD_HIDROCARBURO = re.GetInt32(26);
                                    mov.ID_TIPO_TRANSACCION = re.GetInt32(27);
                                    mov.TIPO_TRANSACCION = re[28].ToString();
                                    mov.RAZON_SOCIAL = re[29].ToString();
                                    mov.NIT_CONSUMIDOR = re.GetInt64(30);
                                    mov.NRO_TRANSACCION = re[31].ToString();
                                    mov.NRO_AUTORIZACION = re.GetInt64(32);
                                    mov.PRECIO_UNITARIO = re.GetDecimal(33);
                                    mov.PRECIO_TOTAL = re.GetDecimal(34);
                                    mov.BINARIO_CONTROL = re[35].ToString();
                                    mov.OBSERVACION_MOVIMIENTO = re[36].ToString();
                                    mov.ID_ESTADO_MOVIMIENTO = re.GetInt32(37);
                                    mov.ESTADO_MOVIMIENTO = (mov.ID_ESTADO_MOVIMIENTO == 11) ? "V" : "A";
                                    mov.CANTIDAD = re.GetDecimal(39);
                                    mov.CANTIDAD_ADICIONAL = re.GetDecimal(40);
                                    mov.FECHA_OPERACION_DIA = re.GetInt32(41);
                                    mov.GRADO_API = re[42].ToString();
                                    mov.TEMPERATURA = re.GetDecimal(43);
                                    mov.ID_UNIDAD_TEMPERATURA = re.GetInt32(44);
                                    mov.CANTIDAD_ENVASES = re.GetDecimal(45);
                                    mov.ID_UNIDAD_ENVASES = re.GetInt32(46);
                                    mov.APP_USUARIO = re[47].ToString();
                                    var f = DateTime.Parse(re[48].ToString());
                                    var ff = Convert.ToDecimal(f.Year + f.Month.ToString().PadLeft(2, '0') +
                                             f.Day.ToString().PadLeft(2, '0') + f.Hour.ToString().PadLeft(2, '0') +
                                             f.Minute.ToString().PadLeft(2, '0') + f.Second.ToString().PadLeft(2, '0'));

                                    mov.APP_FECHA_REGISTRO = f;//ff;//re.GetInt64(48);
                                    mov.CODIGO = re[49].ToString();
                                    mov.TIPO_INFRAESTRUCTURA = re.GetInt32(50);
                                    mov.ID_INTEGRACION = re.GetInt64(51);
                                    mov.BINARIO_CONTROL_TRAN = re[52].ToString();
                                    mov.BARCODE = re.GetDecimal(53);

                                    mov.JSON_REFERENCIA = re[56].ToString();
                                    mov.ESTADO_JSON_REFERENCIA = re.GetDecimal(57);
                                    mov.ID_CUENTA = re.GetDecimal(58);
                                    mov.ID_CUENTA_AUX = re.GetDecimal(59);
                                    mov.ID_PARAM_GLOBAL = re.GetDecimal(60);
                                    mov.ID_PARAM_ESPEC = re.GetDecimal(61);
                                    mov.ID_TIPO_CONSUMIDOR = re.GetDecimal(62);
                                    mov.ID_TIPO_INFRA_AUX = re.GetDecimal(63);
                                    mov.ID_INFRA_AUX = re.GetDecimal(64);
                                    mov.PRECIO_UNITARIO_TRANS = re.GetDecimal(65);
                                    mov.PRECIO_TOTAL_TRANS = re.GetDecimal(66);
                                    mov.CANTIDAD_TRANS = re.GetDecimal(67);
                                    mov.CODIGO_CUF = re.GetInt64(32) == 0 ? re[68].ToString() : re.GetInt64(32).ToString();
                                    //mov.JSON_REFERENCIA = (re[56] == null) ? "" : re[56].ToString();
                                    //mov.ESTADO_JSON_REFERENCIA = (re[57] == null) ? -99999 : re.GetDecimal(57);
                                    //mov.ID_CUENTA = (re[58] == null) ? -99999 : re.GetDecimal(58);
                                    //mov.ID_CUENTA_AUX = (re[59] == null) ? -99999 : re.GetDecimal(59);
                                    //mov.ID_PARAM_GLOBAL = (re[60] == null) ? -99999 : re.GetDecimal(60);
                                    //mov.ID_PARAM_ESPEC = (re[61] == null) ? -99999 : re.GetDecimal(61);
                                    //mov.ID_TIPO_CONSUMIDOR = (re[62] == null) ? -99999 : re.GetDecimal(62);
                                    //mov.ID_TIPO_INFRA_AUX = (re[63] == null) ? -99999 : re.GetDecimal(63);
                                    //mov.ID_INFRA_AUX = (re[64] == null) ? -99999 : re.GetDecimal(64);

                                    //mov.JSON_REFERENCIA = "";
                                    //mov.ESTADO_JSON_REFERENCIA = -99999;
                                    //mov.ID_CUENTA = -99999;
                                    //mov.ID_CUENTA_AUX = -99999;
                                    //mov.ID_PARAM_GLOBAL = -99999;
                                    //mov.ID_PARAM_ESPEC = -99999;
                                    //mov.ID_TIPO_CONSUMIDOR = -99999;
                                    //mov.ID_TIPO_INFRA_AUX = -99999;
                                    //mov.ID_INFRA_AUX = -99999;

                                    lstMovimientos.Add(mov);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (!lstMovimientos.IsNullOrEmpty())
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstMovimientos;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ReporteMovGral(EObjetoEnvio6ParametrosProto ob)
        {
            var lstReporteFlujo = new List<O_LISTA_REPORTE_MOVGRAL_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REPORTE_MOVGRAL";

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
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, ob.strParametro5,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, ob.strParametro6,
                             ParameterDirection.Input));
                        OracleParameter op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    O_LISTA_REPORTE_MOVGRAL_CTY ope = new O_LISTA_REPORTE_MOVGRAL_CTY();
                                    ope.FECHA = re.GetInt64(0);
                                    ope.ID_TIPO_OPERACION = re.GetInt32(1);
                                    ope.TIPO_OPERACION = re.GetString(2);
                                    ope.CANTIDAD = re.GetDecimal(3);
                                    lstReporteFlujo.Add(ope);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (!lstReporteFlujo.IsNullOrEmpty())
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstReporteFlujo;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListaDeclaracionJuradaAcumulada(EListarDeclaracionJuradaAcumuladaD ob)
        {
            string strMensajeError = "";
            EPResultado listaAcumulada = null;
            listaAcumulada = ListaAcumuladaPrueba(ob);
            if (listaAcumulada.oResultado != null && listaAcumulada.intCodigo > 0)
            {
                //List<CDjAcumulado> lstDjAcumulado = (List<CDjAcumulado>)listaAcumulada.oResultado;
                _resultado.oResultado = listaAcumulada.oResultado;
                _resultado.intCodigo = 1;
                _resultado.strMensaje = "OK";
            }
            else
            {
                _resultado.intCodigo = -1;
                _resultado.strMensaje = "No se pudo obtener la declaración jurada acumulada: " + listaAcumulada.strMensaje;
                _resultado.oResultado = null;
            }
            return _resultado;
        }

        public EPResultado ListaAcumuladaPrueba(EListarDeclaracionJuradaAcumuladaD ob)
        {
            string strMensajeErrorTO = "";
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OPERACIONES_SALDOS_PRINC";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, ob.decIdConsumidor,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, ob.decIdTipoActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int64, ob.decFechaDiaInicio,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int64, ob.decFechaDiaFin,
                            ParameterDirection.Input));
                        OracleParameter op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        var declaracionJurada = new O_DECLARACION_JURADA_CTY();
                        var lstDjAcumulado = new List<CDjAcumulado>();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var djAcumulado = new CDjAcumulado();
                                    djAcumulado.DecFecha = re.GetInt32(0); //oOpSaldosPrinc.FECHA_OPERACION_DIA;
                                    djAcumulado.DecIdHidrocarburo = re.GetInt32(1); //oOpSaldosPrinc.ID_HIDROCARBURO;
                                    djAcumulado.StrCodHidrocarburo = re.GetString(2); //oOpSaldosPrinc.COD_HIDROCARBURO;
                                    djAcumulado.StrHidrocarburo = re.GetString(3); //oOpSaldosPrinc.HIDROCARBURO;
                                    djAcumulado.StrUnidadMedida = re.GetString(5); //oOpSaldosPrinc.UNIDAD_MEDIDA;
                                    djAcumulado.DecSaldoInicial = re.GetDecimal(6); //oOpSaldosPrinc.SALDO_INICIAL;
                                    djAcumulado.DecSaldoFinal = re.GetDecimal(7); //oOpSaldosPrinc.SALDO_FINAL;
                                    djAcumulado.DecTotalIngreso = re.GetDecimal(8); //oOpSaldosPrinc.TOTAL_INGRESO;
                                    djAcumulado.DecTotalEgreso = re.GetDecimal(9); //oOpSaldosPrinc.TOTAL_EGRESO;
                                    djAcumulado.DecIdInfraestructura = re.GetInt32(10);
                                    //oOpSaldosPrinc.ID_INFRAESTRUCTURA;
                                    djAcumulado.DecIdTipoInfraestructura = re.GetInt32(11);
                                    //oOpSaldosPrinc.ID_TIPO_INFRAESTRUCTURA;
                                    djAcumulado.StrInfraestructura = re.GetString(12); //oOpSaldosPrinc.INFRAESTRUCTURA;
                                    djAcumulado.StrTipoInfraestructura = re.GetString(13);
                                    //oOpSaldosPrinc.TIPO_INFRAESTRUCTURA;
                                    djAcumulado.StrInfraestructuraAnh = re.GetString(14);
                                    //oOpSaldosPrinc.INFRAESTRUCTURA_ANH;

                                    var lstOperaciones = new List<O_OP_SALDOS_CTY>();
                                    using (OracleCommand cmd1 = xon.CreateCommand())
                                    {
                                        cmd1.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OPERACIONES_SALDOS";
                                        cmd1.CommandType = CommandType.StoredProcedure;
                                        cmd1.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2,
                                            ob.strCredencial,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, 1,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("3", OracleDbType.Int32,
                                            ob.decIdConsumidor,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("4", OracleDbType.Int32,
                                            ob.decIdTipoActividad,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("5", OracleDbType.Int32,
                                            djAcumulado.DecIdHidrocarburo,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("6", OracleDbType.Int32,
                                            djAcumulado.DecIdInfraestructura,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("7", OracleDbType.Int32,
                                            djAcumulado.DecIdTipoInfraestructura,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("8", OracleDbType.Int64,
                                            ob.decFechaDiaInicio,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("9", OracleDbType.Int64,
                                            ob.decFechaDiaFin,
                                            ParameterDirection.Input));
                                        cmd1.Parameters.Add(new OracleParameter("10", OracleDbType.Int32,
                                            djAcumulado.DecFecha,
                                            ParameterDirection.Input));

                                        OracleParameter op1 = new OracleParameter("11", OracleDbType.RefCursor,
                                            ParameterDirection.Output);
                                        cmd1.Parameters.Add(op1);
                                        cmd1.ExecuteNonQuery();
                                        //using (OracleDataReader re1 = cmd1.ExecuteReader())
                                        using (OracleDataReader re1 = ((OracleRefCursor)op1.Value).GetDataReader())
                                        {
                                            while (re1.Read())
                                            {
                                                if (re1.HasRows)
                                                {
                                                    var ope = new O_OP_SALDOS_CTY();
                                                    ope.FECHA_OPERACION_DIA = re1.GetInt32(0);
                                                    ope.CANTIDAD = re1.GetDecimal(1);
                                                    ope.OPERACION_ORDEN = re1.GetInt32(3);
                                                    ope.REPORTE_NOMBRE = re1.GetString(4);
                                                    ope.REPORTE_ORDEN = re1.GetInt32(5);
                                                    ope.FACTOR_TIPO = re1.GetInt32(6);
                                                    lstOperaciones.Add(ope);
                                                }
                                            }
                                            re1.Close();
                                            op1.Dispose();
                                            //re.Dispose();                                           
                                        }
                                        //cmd1.Dispose();
                                        //if (!lstOperaciones.IsNullOrEmpty())
                                        //{
                                        //    resultado.intCodigo = 1;
                                        //    resultado.strMensaje = "OK";
                                        //    resultado.oResultado = lstOperaciones;
                                        //}
                                        //else
                                        //{
                                        //    resultado.intCodigo = -1;
                                        //    resultado.strMensaje = "No se encontraron registros";
                                        //    resultado.oResultado = null;
                                        //}
                                        //return resultado;
                                    }


                                    //if (listadoOperaciones.oResultado != null)
                                    if (lstOperaciones != null)
                                    {
                                        var listadoOperacionesR = (List<O_OP_SALDOS_CTY>)lstOperaciones;
                                        djAcumulado.LstIngresos =
                                            listadoOperacionesR.Where(p => p.FACTOR_TIPO == 1).ToList();
                                        djAcumulado.LstEgresos =
                                            listadoOperacionesR.Where(p => p.FACTOR_TIPO == -1).ToList();
                                    }
                                    lstDjAcumulado.Add(djAcumulado);
                                }
                            }
                            re.Close();
                            //re.Dispose();
                            op0.Dispose();
                        }
                        //cmd.Dispose();
                        if (!lstDjAcumulado.IsNullOrEmpty() && lstDjAcumulado.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstDjAcumulado;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
                            _resultado.oResultado = null;
                        }
                        return _resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                //return new List<EServicioAdicional>();
                _resultado.intCodigo = -1;
                _resultado.strMensaje = "Error: " + ex.Message;
                _resultado.oResultado = null;
                return _resultado;
            }
            //}
            //catch (Exception ex)
            //{
            //    xon.Close();
            //    xon.Dispose();
            //    resultado.intCodigo = -100;
            //    resultado.strMensaje = ex.Message;
            //    resultado.oResultado = null;
            //    return resultado;
            //}
            //finally
            //{
            //    xon.Close();
            //    xon.Dispose();
            //    xon = null;
            //}
        }

        public EPResultado ListaTransaccionTipo(EListarDeclaracionJuradaAcumuladaD ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_TRANSACCIONES_TIPO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, ob.decIdConsumidor.ToString(),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.decFechaDiaInicio.ToString(),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.decFechaDiaFin.ToString(),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Varchar2, ob.decIdTipoActividad.ToString(),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Varchar2, "0",
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        var lstObjDjTransaccion = new List<ObjDjTransaccion>();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var transaccion = new ObjDjTransaccion();
                                    transaccion.fechaOperacion = re.GetInt32(0);
                                    transaccion.idHidrocarburo = re.GetInt32(1);
                                    transaccion.hidrocarburo = re.GetString(2);
                                    transaccion.codHidrocarburo = re.GetString(3);
                                    transaccion.unidadMedida = re.GetString(4);
                                    transaccion.cantidadHidrocarburo = re.GetDecimal(5);
                                    transaccion.identificador = re.GetString(6);
                                    transaccion.subIdentificador = re[7].ToString();
                                    transaccion.idUbicacionOperacion = re.GetInt32(8);
                                    transaccion.ubicacionOperacion = re.GetString(9);
                                    transaccion.idMovimiento = re.GetInt32(10);
                                    transaccion.idTransaccion = re.GetInt32(11);
                                    transaccion.idUsuarioAplicacion = re.GetInt32(12);
                                    transaccion.fechaRegistroTransaccion = re.GetInt64(13);
                                    transaccion.idTipoTransaccion = re.GetInt32(14);
                                    transaccion.tipoTransaccion = re.GetString(15);
                                    transaccion.fechaEmision = re.GetInt64(16);
                                    transaccion.numeroTransaccion = re.GetString(17);
                                    transaccion.numeroAutorizacion = re.GetInt64(18);
                                    transaccion.codigoControl = re[19].ToString();
                                    transaccion.nitConsumidor = re.GetInt64(20);
                                    transaccion.razonSocial = re[21].ToString();
                                    transaccion.precioUnitario = re.GetDecimal(22);
                                    transaccion.precioTotal = re.GetDecimal(23);
                                    transaccion.idEstadoTransaccion = re.GetInt32(24);
                                    transaccion.EstadoTransaccion = re.GetString(25);
                                    transaccion.idTipoCreditoFiscal = re.GetInt32(26);
                                    transaccion.tipoCreditoFiscal = re.GetString(27);
                                    transaccion.idFormaPago = re.GetInt32(28);
                                    transaccion.formaPago = re.GetString(29);
                                    transaccion.idTipoMoneda = re.GetInt32(30);
                                    transaccion.idTipoEmisor = re.GetInt32(31);
                                    transaccion.tipoEnvase = re.GetString(32);
                                    transaccion.tipoMovimiento = re.GetString(33);
                                    lstObjDjTransaccion.Add(transaccion);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (!lstObjDjTransaccion.IsNullOrEmpty() && lstObjDjTransaccion.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstObjDjTransaccion;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "ERROR";
                            _resultado.oResultado = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -1;
                _resultado.strMensaje = "Error: " + ex.Message;
                _resultado.oResultado = null;
            }
            return _resultado;
            //finally
            //{
            //    xon.Close();
            //    xon.Dispose();
            //    xon = null;
            //}
        }

        public EPResultado ListaRecaudoExterno(EObjetoEnvio5ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_RECAUDO_EXTERNO";
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
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, ob.strParametro6,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        var lstResultado = new List<O_RECAUDO_EXTERNO_CTY>();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_RECAUDO_EXTERNO_CTY();
                                    reg.ID_TIPO_TRANSACCION = re.GetDecimal(0);
                                    reg.TIPO_TRANSACCION = re[1].ToString();
                                    reg.ID_PRODUCTO = re.GetDecimal(2);
                                    reg.PRODUCTO = re[3].ToString();
                                    reg.ID_UNIDAD_MEDIDA = re.GetDecimal(4);
                                    reg.UNIDAD_MEDIDA = re[5].ToString();
                                    reg.CANTIDAD_FACTURA = re.GetDecimal(6);
                                    reg.IMPORTE_TOTAL = re.GetDecimal(7);
                                    lstResultado.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (!lstResultado.IsNullOrEmpty() && lstResultado.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstResultado;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros.";
                            _resultado.oResultado = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _resultado.intCodigo = -1;
                _resultado.strMensaje = "Error: " + ex.Message;
                _resultado.oResultado = null;
            }
            return _resultado;
        }

        public EPResultado RegistrosDdjj(EObjetoEnvio3ParametrosProto ob)
        {
            var objDatos = new List<O_LISTA_REGISTROS_DDJJ_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REGISTROS_DDJJ";
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

                        var op0 = new OracleParameter("6", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LISTA_REGISTROS_DDJJ_CTY();

                                    reg.DEC_TIPO = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.STR_MENSAJE = re[1].ToString();
                                    reg.JSON_REPORTE_DATOS = re[2].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerAeronaves(EObjetoEnvio1ParametrosProto ob)
        {
            var lstRegistros = new List<O_OBTENER_AERONAVES_CTY>();
            var lstRegistro1 = new List<O_IMAGEN_MTR_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_AERONAVES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));

                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            if (ob.decFiltro == 3)
                            {
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_IMAGEN_MTR_CTY();
                                        reg.TIPO_IMAGEN = re[0].ToString();
                                        reg.IMAGEN = (byte[])re[1];

                                        lstRegistro1.Add(reg);
                                    }
                                }
                            }
                            else
                            {
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_OBTENER_AERONAVES_CTY();
                                        reg.TIPO_MTR = re[0].ToString();
                                        reg.ID_EXPLOTADOR = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                        reg.EXPLOTADOR = re[2].ToString();
                                        reg.PAIS_EXPLOTADOR = re[3].ToString();
                                        reg.CIUDAD_EXPLOTADOR = re[4].ToString();
                                        reg.DOMICILIO_EXPLOTADOR = re[5].ToString();
                                        reg.FECHA_REGISTRO = re[6].ToString();
                                        reg.LUGAR_REGISTRO = re[7].ToString();
                                        reg.USUARIO_NOMBRE = re[8].ToString();
                                        reg.IMEI = (re[9].ToString() == "") ? 0 : Convert.ToDecimal(re[9].ToString());
                                        reg.TIPO_USUARIO = re[10].ToString();
                                        reg.IDENTIFICADOR = re[11].ToString();
                                        reg.SUB_IDENTIFICADOR = re[12].ToString();
                                        reg.BARCODE = (re[13].ToString() == "") ? 0 : Convert.ToDecimal(re[13].ToString());
                                        reg.CLASE = re[14].ToString();
                                        reg.MARCA = re[15].ToString();
                                        reg.SERVICIO = re[16].ToString();
                                        reg.MODELO = re[17].ToString();
                                        reg.SERIE = re[18].ToString();
                                        reg.GESTION_FABRICACION = (re[19].ToString() == "") ? 0 : Convert.ToDecimal(re[19].ToString());
                                        reg.CARGA = (re[20].ToString() == "") ? 0 : Convert.ToDecimal(re[20].ToString());
                                        reg.RA_TIPO_MATRICULA = re[21].ToString();
                                        reg.BASE_OPERACIONES = re[22].ToString();
                                        reg.CHASIS = re[23].ToString();
                                        reg.ESTADO_REPOSICION = (re[24].ToString() == "") ? 0 : Convert.ToDecimal(re[24].ToString());
                                        reg.ESTADO_BLOQUEO = (re[25].ToString() == "") ? 0 : Convert.ToDecimal(re[25].ToString());
                                        reg.MENSAJE_IMPRESION = re[26].ToString();
                                        reg.MENSAJE_PANTALLA = re[27].ToString();
                                        reg.ESTACION = re[28].ToString();
                                        reg.ID_PROPIETARIO = (re[29].ToString() == "") ? 0 : Convert.ToDecimal(re[29].ToString());
                                        reg.PROPIETARIO = re[30].ToString();
                                        reg.PAIS_PROPIETARIO = re[31].ToString();
                                        reg.CIUDAD_PROPIETARIO = re[32].ToString();
                                        reg.DOMICILIO_PROPIETARIO = re[33].ToString();
                                        reg.ESTADO = re[34].ToString();
                                        reg.BLOQUEO = re[35].ToString();
                                        reg.AUD_ESTADO = (re[36].ToString() == "") ? 0 : Convert.ToDecimal(re[36].ToString());
                                        reg.TOTAL_CXC = (re[37].ToString() == "") ? 0 : Convert.ToDecimal(re[37].ToString());
                                        reg.ID_PRODUCTO = Convert.ToDecimal(re[38].ToString());
                                        reg.PRODUCTO = re[39].ToString();

                                        lstRegistros.Add(reg);
                                    }
                                }
                            }

                            re.Close();
                            op0.Dispose();
                        }
                        if (ob.decFiltro == 3)
                        {
                            if (!lstRegistro1.IsNullOrEmpty())
                            {
                                _resultado.intCodigo = 1;
                                _resultado.strMensaje = "OK";
                                _resultado.oResultado = lstRegistro1;
                            }
                            else
                            {
                                _resultado.intCodigo = -1;
                                _resultado.strMensaje = "No se encontraron registros";
                                _resultado.oResultado = null;
                            }
                        }
                        else
                        {
                            if (!lstRegistros.IsNullOrEmpty())
                            {
                                _resultado.intCodigo = 1;
                                _resultado.strMensaje = "OK";
                                _resultado.oResultado = lstRegistros;
                            }
                            else
                            {
                                _resultado.intCodigo = -1;
                                _resultado.strMensaje = "No se encontraron registros";
                                _resultado.oResultado = null;
                            }
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

        public EPResultado ObtenerDatoAeronave(EObjetoEnvio1ParametrosProto ob)
        {
            var lstRegistros = new List<O_OBTENER_DATO_AERONAVES_CTY>();
            var lstRegistro1 = new List<O_IMAGEN_MTR_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_AERONAVES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));

                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            if (ob.decFiltro == 3)
                            {
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_IMAGEN_MTR_CTY();
                                        reg.TIPO_IMAGEN = re[0].ToString();
                                        reg.IMAGEN = (byte[])re[1];

                                        lstRegistro1.Add(reg);
                                    }
                                }
                            }
                            else
                            {
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_OBTENER_DATO_AERONAVES_CTY();
                                        reg.TIPO_MTR = re[0].ToString();
                                        reg.ID_EXPLOTADOR = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[1].ToString());
                                        reg.EXPLOTADOR = re[2].ToString();
                                        reg.PAIS_EXPLOTADOR = re[3].ToString();
                                        reg.CIUDAD_EXPLOTADOR = re[4].ToString();
                                        reg.DOMICILIO_EXPLOTADOR = re[5].ToString();
                                        reg.FECHA_REGISTRO = re[6].ToString();
                                        reg.LUGAR_REGISTRO = re[7].ToString();
                                        reg.USUARIO_NOMBRE = re[8].ToString();
                                        reg.IMEI = (re[9].ToString() == "") ? 0 : Convert.ToDecimal(re[9].ToString());
                                        reg.TIPO_USUARIO = re[10].ToString();
                                        reg.IDENTIFICADOR = re[11].ToString();
                                        reg.SUB_IDENTIFICADOR = re[12].ToString();
                                        reg.BARCODE = (re[13].ToString() == "") ? "0" : re[13].ToString();
                                        reg.CLASE = re[14].ToString();
                                        reg.MARCA = re[15].ToString();
                                        reg.SERVICIO = re[16].ToString();
                                        reg.MODELO = re[17].ToString();
                                        reg.SERIE = re[18].ToString();
                                        reg.GESTION_FABRICACION = (re[19].ToString() == "") ? 0 : Convert.ToDecimal(re[19].ToString());
                                        reg.CARGA = (re[20].ToString() == "") ? 0 : Convert.ToDecimal(re[20].ToString());
                                        reg.RA_TIPO_MATRICULA = re[21].ToString();
                                        reg.BASE_OPERACIONES = re[22].ToString();
                                        reg.CHASIS = re[23].ToString();
                                        reg.ESTADO_REPOSICION = (re[24].ToString() == "") ? 0 : Convert.ToDecimal(re[24].ToString());
                                        reg.ESTADO_BLOQUEO = (re[25].ToString() == "") ? 0 : Convert.ToDecimal(re[25].ToString());
                                        reg.MENSAJE_IMPRESION = re[26].ToString();
                                        reg.MENSAJE_PANTALLA = re[27].ToString();
                                        reg.ESTACION = re[28].ToString();
                                        reg.ID_PROPIETARIO = (re[29].ToString() == "") ? 0 : Convert.ToDecimal(re[29].ToString());
                                        reg.PROPIETARIO = re[30].ToString();
                                        reg.PAIS_PROPIETARIO = re[31].ToString();
                                        reg.CIUDAD_PROPIETARIO = re[32].ToString();
                                        reg.DOMICILIO_PROPIETARIO = re[33].ToString();
                                        reg.ESTADO = re[34].ToString();
                                        reg.BLOQUEO = re[35].ToString();
                                        reg.AUD_ESTADO = (re[36].ToString() == "") ? 0 : Convert.ToDecimal(re[36].ToString());
                                        reg.TOTAL_CXC = (re[37].ToString() == "") ? 0 : Convert.ToDecimal(re[37].ToString());
                                        reg.ID_PRODUCTO = Convert.ToDecimal(re[38].ToString());
                                        reg.PRODUCTO = re[39].ToString();

                                        lstRegistros.Add(reg);
                                    }
                                }
                            }

                            re.Close();
                            op0.Dispose();
                        }
                        if (ob.decFiltro == 3)
                        {
                            if (!lstRegistro1.IsNullOrEmpty())
                            {
                                _resultado.intCodigo = 1;
                                _resultado.strMensaje = "OK";
                                _resultado.oResultado = lstRegistro1;
                            }
                            else
                            {
                                _resultado.intCodigo = -1;
                                _resultado.strMensaje = "No se encontraron registros";
                                _resultado.oResultado = null;
                            }
                        }
                        else
                        {
                            if (!lstRegistros.IsNullOrEmpty())
                            {
                                _resultado.intCodigo = 1;
                                _resultado.strMensaje = "OK";
                                _resultado.oResultado = lstRegistros;
                            }
                            else
                            {
                                _resultado.intCodigo = -1;
                                _resultado.strMensaje = "No se encontraron registros";
                                _resultado.oResultado = null;
                            }
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

        public EPResultado ObtenerAcumuladoGrafico(EObjetoEnvio5ParametrosProto ob)
        {
            var lstRegistros = new List<O_OBTENER_ACUM_GRAFICO_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_ACUM_GRAFICO";
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
                                    var reg = new O_OBTENER_ACUM_GRAFICO_CTY();
                                    reg.ID_AGRUPA = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.AGRUPA = re[1].ToString();
                                    reg.PERIODO = re[2].ToString();
                                    reg.CANTIDAD = (re[3].ToString() == "") ? 0 : Convert.ToDecimal(re[3].ToString());
                                    lstRegistros.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (!lstRegistros.IsNullOrEmpty())
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                            _resultado.oResultado = lstRegistros;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListaErrorLog(EObjetoEnvio2ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_LOG_ERROR";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro2,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;
                            if (ob.decFiltro == 1 || ob.decFiltro == 3)
                            {
                                var lstRegistros = new List<O_LISTA_LOG_ERROR_CAB_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_LOG_ERROR_CAB_CTY();

                                        op1.CODIGO_ORACLE = re[0].ToString();
                                        op1.MENSAJE_ORACLE = re[1].ToString();
                                        op1.CANTIDAD = re.GetDecimal(2);
                                        lstRegistros.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                            }
                            else
                            {
                                var lstRegistros = new List<O_LISTA_LOG_ERROR_DET_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op = new O_LISTA_LOG_ERROR_DET_CTY();

                                        op.ID_ERROR = re.GetDecimal(0);
                                        op.AUD_FECHA = re[1].ToString();
                                        op.AUD_USUARIO = re[2].ToString();
                                        op.APLICACION = re[3].ToString();
                                        op.PROCEDIMIENTO = re[4].ToString();
                                        op.MENSAJE_PERSONALIZADO = re[5].ToString();
                                        op.MENSAJE_ORACLE = re[6].ToString();
                                        op.ID_ORACLE_ERROR = re.GetDecimal(7);
                                        op.ID_ORACLE_MENSAJE = re[8].ToString();
                                        lstRegistros.Add(op);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListaControlUsuario(EListaControlesUsuario ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_CONTROLES_USUARIO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decIdUsuario,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;
                            var lstRegistros = new List<O_LISTA_CONTROLES_USUARIO>();
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var op1 = new O_LISTA_CONTROLES_USUARIO();
                                    op1.ID_CONTROL_USUARIO = re.GetDecimal(0); //re[0].ToString();
                                    op1.FECHA_INICIO = re.GetDecimal(1);
                                    op1.FECHA_FIN = re.GetDecimal(2);
                                    op1.ID_TIPO_ACTIVIDAD = re.GetDecimal(3);
                                    op1.ID_UBICACION = re.GetDecimal(4);
                                    op1.OBSERVACIONES = re[5].ToString();
                                    lstRegistros.Add(op1);
                                }
                            }
                            _resultado.oResultado = lstRegistros;

                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public decimal VerificaUsuario(EVerificaUsuario ob)
        {
            try
            {
                decimal a = 0;
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT APP_MOVIMIENTOS.PMOV_CONSULTAS.F_VERIFICA_ID_USUARIO(" + ob.decIdUsuario + "," + ob.decIdModulo + ") FROM DUAL";
                        cmd.CommandType = CommandType.Text;
                        //ejecutar el oraclecommand
                        OracleDataReader lDr = cmd.ExecuteReader();
                        //mostrar el resultado
                        if (lDr.Read() == true)
                        {
                            a = Convert.ToDecimal(lDr[0]);
                        }
                        return a;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public EPResultado ListaActividades(string listaActividades)
        {
            string cadena = "SELECT ID_TIPO_ACTIVIDAD, ID_CATEGORIA_ACTIVIDAD, DESCRIPCION FROM ANH_PAR.TPAR_TIPOS_ACTIVIDAD WHERE ID_TIPO_ACTIVIDAD IN (" + listaActividades + ")";
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = cadena;
                        cmd.CommandType = CommandType.Text;
                        //ejecutar el oraclecommand
                        OracleDataReader lDr = cmd.ExecuteReader();
                        var lstActividades = new List<O_TIPOS_ACTIVIDAD_CTY>();
                        //mostrar el resultado
                        //if (lDr.Read() == true)
                        //{
                        while (lDr.Read())
                        {
                            if (lDr.HasRows)
                            {
                                var actividad = new O_TIPOS_ACTIVIDAD_CTY();
                                actividad.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(lDr[0]);
                                actividad.ID_CATEGORIA_ACTIVIDAD = Convert.ToDecimal(lDr[1]);
                                actividad.DESCRIPCION = (lDr[2]).ToString();
                                lstActividades.Add(actividad);
                            }
                        }
                        _resultado.intCodigo = 1;
                        _resultado.strMensaje = "OK";
                        _resultado.oResultado = lstActividades;
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

        public EPResultado ListaComparativoEnt(EObjetoEnvio1ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_COMPARATIVO_ENT";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;
                            var lstRegistros = new List<O_LISTA_COMPARATIVO_ENT_CTY>();
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var op1 = new O_LISTA_COMPARATIVO_ENT_CTY();

                                    op1.HYD_DEPTO = re[0].ToString();
                                    op1.HYD_ID_ENTIDAD = re.GetDecimal(1);
                                    op1.HYD_ID_CONSUMIDOR = re.GetDecimal(2);
                                    op1.HYD_NIT = re[3].ToString();
                                    op1.HYD_DENOMINACION = re[4].ToString();
                                    op1.HYD_DIRECCION = re[5].ToString();
                                    op1.HYD_SIST_GEST = re[6].ToString();
                                    op1.HYD_LICENCIA = re[7].ToString();
                                    op1.HYD_LIC_VIGENCIA = re[8].ToString();
                                    op1.BSA_DEPTO = re[9].ToString();
                                    op1.BSA_ID_ENT_HYD = re.GetDecimal(10);
                                    op1.BSA_NIT = re.GetDecimal(11);
                                    op1.BSA_DENOMINACION = re[12].ToString();
                                    op1.BSA_DIRECCION = re[13].ToString();
                                    op1.BSA_SIST_GEST = re[14].ToString();
                                    op1.BSA_COD_EESS = re[15].ToString();
                                    op1.BSA_VERSION_MDW = re.GetDecimal(16);
                                    op1.BSA_ID_HYLIQ = re.GetDecimal(17);
                                    op1.BSA_ID_HYGNV = re.GetDecimal(18);

                                    lstRegistros.Add(op1);
                                }
                            }
                            _resultado.oResultado = lstRegistros;

                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListaPrecioHidroc(EObjetoEnvio2ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_PRECIOS_HID";

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro2,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            _resultado.oResultado = null;
                            if (ob.decFiltro == 1 || ob.decFiltro == 3)
                            {
                                var lstRegistros = new List<O_LISTA_PRECIOS_HID_CAB_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op1 = new O_LISTA_PRECIOS_HID_CAB_CTY();
                                        op1.ID_TABLA = re.GetDecimal(0);
                                        op1.CATEGORIA = re[1].ToString();
                                        op1.ID_CATEGORIA_VEHICULO = re.GetDecimal(2);
                                        op1.HIDROCARBURO = re[3].ToString();
                                        op1.HIDRO = re[4].ToString();
                                        op1.ID_HIDROCARBURO = re.GetDecimal(5);
                                        op1.PRECIO_BS = re.GetDecimal(6);
                                        op1.FECHA_DESDE = re[7].ToString();
                                        lstRegistros.Add(op1);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                            }
                            else
                            {
                                var lstRegistros = new List<O_LISTA_PRECIOS_HID_DET_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var op = new O_LISTA_PRECIOS_HID_DET_CTY();
                                        op.ID_TABLA = re.GetDecimal(0);
                                        op.FECHA_DESDE = re[1].ToString();
                                        op.FECHA_HASTA = re[2].ToString();
                                        op.PRECIO_BS = re.GetDecimal(3);
                                        op.FECHA_REGISTRO = re[4].ToString();
                                        lstRegistros.Add(op);
                                    }
                                }
                                _resultado.oResultado = lstRegistros;
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros";
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

        public EPResultado ListaRecepciones(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new List<O_LISTA_RECEPCIONES_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_RECEPCIONES";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LISTA_RECEPCIONES_CTY();
                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.FECHA_OPERACION = re.GetDecimal(1);
                                    reg.IDENTIFICADOR = re[2].ToString();
                                    reg.TIPO_TRANSACCION = re[3].ToString();
                                    reg.FECHA_HORA_EMISION = re.GetDecimal(4);
                                    reg.NRO_TRANSACCION = re[5].ToString();
                                    reg.NRO_AUTORIZACION = re.GetDecimal(6);
                                    reg.CODIGO_CONTROL = re[7].ToString();
                                    reg.NIT_EMISOR = re.GetDecimal(8);
                                    reg.RAZON_SOCIAL = re[9].ToString();
                                    reg.ID_ESTADO_MOVIMIENTO = re.GetDecimal(10);
                                    reg.ID_HIDROCARBURO = re.GetDecimal(11);
                                    reg.PRODUCTO = re[12].ToString();
                                    reg.PRECIO_UNITARIO = re.GetDecimal(14);
                                    reg.NRO_DESPACHO = re[16].ToString();
                                    reg.PRECINTOS = re[17].ToString();

                                    if (reg.ID_ESTADO_MOVIMIENTO == 11)
                                    {
                                        reg.CANTIDAD = re.GetDecimal(13);
                                        reg.STR_ESTADO_MOVIMIENTO = "V";
                                        reg.PRECIO_TOTAL = re.GetDecimal(15);
                                    }
                                    else
                                    {
                                        reg.CANTIDAD = 0;
                                        reg.STR_ESTADO_MOVIMIENTO = "A";
                                        reg.PRECIO_TOTAL = 0;
                                    }

                                    var strFec = reg.FECHA_OPERACION.ToString();
                                    reg.STR_FECHA_OPERACION = strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4) + " " +
                                                              strFec.Substring(8, 2) + ":" + strFec.Substring(10, 2) + ":" + strFec.Substring(12, 2);
                                    strFec = reg.FECHA_HORA_EMISION.ToString();
                                    reg.STR_FECHA_HORA_EMISION = strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4);


                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ListaMoviTran(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_LISTA_MOVI_TRAN_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_MOVI_TRAN";
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
                                    var reg = new O_LISTA_MOVI_TRAN_CTY();
                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.ID_TIPO_TRANSACCION = re.GetDecimal(1);
                                    reg.TIPO_TRANSACCION = re[2].ToString();
                                    reg.FECHA_OPERACION = re.GetDecimal(3);
                                    reg.FECHA_HORA_EMISION = re.GetDecimal(4);
                                    reg.IDENTIFICADOR = re[5].ToString();
                                    reg.ID_HIDROCARBURO = re.GetDecimal(6);
                                    reg.PRODUCTO = re[7].ToString();
                                    reg.MOV_OBS = re[9].ToString();
                                    reg.ID_ESTADO_MOVIMIENTO = re.GetDecimal(10);
                                    reg.NRO_TRANSACCION = re[11].ToString();
                                    reg.NRO_AUTORIZACION = re[12].ToString(); //re.GetDecimal(12);
                                    reg.CODIGO_CONTROL = re[13].ToString();
                                    reg.NIT_EMISOR = re.GetDecimal(14);
                                    reg.NIT_CONSUMIDOR = re.GetDecimal(15);
                                    reg.RAZON_SOCIAL = re[16].ToString();
                                    reg.PRECIO_UNITARIO = re.GetDecimal(17);
                                    reg.USUARIO_TRANSACCION = re[19].ToString();
                                    reg.TRA_OBS = re[20].ToString();
                                    reg.BINARIO_CONTROL = re[21].ToString();
                                    reg.ID_UNIDAD_ENVASES = re.GetDecimal(22);
                                    reg.ID_FORMA_PAGO = re.GetDecimal(23);
                                    reg.ENVASE = re[24].ToString();
                                    reg.ID_TIPO_FLUJO = re.GetDecimal(25);
                                    reg.ID_TIPO_MOVIMIENTO = re.GetDecimal(26);
                                    reg.ID_UBICACION_OPERACION = re.GetDecimal(27);
                                    reg.PAIS = re[28].ToString();
                                    reg.TIPO_CONSUMIDOR = re[29].ToString();
                                    reg.BARCODE = re[30].ToString();
                                    reg.ID_TRANSACCION = re[31].ToString();
                                    reg.DESC_FUENTE_ORIGEN = re[32].ToString();
                                    if (reg.ID_ESTADO_MOVIMIENTO == 11)
                                    {
                                        reg.CANTIDAD = re.GetDecimal(8);
                                        reg.CANTIDAD_MOV = (reg.BINARIO_CONTROL.Substring(0, 1) == "1") ? reg.CANTIDAD : 0;
                                        reg.PRECIO_TOTAL = re.GetDecimal(18);
                                    }
                                    else
                                    {
                                        reg.CANTIDAD = 0;
                                        reg.CANTIDAD_MOV = 0;
                                        reg.PRECIO_TOTAL = 0;
                                    }

                                    var strFec = reg.FECHA_OPERACION.ToString();
                                    reg.STR_FECHA_OPERACION = strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4) + " " +
                                                              strFec.Substring(8, 2) + ":" + strFec.Substring(10, 2) + ":" + strFec.Substring(12, 2);
                                    reg.FECHA_MOV = reg.STR_FECHA_OPERACION.Substring(0, 10);
                                    reg.HORA_MOV = reg.STR_FECHA_OPERACION.Substring(11, 8);
                                    strFec = reg.FECHA_HORA_EMISION.ToString();
                                    reg.STR_FECHA_HORA_EMISION = strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4) + " " +
                                                              strFec.Substring(8, 2) + ":" + strFec.Substring(10, 2) + ":" + strFec.Substring(12, 2);
                                    reg.STR_ESTADO_MOVIMIENTO = (reg.ID_ESTADO_MOVIMIENTO == 11) ? "V" : "A";
                                    reg.CANTIDAD_MOV = (reg.BINARIO_CONTROL.Substring(0, 1) == "1") ? reg.CANTIDAD : 0;

                                    reg.FORMA_PAGO = (reg.ID_FORMA_PAGO == 1) ? "EFECTIVO" : (reg.ID_FORMA_PAGO == 2) ? "PRE-PAGO" : "VALE";
                                    reg.TIPO_FLUJO = (reg.ID_TIPO_FLUJO == 20) ? "FISICO" : "CONTABLE";
                                    reg.TIPO_MOVIMIENTO = (reg.ID_TIPO_MOVIMIENTO == 18) ? "NACIONAL A NACIONAL" : (reg.ID_TIPO_MOVIMIENTO == 17) ? "NACIONAL A INTERNACIONAL(EXPORTACION)" : (reg.ID_TIPO_MOVIMIENTO == 37) ? "SIVETUR" : "INTERNACIONAL A NACIONAL(IMPORTACION)";

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

        public EPResultado ListaComparativoInv(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_COMPARATIVO_FIS_DOC_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_COMPARATIVO_FIS_DOC";
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
                                    var reg = new O_COMPARATIVO_FIS_DOC_CTY();
                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.FECHA_OPERACION = re.GetDecimal(1);
                                    reg.TIPO_INFRAESTRUCTURA = re.GetDecimal(2);
                                    reg.TIPO = re[3].ToString();
                                    reg.ID_INFRAESTRUCTURA = re.GetDecimal(4);
                                    reg.CODIGO_OPERADOR = re[5].ToString();
                                    reg.ID_HIDROCARBURO = re.GetDecimal(6);
                                    reg.PRODUCTO = re[7].ToString();
                                    reg.ID_UNIDAD_HIDROCARBURO = re.GetDecimal(8);
                                    reg.U_MEDIDA = re[9].ToString();
                                    reg.SALDO_FIS = re.GetDecimal(10);
                                    reg.APP_ID_USUARIO = re.GetDecimal(11);
                                    reg.APP_FECHA_REGISTRO = re[12].ToString();
                                    reg.SALDO_DOC = re.GetDecimal(13);

                                    var strFec = reg.FECHA_OPERACION.ToString();
                                    reg.FECHA = strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4) + " " +
                                                              strFec.Substring(8, 2) + ":" + strFec.Substring(10, 2) + ":" + strFec.Substring(12, 2);
                                    reg.DIFERENCIA = reg.SALDO_FIS - reg.SALDO_DOC;

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ReporteKardex(EObjetoEnvio7ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_KARDEX_CTY>();
            try
            {
                decimal decSaldo = 0;
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_REPORTE_KARDEX";
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
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, ob.strParametro6,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("9", OracleDbType.Varchar2, ob.strParametro7,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("10", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_KARDEX_CTY();
                                    reg.TIPO_INFRAESTRUCTURA = re.GetDecimal(0);
                                    reg.ID_INFRAESTRUCTURA = re.GetDecimal(1);
                                    reg.INF_TIPO = re[2].ToString();
                                    reg.INF_CODIGO = re[3].ToString();
                                    reg.ID_HIDROCARBURO = re.GetDecimal(4);
                                    reg.PRODUCTO = re[5].ToString();
                                    reg.ID_UNIDAD_HIDROCARBURO = re.GetDecimal(6);
                                    reg.U_MEDIDA = re[7].ToString();
                                    reg.FECHA_OPERACION = re.GetDecimal(8);
                                    reg.ID_TIPO_OPERACION = re.GetDecimal(9);
                                    reg.TIPO_OPERACION = re[10].ToString();
                                    reg.FACTOR_CALCULO = re.GetDecimal(11);
                                    reg.CANTIDAD = re.GetDecimal(12);
                                    reg.IDENTIFICADOR = re[13].ToString();

                                    string strFec;
                                    if (reg.FECHA_OPERACION == 0)
                                    {
                                        decSaldo = reg.CANTIDAD;
                                        reg.INGRESO = 0;
                                        reg.SALIDA = 0;
                                        strFec = ob.strParametro6;
                                    }
                                    else
                                    {
                                        reg.INGRESO = (reg.FACTOR_CALCULO > 0) ? reg.CANTIDAD : 0;
                                        reg.SALIDA = (reg.FACTOR_CALCULO < 0) ? reg.CANTIDAD : 0;
                                        decSaldo = decSaldo + (reg.CANTIDAD * reg.FACTOR_CALCULO);
                                        strFec = reg.FECHA_OPERACION.ToString();
                                    }
                                    reg.FECHA = DateTime.Parse(strFec.Substring(6, 2) + "/" + strFec.Substring(4, 2) + "/" + strFec.Substring(0, 4) + " " +
                                                  strFec.Substring(8, 2) + ":" + strFec.Substring(10, 2) + ":" + strFec.Substring(12, 2));
                                    reg.SALDO = decSaldo;
                                    if (ob.decFiltro == 1)
                                    {
                                        reg.ID_AGRUPACION = reg.TIPO_INFRAESTRUCTURA + reg.ID_INFRAESTRUCTURA + reg.ID_HIDROCARBURO;
                                        reg.AGRUPACION = reg.INF_TIPO + " - " + reg.INF_CODIGO + " - " + reg.PRODUCTO + " - " + reg.U_MEDIDA;
                                    }
                                    else
                                    {
                                        reg.ID_AGRUPACION = reg.ID_HIDROCARBURO;
                                        reg.AGRUPACION = reg.PRODUCTO + " - " + reg.U_MEDIDA;
                                    }
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ListaObligacionDia(EObjetoEnvio6ParametrosProto ob)
        {
            var objDatos = new List<O_CUMPLIMIENTO_DIA_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REPORTE_DIA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, Convert.ToInt32(ob.strParametro1),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, Convert.ToInt32(ob.strParametro2),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, Convert.ToInt32(ob.strParametro3),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, Convert.ToInt32(ob.strParametro4),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("7", OracleDbType.Int32, Convert.ToInt32(ob.strParametro5),
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Int32, Convert.ToInt32(ob.strParametro6),
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_CUMPLIMIENTO_DIA_CTY();
                                    reg.ID_ACTIVIDAD = re.GetDecimal(0);
                                    reg.DEPARTAMENTO = re[1].ToString();
                                    reg.ID_CONSUMIDOR = re.GetDecimal(2);
                                    reg.DENOMINACION = re[3].ToString();
                                    reg.TIPO_OP = re[4].ToString();
                                    reg.ID_TIPO_OPERACION = re.GetDecimal(5);
                                    reg.TIPO_OPERACION = re[6].ToString();
                                    reg.ID_PRODUCTO = re.GetDecimal(7);
                                    reg.PRODUCTO = re[8] + " - " + re[9];
                                    reg.UNIDAD_MEDIDA = re[9].ToString();
                                    reg.CANTIDAD = re.GetDecimal(10);

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public ERepMesResultado ListaObligacionMes(EObjetoEnvio5ParametrosProto ob)
        {
            var oResultado = new ERepMesResultado();
            var objDatos = new List<O_LISTA_REPORTE_MENSUAL_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REPORTE_MENSUAL";
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
                                    var reg = new O_LISTA_REPORTE_MENSUAL_CTY();
                                    reg.ESTADO_MENSUAL = re.GetDecimal(0);
                                    reg.MENSAJE_MENSUAL = re[1].ToString();
                                    reg.ID_CONSUMIDOR = re.GetDecimal(2);
                                    reg.NIT = re[3].ToString();
                                    reg.DIRECCION = re[4].ToString();
                                    reg.NOMBRES_REPRESENTANTE = re[5].ToString();
                                    reg.ID_TIPO_ACTIVIDAD = re.GetDecimal(6);
                                    reg.DEPARTAMENTO = re[7].ToString();
                                    reg.DENOMINACION = re[8].ToString();
                                    reg.FECHA_REPORTE = (re[9].ToString() == "") ? 0 : re.GetDecimal(9);
                                    reg.CITE_GENERADO = re[10].ToString();
                                    reg.JSON_REPORTE_DATOS = re[11].ToString();
                                    reg.LATITUD = re.GetDecimal(12);
                                    reg.LONGITUD = re.GetDecimal(13);
                                    reg.strUltEstado = (re.GetDecimal(14) == 4) ? "SI" : "NO";

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
                        {
                            oResultado.intCodigo = 1;
                            oResultado.strMensaje = "OK";
                            oResultado.oResultado = objDatos;
                        }
                        else
                        {
                            oResultado.intCodigo = -1;
                            oResultado.strMensaje = "No se encontraron registros.";
                            oResultado.oResultado = null;
                        }
                        return oResultado;
                    }
                }
            }
            catch (Exception ex)
            {
                oResultado.intCodigo = -100;
                oResultado.strMensaje = ex.Message;
                oResultado.oResultado = null;
                return oResultado;
            }
        }

        public EPResultado ListadoDdjj(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_LISTADO_DDJJ_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTADO_DDJJ";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        switch (Convert.ToInt16(ob.strParametro5))
                        {
                            case 1:
                                #region lista acumulada de movimientos
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    while (re.Read())
                                    {
                                        if (!re.HasRows) continue;

                                        var intAux = 0;
                                        var reg = new O_LISTADO_DDJJ_CTY();
                                        O_LISTADO_DDJJ_CTY reg1;
                                        var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(re[7].ToString());
                                        var lstAcumulado = objDdjj.LstAcumulado;

                                        var strValFec = "";

                                        foreach (var fila in lstAcumulado)
                                        {
                                            if (intAux > 0)
                                            {
                                                reg = new O_LISTADO_DDJJ_CTY();
                                                intAux = 1;
                                            }
                                            reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                            reg.PROYECTO = re[1].ToString();
                                            reg.DEPTO = re[2].ToString();
                                            reg.ID_CITE = re.GetDecimal(3);
                                            reg.CITE = re[4].ToString();
                                            reg.FECHA_REPORTE = re.GetDecimal(5);
                                            var strMesGestion = re[6].ToString().Split('-');
                                            var strMesNum = Convert.ToDateTime("01/" + strMesGestion[0].Trim() + "/" + strMesGestion[1].Trim()).Month.ToString().PadLeft(2, '0');
                                            reg.strMes = strMesNum + " - " + strMesGestion[0].Trim();
                                            reg.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                            reg.PERIODO = strMesNum + "/" + strMesGestion[1].Trim();
                                            reg.datFechaRegistro = CFechas.ConvierteLongDateTime(re.GetDecimal(5));
                                            reg.strDia = fila.DecFecha.ToString().Substring(6, 2) + "/" + fila.DecFecha.ToString().Substring(4, 2) + "/" + fila.DecFecha.ToString().Substring(0, 4);
                                            reg.strProducto = fila.StrCodHidrocarburo + " - " + fila.StrUnidadMedida;
                                            reg.strTipoInf = fila.StrTipoInfraestructura;
                                            reg.strInf = fila.StrInfraestructura;

                                            reg1 = new O_LISTADO_DDJJ_CTY();
                                            reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                            reg1.PROYECTO = reg.PROYECTO;
                                            reg1.DEPTO = reg.DEPTO;
                                            reg1.ID_CITE = reg.ID_CITE;
                                            reg1.CITE = reg.CITE;
                                            reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                            reg1.PERIODO = reg.PERIODO;
                                            reg1.strMes = reg.strMes;
                                            reg1.strGestion = reg.strGestion;
                                            reg1.datFechaRegistro = reg.datFechaRegistro;
                                            reg1.strDia = reg.strDia;
                                            reg1.strProducto = reg.strProducto;
                                            reg1.strTipoInf = reg.strTipoInf;
                                            reg1.strInf = reg.strInf;
                                            reg1.strFactorTipo = "A - SALDO INICIAL";
                                            reg1.strTipoOp = "SALDO INICIAL DIARIO";
                                            reg1.decCantidad = fila.DecSaldoInicial;
                                            objDatos.Add(reg1);

                                            reg1 = new O_LISTADO_DDJJ_CTY();
                                            reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                            reg1.PROYECTO = reg.PROYECTO;
                                            reg1.DEPTO = reg.DEPTO;
                                            reg1.ID_CITE = reg.ID_CITE;
                                            reg1.CITE = reg.CITE;
                                            reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                            reg1.PERIODO = reg.PERIODO;
                                            reg1.strMes = reg.strMes;
                                            reg1.strGestion = reg.strGestion;
                                            reg1.datFechaRegistro = reg.datFechaRegistro;
                                            reg1.strDia = reg.strDia;
                                            reg1.strProducto = reg.strProducto;
                                            reg1.strTipoInf = reg.strTipoInf;
                                            reg1.strInf = reg.strInf;
                                            reg1.strFactorTipo = "A - SALDO INICIAL";
                                            reg1.strTipoOp = "SALDO INICIAL GRAL.";
                                            reg1.decCantidad = (reg.strDia.Substring(0, 2) == "01") ? fila.DecSaldoInicial : 0;
                                            objDatos.Add(reg1);

                                            var strUltDia = "";
                                            if (strValFec != reg.strDia)
                                            {
                                                strUltDia = DateTime.DaysInMonth(Convert.ToInt16(reg.strDia.Substring(6, 4)), Convert.ToInt16(reg.strDia.Substring(3, 2))).ToString().PadLeft(2, '0') + reg.strDia.Substring(2, 8);
                                                strValFec = reg.strDia;
                                            }

                                            reg1 = new O_LISTADO_DDJJ_CTY();
                                            reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                            reg1.PROYECTO = reg.PROYECTO;
                                            reg1.DEPTO = reg.DEPTO;
                                            reg1.ID_CITE = reg.ID_CITE;
                                            reg1.CITE = reg.CITE;
                                            reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                            reg1.PERIODO = reg.PERIODO;
                                            reg1.strMes = reg.strMes;
                                            reg1.strGestion = reg.strGestion;
                                            reg1.datFechaRegistro = reg.datFechaRegistro;
                                            reg1.strDia = reg.strDia;
                                            reg1.strProducto = reg.strProducto;
                                            reg1.strTipoInf = reg.strTipoInf;
                                            reg1.strInf = reg.strInf;
                                            reg1.strFactorTipo = "D - SALDO FINAL";
                                            reg1.strTipoOp = "SALDO FINAL GRAL.";
                                            reg1.decCantidad = (reg.strDia == strUltDia) ? fila.DecSaldoFinal : 0;
                                            objDatos.Add(reg1);

                                            reg1 = new O_LISTADO_DDJJ_CTY();
                                            reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                            reg1.PROYECTO = reg.PROYECTO;
                                            reg1.DEPTO = reg.DEPTO;
                                            reg1.ID_CITE = reg.ID_CITE;
                                            reg1.CITE = reg.CITE;
                                            reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                            reg1.PERIODO = reg.PERIODO;
                                            reg1.strMes = reg.strMes;
                                            reg1.strGestion = reg.strGestion;
                                            reg1.datFechaRegistro = reg.datFechaRegistro;
                                            reg1.strDia = reg.strDia;
                                            reg1.strProducto = reg.strProducto;
                                            reg1.strTipoInf = reg.strTipoInf;
                                            reg1.strInf = reg.strInf;
                                            reg1.strFactorTipo = "D - SALDO FINAL";
                                            reg1.strTipoOp = "SALDO FINAL DIARIO";
                                            reg1.decCantidad = fila.DecSaldoFinal;
                                            objDatos.Add(reg1);


                                            var lstIngreso = fila.LstIngresos;
                                            foreach (var rIng in lstIngreso)
                                            {
                                                reg.strTipoOp = rIng.OPERACION_NOMBRE;
                                                reg.decCantidad = rIng.CANTIDAD;

                                                reg1 = new O_LISTADO_DDJJ_CTY();
                                                reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                                reg1.PROYECTO = reg.PROYECTO;
                                                reg1.DEPTO = reg.DEPTO;
                                                reg1.ID_CITE = reg.ID_CITE;
                                                reg1.CITE = reg.CITE;
                                                reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                                reg1.PERIODO = reg.PERIODO;
                                                reg1.strMes = reg.strMes;
                                                reg1.strGestion = reg.strGestion;
                                                reg1.datFechaRegistro = reg.datFechaRegistro;
                                                reg1.strDia = reg.strDia;
                                                reg1.strProducto = reg.strProducto;
                                                reg1.strTipoInf = reg.strTipoInf;
                                                reg1.strInf = reg.strInf;
                                                reg1.strFactorTipo = "B - INGRESOS";
                                                reg1.strTipoOp = reg.strTipoOp;
                                                reg1.decCantidad = reg.decCantidad;
                                                objDatos.Add(reg1);
                                            }

                                            var lstEgreso = fila.LstEgresos;
                                            foreach (var rIng in lstEgreso)
                                            {
                                                reg.strTipoOp = rIng.OPERACION_NOMBRE;
                                                reg.decCantidad = rIng.CANTIDAD;

                                                reg1 = new O_LISTADO_DDJJ_CTY();
                                                reg1.ID_CONSUMIDOR = reg.ID_CONSUMIDOR;
                                                reg1.PROYECTO = reg.PROYECTO;
                                                reg1.DEPTO = reg.DEPTO;
                                                reg1.ID_CITE = reg.ID_CITE;
                                                reg1.CITE = reg.CITE;
                                                reg1.FECHA_REPORTE = reg.FECHA_REPORTE;
                                                reg1.PERIODO = reg.PERIODO;
                                                reg1.strMes = reg.strMes;
                                                reg1.strGestion = reg.strGestion;
                                                reg1.datFechaRegistro = reg.datFechaRegistro;
                                                reg1.strDia = reg.strDia;
                                                reg1.strProducto = reg.strProducto;
                                                reg1.strTipoInf = reg.strTipoInf;
                                                reg1.strInf = reg.strInf;
                                                reg1.strFactorTipo = "C - SALIDAS";
                                                reg1.strTipoOp = reg.strTipoOp;
                                                reg1.decCantidad = reg.decCantidad;
                                                objDatos.Add(reg1);
                                            }
                                        }
                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                            case 2:
                                #region lista vta internacional
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    while (re.Read())
                                    {
                                        if (!re.HasRows) continue;
                                        var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(re[7].ToString());
                                        var lstAcumulado = objDdjj.LstaAdicional2;
                                        //var lstAcumulado = objDdjj.LstaAdicional2.Where(l => l.tipoMovimiento != "SIVETUR").ToList();

                                        if (lstAcumulado != null)
                                        {
                                            foreach (var fila in lstAcumulado)
                                            {
                                                if (fila.idTipoEmisor == 14 && fila.idEstadoTransaccion == 11)
                                                {
                                                    var reg = new O_LISTADO_DDJJ_CTY();
                                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                                    reg.PROYECTO = re[1].ToString();
                                                    reg.DEPTO = re[2].ToString();
                                                    reg.ID_CITE = re.GetDecimal(3);
                                                    reg.CITE = re[4].ToString();
                                                    reg.FECHA_REPORTE = re.GetDecimal(5);

                                                    var strMesGestion = re[6].ToString().Split('-');
                                                    var strMesNum = Convert.ToDateTime("01/" + strMesGestion[0].Trim() + "/" + strMesGestion[1].Trim()).Month.ToString().PadLeft(2, '0');
                                                    reg.strMes = strMesNum + " - " + strMesGestion[0].Trim();
                                                    reg.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                                    reg.PERIODO = strMesNum + "/" + strMesGestion[1].Trim();
                                                    reg.datFechaRegistro = CFechas.ConvierteLongDateTime(re.GetDecimal(5));
                                                    reg.strDia = fila.fechaOperacion.ToString().Substring(6, 2) + "/" + fila.fechaOperacion.ToString().Substring(4, 2) + "/" + fila.fechaOperacion.ToString().Substring(0, 4);
                                                    reg.strProducto = fila.codHidrocarburo + " - " + fila.unidadMedida;
                                                    reg.decCantidad = fila.cantidadHidrocarburo;
                                                    reg.strPais = fila.ubicacionOperacion;
                                                    reg.strIdentificador = fila.identificador;
                                                    reg.strCliente = fila.nitConsumidor + " - " + fila.razonSocial;

                                                    objDatos.Add(reg);
                                                }
                                            }
                                        }



                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                            case 3:
                                #region lista vta internacional
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    while (re.Read())
                                    {
                                        if (!re.HasRows) continue;
                                        var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(re[7].ToString());
                                        var lstAcumulado = objDdjj.LstaAdicional1;

                                        if (lstAcumulado != null)
                                        {
                                            foreach (var fila in lstAcumulado)
                                            {
                                                if (fila.idTipoTransaccion == 7 && fila.idEstadoTransaccion == 11)
                                                {
                                                    var reg = new O_LISTADO_DDJJ_CTY();
                                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                                    reg.PROYECTO = re[1].ToString();
                                                    reg.DEPTO = re[2].ToString();
                                                    reg.ID_CITE = re.GetDecimal(3);
                                                    reg.CITE = re[4].ToString();
                                                    reg.FECHA_REPORTE = re.GetDecimal(5);

                                                    var strMesGestion = re[6].ToString().Split('-');
                                                    var strMesNum = Convert.ToDateTime("01/" + strMesGestion[0].Trim() + "/" + strMesGestion[1].Trim()).Month.ToString().PadLeft(2, '0');
                                                    reg.strMes = strMesNum + " - " + strMesGestion[0].Trim();
                                                    reg.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                                    reg.PERIODO = strMesNum + "/" + strMesGestion[1].Trim();
                                                    reg.datFechaRegistro = CFechas.ConvierteLongDateTime(re.GetDecimal(5));
                                                    reg.strDia = fila.fechaOperacion.ToString().Substring(6, 2) + "/" + fila.fechaOperacion.ToString().Substring(4, 2) + "/" + fila.fechaOperacion.ToString().Substring(0, 4);
                                                    reg.strProducto = fila.codHidrocarburo + " - " + fila.unidadMedida;
                                                    reg.decCantidad = fila.cantidadHidrocarburo;
                                                    //reg.strPais = fila.ubicacionOperacion;
                                                    reg.strIdentificador = fila.identificador;
                                                    reg.strCliente = fila.nitConsumidor + " - " + fila.razonSocial;
                                                    reg.strNroSustCtrl = fila.numeroTransaccion;
                                                    reg.strTipoEnvase = fila.tipoEnvase;

                                                    objDatos.Add(reg);
                                                }
                                            }
                                        }
                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                            case 4:
                                #region lista vta sivetur
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    while (re.Read())
                                    {
                                        if (!re.HasRows) continue;
                                        var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(re[7].ToString());
                                        var lstAcumulado = objDdjj.LstaAdicional2.Where(l => l.tipoMovimiento == "SIVETUR").ToList();

                                        if (lstAcumulado != null)
                                        {
                                            foreach (var fila in lstAcumulado)
                                            {
                                                if (fila.idTipoEmisor == 14 && fila.idEstadoTransaccion == 11)
                                                {
                                                    var reg = new O_LISTADO_DDJJ_CTY();
                                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                                    reg.PROYECTO = re[1].ToString();
                                                    reg.DEPTO = re[2].ToString();
                                                    reg.ID_CITE = re.GetDecimal(3);
                                                    reg.CITE = re[4].ToString();
                                                    reg.FECHA_REPORTE = re.GetDecimal(5);

                                                    var strMesGestion = re[6].ToString().Split('-');
                                                    var strMesNum = Convert.ToDateTime("01/" + strMesGestion[0].Trim() + "/" + strMesGestion[1].Trim()).Month.ToString().PadLeft(2, '0');
                                                    reg.strMes = strMesNum + " - " + strMesGestion[0].Trim();
                                                    reg.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                                    reg.PERIODO = strMesNum + "/" + strMesGestion[1].Trim();
                                                    reg.datFechaRegistro = CFechas.ConvierteLongDateTime(re.GetDecimal(5));
                                                    reg.strDia = fila.fechaOperacion.ToString().Substring(6, 2) + "/" + fila.fechaOperacion.ToString().Substring(4, 2) + "/" + fila.fechaOperacion.ToString().Substring(0, 4);
                                                    reg.strProducto = fila.codHidrocarburo + " - " + fila.unidadMedida;
                                                    reg.decCantidad = fila.cantidadHidrocarburo;
                                                    reg.strPais = fila.ubicacionOperacion;
                                                    reg.strIdentificador = fila.identificador;
                                                    reg.strCliente = fila.nitConsumidor + " - " + fila.razonSocial;

                                                    objDatos.Add(reg);
                                                }
                                            }
                                        }



                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                        }

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteMovDetalleDdjj(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_MOV_DETALLE_DDJJ_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_MOV_DETALLE_DDJJ";
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

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_MOV_DETALLE_DDJJ_CTY();

                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.ID_TIPO_ACTIVIDAD = re.GetDecimal(1);
                                    reg.ID_TIPO_OPERACION = re.GetDecimal(2);
                                    reg.ID_CONSUMIDOR = re.GetDecimal(3);
                                    reg.ID_INTEGRACION = re.GetDecimal(4);
                                    reg.ID_TRANSACCION = re[5].ToString();
                                    reg.DEPTO = re[6].ToString();
                                    reg.PROYECTO = re[7].ToString();

                                    var decAux = Convert.ToDecimal(re[8] + "000000");
                                    reg.FECHA_OPERACION_DIA = CFechas.ConvierteLongDateTime(decAux).ToShortDateString();
                                    reg.FECHA_OPERACION = CFechas.ConvierteLongDateTime(re.GetInt64(9));
                                    reg.PROY_ORIGEN = re[10].ToString();
                                    reg.TIPO_INFRAESTRUCTURA = re.GetDecimal(11);
                                    reg.INF_TIPO = re[12].ToString();
                                    reg.ID_INFRAESTRUCTURA = re.GetDecimal(13);
                                    reg.INF_CODIGO = re[14].ToString();
                                    reg.TIPO_DESPACHO = re[15].ToString();
                                    reg.ENVASE = re[16].ToString();
                                    reg.CANTIDAD_ENVASES = re.GetDecimal(17);
                                    reg.ESTADO = re[18].ToString();
                                    reg.TIPO_FLUJO = re[19].ToString();
                                    reg.TIPO_MOV = re[20].ToString();
                                    reg.PAIS = re[21].ToString();
                                    reg.IDENTIFICADOR = re[22].ToString();
                                    reg.SUB_IDENTIFICADOR = re[23].ToString();
                                    reg.CODIGO = re[24].ToString();
                                    reg.BARCODE = re.GetDecimal(25);
                                    reg.GRADO_API = re[26].ToString();
                                    reg.UTEMP = re[27].ToString();
                                    reg.TEMPERATURA = re.GetDecimal(28);
                                    reg.PRODUCTO = re[29] + " - " + re[30];
                                    reg.UMEDIDA = re[30].ToString();
                                    reg.CANTIDAD = re.GetDecimal(31);
                                    reg.TIPO_MEDICION = re[32].ToString();
                                    reg.CANTIDAD_ADICIONAL = re.GetDecimal(33);
                                    reg.ID_CUENTA = re.GetDecimal(34);
                                    reg.ID_FUENTE_ORIGEN = re.GetDecimal(35);
                                    reg.APP_ID_USUARIO = re.GetDecimal(36);
                                    reg.APP_FECHA_REGISTRO = re[37].ToString();
                                    reg.DOC_FACT_PR = re[38].ToString();
                                    reg.DOC_FACT_EX = re[39].ToString();
                                    reg.DOC_NMOV = re[40].ToString();
                                    reg.AUT_SCTRL = re[41].ToString();
                                    reg.DOC_SVCADIC = re[42].ToString();
                                    reg.DOC_PRECINTO = re[43].ToString();
                                    reg.DOC_CALIDAD = re[44].ToString();
                                    reg.DOC_PVUELO = re[45].ToString();
                                    reg.DOC_CETREC = re[46].ToString();
                                    reg.DOC_LOTE = re[47].ToString();
                                    reg.DOC_VALE = re[48].ToString();
                                    reg.DOC_FACREF = re[49].ToString();
                                    reg.DOC_DESP = re[50].ToString();
                                    reg.AUT_ANHFA = re[51].ToString();
                                    reg.DOC_MED = re[52].ToString();
                                    reg.DOC_CTA = re[53].ToString();
                                    reg.DOC_PRTSAL = re[54].ToString();
                                    reg.ID_CITE = re.GetDecimal(55);
                                    reg.CITE = re[56].ToString();

                                    var arrAux = reg.DOC_FACT_PR.Split('|');
                                    if (arrAux.Count() == 8)
                                    {
                                        reg.FP_NIT = Convert.ToDecimal(arrAux[0]);
                                        reg.FP_RS = arrAux[1];
                                        reg.FP_FP = arrAux[2];
                                        reg.FP_NRO = Convert.ToDecimal(arrAux[3]);
                                        reg.FP_NAUT = Convert.ToDecimal(arrAux[4]);
                                        reg.FP_CCTRL = arrAux[5];
                                        reg.FP_PRECIO = Convert.ToDecimal(arrAux[6].Replace(",", "."));
                                        reg.FP_IMPORTE = Convert.ToDecimal(arrAux[7].Replace(",", "."));
                                    }

                                    arrAux = reg.DOC_FACT_EX.Split('|');
                                    if (arrAux.Count() == 8)
                                    {
                                        reg.FE_NIT = Convert.ToDecimal(arrAux[0]);
                                        reg.FE_RS = arrAux[1];
                                        reg.FE_FP = arrAux[2];
                                        reg.FE_NRO = Convert.ToDecimal(arrAux[3]);
                                        reg.FE_NAUT = Convert.ToDecimal(arrAux[4]);
                                        reg.FE_CCTRL = arrAux[5];
                                        reg.FE_PRECIO = Convert.ToDecimal(arrAux[6].Replace(",", "."));
                                        reg.FE_IMPORTE = Convert.ToDecimal(arrAux[7].Replace(",", "."));
                                    }

                                    arrAux = reg.DOC_SVCADIC.Split('|');
                                    if (arrAux.Count() == 4)
                                    {
                                        reg.SA_TIPO = arrAux[0];
                                        reg.FE_NRO = Convert.ToDecimal(arrAux[1]);
                                        reg.FE_PRECIO = Convert.ToDecimal(arrAux[2].Replace(",", "."));
                                        reg.FE_IMPORTE = Convert.ToDecimal(arrAux[3].Replace(",", "."));
                                    }

                                    arrAux = reg.DOC_FACREF.Split('|');
                                    if (arrAux.Count() == 8)
                                    {
                                        reg.FR_NIT = Convert.ToDecimal(arrAux[0]);
                                        reg.FR_RS = arrAux[1];
                                        reg.FR_FP = arrAux[2];
                                        reg.FR_NRO = Convert.ToDecimal(arrAux[3]);
                                        reg.FR_NAUT = Convert.ToDecimal(arrAux[4]);
                                        reg.FR_CCTRL = arrAux[5];
                                        reg.FR_PRECIO = Convert.ToDecimal(arrAux[6].Replace(",", "."));
                                        reg.FR_IMPORTE = Convert.ToDecimal(arrAux[7].Replace(",", "."));
                                    }

                                    reg.DEC_GESTION = reg.FECHA_OPERACION.Year;
                                    reg.STR_PERIODO = reg.FECHA_OPERACION.Month.ToString().PadLeft(2, '0') + "/" + reg.FECHA_OPERACION.Year;
                                    reg.STR_MES = reg.FECHA_OPERACION.Month.ToString().PadLeft(2, '0') + " - " + dtinfo.GetMonthName(reg.FECHA_OPERACION.Month).ToUpper();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteMovRecaudoExterno(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_MOV_REPORTE_REC_EXTERNO_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_MOV_REPORTE_RECAUDO_EXTERNO";
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

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_MOV_REPORTE_REC_EXTERNO_CTY();

                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.ID_TIPO_ACTIVIDAD = re.GetDecimal(1);
                                    reg.ID_TIPO_OPERACION = re.GetDecimal(2);
                                    reg.ID_CONSUMIDOR = re.GetDecimal(3);
                                    reg.ID_INTEGRACION = re.GetDecimal(4);
                                    reg.ID_TRANSACCION = re[5].ToString();
                                    reg.DEPTO = re[6].ToString();
                                    reg.PROYECTO = re[7].ToString();
                                    reg.FECHA_OPERACION = CFechas.ConvierteLongDateTime(re.GetInt64(8));

                                    reg.ESTADO = re[9].ToString();
                                    reg.TIPO_FLUJO = re[10].ToString();
                                    reg.TIPO_MOV = re[11].ToString();
                                    reg.PAIS = re[12].ToString();
                                    reg.IDENTIFICADOR = re[13].ToString();
                                    reg.SUB_IDENTIFICADOR = re[14].ToString();
                                    reg.CODIGO = re[15].ToString();
                                    reg.BARCODE = re.GetDecimal(16);

                                    reg.PRODUCTO = re[17].ToString();
                                    reg.UMEDIDA = re[18].ToString();
                                    reg.CANTIDAD = re.GetDecimal(19);

                                    reg.ID_FUENTE_ORIGEN = re.GetDecimal(20);
                                    reg.APP_ID_USUARIO = re.GetDecimal(21);
                                    reg.APP_FECHA_REGISTRO = re[22].ToString();
                                    reg.DOC_FACT_PR = re[23].ToString();

                                    reg.DOC_SVCADIC = re[24].ToString();

                                    reg.ID_CITE = re.GetDecimal(25);
                                    reg.CITE = re[26].ToString();

                                    var arrAux = reg.DOC_FACT_PR.Split('|');
                                    if (arrAux.Count() == 8)
                                    {
                                        reg.FP_NIT = Convert.ToDecimal(arrAux[0]);
                                        reg.FP_RS = arrAux[1];
                                        reg.FP_FP = arrAux[2];
                                        reg.FP_NRO = Convert.ToDecimal(arrAux[3]);
                                        reg.FP_NAUT = Convert.ToDecimal(arrAux[4]);
                                        reg.FP_CCTRL = arrAux[5];
                                        reg.FP_PRECIO = Convert.ToDecimal(arrAux[6].Replace(",", "."));
                                        reg.FP_IMPORTE = Convert.ToDecimal(arrAux[7].Replace(",", "."));
                                    }

                                    arrAux = reg.DOC_SVCADIC.Split('|');
                                    if (arrAux.Count() == 4)
                                    {
                                        reg.SA_TIPO = arrAux[0];
                                        reg.SA_NRO = Convert.ToDecimal(arrAux[1]);
                                        reg.SA_PRECIO = Convert.ToDecimal(arrAux[2].Replace(",", "."));
                                        reg.SA_IMPORTE = Convert.ToDecimal(arrAux[3].Replace(",", "."));
                                    }

                                    reg.DEC_GESTION = reg.FECHA_OPERACION.Year;
                                    reg.STR_PERIODO = reg.FECHA_OPERACION.Month.ToString().PadLeft(2, '0') + "/" + reg.FECHA_OPERACION.Year;
                                    reg.STR_MES = reg.FECHA_OPERACION.Month.ToString().PadLeft(2, '0') + " - " + dtinfo.GetMonthName(reg.FECHA_OPERACION.Month).ToUpper();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteConsumoMtr(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_CONSUMO_GRAL_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_CONSUMO_GRAL";
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
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, ob.strParametro6,
                           ParameterDirection.Input));  
                        var op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            var decContador = 0;
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_CONSUMO_GRAL_CTY();

                                    reg.ID = decContador++;
                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                    reg.DENOMINACION = re[1].ToString();
                                    reg.NIT_EMISOR = re.GetDecimal(2);
                                    reg.ID_DEPARTAMENTO = re.GetDecimal(3);
                                    reg.DEPARTAMENTO = re[4].ToString();
                                    reg.NIT_CONSUMIDOR = re.GetDecimal(5);
                                    reg.RAZON_SOCIAL = re[6].ToString();
                                    reg.ID_HIDROCARBURO = re.GetDecimal(7);
                                    reg.PRODUCTO = re[8].ToString();
                                    reg.CANTIDAD = re.GetDecimal(9);
                                    reg.PRECIO_UNITARIO = re.GetDecimal(10);
                                    reg.PRECIO_TOTAL = re.GetDecimal(11);
                                    reg.FECHA_OPERACION = re.GetDecimal(12);
                                    reg.NRO_TRANSACCION = re[13].ToString();
                                    reg.ID_TIPO_TRANSACCION = re[14].ToString();
                                    reg.TIPO_DOC = re[15].ToString();
                                    reg.DAT_FECHA = CFechas.ConvierteLongDateTime(re.GetInt64(12));                                   
                                    reg.FECHA_DJ = Convert.ToDateTime(re[16]);
                                    reg.CITE_GENERADO = re[17].ToString();
                                    reg.TIPO_CONSUMIDOR = re[18].ToString();
                                    reg.IDENTIFICADOR = re[19].ToString();
                                    reg.UMEDIDA = re[20].ToString();
                                    reg.MUNICIPIO = re[21].ToString();
                                    reg.USUARIO_TRANSACCION = re[24].ToString();
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

        public EPResultado Consolidacion_Siuc(EObjetoEnvio5ParametrosProto ob)
        {
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_CONSOLIDACION_SUIC";
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

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            var decContador = 0;

                            _resultado.oResultado = null;

                            if (ob.decFiltro == 1)
                            {
                                var objDatos = new List<O_CONSOLIDACION_SUIC_ACU_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_CONSOLIDACION_SUIC_ACU_CTY();

                                        reg.ID = decContador++;
                                        reg.ID_PRODUCTO = re.GetDecimal(0);
                                        reg.PRODUCTO = re[1].ToString() + " - " + re[3].ToString();
                                        reg.ID_UNIDAD_MEDIDA = re.GetDecimal(2);
                                        reg.UNIDAD_MEDIDA = re[3].ToString();
                                        reg.MES = re[4].ToString();
                                        reg.TOT_VOLUMEN = re.GetDecimal(5);
                                        reg.CANT_FACT = re.GetDecimal(6);
                                        reg.TOT_IMPORTE = re.GetDecimal(7);
                                        reg.STR_MES = reg.MES.Substring(4, 2) + "/" + reg.MES.Substring(0, 4);
                                        objDatos.Add(reg);
                                    }
                                }
                                if (objDatos.Any())
                                    _resultado.oResultado = objDatos;
                            }
                            else
                            {
                                var objDatos = new List<O_CONSOLIDACION_SUIC_DET_CTY>();
                                while (re.Read())
                                {
                                    if (re.HasRows)
                                    {
                                        var reg = new O_CONSOLIDACION_SUIC_DET_CTY();

                                        reg.ID = decContador++;
                                        reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                        reg.FECHA_OPERACION = re.GetDecimal(1);
                                        reg.ID_INTEGRACION = re.GetDecimal(2);
                                        reg.IDENTIFICADOR = re[3].ToString();
                                        reg.ID_PRODUCTO = re.GetDecimal(4);
                                        reg.PRODUCTO = re[5].ToString();
                                        reg.ID_UNIDAD_MEDIDA = re.GetDecimal(6);
                                        reg.UNIDAD_MEDIDA = re[7].ToString();
                                        reg.FACT_NEM = re.GetDecimal(8);
                                        reg.FACT_NIT = re.GetDecimal(9);
                                        reg.FACT_RSO = re[10].ToString();
                                        reg.FACT_NAU = re.GetDecimal(11);
                                        reg.FACT_NRO = re[12].ToString();
                                        reg.FACT_CCT = re[13].ToString();
                                        reg.CANTIDAD = re.GetDecimal(14);
                                        reg.FACT_PRECIO = re.GetDecimal(15);
                                        reg.FACT_IMPORTE = re.GetDecimal(16);
                                        reg.SUIC_NSR = re[17].ToString();
                                        reg.SUIC_NDC = re.GetDecimal(18);
                                        reg.SIUC_PRU = re.GetDecimal(19);
                                        reg.SIUC_IMP = re.GetDecimal(20);
                                        var datFecha = CFechas.ConvierteLongDateTime(reg.FECHA_OPERACION);
                                        reg.STR_FECHA = datFecha.ToShortDateString() + " " + datFecha.ToShortTimeString();

                                        objDatos.Add(reg);
                                    }
                                }
                                if (objDatos.Any())
                                    _resultado.oResultado = objDatos;
                            }

                            re.Close();
                            op0.Dispose();
                        }

                        if (_resultado.oResultado != null)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK";
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros.";
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

        public EPResultado ReporteSuicDdjj(EObjetoEnvio9ParametrosProto ob)
        {
            var objDatos = new List<O_LISTA_REPORTE_SUIC_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_REPORTE_SUIC_ANUAL";
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
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, ob.decParametro4,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LISTA_REPORTE_SUIC_CTY();

                                    reg.ID_REGISTRO = re.GetDecimal(0);
                                    reg.ID_REPORTE_DATO_PRINC = re.GetDecimal(1);
                                    reg.DEPARTAMENTO = re[2].ToString();
                                    reg.PROYECTO = re[3].ToString();
                                    reg.PERIODO_DECLARADO = re[4].ToString();
                                    reg.TIPO_DECLARACION = re[5].ToString();
                                    reg.CANT_MOV = re.GetDecimal(6);
                                    reg.CITE_GENERADO = re[7].ToString();
                                    reg.ESTADO_DECLARACION = re[8].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        //glp
        public EPResultado ObtenerDatoPersonaHydro(string ob)
        {
            var objDatos = new List<O_OBTIENE_DATO_PERSONA_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "ANH_HYD.PHYD_CONSULTAS.P_OBTIENE_PERSONA_CI";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("2", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTIENE_DATO_PERSONA_CTY();
                                    reg.ID_PERSONA = re.GetDecimal(0);
                                    reg.NOMBRE_COMPLETO = re[1].ToString();
                                    reg.NOMBRE = re[2].ToString();
                                    reg.APELLIDO_1 = re[3].ToString();
                                    reg.APELLIDO_2 = re[4].ToString();
                                    reg.FECHA_NACIMIENTO = re[5].ToString();
                                    reg.CI = re[6].ToString();
                                    reg.COMPLEMENTO = re[7].ToString();
                                    reg.EXPEDICION_ID = re.GetDecimal(8);
                                    reg.EXPEDICION = re[9].ToString();
                                    reg.GENERO_ID = re.GetDecimal(10);
                                    reg.GENERO = re[11].ToString();
                                    reg.EMAIL = re[12].ToString();
                                    reg.ID_TIPO_IDENTIFICACION = re.GetDecimal(13);
                                    reg.TIPO_IDENT_DESCRIPCION = re[14].ToString();

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

        //reportes  importadores

        public EPResultado ReporteImportacionInsumo(EObjetoEnvio10ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_IMPORTACION_INSUMO_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_IMPORTACION_INSUMO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_IMPORTACION_INSUMO_CTY();

                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.FECHA = re[1].ToString();
                                    reg.EMPRESA = re[2].ToString();
                                    reg.OBSERVACIONES = re[3].ToString();
                                    reg.CANTIDAD = re.GetDecimal(4);
                                    reg.RESOLUCION = re[5].ToString();
                                    reg.PRODUCTO = re[6].ToString();
                                    reg.MEDIDA = re[7].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteImortacionEntidad(EObjetoEnvio10ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_IMPORTACION_ENTIDAD_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_IMPORTACION_ENTIDAD";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_IMPORTACION_ENTIDAD_CTY();

                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.RESOLUCION = re[1].ToString();
                                    reg.EMPRESA = re[2].ToString();
                                    reg.MES = re[3].ToString();
                                    reg.GESTION = re[4].ToString();
                                    reg.ACTIVIDAD = re[5].ToString();
                                    reg.CLASIFICACION = re[6].ToString();
                                    reg.MEDIDA = re[8].ToString();

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteMovimientoSaldo(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_MOV_SALDO_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_MOVIMIENTO_SALDO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, ob.strParametro2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, ob.strParametro3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, ob.strParametro4,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("6", OracleDbType.Int32, ob.strParametro5,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_MOV_SALDO_CTY();

                                    reg.ID_DEPARTAMENTO = re.GetDecimal(0);
                                    reg.DEPARTAMENTO = re[1].ToString();
                                    reg.ID_ENTIDAD = re.GetDecimal(2);
                                    reg.ENTIDAD = re[3].ToString();
                                    reg.FECHA_OPERACION_DIA = ob.strParametro1.Substring(6, 2) + "/" + ob.strParametro1.Substring(4, 2) + "/" + ob.strParametro1.Substring(0, 4);
                                    reg.ID_PRODUCTO = re.GetDecimal(4);
                                    reg.PRODUCTO = re[5].ToString();
                                    reg.CODIGO = re[6].ToString();
                                    reg.SALDOI = re.GetDecimal(7);
                                    reg.RECEPCION = re.GetDecimal(8);
                                    reg.DEMASIA = re.GetDecimal(9);
                                    reg.VENTA = re.GetDecimal(10);
                                    reg.MERMA = re.GetDecimal(11);
                                    reg.SALDOF = re.GetDecimal(12);

                                    if (re.GetDecimal(12) < 0)
                                    {
                                        reg.ESTADO = "ERROR";
                                    }
                                    else if (re.GetDecimal(12) >= 0 && re.GetDecimal(12) < 2000)
                                    {
                                        reg.ESTADO = "SALDO BAJO";
                                    }
                                    else if (re.GetDecimal(12) >= 2000 && re.GetDecimal(12) < 5000)
                                    {
                                        reg.ESTADO = "SALDO MEDIO";
                                    }
                                    else if (re.GetDecimal(12) >= 5000)
                                    {
                                        reg.ESTADO = "SALDO ALTO";
                                    }
                                    else
                                    {
                                        reg.ESTADO = "SIN DATO";
                                    }

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteEntDepAct(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_ENT_ACTDEP_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_MOVIMIENTO_SALDO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, ob.strParametro2,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, ob.strParametro3,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("5", OracleDbType.Int32, ob.strParametro4,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("6", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_ENT_ACTDEP_CTY();

                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                    reg.DENOMINACION = re[1].ToString();
                                    reg.ID_PRODUCTO = re.GetDecimal(2);
                                    reg.PRODUCTO = re[3].ToString();
                                    reg.CANTIDAD = re.GetDecimal(4);
                                    reg.RECEPCION = re.GetDecimal(5);
                                    reg.DEMASIA = re.GetDecimal(6);
                                    reg.VENTA = re.GetDecimal(7);
                                    reg.MERMA = re.GetDecimal(8);

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ReporteMovimientoConsumoUsuario(EObjetoEnvio1ParametrosProto ob)
        {
            var objDatos = new List<O_REPORTE_MOV_CONSUMO_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_CONSUMO_USUARIO";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.strParametro1,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("3", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_REPORTE_MOV_CONSUMO_CTY();

                                    reg.ID_MOVIMIENTO = re.GetDecimal(0);
                                    reg.FECHA_OPERACION = CFechas.ConvierteLongDateTime(re.GetDecimal(1));
                                    reg.FECHA_OPERACION_DIA = CFechas.ConvierteLongDateTime(re.GetDecimal(2));
                                    reg.PRECIO_UNITARIO = re.GetDecimal(3);
                                    reg.CANTIDAD = re.GetDecimal(4);
                                    reg.PRECIO_TOTAL = re.GetDecimal(5);

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerTechoProde(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_OBTENER_TECHO_PRODE_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_TECHO_PRODE";
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

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_OBTENER_TECHO_PRODE_CTY();

                                    reg.ID_TIPO_OPERACION = re.GetDecimal(0);
                                    reg.NOMBRE_OPERACION = re[1].ToString();
                                    reg.ID_MOVIMIENTO = re.GetDecimal(2);
                                    reg.VOLUMEN = re.GetDecimal(3);
                                    reg.ID_HIDROCARBURO = re.GetDecimal(4);

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerVolumenProgramadoProde(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new List<O_VOLUMEN_PRODE_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_PRODE_REGISTRADO";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        #region lista acumulada de movimientos
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_VOLUMEN_PRODE_CTY();

                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                    reg.ESTACION_SERVICIO = re[1].ToString();
                                    reg.COD_HYDRO_PROYECTO = re[2].ToString();
                                    reg.ID_HIDROCARBURO = re.GetDecimal(3);
                                    reg.ID_DEPARTAMENTO = re.GetDecimal(4);
                                    reg.DEPARTAMENTO = re[5].ToString();
                                    reg.SEMANA = re[6].ToString();
                                    reg.RECEPCION = re[7].ToString() == "" ? 0 : Convert.ToDecimal(re[7].ToString());
                                    reg.DEMASIA = re[8].ToString() == "" ? 0 : Convert.ToDecimal(re[8].ToString());
                                    reg.VENTA = re[9].ToString() == "" ? 0 : Convert.ToDecimal(re[9].ToString());
                                    reg.MERMA = re[10].ToString() == "" ? 0 : Convert.ToDecimal(re[10].ToString());
                                    reg.PRODE = re[11].ToString() == "" ? 0 : Convert.ToDecimal(re[11].ToString());

                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        #endregion

                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerIncidentes(EObjetoEnvioIncidentes ob)
        {
            var objDatos = new List<O_INCIDENTES_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_INCIDENTES";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decIdActividad,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.decIdTipoIncidente,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decIdPeridod,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.decIdGestion,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.deciIdOperador,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_INCIDENTES_CTY();

                                    reg.ID_INCIDENTES = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_ACTIVIDAD = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.TIPO_ACTIVIDAD = re[2].ToString();
                                    reg.FECHA_REGISTRO = re.GetDateTime(3);
                                    reg.ID_TIPO_INCIDENTE = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.TIPO_INCIDENTE = re[5].ToString();
                                    reg.ID_AREA_UNIDAD = (re[6].ToString() == "") ? 0 : Convert.ToDecimal(re[6].ToString());
                                    reg.AREA_UYNIDAD = re[7].ToString();
                                    reg.DESCRIPCION = re[8].ToString();
                                    reg.FECHA_INICIO_SUCESO = re.GetDateTime(9);
                                    reg.FECHA_FIN_SUCESO = re.GetDateTime(10);
                                    reg.FECHA_REINCIO = re.GetDateTime(11);
                                    reg.PERIODO = (re[12].ToString() == "") ? 0 : Convert.ToDecimal(re[12].ToString());
                                    reg.APP_ID_USUARIO = (re[13].ToString() == "") ? 0 : Convert.ToDecimal(re[13].ToString());
                                    reg.GESTION = (re[14].ToString() == "") ? 0 : Convert.ToDecimal(re[13].ToString());
                                    reg.PEROP_GESTION = re[15].ToString();
                                    reg.CLASIFICADOR = re[16].ToString();
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado ObtenerListTipoIncidente(string strgCredencial, string decIdParParamDomi)
        {
            var objDatos = new List<O_TIP_INCIDENTE_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_TIP_INCIDENT";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, decIdParParamDomi,
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
                                    var reg = new O_TIP_INCIDENTE_CTY();

                                    reg.ID_PARAMETRICA_GLOBAL = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.CODIGO = re[1].ToString();
                                    reg.DESCRIPCION = re[2].ToString();
                                    reg.ORDEN = (re[3].ToString() == "") ? 0 : Convert.ToDecimal(re[3].ToString());
                                    reg.TIPO_INCIDENTE = re[4].ToString();
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado listaUnidadesAreaRefineria(string strgCredencia, string strgCodRefineria, string strgFiltroTipoConsulta)
        {
            var objDatos = new List<O_UNIDADES_REF_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_UNIDAR_REF";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencia,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Varchar2, strgCodRefineria,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, strgFiltroTipoConsulta,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("4", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_UNIDADES_REF_CTY();

                                    reg.ID_PARAMETRICA_GLOBAL = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.NOM_AREA_REFIN = re[1].ToString();
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado ObtenerRendimientos(string strgCredencia, decimal decIdOperador, decimal decPerido, decimal decGestion)
        {
            var objDatos = new List<O_RENDIMIENTO_VOL_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_RENDIMIENTO_VOL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, strgCredencia,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, decIdOperador,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Int32, decPerido,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Int32, decGestion,
                            ParameterDirection.Input));
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_RENDIMIENTO_VOL_CTY();

                                    reg.ID_RENDIMIEN_VOLUMET = (re[0].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.ID_OPERADOR = (re[1].ToString() == "") ? 0 : Convert.ToDecimal(re[0].ToString());
                                    reg.FECHA_REGISTRO = Convert.ToDateTime(re[2].ToString());
                                    reg.JSON_REGIS_MENSUALES = re[3].ToString();
                                    reg.APP_USUARIO = (re[4].ToString() == "") ? 0 : Convert.ToDecimal(re[4].ToString());
                                    reg.PERIODO = (re[5].ToString() == "") ? 0 : Convert.ToDecimal(re[5].ToString());
                                    reg.GESTION = (re[6].ToString() == "") ? 0 : Convert.ToDecimal(re[6].ToString());
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado ObtenerDjCDirecto(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_OBTENER_DJ_CDIRECTO>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_DJ_CDIRECTO";
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
                                    var reg = new O_OBTENER_DJ_CDIRECTO();                                    
                                    reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                    reg.FECHA_DJ = Convert.ToDateTime(re[1]);
                                    reg.CITE_GENERADO = re[2].ToString();
                                    reg.TIPO_CONSUMIDOR = re[3].ToString();                                    
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado obtieneEessBsa(EObjetoEnvio2ParametrosProto ob)
        {
            var objDatos = new List<O_LISTA_ESS_BSA_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTA_EESS_BSA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Varchar2, ob.strCredencial,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("2", OracleDbType.Int32, ob.decFiltro,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("3", OracleDbType.Varchar2, ob.strParametro1,
                            ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("4", OracleDbType.Varchar2, ob.strParametro2,
                           ParameterDirection.Input));  
                        var op0 = new OracleParameter("5", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LISTA_ESS_BSA_CTY();
                                    
                                    reg.ID_EESS = re.GetDecimal(0);
                                    reg.DEPTO = re[1].ToString();
                                    reg.DENOMINACION = re[2].ToString();
                                    reg.ID_CONSUMIDOR = re.GetDecimal(3);
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerSaldo(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new List<O_SALDO_FINAL_DIA_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_SALDO_FINAL_DIA";
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

                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();

                        using (OracleDataReader re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_SALDO_FINAL_DIA_CTY();

                                    reg.ID_PRODUCTO = re.GetDecimal(0);
                                    reg.ID_CONSUMIDOR = re.GetDecimal(1);
                                    reg.SALDO_INI = re.GetDecimal(2);
                                    reg.AC_MOV = re.GetDecimal(3);
                                    reg.SALDO_FIN = re.GetDecimal(4);
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }
                        if (objDatos.Count > 0)
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
        public EPResultado ObtenerVolProgramadoPDE(EObjetoEnvio3ParametrosProto ob)
        {
            var objDatos = new List<O_PRODUCTO_PDE_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_PRODUCTO_PDE";
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
                        var op0 = new OracleParameter("6", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_PRODUCTO_PDE_CTY();

                                    reg.PERIODO = re.GetDecimal(0);
                                    reg.IDPRODUCTO = re.GetDecimal(1);
                                    reg.PRODUCTO = re[2].ToString();
                                    reg.PROGRAMADO = re.GetDouble(3);
                                    reg.DESPACHO = re.GetDouble(4);
                                    reg.SALDO = re.GetDouble(5);
                                    reg.IDCONSUMIDOR = re.GetDecimal(6);
                                    reg.DEPARTAMENTO = re[7].ToString();
                                    reg.LOCALIDAD = re[8].ToString();
                                    reg.DENOMINACION = re[9].ToString();
                                    reg.ACTIVIDAD = re[10].ToString();
                                    reg.LICENCIA = re[11].ToString();
                                    reg.VIGENCIA = re[12].ToString();
                                    reg.VENCIMIENTO = re[13].ToString();
                                    reg.CODIGO = re[14].ToString();
                                    reg.DESCRIPCION = re[15].ToString();
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }              

                        if (objDatos.Count > 0)
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

        public EPResultado ObtenerDocAutorizadoPDE(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_DOCUMENTO_PDE_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_DOCUMENTO_PDE";
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
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_DOCUMENTO_PDE_CTY();
                                    reg.decIdDocumento = re.GetDecimal(0);
                                    reg.strFechaEmision = re[1].ToString();
                                    reg.strTipoDocumento = re[2].ToString();
                                    reg.strNroDocumento = re[3].ToString();
                                    reg.decNit = re.GetDecimal(4);
                                    reg.strNroAutorizacion = re[5].ToString();
                                    reg.strCodigoControl = re[6].ToString();
                                    reg.strChofer = re[7].ToString();
                                    reg.strIdentificador = re[8].ToString();
                                    reg.strPrecintos = re[9].ToString();
                                    reg.decCantidad = re.GetDecimal(10);
                                    reg.strTipoDocDgsc = re[11].ToString();
                                    reg.strFechaVigencia = re[12].ToString();
                                    reg.decPrecio = re.GetDecimal(13);
                                    reg.decImporte = re.GetDecimal(14);
                                    reg.strEstado = re[15].ToString();
                                    reg.strUsuario = re[16].ToString();
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (objDatos.Count > 0)
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
        public EPResultado ObtenerIdProdePDE(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new O_PROGRAMACION_PDE_CTY();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_PROGRAMACION_PDE";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    objDatos.decIdProde = re.GetDecimal(0);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (objDatos != null)
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
        public EPResultado ReporteVentaXHora(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new List<O_VENTA_HORA_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_VENTA_HORA";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {                          
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_VENTA_HORA_CTY();

                                    reg.ANIO = re[0].ToString();
                                    reg.MES = re[1].ToString();
                                    reg.DIA = re[2].ToString();
                                    reg.HORA = re[3].ToString();
                                    reg.DEPARTAMENTO = re[4].ToString();
                                    reg.DENOMINACION = re[5].ToString();
                                    reg.PRODUCTO = re[6].ToString();
                                    reg.IDENTIFICADOR = re[7].ToString();
                                    reg.SERVICIO = re[8].ToString();
                                    reg.CLASE = re[9].ToString();
                                    reg.CANTIDAD = re.GetDecimal(10);
                                    reg.TRANSACCIONES = re.GetDecimal(11);                                   
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
        public EPResultado ObtenerLecturaRFID(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_LECTURA_RFID_CTY>();

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_OBTENER_LECTURAS_RFID";
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
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_LECTURA_RFID_CTY();
                                    
                                    reg.ID_LECTURA_RFID = re.GetDecimal(0);
                                    reg.DEPTO = re[1].ToString();
                                    reg.PLANTA = re[2].ToString();
                                    reg.BARCODE = re[3].ToString();
                                    reg.FECHA_LECTURA = CFechas.ConvierteLongDateTime(re.GetDecimal(4));
                                    reg.IDENTIFICADOR = re[5].ToString();
                                    reg.CLASE = re[6].ToString();
                                    var cc = re[7].ToString();                           
                                    //reg.IMAGEN = (cc.Length > 0) ? (byte[])re[7] : Convert.FromBase64String("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");                                   
                                    byte[] imgAux = (cc.Length > 0) ? (byte[])re[7] : Convert.FromBase64String("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");
                                    reg.IMAGEN = ObtenerImagen(imgAux, cc, re[3].ToString(), re[5].ToString(), re[6].ToString(), re[11].ToString());
                                    reg.NRO_COMPARTIMIENTO = re.GetDecimal(8);
                                    reg.VOLUMEN_NOMINAL = re.GetDecimal(9);
                                    reg.VOLUMEN_NOMINAL_COMPTO = re[10].ToString();
                                    reg.MARCA = re[11].ToString();
                                    reg.CODIGO_ANTENA = re[12].ToString();  
                                    objDatos.Add(reg);
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (objDatos.Count > 0)
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

        private byte[] ObtenerImagen(byte[] inArray, string strAux, string strBarcode, string strIdentificador,string strClase,string strMarca)
        {
            List<Atributos>  list5 = new List<Atributos>();
            if (strAux.Length > 0 || (strBarcode != "" && strIdentificador!=""))
            {
                Atributos atributos5 = new Atributos
                {
                    Titulo = "PLACA",
                    Descripcion = strIdentificador
                };
                list5.Add(atributos5);
                Atributos atributos6 = new Atributos
                {
                    Titulo = "BARCODE",
                    Descripcion = strBarcode
                };
                list5.Add(atributos6);
                //Atributos atributos7 = new Atributos
                //{
                //    Titulo = "MODELO",
                //    Descripcion = resultado.modelo
                //};
                //list5.Add(atributos7);
                Atributos atributos8 = new Atributos
                {
                    Titulo = "MARCA",
                    Descripcion = strMarca
                };
                list5.Add(atributos8);             
            }
            else
            {
                Atributos atributos9 = new Atributos
                {
                    Titulo = "BARCODE",
                    Descripcion = "?"
                };
                list5.Add(atributos9);
                Atributos atributos10 = new Atributos
                {
                    Titulo = "PLACA",
                    Descripcion = "?"
                };
                list5.Add(atributos10);
                Atributos atributos11 = new Atributos
                {
                    Titulo = "MARCA",
                    Descripcion = "?"
                };
                list5.Add(atributos11);
            }
            Fotografia fotografia = new Fotografia
            {
                Imagen = Convert.ToBase64String(inArray),
                FechaHora = DateTime.Now.ToString(),
                TamañoTextoFechaHora = 25,
                FuenteImagen = FuenteImagen.GenerarImagenAnh,
                TipoDeteccion = Deteccion.Manual,
                TamañoImagen = 100,
                TamañoTextoContenido = 28,
                ImagenBase = DTICUIDPPI001.Imagen.Base64AImagen(Fotografia.TipoImagen(FormatoImagen.ImagenBaseVerde)),
                ColorEtiqueta = Brushes.OrangeRed,
                ColorEtiquetaDescripcion = Brushes.Black,
                ColorTitulo = Brushes.DarkSlateGray,
                ColorAlerta = Brushes.LawnGreen,
                TextoTitulo = strClase
            };
            Organizacion organizacion = new Organizacion
            {
                MarcaAgua = 30,
                Orientacion = Orientacion.Vertical,
                Calidad = 100
            };

            return (byte[])new ImageConverter().ConvertTo(DTICUIDPPI001.Imagen.Base64AImagen(DTICUIDPPI001.Imagen.ProcesarImagen(fotografia, list5, organizacion)), typeof(byte[]));
        }
        public EPResultado ListadoDdjjGrafico(EObjetoEnvio5ParametrosProto ob)
        {
            var objDatos = new List<O_LISTADO_DDJJ_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_LISTADO_DDJJ";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();
                        var opciones = ob.strParametro5.Split('|');
                        //string strParametros = ob.strParametro1 +"; "+ ob.strParametro2 +"; "+ ob.strParametro3 +"; "+ ob.strParametro4;
                        string strJsonAux = "";
                        switch (Convert.ToInt16(opciones[0]))
                        {                           
                            case 5:
                                #region lista acumulada de movimientos
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    decimal decConsumidor = Convert.ToInt32(opciones[1]);
                                    decimal decEntidad = decConsumidor;
                                    while (re.Read())
                                    {

                                        if (!re.HasRows) continue;
                                        var reg = new O_LISTADO_DDJJ_CTY();
                                        reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                        if (reg.ID_CONSUMIDOR == decConsumidor)
                                        {
                                            var intAux = 0;
                                            
                                            var reg2 = new O_LISTADO_DDJJ_CTY();
                                            var reg3 = new O_LISTADO_DDJJ_CTY();
                                            O_LISTADO_DDJJ_CTY reg1;
                                            strJsonAux = re[7].ToString();                                         
                                            
                                            var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonAux);
                                            var lstAcumulado = objDdjj.LstAcumulado;

                                            var strValFec = "";
                                            decimal total = 0;
                                            if (lstAcumulado != null)
                                            {
                                                foreach (var fila in lstAcumulado)
                                                {
                                                    //if (fila.StrCodHidrocarburo.ToString() == "GE" || fila.StrCodHidrocarburo.ToString() == "GE+")
                                                    if (fila.StrCodHidrocarburo.ToString() != "DO")
                                                    {
                                                        total = total + fila.DecTotalEgreso;
                                                    }                                                   
                                                }
                                            }
                                            
                                            reg.PROYECTO = re[1].ToString();
                                            var strMesGestion = re[6].ToString().Split('-');
                                            reg.strMes = obtenerMes(strMesGestion[0].Trim()).ToString() +"-"+ strMesGestion[0].Trim();                                            
                                            reg.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                            reg.strProducto = lstAcumulado[0].StrCodHidrocarburo;
                                            reg.decCantidad = Math.Round(total/1000,0);                                        
                                            //if ( (reg.strProducto == "GE" || reg.strProducto == "GE+"))
                                            if (reg.strProducto != "DO")
                                            {
                                                objDatos.Add(reg);
                                            }


                                        }
                                        
                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                            case 6:
                                #region lista acumulada de movimientos
                                using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                                {
                                    decimal decConsumidor = Convert.ToInt32(opciones[1]);
                                    decimal decEntidad = decConsumidor;
                                    while (re.Read())
                                    {

                                        if (!re.HasRows) continue;
                                        var reg = new O_LISTADO_DDJJ_CTY();
                                        reg.ID_CONSUMIDOR = re.GetDecimal(0);
                                        if (reg.ID_CONSUMIDOR == decConsumidor)
                                        {
                                            var intAux = 0;
                                            
                                            var reg2 = new O_LISTADO_DDJJ_CTY();
                                            var reg3 = new O_LISTADO_DDJJ_CTY();
                                            O_LISTADO_DDJJ_CTY reg1;
                                            strJsonAux = re[7].ToString();
                                            var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonAux);
                                            var lstAcumulado = objDdjj.LstAcumulado;

                                            var strValFec = "";
                                            decimal total = 0;
                                            if (lstAcumulado != null)
                                            {
                                                foreach (var fila in lstAcumulado)
                                                {
                                                    if (fila.StrCodHidrocarburo.ToString() == "DO")
                                                    {
                                                        total = total + fila.DecTotalEgreso;
                                                    }                                                   
                                                }
                                            }
                                           
                                            reg2.PROYECTO = re[1].ToString();
                                            var strMesGestion = re[6].ToString().Split('-');
                                            reg2.strMes = obtenerMes(strMesGestion[0].Trim()).ToString() + "-" + strMesGestion[0].Trim();
                                            reg2.strGestion = Convert.ToDecimal(strMesGestion[1].Trim());
                                            reg2.strProducto = "DO";
                                            reg2.decCantidad = Math.Round(total/1000,0);
                                            if ( reg2.strProducto == "DO")
                                            {
                                                objDatos.Add(reg2);
                                            }


                                        }
                                        
                                    }
                                    re.Close();
                                    op0.Dispose();
                                }
                                #endregion
                                break;
                        }

                        if (objDatos.Count > 0)
                        {
                            _resultado.intCodigo = 1;
                            _resultado.strMensaje = "OK ";
                            _resultado.oResultado = objDatos;
                        }
                        else
                        {
                            _resultado.intCodigo = -1;
                            _resultado.strMensaje = "No se encontraron registros. ";
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
        public string obtenerMes(string strMes)
        {
            string strValor = "";
            switch (strMes)
            {
                case "ENERO":
                    strValor = "01";
                    break;
                case "FEBRERO":
                    strValor = "02";
                    break;
                case "MARZO":
                    strValor = "03";
                    break;
                case "ABRIL":
                    strValor = "04";
                    break;
                case "MAYO":
                    strValor = "05";
                    break;
                case "JUNIO":
                    strValor = "06";
                    break;
                case "JULIO":
                    strValor = "07";
                    break;
                case "AGOSTO":
                    strValor = "08";
                    break;
                case "SEPTIEMBRE":
                    strValor = "09";
                    break;
                case "OCTUBRE":
                    strValor = "10";
                    break;
                case "NOVIEMBRE":
                    strValor = "11";
                    break;
                case "DICIEMBRE":
                    strValor = "12";
                    break;                
            }

            return strValor;
        }

        public EPResultado ReporteRank50(EObjetoEnvio6ParametrosProto ob)
        {
            var objDatos = new List<O_RANK50_CTY>();
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;

            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_REPORTES.P_REPORTE_RANK50_VENTA";
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
                        cmd.Parameters.Add(new OracleParameter("8", OracleDbType.Varchar2, ob.strParametro6,
                            ParameterDirection.Input));                  
                        var op0 = new OracleParameter("9", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);

                        cmd.ExecuteNonQuery();

                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            var decContador = 1;
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    var reg = new O_RANK50_CTY();

                                    reg.ID = decContador++;
                                    reg.GESTION = re[0].ToString();
                                    reg.MES = re[1].ToString();
                                    reg.CLASE = re[2].ToString();
                                    reg.DENOMINACION = re[3].ToString();
                                    reg.IDENTIFICADOR = re[4].ToString();
                                    reg.DEPARTAMENTO = re[5].ToString();
                                    reg.PRODUCTO = re[6].ToString();                                          
                                    reg.CENTENAR = re.GetDecimal(7);
                                    reg.VOLUMEN = re.GetDecimal(8);
                                    reg.MAX_VOL = re.GetDecimal(9);
                                    reg.MIN_VOL = re.GetDecimal(10);
                                    reg.PROM_VOL = re.GetDecimal(11);
                                    reg.CONT = re.GetDecimal(12);                         
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
        public EPResultado ValidaDJSaldoFisico(EObjetoEnvio4ParametrosProto ob)
        {
            var objDatos = new O_VAL_DJ_CTY();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (var cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_CONSULTAS.P_VAL_DJ_SALDO_FISICO";
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
                        var op0 = new OracleParameter("7", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(op0);
                        cmd.ExecuteNonQuery();
                        using (var re = ((OracleRefCursor)op0.Value).GetDataReader())
                        {
                            while (re.Read())
                            {
                                if (re.HasRows)
                                {
                                    objDatos.VALIDA = re.GetDecimal(0);
                                    objDatos.MENSAJE = re[1].ToString();
                                    objDatos.MES = re[2].ToString(); 
                                }
                            }
                            re.Close();
                            op0.Dispose();
                        }

                        if (objDatos != null)
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
