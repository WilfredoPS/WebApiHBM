using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_MotivoNoActividad
    {
        public int CodNoActividad { get; set; }
        public string Evento { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public string DivisionID { get; set; }
        public int Estado { get; set; }
    }
}