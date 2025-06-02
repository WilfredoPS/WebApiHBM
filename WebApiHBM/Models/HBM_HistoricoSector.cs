using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Historial.Models
{
    public class HBM_HistoricoSector
    {
        public string HistoricoID { get; set; }
        public int Estado { get; set; }
        public string Fecha { get; set; }
        public int Documento { get; set; }
        public string CodUsuario { get; set; }
        public string Obs { get; set; }
        public string EstadoID { get; set; }
        public string NombreEstado { get; set; }
        public string PedidoID { get; set; }
        public string RazonSocial { get; set; }
    
    }

    public class HBM_HistoricoPedido
    {
        public string HistoricoID { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string Documento { get; set; }
        public string NroSAP { get; set; }
        public string CodUsuario { get; set; }
        public string CodUsuarioSAP { get; set; }
        public string Obs { get; set; }
        public string PedidoID { get; set; }
        public string NroPedido { get; set; }
        public string Cliente { get; set; }
        public string Caso { get; set; }

    }

    public class HBM_HistorialFactura
    {
        public string historicoID { get; set; }
        public string facturaID { get; set; }
        public string orgVenta { get; set; }
        public string cuf { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        //public string descripcion { get; set; }
        public string nroDocumento { get; set; }
        public string codUsuario { get; set; }
        public string transaccion { get; set; }
        public string mensaje { get; set; }
        public string mensajeTec { get; set; }
        public string origen { get; set; }

    }
}