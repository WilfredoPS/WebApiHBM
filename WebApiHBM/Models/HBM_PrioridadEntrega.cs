namespace WebApiHBM.Models
{
    public class HBM_PrioridadEntrega
    {
        public string PrioridadEntregaID  { get; set; }
        public int CodPrioridad { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string Prioridad { get; set; }
        public string FechaModificacion { get; set; }
        public int Estado { get; set; }
    }
}