using System.Configuration;
using SAP.Middleware.Connector;

namespace SAP
{
    public class Configuraciones : IDestinationConfiguration
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

        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {
            RfcConfigParameters Parametros = new RfcConfigParameters();
            Parametros.Add(RfcConfigParameters.Name, destinationName);
            Parametros.Add(RfcConfigParameters.AppServerHost, Servidor);
            Parametros.Add(RfcConfigParameters.User, Usuario);
            Parametros.Add(RfcConfigParameters.Password, Contrasena);
            Parametros.Add(RfcConfigParameters.Client, Mandante);
            Parametros.Add(RfcConfigParameters.SystemID, SistemaID);
            Parametros.Add(RfcConfigParameters.Language, Lenguaje);
            Parametros.Add(RfcConfigParameters.SystemNumber, Instancia);
            Parametros.Add(RfcConfigParameters.PoolSize, PoolSize);

            return Parametros;
        }
    }
}
