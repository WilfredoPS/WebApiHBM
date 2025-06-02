using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_RegimenTributario
    {
        public string RegimenTribID { get; set; }
        public string CodRegimenTrib { get; set; }
        public string Denominacion { get; set; }
        public int Estado{ get; set; }
        public string FechaModificacion { get; set; }
        public decimal Monto { get; set; }
    }
}