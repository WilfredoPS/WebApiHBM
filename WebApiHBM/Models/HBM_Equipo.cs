using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_RecepcionEquipos
    {
        public string RecepcionID { get; set; }
        public string Estado { get; set; }
        public string CodRecepcion { get; set; }
        public string IdVendedor { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string OrgVenta { get; set; }
        public string Regional { get; set; }
        public string KUNNR { get; set; }
        public string NombreCliente { get; set; }
        public string Distribuidor { get; set; }
        public string Email { get; set; }
        public string TelefonoCelular { get; set; }
        public string NombreEquipo { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Obs { get; set; }
        public string Foto { get; set; }
        public string Fecha { get; set; }
        public string TipoEnvio { get; set; }
        public string FechaCreate { get; set; }
        public string FechaUpdate { get; set; }
        public string NombreEV { get; set; }
        public string TipoRecepcion { get; set; }
        public string OT { get; set; }
    }
    public class HistorialRecepcionEquipos
    {
        public string HistoricoID { get; set; }
        public string RecepcionID { get; set; }
        public string CodRecepcion { get; set; }
        public string Estado { get; set; }
        public string IdVendedor { get; set; }
        public string Usuario { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string TipoEnvio { get; set; }
        public string NombreRecepcionista { get; set; }
        public string Obs { get; set; }
        public string Transportadora { get; set; }
        public string NroGuia { get; set; }
        public string FechaEnvio { get; set; }
        public string ContEstado { get; set; }
        public string Foto { get; set; }
    }

    public class RecepcionEquiposRegionalTaller
    {
        public string RegionalTallerID { get; set; }
        public string CodigoRegional { get; set; }
        public string Regional { get; set; }
    }
}