using System;
using System.Configuration;

namespace AnhHydroOctanoAgenteServicio
{
    public class ServiciosRest
    {
        #region Servicios SIRH
        public static readonly string ServicioJsonSIRH =
            Convert.ToString(ConfigurationManager.AppSettings["ServicioJsonSirh"]);
        public static readonly string SvcDatosFuncionarioANH = "/EDatosFuncionarioANH/";
        public static readonly string SvcFotoPerfilFuncionario = "/EFotoPerfilFuncionario/";
        #endregion


        //#region servicios Gestion Documental
        //public static readonly string ServicioSisgen = Convert.ToString(ConfigurationManager.AppSettings["StrServicioSisgen"]);
        //#region consultas
        public static readonly string SvcOrganigramaVersionSirh = "/ListaOrganigrama/";
        public static readonly string SvcGestionesSirh = "/ListaVersiones/";

        //public static readonly string SvcListaArea = "/ListaArea/";
        //public static readonly string SvcEInsertarArea = "/InsertarArea/";
        //public static readonly string SvcModificarArea = "/ModificarArea/";
        //public static readonly string SvcEliminarArea = "/EliminarArea/";

        //public static readonly string SvcInsertarActividad = "/InsertarActividad/";
        //public static readonly string SvcModificarActividad = "/ModificarActividad/";
        //public static readonly string SvcEliminarActividad = "/EliminarActividad/";
        //public static readonly string SvcListaActividad = "/ListaActividad/";

        //public static readonly string SvcListaTipoNormativa = "/ListaTipoNormativa/";

        //public static readonly string SvcListaCorrelativoNormativa = "/ListaCorrelativoNormativa/";
        //public static readonly string SvcGenerarCorrelativo = "/GenerarCorrelativo/";

        //public static readonly string SvcInsertarDocumento = "/InsertarDocumento/";
        //public static readonly string SvcListaDocumento = "/ListaDocumento/";
        //public static readonly string SvcModificarDocumento = "/ModificarDocumento/";
        //public static readonly string SvcEliminarDocumento = "/EliminarDocumento/";

        //public static readonly string SvcInsertarDocumentoNormativa = "/InsertarDocumentoNormativa/";

        //#endregion
        //#endregion


    }
}
