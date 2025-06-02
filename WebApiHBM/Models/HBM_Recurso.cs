using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{    
    public class HBM_Recurso
    {
        public string RecursoID { get; set; }
        public string Recurso { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
        public string Tipo { get; set; }
        public string Modulo { get; set; }
        public int Orden { get; set; }
    }
}