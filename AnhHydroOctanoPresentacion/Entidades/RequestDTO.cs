
using System;
using AnhHydroOctanoPersistencia.Entidades;
using ServiceStack.ServiceHost; 
using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Entidades
{
    [Serializable]
    public class EListarDeclaracionJurada : IReturn<EResultado>
    {
    	public string I_DATO_1 { get; set; }
    	public string I_DATO_2 { get; set; }
    	public string I_DATO_3 { get; set; }
    	public string I_DATO_4 { get; set; }
    	public string I_DATO_5 { get; set; }
    	public string I_DATO_6 { get; set; }
    	public decimal I_FILTRO { get; set; }
    } 
    
    [Serializable]
    public class EGuardarDeclaracionJurada : IReturn<EResultado>
    {
    	public O_LISTA_REPORTE_DATOSP_CTY cabecera { get; set; }
    	public decimal decFechaRegistro { get; set; }
    	public decimal decIdCitePadre { get; set; }
    	public decimal decIdOpcion { get; set; }
    	public decimal decIdUsuario { get; set; }
        public decimal decIdMaxMovimiento { get; set; }
        //public AnhHydroOctanoAgenteServicio.Aplicacion.Consultas.O_DECLARACION_JURADA_CTY listaDeclaracion { get; set; }
        public O_DECLARACION_JURADA_CTY listaDeclaracion { get; set; }
    	public string strCodigoFormularioFac { get; set; }
    	public string strCodigoFormularioMov { get; set; }
    	public string strCredencial { get; set; }
    	public string strCredencialFuncionario { get; set; }
    	public string strDpto { get; set; }
    	public string strReferencia { get; set; }
    	public string strSigla { get; set; }
    } 
    
    [Serializable]
    public class EAutorizacionMov : IReturn<EResultado>
    {
    	public O_AUTORIZACION_MOV_CTY AutorizacionMov { get; set; }
    	public decimal decIdEntidad { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EActivaAutorizacionMov : IReturn<EResultado>
    {
    	public decimal decIdAutorizacion { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EEliminarAutorizacionMov : IReturn<EResultado>
    {
    	public decimal decIdAutorizacion { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EObtenerAutorizacionMov : IReturn<EResultado>
    {
    	public string I_DATO_1 { get; set; }
    	public string I_DATO_2 { get; set; }
    	public string I_DATO_3 { get; set; }
    	public string I_DATO_4 { get; set; }
    	public string I_DATO_5 { get; set; }
    	public decimal I_FILTRO { get; set; }
    } 
    
    [Serializable]
    public class EDocumento : IReturn<EResultado>
    {
    	public O_DOCUMENTO_TRANS_CTY Documento { get; set; }
    	public decimal decIdEntidad { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EEliminarDocumento : IReturn<EResultado>
    {
    	public decimal decIdDocumento { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EAnularDocumento : IReturn<EResultado>
    {
    	public decimal decIdDocumento { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EObtenerDocumento : IReturn<EResultado>
    {
    	public string I_DATO_1 { get; set; }
    	public string I_DATO_2 { get; set; }
    	public string I_DATO_3 { get; set; }
    	public string I_DATO_4 { get; set; }
    	public string I_DATO_5 { get; set; }
    	public string I_DATO_6 { get; set; }
    	public decimal I_FILTRO { get; set; }
    }

    [Serializable]
    public class ETransaccion : IReturn<EResultado>
    {
        public ObjTransaccion ObjTransaccion { get; set; }
        public string strCredencial { get; set; }
    } 

    [Serializable]
    public class EFactura : IReturn<EResultado>
    {
    	public O_FACTURA_CTY Factura { get; set; }
    	public decimal decIdEntidad { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EAnularFactura : IReturn<EResultado>
    {
    	public decimal decIdFactura { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EEliminarFactura : IReturn<EResultado>
    {
    	public decimal decIdFactura { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EObtenerFactura : IReturn<EResultado>
    {
    	public string I_DATO_1 { get; set; }
    	public string I_DATO_2 { get; set; }
    	public string I_DATO_3 { get; set; }
    	public string I_DATO_4 { get; set; }
    	public string I_DATO_5 { get; set; }
    	public string I_DATO_6 { get; set; }
    	public decimal I_FILTRO { get; set; }
    } 
    
    [Serializable]
    public class ERegistrarPlanVuelo : IReturn<EResultado>
    {
    	public decimal decFechaRegistro { get; set; }
    	public List<O_PLAN_VUELO_CTY> objListaPlanVuelo { get; set; }
    	public string strIdentificadorAereonave { get; set; }
    	public string strLlave { get; set; }
    	public string strUsuarioRegistro { get; set; }
    } 
    
    [Serializable]
    public class ERegistrarAeronave : IReturn<EResultado>
    {
    	public decimal decAnoFabricacion { get; set; }
    	public decimal decCargaUtil { get; set; }
    	public decimal decFechaRegistro { get; set; }
    	public decimal decIdBaseOperaciones { get; set; }
    	public decimal decIdCategoria { get; set; }
    	public decimal decIdClase { get; set; }
    	public decimal decIdDepartamentoRegistro { get; set; }
    	public decimal decIdEstadoConsumidor { get; set; }
    	public decimal decIdMarca { get; set; }
    	public decimal decIdServicioAdicional { get; set; }
    	public decimal decIdTipo { get; set; }
    	public decimal decIdTipoAccionRegistro { get; set; }
    	public decimal decIdTipoMatricula { get; set; }
    	public decimal decIdTipoServicio { get; set; }
    	public List<byte> lstFotografias { get; set; }
    	public List<decimal> lstIdTipoCombustible { get; set; }
    	public object objDatosExplotador { get; set; }
    	public object objDatosPropietario { get; set; }
    	public string strBarcode { get; set; }
    	public string strIdentificador { get; set; }
    	public string strLlave { get; set; }
    	public string strModelo { get; set; }
    	public string strSerieChasis { get; set; }
    	public string strSubIdentificador { get; set; }
    	public string strUsuarioRegistro { get; set; }
    } 
    
    [Serializable]
    public class EObtenerEntidadesUsuario : IReturn<EResultado>
    {
        public decimal decFiltro { get; set; }
        public string strParametro1 { get; set; }
        public string strParametro2 { get; set; }
        public string strParametro3 { get; set; }
        public string strParametro4 { get; set; }
        public string strParametro5 { get; set; } 
    } 
    
    [Serializable]
    public class EObtenerCatalogos : IReturn<EResultado>
    {
    	public string strLlave { get; set; }
    } 
    
    [Serializable]
    public class ERegistrarMedicion : IReturn<EResultado>
    {
    	public decimal decFechaHoraMedicion { get; set; }
    	public decimal decIdTanque { get; set; }
    	public decimal decIdTipoMedicion { get; set; }
    	public decimal decIdTipoRegistro { get; set; }
    	public decimal decVolumenFisico { get; set; }
    	public string strLlave { get; set; }
    	public string strObservaciones { get; set; }
    	public string strUsuario { get; set; }
    } 
    
    [Serializable]
    public class EObtenerEstructura : IReturn<EResultado>
    {
    	public string Credencial { get; set; }
    	public decimal IdTipoConsulta { get; set; }
    	public string Identificador { get; set; }
    	public string Sub_identificador { get; set; }
    } 
    
    [Serializable]
    public class EObtenerMovimientoProducto : IReturn<EResultado>
    {
    	public string I_DATO_1 { get; set; }
    	public string I_DATO_2 { get; set; }
    	public string I_DATO_3 { get; set; }
    	public string I_DATO_4 { get; set; }
    	public string I_DATO_5 { get; set; }
    	public string I_DATO_6 { get; set; }
    	public decimal I_FILTRO { get; set; }
    } 
    
    [Serializable]
    public class EMovimientoProducto : IReturn<EResultado>
    {
    	public string strCredencial { get; set; }
        public List<ObjMovimientoProducto> listaMovimientoProducto { get; set; }
        //public List<ObjMovimientoProducto> LstMovimientos { get; set; }
    }
    [Serializable]
    public class EMovimientoProductos : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public List<ObjMovimientoProducto> listaMovimientoProducto { get; set; }
    }

    [Serializable]
    public class ERegistraMovimientoTransaccion : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public decimal decFiltro { get; set; } //1 uno a 1 ; 2 masivo
        public List<ObjMovimientoTransaccion> listaMovimientoTransaccion { get; set; }
    }

    [Serializable]
    public class ERegistraProtoMovimientoTransaccion : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public decimal decFiltro { get; set; } //1 uno a 1 ; 2 masivo
        public decimal decIdConsumidor { get; set; }
        public decimal decIdTipoActividad { get; set; }
        public decimal decIdTipoOperacion { get; set; }
        public decimal decFechaInicio { get; set; }
        public decimal decFechaFin { get; set; }
        public List<ObjProtoMovimientoTransaccion> listaMovimientoTransaccion { get; set; }
    }

    [Serializable]
    public class EActualizaMovimientoTransaccion : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public List<ObjMovimientoTransaccion> listaMovimientoTransaccion { get; set; }
    }

    [Serializable]
    public class EEliminarMovimientoProducto : IReturn<EResultado>
    {
    	public decimal decIdMovimiento { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EAnularMovimientoProducto : IReturn<EResultado>
    {
    	public decimal decIdMovimiento { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EActivarMovimientoProducto : IReturn<EResultado>
    {
    	public decimal decIdMovimiento { get; set; }
    	public string strCredencial { get; set; }
    } 
    
    [Serializable]
    public class EObtenerConsumidor : IReturn<EResultado>
    {
    	public decimal decTipoConsulta { get; set; }
    	public string strCredencial { get; set; }
    	public string strIdentificador { get; set; }
    	public string strSubIdentificador { get; set; }
    } 
    
    //[Serializable]
    //public class O_LISTA_REPORTE_DATOSP_CTY : IReturn<EResultado>
    //{
    //    public string AMBITO_OPERACION { get; set; }
    //    public decimal APP_FECHA_REGISTRO { get; set; }
    //    public decimal APP_ID_HISTORICO { get; set; }
    //    public decimal APP_ID_USUARIO { get; set; }
    //    public decimal CI { get; set; }
    //    public string DIRECCION { get; set; }
    //    public byte[] DOCUMENTO { get; set; }
    //    public decimal ESTADO { get; set; }
    //    public decimal FECHA_FIN { get; set; }
    //    public decimal FECHA_INICIO { get; set; }
    //    public decimal FECHA_PRESENTACION { get; set; }
    //    public decimal ID_CITE { get; set; }
    //    public decimal ID_CONSUMIDOR { get; set; }
    //    public decimal ID_REPORTE_DATO_PRINC { get; set; }
    //    public decimal ID_REPORTE_PRINCIPAL { get; set; }
    //    public string LUGAR_REGISTRO { get; set; }
    //    public string MATERNO { get; set; }
    //    public string MES { get; set; }
    //    public string NOMBRES { get; set; }
    //    public string OBSERVACIONES { get; set; }
    //    public string PATERNO { get; set; }
    //    public string RAZON_SOCIAL { get; set; }
    //    public string TELEFONO { get; set; }
    //    public string TIPO_SOCIEDAD { get; set; }
    //} 
    
    //[Serializable]
    //public class O_LISTA_DECLARACION_CTY : IReturn<EResultado>
    //{
    //    public decimal AUD_ESTADO { get; set; }
    //    public decimal CONSUMO_PROPIO { get; set; }
    //    public decimal DEMASIA { get; set; }
    //    public decimal FECHA_OPERACION { get; set; }
    //    public decimal ID_CONSUMIDOR { get; set; }
    //    public decimal ID_PRODUCTO { get; set; }
    //    public decimal ID_TIPO_ACTIVIDAD { get; set; }
    //    public decimal MERMA { get; set; }
    //    public decimal RECEPCION { get; set; }
    //    public decimal SALDO_FINAL { get; set; }
    //    public decimal SALDO_INICIAL { get; set; }
    //    public decimal SALIDA_VENTA { get; set; }
    //} 
    
    [Serializable]
    public class CInternacionalDj : IReturn<EResultado>
    {
    	public decimal DecCantidadDo { get; set; }
    	public decimal DecCantidadExt { get; set; }
    	public decimal DecCantidadGe { get; set; }
    	public decimal DecCantidadGn { get; set; }
    	public decimal DecCantidadGp { get; set; }
    	public decimal DecCantidadProp { get; set; }
    	public decimal DecImporteExt { get; set; }
    	public decimal DecImporteProp { get; set; }
    	public decimal DecPrecioDo { get; set; }
    	public decimal DecPrecioExt { get; set; }
    	public decimal DecPrecioGe { get; set; }
    	public decimal DecPrecioGn { get; set; }
    	public decimal DecPrecioGp { get; set; }
    	public decimal DecPrecioProp { get; set; }
    	public long LngId { get; set; }
    	public long LngNit { get; set; }
    	public long LngNoAutExt { get; set; }
    	public long LngNoAutProp { get; set; }
    	public long LngNoFactExt { get; set; }
    	public long LngNoFactProp { get; set; }
    	public string StrEstado { get; set; }
    	public string StrFecha { get; set; }
    	public string StrPais { get; set; }
    	public string StrPlaca { get; set; }
    	public string StrProducto { get; set; }
        public decimal DecIdProducto { get; set; }
        
    }
    [Serializable]
    public class CDjInternacional : IReturn<EResultado>
    {
        public long LngId { get; set; }
        public long LngNit { get; set; }
        public string StrRazonSocial { get; set; }
        public long LngNoAutExt { get; set; }
        public long LngNoAutProp { get; set; }
        public long LngNoFactExt { get; set; }
        public long LngNoFactProp { get; set; }
        public string StrCodCtrlExt { get; set; }
        public string StrCodCtrlProp { get; set; }
        public string StrEstado { get; set; }
        public decimal DecIdEstado { get; set; }
        public string StrFecha { get; set; }
        public string StrPais { get; set; }
        public string StrPlaca { get; set; }
        public string StrProducto { get; set; }
        public string StrCodProducto { get; set; }
        public string StrUnidadMedida { get; set; }
        public decimal DecIdProducto { get; set; }
        public decimal DecCantidadExt { get; set; }
        public decimal DecCantidadProp { get; set; }
        public decimal DecImporteExt { get; set; }
        public decimal DecImporteProp { get; set; }
        public decimal DecPrecioExt { get; set; }
        public decimal DecPrecioProp { get; set; }
        public string StrTipoMovimiento { get; set; }
    }

    [Serializable]
    public class CDjContenedor : IReturn<EResultado>
    {
        public long LngId { get; set; }

        public string StrFecha { get; set; }

        public string StrPais { get; set; }
        public string StrIdentificador { get; set; }
        public string StrSubIdentificador { get; set; }

        public string StrTipoContenedor { get; set; }

        public long LngNit { get; set; }
        public string StrRazonSocial { get; set; }

        public long LngNoAut { get; set; }
        public string StrNoTransaccion { get; set; }
        public string StrCodCtrl { get; set; }

        public string AutSustCtrl { get; set; }
        public string AutAnh { get; set; }

        public decimal DecIdProducto { get; set; }
        public string StrProducto { get; set; }
        public string StrCodProducto { get; set; }
        public string StrUnidadMedida { get; set; }

        public string StrEstado { get; set; }
        public decimal DecIdEstado { get; set; }

        public decimal DecCantidad { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecImporte { get; set; }
    } 

    [Serializable]
    public class O_AUTORIZACION_MOV_CTY : IReturn<EResultado>
    {
    	public string CODIGO_AUTORIZACION { get; set; }
    	public string DETALLE { get; set; }
        public Nullable<decimal> ESTADO_AUTORIZACION { get; set; }
        public Nullable<decimal> FECHA_REGISTRO { get; set; }
        public Nullable<decimal> ID_AUTORIZACION_MOV { get; set; }
        public Nullable<decimal> ID_CONSUMIDOR_AUTORIZACION { get; set; }
        public Nullable<decimal> ID_MOVIMIENTO { get; set; }
        public Nullable<decimal> ID_TIPO_AUTORIZACION { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
        public Nullable<decimal> IMPORTE_SALDO { get; set; }
        public Nullable<decimal> NIT_DOCUMENTO { get; set; }
        public Nullable<decimal> NRO_AUTORIZACION { get; set; }
    	public string OBSERVACION { get; set; }
        public Nullable<decimal> PERIODO { get; set; }
        public Nullable<decimal> VOLUMEN_SALDO { get; set; }
    } 
    
    [Serializable]
    public class O_DOCUMENTO_TRANS_CTY : IReturn<EResultado>
    {
    	public decimal ESTADO_DOCUMENTO { get; set; }
    	public decimal FECHA_HORA_EMISION { get; set; }
    	public decimal FECHA_HORA_FIN { get; set; }
    	public decimal FECHA_HORA_INICIO { get; set; }
    	public decimal FECHA_REGISTRO { get; set; }
    	public decimal ID_CITE { get; set; }
    	public decimal ID_DOCUMENTO { get; set; }
    	public decimal ID_GESTION_SERVICIO { get; set; }
    	public decimal ID_SERVICIO_ADICIONAL { get; set; }
    	public decimal ID_TIPO_DOCUMENTO { get; set; }
    	public decimal ID_USUARIO { get; set; }
    	public decimal IMPORTE_DOCUMENTO { get; set; }
    	public decimal IMPORTE_TOTAL { get; set; }
    	public decimal NIT_EMISOR { get; set; }
    	public decimal NRO_DOCUMENTO { get; set; }
    	public string OBSERVACIONES { get; set; }
    } 
    
    [Serializable]
    public class O_FACTURA_CTY : IReturn<EResultado>
    {
        public Nullable<decimal> CANTIDAD { get; set; }
    	public string CODIGO_CONTROL { get; set; }
    	public string DETALLE { get; set; }
        public Nullable<decimal> ESTADO_FACTURA { get; set; }
        public Nullable<decimal> FECHA_HORA_EMISION { get; set; }
        public Nullable<decimal> FECHA_REGISTRO { get; set; }
    	public string IDENTIFICADOR { get; set; }
        public Nullable<decimal> ID_DOCUMENTO_TRANSACCION { get; set; }
        public Nullable<decimal> ID_FACTURA { get; set; }
        public Nullable<decimal> ID_INTEGRACION { get; set; }
        public Nullable<decimal> ID_MOVIMIENTO { get; set; }
        public Nullable<decimal> ID_PAIS { get; set; }
        public Nullable<decimal> ID_PRODUCTO { get; set; }
        public Nullable<decimal> ID_SERVICIO_ADICIONAL { get; set; }
        public Nullable<decimal> ID_TIPO_CREDITO_FISCAL { get; set; }
        public Nullable<decimal> ID_TIPO_EMISOR { get; set; }
        public Nullable<decimal> ID_TIPO_FACTURA { get; set; }
        public Nullable<decimal> ID_TIPO_MONEDA { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
        public Nullable<decimal> IMPORTE { get; set; }
        public Nullable<decimal> NIT_CONSUMIDOR { get; set; }
        public Nullable<decimal> NIT_EMISOR { get; set; }
        public Nullable<decimal> NRO_AUTORIZACION { get; set; }
        public Nullable<decimal> NRO_FACTURA { get; set; }
    	public string OBSERVACIONES { get; set; }
        public Nullable<decimal> PRECIO_UNITARIO { get; set; }
    	public string RAZON_SOCIAL { get; set; }
    	public string USUARIO { get; set; }
    } 
    
    [Serializable]
    public class O_PLAN_VUELO_CTY : IReturn<EResultado>
    {
    	public decimal FECHA_HORA_LLEGADA { get; set; }
    	public decimal FECHA_HORA_SALIDA { get; set; }
    	public decimal ID_AEREOPUERTO { get; set; }
    	public decimal ID_AEREOPUERTO_ALTERNO { get; set; }
    } 
    
    [Serializable]
    public class O_MOVIMIENTO_PRODUCTO_CTY : IReturn<EResultado>
    {
    	public Nullable<decimal> BARCODE { get; set; }
    	public string BINARIO_CONTROL { get; set; }
        public Nullable<decimal> CANTIDAD_ENVASES { get; set; }
        public Nullable<decimal> D_NRO_DOCUMENTO { get; set; }
        public Nullable<decimal> ESTADO_MOVIMIENTO { get; set; }
        public Nullable<decimal> FECHA_OPERACION { get; set; }
        public Nullable<decimal> FECHA_REGISTRO { get; set; }
        public Nullable<decimal> F_IMPORTE { get; set; }
        public Nullable<decimal> F_NIT_CONSUMIDOR { get; set; }
        public Nullable<decimal> F_NRO_AUTORIZACION { get; set; }
        public Nullable<decimal> F_NRO_FACTURA { get; set; }
    	public string F_RAZON_SOCIAL { get; set; }
        public Nullable<decimal> GRADO_API { get; set; }
    	public string IDENTIFICADOR { get; set; }
        public Nullable<decimal> ID_CATEGORIA_CONSUMIDOR { get; set; }
        public Nullable<decimal> ID_CONSUMIDOR { get; set; }
        public Nullable<decimal> ID_CONSUMIDOR_ADICIONAL { get; set; }
        public Nullable<decimal> ID_FORMA_PAGO { get; set; }
        public Nullable<decimal> ID_INFRAESTRUCTURA { get; set; }
        public Nullable<decimal> ID_INFRAESTRUCTURA_ADICIONAL { get; set; }
        public Nullable<decimal> ID_INTEGRACION { get; set; }
        public Nullable<decimal> ID_MEDIO_TRANSPORTE { get; set; }
        public Nullable<decimal> ID_MOVIMIENTO { get; set; }
        public Nullable<decimal> ID_MOVIMIENTO_PADRE { get; set; }
        public Nullable<decimal> ID_PRODUCTO { get; set; }
        public Nullable<decimal> ID_TIPO_ACTIVIDAD { get; set; }
        public Nullable<decimal> ID_TIPO_MEDICION { get; set; }
        public Nullable<decimal> ID_TIPO_MEDIO_TRANSPORTE { get; set; }
        public Nullable<decimal> ID_TIPO_MONEDA { get; set; }
        public Nullable<decimal> ID_TIPO_MOVIMIENTO { get; set; }
        public Nullable<decimal> ID_TIPO_OPERACION { get; set; }
    	public decimal ID_TIPO_REGISTRO { get; set; }
        public Nullable<decimal> ID_TIPO_REPORTE { get; set; }
    	public string ID_TRANSACCION { get; set; }
        public Nullable<decimal> ID_UBICACION_OPERACION { get; set; }
        public Nullable<decimal> ID_UNIDAD_MEDIDA { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
    	public List<O_AUTORIZACION_MOV_CTY> ListaAutorizaciones { get; set; }
    	public List<O_AUTORIZACION_MOV_CTY> ListaDocumentos { get; set; }
    	public List<O_FACTURA_CTY> ListaFacturas { get; set; }
    	public string OBSERVACIONES { get; set; }
        public Nullable<decimal> PRECIO_MOVIMIENTO { get; set; }
        public Nullable<decimal> PRECIO_PRODUCTO { get; set; }
        public Nullable<decimal> TEMPERATURA { get; set; }
        public Nullable<decimal> VOLUMEN { get; set; }
        public Nullable<decimal> VOLUMEN_ADICIONAL { get; set; }
    }

    public class ObjMovimientoProducto
    {
        public decimal idMovimiento { get; set; }
        public decimal idMovimientoPadre { get; set; }

        public decimal idTipoActividad { get; set; }
        public decimal idTipoOperacion { get; set; }
        public decimal idConsumidor { get; set; }      		// QUIEN A PRODUCIDO EL MOVIMIENTO   anhmov

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaRegistroMovimiento { get; set; }

        public decimal fechaOperacion { get; set; }
        public decimal idTipoMovimientoFlujo { get; set; } //transaccion-fisico /***/
        public decimal idTipoMovimiento { get; set; } //Nacional e internacional
        public decimal idEstadoMovimiento { get; set; }
        public decimal idFuenteOrigenDato { get; set; }

        public decimal idUnidadMedidaHidrocarburo { get; set; }
        public decimal idHidrocarburo { get; set; }
        public decimal cantidadHidrocarburo { get; set; } // EN BD CANTIDAD	      ???
        public string identificador { get; set; }
        public string subIdentificador { get; set; }
        public string observacion { get; set; }

        public decimal idInfraestructura { get; set; }
        public decimal idTipoMedicion { get; set; }
        public decimal cantidadAguaLitros { get; set; }	  // EN BD CANTIDAD_ADICIONAL ???
        
        public decimal idUnidadTemperatura { get; set; }
        public decimal temperatura { get; set; }
        public string gradoApi { get; set; }

        public decimal idEntidad { get; set; } ///// ???????    anhhyd ESCRITO POR BISMARK :)
        public decimal idUbicacionOperacion { get; set; } //idUbicacionOperacion
        public decimal idTipoMedioTransporte { get; set; } // COMO MEDIO O TIPO ??????
        public decimal idMedioTransporte { get; set; }

        public decimal idUnidadEnvasesTransporte { get; set; }
        public decimal cantidadEnvasesTransporte { get; set; }

        public string binarioControl { get; set; }
        public decimal idIntegracion { get; set; }
        public string idTransaccionBaseExterna { get; set; } // EN BD ID_TRANSACCION
        
        public List<ObjFactura> listaFacturas { get; set; }
        public List<ObjDocumento> listaDocumentos { get; set; }
    }

    public class ObjFactura
    {
        public decimal idMovimiento { get; set; }

        public decimal idFactura { get; set; }
        public decimal idFacturaPadre { get; set; }

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaOperacion { get; set; }

        public decimal idTipoTransaccion { get; set; } //factura nota vale

        public decimal fechaHoraEmision { get; set; }
        public string numeroFactura { get; set; }
        public decimal numeroAutorizacion { get; set; }
        public string codigoControl { get; set; }
        public decimal nitConsumidor { get; set; }
        public string razonSocial { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioTotal { get; set; } //importe
        public string usuarioTransaccion { get; set; }
        public string observaciones { get; set; }
        public decimal nitEmisor { get; set; }

        public decimal idEstadoFactura { get; set; } //anulado activo
        public decimal idTipoCreditoFiscal { get; set; } // con o sin credito
        //public decimal idServicioAdicional { get; set; } //tiene, no tiene cual es
        public decimal idFormaPago { get; set; }
        public decimal idTipoMoneda { get; set; }
        public decimal idTipoEmisor { get; set; }//idTipoMovimiento Nacional-Internacional

        public decimal codigoSeguridad { get; set; }//?????????????????
    }

    public class ObjDocumento
    {
        public decimal idMovimiento { get; set; }

        public decimal idDocumento { get; set; }
        public decimal idDocumentoPadre { get; set; }

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaOperacion { get; set; }

        public decimal idTipoTransaccion { get; set; } //factura nota vale documento

        public decimal fechaHoraEmision { get; set; }
        public string numeroDocumento { get; set; }
        public decimal nitDocumento { get; set; }
        public string razonSocial { get; set; }
        public decimal precioUnitario { get; set; }  /***/
        public decimal precioTotal { get; set; }     /***/
        public string usuarioTransaccion { get; set; } /***/
        public string observacion { get; set; }

        public decimal idFormaPago { get; set; }
        public decimal idTipoMoneda { get; set; }
        public decimal idEstadoDocumento { get; set; } //anulado activo
        
        public decimal codigoSeguridad { get; set; }
    }
    
    [Serializable]
    public class EResultado : IReturn<EResultado>
    {
    	public int intCodigo { get; set; }
    	public object oResultado { get; set; }
    	public string strMensaje { get; set; }
    }

    public class EResultadoGestion
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public O_RESULTADO_CTY oResultado { get; set; }
    }

    public class O_RESULTADO_CALIDAD_CTY
    {
        public decimal ID_TABLA { get; set; }
        public string MENSAJE_ERROR { get; set; }
        public Nullable<decimal> RESULTADO { get; set; }
    }    
    
    public class EResultadoI : IReturn<EResultado>
    {
        public decimal decCodigo { get; set; }
        public object oResultado { get; set; }
        public string strMensaje { get; set; }
    } 
    
    public class O_USUARIO_CTY 
    {
    	public decimal ESTADO { get; set; }
        public decimal ID_ENTIDAD { get; set; }
    	public decimal ID_USUARIO { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
    	public string PERFIL { get; set; }
    	public decimal PERFIL_ID { get; set; }
    }

    public class ObjProtoMovimientoTransaccion
    {
        public decimal idMovimientoPadre { get; set; }

        public decimal idTipoActividad { get; set; }
        public decimal idTipoOperacion { get; set; }
        public decimal idConsumidor { get; set; }  // QUIEN A PRODUCIDO EL MOVIMIENTO   anhmov

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaRegistroMovimiento { get; set; }

        public decimal fechaOperacion { get; set; }
        public decimal idTipoMovimientoFlujo { get; set; } //transaccion-fisico /***/
        public decimal idTipoMovimiento { get; set; } //Nacional e internacional
        public decimal idEstadoMovimiento { get; set; }
        public decimal idFuenteOrigenDato { get; set; }

        public decimal idUnidadMedidaHidrocarburo { get; set; }
        public decimal idHidrocarburo { get; set; } //Producto
        public decimal cantidadHidrocarburo { get; set; } // EN BD CANTIDAD	      ???
        public string identificador { get; set; }
        public string subIdentificador { get; set; }
        public string observacion { get; set; }

        public decimal idInfraestructura { get; set; }
        public decimal idTipoMedicion { get; set; }
        public decimal cantidadAguaLitros { get; set; }	  // EN BD CANTIDAD_ADICIONAL ???

        public decimal idUnidadTemperatura { get; set; }
        public decimal temperatura { get; set; }
        public string gradoApi { get; set; }

        public decimal idEntidad { get; set; } ///// ???????    anhhyd ESCRITO POR BISMARK :)
        public decimal idUbicacionOperacion { get; set; } //idUbicacionOperacion
        public decimal idTipoMedioTransporte { get; set; } // COMO MEDIO O TIPO ??????
        public decimal idMedioTransporte { get; set; }

        public decimal idUnidadEnvasesTransporte { get; set; }
        public decimal cantidadEnvasesTransporte { get; set; }

        public string binarioControl { get; set; }
        public decimal idIntegracion { get; set; }
        public string idTransaccionBaseExterna { get; set; } // EN BD ID_TRANSACCION

        public decimal fechaOperacionDia { get; set; }

        public string listaTransaccion { get; set; }

        public string codigo { get; set; }
        public decimal tipoInfraestructura { get; set; }

        public decimal barcode { get; set; }


        public string jsonReferencia { get; set; }
        public decimal estadoJsonRef { get; set; }
        public decimal idCuenta { get; set; }
        public decimal idCuentaAux { get; set; }
        public decimal idParamGlobal { get; set; }
        public decimal idParamEspec { get; set; }
        public decimal idTipoConsumidor { get; set; }
        public decimal idTipoInfraAux { get; set; }
        public decimal idInfraAux { get; set; }


    }

    public class ObjMovimientoTransaccion
    {
        public decimal idMovimiento { get; set; }
        public decimal idMovimientoPadre { get; set; }

        public decimal idTipoActividad { get; set; }
        public decimal idTipoOperacion { get; set; }
        public decimal idConsumidor { get; set; }      		// QUIEN A PRODUCIDO EL MOVIMIENTO   anhmov

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaRegistroMovimiento { get; set; }

        public decimal fechaOperacion { get; set; }
        public decimal idTipoMovimientoFlujo { get; set; } //transaccion-fisico /***/
        public decimal idTipoMovimiento { get; set; } //Nacional e internacional
        public decimal idEstadoMovimiento { get; set; }
        public decimal idFuenteOrigenDato { get; set; }

        public decimal idUnidadMedidaHidrocarburo { get; set; }
        public decimal idHidrocarburo { get; set; } //Producto
        public decimal cantidadHidrocarburo { get; set; } // EN BD CANTIDAD	      ???
        public string identificador { get; set; }
        public string subIdentificador { get; set; }
        public string observacion { get; set; }

        public decimal idInfraestructura { get; set; }
        public decimal idTipoMedicion { get; set; }
        public decimal cantidadAguaLitros { get; set; }	  // EN BD CANTIDAD_ADICIONAL ???

        public decimal idUnidadTemperatura { get; set; }
        public decimal temperatura { get; set; }
        public string gradoApi { get; set; }

        public decimal idEntidad { get; set; } ///// ???????    anhhyd ESCRITO POR BISMARK :)
        public decimal idUbicacionOperacion { get; set; } //idUbicacionOperacion
        public decimal idTipoMedioTransporte { get; set; } // COMO MEDIO O TIPO ??????
        public decimal idMedioTransporte { get; set; }

        public decimal idUnidadEnvasesTransporte { get; set; }
        public decimal cantidadEnvasesTransporte { get; set; }

        public string binarioControl { get; set; }
        public decimal idIntegracion { get; set; }
        public string idTransaccionBaseExterna { get; set; } // EN BD ID_TRANSACCION

        public string codigo { get; set; }
        public decimal tipoInfraestructura { get; set; }

        public decimal barcode { get; set; }

        public List<ObjTransaccion> listaTransaccion { get; set; }

        public string jsonReferencia { get; set; }
        public decimal estadoJsonRef { get; set; }
        public decimal idCuenta { get; set; }
        public decimal idCuentaAux { get; set; }
        public decimal idParamGlobal { get; set; }
        public decimal idParamEspec { get; set; }
        public decimal idTipoConsumidor { get; set; }
        public decimal idTipoInfraAux { get; set; }
        public decimal idInfraAux { get; set; }

    }
    public class ObjTransaccion
    {
        public decimal idMovimiento { get; set; }

        public decimal idTransaccion { get; set; }
        public decimal idTransaccionPadre { get; set; }

        public decimal idUsuarioAplicacion { get; set; }    //I_APP_ID_USUARIO     
        public decimal fechaRegistroTransaccion { get; set; } //I_APP_FECHA_REGISTRO

        public decimal idTipoTransaccion { get; set; }

        public decimal fechaEmision { get; set; }
        public string numeroTransaccion { get; set; } //factura nota vale
        public decimal numeroAutorizacion { get; set; }
        public string codigoAutorizacion { get; set; }  //adicionado en fecha 09-02-2022 
        public string codigoControl { get; set; }
        public decimal nitConsumidor { get; set; }
        public string razonSocial { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioTotal { get; set; } //importe
        public string usuarioTransaccion { get; set; }
        public string observaciones { get; set; }

        public decimal nitEmisor { get; set; }

        public decimal idEstadoTransaccion { get; set; }
        public decimal idTipoCreditoFiscal { get; set; } // con o sin credito
        public decimal idFormaPago { get; set; }
        public decimal idTipoMoneda { get; set; }
        public decimal idTipoEmisor { get; set; }//idTipoMovimiento Nacional-Internacional

        public decimal codigoSeguridad { get; set; }
        public decimal idAutorizacionDato { get; set; }

        public string binarioControl { get; set; }

        public decimal fechaOperacion { get; set; }
        public decimal idActividad { get; set; }
        public string codigo { get; set; }

    }
    [Serializable]
    public class EResultadoSegip : IReturn<EResultado>
    {
        public ERespConsultaFoto RespuestaConsulta { get; set; }
        public string Resultado { get; set; }
    }
    public class ERespConsultaFoto
    {
        public string strNumeroDocumento { get; set; }
        public string strComplemento { get; set; }
        public string strNombres { get; set; }
        public string strPrimerApellido { get; set; }
        public string strSegundoApellido { get; set; }
        public string strApellidoEsposo { get; set; }
        public string strFechaNacimiento { get; set; }
        public string strLugarNacimientoPais { get; set; }
        public string strLugarNacimientoDepartamento { get; set; }
        public string strLugarNacimientoProvincia { get; set; }
        public string strLugarNacimientoLocalidad { get; set; }
        public string strDomicilio { get; set; }
        public string Credencial { get; set; }
        public byte[] btFoto { get; set; }
    }
    public class RespConsultaFoto
    {
        public string Llave { get; private set; }
        public string NumeroDocumento { get; set; }
        public List<ERespConsultaFoto> RespuestaConsulta { get; set; }
        public Exception Resultado { get; set; }
    }

    public class ResultadoConsumidor
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public objResultado oResultado { get; set; }

        public class objResultado
        {
            public string idTransaccion { get; set; }
            public string identificador { get; set; }
            public decimal estado { get; set; }
            public string modelo { get; set; }
            public string marca { get; set; }
            public string explotador { get; set; }
            public List<oProducto> producto { get; set; }
            public List<Mensajes> mensaje { get; set; }
            public List<Imagen> imagen { get; set; }

            public class oProducto
            {
                public decimal idProducto { get; set; }
                public decimal idUnidad { get; set; }
                public decimal precioNac { get; set; }
                public decimal precioInt { get; set; }
                public decimal idTipoRestriccion { get; set; }
                public objConsumo oConsumo { get; set; }
                public class objConsumo
                {
                    public decimal volMaximo { get; set; }
                }
                public string strProducto { get; set; }
            }
            public class Mensajes
            {
                public decimal idTipoMensaje { get; set; }
                public string mensaje { get; set; }
            }
            public class Imagen
            {
                public string TIPO_IMAGEN { get; set; }
                public byte[] IMAGEN { get; set; }
            }
        }


    }

    //public class ObjDjTransaccion
    //{
    //    public decimal fechaOperacion { get; set; }
    //    public decimal idHidrocarburo { get; set; }
    //    public string hidrocarburo { get; set; }
    //    public string codHidrocarburo { get; set; }
    //    public string unidadMedida { get; set; }
    //    public decimal cantidadHidrocarburo { get; set; }
    //    public string identificador { get; set; }
    //    public string subIdentificador { get; set; }
    //    public decimal idUbicacionOperacion { get; set; }
    //    public string ubicacionOperacion { get; set; } 

    //    public decimal idMovimiento { get; set; }
    //    public decimal idTransaccion { get; set; }

    //    public decimal idUsuarioAplicacion { get; set; }
    //    public decimal fechaRegistroTransaccion { get; set; }
    //    public decimal idTipoTransaccion { get; set; }
    //    public string tipoTransaccion { get; set; }
    //    public decimal fechaEmision { get; set; }
    //    public string numeroTransaccion { get; set; }
    //    public decimal numeroAutorizacion { get; set; }
    //    public string codigoControl { get; set; }
    //    public decimal nitConsumidor { get; set; }
    //    public string razonSocial { get; set; }
    //    public decimal precioUnitario { get; set; }
    //    public decimal precioTotal { get; set; }
    //    public decimal idEstadoTransaccion { get; set; }
    //    public string EstadoTransaccion { get; set; }
    //    public decimal idTipoCreditoFiscal { get; set; }
    //    public string tipoCreditoFiscal { get; set; }
    //    public decimal idFormaPago { get; set; }
    //    public string formaPago { get; set; }
    //    public decimal idTipoMoneda { get; set; }
    //    public decimal idTipoEmisor { get; set; }
    //    public string tipoEnvase { get; set; }
    //}

}
