using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Tarea
    {
        public string TareaID { get; set; }
        public string ActividadID { get; set; }
        public int Tarea { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int Estado { get; set; }
        public string Observacion { get; set; }
        public string CodUsuario { get; set; }
    }
    public class HBM_TipoTarea
    {
        public string TipoTareaID { get; set; }
        public string CodTipoTarea { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public int Division { get; set; }
        public string RolID { get; set; }
        public int Estado { get; set; }
        public int TiempoEstimado { get; set; }
    }
    
}