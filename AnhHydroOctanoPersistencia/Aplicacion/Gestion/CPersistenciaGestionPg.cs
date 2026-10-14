using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AnhHydroOctanoPersistencia.Entidades;
using Npgsql; // Namespace para PostgreSQL
using NpgsqlTypes;
using System.Threading.Tasks;
using System.Xml;
using ServiceStack.Text;

namespace AnhHydroOctanoPersistencia.Aplicacion.Gestion
{
    public class CPersistenciaGestionPg
    {
        private readonly EPResultado _resultado = new EPResultado();


        public static string conStringPg = "Server=" + ConfigurationManager.AppSettings["ServidorPg"] + "; " +
                                           "Port=" + ConfigurationManager.AppSettings["PuertoPg"] + "; " +
                                           "User Id=" + ConfigurationManager.AppSettings["UsuarioPg"] + "; " +
                                           "Database=" + ConfigurationManager.AppSettings["BaseDatosPg"] + "; " +
                                           "Password=" + ConfigurationManager.AppSettings["PasswordPg"] + "; Pooling=true;MinPoolSize=0;MaxPoolSize=200;CommandTimeout=120";


        public void RegistrarSaldoPg(decimal decIdEntidad, decimal decTipoOperacion, decimal decCantidad, decimal decIdProducto)
        {
            try
            {
                string sqlSaldosAux = null;

                switch (Convert.ToInt32(decTipoOperacion))
                {
                    case 30://Demasia   
                        sqlSaldosAux = "UPDATE anh_bsa.tbsa_eess_saldo SET saldo_octano = saldo_octano + @p_saldo_octano, saldo_bsa = saldo_bsa + @p_saldo_bsa, saldo_planta = saldo_planta + @p_saldo_planta, fecha_actualizacion = @p_fecha_actualizacion WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";
                        break;
                    case 32://Merma  
                        sqlSaldosAux = "UPDATE anh_bsa.tbsa_eess_saldo SET saldo_octano = saldo_octano - @p_saldo_octano, saldo_bsa = saldo_bsa - @p_saldo_bsa, saldo_planta = saldo_planta - @p_saldo_planta, fecha_actualizacion = @p_fecha_actualizacion WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";
                        break;
                    case 33://SALDO  
                        MRegistraSaldo(decIdEntidad, decIdProducto, decCantidad, DateTime.Now);
                        return;
                }

                if (sqlSaldosAux != null)
                {
                    EjecutarConsulta(sqlSaldosAux, decIdEntidad, decIdProducto, decCantidad, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                ///loggerV4(cr.ObtenerCre(), ex, "RegistrarAnuladaVentaPg", JsonConvert.SerializeObject(ob));
            }
        }

        private void MRegistraSaldo(decimal decIdEntidad, decimal decIdProducto, decimal Volumen, DateTime fechaActual)
        {
            string sqlSaldos = "UPDATE anh_bsa.tbsa_eess_saldo SET saldo_bsa = @p_saldo_bsa, fecha_actualizacion = @p_fecha_actualizacion WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";
            EjecutarConsulta(sqlSaldos, decIdEntidad, decIdProducto, Volumen, fechaActual);
        }

        private void EjecutarConsulta(string sql, decimal decIdEntidad, decimal decIdProducto, decimal Volumen, DateTime fechaActual)
        {
            using (var connection = new NpgsqlConnection(conStringPg))
            {
                try
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("p_id_entidad", decIdEntidad);
                        cmd.Parameters.AddWithValue("p_id_producto_hydro", decIdProducto);
                        cmd.Parameters.AddWithValue("p_saldo_bsa", Volumen);
                        cmd.Parameters.AddWithValue("p_fecha_actualizacion", fechaActual);

                        if (sql.Contains("saldo_octano"))
                        {
                            cmd.Parameters.AddWithValue("p_saldo_octano", Volumen);
                            cmd.Parameters.AddWithValue("p_saldo_planta", Volumen);
                        }

                        //cmd.ExecuteNonQuery();
                        var aux = (int)cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }
        }
       

        //public void RegistrarSaldoPg(decimal decIdEntidad, decimal decTipoOperacion, decimal decCantidad, decimal decIdProducto, decimal decFactorCalculo)
        //{
        //    try
        //    {
        //        DateTime fechaActual;
        //        fechaActual = DateTime.Now;                
        //        string sqlSaldosAux = "";
        //        switch (Convert.ToInt32(decTipoOperacion))
        //        {
        //            case 30://Demasia                      
        //                sqlSaldosAux = "UPDATE anh_bsa.tbsa_eess_saldo " +
        //                "SET saldo_octano = saldo_octano + @p_saldo_octano, saldo_bsa = saldo_bsa + @p_saldo_bsa, saldo_planta = saldo_planta + @p_saldo_planta, fecha_actualizacion = @p_fecha_actualizacion " +
        //                "WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";
        //                MRegistraOperacion(decIdEntidad, decIdProducto, decCantidad, fechaActual, sqlSaldosAux);
        //                break;
        //            case 33://SALDO                   
        //                MRegistraSaldo(decIdEntidad, decIdProducto, decCantidad, fechaActual);                       
        //                break;
        //            case 32://Merma                       
        //                sqlSaldosAux = "UPDATE anh_bsa.tbsa_eess_saldo " +
        //                "SET saldo_octano = saldo_octano - @p_saldo_octano, saldo_bsa = saldo_bsa - @p_saldo_bsa, saldo_planta = saldo_planta - @p_saldo_planta, fecha_actualizacion = @p_fecha_actualizacion " +
        //                "WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";
        //                MRegistraOperacion(decIdEntidad, decIdProducto, decCantidad, fechaActual, sqlSaldosAux);
        //                break;
        //            default:
        //                break;
        //        }               
        //    }
        //    catch (Exception ex)
        //    {
        //        ///loggerV4(cr.ObtenerCre(), ex, "RegistrarAnuladaVentaPg", JsonConvert.SerializeObject(ob));
        //    }
        //}
        //private void MRegistraSaldo(decimal decIdEntidad, decimal decIdProducto, decimal Volumen, DateTime fechaActual)
        //{
        //    using (var connection = new NpgsqlConnection(conStringPg))
        //    {
        //        connection.Open();
                               
        //        string sqlSaldos = "UPDATE anh_bsa.tbsa_eess_saldo " +
        //               "SET saldo_bsa = @p_saldo_bsa, fecha_actualizacion = @p_fecha_actualizacion " +
        //               "WHERE id_entidad = @p_id_entidad and id_producto_hydro = @p_id_producto_hydro";

        //        using (var cmd = new NpgsqlCommand(sqlSaldos, connection))
        //        {
        //            // Configurar parámetros
        //            cmd.Parameters.AddWithValue("p_id_entidad", decIdEntidad);
        //            cmd.Parameters.AddWithValue("p_id_producto_hydro", decIdProducto);                    
        //            cmd.Parameters.AddWithValue("p_saldo_bsa", Volumen);                    
        //            cmd.Parameters.AddWithValue("p_fecha_actualizacion", fechaActual);
        //            // Ejecutar la consulta
        //            int respuesta = cmd.ExecuteNonQuery();
        //        }

        //    }
        //}

        //private void MRegistraOperacion(decimal decIdEntidad, decimal decIdProducto, decimal Volumen, DateTime fechaActual, string sqlSaldos)
        //{
        //    using (var connection = new NpgsqlConnection(conStringPg))
        //    {
        //        connection.Open();                         
        //        using (var cmd = new NpgsqlCommand(sqlSaldos, connection))
        //        {
        //            // Configurar parámetros
        //            cmd.Parameters.AddWithValue("p_id_entidad", decIdEntidad);
        //            cmd.Parameters.AddWithValue("p_id_producto_hydro", decIdProducto);
        //            cmd.Parameters.AddWithValue("p_saldo_octano", Volumen);
        //            cmd.Parameters.AddWithValue("p_saldo_bsa", Volumen);
        //            cmd.Parameters.AddWithValue("p_saldo_planta", Volumen);
        //            cmd.Parameters.AddWithValue("p_fecha_actualizacion", fechaActual);
        //            // Ejecutar la consulta
        //            int respuesta = cmd.ExecuteNonQuery();
        //        }

        //    }
        //}
    }
       
}
