namespace AnhHydroOctanoPresentacion.Entidades
{
    public class ELog
    {
        public class LogAccesoApp
        {
            public decimal decIdUsuario { get; set; }
            public string strAccion { get; set; }
            public string strIP { get; set; }
            public string strMetodoUtilizado { get; set; }
            public string strNombreServicio { get; set; }
            public string strRutaFormulario { get; set; }
            public ObjetoEntrada ObjetoEntrada { get; set; }
        }

        public class ObjetoEntrada
        {
            public string strCredencial { get; set; }
            public decimal decFuenteOrigen { get; set; }
            public decimal decTipoCategoria { get; set; }
            public decimal decTipoConsulta { get; set; }
            public string strIdentificador { get; set; }
            public string strSubIdentificador { get; set; }
            public string strLugar { get; set; }
            public decimal decFechaInicioConsumo { get; set; }
            public decimal decFechaFinConsumo { get; set; }
        }
    }
}