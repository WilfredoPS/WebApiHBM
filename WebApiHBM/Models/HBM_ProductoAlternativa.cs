using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_ProductoAlternativa
    {
        public string ProductoAlternativaID { get; set; }
        public string CodigoAlternativa { get; set; }
        public string CodigoSinAlternativa { get; set; }
        public string CodigoProducto { get; set; }
        public string OficinaVentas { get; set; }
        public string CodCanal { get; set; }
        public string Descripcion { get; set; }
        public string CodVendedor { get; set; }
        public int Estado { get; set; } 
        public string EstadoStock { get; set; }
        public string FechaValidez { get; set; }
        public string GrupoCliente { get; set; }
        public string FechaModificacion { get; set; }
        public string Moneda { get; set; }
        public string Posiciones { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Centro { get; set; }
    }
}