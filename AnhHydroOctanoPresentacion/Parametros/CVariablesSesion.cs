using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using AnhHydroOctanoPresentacion.Entidades;

namespace AnhHydroOctanoPresentacion.Parametros
{
    using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
    public static class CVariablesSesion
    {
        #region Variables de Usuario
        public static readonly string UsuarioId = "UsuarioId";
        public static readonly string UsuarioPerfil = "UsuarioPerfil";
        public static readonly string UsuarioPerfilId = "UsuarioPerfilId";
        public static readonly string UsuarioLogin = "UsuarioLogin";
        public static readonly string ListaPerfiles = "ListaPerfiles";
        public static readonly string IdentificadorEntidad = "IdentificadorEntidad";
        public static readonly string NombreEntidad = "NombreEntidad";

        public static readonly string ListaAlertaSaldoDiario = "ListaAlertaSaldoDiario";

        public static readonly string NombreDireccion = "NombreDireccion";
        
        public static readonly string IdEntidadPadre = "IdEntidadPadre";
        public static readonly string IsAdminTransporte = "IsAdminTransporte";
        public static readonly string IsAdminDteyp = "IsAdminDteyp";
       
        public static readonly string UsuarioAdministrador = "UsuarioAdministrador";
        #endregion

        #region Variables Autenticacion

        public static readonly string UsuarioNombre = "UsuarioNombre";
        public static readonly string UsuarioPassword = "UsuarioPassword";
        public static readonly string UsuarioNombreCompleto = "UsuarioNombreCompleto";
        public static readonly string DatosUsuario = "DatosUsuario";
        public static readonly string PerfilUsuario = "PerfilUsuario";
        public static readonly string PerfilesUsuario = "PerfilesUsuario";
        public static readonly string Aux = "Aux";
        public static readonly string Opcion = "Opcion";
        public static readonly string ValidarCi = "ValidarCI";
        public static readonly string DatosBusqueda = "DatosBusqueda";
        public static readonly string Mail = "mail";
        public static readonly string CI = "CI";
        public static readonly decimal IdFuncionarioAnh = Convert.ToDecimal(ConfigurationManager.AppSettings["IdFuncionarioAnh"]);
        public static readonly decimal IdRegulado = Convert.ToDecimal(ConfigurationManager.AppSettings["IdRegulado"]);

        #endregion

        #region Variables ID

        public static readonly string IdUsuario = "IdUsuario";
        public static readonly string IdEntidad = "IdEntidad";
        public static readonly string IdEntidadRevision = "IdEntidadRevision"; //funciona como id de entidad para revision de dj
        public static readonly string IdSucursal = "IdSucursal"; //funciona como auxiliar de entidad para sucursales
        public static readonly string IdFuncionario = ConfigurationManager.AppSettings["IdFuncionario"];
        public static readonly string IdRepresentante = "IdRepresentante";
        public static readonly string IdActividadEntidad = "IdActividadEntidad";
        public static readonly string IdActividad = "IdActividad";
        public static readonly string IdCategoriaActividad = "IdCategoriaActividad";
        public static readonly string IdSociedad = "IdSociedad";
        public static readonly string IdDocumentoEntidad = "IdDocumentoEntidad";
        public static readonly string IdTramiteCambioRepresentante = "IdTramiteCambioRepresentante";
        public static readonly string IdAutenticacion = "HNA_oirausU";

        #endregion

        #region Variables Documentacion

        public static readonly string DocumentoId = "DocumentoId";

        #endregion

        #region Variables Correspondencia

        public static readonly string CorrespondenciaId = "CorrespondenciaId";
        public static readonly string CorrespondenciaModalidad = "CorrespondenciaModalidad";
        public static readonly string CorrespondenciaOpcion = "CorrespondenciaOpcion";
        public static readonly string CorrespondenciaTipoEnvio = "CorrespondenciaTipoEnvio";
        public static readonly string CorrespodenciaCite = "CorrespondenciaCite";
        public static readonly string CorrespondenciaIdCliente = "id";

        #endregion

        #region Variables Siscondoc

        public static readonly string SiscondocId = "SiscondocId";
        public static readonly string SiscondoValido = "SiscondocValido";

        #endregion

        #region Variables Correo

        public static readonly string CorreoContenido = "CorreoContenido";
        public static readonly string CorreoEnviado = "CorreoEnviado";

        //public static readonly string LinkCrearCuenta = ConfigurationManager.AppSettings["LinkCrearCuenta"].ToString();
        //public static readonly string LinkSireHydro = ConfigurationManager.AppSettings["LinkSireHydro"].ToString();
        #endregion

        #region Octano Volumenes

        public static readonly string FechaHoraServ = "FechaHoraServ";
        public static readonly string EntidadSeleccionada = "EntidadSeleccionada";

        public static readonly string Callback = "Callback";

        public static readonly string EstructuraArbol = "EstructuraArbol";
        public static readonly string ObjetoArbol = "ObjetoArbol";
        public static readonly string ObjetoEntidad = "ObjetoEntidad";        
        public static readonly string ObtenerEntidades = "ObtenerEntidades";
        public static readonly string GrillaTransaccion = "GrillaTransaccion";
        public static readonly string GrillaTransCisMas = "GrillaTransCisMas";
        public static readonly string GrillaRegObservado = "GrillaRegObservado";
        public static readonly string GrillaAcumulado = "GrillaAcumulado";
        public static readonly string GrillaInternacional = "GrillaInternacional";
        public static readonly string GrillaRecaudo = "GrillaRecaudo";
        public static readonly string GrillaRefineria = "GrillaRefineria";
        public static readonly string GrillaTransporteDucto = "GrillaTransporteDucto";
        public static readonly string GrillaRedesGas = "GrillaRedesGas";
        public static readonly string GrillaTransPlantilla = "GrillaTransPlantilla";

        public static readonly string ListaProyectoProducto = "ListaProyectoProducto";
        public static readonly string VolumenTotalCsv = "VolumenTotalCsv";
        public static readonly string PrimerDiaSemana = "PrimerDiaSemana";
        public static readonly string ProdeProgramado = "ProdeProgramado";
        public static readonly string ProgramadoActividadPDE = "ProgramadoActividadPDE";
        public static readonly string ValorProde = "ValorProde";
        public static readonly string ValorRecepcion = "ValorRecepcion";
        public static readonly string ValorVenta = "ValorVenta";
        public static readonly string LstIdConsumidorIdActividad = "LstIdConsumidorIdActividad"; //add 09-02-2023
        public static readonly string ListaLectorRFID = "ListaLectorRFID";
        public static readonly string ImgMTR = "ImgMTR"; 

        //importadores
        public static readonly string GrillaFormulario1Rubro3 = "GrillaFormulario1Rubro3";
        public static readonly string GrillaFormulario1Rubro4 = "GrillaFormulario1Rubro4";
        public static readonly string GrillaFormulario2Rubro2 = "GrillaFormulario2Rubro2";
        public static readonly string GrillaFormulario2Rubro3 = "GrillaFormulario2Rubro3";
        public static readonly string GrillaFormulario2Rubro4 = "GrillaFormulario2Rubro4";
        public static readonly string GrillaFormulario2Rubro5 = "GrillaFormulario2Rubro5";

        public static readonly string GrillaCostoTotalFosil = "GrillaCostoTotalFosil";

        public static readonly string GrillaCostoUnitarioSub = "GrillaCostoUnitarioSub";
        public static readonly string GrillaCostoUniSubvencion1 = "GrillaCostoUniSubvencion1";
        public static readonly string GrillaCostoUniSubvencion2 = "GrillaCostoUniSubvencion2";

        public static readonly string GrillaAnexoA = "GrillaAnexoA";
        public static readonly string GrillaAnexoAnGb = "GrillaAnexoAnGb";
        public static readonly string GrillaAnexoB1 = "GrillaAnexoB1";
        public static readonly string GrillaAnexoB1Gb = "GrillaAnexoB1Gb";
        public static readonly string GrillaAnexoB2 = "GrillaAnexoB2";
        public static readonly string GrillaAnexoB2GbD = "GrillaAnexoB2GbD";
        public static readonly string GrillaAnexoB2Gbc = "GrillaAnexoB2Gbc";
        public static readonly string GrillaAnexoB3 = "GrillaAnexoB3";
        public static readonly string GrillaAnexoC = "GrillaAnexoC";
        public static readonly string GrillaAnexoD1 = "GrillaAnexoD1";
        public static readonly string GrillaAnexoD1Gb = "GrillaAnexoD1Gb";
        public static readonly string GrillaAnexoD2 = "GrillaAnexoD2";
        public static readonly string GrillaAnexoD2Gb = "GrillaAnexoD2Gb";
        public static readonly string GrillaAnexoD3 = "GrillaAnexoD3";
        public static readonly string GrillaAnexoD3Gb = "GrillaAnexoD3Gb";
        public static readonly string GrillaAnexoE = "GrillaAnexoE";
        public static readonly string GrillaAnexoF1 = "GrillaAnexoF1";
        public static readonly string GrillaAnexoF2 = "GrillaAnexoF2";
        public static readonly string GrillaAnexoG1 = "GrillaAnexoG1";
        public static readonly string GrillaAnexoG1Gb = "GrillaAnexoG1Gb";
        public static readonly string GrillaAnexoG2 = "GrillaAnexoG2";
        public static readonly string GrillaAnexoG2Gb = "GrillaAnexoG2Gb";
        public static readonly string GrillaAnexoH = "GrillaAnexoH";
        public static readonly string GrillaAnexoHnGb = "GrillaAnexoHnGb";
        public static readonly string GrillaAnexoI1 = "GrillaAnexoI1";
        public static readonly string GrillaAnexoI2 = "GrillaAnexoI2";
        public static readonly string GrillaAnexoIGb = "GrillaAnexoIGb";
        public static readonly string GrillaAnexoIGbase = "GrillaAnexoIGbase";

        public static readonly string GrillaResumenAnexoA = "GrillaAnexoA";
        public static readonly string GrillaResumenAnexoB1 = "GrillaAnexoB1";
        public static readonly string GrillaResumenAnexoB2 = "GrillaAnexoB2";
        public static readonly string GrillaResumenAnexoB3 = "GrillaAnexoB3";
        public static readonly string GrillaResumenAnexoC = "GrillaAnexoC";
        public static readonly string GrillaResumenAnexoD1 = "GrillaAnexoD1";
        public static readonly string GrillaResumenAnexoD2 = "GrillaAnexoD2";
        public static readonly string GrillaResumenAnexoD3 = "GrillaAnexoD3";
        public static readonly string GrillaResumenAnexoE = "GrillaAnexoE";
        public static readonly string GrillaResumenAnexoF1 = "GrillaAnexoF1";
        public static readonly string GrillaResumenAnexoF2 = "GrillaAnexoF2";
        public static readonly string GrillaResumenAnexoG1 = "GrillaAnexoG1";
        public static readonly string GrillaResumenAnexoG2 = "GrillaAnexoG2";
        public static readonly string GrillaResumenAnexoH = "GrillaAnexoH";
        public static readonly string GrillaResumenAnexoI1 = "GrillaAnexoI1";
        public static readonly string GrillaResumenAnexoI2 = "GrillaAnexoI2";


        public static readonly string txtDuiRegularizadaF2 = "txtDuiRegularizadaF2";
        public static readonly string txtFechaDuiF2 = "txtFechaDuiF2";
        public static readonly string txtFronteraIngresoF2 = "txtFronteraIngresoF2";
        public static readonly string txtIncontermsF2 = "txtIncontermsF2";
        public static readonly string txtPrecioGasolinaBlancaF2 = "txtPrecioGasolinaBlancaF2";
        public static readonly string txtPrecioNetoIvaF2 = "txtPrecioNetoIvaF2";
        public static readonly string txtTarifaPoliductoF2 = "txtTarifaPoliductoF2";
        public static readonly string txtTarifaNetoIvaF2 = "txtTarifaNetoIvaF2";

        //GLP

        public static readonly string txtNombreCompletoGlp = "txtNombreCompletoGlp";
        public static readonly string txtDocIdentidadPersonaGlp = "txtDocIdentidadPersonaGlp";

        public static readonly string Nombres = "Nombres";
        public static readonly string PrimerApellido = "PrimerApellido";
        public static readonly string SegundoApellido = "SegundoApellido";
        public static readonly string Complemento = "Complemento";
        public static readonly string NumeroDocumento = "NumeroDocumento";
        public static readonly string FechaNacimiento = "FechaNacimiento";


        //importadores

        public static readonly string GrillaFosil = "GrillaFosil";
        public static readonly string Grillabase = "Grillabase";
        public static readonly string GrillaGasolinaBlanca = "GrillaGasolinaBlanca";
        public static readonly string GrillaOtro = "GrillaOtro";

        public static readonly string GrillaCabecera = "GrillaCabecera";
        public static readonly string GrillaVentaHora = "GrillaVentaHora";
        public static readonly string GrillaDetalle = "GrillaDetalle";
        public static readonly string GrillaHistorial = "GrillaHistorial";
        public static readonly string GrillaAmpliacion = "GrillaAmpliacion";
        public static readonly string GrillaIntimacion = "GrillaIntimacion";
        public static readonly string GrillaAuxiliar = "GrillaAuxiliar";
        public static readonly string GrillaListaAuxiliar = "GrillaListaAuxiliar";

        public static readonly string GrillaAcumRep = "GrillaAcumRep";
        public static readonly string GrillaTitulo = "GrillaTitulo";

        public static readonly string ComboTipoOperacion = "ComboTipoOperacion";
        public static readonly string ComboTiposOperacion = "ComboTiposOperacion";
        public static readonly string ComboTipoConsumidor = "ComboTipoConsumidor";
        public static readonly string ComboGenerico = "ComboGenerico";
        public static readonly string ListaParametrica = "ListaParametrica";
        public static readonly string ListaSectorEconomico = "ListaSectorEconomico";

        public static readonly string ListaTransaccion = "ListaTransaccion";

        public static readonly string ListaPoi = "ListaPoi";

        public static readonly string DjAcumulado = "DjAcumulado";
        public static readonly string DjFacturas = "DjFacturas";

        public static readonly string AuxValidaLlave = "AuxValidaLlave";
        public static readonly string IdAuxiliar = "IdAuxiliar";
        public static readonly string StrAuxiliar = "StrAuxiliar";
        public static readonly string ParamAux = "ParamAux";

        public static readonly string TipoReporte = "TipoReporte";
        public static readonly string AuxActualiza = "AuxActualiza";

        public static readonly string ReportePrincipal = "ReportePrincipal";
        public static readonly string ReporteSecundario = "ReporteSecundario";
        public static readonly string ReporteTercero = "ReporteTercero";
        public static readonly string ReporteCuarto = "ReporteCuarto";

        public static readonly string ArchivoDescarga = "ArchivoDescarga";
        public static readonly string ListaParamGral = "ListaParamGral";
        public static readonly string OctanoParametrica = "OctanoParametrica";

        public static readonly string ListaCatalogo = "ListaCatalogo";

        public static readonly string ListaPoiRecepcion = "ListaPoiRecepcion";
        public static readonly string ListaPoiEntrega = "ListaPoiEntrega";
        public static readonly string ListaSistemaDucto = "ListaSistemaDucto";
        public static readonly string ListaTipoTransporte = "ListaTipoTransporte";
        public static readonly string ListaTipoConsumidor = "ListaTipoConsumidor";

        public static readonly string ListaPeriodos = "ListaPeriodos";
        public static readonly string IdPeriodoActual = "IdPeriodoActual";

        public static readonly string ListaInfraestructura = "ListaInfraestructura";
        public static readonly string ListaEntidad = "ListaEntidad";
        public static readonly string ListaParamInfra = "ListaParamInfra";

        public static readonly string GridCabecera = "GridCabecera";

        public static readonly string ListaUsuarios = "ListaUsuarios";
        public static readonly string ListaUsuariosModificarPermiso = "ListaUsuariosModificarPermiso";
        public static readonly string ListaUsuariosReplica = "ListaUsuariosReplica";
        public static readonly string ListaUsuariosEliminarPermiso = "ListaUsuariosEliminarPermiso";
        public static readonly string ListaUsuariosNewReplica = "ListaUsuariosNewReplica";
        public static readonly string ListaEntidades = "ListaEntidades";

        public static readonly string HabilitaFunciones = "HabilitaFunciones";
        public static readonly string HabilitaBotones = "HabilitaBotones";

        public static readonly string HabBotonesSaldoFisico = "HabBotonesSaldoFisico";    

        public static readonly string IdMovDdjj = "IdMovDdjj";
        public static readonly string ListaDepartamentos = "ListaDepartamentos";
        public static readonly string ListaActividades = "ListaActividades"; 
        public static readonly string TipoOperacion = "TipoOperacion";       

        #endregion
        #region variables sesion reporte refinerias
        public static readonly string listaReporteSession = "listaReporteSession";

#endregion
        #region Variables Autenticacion Nuevo
        //public static readonly string IdAutenticacion = "HNA_oirausU";
        //public static readonly string IdEntidad = "IdEntidad";
        public static readonly string ObjUsuario = "ObjUsuario";
        public static readonly string ObjFuncionarioANH = "ObjFuncionarioANH";
        public static readonly string objPerfilesUsauario = "objPerfilesUsauario";

        #endregion

        #region Permisos
        public static readonly string PermisoCons = "PermisoCons";
        public static readonly string PermisoAlta = "PermisoAlta";
        public static readonly string PermisoBaja = "PermisoBaja";
        public static readonly string PermisoModi = "PermisoModi";
        public static readonly string PermisoApro = "PermisoApro";
        public static readonly string PermisoRevi = "PermisoRevi";
        #endregion


        #region Metodos para obtener valor de objetos
        public static O_USUARIO_CTY UsuarioSession(HttpContext contexto)
        {
            if (contexto.Session[ObjUsuario] != null)
            {
                return (O_USUARIO_CTY)contexto.Session[ObjUsuario];
            }
            else
            {
                //contexto.Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Sitio/Persona/wfSalir.aspx");
            }
            return null;
        }

        public static O_DATOS_FUNCIONARIO_CTY FuncionarioSesionAnh(HttpContext contexto)
        {
            if (contexto.Session[ObjFuncionarioANH] != null)
            {
                return (O_DATOS_FUNCIONARIO_CTY)contexto.Session[ObjFuncionarioANH];
            }
            else
            {
                //contexto.Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
                DevExpress.Web.ASPxWebControl.RedirectOnCallback("~/Sitio/Persona/wfSalir.aspx");
            }
            return null;
        }
        #endregion
    }    
}