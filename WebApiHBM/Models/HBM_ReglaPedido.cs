namespace WebApiHBM.Models
{
    public class HBM_ReglaPedido
    {
        public string ReglaPedidoID { get; set; }
        public string Bonificacion { get; set; }
        public int Grupo { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public int LimitePosicionFactura { get; set; }
        public double MontoMaximoPedido { get; set; }
        public double MontoFactNombre { get; set; }
        public string FechaModificacion { get; set; }
        public string OrgVenta { get; set; }
        public int EsProveedor { get; set; }
        public string MonedaTrabajo { get; set; }
    }
}