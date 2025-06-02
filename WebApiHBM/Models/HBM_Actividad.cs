using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Actividad
    {
        public string ActividadID { get; set; }
        public string CustomerID { get; set; }
        public string Kunnr { get; set; }
        public string Descripcion { get; set; }
        public string CodUsuario { get; set; }
        public string FechaIniPlan { get; set; }
        public string FechaFinPlan { get; set; }
        public string FechaIniReal { get; set; }
        public string FechaFinReal { get; set; }
        public decimal LatitudPlan { get; set; }
        public decimal LongitudPlan { get; set; }
        public decimal LatitudReal { get; set; }
        public decimal LongitudReal { get; set; }
        public string FechaCrea { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string MotivoNoActividad { get; set; }
    }
}