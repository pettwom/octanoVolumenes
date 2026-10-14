using System;
using System.Configuration;
using System.Drawing;
using Librerias.Anh.Us;

namespace AnhHydroOctanoPresentacion.Parametros
{
    public class CParametrosHydro
    {
        #region Variables de entorno para Configuracion de Servidor de Correo

        /// <summary>
        /// Direccion IP del servidor
        /// </summary>
        //public static readonly string StrServidorDireccion =ConfigurationManager.AppSettings["CorreoServidor"];//172.17.20.41
        public static string StrServidorDireccion = "172.17.20.14";//172.17.20.41

        /// <summary>
        /// Puerto SMTP del servidor
        /// </summary>
        //public static int IntServidorPuerto = Convert.ToInt32(ConfigurationManager.AppSettings["CorreoPuerto"]);
        public static int IntServidorPuerto = 25;

        /// <summary>
        /// Cuenta de origen
        /// </summary>
        //public static string StrUsuarioDe = ConfigurationManager.AppSettings["CorreoDe"];
        public static string StrUsuarioDe = "sistemas@anh.gob.bo";

        /// <summary>
        /// Cuenta de copia de email
        /// </summary>
        public static string StrUsuarioCc = ConfigurationManager.AppSettings["CorreoCc"];

        /// <summary>
        /// Cuenta de copia oculta
        /// </summary>
        //public static string StrUsuarioCco = "lsilva@anh.gob.bo";
        public static string StrUsuarioCco = "lufernandez@anh.gob.bo, avargas@anh.gob.bo, hherrera@anh.gob.bo, fconde@anh.gob.bo";

        //public static string strUsuarioCco = "sistemas@anh.gob.bo, abenitez@anh.gob.bo, mpaco@anh.gob.bo";

        /// <summary>
        /// Login de cuenta origen
        /// </summary>
        public static string StrUsuarioLogin = "sistemas";

        /// <summary>
        /// Passord de cuenta origen
        /// </summary>
        public static string StrUsuarioPassword = "8TKI4.5@VY93Is";

        /// <summary>
        /// Habilitar SSl email
        /// </summary>
        public static bool bolHabilitarSsl = false;

        /// <summary>
        /// Mostrar Excepcion de error envio email
        /// </summary>
        public static bool bolNotificarError = true;

        #endregion

        #region Variables de entorno para el Capcha

        /// <summary>
        /// Cantidad d caracteres visualizados en el capcha
        /// </summary>
        public static int intCantidadCaracteres = 6;

        /// <summary>
        /// Ancho de la imagen capcha
        /// </summary>
        public static int intAncho = 300;

        /// <summary>
        /// Alto de la imagen capcha
        /// </summary>
        public static int intAlto = 60;

        /// <summary>
        /// Tamaño de la fuente capcha
        /// </summary>
        public static int intTamañoFuente = 50;

        /// <summary>
        /// Color del capcha
        /// </summary>
        public static Color colorCapcha = Color.YellowGreen;

        #endregion

        #region Variables ActiveDirectory

        /// <summary>
        /// Nombre de Dominio donde se ejecuta loa aplicacion LDAP
        /// </summary>
        public static string strDominioLdap = "LDAP://anh.gob.bo";

        /// <summary>
        /// Nombre de Dominio 
        /// </summary>
        public static string strDominio = "anh";

        #endregion

        #region Credenciales

        public static string StrCredencialEmpadronamiento = CEncriptacion.generarMD5("anh321.");
        public static string strCredencialHydroAdmin = "EBF0FDEFA0ED00CF8C47EZAB189FD4F5";
        public static string strCredencialOctanoVolumen = CEncriptacion.generarMD5("anh321.");

        public static string strCredencialFuncionario = "7C43558887B1D7ECB76F3B9917C8818D55171";
        public static string strCredencialDeclaracion = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";
        public static string strCredencialListadoDj = "83809AD945F1F72D0EA9FDA0E599E0B3";

        public static string strCredencialInfra = "60A0B4CB8CF3806785DB2C60B1507218";
        public static string StrCredencialSireHidro = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";
        public static string StrCredencialRrhh = "MCKDI498LK32LKJF8LS23KSJSRH4921H";

        public static string StrCredencialMtr = "102DDAF691E1615D5DACD4C86299BFA4";

        public static string StrCredencialOctCal = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";//LMQC

        public static string StrCredencialSisgen = "8359502F5BA2AC5B31AF64EBB696FD76";

        public static string StrCredencialExterno = "EBF8FDEFA7ED24CF8C47EE0B149FD4F5";
        public static string StrTokenYpfbAbp = "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJmZWNoYSI6IjIwMTgwODAxMTU0NTIzIiwic2lyZWhpZHJvIjoiQU5IMDIyMzMiLCJvcGVyYWRvciI6IkFJUiBCUCBCT0xJVklBIFMuQS4gQUJCU0EiLCJ2aWdlbmNpYSI6IjIwMjgxMjMxMDAwMDAwIn0._I4ojD5n1WATqGolIOAUPHXPyaED2ISKspFYMSiODYY";
        //public static string StrTokenYpfbAbp = "Bearer eyJhb.eyJmZWNo._I4oj";

        #endregion

        #region Variables de aplicacion

        public static decimal decIdAplicacion = Convert.ToDecimal(ConfigurationManager.AppSettings["idAplicacion"]);
        //public static string strUrlRaiz = ConfigurationManager.AppSettings["UrlRaiz"] + "";

        #endregion

        #region Pametros de configuración
        //public static decimal decIdAplicacion = Convert.ToDecimal(ConfigurationManager.AppSettings["idAplicacion"]);
        public static decimal IdFuncionarioAnh = Convert.ToDecimal(ConfigurationManager.AppSettings["IdFuncionarioAnh"]);
        public static decimal IdRegulado = Convert.ToDecimal(ConfigurationManager.AppSettings["IdRegulado"]);
        #endregion
    }
}