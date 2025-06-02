using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_ProductoLote
    {
        public string ProductoLoteID { get; set; }
        public string ProductCode { get; set; }
        public string Almacen { get; set; }
        public string Lote { get; set; }
        public string FechaVencimiento { get; set; }
        public int Stock { get; set; }
        public string FechaModificacion { get; set; }
        ///public string Mensaje { get; set; }
    }
}