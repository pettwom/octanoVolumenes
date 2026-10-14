//===================================================================================
// Agencia Nacional de Hidrocarburos
// Unidad de Sistemas
//=================================================================================== 
// Clase "CGestion.cs" implementa un Servicio de la Aplicación.
// Realiza altas, bajas, modificaciones y lista información de la entidad Enlaces
//===================================================================================
// Autor: Hebert Cussi Cuentas Versión 1.
// Derechos Reservado Agencia Nacional de Hidrocraburos. 
// http://www.anh.gob.bo
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AnhHydroAgenteServicio;
using AnhHydroOctanoAgenteServicio.ServicioParametricas;
//using AnhHydroAgenteServicio.ServicioParametricas;
using AnhHydroOctanoPresentacion.Parametros;
using AnhHydroOctanoPresentacion.Lib;
using DevExpress.Office.Utils;
using LocalizadorProxy = AnhHydroOctanoAgenteServicio.LocalizadorProxy;
using O_PARAMETRICA_CTY = AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY;

namespace AnhHydroOctanoPresentacion.Clases.Parametricas
{
    public class CParametricas
    {
        #region Variables de Entorno
        
        private string _mensajeDeError = string.Empty;
        private AnhHydroOctanoAgenteServicio.ServicioParametricas.IServicioParametricas _servicio;
        private string _mapPath;
        private CLogs _logs;
        
        #endregion


        #region Metodos y Funciones

        public CParametricas(string mapPath, string strIp)
        {
            _servicio = LocalizadorProxy.ServicioParametricas();
            _mapPath = mapPath;
            _logs = new CLogs(_mapPath,strIp);
        }

        public AnhHydroOctanoAgenteServicio.ServicioParametricas.IServicioParametricas ObtenerServicio()
        {
            return _servicio;
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaDepartamentos()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoDepartamentos(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }
        
        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaGenero()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoGeneros(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if(_mensajeDeError!="")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
                return null;
            }
        }
        
        public  List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaCategoriasActividad()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoCategoriasActividad(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaAmbitoOperacion()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoAmbitosOperacion(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaProvinciaDepartamento(decimal idDepartamento)
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoProvincias(CParametrosHydro.StrCredencialEmpadronamiento, idDepartamento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaLocalidad(decimal idProvincia)
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoLocalidades(CParametrosHydro.StrCredencialEmpadronamiento, idProvincia, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaMunicipios(decimal idProvincia)
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoMunicipios(CParametrosHydro.StrCredencialEmpadronamiento,idProvincia, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {

                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {

                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaTiposActividad(decimal idCategoriaActividad)
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoTiposActividad(CParametrosHydro.StrCredencialEmpadronamiento, idCategoriaActividad, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaTiposPoliza()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoTiposPoliza(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaAseguradoras()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoAseguradoras(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaBancos()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoBancos(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaTiposCertificados()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoTiposCertificado(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> ListaInspectores()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoInspectores(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        public List<O_PARAMETRICA_CTY> ListaTiposIdentificacion()
        {
            try
            {
                List<AnhHydroOctanoAgenteServicio.ServicioParametricas.O_PARAMETRICA_CTY> oParametricaCtys = _servicio.listadoTiposIdentificacion(CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeDeError);
                if (_mensajeDeError != "")
                {
                    
                    _logs.Error(_mensajeDeError);
                }
                return oParametricaCtys;
            }
            catch (Exception exp)
            {
                
                _logs.Error(exp);
                return null;
            }
        }

        #endregion
    }
}