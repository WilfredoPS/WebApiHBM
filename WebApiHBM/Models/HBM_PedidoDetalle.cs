using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_PedidoDetalle
    {
        public string PedidoDetalleID { get; set; }
        public string PedidoID { get; set; }
        public string NroPedido { get; set; }
        public string Fecha { get; set; }
        public string BonoId { get; set; }
        public string TipoBono { get; set; }
        public int CorrelativoBono { get; set; }
        public string CorrelativoVirtual { get; set; }
        public string Almacen { get; set; }
        public string ProductCode { get; set; }
        public string Lote { get; set; }
        public string Producto { get; set; }
        public string CodSector { get; set; }
        public decimal Condicion1 { get; set; }
        public decimal Condicion2 { get; set; }
        public decimal Condicion3 { get; set; }
        public decimal Condicion4 { get; set; }
        public string TipoPM1 { get; set; }
        public string TipoPM2 { get; set; }
        public string TipoPM3 { get; set; }
        public string TipoPM4 { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Moneda { get; set; }
        public decimal SubTotalNeto { get; set; }
        public decimal SubTotalDesc { get; set; }
        public decimal SubTotal { get; set; }
        public string CodigoAlternativa { get; set; }
        public string CodigoSinAlternativa { get; set; }
        public string CodUsuario { get; set; }
        public string FechaModificacion { get; set; }
        public string ProductoAlt { get; set; }
        public string FechaValidezAlt { get; set; }
        public string EstadoRegistro { get; set; }
    }
}