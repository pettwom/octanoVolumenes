using System;
using System.Collections.Generic;
using AnhHydroOctanoAgenteServicio.Aplicacion.Consultas;
using AnhHydroOctanoAgenteServicio.ServicioHydroListados;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Clases.Controles;

namespace AnhHydroOctanoPresentacion.Entidades
{
    public class EResultadoMovimiento
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_MOVIMIENTO_PROD_CTY> OResultado { get; set; }
    }

    public class EResultadoAuxiliar
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_BARCODE_HISTORIAL_CTY> oResultado { get; set; }
    }
    public class EResultadoAuxiliar1
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_BARCODE_CXC_CTY> oResultado { get; set; }
    }

    public class EResultadoMovimientoVista
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_CONSULTA_MOVI_TRAN_CTY> OResultado { get; set; }
    }

    public class EResultadoParametrica
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public O_OBTENER_CATALOGOS_CTY OResultado { get; set; }
    }
    public class EResultadoSectorEconomico
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_SECTOR_ECONOMICO_CTY> OResultado { get; set; }
    }
    public class EResultadoTipoTransaccion
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_TIPO_TRANSACCION_CTY> OResultado { get; set; }
    }
    public class EResultadoUbicacion
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_UBICACION_CTY> OResultado { get; set; }
    }
    public class EResultadoFeHoServ
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_FECHA_HORA_ACTUAL_CTY> OResultado { get; set; }
    }
    public class EResultadoPeriodos
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_PERIODO_CTY> OResultado { get; set; }
    }
    public class EResultadoProducto
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_PRODUCTOS_CTY> OResultado { get; set; }
    }
    public class EResultadoParametricaGlobal
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_PARAMETRICA_GLOBAL_CTY> OResultado { get; set; }
    }

    public class EResultadoEntidad
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_OBTIENE_ENTIDADES_USR_CTY> OResultado { get; set; }
        //public List<O_OBTIENE_ENTIDADES_CTY> OResultado { get; set; }
    }

    public class EResultadoBsisa
    {
        public int decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_LLAVE_MIDDLEWARE_CTY> oResultado { get; set; }
    }

    public class EResultadoInfraestructura
    {
        public int decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_INFRAESTRUCTURA_VOL_CTY> oResultado { get; set; }
    }

    public class EResultadoTransaccion
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_LISTA_TRANSACCION_CTY> oResultado { get; set; }
    }
    public class EResultadoFactura
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_FACTURA_CTY> OResultado { get; set; }
    }

    public class EResultadoDocumento
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_AUTORIZACION_MOV_CTY> OResultado { get; set; }
    }

    public class EResultadoDjAcumuado
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_DECLARACION_CTY> OResultado { get; set; }
    }

    public class EResultadoCumplimientoMes
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_REPORTE_MENSUAL_CTY> OResultado { get; set; }
    }

    public class EResultadoAutCons
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_AUTORIZACION_CONS_CTY> OResultado { get; set; }
    }
    public class EResultadoHistorialCons
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_BITACORA_CTY> OResultado { get; set; }
    }

    public class EResultadoTipoOperacion
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_TIPOS_OPE_ACT_CTY> OResultado { get; set; }
    }

    public class EResultadoListarUsuario
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTADO_USUARIOS_CTY> OResultado { get; set; }
    }

    public class EResultadoReporteVariable
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_REPORTE_VARIABLES_CTY> OResultado { get; set; }
    }

    public class EResultadoConfigTipoOperacion
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_AUTORIZACION_ACCI_CTY> OResultado { get; set; }
    }
    public class EResultadoConfigHidrocarburo
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_AUTORIZACION_COND_CTY> OResultado { get; set; }
    }
    public class EResultadoOctanoParametro
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_OCTANO_PAR_CTY> OResultado { get; set; }
    }
    public class EResultadoObtenerDj
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public O_DECLARACION_JURADA_CTY OResultado { get; set; }
    }
    public class EResultadoCumplimientoDia
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_CUMPLIMIENTO_DIA_CTY> OResultado { get; set; }
    }

    public class EResultadoListadoDj
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_REPORTE_DATOSP_CTY> OResultado { get; set; }
    }

    public class EResultadoListadoHistorialDj
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_HISTORIAL_REPORTE_DATOSP_CTY> OResultado { get; set; }
    }

    public class EResultadoAdjuntos
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_LISTA_DOCUMENTO_DIGITAL_CTY> OResultado { get; set; }
    }

    public class EResultadoGenerico
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public O_RESULTADO_CTY OResultado { get; set; }
    }

    public class EResultadoInfoHidro
    {
        public int Resultado { get; set; }
        public List<InformacionHydro> InformacionHydro { get; set; }
    }

    public class EResultadoParamInfra
    {
        public int decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_PARAMETROS_CTY> oResultado { get; set; }
    }

    public class EResultadoTipoContenedor
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_LISTADO_TIPOS_COTENEDOR_CTY> oResultado { get; set; }
    }

    public class EResultadoEntidades
    {
        public int decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<CListaEntidades> oResultado { get; set; }
    }
    public class EResultado<T>
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public T oResultado { get; set; }
    }
    public class EResultadoLista<T>
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<T> oResultado { get; set; }
    }
    public class EResultadoInt<T>
    {
        public decimal intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<T> oResultado { get; set; }
    }

    public class EResultadoSaldoIni
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_SALDO_INICIAL_DIA_CTY> oResultado { get; set; }
    }
    public class EResultadoMovAcum
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_OBTENER_MOVIMIENTO_ACUM_CTY> oResultado { get; set; }
    }
    public class EResultadoTiOpAcum
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_OBTENER_TIPO_OPE_ACUM_CTY> oResultado { get; set; }
    }


    public class O_DATOS_FUNCIONARIO_CTY
    {
        public decimal ID_PERSONA { get; set; }
        public decimal ID_FUNCIONARIO { get; set; }
        public string NOMBRES { get; set; }
        public string P_APELLIDO { get; set; }
        public string S_APELLIDO { get; set; }
        public string UNIDAD { get; set; }
        public string CARGO { get; set; }
        public Nullable<decimal> ID_ESCALA_SALARIAL { get; set; }
        public string CONDICION { get; set; }
        public Nullable<decimal> ITEM { get; set; }
        public string ESTADO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string DENOMINACION_NIVEL { get; set; }
        public Nullable<decimal> ID_PUESTO { get; set; }
        public Nullable<decimal> ID_ORGANIGRAMA { get; set; }
        public Nullable<decimal> VALIDADO { get; set; }
        public string DIRECCION { get; set; }
        public string CATEGORIA_NIVEL { get; set; }
        public Nullable<decimal> ID_MOVILIDAD_ORIGEN { get; set; }
        public Nullable<decimal> LUGAR_TRABAJO_ID { get; set; }
        public string NIVEL_LUGAR_TRABAJO { get; set; }
        public Nullable<System.DateTime> FECHA_ULT_INCORP { get; set; }
        public Nullable<decimal> FOTO_DIGITAL_ID { get; set; }
        public string USUARIO_DOMINIO { get; set; }
    }
    public class O_ARCHIVO_DIGITAL_CTY
    {
        public decimal ID_ARCHIVO_DIGITAL { get; set; }
        public byte[] ARCHIVO_BINARIO { get; set; }
        public string NOMBRE_ARCH { get; set; }
        public string EXTENSION_ARCH { get; set; }
        public string OBSERVACION { get; set; }
    }

    public class O_AASANA_REGISTRO
    {
        public decimal idmovfic { get; set; }
        public string idavion { get; set; }
        public decimal fecha_origen { get; set; }
        public decimal fecha_despegue { get; set; }
        public decimal fecha_arribo { get; set; }
        public string procedencia { get; set; }
        public string destino { get; set; }
        public decimal horadespegue { get; set; }
        public decimal horaarribo { get; set; }
        public string habilitado { get; set; }
        public string ruta { get; set; }
        public decimal fhLeido { get; set; }
        public string leido { get; set; }
        public string observacionesRuta { get; set; }
        //Avion
        public string idempresa { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        public string observacionesAvion { get; set; }
        //Persona
        public string idcontrolador { get; set; }
        public string ci { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string observacionesPersona { get; set; }
    }

    public class O_AASANA_PVUELO
    {
        public decimal idTabla { get; set; }
        public decimal idPlanVuelo { get; set; }
        public string noVuelo { get; set; }
        public string modelo { get; set; }
        public string origen { get; set; }
        public string rutaGps { get; set; }
        public string destino { get; set; }
        public string hraAproxDestino { get; set; }
        public string aeroAlternativo { get; set; }
        public string matricula { get; set; }
        public string fhRegistro { get; set; }
        public decimal fechaRegistro { get; set; }
    }
    public class O_AASANA_MOVIMIENTO
    {
        public decimal idmovfic { get; set; }
        public string idavion { get; set; }
        public string fecha_origen { get; set; }
        public string procedencia { get; set; }
        public string destino { get; set; }
        public string fecha_despegue { get; set; }
        public string fecha_arribo { get; set; }
        public string horadespegue { get; set; }
        public string horarribo { get; set; }
        public string ruta { get; set; }
        public string idcontrolador { get; set; }
        public string habilitado { get; set; }
        public string obs { get; set; }
        public string leido { get; set; }
        public string fhleido { get; set; }
        public string nrovuelo { get; set; }
        public string matriculadgac { get; set; }
    }
    public class O_AASANA_AEROPUERTO
    {
        public string idaeropuerto { get; set; }
        public string nombre { get; set; }
        public string region { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string tipo { get; set; }
        public string observacionesAeropuerto { get; set; }
    }
    public class O_AASANA_EMPRESA
    {
        public string idempresa { get; set; }
        public string propietario { get; set; }
        public string nacionalidad { get; set; }
        public string tipo { get; set; }
        public string obs { get; set; }
    }
    public class O_AASANA_CONTROLADOR
    {
        public string idcontrolador { get; set; }
        public string ci { get; set; }
        public string nombre { get; set; }
        public string paterno { get; set; }
        public string materno { get; set; }
        public string obs { get; set; }
    }
    public class O_AASANA_AVION
    {
        public string idavion { get; set; }
        public string idempresa { get; set; }
        public string modelo { get; set; }
        public string tipo { get; set; }
        public string obs { get; set; }
    }

    public class EResAasanaRa
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_REGISTRO> oResultado { get; set; }
        public string strMensaje { get; set; }
    }
    public class EResAasanaRu
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_MOVIMIENTO> oResultado { get; set; }
        public string strMensaje { get; set; }
    }
    public class EResAasanaAv
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_AVION> oResultado { get; set; }
        public string strMensaje { get; set; }
    }
    public class EResAasanaAe
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_AEROPUERTO> oResultado { get; set; }
        public string strMensaje { get; set; }
    }
    public class EResAasanaEm
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_EMPRESA> oResultado { get; set; }
        public string strMensaje { get; set; }
    }
    public class EResAasanaCo
    {
        public decimal decCodigo { get; set; }
        public List<O_AASANA_CONTROLADOR> oResultado { get; set; }
        public string strMensaje { get; set; }
    }

    /*public class O_DGAC_AVION
    {
        public decimal nroCorrelativo { get; set; }
        public string nroMatricula { get; set; }
        public string nroSerie { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string fechaInscripcion { get; set; }
        public string propietario { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string observaciones { get; set; }
        public decimal volumenReferencial { get; set; }
        public decimal decFechaInscripcion { get; set; }
    }*/

    public class O_DGAC_AVION
    {
        public string nroMatricula { get; set; }
        public string nroSerie { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string propietario { get; set; }
        public string explotador { get; set; }
        public string fechaActualizacion { get; set; }
    }

    /*public class O_DGAC_PILOTO
    {
        public string ciudad { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string nombre { get; set; }
        public string nroLicencia { get; set; }
        public string titulo { get; set; }
        public string vigencia { get; set; }
        public decimal decVigencia { get; set; }
    }*/

    public class O_DGAC_PILOTO
    {
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string nombre { get; set; }
        public string fechaNacimiento { get; set; }
        public string nacionalidad { get; set; }
        public string nroLicencia { get; set; }
        public string titulo { get; set; }
        public string Habilitación { get; set; }
        public string PIC { get; set; }
        public string SIC { get; set; }
        public string vigencia { get; set; }
    }

    public class O_LISTA_DGAC_AERONAVE_CTY
    {
        public decimal ID_DGAC_AERONAVE { get; set; }
        public decimal NRO_CORRELATIVO { get; set; }
        public string NRO_MATRICULA { get; set; }
        public string NRO_SERIE { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string FECHA_INSCRIPCION { get; set; }
        public string PROPIETARIO { get; set; }
        public string CIUDAD { get; set; }
        public string ESTADO { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal VOLUMEN_REFERENCIAL { get; set; }
        public decimal DEC_FECHA_INSCRIPCION { get; set; }
        public decimal ID_ESTADO { get; set; }
        public decimal ID_PROPIETARIO { get; set; }
        public decimal ID_MARCA { get; set; }
        public decimal ID_MODELO { get; set; }
        public decimal ID_CIUDAD { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
    }

    public class O_LISTA_DGAC_PILOTO_CTY
    {
        public decimal ID_DGAC_PILOTO { get; set; }
        public string NRO_LICENCIA { get; set; }
        public string NOMBRE { get; set; }
        public string AP_PATERNO { get; set; }
        public string AP_MATERNO { get; set; }
        public string CIUDAD { get; set; }
        public string TITULO { get; set; }
        public string VIGENCIA { get; set; }
        public decimal DEC_VIGENCIA { get; set; }
        public decimal ID_ESTADO { get; set; }
        public string CI_NUMERO { get; set; }
        public string CI_COMPLEMENTO { get; set; }
        public decimal ID_CIUDAD { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
    }

    public class O_LISTA_AASANA_AEROPUERTO_CTY
    {
        public decimal ID_AASANA_AEROPUERTO { get; set; }
        public string ID_AEROPUERTO { get; set; }
        public string NOMBRE { get; set; }
        public string REGION { get; set; }
        public string PAIS { get; set; }
        public string CIUDAD { get; set; }
        public string TIPO { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal ID_REGION { get; set; }
        public decimal ID_PAIS { get; set; }
        public decimal ID_CIUDAD { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
    }

    public class O_LISTA_AASANA_CONTROLADOR_CTY
    {
        public decimal ID_AASANA_CONTROLADOR { get; set; }
        public string ID_CONTROLADOR { get; set; }
        public string CI { get; set; }
        public string NOMBRE { get; set; }
        public string AP_PATERNO { get; set; }
        public string AP_MATERNO { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
    }

    public class O_LISTA_AASANA_MOVFIC_CTY
    {
        public decimal ID_AASANA_MOV_AERONAVE { get; set; }
        public decimal ID_MOVFIC { get; set; }
        public string ID_AVION { get; set; }
        public string FECHA_ORIGEN { get; set; }
        public string PROCEDENCIA { get; set; }
        public string DESTINO { get; set; }
        public string FECHA_DESPEGUE { get; set; }
        public string FECHA_ARRIBO { get; set; }
        public string HORA_DESPEGUE { get; set; }
        public string HORA_ARRIBO { get; set; }
        public string RUTA { get; set; }
        public string ID_CONTROLADOR { get; set; }
        public string HABILITADO { get; set; }
        public string NRO_VUELO { get; set; }
        public string OBSERVACION { get; set; }
        public string LEIDO { get; set; }
        public string FHLEIDO { get; set; }
        public string NROVUELO { get; set; }
        public decimal D_FECHA_ORIGEN { get; set; }
        public decimal D_FECHA_DESPEGUE { get; set; }
        public decimal D_FECHA_ARRIBO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
        public string MATRICULA_DGAC { get; set; }
    }

    public class O_LISTA_AASANA_PVUELO_CTY
    {
        public decimal ID_AASANA_PVUELO { get; set; }
        public decimal ID_TABLA { get; set; }
        public decimal ID_PLAN_VUELO { get; set; }
        public string NRO_VUELO { get; set; }
        public string MODELO { get; set; }
        public string ORIGEN { get; set; }
        public string DESTINO { get; set; }
        public string HRA_APROX_DESTINO { get; set; }
        public string RUTA_GPS { get; set; }
        public string AERO_ALTERNATIVO { get; set; }
        public string MATRICULA { get; set; }
        public string FH_REGISTRO { get; set; }
        public decimal FECHA_REGISTRO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string AUD_USUARIO { get; set; }
        public DateTime AUD_FECHA { get; set; }
    }

    public class O_SERVICIO_PARAMETRICAS
    {
        public decimal ID { get; set; }
        public string NOMBRE { get; set; }
    }

    public class O_OBTENER_VOLUMEN_REF_CTY
    {
        public decimal ID_VOLUMEN_REFERENCIAL { get; set; }
        public decimal ID_EXPLOTADOR { get; set; }
        public string EXPLOTADOR { get; set; }
        public string MODELO { get; set; }
        public string MATRICULA { get; set; }
        public string ID_AEROPUERTO_ORIGEN { get; set; }
        public string AEROPUERTO_ORIGEN { get; set; }
        public string ID_AEROPUERTO_DESTINO { get; set; }
        public string AEROPUERTO_DESTINO { get; set; }
        public string ID_AEROPUERTO_ALTERNO { get; set; }
        public string AEROPUERTO_ALTERNO { get; set; }
        public string TIEMPO_VUELO { get; set; }
        public decimal CONSUMO_HORA_VUELO { get; set; }
        public string TIEMPO_HORAS_BLOCK { get; set; }
        public decimal TANQUEO_MAXIMO_REQUERIDO { get; set; }
        public string TIEMPO_SOBREVUELO_MINIMO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string REGISTRO_USUARIO { get; set; }
        public DateTime REGISTRO_FECHA { get; set; }
        public string MNS_ERROR { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public decimal ID_UNIDAD_MEDIDA { get; set; }
    }

    public class O_OBTENER_PARAMETRICA_CTY
    {
        public decimal ID_PARAMETRICA { get; set; }
        public string PARAMETRICA { get; set; }
    }


    public class pscaaObjMovimientoTransaccion
    {
        public string citeInfraestructura { get; set; }
        public pscaaMovimientoTransaccion objetoMovimientoTransaccion { get; set; }
    }
    public class pscaaMovimientoTransaccion
    {
        public decimal idIntegracion { get; set; }
        public decimal idTipoOperacion { get; set; }
        public decimal fechaMovimiento { get; set; }
        public decimal idProyectoRelacionado { get; set; }
        public decimal idActividadProyectoRel { get; set; }
        public decimal idEstadoMovimiento { get; set; }
        public decimal idTipoFlujo { get; set; }
        public decimal idTipoMovimiento { get; set; }
        public decimal idPais { get; set; }
        public decimal idHidrocarburo { get; set; }
        public decimal idUnidadMedidaHidrocarburo { get; set; }
        public decimal cantidadHidrocarburo { get; set; }
        public decimal codigoBarras { get; set; }
        public string identificador { get; set; }
        public string subIdentificador { get; set; }
        public string codigoCs { get; set; }
        public decimal idTipoEnvase { get; set; }
        public decimal idTipoInfraestructuraRef { get; set; }
        public decimal idInfraestructuraRef { get; set; }
        public decimal idTipoInfraestructura { get; set; }
        public decimal idInfraestructura { get; set; }
        public decimal idUnidadTemperatura { get; set; }
        public decimal temperatura { get; set; }
        public string gravedadEspecifica { get; set; }
        public decimal idTipoMedicion { get; set; }
        public decimal cantidadAguaLitro { get; set; }
        public string observacion { get; set; }
        public string idAnhTransaccion { get; set; }
        public List<pscaaFacturas> ListaFacturas { get; set; }//public List<ObjTransaccion> listaTransaccion { get; set; }
        public List<pscaaDocumentos> ListaDocumentos { get; set; }//public List<ObjTransaccion> listaTransaccion { get; set; }
    }

    public class pscaaFacturas //Transaccion
    {
        public decimal fechaEmision { get; set; }//public decimal fechaEmision { get; set; }
        public decimal idTipoTransaccion { get; set; } ///anulada activa //public decimal idTipoTransaccion { get; set; } ///anulada activa
        public decimal idTipoEmisor { get; set; }//idTipoMovimiento Nacional-Internacional public decimal idTipoEmisor { get; set; }
        public decimal idTipoCreditoFiscal { get; set; } // con o sin credito public decimal idTipoCreditoFiscal { get; set; }
        public decimal idFormaPago { get; set; } //public decimal idFormaPago { get; set; }
        public decimal idTipoMoneda { get; set; }//public decimal idTipoMoneda { get; set; }
        public decimal nitEmisor { get; set; }//public decimal nitConsumidor { get; set; }
        public decimal nitConsumidor { get; set; }//public decimal nitConsumidor { get; set; }
        public string razonSocial { get; set; }//public string razonSocial { get; set; }
        public decimal numeroAutorizacion { get; set; }// public decimal numeroAutorizacion { get; set; }
        public string numeroFactura { get; set; } //factura, nota, vale public string numeroTransaccion { get; set; } //factura nota vale
        public string codigoControl { get; set; }      // public string codigoControl { get; set; }
        public decimal precioUnitario { get; set; } //public decimal precioUnitario { get; set; }
        public decimal importeTotal { get; set; } //importe public decimal precioTotal { get; set; } //importe
        public CUsuario usuario { get; set; }//public string usuarioTransaccion { get; set; }
        public string observaciones { get; set; } //public string observaciones { get; set; }
    }

    public class pscaaDocumentos
    {
        public decimal fechaEmision { get; set; }//public decimal fechaEmision { get; set; }
        public decimal idTipoTransaccion { get; set; } ///anulada activa //public decimal idTipoTransaccion { get; set; } ///anulada activa
        public string numeroDocumento { get; set; } //factura, nota, vale public string numeroTransaccion { get; set; } //factura nota vale        
        public decimal ciResponsable { get; set; }//nit
        public string responsable { get; set; }//public string razonSocial { get; set; }
        public decimal precioUnitario { get; set; } //public decimal precioUnitario { get; set; }
        public decimal importeTotal { get; set; } //importe public decimal precioTotal { get; set; } //importe 
        public CUsuario usuario { get; set; }//public string usuarioTransaccion { get; set; }
        public string observaciones { get; set; } //public string observaciones { get; set; }
    }

    public class CUsuario
    {
        public long DocumentoIdentidad { get; set; }
        public string NombreCompleto { get; set; }
    }

    //IMPORTADORES
    public class O_OBTENER_ANEXO_A
    {
        public string NRO_DUI { get; set; }
        public string PROVEEDOR { get; set; }
        public string RON { get; set; }
        public string MES { get; set; }
        public string NRO_FACTURA { get; set; }
        public decimal VOLUMEN_FACTURADO { get; set; }
        public decimal VALOR_FACTURADO { get; set; }
    }

    public class O_OBTENER_ANEXO_A_GB
    {
        public string PRODUCTO { get; set; }
        public string MES_RECIBIDO { get; set; }
        public string VOLUMEN { get; set; }
        public string PRECIO { get; set; }
        public string MONTO_NETO { get; set; }        
    }

    public class O_OBTENER_ANEXO_B1
    {
        public string NRO_DUI { get; set; }
        public string MES { get; set; }
        public string NRO_FACTURA_TRANSPORTE { get; set; }
        public decimal VOLUMEN_DESPACHADO { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal DIFERENCIA { get; set; }
        public decimal MERMA_PERMISIBLE { get; set; }
        public decimal MERMA_EXCEDENTE { get; set; }
        public decimal FLETE { get; set; }
        public decimal COSTO_PRODUCTO { get; set; }
        public decimal TRANSPORTE_TOTAL { get; set; }
        public decimal DESCUENTO_MERMA { get; set; }
        public decimal COSTO_TRANSPORTE { get; set; }
    }

    public class O_OBTENER_ANEXO_B2
    {
        public string NRO_DUI { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string EMPRESA_TRANSPORTE { get; set; }
        public string NRO_FACTURA_TRANSPORTE { get; set; }
        public string PLANTA_DESPACHO { get; set; }
        public string PLANTA_RECEPCION { get; set; }
        public decimal VOLUMEN_DESPACHADO { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal DIFERENCIA { get; set; }
        public decimal MERMA_PERMISIBLE { get; set; }
        public decimal MERMA_EXCEDENTE { get; set; }
        public decimal FLETE { get; set; }
        public decimal COSTO_PRODUCTO { get; set; }
        public decimal TRANSPORTE_TOTAL { get; set; }
        public decimal DESCUENTO_MERMA { get; set; }
        public decimal COSTO_TRANSPORTE { get; set; }
    }

    public class O_OBTENER_ANEXO_B2_DU
    {
        public string PRODUCTO { get; set; }
        public string MES_RECIBIDO { get; set; }
        public string VOLUMEN { get; set; }
        public string PRECIO { get; set; }
        public string MONTO { get; set; }        
    }

    public class O_OBTENER_ANEXO_B3
    {
        public string NRO_DUI { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string PROVEEDOR { get; set; }
        public string EMPRESA { get; set; }
        public string NRO_FACTURA { get; set; }
        public decimal VOLUMEN_DESTINO1 { get; set; }
        public decimal VOLUMEN_DESTINO2 { get; set; }
        public decimal FLETE { get; set; }
        public decimal COSTO_FACTURADO { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }

    public class O_OBTENER_ANEXO_C
    {
        public string NRO_DUI { get; set; }
        public string EMPRESA_ASEGURADORA { get; set; }
        public string PERIODO_COBERTURA { get; set; }
        public string NRO_FACTURA_ASEGURADORA { get; set; }
        public decimal MONTO_FACTURADO { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }

    public class O_OBTENER_ANEXO_D
    {
        public string MES_CARGA { get; set; }
        public string NRO_DUI { get; set; }
        public string PROVEEDOR { get; set; }
        public string NRO_FACTURA { get; set; }
        public decimal VOLUMEN_FACTURADO { get; set; }
        public decimal MONTO_INSPECCION { get; set; }
    }

    public class O_OBTENER_ANEXO_E
    {
        public string NRO_DUI { get; set; }
        public decimal GRAVAMEN { get; set; }
        public decimal SIDUINEA { get; set; }
        public decimal COSTO_DEPOSITO { get; set; }
        public decimal TOTAL_COSTO { get; set; }

    }

    public class O_OBTENER_ANEXO_F
    {
        public string NRO_DUI { get; set; }
        public string MES_RECIBIDO { get; set; }
        public string PROVEEDOR { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public decimal MONTO_FACTURA_PROVEEDOR { get; set; }
        public decimal COSTO_FINANCIERO { get; set; }
        public decimal COSTO_FINANCIERO_NETO { get; set; }
    }

    public class O_OBTENER_ANEXO_G
    {
        public string NRO_DUI { get; set; }
        public string MES { get; set; }
        public string EMPRESA_ALMACENAJE { get; set; }
        public string NRO_FACTURA_ALMACENAJE { get; set; }
        public decimal TARIFA_UNITARIA { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal COSTO_ALMACENAJE { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }
    
    public class O_OBTENER_ANEXO_H
    {
        public string MES { get; set; }
        public string NRO_DUI { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal MONTO_MONITOREO { get; set; }
    }

    public class O_OBTENER_ANEXO_I
    {
        public string NRO_DUI { get; set; }
        public string PROVEEDOR { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string MES_RECEPCION { get; set; }
        public string PLANTA { get; set; }
        public decimal VOLUMEN { get; set; }
    }

    public class O_OBTENER_ANEXO_I_GB
    {
        public string MES_RECIBIDO { get; set; }
        public string PLANTA { get; set; }
        public decimal VOLUMEN { get; set; }

    }

    public class O_OBTENER_ANEXO_I_GBASE
    {
        public string MES_RECIBIDO { get; set; }
        public string PLANTA { get; set; }
        public decimal DATO_1 { get; set; }
        public decimal DATO_2 { get; set; }
        public decimal DATO_3 { get; set; }

    }

    public class O_OBTENER_COSTOS_IMPORTADO
    {
        public string RON { get; set; }
        public decimal A { get; set; }
        public decimal B { get; set; }
        public decimal C { get; set; }
        public decimal D { get; set; }
        public decimal E { get; set; }
        public decimal F { get; set; }
        public decimal G { get; set; }
        public decimal H { get; set; }
        public decimal I { get; set; }
    }

    public class O_OBTENER_COSTOS_SUBVENCION
    {
        public string DATO_1 { get; set; }
        public string DATO_2 { get; set; }
        public decimal DATO_3 { get; set; }
        public decimal DATO_4 { get; set; }
        public decimal DATO_5 { get; set; }
        public decimal DATO_6 { get; set; }
        public decimal DATO_7 { get; set; }
        public decimal DATO_8 { get; set; }
        public decimal DATO_9 { get; set; }
    }

    public class O_OBTENER_COSTOS_SUBVENCION_FINAL
    {
        public string DATO_1 { get; set; }
        public string DATO_2 { get; set; }
        public decimal DATO_3 { get; set; }
        public decimal DATO_4 { get; set; }
        public decimal DATO_5 { get; set; }
        public decimal DATO_6 { get; set; }
    }

    public class O_OBTENER_COSTOS_UNITARIO
    {
        public string DATO_1 { get; set; }
        public decimal DATO_2 { get; set; }
        public decimal DATO_3 { get; set; }
        public decimal DATO_4 { get; set; }
    }

    public class O_OBTENER_COSTOS_TOT_SUB
    {
        public decimal DATO_1 { get; set; }
        public decimal DATO_2 { get; set; }
        public decimal DATO_3 { get; set; }
        public decimal DATO_4 { get; set; }
    }

    public class O_OBTENER_VOLUMEN_IMPORTADO
    {
        public decimal IDMOVIMIENTO { get; set; }
        public string RON { get; set; }
        public decimal VOLUMEN { get; set; }
        public decimal COSTO { get; set; }
    }

    public class O_LISTADO_DUI
    {
        public string NRO_DUI { get; set; }
        public string DUI { get; set; }        
    }

    public class EResultadoEquipos
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_EQUIPO_RD_CTY> oResultado { get; set; }
    }

    public class O_EQUIPO_RD_CTY
    {
        public decimal ESTADO { get; set; }
        public decimal ID_EQUIPO_RD { get; set; }
        public decimal EQUIPO_RD_PADRE_ID { get; set; }
        public decimal TIPO_IFR_ID { get; set; }
        public string TIPO_IFR { get; set; }
        public decimal TIPO_OPERACION_ID { get; set; }
        public string TIPO_OPERACION { get; set; }
        public string CODIGO_ANH { get; set; }
        public string CODIGO { get; set; }
        public string SERIE { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string PLACA { get; set; }
        public string OBSERVACION { get; set; }
        public decimal PRODUCTO_ID { get; set; }
        public string PRODUCTO { get; set; }
        public List<O_IMAGEN_CTY> ImagenesEquiposRD { get; set; }
    }
}