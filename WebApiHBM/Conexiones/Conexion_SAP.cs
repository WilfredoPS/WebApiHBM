using SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Conexiones
{
    public class Conexion_SAP
    {
        ConectaSAP ConexionSAP;

        public Conexion_SAP(string pAmbiente, string pServidor, string pUsuario, string pContrasena, string pMandante, string pSistemaID, string pLenguaje, string pInstancia, string pPoolSize)
        {
            // TODO: Complete member initialization
            ConexionSAP = new ConectaSAP();
            ConexionSAP.NombreConfiguracion = pAmbiente;
            ConexionSAP.Servidor = pServidor;
            ConexionSAP.Usuario = pUsuario;
            ConexionSAP.Contrasena = pContrasena;
            ConexionSAP.Mandante = pMandante;
            ConexionSAP.SistemaID = pSistemaID;
            ConexionSAP.Lenguaje = pLenguaje;
            ConexionSAP.Instancia = pInstancia;
            ConexionSAP.PoolSize = pPoolSize;
        }
    }
}