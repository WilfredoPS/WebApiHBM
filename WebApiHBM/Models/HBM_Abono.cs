using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Abono
    {
        public string AbonoID { get; set; }
        public string CodFactura { get; set; }
        public string CodRecibo { get; set; }
        public decimal Descuento { get; set; }
        public decimal FacSaldoBS { get; set; }
        public decimal FacSaldoUSD { get; set; }
        public string FechaCreacion { get; set; }
        public decimal ImporteBS { get; set; }
        public decimal ImporteUSD { get; set; }
        public decimal MontoFacBS { get; set; }
        public decimal MontoFacUSD { get; set; }
        public string Parcial { get; set; }
        public decimal TipoCambio { get; set; }
        public string Tipo { get; set; }
        public decimal TotalBS { get; set; }
        public decimal TotalUSD { get; set; }
        public string CODSBO { get; set; }
        public string IdVendedor { get; set; }
        public string KUNNR { get; set; }
        public string FechaModificacion { get; set; }
        public string ReciboID { get; set; }
        public string Observacion { get; set; }
        public string NroDocContable { get; set; }
        public string FacturaID { get; set; }
    }
}