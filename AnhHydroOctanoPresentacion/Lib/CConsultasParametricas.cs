using System;
using System.Configuration;
using System.Drawing;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Parametros;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;

namespace AnhHydroOctanoPresentacion.Lib
{
    public class CConsultasParametricas
    {
        public string ObtieneIdsActividadOctanioVol()
        {
            JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);

            var objReporteVariable = _client.Post<EResultado<string>>(CAppSettings.svc_ids_activiades_oct_volumenes,
                new
                {
                    strCredencial = Parametros.CParametrosHydro.strCredencialOctanoVolumen,

                });
            return objReporteVariable.oResultado;
        }
    }
}