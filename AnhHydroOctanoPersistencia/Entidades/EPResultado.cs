using System;
using System.Collections.Generic;

namespace AnhHydroOctanoPersistencia.Entidades
{
    public class EPResultado
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public object oResultado { get; set; }
    }

    public class ERepMesResultado
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_LISTA_REPORTE_MENSUAL_CTY> oResultado { get; set; }
    }

    public class O_DATO_RESULTADO_CTY
    {
        public decimal ID_TABLA { get; set; }
        public decimal RESULTADO { get; set; }
        public string MENSAJE_ERROR { get; set; }
    }

    public class O_DATOS_ADICIONAL_DDJJ_CTY
    {
        public decimal ID_ENTIDAD { get; set; }
        public decimal ID_ENTIDAD_PADRE { get; set; }
        public decimal ID_AMBITO_OPERACION { get; set; }
        public string AMBITO_OPERACION { get; set; }
        public decimal ID_AMBITO_OPERACION_PADRE { get; set; }
        public string AMBITO_OPERACION_PADRE { get; set; }
        public decimal ID_TIPO_SOCIEDAD { get; set; }
        public string TIPO_SOCIEDAD { get; set; }
        public decimal ID_TIPO_SOCIEDAD_PADRE { get; set; }
        public string TIPO_SOCIEDAD_PADRE { get; set; }
        public decimal ID_MUNICIPIO { get; set; }
        public string MUNICIPIO { get; set; }
        public decimal ID_MUNICIPIO_PADRE { get; set; }
        public string MUNICIPIO_PADRE { get; set; }
        public decimal ID_LOCALIDAD { get; set; }
        public string LOCALIDAD { get; set; }
        public decimal ID_LOCALIDAD_PADRE { get; set; }
        public string LOCALIDAD_PADRE { get; set; }
        public string NOMBRES_PROPIETARIO { get; set; }
        public string CI_PROPIETARIO { get; set; }
        public string REPRESENTANTE_LEGAL { get; set; }
    }

    public class O_SALDO_INICIAL_DIA_CTY
    {
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public string CODIGO { get; set; }
        public decimal ID_UMEDIDA { get; set; }
        public string UMEDIDA { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public string TIPO_INF { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public string INFRAESTRUCTURA { get; set; }
        public string INFRAESTRUCTURA_ANH { get; set; }
        public decimal SALDO { get; set; }
        public string OBSERVACIONES { get; set; }

    }

    public class O_OBTENER_MOVIMIENTO_ACUM_CTY
    {
        public decimal FECHA_OPERACION_DIA { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal ID_MOVIMIENTO { get; set; }
        public string OBSERVACIONES { get; set; }
    }

    public class O_OBTENER_MOVIMIENTO_ACUM_SAL
    {
        public decimal FECHA_OPERACION_DIA { get; set; }
        public decimal DIA { get; set; }
        public string DESCRIPCION_DIA { get; set; }
        public string MES { get; set; }
        public string GESTION { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public string OBSERVACIONES { get; set; }
        public string DESCRIPCION_TANQUES { get; set; }
        public decimal VOLUMEN_NOMINAL { get; set; }
        public decimal PROGRAMACION { get; set; }
        public decimal RECEPCION_PROGRAMACION { get; set; }
        public decimal VENTA_PROGRAMACION { get; set; }
    }

    public class O_OBTENER_TIPO_OPE_ACUM_CTY
    {
        public decimal ID_TIPO_OPERACION { get; set; }
        public string OPERACION_NOMBRE { get; set; }
        public decimal OPERACION_ORDEN { get; set; }
        public string REPORTE_NOMBRE { get; set; }
        public decimal REPORTE_ORDEN { get; set; }
        public decimal FACTOR_TIPO { get; set; }
        public string OBSERVACIONES { get; set; }
    }
    
    public class O_CURSORES_DDJJ
    {
        public List<O_SALDO_INICIAL_DIA_CTY> lstSaldos { get; set; }
        public List<O_OBTENER_MOVIMIENTO_ACUM_CTY> lstMovimientos { get; set; }
        public List<O_OBTENER_TIPO_OPE_ACUM_CTY> lstTipoOp { get; set; }
    }
    public class O_CURSORES_DDJJ_SAL
    {
        public List<O_SALDO_INICIAL_DIA_CTY> lstSaldos { get; set; }
        public List<O_OBTENER_MOVIMIENTO_ACUM_SAL> lstMovimientos { get; set; }
        public List<O_OBTENER_TIPO_OPE_ACUM_CTY> lstTipoOp { get; set; }
    }
    public class O_OBTIENE_DATO_PERSONA_CTY
    {
        public decimal ID_PERSONA { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO_1 { get; set; }
        public string APELLIDO_2 { get; set; }
        public string FECHA_NACIMIENTO { get; set; }
        public string CI { get; set; }
        public string COMPLEMENTO { get; set; }
        public decimal EXPEDICION_ID { get; set; }
        public string EXPEDICION { get; set; }
        public decimal GENERO_ID { get; set; }
        public string GENERO { get; set; }
        public string EMAIL { get; set; }
        public decimal ID_TIPO_IDENTIFICACION { get; set; }
        public string TIPO_IDENT_DESCRIPCION { get; set; }
    }

    public class O_OBTIENE_ENTIDADES_CTY
    {
        public string NIT { get; set; }//*
        public Nullable<decimal> ID_ENTIDAD_PADRE { get; set; }//*
        public string DENOMINACION { get; set; }//*
        public string DIRECCION { get; set; }//*
        public Nullable<decimal> LATITUD { get; set; }//*
        public Nullable<decimal> LONGITUD { get; set; }//*
        public string ACTIVIDAD { get; set; }//*
        public string NRO_LICENCIA { get; set; }
        public string REPRESENTANTE_LEGAL { get; set; }//*
        public string DEPARTAMENTO { get; set; }//*
        public decimal ID_ENTIDAD { get; set; }//*
        public string VIGENCIA { get; set; }
        public string DENOMINACION_PADRE { get; set; }//*
        public decimal ID_ACTIVIDAD { get; set; }//*
        public decimal ID_CONSUMIDOR { get; set; }//*
        public Nullable<decimal> ID_MUNICIPIO { get; set; }
        public Nullable<decimal> ID_LOCALIDAD { get; set; }
        public Nullable<decimal> ID_DEPARTAMENTO { get; set; }//*
        public Nullable<decimal> ID_MUNICIPIO_PADRE { get; set; }
        public Nullable<decimal> ID_LOCALIDAD_PADRE { get; set; }
        public Nullable<decimal> ID_TIPO_SOCIEDAD { get; set; }
        public Nullable<decimal> ID_TIPO_SOCIEDAD_PADRE { get; set; }
        public string MUNICIPIO { get; set; }
        public string LOCALIDAD { get; set; }
        public string MUNICIPIO_PADRE { get; set; }
        public string LOCALIDAD_PADRE { get; set; }
        public string OBJETO { get; set; }//*
        public string TELEFONOS { get; set; }//*
        public string AMBITO_OPERACION { get; set; }
        public string AMBITO_OPERACION_PADRE { get; set; }
        public string TIPO_SOCIEDAD { get; set; }
        public string TIPO_SOCIEDAD_PADRE { get; set; }
        public string NOMBRES_PROPIETARIO { get; set; }//*
        public string CI_PROPIETARIO { get; set; }//*
        public string NOMBRES_REPRESENTANTE { get; set; }//*
        public string CI_REPRESENTANTE { get; set; }//*
        public Nullable<decimal> ID_USUARIO_ASIGNADO { get; set; }
        public string USUARIO_ASIGNADO { get; set; }
        public string LIC_VENCIMIENTO { get; set; }
        public string LIC_PROD { get; set; }
        public string LIC_ID_PROD { get; set; }
        public string INF_PROV_SW { get; set; }
        public string NRO_HYDRO { get; set; }
    }

    public class O_LISTA_REPORTE_DOSIF_CAB_CTY
    {
        public string TIPO_OPERACION { get; set; }
        //public decimal NRO_AUTORIZACION { get; set; }
        public string NRO_AUTORIZACION { get; set; }
        public string TIPO_EMISOR { get; set; }
        public string TIPO_FACTURACION { get; set; }
        public decimal MONTO { get; set; }
        public decimal FACTOR_CALCULO { get; set; }
    }

    public class O_LISTA_REPORTE_DOSIF_DET_CTY
    {
        public string FECHA_FACTURA { get; set; }
        public string FECHA_FACTURA_DIA { get; set; }
        public string FECHA_MOVIMIENTO { get; set; }
        public decimal NIT { get; set; }
        public string RAZON_SOCIAL { get; set; }
        //public decimal NRO_AUTORIZACION { get; set; }
        public string NRO_AUTORIZACION { get; set; }
        public string NRO_FACTURA { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public string ESTADO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO { get; set; }
        public decimal IMPORTE_DOC { get; set; }
        public decimal IMPORTE { get; set; }
        public string ACTIVIDAD { get; set; }
        public string PROYECTO { get; set; }
        public decimal ID_ACTIVIDAD { get; set; }
        public decimal ID_PROYECTO { get; set; }
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_TIPO_EMISOR { get; set; }
    }

    public class O_LISTA_REPORTE_DOSIF_GRAL_CTY
    {
        //public decimal NRO_AUTORIZACION { get; set; }
        public string NRO_AUTORIZACION { get; set; }
        public decimal NO_MIN { get; set; }
        public decimal NO_MAX { get; set; }
        public decimal NO_REG_REAL { get; set; }
        public decimal NO_REG_CALC { get; set; }
        public decimal DIFERENCIA { get; set; }
        public string ESTADO { get; set; }
        public decimal DIF_VAL { get; set; }
        public decimal PORCENTAJE { get; set; }

    }

    public class O_LISTA_MOV_CSV_EESSDS_CTY
    {
        public string FECHA { get; set; }
        public string TIPO_INFRA { get; set; }
        public string INFRAESTRUCTURA { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string COPIA_PLACA { get; set; }
        public decimal CODIGO_BARRA { get; set; }
        public string PAIS { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string ESTADO { get; set; }
        public string USUARIO { get; set; }
        public string FORMA_PAGO { get; set; }
        public decimal NIT { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string FACT_NO { get; set; }
        public string FACT_NO_AUTORIZACION { get; set; }
        public string FACT_CODIGO_CONTROL { get; set; }
        public decimal FACT_PRECIO { get; set; }
        public decimal FACT_IMPORTE { get; set; }

        public string VALE_NO { get; set; }
        public decimal VALE_PRECIO { get; set; }
        public decimal VALE_IMPORTE { get; set; }
        public string VALE_RESPONSABLE { get; set; }
        public decimal VALE_CI_RESPONSABLE { get; set; }

        public string FACTEX_NO { get; set; }
        public decimal FACTEX_NO_AUTORIZACION { get; set; }
        public string FACTEX_CODIGO_CONTROL { get; set; }
        public decimal FACTEX_PRECIO { get; set; }
        public decimal FACTEX_IMPORTE { get; set; }

        public string NMOV_NO { get; set; }

        public string SRAD_TIPO { get; set; }
        public string SRAD_NOREF { get; set; }
        public decimal SRAD_IMPORTE { get; set; }

        public string AUVT_TIPO { get; set; }
        public string AUVT_NOREF { get; set; }

    }

    public class O_LISTA_MOV_CSV_EESSRC_CTY
    {
        public string FECHA { get; set; }
        public string PROYECTO_ORIGEN { get; set; }
        public string TIPO_INFRA { get; set; }
        public string INFRAESTRUCTURA { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string COPIA_PLACA { get; set; }
        public decimal CODIGO_BARRA { get; set; }
        public string CODIGO_TKCS { get; set; }
        public string PRECINTOS { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string ESTADO { get; set; }
        public string USUARIO { get; set; }
        public decimal NIT { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string FACT_NO { get; set; }
        public string FACT_NO_AUTORIZACION { get; set; }
        public string FACT_CODIGO_CONTROL { get; set; }
        public decimal FACT_PRECIO { get; set; }
        public decimal FACT_IMPORTE { get; set; }

        public string NMOV_NO { get; set; }
        public string NMOV_RESPONSABLE { get; set; }
        public decimal NMOV_CI_RESP { get; set; }

        public decimal ACL_NO { get; set; }

        public string DDES_FECHA { get; set; }
        public string DDES_NO { get; set; }
        public decimal DDES_NOOR { get; set; }
        public string DDES_CI_COND { get; set; }
        public decimal DDES_VLPL { get; set; }

    }

    public class O_LISTA_SALDOS_DCOD_CTY
    {
        public int ID_MOVIMIENTO { get; set; }
        public DateTime PERIODO { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public decimal SALDO { get; set; }
    }

    public class O_LISTA_RECEPCION_DCOD_CTY
    {
        public decimal ID_RECEPCION { get; set; }
        public decimal F_OPERACION { get; set; }
        public string PLACA { get; set; }
        public string PRECINTO { get; set; }
        public string NRO_ORDEN { get; set; }
        public string HOJA_RUTA { get; set; }
        public decimal FECHA_FACTURA { get; set; }
        public decimal NUMERO_FACTURA { get; set; }
        public decimal LITROS_FACTURA { get; set; }
        public decimal ID_COMBUSTIBLE { get; set; }
    }

    public class O_CONSULTA_MOVIM_TRAN_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_MOVIMIENTO_PADRE { get; set; }

        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }

        public decimal FECHA_OPERACION { get; set; }

        public decimal ID_ESTADO_MOVIMIENTO { get; set; }
        public string ESTADO_MOVIMIENTO { get; set; }

        public decimal ID_FUENTE_ORIGEN { get; set; }
        public string COD_FUENTE_ORIGEN { get; set; }
        public string DES_FUENTE_ORIGEN { get; set; }

        public decimal ID_ENTIDAD { get; set; }
        public decimal ID_TIPO_MEDICION { get; set; }
        public decimal ID_TIPO_MEDIO_TRANSPORTE { get; set; }
        public string TIPO_MEDIO_TRANSAPORTE { get; set; }
        public decimal ID_MEDIO_TRANSPORTE { get; set; }

        public decimal ID_UBICACION_OPERACION { get; set; }
        public string UBICACION_OPERACION { get; set; }

        public decimal ID_INFRAESTRUCTURA { get; set; }

        public decimal ID_TIPO_FLUJO { get; set; }
        public string TIPO_FLUJO { get; set; }

        public decimal ID_TIPO_MOVIMIENTO { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }

        public decimal CANTIDAD { get; set; }

        public decimal ID_HIDROCARBURO { get; set; }
        public string COD_HIDROCARBURO { get; set; }
        public string DES_HIDROCARBURO { get; set; }

        public decimal ID_UNIDAD_HIDROCARBURO { get; set; }

        public string IDENTIFICADOR { get; set; }
        public string SUB_IDENTIFICADOR { get; set; }

        public decimal ID_TIPO_TRANSACCION { get; set; }
        public string TIPO_TRANSACCION { get; set; }

        public decimal NIT_CONSUMIDOR { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string NRO_TRANSACCION { get; set; }
        public decimal NRO_AUTORIZACION { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }

        public decimal TEMPERATURA { get; set; }
        public decimal ID_UNIDAD_TEMPERATURA { get; set; }
        public string GRADO_API { get; set; }
        public decimal ID_UNIDAD_ENVASES { get; set; }
        public decimal CANTIDAD_ENVASES { get; set; }
        public decimal CANTIDAD_ADICIONAL { get; set; }

        public string APP_USUARIO { get; set; }

        public string OBSERVACION_MOVIMIENTO { get; set; }
        public string BINARIO_CONTROL { get; set; }

        //public decimal APP_FECHA_REGISTRO { get; set; }
        public DateTime APP_FECHA_REGISTRO { get; set; }
        public decimal FECHA_OPERACION_DIA { get; set; }

        public string STR_ESTADO_DEC { get; set; }
        public string STR_FECHA { get; set; }

        public string CODIGO { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public decimal ID_INTEGRACION { get; set; }

        public string BINARIO_CONTROL_TRAN { get; set; }
        public decimal BARCODE { get; set; }

        public string JSON_REFERENCIA { get; set; }
        public decimal ESTADO_JSON_REFERENCIA { get; set; }
        public decimal ID_CUENTA { get; set; }
        public decimal ID_CUENTA_AUX { get; set; }
        public decimal ID_PARAM_GLOBAL { get; set; }
        public decimal ID_PARAM_ESPEC { get; set; }
        public decimal ID_TIPO_CONSUMIDOR { get; set; }
        public decimal ID_TIPO_INFRA_AUX { get; set; }
        public decimal ID_INFRA_AUX { get; set; }
        public decimal PRECIO_UNITARIO_TRANS { get; set; }
        public decimal PRECIO_TOTAL_TRANS { get; set; }
        public decimal CANTIDAD_TRANS { get; set; }
        public string CODIGO_CUF { get; set; }

    }

    public class O_LISTA_REPORTE_MOVGRAL_CTY
    {
        public Nullable<decimal> FECHA { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public string TIPO_OPERACION { get; set; }
        public decimal CANTIDAD { get; set; }
    }
    public class O_JSON_REFERENCIA
    {
        public string StrCodUsuario   { get; set; }
        public string DecCodMedidor { get; set; }
        public string StrCodUbicacion     { get; set; }
        public string StrCiudadPoblacion       { get; set; }
        public string StrCategoria    { get; set; }
        public string DecNit   { get; set; }
        public string StrRazonSocial      { get; set; }
        public string StrDireccion     { get; set; }
        public string StrSectorEcon    { get; set; }
        public string StrTipoMedidor   { get; set; }
        public DateTime StrFecha        { get; set; }
        public string DecCantLectura        { get; set; }
        public string DecTemperatura         { get; set; }
        public string DecPresionPsig         { get; set; }
        public string DecFactorCorrec  { get; set; }
        public string DecCantFactura   { get; set; }
        public string DecCantNoCorregido   { get; set; }
        public string DecImportAdicional   { get; set; }
        public string DecNroFactura  { get; set; }
        public string DecNroAutorizacion   { get; set; }
        public string StrCodigoControl   { get; set; }
        public DateTime StrFechaFactura      { get; set; }
        public string LngId   { get; set; }
        public string DecLecMedidor   { get; set; }
        public string StrProducto     { get; set; }
        public string DecPrecio  { get; set; }
        public string DecNroComprobante  { get; set; }
        public string DecNroReferencia   { get; set; }
    }

    public class O_DECLARACION_JURADA_CTY
    {
        public List<CDjAcumulado> LstAcumulado { get; set; }
        public List<ObjDjTransaccion> LstaAdicional1 { get; set; }
        public List<ObjDjTransaccion> LstaAdicional2 { get; set; }
        public List<O_RECAUDO_EXTERNO_CTY> LstaRecaudoExt { get; set; }
        public List<O_CONSOLIDACION_SUIC_ACU_CTY> LstaConsSuic { get; set; }
    }
    public class O_DECLARACION_JURADA_SAL
    {
        public List<CDjAcumulado_SAL> LstAcumulado { get; set; }
        public List<ObjDjTransaccion> LstaAdicional1 { get; set; }
        public List<ObjDjTransaccion> LstaAdicional2 { get; set; }
        public List<O_RECAUDO_EXTERNO_CTY> LstaRecaudoExt { get; set; }
        public List<O_CONSOLIDACION_SUIC_ACU_CTY> LstaConsSuic { get; set; }
    }
    public class O_MOV_DETALLE_DDJJ_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_INTEGRACION { get; set; }
        public string ID_TRANSACCION { get; set; }
        public string DEPTO { get; set; }
        public string PROYECTO { get; set; }
        public string FECHA_OPERACION_DIA { get; set; }
        public DateTime FECHA_OPERACION { get; set; }
        public string PROY_ORIGEN { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public string INF_TIPO { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public string INF_CODIGO { get; set; }
        public string TIPO_DESPACHO { get; set; }
        public string ENVASE { get; set; }
        public decimal CANTIDAD_ENVASES { get; set; }
        public string ESTADO { get; set; }
        public string TIPO_FLUJO { get; set; }
        public string TIPO_MOV { get; set; }
        public string PAIS { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string SUB_IDENTIFICADOR { get; set; }
        public string CODIGO { get; set; }
        public decimal BARCODE { get; set; }
        public string GRADO_API { get; set; }
        public string UTEMP { get; set; }
        public decimal TEMPERATURA { get; set; }
        public string PRODUCTO { get; set; }
        public string UMEDIDA { get; set; }
        public decimal CANTIDAD { get; set; }
        public string TIPO_MEDICION { get; set; }
        public decimal CANTIDAD_ADICIONAL { get; set; }
        public decimal ID_CUENTA { get; set; }
        public decimal ID_FUENTE_ORIGEN { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string APP_FECHA_REGISTRO { get; set; }
        public string DOC_FACT_PR { get; set; }
        public string DOC_FACT_EX { get; set; }
        public string DOC_NMOV { get; set; }
        public string AUT_SCTRL { get; set; }
        public string DOC_SVCADIC { get; set; }
        public string DOC_PRECINTO { get; set; }
        public string DOC_CALIDAD { get; set; }
        public string DOC_PVUELO { get; set; }
        public string DOC_CETREC { get; set; }
        public string DOC_LOTE { get; set; }
        public string DOC_VALE { get; set; }
        public string DOC_FACREF { get; set; }
        public string DOC_DESP { get; set; }
        public string AUT_ANHFA { get; set; }
        public string DOC_MED { get; set; }
        public string DOC_CTA { get; set; }
        public string DOC_PRTSAL { get; set; }
        public decimal ID_CITE { get; set; }
        public string CITE { get; set; }



        public decimal FP_NIT { get; set; }
        public string FP_RS { get; set; }
        public string FP_FP { get; set; }
        public decimal FP_NRO { get; set; }
        public decimal FP_NAUT { get; set; }
        public string FP_CCTRL { get; set; }
        public decimal FP_PRECIO { get; set; }
        public decimal FP_IMPORTE { get; set; }

        public decimal FE_NIT { get; set; }
        public string FE_RS { get; set; }
        public string FE_FP { get; set; }
        public decimal FE_NRO { get; set; }
        public decimal FE_NAUT { get; set; }
        public string FE_CCTRL { get; set; }
        public decimal FE_PRECIO { get; set; }
        public decimal FE_IMPORTE { get; set; }

        public string SA_TIPO { get; set; }
        public decimal SA_NRO { get; set; }
        public decimal SA_PRECIO { get; set; }
        public decimal SA_IMPORTE { get; set; }

        public decimal FR_NIT { get; set; }
        public string FR_RS { get; set; }
        public string FR_FP { get; set; }
        public decimal FR_NRO { get; set; }
        public decimal FR_NAUT { get; set; }
        public string FR_CCTRL { get; set; }
        public decimal FR_PRECIO { get; set; }
        public decimal FR_IMPORTE { get; set; }

        public decimal DEC_GESTION { get; set; }
        public string STR_MES { get; set; }
        public string STR_PERIODO { get; set; }


    }

    public class O_MOV_REPORTE_REC_EXTERNO_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_INTEGRACION { get; set; }
        public string ID_TRANSACCION { get; set; }
        public string DEPTO { get; set; }
        public string PROYECTO { get; set; }
        //public string FECHA_OPERACION_DIA { get; set; }
        public DateTime FECHA_OPERACION { get; set; }
        //public string PROY_ORIGEN { get; set; }
        //public decimal TIPO_INFRAESTRUCTURA { get; set; }
        //public string INF_TIPO { get; set; }
        //public decimal ID_INFRAESTRUCTURA { get; set; }
        //public string INF_CODIGO { get; set; }
        //public string TIPO_DESPACHO { get; set; }
        //public string ENVASE { get; set; }
        //public decimal CANTIDAD_ENVASES { get; set; }
        public string ESTADO { get; set; }
        public string TIPO_FLUJO { get; set; }
        public string TIPO_MOV { get; set; }
        public string PAIS { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string SUB_IDENTIFICADOR { get; set; }
        public string CODIGO { get; set; }
        public decimal BARCODE { get; set; }
        //public string GRADO_API { get; set; }
        //public string UTEMP { get; set; }
        //public decimal TEMPERATURA { get; set; }
        public string PRODUCTO { get; set; }
        public string UMEDIDA { get; set; }
        public decimal CANTIDAD { get; set; }
        //public string TIPO_MEDICION { get; set; }
        //public decimal CANTIDAD_ADICIONAL { get; set; }
        //public decimal ID_CUENTA { get; set; }
        public decimal ID_FUENTE_ORIGEN { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string APP_FECHA_REGISTRO { get; set; }
        public string DOC_FACT_PR { get; set; }
        public string DOC_SVCADIC { get; set; }
        public decimal ID_CITE { get; set; }
        public string CITE { get; set; }


        public decimal FP_NIT { get; set; }
        public string FP_RS { get; set; }
        public string FP_FP { get; set; }
        public decimal FP_NRO { get; set; }
        public decimal FP_NAUT { get; set; }
        public string FP_CCTRL { get; set; }
        public decimal FP_PRECIO { get; set; }
        public decimal FP_IMPORTE { get; set; }


        public string SA_TIPO { get; set; }
        public decimal SA_NRO { get; set; }
        public decimal SA_PRECIO { get; set; }
        public decimal SA_IMPORTE { get; set; }

        public decimal DEC_GESTION { get; set; }
        public string STR_MES { get; set; }
        public string STR_PERIODO { get; set; }

    }
    public class O_CONSUMO_GRAL_CTY
    {
        public decimal ID { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public string DENOMINACION { get; set; }
        public decimal NIT_EMISOR { get; set; }
        public decimal ID_DEPARTAMENTO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public decimal NIT_CONSUMIDOR { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public string NRO_TRANSACCION { get; set; }
        public string ID_TIPO_TRANSACCION { get; set; }
        public string TIPO_DOC { get; set; }
        public DateTime DAT_FECHA { get; set; }     
        public DateTime FECHA_DJ { get; set; }
        public string CITE_GENERADO { get; set; }
        public string TIPO_CONSUMIDOR { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string UMEDIDA { get; set; }
        public string MUNICIPIO { get; set; }
        public string USUARIO_TRANSACCION { get; set; }
    }

    public class O_CONSOLIDACION_SUIC_ACU_CTY
    {
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_UNIDAD_MEDIDA { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public string MES { get; set; }
        public decimal TOT_VOLUMEN { get; set; }
        public decimal CANT_FACT { get; set; }
        public decimal TOT_IMPORTE { get; set; }
        public decimal ID { get; set; }
        public string STR_MES { get; set; }
    }

    public class O_CONSOLIDACION_SUIC_DET_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public decimal ID_INTEGRACION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_UNIDAD_MEDIDA { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public decimal FACT_NEM { get; set; }
        public decimal FACT_NIT { get; set; }
        public string FACT_RSO { get; set; }
        public decimal FACT_NAU { get; set; }
        public string FACT_NRO { get; set; }
        public string FACT_CCT { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal FACT_PRECIO { get; set; }
        public decimal FACT_IMPORTE { get; set; }
        public string SUIC_NSR { get; set; }
        public decimal SUIC_NDC { get; set; }
        public decimal SIUC_PRU { get; set; }
        public decimal SIUC_IMP { get; set; }
        public decimal ID { get; set; }
        public string STR_FECHA { get; set; }
    }

    public class CDjAcumulado
    {
        public decimal DecFecha { get; set; }
        public decimal DecIdUnicoHidrocarburo { get; set; }
        public decimal DecIdHidrocarburo { get; set; }
        public string StrHidrocarburo { get; set; }
        public string StrCodHidrocarburo { get; set; }
        public string StrUnidadMedida { get; set; }
        public decimal DecIdUnidadMedida { get; set; }

        public decimal DecIdTipoInfraestructura { get; set; }
        public string StrTipoInfraestructura { get; set; }
        public decimal DecIdInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrInfraestructuraAnh { get; set; }
        public string StrObservaciones { get; set; }

        public decimal DecSaldoInicial { get; set; }
        public decimal DecTotalIngreso { get; set; }
        public decimal DecTotalEgreso { get; set; }
        public decimal DecSaldoFinal { get; set; }
        public decimal DecTotalImporte { get; set; }
        public List<O_OP_SALDOS_CTY> LstIngresos { get; set; }
        public List<O_OP_SALDOS_CTY> LstEgresos { get; set; }
    }
    public class CDjAcumulado_SAL
    {
        public decimal DecFecha { get; set; }
        public decimal DecIdUnicoHidrocarburo { get; set; }
        public decimal DecIdHidrocarburo { get; set; }
        public string StrHidrocarburo { get; set; }
        public string StrCodHidrocarburo { get; set; }
        public string StrUnidadMedida { get; set; }
        public decimal DecIdUnidadMedida { get; set; }

        public decimal DecIdTipoInfraestructura { get; set; }
        public string StrTipoInfraestructura { get; set; }
        public decimal DecIdInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrInfraestructuraAnh { get; set; }
        public string StrObservaciones { get; set; }

        public decimal DecSaldoInicial { get; set; }
        public decimal DecTotalIngreso { get; set; }
        public decimal DecTotalEgreso { get; set; }
        public decimal DecSaldoFinal { get; set; }
        public decimal DecTotalImporte { get; set; }

        public decimal DecDia { get; set; }
        public string StrDescripcionDia { get; set; }
        public string StrMes { get; set; }
        public string StrAnio { get; set; }
        public string StrDescripcionTanque { get; set; }
        public decimal DecVolumenNominal { get; set; }
        public decimal DecProgramacion { get; set; }
        public decimal DecRecepProg { get; set; }
        public decimal DecVentaProg { get; set; }

        public List<O_OP_SALDOS_SAL> LstIngresos { get; set; }
        public List<O_OP_SALDOS_SAL> LstEgresos { get; set; }
    }
    public class O_OP_SALDOS_CTY
    {
        public string OPERACION_NOMBRE { get; set; }
        public decimal OPERACION_ORDEN { get; set; }
        public string REPORTE_NOMBRE { get; set; }
        public decimal REPORTE_ORDEN { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal FACTOR_TIPO { get; set; }
        public decimal FECHA_OPERACION_DIA { get; set; }
    }
    public class O_OP_SALDOS_SAL
    {
        public string OPERACION_NOMBRE { get; set; }
        public decimal OPERACION_ORDEN { get; set; }
        public string REPORTE_NOMBRE { get; set; }
        public decimal REPORTE_ORDEN { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal FACTOR_TIPO { get; set; }
     
        public decimal FECHA_OPERACION_DIA { get; set; }
        public string DIA { get; set; }
        public string MES { get; set; }
        public string GESTION { get; set; }
        public string DESCRIPCION_TANQUES { get; set; }
        public decimal VOLUMEN_NOMINAL { get; set; }

    }

    public class O_RECAUDO_EXTERNO_CTY
    {
        public decimal ID_TIPO_TRANSACCION { get; set; }
        public string TIPO_TRANSACCION { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_UNIDAD_MEDIDA { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public decimal CANTIDAD_FACTURA { get; set; }
        public decimal IMPORTE_TOTAL { get; set; }
    }

    public class ObjDjTransaccion
    {
        public decimal fechaOperacion { get; set; }
        public decimal idHidrocarburo { get; set; }
        public string hidrocarburo { get; set; }
        public string codHidrocarburo { get; set; }
        public string unidadMedida { get; set; }
        public decimal cantidadHidrocarburo { get; set; }
        public string identificador { get; set; }
        public string subIdentificador { get; set; }
        public decimal idUbicacionOperacion { get; set; }
        public string ubicacionOperacion { get; set; }

        public decimal idMovimiento { get; set; }
        public decimal idTransaccion { get; set; }

        public decimal idUsuarioAplicacion { get; set; }
        public decimal fechaRegistroTransaccion { get; set; }
        public decimal idTipoTransaccion { get; set; }
        public string tipoTransaccion { get; set; }
        public decimal fechaEmision { get; set; }
        public string numeroTransaccion { get; set; }
        public decimal numeroAutorizacion { get; set; }
        public string codigoControl { get; set; }
        public decimal nitConsumidor { get; set; }
        public string razonSocial { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioTotal { get; set; }
        public decimal idEstadoTransaccion { get; set; }
        public string EstadoTransaccion { get; set; }
        public decimal idTipoCreditoFiscal { get; set; }
        public string tipoCreditoFiscal { get; set; }
        public decimal idFormaPago { get; set; }
        public string formaPago { get; set; }
        public decimal idTipoMoneda { get; set; }
        public decimal idTipoEmisor { get; set; }
        public string tipoEnvase { get; set; }
        public string tipoMovimiento { get; set; }
    }

    public class O_LISTA_REGISTROS_DDJJ_CTY
    {
        public decimal DEC_TIPO { get; set; }
        public string STR_MENSAJE { get; set; }
        public string JSON_REPORTE_DATOS { get; set; }
    }

    public class O_OBTENER_AERONAVES_CTY
    {
        public string TIPO_MTR { get; set; }
        public decimal ID_EXPLOTADOR { get; set; }
        public string EXPLOTADOR { get; set; }
        public string PAIS_EXPLOTADOR { get; set; }
        public string CIUDAD_EXPLOTADOR { get; set; }
        public string DOMICILIO_EXPLOTADOR { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string LUGAR_REGISTRO { get; set; }
        public string USUARIO_NOMBRE { get; set; }
        public decimal IMEI { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string SUB_IDENTIFICADOR { get; set; }
        public decimal BARCODE { get; set; }
        public string CLASE { get; set; }
        public string MARCA { get; set; }
        public string SERVICIO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public decimal GESTION_FABRICACION { get; set; }
        public decimal CARGA { get; set; }
        public string RA_TIPO_MATRICULA { get; set; }
        public string BASE_OPERACIONES { get; set; }
        public string CHASIS { get; set; }
        public decimal ESTADO_REPOSICION { get; set; }
        public decimal ESTADO_BLOQUEO { get; set; }
        public string MENSAJE_IMPRESION { get; set; }
        public string MENSAJE_PANTALLA { get; set; }
        public string ESTACION { get; set; }
        public decimal ID_PROPIETARIO { get; set; }
        public string PROPIETARIO { get; set; }
        public string PAIS_PROPIETARIO { get; set; }
        public string CIUDAD_PROPIETARIO { get; set; }
        public string DOMICILIO_PROPIETARIO { get; set; }
        public string ESTADO { get; set; }
        public string BLOQUEO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public decimal TOTAL_CXC { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }

    }

    public class O_OBTENER_DATO_AERONAVES_CTY
    {
        public string TIPO_MTR { get; set; }
        public decimal ID_EXPLOTADOR { get; set; }
        public string EXPLOTADOR { get; set; }
        public string PAIS_EXPLOTADOR { get; set; }
        public string CIUDAD_EXPLOTADOR { get; set; }
        public string DOMICILIO_EXPLOTADOR { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string LUGAR_REGISTRO { get; set; }
        public string USUARIO_NOMBRE { get; set; }
        public decimal IMEI { get; set; }
        public string TIPO_USUARIO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string SUB_IDENTIFICADOR { get; set; }
        public string BARCODE { get; set; }
        public string CLASE { get; set; }
        public string MARCA { get; set; }
        public string SERVICIO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public decimal GESTION_FABRICACION { get; set; }
        public decimal CARGA { get; set; }
        public string RA_TIPO_MATRICULA { get; set; }
        public string BASE_OPERACIONES { get; set; }
        public string CHASIS { get; set; }
        public decimal ESTADO_REPOSICION { get; set; }
        public decimal ESTADO_BLOQUEO { get; set; }
        public string MENSAJE_IMPRESION { get; set; }
        public string MENSAJE_PANTALLA { get; set; }
        public string ESTACION { get; set; }
        public decimal ID_PROPIETARIO { get; set; }
        public string PROPIETARIO { get; set; }
        public string PAIS_PROPIETARIO { get; set; }
        public string CIUDAD_PROPIETARIO { get; set; }
        public string DOMICILIO_PROPIETARIO { get; set; }
        public string ESTADO { get; set; }
        public string BLOQUEO { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public decimal TOTAL_CXC { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }

    }

    public class O_IMAGEN_MTR_CTY
    {
        public string TIPO_IMAGEN { get; set; }
        public byte[] IMAGEN { get; set; }
    }

    public class O_OBTENER_AERONAVE_EXTR_CTY
    {
        public decimal ID_AERONAVE_EXTRANJERA { get; set; }
        public string PROPIETARIO { get; set; }
        public string PROPIETARIO_DOC { get; set; }
        public decimal ID_PAIS { get; set; }
        public string PAIS { get; set; }
        public string MATRICULA { get; set; }
        public decimal ID_CLASE { get; set; }
        public string CLASE { get; set; }
        public decimal ID_MARCA { get; set; }
        public string MARCA { get; set; }
        public decimal ID_TIPO_SERVICIO { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public decimal GESTION_FABRICA { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal APP_FECHA_REGISTRO { get; set; }
    }

    public class O_BARCODE_HISTORIAL_CTY
    {
        public decimal BARCODE { get; set; }
        public string TIPO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string LUGAR_ENTREGA { get; set; }
        public string USUARIO_ENTREGA { get; set; }
        public string ESTADO { get; set; }
        public decimal CARGO { get; set; }
        public decimal ABONO { get; set; }
        public decimal SALDO { get; set; }
    }

    public class O_BARCODE_CXC_CTY
    {
        public decimal BARCODE { get; set; }
        public decimal ID_TIPO { get; set; }
        public string TIPO { get; set; }
        public decimal FECHA { get; set; }
        public string FECHA_STR { get; set; }
        public string USUARIO { get; set; }
        public string ESTADO { get; set; }
        public string TIPO_PAGO { get; set; }
        public string BANCO { get; set; }
        public string NRO_CTA { get; set; }
        public string NRO_REF { get; set; }
        public decimal CARGO { get; set; }
        public decimal ABONO { get; set; }
    }

    public class O_BARCODE_CXC_REPORTE
    {
        public decimal BARCODE { get; set; }
        public string TIPO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string LUGAR_ENTREGA { get; set; }
        public string USUARIO_ENTREGA { get; set; }
        public string ESTADO { get; set; }
        public decimal CARGO { get; set; }
        public decimal ABONO { get; set; }
        public decimal SALDO { get; set; }
        public decimal E_SALDO { get; set; }

        public string D_TIPO { get; set; }
        public string D_FECHA_STR { get; set; }
        public string D_USUARIO { get; set; }
        public string D_ESTADO { get; set; }
        public string D_TIPO_PAGO { get; set; }
        public string D_BANCO { get; set; }
        public string D_NRO_CTA { get; set; }
        public string D_NRO_REF { get; set; }
        public decimal D_CARGO { get; set; }
        public decimal D_ABONO { get; set; }
        public decimal D_SALDO { get; set; }
    }

    public class O_SERIES
    {
        public string name { get; set; }
        public double[] data { get; set; }
        public string stack { get; set; }
    }

    public class O_SERIES_DEC
    {
        public string name { get; set; }
        public decimal[] data { get; set; }
    }

    public class O_SERIES_INT
    {
        public string name { get; set; }
        public int[] data { get; set; }
        public string color { get; set; }
        public string type { get; set; }

    }
    public class O_GRAFICO_TIPO1_INT
    {
        public string[] names { get; set; }
        public List<O_SERIES_INT> series { get; set; }
    }

    public class O_GRAFICO_TIPO1
    {
        public string[] names { get; set; }
        public List<O_SERIES> series { get; set; }
    }

    public class O_OBTENER_ACUM_GRAFICO_CTY
    {
        public decimal ID_AGRUPA { get; set; }
        public string AGRUPA { get; set; }
        public string PERIODO { get; set; }
        public decimal CANTIDAD { get; set; }
    }

    public class O_LISTA_LOG_ERROR_CAB_CTY
    {
        public string CODIGO_ORACLE { get; set; }
        public string MENSAJE_ORACLE { get; set; }
        public decimal CANTIDAD { get; set; }
    }

    public class O_LISTA_LOG_ERROR_DET_CTY
    {
        public decimal ID_ERROR { get; set; }
        public string AUD_FECHA { get; set; }
        public string AUD_USUARIO { get; set; }
        public string APLICACION { get; set; }
        public string PROCEDIMIENTO { get; set; }
        public string MENSAJE_PERSONALIZADO { get; set; }
        public string MENSAJE_ORACLE { get; set; }
        public decimal ID_ORACLE_ERROR { get; set; }
        public string ID_ORACLE_MENSAJE { get; set; }
    }

    public class O_LISTA_COMPARATIVO_ENT_CTY
    {
        public string HYD_DEPTO { get; set; }
        public decimal HYD_ID_ENTIDAD { get; set; }
        public decimal HYD_ID_CONSUMIDOR { get; set; }
        public string HYD_NIT { get; set; }
        public string HYD_DENOMINACION { get; set; }
        public string HYD_DIRECCION { get; set; }
        public string HYD_SIST_GEST { get; set; }
        public string HYD_LICENCIA { get; set; }
        public string HYD_LIC_VIGENCIA { get; set; }
        public string BSA_DEPTO { get; set; }
        public decimal BSA_ID_ENT_HYD { get; set; }
        public decimal BSA_NIT { get; set; }
        public string BSA_DENOMINACION { get; set; }
        public string BSA_DIRECCION { get; set; }
        public string BSA_SIST_GEST { get; set; }
        public string BSA_COD_EESS { get; set; }
        public decimal BSA_VERSION_MDW { get; set; }
        public decimal BSA_ID_HYLIQ { get; set; }
        public decimal BSA_ID_HYGNV { get; set; }
    }

    public class O_LISTA_PRECIOS_HID_CAB_CTY
    {
        public decimal ID_TABLA { get; set; }
        public string CATEGORIA { get; set; }
        public decimal ID_CATEGORIA_VEHICULO { get; set; }
        public string HIDROCARBURO { get; set; }
        public string HIDRO { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public decimal PRECIO_BS { get; set; }
        public string FECHA_DESDE { get; set; }
    }

    public class O_LISTA_PRECIOS_HID_DET_CTY
    {
        public decimal ID_TABLA { get; set; }
        public string FECHA_DESDE { get; set; }
        public string FECHA_HASTA { get; set; }
        public decimal PRECIO_BS { get; set; }
        public string FECHA_REGISTRO { get; set; }
    }

    public class O_TIPOS_ACTIVIDAD_CTY
    {
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_CATEGORIA_ACTIVIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class O_LISTA_CONTROLES_USUARIO
    {
        public decimal ID_CONTROL_USUARIO { get; set; }
        public decimal FECHA_INICIO { get; set; }
        public decimal FECHA_FIN { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_UBICACION { get; set; }
        public string OBSERVACIONES { get; set; }
    }

    public class O_LISTA_RECEPCIONES_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string TIPO_TRANSACCION { get; set; }
        public decimal FECHA_HORA_EMISION { get; set; }
        public string NRO_TRANSACCION { get; set; }
        public decimal NRO_AUTORIZACION { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public decimal NIT_EMISOR { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public decimal ID_ESTADO_MOVIMIENTO { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public string NRO_DESPACHO { get; set; }
        public string PRECINTOS { get; set; }
        public string STR_FECHA_OPERACION { get; set; }
        public string STR_FECHA_HORA_EMISION { get; set; }
        public string STR_ESTADO_MOVIMIENTO { get; set; }
    }

    public class O_LISTA_MOVI_TRAN_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_TIPO_TRANSACCION { get; set; }
        public string TIPO_TRANSACCION { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public decimal FECHA_HORA_EMISION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string MOV_OBS { get; set; }
        public decimal ID_ESTADO_MOVIMIENTO { get; set; }
        public string NRO_TRANSACCION { get; set; }
        public string NRO_AUTORIZACION { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public decimal NIT_EMISOR { get; set; }
        public decimal NIT_CONSUMIDOR { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public string USUARIO_TRANSACCION { get; set; }
        public string TRA_OBS { get; set; }
        public string BINARIO_CONTROL { get; set; }

        public decimal ID_UNIDAD_ENVASES { get; set; }
        public decimal ID_FORMA_PAGO { get; set; }
        public string ENVASE { get; set; }
        public decimal ID_TIPO_FLUJO { get; set; }
        public decimal ID_TIPO_MOVIMIENTO { get; set; }
        public decimal ID_UBICACION_OPERACION { get; set; }
        public string PAIS { get; set; }

        public string STR_FECHA_OPERACION { get; set; }
        public string STR_FECHA_HORA_EMISION { get; set; }
        public string STR_ESTADO_MOVIMIENTO { get; set; }
        public decimal CANTIDAD_MOV { get; set; }
        public string FORMA_PAGO { get; set; }
        public string TIPO_FLUJO { get; set; }
        public string TIPO_MOVIMIENTO { get; set; }
        public string FECHA_MOV { get; set; }
        public string HORA_MOV { get; set; }
        public string TIPO_CONSUMIDOR { get; set; }
        public string BARCODE { get; set; }
        public string ID_TRANSACCION { get; set; }
        public string DESC_FUENTE_ORIGEN { get; set; }
    }

    public class O_COMPARATIVO_FIS_DOC_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public string TIPO { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public string CODIGO_OPERADOR { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_UNIDAD_HIDROCARBURO { get; set; }
        public string U_MEDIDA { get; set; }
        public decimal SALDO_FIS { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string APP_FECHA_REGISTRO { get; set; }
        public decimal SALDO_DOC { get; set; }
        public string FECHA { get; set; }
        public decimal DIFERENCIA { get; set; }
    }

    public class O_REPORTE_KARDEX_CTY
    {
        public decimal TIPO_INFRAESTRUCTURA { get; set; }
        public decimal ID_INFRAESTRUCTURA { get; set; }
        public string INF_TIPO { get; set; }
        public string INF_CODIGO { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_UNIDAD_HIDROCARBURO { get; set; }
        public string U_MEDIDA { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public string TIPO_OPERACION { get; set; }
        public decimal FACTOR_CALCULO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string IDENTIFICADOR { get; set; }
        public DateTime FECHA { get; set; }
        public decimal INGRESO { get; set; }
        public decimal SALIDA { get; set; }
        public decimal SALDO { get; set; }
        public decimal ID_AGRUPACION { get; set; }
        public string AGRUPACION { get; set; }
    }

    public class O_PAHL_RECEP_DA
    {
        public decimal FACTOR_CORRECCION { get; set; }
        public string API_MUESTRA { get; set; }
        public decimal TEMPERATURA_METER { get; set; }
        public string HORA_INICIO { get; set; }
        public decimal METER_INICIAL { get; set; }
        public string HORA_FINAL { get; set; }
        public decimal METER_FINAL { get; set; }
        public decimal VOL_REMITIDO { get; set; }
        public decimal VOL_RECIBIDO { get; set; }
        public decimal VOL_CORREGIDO { get; set; }
        public decimal VOL_PROD_INI { get; set; }
        public decimal VOL_PROD_FIN { get; set; }
        public decimal VOL_NETO { get; set; }
        public decimal NIVEL_PROD_INI { get; set; }
        public decimal NIVEL_AGUA_INI { get; set; }
        public decimal NIVEL_PROD_FIN { get; set; }
        public decimal NIVEL_AGUA_FIN { get; set; }
        public decimal TECHO_FLOTANTE_INI { get; set; }
        public decimal TECHO_FLOTANTE_FIN { get; set; }
    }

    public class O_PAHL_DESP_DA
    {
        public decimal FACTOR_CORRECCION_INI { get; set; }
        public decimal FACTOR_CORRECCION_FIN { get; set; }
        public string API_CORREGIDO { get; set; }
        public decimal TEMPERATURA_OBSERV { get; set; }
        public string HORA_INICIO { get; set; }
        public string HORA_FINAL { get; set; }
        public decimal METER_INICIAL { get; set; }
        public decimal METER_FINAL { get; set; }
        public string API_OBSERV_INI { get; set; }
        public string API_OBSERV_FIN { get; set; }
        public decimal TEMPERATURA_TK_INI { get; set; }
        public decimal TEMPERATURA_TK_FIN { get; set; }
        public decimal VOL_NETO_INI { get; set; }
        public decimal VOL_AGUA_NETO_INI { get; set; }
        public decimal VOL_CORREGIDO_TK_INI { get; set; }
        public decimal VOL_CORREGIDO_TK_FIN { get; set; }
        public decimal VOL_NETO_FIN { get; set; }
        public decimal VOL_AGUA_NETO_FIN { get; set; }
        public decimal NIVEL_PROD_INI { get; set; }
        public decimal NIVEL_AGUA_INI { get; set; }
        public decimal NIVEL_PROD_FIN { get; set; }
        public decimal NIVEL_AGUA_FIN { get; set; }
        public decimal TECHO_FLOTANTE_INI { get; set; }
        public decimal TECHO_FLOTANTE_FIN { get; set; }
    }

    public class O_PAHL_MEDIC_DA
    {
        public decimal FACTOR_CORRECCION { get; set; }
        public decimal NIVEL_PROD { get; set; }
        public decimal NIVEL_AGUA { get; set; }
        public decimal TEMPERATURA_TK { get; set; }
        public string API_OBSERVADO { get; set; }
        public decimal VOL_NETO { get; set; }
        public decimal CARGA_MUERTA { get; set; }
        public decimal ESPACIO_DISP_TK { get; set; }
    }

    public class O_CUMPLIMIENTO_DIA_CTY
    {
        public decimal ID_ACTIVIDAD { get; set; }
        public string DEPARTAMENTO { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public string DENOMINACION { get; set; }
        public string TIPO_OP { get; set; }
        public decimal ID_TIPO_OPERACION { get; set; }
        public string TIPO_OPERACION { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public string UNIDAD_MEDIDA { get; set; }
        public decimal CANTIDAD { get; set; }
    }

    public class O_LISTA_REPORTE_MENSUAL_CTY
    {
        public string DEPARTAMENTO { get; set; }
        public string DENOMINACION { get; set; }
        public string CITE_GENERADO { get; set; }
        public string JSON_REPORTE_DATOS { get; set; }
        public decimal ESTADO_MENSUAL { get; set; }
        public string MENSAJE_MENSUAL { get; set; }
        public Nullable<decimal> FECHA_REPORTE { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public string DIRECCION { get; set; }
        public string NOMBRES_REPRESENTANTE { get; set; }
        public string NIT { get; set; }
        public decimal LATITUD { get; set; }
        public decimal LONGITUD { get; set; }
        public decimal ID_USUARIO_ASIGNADO { get; set; }

        public string strPeriodo { get; set; }
        public string strEstado { get; set; }
        public string strFechaRegistro { get; set; }
        public string strFechaActIni { get; set; }
        public string strFechaActFin { get; set; }
        public string strFechaReg { get; set; }
        public string strUltEstado { get; set; }
    }

    public class O_LISTADO_DDJJ_CTY
    {
        public decimal ID_CONSUMIDOR { get; set; }
        public string PROYECTO { get; set; }
        public string DEPTO { get; set; }
        public decimal ID_CITE { get; set; }
        public string CITE { get; set; }
        public decimal FECHA_REPORTE { get; set; }
        public string PERIODO { get; set; }

        public DateTime datFechaRegistro { get; set; }
        public string strDia { get; set; }
        public string strMes { get; set; }
        public decimal strGestion { get; set; }
        public string strProducto { get; set; }
        public string strTipoInf { get; set; }
        public string strInf { get; set; }
        public string strFactorTipo { get; set; }
        public string strTipoOp { get; set; }
        public decimal decCantidad { get; set; }

        //vta internacional
        public string strPais { get; set; }
        public string strIdentificador { get; set; }
        public string strCliente { get; set; }
        //fuera de ala
        public string strNroSustCtrl { get; set; }
        public string strTipoEnvase { get; set; }
    }

    public class O_LISTADO_USUARIOS_CTY
    {
        public decimal ID_TABLA { get; set; }
        public decimal ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string ENTIDAD { get; set; }
        public DateTime FECHA_DESDE { get; set; }
        public DateTime FECHA_HASTA { get; set; }
        public decimal TIPO_USUARIO { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public string ACTIVIDAD { get; set; }
        public DateTime APP_FECHA_REGISTRO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public string APP_USUARIO { get; set; }
        public decimal ID_TIPO_CONTROL { get; set; }
        public decimal ID_UBICACION { get; set; }
        public string UBICACION { get; set; }
        public string STR_TIPO_USUARIO { get; set; }
        public string STR_TIPO_CONTROL { get; set; }
    }

    public class EResultadoUsuarioPermisosForm
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<EResultadoPermisos> oResultado { get; set; }
    }

    public class EResultadoPermisos
    {
        public decimal ID_FORMULARIO_ASP { get; set; }
        public string NOMBRE_FORMULARIO { get; set; }
        public string NOMBRE_ARCHIVO { get; set; }
        public decimal MODULO_ID { get; set; }
        public string NOMBRE_MODULO { get; set; }
        public decimal CONSULTA { get; set; }
        public decimal ALTAS { get; set; }
        public decimal BAJAS { get; set; }
        public decimal MODIFICACIONES { get; set; }
        public decimal REVISION { get; set; }
        public decimal APROBACION { get; set; }
        public string OTROS { get; set; }
    }

    public class O_RECEPCIONES_BSISA
    {
        public decimal ID_INTEGRACION { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal ID_TIPO_MOVIMIENTO { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public decimal ID_PRODUCTO_HYDRO { get; set; }
        public decimal CANTIDAD { get; set; }
        public string ID_TRANSACCION { get; set; }
        public decimal BARCODE { get; set; }
        public decimal ID_CODIGO_PAIS { get; set; }
        public decimal FECHA_ENVIO { get; set; }
        public string BINARIO_CONTROL { get; set; }
        public string USUARIO { get; set; }
        public string CI_USUARIO { get; set; }
        public string COD_TANQUE { get; set; }
        public string PRECINTOS { get; set; }
        public string CONDUCTOR { get; set; }
        public string CI_CONDUCTOR { get; set; }
        public decimal FECHA_DESPACHO { get; set; }
        public decimal NRO_DESPACHO { get; set; }
        public decimal NRO_ORDEN { get; set; }
        public string DESTINO { get; set; }
        public decimal VOLUMEN_PLANTA { get; set; }
        public decimal ID_TIPO_TRANSACCION { get; set; }
        public decimal NIT_CONSUMIDOR { get; set; }
        public decimal NIT_EMISOR { get; set; }
        public decimal NRO_TRANSACCION { get; set; }
        public decimal NRO_AUTORIZACION { get; set; }
        public string CODIGO_CONTROL { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal I_FECHA_TRANSACCION { get; set; }
        //public decimal I_OBSERVACIONES { get; set; }

    }

    public class O_TRANSACCION_BSISA
    {
        public Nullable<decimal> I_ID_TRANSACCION_PADRE { get; set; }
        public decimal I_ID_MOVIMIENTO { get; set; }
        public decimal I_ID_TIPO_TRANSACCION { get; set; }
        public decimal I_ID_ESTADO_TRANSACCION { get; set; }
        public decimal I_FECHA_HORA_EMISION { get; set; }
        public decimal I_NIT_CONSUMIDOR { get; set; }
        public decimal I_NIT_EMISOR { get; set; }
        public string I_NRO_TRANSACCION { get; set; }
        public decimal I_NRO_AUTORIZACION { get; set; }
        public string I_CODIGO_CONTROL { get; set; }
        public string I_RAZON_SOCIAL { get; set; }
        public decimal I_PRECIO_UNITARIO { get; set; }
        public decimal I_PRECIO_TOTAL { get; set; }
        public decimal I_CODIGO_SEGURIDAD { get; set; }
        public string I_USUARIO_TRANSACCION { get; set; }
        public string I_OBSERVACIONES { get; set; }
        public decimal I_ID_TIPO_CREDITO_FISCAL { get; set; }
        public decimal I_ID_FORMA_PAGO { get; set; }
        public decimal I_ID_TIPO_MONEDA { get; set; }
        public decimal I_ID_TIPO_EMISOR { get; set; }
        public decimal I_ID_AUTORIZACION_DATO { get; set; }
        public string I_BINARIO_CONTROL { get; set; }
        public decimal I_ID_TIPO_ACTIVIDAD { get; set; }
        public decimal I_FECHA_OPERACION { get; set; }
        public string I_CODIGO { get; set; }
        public decimal I_APP_ID_USUARIO { get; set; }
        public decimal I_APP_FECHA_REGISTRO { get; set; }
    }

    public class O_TRANSACCION_ADIC_BSISA
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal ID_INTEGRACION { get; set; }
        public decimal ID_TIPO_FACTURA { get; set; }
        public decimal NRO_TRANSACCION { get; set; }
        public decimal NRO_AUTORIZACION { get; set; }
        public decimal TIPO_CREDITO_FISCAL { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal IMPORTE { get; set; }
        public decimal ID_FORMA_PAGO { get; set; }
        public decimal ID_TIPO_EMISOR { get; set; }
        public string DETALLE_DOC { get; set; }
        public decimal FECHA_TRANSACCION { get; set; }
        public decimal FECHA_ENVIO { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public string I_CODIGO { get; set; }
    }

    public class O_MODULO_CTY
    {
        public decimal ID_MODULO { get; set; }
        public string DESCRIPCION { get; set; }
        public string URL { get; set; }
        public string NOMBRE { get; set; }
        public string CODIGO { get; set; }
        public decimal ID_APLICACION { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public string CREDENCIAL { get; set; }
    }

    public class O_LISTA_REPORTE_SUIC_CTY
    {
        public decimal ID_REGISTRO { get; set; }
        public decimal ID_REPORTE_DATO_PRINC { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROYECTO { get; set; }
        public string PERIODO_DECLARADO { get; set; }
        public string TIPO_DECLARACION { get; set; }
        public decimal CANT_MOV { get; set; }
        public string CITE_GENERADO { get; set; }
        public string ESTADO_DECLARACION { get; set; }
    }

    public class O_REPORTE_IMPORTACION_INSUMO_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public string FECHA { get; set; }
        public string EMPRESA { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal CANTIDAD { get; set; }
        public string RESOLUCION { get; set; }
        public string PRODUCTO { get; set; }
        public string MEDIDA { get; set; }
    }

    public class O_REPORTE_IMPORTACION_ENTIDAD_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public string RESOLUCION { get; set; }
        public string EMPRESA { get; set; }
        public string MES { get; set; }
        public string GESTION { get; set; }
        public string ACTIVIDAD { get; set; }
        public string CLASIFICACION { get; set; }
        public string MEDIDA { get; set; }
    }

    public class O_REPORTE_MOV_SALDO_CTY
    {
        public decimal ID_DEPARTAMENTO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string ENTIDAD { get; set; }
        public string FECHA_OPERACION_DIA { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public string CODIGO { get; set; }
        public decimal SALDOI { get; set; }
        public decimal RECEPCION { get; set; }
        public decimal DEMASIA { get; set; }
        public decimal VENTA { get; set; }
        public decimal MERMA { get; set; }
        public decimal SALDOF { get; set; }
        public string ESTADO { get; set; }
    }

    public class O_REPORTE_SALDO_DIA_CTY
    {
        public decimal ID_CONSUMIDOR { get; set; }
        public string DENOMINACION { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal SALDO_INICIO_MES { get; set; }
        public decimal INGRESOS { get; set; }
        public decimal EGRESOS { get; set; }
        public decimal SALDO_ACTUAL { get; set; }
    }

    public class O_REPORTE_ENT_ACTDEP_CTY
    {
        public decimal ID_CONSUMIDOR { get; set; }        
        public string DENOMINACION { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal RECEPCION { get; set; }
        public decimal DEMASIA { get; set; }
        public decimal VENTA { get; set; }
        public decimal MERMA { get; set; }
    }

    public partial class O_OBTENER_MOTORIZADO_CTY
    {
        public string TIPO_MTR { get; set; }
        public decimal ID_EXPLOTADOR { get; set; }
        public string EXPLOTADOR { get; set; }
        public string FA_CI { get; set; }
        public string FA_NOMBRE { get; set; }
        public string ID_TRANSACCION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public decimal SUB_IDENTIFICADOR { get; set; }
        public string BARCODE { get; set; }
        public decimal BARCODE_NRO { get; set; }
        public string CLASE { get; set; }
        public string MARCA { get; set; }
        public string SERVICIO { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public decimal GESTION_FABRICACION { get; set; }
        public decimal CARGA { get; set; }
        public string RA_TIPO_MATRICULA { get; set; }
        public string MENSAJE_IMPRESION { get; set; }
        public string MENSAJE_PANTALLA { get; set; }
        public byte[] IMAGEN { get; set; }
        public decimal TOTAL_CXC { get; set; }
        public decimal AUD_ESTADO { get; set; }
        public decimal ESTADO_REPOSICION { get; set; }
        public decimal ESTADO_BLOQUEO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
    }

    public class O_REPORTE_MOV_CONSUMO_CTY
    {
        public decimal ID_MOVIMIENTO { get; set; }
        public DateTime FECHA_OPERACION { get; set; }
        public DateTime FECHA_OPERACION_DIA { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
    }

    public class O_LISTA_PERMISO_CALIDAD_CTY
    {
        public decimal ID_GESTION_OCTANO { get; set; }
        public decimal ID_USUARIO_ANH { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string DIRECCION { get; set; }
        public string FECHA_FIN { get; set; }
        public string APLICACION { get; set; }
        public string ACTIVIDAD { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string ENTIDAD { get; set; }
    }

    public partial class O_OBTENER_TECHO_PRODE_CTY
    {
        public decimal ID_TIPO_OPERACION { get; set; }
        public string NOMBRE_OPERACION { get; set; }
        public decimal ID_MOVIMIENTO { get; set; }
        public decimal VOLUMEN { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
    }

    public partial class O_VOLUMEN_PRODE_CTY
    {
        public decimal ID_CONSUMIDOR { get; set; }
        public string ESTACION_SERVICIO { get; set; }
        public string COD_HYDRO_PROYECTO { get; set; }
        public decimal ID_HIDROCARBURO { get; set; }
        public decimal ID_DEPARTAMENTO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string SEMANA { get; set; }
        public decimal RECEPCION { get; set; }
        public decimal DEMASIA { get; set; }
        public decimal VENTA { get; set; }
        public decimal MERMA { get; set; }
        public decimal PRODE { get; set; }

    }

    public partial class O_REPORTE_PROYECTO_PRODUCTO_CTY
    {
        public decimal ID_PROYECTO { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public string COD_HYDRO_PROYECTO { get; set; }
        public string DENOMINACION_PROYECTO { get; set; }
        public decimal ID_DEPARTAMENTO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string ACTIVIDAD { get; set; }
        public string LICENCIA { get; set; }
        public string PRODUCTO { get; set; }
    }
    public partial class O_INCIDENTES_CTY
    {
        public decimal ID_INCIDENTES { get; set; }
        public decimal ID_ACTIVIDAD { get; set; }
        public string TIPO_ACTIVIDAD { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public decimal ID_TIPO_INCIDENTE { get; set; }
        public string TIPO_INCIDENTE { get; set; }
        public decimal ID_AREA_UNIDAD { get; set; }
        public string AREA_UYNIDAD { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHA_INICIO_SUCESO { get; set; }
        public DateTime FECHA_FIN_SUCESO { get; set; }
        public DateTime FECHA_REINCIO { get; set; }
        public decimal PERIODO { get; set; }
        public decimal APP_ID_USUARIO { get; set; }
        public decimal ID_OPERADOR { get; set; }
        public decimal GESTION { get; set; }
        public string PEROP_GESTION { get; set; }
        public decimal ID_CALSIFICADOR { get; set; }
        public string CLASIFICADOR { get; set; }
    }
    public partial class O_TIP_INCIDENTE_CTY
    {
        public decimal ID_PARAMETRICA_GLOBAL { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }
        public decimal ORDEN { get; set; }
        public string TIPO_INCIDENTE { get; set; }
    }

    public partial class O_RENDIMIENTO_VOL_CTY
    {
        public decimal ID_RENDIMIEN_VOLUMET { get; set; }
        public decimal ID_OPERADOR { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public string JSON_REGIS_MENSUALES { get; set; }
        public decimal PERIODO { get; set; }
        public decimal GESTION { get; set; }
        public decimal APP_USUARIO { get; set; }
        public string TIPO_INCIDENTE { get; set; }
    }
    public partial class O_RENDIMIENTO_CTY
    {
        public decimal ID_RENDIMIEN { get; set; }
        public string AREA_UNIDAD { get; set; }
        public decimal ID_TIPO_PRODUCTO { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string OPERACION_DIA { get; set; }
        public string CARGA_TOTAL { get; set; }
        public string CARGA_PROMEDIO { get; set; }
        public string CAPACIDAD_OPERACION { get; set; }
        public string COMB_LIQUIDO { get; set; }
        public string COMB_GAS { get; set; }
        public string VAPOR_TN { get; set; }
        public string AGUA_ENFR { get; set; }
        public string AIRE_COMP { get; set; }
        public string COMPARAD_KWH { get; set; }
        public string PROPIA_KWH { get; set; }
        public string RENDIMIENTO_PORCENT_VOLUMETRICO { get; set; }
        public List<O_RENDIMIENTO_VOLUM_CTY> oRendimientoVolumetricoCty { get; set; }
    }
    public partial class O_RENDIMIENTO_VOLUM_CTY
    {
        public decimal ID_RENDIMIEN_VOLUM { get; set; }
        public decimal ID_RENDIMIEN { get; set; }
        public string PRODUCTO { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string VOLUMEN { get; set; }
    }
    public partial class O_UNIDADES_REF_CTY
    {
        public decimal ID_PARAMETRICA_GLOBAL { get; set; }
        public string NOM_AREA_REFIN { get; set; }
    }
    public partial class O_OBJ_RENDIMIENTOS_VOLUM_CTY
    {
        public decimal ID_OPERADOR { get; set; }
        public List<O_RENDIMIENTO_CTY> oRendimientoVolumCty { get; set; }

    }
    public partial class O_OBTENER_DJ_CDIRECTO
    {
        public decimal ID_CONSUMIDOR { get; set; }
        public DateTime FECHA_DJ { get; set; }
        public string CITE_GENERADO { get; set; }
        public string TIPO_CONSUMIDOR { get; set; }        
    }

    public class O_CUSUARIOS_DIRECTO_CTY
    {
        public decimal ID { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }          
        public string DENOMINACION { get; set; }
        public decimal NIT_EMISOR { get; set; }     
        public string DEPARTAMENTO { get; set; }
        public decimal NIT_CONSUMIDOR { get; set; }
        public string RAZON_SOCIAL { get; set; }        
        public string PRODUCTO { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal PRECIO_UNITARIO { get; set; }
        public decimal PRECIO_TOTAL { get; set; }
        public decimal FECHA_OPERACION { get; set; }
        public string NRO_TRANSACCION { get; set; }        
        public string TIPO_DOC { get; set; }
        public DateTime DAT_FECHA { get; set; }
        public string CITE_GENERADO { get; set; }
        public string REP_NOMBRE { get; set; }
        public string MES { get; set; }
    }    
   
    public class O_LISTA_ESS_BSA_CTY
    {
        public decimal ID_EESS { get; set; }
        public string DEPTO { get; set; }
        public string DENOMINACION { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
    }

    public class O_SALDO_FINAL_DIA_CTY
    {
        public decimal ID_PRODUCTO { get; set; }
        public decimal ID_CONSUMIDOR { get; set; }
        public decimal SALDO_INI { get; set; }
        public decimal AC_MOV { get; set; }
        public decimal SALDO_FIN { get; set; }
    }

    public partial class O_PRODUCTO_PDE_CTY
    {
        public decimal PERIODO { get; set; }
        public decimal IDPRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public double PROGRAMADO { get; set; }
        public double DESPACHO { get; set; }
        public double SALDO { get; set; }
        public decimal IDCONSUMIDOR { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string LOCALIDAD { get; set; }
        public string DENOMINACION { get; set; }
        public string ACTIVIDAD { get; set; }
        public string LICENCIA { get; set; }
        public string VIGENCIA { get; set; }
        public string VENCIMIENTO { get; set; }
        public string CODIGO { get; set; }
        public string DESCRIPCION { get; set; }   
    }
    public class O_PROGRAMACION_PDE_CTY
    {
        public decimal decIdProde { get; set; }
        public decimal decIdConsumidor { get; set; }
        public string strDenominacion { get; set; }
        public string strDepartamento { get; set; }        
        public decimal decIdActividad { get; set; }
        public string strActividad { get; set; }
        public string strLicencia { get; set; }
        public string strVigencia { get; set; }
        public string strVencimiento { get; set; }
        public string strCodigoSirehidro { get; set; }
        public decimal decPeriodo { get; set; }
    }
    public class O_DOCUMENTO_PDE_CTY
    {
        public decimal decIdDocumento { get; set; }
        public string strFechaEmision { get; set; }
        public string strTipoDocumento { get; set; }
        public string strNroDocumento { get; set; }
        public decimal decNit { get; set; }
        public string strNroAutorizacion { get; set; }
        public string strCodigoControl { get; set; }
        public string strChofer { get; set; }
        public string strIdentificador { get; set; }
        public string strPrecintos { get; set; }
        public decimal decCantidad { get; set; }
        public string strTipoDocDgsc { get; set; }
        public string strFechaVigencia { get; set; }
        public decimal decPrecio { get; set; }
        public decimal decImporte { get; set; }
        public string strEstado { get; set; }
        public string strUsuario { get; set; }
    }

    public class O_VENTA_HORA_CTY
    {
        public string ANIO { get; set; }
        public string MES { get; set; }
        public string DIA { get; set; }
        public string HORA { get; set; }  
        public string DEPARTAMENTO { get; set; }
        public string DENOMINACION { get; set; }
        public string PRODUCTO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string SERVICIO { get; set; }
        public string CLASE { get; set; }
        public decimal CANTIDAD { get; set; }
        public decimal TRANSACCIONES { get; set; }    
    }

    public partial class O_LECTURA_RFID_CTY
    {
        public decimal ID_LECTURA_RFID { get; set; }
        public string DEPTO { get; set; }
        public string PLANTA { get; set; }
        public string BARCODE { get; set; }
        public DateTime FECHA_LECTURA { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string CLASE { get; set; }
        public byte[] IMAGEN { get; set; }
        public decimal NRO_COMPARTIMIENTO { get; set; }
        public decimal VOLUMEN_NOMINAL { get; set; }
        public string VOLUMEN_NOMINAL_COMPTO { get; set; }
        public string MARCA { get; set; }
        public string CODIGO_ANTENA { get; set; }  
    }

    public class O_RANK50_CTY
    {
        public decimal ID { get; set; }
        public string GESTION { get; set; }
        public string MES { get; set; }
        public string CLASE { get; set; }
        public string DENOMINACION { get; set; }
        public string IDENTIFICADOR { get; set; }       
        public string DEPARTAMENTO { get; set; }
        public string PRODUCTO { get; set; }
        public decimal CENTENAR { get; set; }
        public decimal VOLUMEN { get; set; }
        public decimal MAX_VOL { get; set; }
        public decimal MIN_VOL { get; set; }
        public decimal PROM_VOL { get; set; }
        public decimal CONT { get; set; }      
    }
    public class O_VAL_DJ_CTY
    {
        public decimal VALIDA { get; set; }
        public string MENSAJE { get; set; }
        public string MES { get; set; }    
    }

    public class O_MOV_PAHL_CTY
    {
        public string FECHA_MOV { get; set; }
        public string DENOMINACION { get; set; }
        public string DPTO { get; set; }
        public string PRODUCTO { get; set; }
        public string EESS { get; set; }
        public string TIPO_OPERACION { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string INF_TIPO { get; set; }
        public string INF_CODIGO { get; set; }
        public decimal TOTAL_INGRESO { get; set; }
        public decimal TOTAL_SALIDAS { get; set; }
        public decimal ID_MOVIMIENTO { get; set; }
        public string NRO_TRANSACCION { get; set; }
    }
    public class O_PRECIO_IMP_BSA_CTY
    {
        public decimal ID_PRECIO_IMPORTADO { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string IMPORTADORA { get; set; }
        public decimal ID_PRODUCTO { get; set; }
        public string PRODUCTO { get; set; }
        public string COD_DISDENSER { get; set; }
        public string COD_MANGUERAS { get; set; }
        public decimal PRECIO { get; set; }
        public decimal VOLUMEN { get; set; }
        public string FECHA_DESDE { get; set; }
        public string FECHA_HASTA { get; set; }
        public string OBSERVACIONES { get; set; }
        public decimal ESTADO { get; set; }    
    }  
}
