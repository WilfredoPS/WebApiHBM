using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_TipoPedido
    {
        public string TipoPedidoID { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public int CodigoTipoPedido { get; set; }
        public string Descripcion { get; set; }
        public string CodUsuario { get; set; }
        public int Estado { get; set; }
        public string FechaModificacion { get; set; }
    }
}