using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Facturas
    {
        public string FacturaID { get; set; }
        public string IdVendedor { get; set; }
        public string AM { get; set; }
        public decimal  BMoraBS { get; set; }
        public decimal  BMoraUSD { get; set; }
        public decimal  BSaldoBS { get; set; }
        public decimal  BSaldoUSD { get; set; }
        public string CodFactura { get; set; }
        public string CODSBO { get; set; }
        public int DiaMora { get; set; }
        public string FecEmision { get; set; }
        public string FecVencimiento { get; set; }
        public decimal MoraCarteraBS { get; set; }
        public decimal MoraCarteraUSD { get; set; }
        public decimal MoraFacturaUSD { get; set; }
        public string Moneda { get; set; }
        public decimal MonedaFacturaBS { get; set; }
        public string NombreCliente { get; set; }
        public decimal  PagoBS { get; set; }
        public decimal  PagoUSD { get; set; }
        public decimal  SaldoBS { get; set; }
        public decimal  SaldoUSD { get; set; }
        public string SePaga { get; set; }
        public decimal TCFactura { get; set; }
        public string MCODMora { get; set; }
        public string KUNNR { get; set; }
        public string FechaModificacion { get; set; }
        public int Contado { get; set; }
    }
}