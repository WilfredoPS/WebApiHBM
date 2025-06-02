using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Ramo
    {
        public string CodRamo { get; set; }
        public string Denominacion { get; set; }
        public string Division { get; set; }
        public string FechaModificacion { get; set; }
    }

    public class HBM_SubRamo
    {
        public string CodSubRamo { get; set; }
        public string Denominacion { get; set; }
        public string Division { get; set; }
        public string FechaModificacion { get; set; }
    }
}