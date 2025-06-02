using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Pedido
    {
        public string PedidoID { get; set; }
        public string ActividadID { get; set; }
        public string TareaID { get; set; }
        public string NroPedido { get; set; }
        public string Fecha { get; set; }
        public string NroSAP { get; set; }
        public int CodTipoPedido { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string KUNNR { get; set; }
        public string Moneda { get; set; }
        public decimal MontoTotal { get; set; }
        public string CodTipoPago { get; set; }
        public string KunnrFact { get; set; }
        public string RazonSocial { get; set; }
        public string Nit { get; set; }
        public string DirFactura { get; set; }
        public int CodPrioridadEntrega { get; set; }
        public string KunnrDest { get; set; }
        public string DirEntrega { get; set; }
        public string FechaEntrega { get; set; }
        public string LugarEntrega { get; set; }
        public string FechaCompromisoPago { get; set; }
        public string MonedaCompromisoPago { get; set; }
        public decimal MontoCompromisoPago { get; set; }
        public string ComentarioCompPago { get; set; }
        public decimal Condicion1 { get; set; }
        public decimal Condicion2 { get; set; }
        public decimal Condicion3 { get; set; }
        public decimal Condicion4 { get; set; }
        public string ContactoSolID { get; set; }
        public string ContactoJmID { get; set; }
        public string ContactoRpID { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public string Nota3 { get; set; }
        public decimal TipoCambio { get; set; }
        public int TipoDescCab { get; set; }
        public int Pendiente { get; set; }
        public string TipoSinc { get; set; }
        public int EstadoSync { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string CodUsuario { get; set; }
        public string Obs { get; set; }
        public string FechaModificacion { get; set; }
    }

    public class HBM_Orders
    {
        public string cabecera { get; set; }
        public string detalle { get; set; }
    }

    public class HBM_ped_cab
    {
        //public string zcabepos { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string ClasePedido { get; set; }
        public string kunnr { get; set; }
        public string KunnrDest { get; set; }
        public string KunnrFact { get; set; }
        public string Fecha { get; set; }
        public string HORA_CREA { get; set; }
        public string NroPedido { get; set; }
        public string TipoSinc { get; set; }
        public string FechaEntrega { get; set; }
        public string MontoTotal { get; set; }
        public string CodTipoPago { get; set; }
        public string CodUsuario { get; set; }
        public string PUNTO_RECEP { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public string Obs { get; set; }
        //public string STCEG { get; set; }
        public string Moneda { get; set; }
        public string PedidoID { get; set; }
        public string CodPrioridadEntrega { get; set; }
    }

    public class HBM_Pedido_det
    {
        //public string ZCABEPOS { get; set; }
        public string ProductCode { get; set; }
        public string Cantidad { get; set; }
       // public string KSCHL { get; set; }
        public string Condicion1 { get; set; }
        //public string KSCHL1 { get; set; }
        public string Condicion2 { get; set; }
        //public string KSCHL2 { get; set; }
        public string Condicion3 { get; set; }
        //public string KSCHL3 { get; set; }
        public string Condicion4 { get; set; }
        public string CodUsuario { get; set; }
        public string Lote { get; set; }
        public string Almacen { get; set; }
        public string TipoBono { get; set; }
        //public string PRIORIDAD { get; set; }
        public string CodSector { get; set; }

    }

    public class Root
    {
        public List<HBM_Pedido_det> detalle { get; set; }
    }
}