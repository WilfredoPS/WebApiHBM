using System;
using SAP.Middleware.Connector;
using System.Data;

namespace SAP
{
    /// ESTO ES UNA MODIFICACION DE PRUEBA
    public class ConectaSAP
    {
        public string NombreConfiguracion { get; set; }
        public string Servidor { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Mandante { get; set; }
        public string SistemaID { get; set; }
        public string Lenguaje { get; set; }
        public string Instancia { get; set; }
        public string PoolSize { get; set; }
         
        public Configuraciones ConfiguracionSAP;

        /// <summary>
        /// Registra la configuracion de la conexion al SAP
        /// </summary>
        /// <param name="pNombreDestino">Nombre del Destino EJ: DESARROLLO,CALIDAD,PRODUCTIVO, etc.(Se puede registrar varios destinos con diferentes pNombreDestino Ej: CALIDAD,CALIDAD1)</param>
        /// <param name="oMensajeSalida">Mensaje de salida</param>
        /// <returns>Retorna un objeto RfcDestination caso contrario null si no logro registrar las configuraciones</returns>
        public RfcDestination RegistrarConexion(string pNombreDestino, out string oMensajeSalida)
        {
            try
            {
                ConfiguracionSAP = new Configuraciones();
                ConfiguracionSAP.Servidor = Servidor;
                ConfiguracionSAP.Usuario = Usuario;
                ConfiguracionSAP.Contrasena = Contrasena;
                ConfiguracionSAP.Mandante = Mandante;
                ConfiguracionSAP.SistemaID = SistemaID;
                ConfiguracionSAP.Lenguaje = Lenguaje;
                ConfiguracionSAP.Instancia = Instancia;
                ConfiguracionSAP.PoolSize = PoolSize;
                ConfiguracionSAP.NombreConfiguracion = pNombreDestino;
                RfcDestinationManager.RegisterDestinationConfiguration(ConfiguracionSAP);                
                RfcDestination Destino = RfcDestinationManager.GetDestination(pNombreDestino);
                oMensajeSalida = "Registro de configuracion exitoso";
                return Destino;

            }
            catch (Exception ex)
            {
                //RfcDestinationManager.UnregisterDestinationConfiguration(Destino);
                oMensajeSalida = ex.Message;
                return null;
            }
                
        }

        /// <summary>
        /// Prueba la conexion al SAP
        /// </summary>
        /// <param name="pDestino">Registro de la configuracion de conexion al SAP </param>
        /// <param name="oMensajeSalida">Mensaje de salida test de conexion al SAP </param>
        /// <returns>True: Hay conexion al SAP, False: No hay conexion al SAP</returns>
        public bool ProbarConexion(RfcDestination pDestino, out string oMensajeSalida)
        {
            try
            {
                pDestino.Ping();
                oMensajeSalida = "Test de conexion se realizo correctamente";
                return true;
            }
            catch(Exception ex)
            {
                oMensajeSalida = ex.Message;
                return false;
            }        
        }

        /// <summary>
        /// Desregistra la configuracion al SAP
        /// </summary>
        /// <param name="pNombreDestino">Nombre del destino</param>
        public void CerrarConexionSAP(string pNombreDestino)
        {
            if(ConfiguracionSAP.NombreConfiguracion == pNombreDestino)
              RfcDestinationManager.UnregisterDestinationConfiguration(ConfiguracionSAP);
        }
    }
}
