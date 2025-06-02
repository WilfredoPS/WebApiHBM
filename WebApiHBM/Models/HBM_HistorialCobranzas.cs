using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_HistorialCobranzas
    {
        public string HistoricoID { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string Tarea { get; set; }
        public string Caso { get; set; }
        public string CodUsuario { get; set; }
        public string CodUsuarioPM { get; set; }
        public string Obs { get; set; }
        public string ReciboID { get; set; }
        public string CodRecibo { get; set; }
        public string Cliente { get; set; }
        public string Comentario { get; set; }
    }
}