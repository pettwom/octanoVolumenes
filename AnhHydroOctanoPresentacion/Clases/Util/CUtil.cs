using System;
using System.Collections.Generic;
using System.Linq;
using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Parametros;
using ServiceStack.Text;
using AnhHydroOctanoPresentacion.Enumeradores;
using System.Text.RegularExpressions;

namespace AnhHydroOctanoPresentacion.Clases.Util
{
    public class CUtil
    {
        #region Variables de entorno
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        #endregion

        #region Metodos

        /// <summary>
        /// METODO. MObtenerArbolOrdenado
        /// </summary>
        /// <param name="intTipoOrden"></param>
        /// <param name="objArbol"></param>
        /// <returns></returns>
        public List<CArbol> MObtenerArbolOrdenado(int intTipoOrden, List<O_OBTIENE_ENTIDADES_CTY> objArbol)
        {
            var lstResultado = new List<CArbol>();
            if (objArbol != null)
            {
                var lstEntidad = objArbol;

                int idDe = 0, idAc = 0;
                int idUnico = 1, idPadre = 0, idUnic1 = 0;
                int intNivel1 = 0, intNivel2 = 0, intNivel3 = 0;
                string strNivel1 = "", strNivel2 = "";
                string idStrAc = "";

                switch (intTipoOrden)
                {
                    case 0:
                        #region Solo Actividad
                        
                        lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
                        foreach (var lst in lstEntidad)
                        {
                            if (idDe != lst.ID_ACTIVIDAD)
                            {
                                var cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = 0,
                                    IntIdEntidad = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    IntIdOperador = (int) lst.ID_CONSUMIDOR,
                                    StrDescripcion = lst.ACTIVIDAD.Trim(),
                                    StrDescPadre = lst.ACTIVIDAD.Trim(),
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = "",
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    IntAccion = 1,
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idDe = (int)lst.ID_ACTIVIDAD;
                                idUnico++;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            idUnico++;
                        }

                        #endregion
                        break;
                    case 1:
                        #region orden Entidad

                        lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();
                        foreach (var lst in lstEntidad)
                        {
                            long lng;
                            var lngNit = long.TryParse(lst.NIT, out lng) ? Convert.ToInt64(lst.NIT) : 0;

                            CArbol cDato;
                            if (idDe != lst.ID_ENTIDAD_PADRE)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = 0,
                                    IntIdEntidadPadre = (int) lst.ID_ENTIDAD_PADRE,
                                    IntIdActividad = 0,
                                    StrActividad = "",
                                    IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE,
                                    IntIdOperador = 0,
                                    StrDescripcion = lst.DENOMINACION_PADRE.Trim(),
                                    StrDescPadre = lst.DENOMINACION_PADRE.Trim(),
                                    //LngNit = Convert.ToInt64(lst.NIT),
                                    LngNit = lngNit,
                                    StrLicencia = "",
                                    StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    StrMunicipio = "",
                                    StrLocalidad = "",
                                    StrTelefono = "",
                                    StrAmbito = "",
                                    StrTipoSociedad = "",
                                    StrPropietario = "",
                                    StrCiPropietario = "",
                                    StrCiRepresentante = "",
                                    IntAccion = 0,
                                    StrUbicacion = intNivel1.ToString(),
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idUnic1 = idUnico;
                                idDe = (int)lst.ID_ENTIDAD_PADRE;
                                idAc = 0;
                                idUnico++;
                                strNivel1 = cDato.StrUbicacion;
                                intNivel1++;
                                intNivel2 = 0;
                            }
                            if (idAc != lst.ID_ACTIVIDAD)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = idUnic1,
                                    IntIdEntidadPadre = (int) lst.ID_ENTIDAD_PADRE,
                                    IntIdEntidad = 0,
                                    IntIdOperador = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    StrActividad = lst.ACTIVIDAD,
                                    StrDescripcion = lst.ACTIVIDAD,
                                    StrDescPadre = lst.DENOMINACION_PADRE,
                                    //LngNit = Convert.ToInt64(lst.NIT),
                                    LngNit = lngNit,
                                    StrLicencia = "",
                                    StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    StrMunicipio = "",
                                    StrLocalidad = "",
                                    StrTelefono = "",
                                    StrAmbito = "",
                                    StrTipoSociedad = "",
                                    StrPropietario = "",
                                    StrCiPropietario = "",
                                    StrCiRepresentante = "",
                                    IntAccion = 1,
                                    StrUbicacion = strNivel1 + "-" + intNivel2,
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idAc = (int)lst.ID_ACTIVIDAD;
                                idUnico++;
                                strNivel2 = cDato.StrUbicacion;
                                intNivel2++;
                                intNivel3 = 0;
                            }
                            cDato = new CArbol
                            {
                                IntId = idUnico,
                                IntIdPadre = idPadre,
                                IntIdEntidadPadre = (int) lst.ID_ENTIDAD_PADRE,
                                IntIdEntidad = (int) lst.ID_ENTIDAD,
                                IntIdOperador = (int) lst.ID_CONSUMIDOR,
                                IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                StrActividad = lst.ACTIVIDAD,
                                StrDescripcion = lst.DENOMINACION,
                                StrDescPadre = lst.DENOMINACION_PADRE,
                                //LngNit = Convert.ToInt64(lst.NIT),
                                LngNit = lngNit,
                                StrLicencia = lst.NRO_LICENCIA,
                                StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                StrDireccion = lst.DIRECCION,
                                StrDepartamento = lst.DEPARTAMENTO,
                                DecIdDepartamento = (decimal)lst.ID_DEPARTAMENTO,
                                StrMunicipio = lst.MUNICIPIO,
                                StrLocalidad = lst.LOCALIDAD,
                                StrTelefono = lst.TELEFONOS,
                                StrAmbito = lst.AMBITO_OPERACION,
                                StrTipoSociedad = lst.TIPO_SOCIEDAD,
                                StrPropietario = lst.NOMBRES_PROPIETARIO,
                                StrCiPropietario = lst.CI_PROPIETARIO,
                                StrCiRepresentante = lst.CI_REPRESENTANTE,
                                IntAccion = 2,
                                StrUbicacion = strNivel2 + "-" + intNivel3,
                                StrNroHydro = lst.NRO_HYDRO
                            };
                            lstResultado.Add(cDato);
                            idUnico++;
                            intNivel3++;
                        }

                        #endregion
                        break;
                    case 2:
                        #region orden Actividad
                        lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                        foreach (var lst in lstEntidad)
                        {
                            long lng;
                            var lngNit = long.TryParse(lst.NIT, out lng) ? Convert.ToInt64(lst.NIT) : 0;

                            CArbol cDato;
                            if (idDe != lst.ID_ACTIVIDAD)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = 0,
                                    IntIdEntidad = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    IntIdOperador = 0,
                                    IntIdProyecto = 0,
                                    StrDescripcion = lst.ACTIVIDAD.Trim(),
                                    StrDescPadre = lst.ACTIVIDAD.Trim(),
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    DecIdDepartamento = 0,
                                    StrMunicipio = "",
                                    StrLocalidad = "",
                                    StrTelefono = "",
                                    StrAmbito = "",
                                    StrPropietario = "",
                                    StrCiPropietario = "",
                                    StrCiRepresentante = "",
                                    IntAccion = 0,
                                    StrUbicacion = intNivel1.ToString(),
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idUnic1 = idUnico;
                                idDe = (int)lst.ID_ACTIVIDAD;
                                idStrAc = "";
                                idUnico++;
                                strNivel1 = cDato.StrUbicacion;
                                intNivel1++;
                                intNivel2 = 0;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            if (idStrAc != lst.DEPARTAMENTO)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = idUnic1,
                                    IntIdEntidad = 0,
                                    IntIdOperador = 0,
                                    IntIdProyecto = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    StrDescripcion = lst.DEPARTAMENTO,
                                    StrDescPadre = lst.ACTIVIDAD,
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = "",
                                    StrDireccion = "",
                                    StrDepartamento = lst.DEPARTAMENTO,
                                    DecIdDepartamento = (decimal) lst.ID_DEPARTAMENTO,
                                    StrMunicipio = "",
                                    StrLocalidad = "",
                                    StrTelefono = "",
                                    StrAmbito = "",
                                    StrPropietario = "",
                                    StrCiPropietario = "",
                                    StrCiRepresentante = "",
                                    IntAccion = 0,
                                    StrUbicacion = strNivel1 + "-" + intNivel2,
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idStrAc = lst.DEPARTAMENTO;
                                idUnico++;
                                strNivel2 = cDato.StrUbicacion;
                                intNivel2++;
                                intNivel3 = 0;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            cDato = new CArbol
                            {
                                IntId = idUnico,
                                IntIdPadre = idPadre,
                                IntIdEntidad = (int) lst.ID_ENTIDAD_PADRE,
                                IntIdOperador = (int) lst.ID_CONSUMIDOR,
                                IntIdProyecto = (int) lst.ID_ENTIDAD,
                                IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                StrDescripcion = lst.DENOMINACION,
                                StrDescPadre = lst.DENOMINACION,
                                LngNit = lngNit,
                                StrActividad = lst.ACTIVIDAD,
                                StrLicencia = lst.NRO_LICENCIA,
                                StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                StrDireccion = lst.DIRECCION,
                                StrDepartamento = lst.DEPARTAMENTO,
                                DecIdDepartamento = (decimal) lst.ID_DEPARTAMENTO,
                                StrMunicipio = lst.MUNICIPIO,
                                StrLocalidad = lst.LOCALIDAD,
                                StrTelefono = lst.TELEFONOS,
                                StrAmbito = lst.AMBITO_OPERACION,
                                StrPropietario = lst.NOMBRES_PROPIETARIO,
                                StrCiPropietario = lst.CI_PROPIETARIO,
                                StrCiRepresentante = lst.CI_REPRESENTANTE,
                                IntAccion = 1,
                                StrUbicacion = strNivel2 + "-" + intNivel3,
                                StrNroHydro = lst.NRO_HYDRO
                            };
                            lstResultado.Add(cDato);
                            idUnico++;
                            intNivel3++;
                        }

                        #endregion
                        break;
                    case 3:
                        #region orden Actividad con varias acciones
                        lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
                        foreach (var lst in lstEntidad)
                        {
                            long lng;
                            var lngNit = long.TryParse(lst.NIT, out lng) ? Convert.ToInt64(lst.NIT) : 0;

                            CArbol cDato;
                            if (idDe != lst.ID_ACTIVIDAD)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = 0,
                                    IntIdEntidadPadre = 0,
                                    IntIdEntidad = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    IntIdOperador = (int)lst.ID_CONSUMIDOR,
                                    StrDescripcion = lst.ACTIVIDAD.Trim(),
                                    StrDescPadre = lst.ACTIVIDAD.Trim(),
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = "",
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    DecIdDepartamento = 0,
                                    IntAccion = 1,
                                    StrUbicacion = intNivel1.ToString(),
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idUnic1 = idUnico;
                                idDe = (int)lst.ID_ACTIVIDAD;
                                idStrAc = "";
                                strNivel1 = cDato.StrUbicacion;
                                idUnico++;
                                intNivel1++;
                                intNivel2 = 0;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            if (idStrAc != lst.DEPARTAMENTO)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = idUnic1,
                                    IntIdEntidadPadre = 0,
                                    IntIdEntidad = 0,
                                    IntIdOperador = 0,
                                    IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                    StrDescripcion = lst.DEPARTAMENTO,
                                    DecIdDepartamento = (decimal)lst.ID_DEPARTAMENTO,
                                    StrDescPadre = lst.ACTIVIDAD,
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = "",
                                    StrDireccion = "",
                                    StrDepartamento = lst.DEPARTAMENTO,
                                    IntAccion = 2,
                                    StrUbicacion = strNivel1 + "-" + intNivel2,
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idStrAc = lst.DEPARTAMENTO;
                                idUnico++;
                                strNivel2 = cDato.StrUbicacion;
                                intNivel2++;
                                intNivel3 = 0;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            cDato = new CArbol
                            {
                                IntId = idUnico,
                                IntIdPadre = idPadre,
                                IntIdEntidadPadre = (int)lst.ID_ENTIDAD_PADRE,
                                IntIdEntidad = (int)lst.ID_ENTIDAD,
                                IntIdOperador = (int) lst.ID_CONSUMIDOR,
                                IntIdActividad = (int) lst.ID_ACTIVIDAD,
                                StrDescripcion = lst.DENOMINACION,
                                StrDescPadre = lst.ACTIVIDAD,
                                LngNit = lngNit,
                                StrLicencia = lst.NRO_LICENCIA,
                                StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                StrDireccion = lst.DIRECCION,
                                StrDepartamento = lst.DEPARTAMENTO,
                                DecIdDepartamento = (decimal)lst.ID_DEPARTAMENTO,
                                IntAccion = 3,
                                StrUbicacion = strNivel2 + "-" + intNivel3,
                                StrNroHydro = lst.NRO_HYDRO
                            };
                            lstResultado.Add(cDato);
                            idUnico++;
                            intNivel3++;
                        }
                        #endregion
                        break;
                }
            }
            return lstResultado;
        }

        /// <summary>
        /// METODO. MObtieneMovAcumulado
        /// </summary>
        /// <param name="decFiltro"></param>
        /// <param name="strIdTipoActividad"></param>
        /// <param name="strIdConsumidor"></param>
        /// <param name="strFechaDiaInicio"></param>
        /// <param name="strFechaDiaFin"></param>
        /// <param name="intTipo"></param>
        /// <returns></returns>
        public EPResultado MObtieneMovAcumulado(decimal decFiltro, string strIdTipoActividad, string strIdConsumidor, string strFechaDiaInicio, string strFechaDiaFin, int intTipo, decimal decidTipoOp)
        {
            var resultado = new EPResultado();
            List<O_SALDO_INICIAL_DIA_CTY> lstSaldos;
            List<O_OBTENER_MOVIMIENTO_ACUM_CTY> lstMovAcum;
            List<O_OBTENER_TIPO_OPE_ACUM_CTY> lstTipoOp; 

            var objResultado = _consulta.ObtenerCursoresDdjj(
            new EObjetoEnvio4ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = decFiltro,
                strParametro1 = strIdTipoActividad,
                strParametro2 = strIdConsumidor,
                strParametro3 = strFechaDiaInicio,
                strParametro4 = strFechaDiaFin
            });
            if (objResultado.intCodigo == 1)
            {
                var objRes = (O_CURSORES_DDJJ)objResultado.oResultado;
                lstSaldos = objRes.lstSaldos;
                lstMovAcum = objRes.lstMovimientos;
                lstTipoOp = objRes.lstTipoOp;

                if (decFiltro == 6)
                { strIdTipoActividad = "16"; }

                var decIdMov = (lstMovAcum.IsNullOrEmpty())? 0 : lstMovAcum.FirstOrDefault().ID_MOVIMIENTO;
                var decFechaIni = Convert.ToDecimal(strFechaDiaInicio);
                var decFechaFin = Convert.ToDecimal(strFechaDiaFin);
                var lstAcumulado = new List<CDjAcumulado>();

                foreach (var reg in lstSaldos)
                {
                    decimal decSalIni = 0;
                    var objAcumulado1 = new CDjAcumulado();

                    objAcumulado1.DecIdHidrocarburo = reg.ID_PRODUCTO;
                    objAcumulado1.StrHidrocarburo = reg.PRODUCTO;
                    objAcumulado1.StrCodHidrocarburo = reg.CODIGO;
                    objAcumulado1.StrUnidadMedida = reg.UMEDIDA;
                    objAcumulado1.DecIdUnidadMedida = reg.ID_UMEDIDA;

                    objAcumulado1.DecIdTipoInfraestructura = reg.TIPO_INFRAESTRUCTURA;
                    objAcumulado1.StrTipoInfraestructura = reg.TIPO_INF;
                    objAcumulado1.DecIdInfraestructura = reg.ID_INFRAESTRUCTURA;
                    objAcumulado1.StrInfraestructura = reg.INFRAESTRUCTURA;
                    objAcumulado1.StrInfraestructuraAnh = reg.INFRAESTRUCTURA_ANH;
                    objAcumulado1.StrObservaciones = reg.OBSERVACIONES;

                    for (decimal i = decFechaIni; i <= decFechaFin; i++)
                    {
                        var objAcumulado2 = new CDjAcumulado();

                        objAcumulado2.DecIdHidrocarburo = objAcumulado1.DecIdHidrocarburo;
                        objAcumulado2.StrHidrocarburo = objAcumulado1.StrHidrocarburo;
                        objAcumulado2.StrCodHidrocarburo = objAcumulado1.StrCodHidrocarburo;
                        objAcumulado2.StrUnidadMedida = objAcumulado1.StrUnidadMedida;
                        objAcumulado2.DecIdUnidadMedida = objAcumulado1.DecIdUnidadMedida;

                        objAcumulado2.DecIdTipoInfraestructura = objAcumulado1.DecIdTipoInfraestructura;
                        objAcumulado2.StrTipoInfraestructura = objAcumulado1.StrTipoInfraestructura;
                        objAcumulado2.DecIdInfraestructura = objAcumulado1.DecIdInfraestructura;
                        objAcumulado2.StrInfraestructura = objAcumulado1.StrInfraestructura;
                        objAcumulado2.StrInfraestructuraAnh = objAcumulado1.StrInfraestructuraAnh;
                        objAcumulado2.StrObservaciones = reg.OBSERVACIONES;

                        objAcumulado2.DecFecha = i;
                        if (i == decFechaIni)
                        {
                            decSalIni = reg.SALDO;
                        }
                        objAcumulado2.DecSaldoInicial = decSalIni;

                        var LstIngresos = new List<O_OP_SALDOS_CTY>();
                        var LstEgresos = new List<O_OP_SALDOS_CTY>();
                        decimal decTotIng = 0;
                        decimal decTotSal = 0;
                        decimal decImpTot = 0;

                        foreach (var top in lstTipoOp)
                        {
                            var objMov = new O_OP_SALDOS_CTY();
                            objMov.OPERACION_NOMBRE = top.OPERACION_NOMBRE;
                            objMov.OPERACION_ORDEN = top.OPERACION_ORDEN;
                            objMov.REPORTE_NOMBRE = top.REPORTE_NOMBRE;
                            objMov.REPORTE_ORDEN = top.REPORTE_ORDEN;
                            objMov.FACTOR_TIPO = top.FACTOR_TIPO;
                            objMov.FECHA_OPERACION_DIA = i;
                            objMov.OPERACION_NOMBRE = top.OPERACION_NOMBRE;

                            var mov = new List<O_OBTENER_MOVIMIENTO_ACUM_CTY>();
                            if (lstMovAcum != null && lstMovAcum.Any())
                            {
                                mov = lstMovAcum.Where(
                                        x =>
                                            x.FECHA_OPERACION_DIA == i &&
                                            x.ID_HIDROCARBURO == reg.ID_PRODUCTO &&
                                            x.TIPO_INFRAESTRUCTURA == reg.TIPO_INFRAESTRUCTURA &&
                                            x.ID_INFRAESTRUCTURA == reg.ID_INFRAESTRUCTURA &&
                                            x.ID_TIPO_OPERACION == top.ID_TIPO_OPERACION).ToList();
                            }

                            objMov.CANTIDAD = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().CANTIDAD;
                            objMov.PRECIO_TOTAL = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().PRECIO_TOTAL;

                            if (top.FACTOR_TIPO == 1)
                            {
                                LstIngresos.Add(objMov);
                                decTotIng += objMov.CANTIDAD;
                            }
                            else
                            {
                                LstEgresos.Add(objMov);
                                decTotSal += objMov.CANTIDAD;
                                decImpTot += objMov.PRECIO_TOTAL;
                            }
                        }
                        objAcumulado2.LstIngresos = LstIngresos;
                        objAcumulado2.LstEgresos = LstEgresos;
                        objAcumulado2.DecTotalIngreso = decTotIng;
                        objAcumulado2.DecTotalEgreso = decTotSal;
                        objAcumulado2.DecSaldoFinal = decSalIni + decTotIng - decTotSal;
                        objAcumulado2.DecTotalImporte = decImpTot;
                        decSalIni = objAcumulado2.DecSaldoFinal;
                        lstAcumulado.Add(objAcumulado2);
                    }
                }

                if (!lstAcumulado.IsNullOrEmpty() && lstAcumulado.Count > 0)
                {
                    resultado.intCodigo = 1;
                    resultado.strMensaje = decIdMov.ToString();
                    var oResp = new O_DECLARACION_JURADA_CTY();
                    oResp.LstAcumulado = lstAcumulado;
                    if (intTipo == 1)
                    {
                        var request = new EListarDeclaracionJuradaAcumuladaD();
                        request.decFiltro = 1;
                        request.decIdConsumidor = Convert.ToDecimal(strIdConsumidor);
                        request.decIdTipoActividad = Convert.ToDecimal(strIdTipoActividad);
                        request.decFechaDiaInicio = Convert.ToDecimal(strFechaDiaInicio);
                        request.decFechaDiaFin = Convert.ToDecimal(strFechaDiaFin);

                        switch (strIdTipoActividad)
                        {
                            case "1":
                            case "2":
                                var listaTransaccionTipo = _consulta.ListaTransaccionTipo(request);
                                oResp.LstaAdicional2 = (List<ObjDjTransaccion>)listaTransaccionTipo.oResultado;

                                if (request.decFechaDiaInicio > 20180601)
                                {
                                    var objRecaudo = _consulta.ListaRecaudoExterno(
                                    new EObjetoEnvio5ParametrosProto
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = request.decFiltro,
                                        strParametro1 = request.decFechaDiaInicio + "000000",
                                        strParametro2 = request.decFechaDiaFin + "235959",
                                        strParametro3 = request.decIdTipoActividad.ToString(),
                                        strParametro4 = request.decIdConsumidor.ToString(),
                                        strParametro5 = decidTipoOp.ToString(),
                                        strParametro6 =Convert.ToString((decimal)EnumTipoTransaccion.SrvSuic + "|" + (decimal)EnumTipoTransaccion.SrvDs3578),//decidTipoOp.ToString(),
                                    });
                                    oResp.LstaRecaudoExt = (List<O_RECAUDO_EXTERNO_CTY>)objRecaudo.oResultado;
                                }
                                break;
                            case "50":
                                request.decFiltro = 2;
                                var listaTransaccionTipo1 = _consulta.ListaTransaccionTipo(request);
                                oResp.LstaAdicional1 = (List<ObjDjTransaccion>)listaTransaccionTipo1.oResultado;
                                break;
                        }
                    }
                    resultado.oResultado = oResp;
                }
                else
                {
                    resultado.intCodigo = -1;
                    resultado.strMensaje = "No se encontraron registros.";
                    resultado.oResultado = null;
                }
            }
            else
            {
                resultado.intCodigo = -1;
                resultado.strMensaje = objResultado.strMensaje;
            }

            return resultado;
        }
        public EPResultado MObtieneMovAcumuladoSaldo(decimal decFiltro, string strIdTipoActividad, string strIdConsumidor, string strFechaDiaInicio, string strFechaDiaFin, int intTipo, decimal decidTipoOp)
        {
            var resultado = new EPResultado();
            List<O_SALDO_INICIAL_DIA_CTY> lstSaldos;
            List<O_OBTENER_MOVIMIENTO_ACUM_SAL> lstMovAcum;
            List<O_OBTENER_TIPO_OPE_ACUM_CTY> lstTipoOp;

            var objResultado = _consulta.ObtenerCursoresDdjjSaldo(
            new EObjetoEnvio4ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = decFiltro,
                strParametro1 = strIdTipoActividad,
                strParametro2 = strIdConsumidor,
                strParametro3 = strFechaDiaInicio,
                strParametro4 = strFechaDiaFin
            });
            if (objResultado.intCodigo == 1)
            {
                var objRes = (O_CURSORES_DDJJ_SAL)objResultado.oResultado;
                lstSaldos = objRes.lstSaldos;
                lstMovAcum = objRes.lstMovimientos;
                lstTipoOp = objRes.lstTipoOp;

                if (decFiltro == 6)
                { strIdTipoActividad = "16"; }

                var decIdMov = (lstMovAcum.IsNullOrEmpty()) ? 0 : lstMovAcum.FirstOrDefault().ID_MOVIMIENTO;
                var decFechaIni = Convert.ToDecimal(strFechaDiaInicio);
                var decFechaFin = Convert.ToDecimal(strFechaDiaFin);
                var lstAcumulado = new List<CDjAcumulado_SAL>();

                foreach (var reg in lstSaldos)
                {
                    decimal decSalIni = 0;
                    var objAcumulado1 = new CDjAcumulado_SAL();

                    objAcumulado1.DecIdHidrocarburo = reg.ID_PRODUCTO;
                    objAcumulado1.StrHidrocarburo = reg.PRODUCTO;
                    objAcumulado1.StrCodHidrocarburo = reg.CODIGO;
                    objAcumulado1.StrUnidadMedida = reg.UMEDIDA;
                    objAcumulado1.DecIdUnidadMedida = reg.ID_UMEDIDA;

                    objAcumulado1.DecIdTipoInfraestructura = reg.TIPO_INFRAESTRUCTURA;
                    objAcumulado1.StrTipoInfraestructura = reg.TIPO_INF;
                    objAcumulado1.DecIdInfraestructura = reg.ID_INFRAESTRUCTURA;
                    objAcumulado1.StrInfraestructura = reg.INFRAESTRUCTURA;
                    objAcumulado1.StrInfraestructuraAnh = reg.INFRAESTRUCTURA_ANH;
                    objAcumulado1.StrObservaciones = reg.OBSERVACIONES;
                    

                    for (decimal i = decFechaIni; i <= decFechaFin; i++)
                    {
                        var objAcumulado2 = new CDjAcumulado_SAL();

                        objAcumulado2.DecIdHidrocarburo = objAcumulado1.DecIdHidrocarburo;
                        objAcumulado2.StrHidrocarburo = objAcumulado1.StrHidrocarburo;
                        objAcumulado2.StrCodHidrocarburo = objAcumulado1.StrCodHidrocarburo;
                        objAcumulado2.StrUnidadMedida = objAcumulado1.StrUnidadMedida;
                        objAcumulado2.DecIdUnidadMedida = objAcumulado1.DecIdUnidadMedida;

                        objAcumulado2.DecIdTipoInfraestructura = objAcumulado1.DecIdTipoInfraestructura;
                        objAcumulado2.StrTipoInfraestructura = objAcumulado1.StrTipoInfraestructura;
                        objAcumulado2.DecIdInfraestructura = objAcumulado1.DecIdInfraestructura;
                        objAcumulado2.StrInfraestructura = objAcumulado1.StrInfraestructura;
                        objAcumulado2.StrInfraestructuraAnh = objAcumulado1.StrInfraestructuraAnh;
                        objAcumulado2.StrObservaciones = reg.OBSERVACIONES;

                        /*objAcumulado2.DecDia = objAcumulado1.DecDia;
                        objAcumulado2.StrMes = objAcumulado1.StrMes;
                        objAcumulado2.StrAnio = objAcumulado1.StrAnio;
                        objAcumulado2.StrDescripcionTanque = objAcumulado1.StrDescripcionTanque;
                        objAcumulado2.DecVolumenNominal = objAcumulado1.DecVolumenNominal;
                        */
                        /*objMov.DIA = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().DIA;
                        objMov.MES = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().MES;
                        objMov.GESTION = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().GESTION;
                        objMov.DESCRIPCION_TANQUES = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().DESCRIPCION_TANQUES;
                        objMov.VOLUMEN_NOMINAL = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().VOLUMEN_NOMINAL;
                            
                         */
                        objAcumulado2.DecFecha = i;
                        if (i == decFechaIni)
                        {
                            decSalIni = reg.SALDO;
                        }
                        objAcumulado2.DecSaldoInicial = decSalIni;

                        var LstIngresos = new List<O_OP_SALDOS_SAL>();
                        var LstEgresos = new List<O_OP_SALDOS_SAL>();
                        decimal decTotIng = 0;
                        decimal decTotSal = 0;
                        decimal decImpTot = 0;


                        /* suma la cantidad por tipo de operacion*/
                        foreach (var top in lstTipoOp)
                        {
                            var objMov = new O_OP_SALDOS_SAL();
                            objMov.OPERACION_NOMBRE = top.OPERACION_NOMBRE;
                            objMov.OPERACION_ORDEN = top.OPERACION_ORDEN;
                            objMov.REPORTE_NOMBRE = top.REPORTE_NOMBRE;
                            objMov.REPORTE_ORDEN = top.REPORTE_ORDEN;
                            objMov.FACTOR_TIPO = top.FACTOR_TIPO;
                            objMov.FECHA_OPERACION_DIA = i;
                            objMov.OPERACION_NOMBRE = top.OPERACION_NOMBRE;

                            var mov = new List<O_OBTENER_MOVIMIENTO_ACUM_SAL>();
                            if (lstMovAcum != null && lstMovAcum.Any())
                            {
                                mov = lstMovAcum.Where(
                                        x =>
                                            x.FECHA_OPERACION_DIA == i &&
                                            x.ID_HIDROCARBURO == reg.ID_PRODUCTO &&
                                            x.TIPO_INFRAESTRUCTURA == reg.TIPO_INFRAESTRUCTURA &&
                                            x.ID_INFRAESTRUCTURA == reg.ID_INFRAESTRUCTURA &&
                                            x.ID_TIPO_OPERACION == top.ID_TIPO_OPERACION).ToList();
                                
                                

                               
                            }

                            objMov.CANTIDAD = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().CANTIDAD;
                            objMov.PRECIO_TOTAL = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().PRECIO_TOTAL;

                            if (mov.Count != 0) {
                                objAcumulado2.DecDia = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().DIA;
                                objAcumulado2.StrDescripcionDia = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().DESCRIPCION_DIA;
                                objAcumulado2.StrMes = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().MES;
                                objAcumulado2.StrAnio = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().GESTION;
                                objAcumulado2.StrDescripcionTanque = mov.IsNullOrEmpty() ? "0" : mov.FirstOrDefault().DESCRIPCION_TANQUES;
                                objAcumulado2.DecVolumenNominal = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().VOLUMEN_NOMINAL;
                                objAcumulado2.DecProgramacion = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().PROGRAMACION;
                                objAcumulado2.DecRecepProg = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().RECEPCION_PROGRAMACION;
                                objAcumulado2.DecVentaProg = mov.IsNullOrEmpty() ? 0 : mov.FirstOrDefault().VENTA_PROGRAMACION;
                            }
                           
                     
                            if (top.FACTOR_TIPO == 1)
                            {
                                LstIngresos.Add(objMov);
                                decTotIng += objMov.CANTIDAD;
                            }
                            else
                            {
                                LstEgresos.Add(objMov);
                                decTotSal += objMov.CANTIDAD;
                                decImpTot += objMov.PRECIO_TOTAL;
                            }
                        }
                        objAcumulado2.LstIngresos = LstIngresos;
                        objAcumulado2.LstEgresos = LstEgresos;
                        objAcumulado2.DecTotalIngreso = decTotIng;
                        objAcumulado2.DecTotalEgreso = decTotSal;
                        objAcumulado2.DecSaldoFinal = decSalIni + decTotIng - decTotSal;
                        objAcumulado2.DecTotalImporte = decImpTot;
                        decSalIni = objAcumulado2.DecSaldoFinal;
                        lstAcumulado.Add(objAcumulado2);
                    }
                }

                if (!lstAcumulado.IsNullOrEmpty() && lstAcumulado.Count > 0)
                {
                    resultado.intCodigo = 1;
                    resultado.strMensaje = decIdMov.ToString();
                    var oResp = new O_DECLARACION_JURADA_SAL();
                    oResp.LstAcumulado = lstAcumulado;
                    if (intTipo == 1)
                    {
                        var request = new EListarDeclaracionJuradaAcumuladaD();
                        request.decFiltro = 1;
                        request.decIdConsumidor = Convert.ToDecimal(strIdConsumidor);
                        request.decIdTipoActividad = Convert.ToDecimal(strIdTipoActividad);
                        request.decFechaDiaInicio = Convert.ToDecimal(strFechaDiaInicio);
                        request.decFechaDiaFin = Convert.ToDecimal(strFechaDiaFin);

                        switch (strIdTipoActividad)
                        {
                            case "1":
                            case "2":
                                var listaTransaccionTipo = _consulta.ListaTransaccionTipo(request);
                                oResp.LstaAdicional2 = (List<ObjDjTransaccion>)listaTransaccionTipo.oResultado;

                                if (request.decFechaDiaInicio > 20180601)
                                {
                                    var objRecaudo = _consulta.ListaRecaudoExterno(
                                    new EObjetoEnvio5ParametrosProto
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = request.decFiltro,
                                        strParametro1 = request.decFechaDiaInicio + "000000",
                                        strParametro2 = request.decFechaDiaFin + "235959",
                                        strParametro3 = request.decIdTipoActividad.ToString(),
                                        strParametro4 = request.decIdConsumidor.ToString(),
                                        strParametro5 = decidTipoOp.ToString(),
                                        strParametro6 = Convert.ToString((decimal)EnumTipoTransaccion.SrvSuic + "|" + (decimal)EnumTipoTransaccion.SrvDs3578),//decidTipoOp.ToString(),
                                    });
                                    oResp.LstaRecaudoExt = (List<O_RECAUDO_EXTERNO_CTY>)objRecaudo.oResultado;
                                }
                                break;
                            case "50":
                                request.decFiltro = 2;
                                var listaTransaccionTipo1 = _consulta.ListaTransaccionTipo(request);
                                oResp.LstaAdicional1 = (List<ObjDjTransaccion>)listaTransaccionTipo1.oResultado;
                                break;
                        }
                    }
                    resultado.oResultado = oResp;
                }
                else
                {
                    resultado.intCodigo = -1;
                    resultado.strMensaje = "No se encontraron registros.";
                    resultado.oResultado = null;
                }
            }
            else
            {
                resultado.intCodigo = -1;
                resultado.strMensaje = objResultado.strMensaje;
            }

            return resultado;
        }
       
        /// <summary>
        /// METODO. MListarDeclaracionJuradaAcumuladaD
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public EPResultado MListarDeclaracionJuradaAcumuladaD(EListarDeclaracionJuradaAcumuladaD request)
        {
            var resultado = new EPResultado();

            string strMensajeError = "";
            string strMensajeErrorF = "";
            string mensaje = "";
            bool valido = true;
            try
            {
                var declaracionJurada = new O_DECLARACION_JURADA_CTY();

                EPResultado listadoDeclaracionJuradaAcumulada = _consulta.ListaDeclaracionJuradaAcumulada(request);
                if (listadoDeclaracionJuradaAcumulada.intCodigo > 0)
                {
                    declaracionJurada.LstAcumulado = (List<CDjAcumulado>)listadoDeclaracionJuradaAcumulada.oResultado;
                    //var js = new JavaScriptSerializer();
                    //string strJsonListaDeclaracion = js.Serialize(declaracionJurada.LstAcumulado);
                    var listaTransaccionTipo = _consulta.ListaTransaccionTipo(request);
                    declaracionJurada.LstaAdicional2 = (List<ObjDjTransaccion>)listaTransaccionTipo.oResultado;

                    request.decFiltro = 2;
                    var listaTransaccionTipo1 = _consulta.ListaTransaccionTipo(request);
                    declaracionJurada.LstaAdicional1 = (List<ObjDjTransaccion>)listaTransaccionTipo1.oResultado;

                    if (declaracionJurada.LstAcumulado.Count > 0)
                    {
                        resultado.intCodigo = 1;
                        resultado.strMensaje = mensaje + "OK";
                        resultado.oResultado = declaracionJurada;
                    }
                    else
                    {
                        resultado.intCodigo = -1;
                        resultado.strMensaje = mensaje + "Faltan Datos...";
                        resultado.oResultado = declaracionJurada;
                    }

                    //CLogTraza.Informacion("RESULTADO: " + GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name
                    //                    + " ENTRADA: " + Newtonsoft.Json.JsonConvert.SerializeObject(request)
                    //                    + " SALIDA: " + Newtonsoft.Json.JsonConvert.SerializeObject(resultado),
                    //                    cParametrosHydro.decIdAplicacion,
                    //                    Convert.ToDecimal(CAppSettings.idUsuario),
                    //                    HttpContext.Current.Request.UserHostAddress);
                    return resultado;
                }
                else
                {
                    return listadoDeclaracionJuradaAcumulada;
                }

            }
            catch (Exception ex)
            {
                //CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                //oMensajeUsuario.decUsuarioId = Session[CVariablesSesion.IdUsuario];
                //oMensajeUsuario.decIdModulo = cParametrosHydro.decIdAplicacion;
                //oMensajeUsuario.strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                //oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
                //oMensajeUsuario.decNivelCapa = (int)CLogTraza.CapasNivel.Servicio;
                //CLogTraza.Error(oMensajeUsuario, ex);
                resultado.oResultado = null;
                resultado.intCodigo = -100;
                resultado.strMensaje = ex.Message;
                return resultado;
            }
        }

        /// <summary>
        /// METODO. MValidacionDdjj
        /// </summary>
        /// <param name="decFiltro"></param>
        /// <param name="strParametro1"></param>
        /// <param name="strParametro2"></param>
        /// <param name="strParametro3"></param>
        /// <param name="objAcumPreDec"></param>
        /// <returns></returns>
        public EPResultado MValidacionDdjj(decimal decFiltro, string strParametro1, string strParametro2, string strParametro3, List<CDjAcumulado> objAcumPreDec)
        {
            var resultado = new EPResultado();

            var objResultado = _consulta.RegistrosDdjj(
            new EObjetoEnvio3ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = decFiltro,
                strParametro1 = strParametro1,
                strParametro2 = strParametro2,
                strParametro3 = strParametro3
            });
            if (objResultado.intCodigo == 1)
            {
                var strObs = "";
                var objRes = (List<O_LISTA_REGISTROS_DDJJ_CTY>) objResultado.oResultado;
                if (objRes == null)
                {
                    strObs = "ERROR. VALIDACION 708 SIN REGISTROS";
                }
                else
                {
                    if (objRes.FirstOrDefault().DEC_TIPO == 0)
                    {
                        strObs = objRes.FirstOrDefault().STR_MENSAJE;
                    }
                    else if (objRes.FirstOrDefault().DEC_TIPO == 2)
                    {
                        var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(objRes.FirstOrDefault().JSON_REPORTE_DATOS);
                        var objAcumPost = objDdjj.LstAcumulado.Where(x => x.DecFecha == Convert.ToDecimal(objRes.FirstOrDefault().STR_MENSAJE.Substring(0, 8))).ToList();

                        var pred = objAcumPreDec.Sum(x => x.DecSaldoFinal);
                        var posd = objAcumPost.Sum(x => x.DecSaldoInicial);
                        if (pred != posd)
                        {
                            strObs = "ERROR. LOS SALDOS DEL PERIODO NO COINCIDEN CON LOS DEL PERIODO SIGUIENTE";
                        }
                        foreach (var reg in objAcumPreDec)
                        {
                            var obVal = objAcumPost.Where(x =>
                                        x.DecIdHidrocarburo == reg.DecIdHidrocarburo &&
                                        x.DecIdInfraestructura == reg.DecIdInfraestructura &&
                                        x.DecIdTipoInfraestructura == reg.DecIdTipoInfraestructura).ToList();
                            var regVal = obVal.FirstOrDefault();
                            if ((regVal == null) || (regVal != null && regVal.DecSaldoInicial != reg.DecSaldoFinal))
                            {
                                strObs = "ERROR. LOS SALDOS DEL PERIODO NO COINCIDEN CON LOS DEL PERIODO SIGUIENTE.";
                            }
                        }
                    }
                }
                if (strObs.Length > 0)
                {
                    resultado.intCodigo = -1;
                    resultado.strMensaje = strObs;
                }
                else
                {
                    resultado.intCodigo = 1;
                    resultado.strMensaje = "";
                }
            }
            else
            {
                resultado.intCodigo = -1;
                resultado.strMensaje = "ERROR. VALIDACION - " + objResultado.strMensaje;
            }
            return resultado;
        }

        /// <summary>
        /// METODO. MGrafico_Tipo1
        /// </summary>
        /// <param name="decTipo"></param>
        /// <param name="decAgrupacion"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdTiOp"></param>
        /// <param name="strIdentificador"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechFin"></param>
        /// <returns></returns>
        public EPResultado MGrafico_Tipo1(decimal decTipo, decimal decAgrupacion, string strIdActividad, string strIdTiOp, string strIdentificador, DateTime fechaIni, DateTime fechFin)
        {
            var resultado = new EPResultado();
            resultado.intCodigo = -1;
            resultado.strMensaje = "No existen registros.";
            var objRes = new O_GRAFICO_TIPO1();

            var fi = fechaIni.Year + fechaIni.Month.ToString().PadLeft(2, '0') + fechaIni.Day.ToString().PadLeft(2, '0') + "000000";
            var ff = fechFin.Year + fechFin.Month.ToString().PadLeft(2, '0') + fechFin.Day.ToString().PadLeft(2, '0') + "235959";
            var objResultado = _consulta.ObtenerAcumuladoGrafico(
            new EObjetoEnvio5ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = decTipo,
                strParametro1 = strIdActividad,
                strParametro2 = strIdTiOp,
                strParametro3 = strIdentificador,
                strParametro4 = fi,
                strParametro5 = ff
            });
            if (objResultado.intCodigo == 1)
            {
                var lstResultado = (List<O_OBTENER_ACUM_GRAFICO_CTY>)objResultado.oResultado;
                List<O_OBTENER_ACUM_GRAFICO_CTY> lstIdAgrupa;
                var series = new List<O_SERIES>();

                lstIdAgrupa = lstResultado
                      .GroupBy(a => a.ID_AGRUPA)
                      .Select(g => g.First())
                      .ToList();

                var td1 = fechaIni - fechFin;
                int intDif = td1.Days * -1;

                var names = new string[intDif + 1];
                var datFechaI = fechaIni;
                for (int i = 0; i <= intDif; i++)
                {
                    names[i] = datFechaI.ToShortDateString();
                    datFechaI = datFechaI.AddDays(1);
                }
                objRes.names = names;

                foreach (var regAgr in lstIdAgrupa)
                {
                    var objSerie = new O_SERIES();
                    objSerie.name = regAgr.AGRUPA;
                    var arrSerie = new double[intDif + 1];
                    datFechaI = fechaIni;
                    for (int i = 0; i <= intDif; i++)
                    {
                        var decFec = datFechaI.Year + datFechaI.Month.ToString().PadLeft(2, '0') + datFechaI.Day.ToString().PadLeft(2, '0');
                        var lstAux = lstResultado.Where(x => x.ID_AGRUPA == regAgr.ID_AGRUPA && x.PERIODO == decFec).ToList();
                        if (lstAux.Any())
                        {
                            arrSerie[i] = Convert.ToDouble(lstAux.FirstOrDefault().CANTIDAD);
                        }
                        else
                        {
                            arrSerie[i] = 0;
                        }
                        datFechaI = datFechaI.AddDays(1);
                    }
                    objSerie.data = arrSerie;

                    series.Add(objSerie);
                }
                objRes.series = series;
            }

            if (objRes.series != null && objRes.series.Count > 0)
            {
                resultado.intCodigo = 1;
                resultado.strMensaje = "";
                resultado.oResultado = objRes;
            }

            return resultado;
        }

        #endregion

        public String[] UNIDADES = { "", "un ", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        public String[] DECENAS = {"diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ",
        "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ",
        "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa "};
        public String[] CENTENAS = {"", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ",
        "setecientos ", "ochocientos ", "novecientos "};

        public Regex r;

        public String Convertir(String numero, bool mayusculas, string moneda = "PESOS")
        {

            String literal = "";
            String parte_decimal;
            //si el numero utiliza (.) en lugar de (,) -> se reemplaza
            numero = numero.Replace(".", ",");

            //si el numero no tiene parte decimal, se le agrega ,00
            if (numero.IndexOf(",") == -1)
            {
                numero = numero + ",00";
            }
            //se valida formato de entrada -> 0,00 y 999 999 999,00
            r = new Regex(@"\d{1,9},\d{1,2}");
            MatchCollection mc = r.Matches(numero);
            if (mc.Count > 0)
            {
                //se divide el numero 0000000,00 -> entero y decimal
                String[] Num = numero.Split(',');

                string MN = " M.N.";
                if (moneda != "PESOS")
                    MN = "";

                //de da formato al numero decimal
                parte_decimal = Num[1] + "/100" + MN + " " + moneda;
                //se convierte el numero a literal
                if (int.Parse(Num[0]) == 0)
                {//si el valor es cero
                    literal = "cero ";
                }
                else if (int.Parse(Num[0]) > 999999)
                {//si es millon
                    literal = getMillones(Num[0]);
                }
                else if (int.Parse(Num[0]) > 999)
                {//si es miles
                    literal = getMiles(Num[0]);
                }
                else if (int.Parse(Num[0]) > 99)
                {//si es centena
                    literal = getCentenas(Num[0]);
                }
                else if (int.Parse(Num[0]) > 9)
                {//si es decena
                    literal = getDecenas(Num[0]);
                }
                else
                {//sino unidades -> 9
                    literal = getUnidades(Num[0]);
                }
                //devuelve el resultado en mayusculas o minusculas
                if (mayusculas)
                {
                    return (literal + parte_decimal).ToUpper();
                }
                else
                {
                    return (literal + parte_decimal);
                }
            }
            else
            {//error, no se puede convertir
                return literal = null;
            }
        }

        /* funciones para convertir los numeros a literales */

        public String getUnidades(String numero)
        {   // 1 - 9
            //si tuviera algun 0 antes se lo quita -> 09 = 9 o 009=9
            String num = numero.Substring(numero.Length - 1);
            return UNIDADES[int.Parse(num)];
        }

        public String getDecenas(String num)
        {// 99
            int n = int.Parse(num);
            if (n < 10)
            {//para casos como -> 01 - 09
                return getUnidades(num);
            }
            else if (n > 19)
            {//para 20...99
                String u = getUnidades(num);
                if (u.Equals(""))
                { //para 20,30,40,50,60,70,80,90
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8];
                }
                else
                {
                    return DECENAS[int.Parse(num.Substring(0, 1)) + 8] + "y " + u;
                }
            }
            else
            {//numeros entre 11 y 19
                return DECENAS[n - 10];
            }
        }

        public String getCentenas(String num)
        {// 999 o 099
            if (int.Parse(num) > 99)
            {//es centena
                if (int.Parse(num) == 100)
                {//caso especial
                    return " cien ";
                }
                else
                {
                    return CENTENAS[int.Parse(num.Substring(0, 1))] + getDecenas(num.Substring(1));
                }
            }
            else
            {//por Ej. 099
                //se quita el 0 antes de convertir a decenas
                return getDecenas(int.Parse(num) + "");
            }
        }

        public String getMiles(String numero)
        {// 999 999
            //obtiene las centenas
            String c = numero.Substring(numero.Length - 3);
            //obtiene los miles
            String m = numero.Substring(0, numero.Length - 3);
            String n = "";
            //se comprueba que miles tenga valor entero
            if (int.Parse(m) > 0)
            {
                n = getCentenas(m);
                return n + "mil " + getCentenas(c);
            }
            else
            {
                return "" + getCentenas(c);
            }

        }

        public String getMillones(String numero)
        { //000 000 000
            //se obtiene los miles
            String miles = numero.Substring(numero.Length - 6);
            //se obtiene los millones
            String millon = numero.Substring(0, numero.Length - 6);
            String n = "";
            if (millon.Length > 1)
            {
                n = getCentenas(millon) + "millones ";
            }
            else
            {
                n = getUnidades(millon) + "millon ";
            }
            return n + getMiles(miles);
        }

        public string enLetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            
            return res;
        }
        
        private string toText(double value)
        {
            string Num2Text = "";

            value = Math.Truncate(value);


            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";


            else if (value == 2) Num2Text = "DOS";


            else if (value == 3) Num2Text = "TRES";


            else if (value == 4) Num2Text = "CUATRO";


            else if (value == 5) Num2Text = "CINCO";


            else if (value == 6) Num2Text = "SEIS";


            else if (value == 7) Num2Text = "SIETE";


            else if (value == 8) Num2Text = "OCHO";


            else if (value == 9) Num2Text = "NUEVE";


            else if (value == 10) Num2Text = "DIEZ";


            else if (value == 11) Num2Text = "ONCE";


            else if (value == 12) Num2Text = "DOCE";


            else if (value == 13) Num2Text = "TRECE";


            else if (value == 14) Num2Text = "CATORCE";


            else if (value == 15) Num2Text = "QUINCE";


            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);


            else if (value == 20) Num2Text = "VEINTE";


            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);


            else if (value == 30) Num2Text = "TREINTA";


            else if (value == 40) Num2Text = "CUARENTA";


            else if (value == 50) Num2Text = "CINCUENTA";


            else if (value == 60) Num2Text = "SESENTA";


            else if (value == 70) Num2Text = "SETENTA";


            else if (value == 80) Num2Text = "OCHENTA";


            else if (value == 90) Num2Text = "NOVENTA";


            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);


            else if (value == 100) Num2Text = "CIEN";


            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);


            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";


            else if (value == 500) Num2Text = "QUINIENTOS";


            else if (value == 700) Num2Text = "SETECIENTOS";


            else if (value == 900) Num2Text = "NOVECIENTOS";


            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);


            else if (value == 1000) Num2Text = "MIL";


            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);


            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }


            else if (value == 1000000) Num2Text = "UN MILLON";


            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);


            else if (value < 1000000000000)
            {


                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";


                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);


            }


            else if (value == 1000000000000) Num2Text = "UN BILLON";


            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);


            else


            {


                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";


                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);


            }


            return Num2Text;


        }

        public string mesLetras(string num)
        {
            var mesLiteral = "";
            if (num == "1"){
                mesLiteral = "Enero";
            }
            else if (num == "2"){
                mesLiteral = "Febrero";
            }
            else if (num == "3"){
                mesLiteral = "Marzo";
            }
            else if (num == "4"){
                mesLiteral = "Abril";
            }
            else if (num == "5"){
                mesLiteral = "Mayo";
            }
            else if (num == "6"){
                mesLiteral = "Junio";
            }
            else if (num == "7"){
                mesLiteral = "Julio";
            }
            else if (num == "8"){
                mesLiteral = "Agosto";
            }
            else if (num == "9"){
                mesLiteral = "Septiembre";
            }
            else if (num == "10"){
                mesLiteral = "Octubre";
            }
            else if (num == "11"){
                mesLiteral = "Novimebre";
            }
            else if (num == "12"){
                mesLiteral = "Diciembre";
            }
            
            return mesLiteral;
        }
    }
}