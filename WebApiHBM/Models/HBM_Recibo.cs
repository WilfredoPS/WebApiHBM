using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Recibo
    {
        public string ReciboID { get; set; }
        public string ActividadID { get; set; }
        public string TareaID { get; set; }
        public string CodBanco { get; set; }
        public decimal Descuento { get; set; }
        public string IdVendedor { get; set; }
        public string FechaDoc { get; set; }
        public string MonedaDoc { get; set; }        
        public string KUNNR { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string FechaModificacion { get; set; }
        public string NroDocumento { get; set; }
        public string OtroBanco { get; set; }
        public int ReAnulado { get; set; }
        public string CodRecibo { get; set; }
        public string Confirmacion { get; set; }
        public string DZ1 { get; set; }
        public string DZ2 { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string FormaPago { get; set; }
        public string FormaPagoSAP { get; set; }
        public string SolAnulacion { get; set; }
        public decimal ImporteBS { get; set; }
        public decimal ImporteUSD { get; set; }
        public decimal ImpTotalBS { get; set; }
        public decimal ImpTotalUSD { get; set; }
        public string Moneda { get; set; }
        public string NroReciboManual { get; set; }
        public string Observacion { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal TotalBS { get; set; }
        public decimal TotalUSD { get; set; }
        public string NroPosiciones { get; set; }
        public string Notas { get; set; }
    }
}