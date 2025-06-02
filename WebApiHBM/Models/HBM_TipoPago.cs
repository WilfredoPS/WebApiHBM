namespace WebApiHBM.Models
{
    public class HBM_TipoPago
    {
        public string TipoPagoID { get; set; }
        public string CodTipoPago { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string TipoPago { get; set; }
        public string FechaModificacion { get; set; }
        public int Estado { get; set; }
        public int Orden { get; set; }
    }
}