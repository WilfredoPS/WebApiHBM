using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class Descuento
    {
        public string CodDescuento { get; set; }
        public string ClaseDocumento { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string Esquema { get; set; }
    }
    public class DescuentoDetalle
    {
        public string CodDetalleDescuento { get; set; }
        public string Esquema { get; set; }
        public string ClaseCondicion { get; set; }
        public int Posicion { get; set; }
        public string Denominacion { get; set; }
        public string Tipo { get; set; }
        public string TipoPM { get; set; }
        public int Activado { get; set; }
        public int Modificable { get; set; }
    }
    public class ProductoDesc
    {
        public string ProductCode { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string Centro { get; set; }
        public string Almacen { get; set; }
        public string GrupoCliente { get; set; }
        public string OrgVenta { get; set; }
        public decimal Descuento { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Fin { get; set; }
        public string Fecha { get; set; }
        public string ClaseCondicion { get; set; }
        public string Esquema { get; set; }
        public string UsuarioCrea { get; set; }
        public string CodDivision { get; set; }
    }
}