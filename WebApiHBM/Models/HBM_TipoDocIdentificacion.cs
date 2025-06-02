using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_TipoDocIdentificacion
    {
        public string CodTipoDocIden { get; set; }
        public string Denominacion { get; set; }
        public int Estado { get; set; }
        public string FechaModificacion { get; set; }
        public string TipoDocIdenID { get; set; }
        
    }
}