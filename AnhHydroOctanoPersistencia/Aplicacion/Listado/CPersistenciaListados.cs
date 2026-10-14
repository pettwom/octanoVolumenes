using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnhHydroOctanoPersistencia.Entidades;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.ManagedDataAccess.Client.OracleDataReader;
using OracleParameter = Oracle.ManagedDataAccess.Client.OracleParameter;
using System.Data;
using Oracle.ManagedDataAccess.Types;

namespace AnhHydroOctanoPersistencia.Aplicacion.Listado
{
    public class CPersistenciaListados
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
        public EPResultado ListadoPreciosImportados(decimal decIdPrecioImportado)
        {
            var objDatos = new List<O_PRECIO_IMP_BSA_CTY>();
            try
            {
                using (var xon = new OracleConnection(ConString))
                {
                    xon.Open();
                    using (OracleCommand cmd = xon.CreateCommand())
                    {
                        cmd.CommandText = "APP_MOVIMIENTOS.PMOV_LISTADOS.P_LISTA_PRECIOS_IMP_BSA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new OracleParameter("1", OracleDbType.Int32, decIdPrecioImportado,
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
                                    var reg = new O_PRECIO_IMP_BSA_CTY();
                                    reg.ID_PRECIO_IMPORTADO = re.GetDecimal(0); 
                                    reg.ID_ENTIDAD = Convert.ToDecimal(re[1].ToString());
                                    reg.IMPORTADORA  = re[2].ToString();
                                    reg.ID_PRODUCTO = Convert.ToDecimal(re[3].ToString());
                                    reg.PRODUCTO  = re[4].ToString();
                                    reg.COD_DISDENSER = re[5].ToString();
                                    reg.COD_MANGUERAS = re[6].ToString();
                                    reg.PRECIO = Convert.ToDecimal(re[7].ToString());
                                    reg.VOLUMEN = Convert.ToDecimal(re[8].ToString());                                    
                                    reg.FECHA_DESDE = re[9].ToString();
                                    reg.FECHA_HASTA = re[10].ToString(); 
                                    reg.OBSERVACIONES  = re[11].ToString();
                                    reg.ESTADO = Convert.ToDecimal(re[12].ToString());                                   

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
