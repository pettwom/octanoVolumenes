using ProtoBuf;

namespace AnhHydroOctanoPersistencia.Entidades
{
    public class EObjetoEnvio1ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
    }

    public class EObjetoEnvio2ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
    }

    public class EObjetoEnvio3ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
    }

    public class EObjetoEnvio4ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public string strParametro4 { get; set; }
    }

    public class EObjetoEnvio5ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public string strParametro4 { get; set; }
        [ProtoMember(7)]
        public string strParametro5 { get; set; }
        [ProtoMember(8)]
        public string strParametro6 { get; set; }
    }

    public class EObjetoEnvio6ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public string strParametro4 { get; set; }
        [ProtoMember(7)]
        public string strParametro5 { get; set; }
        [ProtoMember(8)]
        public string strParametro6 { get; set; }
    }

    public class EObjetoEnvio7ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public string strParametro4 { get; set; }
        [ProtoMember(7)]
        public string strParametro5 { get; set; }
        [ProtoMember(8)]
        public string strParametro6 { get; set; }
        [ProtoMember(8)]
        public string strParametro7 { get; set; }
    }

    public class EObjetoEnvio8ParametrosProto
    {
        [ProtoMember(1)]
        public decimal strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public string strParametro4 { get; set; }
        [ProtoMember(7)]
        public string strParametro5 { get; set; }
        [ProtoMember(8)]
        public string strParametro6 { get; set; }
        [ProtoMember(8)]
        public string strParametro7 { get; set; }
        [ProtoMember(9)]
        public string strParametro8 { get; set; }
    }

    public class EListarDeclaracionJuradaAcumuladaD
    {
        public string strCredencial { get; set; }
        public decimal decFiltro { get; set; }
        public decimal decIdConsumidor { get; set; }
        public decimal decIdTipoActividad { get; set; }
        public decimal decFechaDiaInicio { get; set; }
        public decimal decFechaDiaFin { get; set; }
    }
    public class EVerificaUsuario
    {
        public decimal decIdUsuario { get; set; }
        public decimal decIdModulo { get; set; }
    }
    public class EListaControlesUsuario
    {
        public string strCredencial { get; set; }
        public decimal decIdUsuario { get; set; }
    }

    public class EObjetoEnvio9ParametrosProto
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decFiltro { get; set; }
        [ProtoMember(3)]
        public string strParametro1 { get; set; }
        [ProtoMember(4)]
        public string strParametro2 { get; set; }
        [ProtoMember(5)]
        public string strParametro3 { get; set; }
        [ProtoMember(6)]
        public decimal decParametro4 { get; set; }

    }

    public class EObjetoEnvio10ParametrosProto
    {
        [ProtoMember(1)]
        public decimal decFiltro { get; set; }
        [ProtoMember(2)]
        public string strParametro1 { get; set; }

    }

    public class EObjetoEnvioIncidentes
    {
        [ProtoMember(1)]
        public string strCredencial { get; set; }
        [ProtoMember(2)]
        public decimal decIdActividad { get; set; }
        [ProtoMember(3)]
        public decimal decIdTipoIncidente { get; set; }
        [ProtoMember(4)]
        public decimal decIdPeridod { get; set; }
        [ProtoMember(5)]
        public decimal decIdGestion { get; set; }
        public decimal deciIdOperador { get; set; }

        //public class EObjetoEnvioIncidentes
        //{
        //    [ProtoMember(1)]
        //    public string strCredencial { get; set; }
        //    [ProtoMember(2)]
        //    public decimal decIdActividad { get; set; }
        //    [ProtoMember(3)]
        //    public decimal decIdTipoIncidente { get; set; }
        //    [ProtoMember(4)]
        //    public decimal decIdPeridod { get; set; }
        //    [ProtoMember(5)]
        //    public decimal decIdGestion { get; set; }
        //    public decimal deciIdOperador { get; set; }

        //}
    }
    public class lstImportacionDatos
    {
        public decimal decIdActividad { get; set; }
        public decimal decIdConsumidor { get; set; }
        public string strDenominacion { get; set; }
        public decimal decIdDepartamento { get; set; }
        public string strDepartamento { get; set; }
        public decimal decIdProducto { get; set; }
        public string strProducto { get; set; }
        public decimal decCantidad { get; set; }
        public decimal decUsuarioAplicacion { get; set; }
        public decimal decPeriodo { get; set; }
        public decimal decTipoOperacion { get; set; }
        public decimal decConsumidorOrigen { get; set; }
    }
    public class EObjetoPrecioImportacion
    {
        public decimal decIdEESS { get; set; }
        public decimal decIdEntidad { get; set; }
        public decimal decIdProducto { get; set; }
        public double decVolumen { get; set; }
        public double decPrecio { get; set; }
        public string strImportadora { get; set; }
        public string strDispenser { get; set; }
        public string strManguera { get; set; }
        public decimal decFechaDesde { get; set; }
        public decimal decFechahasta { get; set; }
        public string strObs { get; set; }
        public decimal decEstado { get; set; }
    }

}
