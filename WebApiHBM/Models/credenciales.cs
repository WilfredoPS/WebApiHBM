using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class credenciales
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public string fecha { get; set; }
        public string RolID { get; set; }
        public string OrgVenta { get; set; }
        public string RegionalID { get; set; }

    }
    public class Pedido
    {
        public string PedidoID { get; set; }
    }
    public class Recibo {
        public string ReciboID { get; set; }
    }
    public class Factura
    {
        public string FacturaID { get; set; }
    }
    public class PMActividad
    {
        public string RolID { get; set; }
        public string FechaModificacion { get; set; }
    }
}